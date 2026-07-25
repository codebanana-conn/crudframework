using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Core.Data
{
    /// <summary>
    /// Triển khai <see cref="ISqlCommandClient"/> trên Npgsql 2.2.3 (net45) cho chế độ
    /// <see cref="DbCommandMode.RawSql"/> / <see cref="DbCommandMode.Hybrid"/>.
    ///
    /// Toàn bộ SQL được sinh bởi <see cref="PostgresRawSqlBuilder"/> (hoặc override qua
    /// <see cref="ISqlOverrideProvider"/>) — luôn tham số hóa giá trị, tên bảng/cột đã qua
    /// whitelist. Để đồng nhất với phần còn lại của framework, kết quả đọc ra được bọc
    /// thành jsonb ngay trong SQL (to_jsonb / json_agg) nên client chỉ đọc 1 giá trị scalar.
    ///
    /// Ghi chú phiên bản (giống NpgsqlFunctionClient):
    ///  - Npgsql 2.2.3 KHÔNG có NpgsqlDbType.Jsonb -> truyền JSON dạng TEXT + CAST(... AS jsonb).
    ///  - API async không đáng tin -> bọc lời gọi đồng bộ trong Task.Run, hỗ trợ hủy tốt nhất có thể.
    ///  - Cú pháp tham số dùng ":ten" (không phải "@ten").
    /// </summary>
    public class NpgsqlSqlCommandClient : ISqlCommandClient
    {
        private readonly string _connectionString;

        /// <summary>Timeout cho mỗi command (giây). Mặc định 30s.</summary>
        public int CommandTimeoutSeconds { get; set; }

        public NpgsqlSqlCommandClient(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string must not be empty.", "connectionString");
            _connectionString = connectionString;
            CommandTimeoutSeconds = 30;
        }

        public Task<JObject> GetAsync(RawSqlRequest request, int? id, CancellationToken ct = default(CancellationToken))
        {
            if (request == null) throw new ArgumentNullException("request");
            return Task.Run(() =>
            {
                // Bọc kết quả 1 record thành jsonb: SELECT to_jsonb(t) FROM (<get sql>) t
                var inner = PostgresRawSqlBuilder.BuildGetSql(request);
                var sql = "SELECT to_jsonb(t) FROM (" + inner + ") t";
                var scalar = ExecuteScalarText(sql, cmd => AddIntParam(cmd, "id", id), ct);
                return ParseObjectOrNull(scalar);
            }, ct);
        }

        public Task<JArray> ListAsync(RawSqlRequest request, JObject filter, CancellationToken ct = default(CancellationToken))
        {
            if (request == null) throw new ArgumentNullException("request");
            return Task.Run(() =>
            {
                // Bọc nhiều record thành jsonb array: SELECT COALESCE(json_agg(t),'[]') FROM (<list sql>) t
                var inner = PostgresRawSqlBuilder.BuildListSql(request);
                var sql = "SELECT COALESCE(jsonb_agg(t), '[]'::jsonb) FROM (" + inner + ") t";
                var json = (filter ?? new JObject()).ToString(Newtonsoft.Json.Formatting.None);
                var scalar = ExecuteScalarText(sql, cmd =>
                {
                    // Chỉ bind :p_filter nếu SQL thực sự dùng (override có thể dùng).
                    if (sql.IndexOf(":p_filter", StringComparison.Ordinal) >= 0)
                        AddJsonParam(cmd, "p_filter", json);
                }, ct);
                return ParseArrayOrEmpty(scalar);
            }, ct);
        }

        public Task<JObject> UpsertAsync(RawSqlRequest request, JObject payload, CancellationToken ct = default(CancellationToken))
        {
            if (request == null) throw new ArgumentNullException("request");
            if (payload == null) throw new ArgumentNullException("payload");
            return Task.Run(() =>
            {
                var inner = PostgresRawSqlBuilder.BuildUpsertSql(request);
                // BuildUpsertSql đã RETURNING to_jsonb(t) -> lấy scalar là record vừa ghi.
                var json = payload.ToString(Newtonsoft.Json.Formatting.None);
                var scalar = ExecuteScalarText(inner, cmd => AddJsonParam(cmd, "p_payload", json), ct);
                var data = ParseObjectOrNull(scalar);
                if (data == null)
                    return Failure("Upsert không trả về dữ liệu.");
                // Chuẩn hóa về dạng {success,data,errors} giống function client.
                return new JObject
                {
                    ["success"] = true,
                    ["data"] = data,
                    ["errors"] = new JArray()
                };
            }, ct);
        }

        public Task<JObject> DeleteAsync(RawSqlRequest request, int id, CancellationToken ct = default(CancellationToken))
        {
            if (request == null) throw new ArgumentNullException("request");
            return Task.Run(() =>
            {
                var inner = PostgresRawSqlBuilder.BuildDeleteSql(request);
                // Bọc để trả số dòng bị xóa dưới dạng jsonb {success, affected}.
                var sql = "WITH d AS (" + inner + " RETURNING 1) "
                          + "SELECT jsonb_build_object('success', COUNT(*) > 0, 'affected', COUNT(*)) FROM d";
                var scalar = ExecuteScalarText(sql, cmd => AddIntParam(cmd, "id", id), ct);
                var result = ParseObjectOrNull(scalar);
                if (result == null)
                    return Failure("Xóa không trả về kết quả.");
                bool ok = result.Value<bool?>("success") == true;
                if (!ok) result["message"] = "Không tìm thấy bản ghi để xóa.";
                return result;
            }, ct);
        }

        // ---------------- infrastructure (đồng bộ với NpgsqlFunctionClient) ----------------

        private string ExecuteScalarText(string sql, Action<NpgsqlCommand> bind, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = CommandTimeoutSeconds;
                        bind(cmd);

                        using (ct.Register(() => { try { cmd.Cancel(); } catch { } }))
                        {
                            var result = cmd.ExecuteScalar();
                            ct.ThrowIfCancellationRequested();
                            if (result == null || result == DBNull.Value)
                                return null;
                            return result.ToString();
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NpgsqlException ex)
            {
                throw new DbFunctionException("Lỗi khi chạy SQL PostgreSQL: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new DbFunctionException("Lỗi không xác định khi chạy SQL: " + ex.Message, ex);
            }
        }

        private static void AddIntParam(NpgsqlCommand cmd, string name, int? value)
        {
            var p = new NpgsqlParameter(name, NpgsqlDbType.Integer);
            p.Value = value.HasValue ? (object)value.Value : DBNull.Value;
            cmd.Parameters.Add(p);
        }

        private static void AddJsonParam(NpgsqlCommand cmd, string name, string jsonText)
        {
            var p = new NpgsqlParameter(name, NpgsqlDbType.Text);
            p.Value = jsonText ?? (object)DBNull.Value;
            cmd.Parameters.Add(p);
        }

        private static JObject ParseObjectOrNull(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            var token = JToken.Parse(text);
            return token as JObject;
        }

        private static JArray ParseArrayOrEmpty(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return new JArray();
            var token = JToken.Parse(text);
            return (token as JArray) ?? new JArray();
        }

        private static JObject Failure(string message)
        {
            return new JObject
            {
                ["success"] = false,
                ["message"] = message,
                ["errors"] = new JArray()
            };
        }
    }
}
