using System;
using System.Collections.Generic;
using System.Text;
using CrudFramework.Core.Attributes;
using CrudFramework.Core.Json;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Core.Data
{
    /// <summary>
    /// Sinh <see cref="RawSqlRequest"/> và các câu SQL tham số hóa cho chế độ
    /// <see cref="DbCommandMode.RawSql"/> / <see cref="DbCommandMode.Hybrid"/>, dựa trên
    /// metadata [DbTable]/[DbColumn] của entity.
    ///
    /// NGUYÊN TẮC AN TOÀN:
    ///  - Giá trị người dùng KHÔNG bao giờ nối vào SQL — luôn qua NpgsqlParameter (:id, :p_payload...).
    ///  - Tên bảng/cột/schema CHỈ được ghép sau khi qua whitelist [a-z0-9_]
    ///    (<see cref="DbTableAttribute.ValidateIdentifierOrNull"/>).
    ///  - Mọi identifier còn được bọc trong dấu nháy kép "..." để an toàn với keyword.
    ///
    /// SQL sinh ra tương thích PostgreSQL, dùng jsonb để nạp payload nên tương thích tốt với
    /// pattern function sẵn có (payload là 1 object jsonb).
    /// </summary>
    public static class PostgresRawSqlBuilder
    {
        /// <summary>
        /// Dựng <see cref="RawSqlRequest"/> từ entity type: resolve tên bảng schema-qualified,
        /// danh sách cột đọc/ghi từ [DbColumn]. Ném exception nếu entity thiếu [DbTable].
        /// </summary>
        public static RawSqlRequest BuildRequest(Type entityType, ISqlOverrideProvider overrides = null)
        {
            if (entityType == null) throw new ArgumentNullException("entityType");

            var table = (DbTableAttribute)Attribute.GetCustomAttribute(entityType, typeof(DbTableAttribute));
            if (table == null)
                throw new InvalidOperationException(
                    "Entity " + entityType.Name + " thiếu [DbTable] — không thể sinh SQL thô.");

            // Tên bảng logic = Name (đã kiểm tra rỗng ở attribute). Vẫn validate lại để chắc chắn.
            var tableName = DbTableAttribute.ValidateIdentifierOrNull(table.Name, "Name");
            if (tableName == null)
                throw new InvalidOperationException("Tên bảng không hợp lệ: " + table.Name);

            var qualified = string.IsNullOrEmpty(table.Schema)
                ? Quote(tableName)
                : Quote(table.Schema) + "." + Quote(tableName);

            var req = new RawSqlRequest
            {
                QualifiedTable = qualified,
                KeyColumn = string.IsNullOrEmpty(table.KeyColumn) ? "id" : table.KeyColumn,
                Overrides = overrides
            };

            foreach (var c in EntityJsonMapper.GetColumns(entityType))
            {
                if (c.Ignore) continue;
                var col = DbTableAttribute.ValidateIdentifierOrNull(c.ColumnName, "ColumnName");
                if (col == null) continue; // bỏ qua cột có tên không hợp lệ (không đưa vào SQL)

                req.SelectColumns.Add(col);
                if (!c.ReadOnly)
                    req.WritableColumns.Add(col);
            }

            return req;
        }

        /// <summary>
        /// SELECT các cột WHERE key = :id. VD:
        /// <c>SELECT "id","customer_name" FROM "sales"."customers" WHERE "id" = :id</c>
        /// (Npgsql 2.2.3 dùng cú pháp tham số ":ten" — đồng bộ với NpgsqlFunctionClient.)
        /// </summary>
        public static string BuildGetSql(RawSqlRequest req)
        {
            if (req.Overrides != null)
            {
                var o = req.Overrides.GetSql(req);
                if (!string.IsNullOrEmpty(o)) return o;
            }
            return "SELECT " + JoinQuoted(req.SelectColumns)
                   + " FROM " + req.QualifiedTable
                   + " WHERE " + Quote(req.KeyColumn) + " = :id";
        }

        /// <summary>
        /// SELECT toàn bộ (mặc định không WHERE). Lọc chi tiết theo filter jsonb được ủy thác
        /// cho tầng client (tùy driver). VD:
        /// <c>SELECT "id","customer_name" FROM "customers"</c>
        /// </summary>
        public static string BuildListSql(RawSqlRequest req)
        {
            if (req.Overrides != null)
            {
                var o = req.Overrides.ListSql(req);
                if (!string.IsNullOrEmpty(o)) return o;
            }
            return "SELECT " + JoinQuoted(req.SelectColumns)
                   + " FROM " + req.QualifiedTable;
        }

        /// <summary>
        /// SELECT ... với WHERE động AN TOÀN dựng từ <paramref name="filter"/> (jsonb).
        ///
        /// NGUYÊN TẮC AN TOÀN:
        ///  - Chỉ những key của filter TRÙNG với một cột đã whitelist (<see cref="RawSqlRequest.SelectColumns"/>)
        ///    mới được đưa vào WHERE. Key lạ (không phải cột) bị BỎ QUA hoàn toàn — không thể chèn SQL.
        ///  - Mỗi giá trị được gán vào 1 named parameter riêng (:f0, :f1, ...) — KHÔNG nối chuỗi giá trị.
        ///  - Tên cột lấy từ danh sách đã whitelist + bọc "..." nên an toàn với keyword.
        ///
        /// Ngữ nghĩa giá trị filter:
        ///  - null            -> "col" IS NULL
        ///  - chuỗi           -> "col" ILIKE :fN  (tìm gần đúng, bọc %...% ở tham số)
        ///  - số/bool/khác    -> "col" = :fN      (so khớp chính xác)
        ///
        /// Nếu Hybrid có override ListSql -> trả override, KHÔNG dựng WHERE (client tự bind theo override).
        /// Nếu filter rỗng/không có key hợp lệ -> tương đương <see cref="BuildListSql(RawSqlRequest)"/>.
        ///
        /// Ví dụ:
        /// <code>
        /// IList&lt;FilterParam&gt; ps;
        /// var sql = PostgresRawSqlBuilder.BuildListSql(req, filter, out ps);
        /// // SELECT ... FROM "customers" WHERE "customer_name" ILIKE :f0
        /// </code>
        /// </summary>
        /// <param name="req">Request đã resolve (cột đã whitelist).</param>
        /// <param name="filter">Filter dạng JObject (có thể null).</param>
        /// <param name="parameters">Danh sách (tên tham số -> giá trị) để client bind. Không bao giờ null.</param>
        public static string BuildListSql(RawSqlRequest req, JObject filter, out IList<FilterParam> parameters)
        {
            parameters = new List<FilterParam>();

            if (req.Overrides != null)
            {
                var o = req.Overrides.ListSql(req);
                if (!string.IsNullOrEmpty(o)) return o;
            }

            var baseSql = "SELECT " + JoinQuoted(req.SelectColumns) + " FROM " + req.QualifiedTable;
            if (filter == null || filter.Count == 0 || req.SelectColumns == null || req.SelectColumns.Count == 0)
                return baseSql;

            // Tập cột hợp lệ (đã whitelist) để đối chiếu nhanh, không phân biệt hoa/thường.
            var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var c in req.SelectColumns) allowed.Add(c);

            var clauses = new List<string>();
            int idx = 0;
            foreach (var prop in filter.Properties())
            {
                var key = prop.Name;
                if (!allowed.Contains(key))
                    continue; // key không phải cột hợp lệ -> bỏ qua (an toàn).

                // Lấy đúng tên cột đã whitelist (giữ nguyên casing trong SelectColumns).
                string col = key;
                foreach (var c in req.SelectColumns)
                    if (string.Equals(c, key, StringComparison.OrdinalIgnoreCase)) { col = c; break; }

                var token = prop.Value;
                if (token == null || token.Type == JTokenType.Null)
                {
                    clauses.Add(Quote(col) + " IS NULL");
                    continue;
                }

                var pname = "f" + idx.ToString(System.Globalization.CultureInfo.InvariantCulture);
                idx++;

                if (token.Type == JTokenType.String)
                {
                    // Tìm gần đúng, case-insensitive. Bọc %...% ở GIÁ TRỊ (tham số), không ở SQL.
                    clauses.Add(Quote(col) + " ILIKE :" + pname);
                    parameters.Add(new FilterParam(pname, "%" + token.Value<string>() + "%", FilterParamKind.Text));
                }
                else if (token.Type == JTokenType.Boolean)
                {
                    clauses.Add(Quote(col) + " = :" + pname);
                    parameters.Add(new FilterParam(pname, token.Value<bool>(), FilterParamKind.Bool));
                }
                else if (token.Type == JTokenType.Integer)
                {
                    clauses.Add(Quote(col) + " = :" + pname);
                    parameters.Add(new FilterParam(pname, token.Value<long>(), FilterParamKind.Integer));
                }
                else if (token.Type == JTokenType.Float)
                {
                    clauses.Add(Quote(col) + " = :" + pname);
                    parameters.Add(new FilterParam(pname, token.Value<double>(), FilterParamKind.Float));
                }
                else
                {
                    // Kiểu khác (date...) -> so khớp bằng chuỗi, để driver/DB tự cast.
                    clauses.Add(Quote(col) + " = :" + pname);
                    parameters.Add(new FilterParam(pname, token.ToString(), FilterParamKind.Text));
                }
            }

            if (clauses.Count == 0)
                return baseSql;

            return baseSql + " WHERE " + string.Join(" AND ", clauses.ToArray());
        }


        /// <summary>
        /// UPSERT dựa trên jsonb payload, dùng ON CONFLICT theo key. Payload được nạp bằng
        /// jsonb_populate_record để map cột an toàn (không nối chuỗi giá trị). VD dạng:
        /// <code>
        /// INSERT INTO "customers" AS t
        /// SELECT * FROM jsonb_populate_record(NULL::"customers", CAST(:p_payload AS jsonb))
        /// ON CONFLICT ("id") DO UPDATE SET "customer_name" = EXCLUDED."customer_name", ...
        /// RETURNING to_jsonb(t)
        /// </code>
        /// </summary>
        public static string BuildUpsertSql(RawSqlRequest req)
        {
            if (req.Overrides != null)
            {
                var o = req.Overrides.UpsertSql(req);
                if (!string.IsNullOrEmpty(o)) return o;
            }

            var sb = new StringBuilder();
            sb.Append("INSERT INTO ").Append(req.QualifiedTable).Append(" AS t ");
            sb.Append("SELECT * FROM jsonb_populate_record(NULL::")
              .Append(req.QualifiedTable).Append(", CAST(:p_payload AS jsonb)) ");
            sb.Append("ON CONFLICT (").Append(Quote(req.KeyColumn)).Append(") DO UPDATE SET ");

            var sets = new List<string>();
            foreach (var col in req.WritableColumns)
            {
                if (string.Equals(col, req.KeyColumn, StringComparison.OrdinalIgnoreCase))
                    continue; // không update cột khóa
                sets.Add(Quote(col) + " = EXCLUDED." + Quote(col));
            }
            // Nếu không có cột nào để update (chỉ có key) -> DO NOTHING an toàn.
            if (sets.Count == 0)
                sb.Append("NOTHING");
            else
                sb.Append(string.Join(", ", sets.ToArray()));

            sb.Append(" RETURNING to_jsonb(t)");
            return sb.ToString();
        }

        /// <summary>
        /// DELETE WHERE key = :id. VD: <c>DELETE FROM "customers" WHERE "id" = :id</c>
        /// </summary>
        public static string BuildDeleteSql(RawSqlRequest req)
        {
            if (req.Overrides != null)
            {
                var o = req.Overrides.DeleteSql(req);
                if (!string.IsNullOrEmpty(o)) return o;
            }
            return "DELETE FROM " + req.QualifiedTable
                   + " WHERE " + Quote(req.KeyColumn) + " = :id";
        }

        // ---- helpers ----

        /// <summary>Bọc identifier trong dấu nháy kép. Identifier PHẢI đã qua whitelist trước đó.</summary>
        private static string Quote(string identifier)
        {
            // identifier đã qua whitelist [a-z0-9_] nên không thể chứa dấu nháy kép,
            // nhưng vẫn double-up cho chắc chắn theo chuẩn PostgreSQL.
            return "\"" + identifier.Replace("\"", "\"\"") + "\"";
        }

        private static string JoinQuoted(IList<string> columns)
        {
            if (columns == null || columns.Count == 0) return "*";
            var parts = new List<string>(columns.Count);
            foreach (var c in columns) parts.Add(Quote(c));
            return string.Join(", ", parts.ToArray());
        }
    }
}
