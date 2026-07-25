using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Core.Data
{
    /// <summary>
    /// Hợp đồng cho client làm việc theo chế độ SQL thô (RawSql/Hybrid). Bề mặt CRUD
    /// giống <see cref="IDbFunctionClient"/> nhưng làm việc trên bảng/cột thay vì function,
    /// và trả/nhận dữ liệu dạng JObject/JArray để đồng nhất với phần còn lại của framework.
    ///
    /// Điểm khác biệt chính: các method nhận thẳng <c>tableName</c> đã schema-qualified
    /// (VD "sales.orders") và danh sách cột được suy ra từ metadata [DbColumn].
    /// KHÔNG có SQL nối chuỗi giá trị: tất cả giá trị đi qua tham số hóa.
    /// </summary>
    public interface ISqlCommandClient
    {
        /// <summary>SELECT ... WHERE id = :id -> 1 record (JObject) hoặc null.</summary>
        Task<JObject> GetAsync(RawSqlRequest request, int? id, CancellationToken ct = default(CancellationToken));

        /// <summary>SELECT ... [WHERE lọc theo filter] -> JArray các record.</summary>
        Task<JArray> ListAsync(RawSqlRequest request, JObject filter, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// INSERT (khi không có id) hoặc UPDATE (khi có id) -> {success,data,errors}.
        /// Trả về record sau khi ghi (RETURNING *).
        /// </summary>
        Task<JObject> UpsertAsync(RawSqlRequest request, JObject payload, CancellationToken ct = default(CancellationToken));

        /// <summary>DELETE ... WHERE id = :id -> {success,message}.</summary>
        Task<JObject> DeleteAsync(RawSqlRequest request, int id, CancellationToken ct = default(CancellationToken));
    }

    /// <summary>
    /// Mô tả bảng/cột đã được resolve từ metadata entity, dùng cho <see cref="ISqlCommandClient"/>.
    /// Mọi tên trong đây PHẢI đã qua whitelist [a-z0-9_] trước khi tạo (xem
    /// <see cref="PostgresRawSqlBuilder"/> / <see cref="Attributes.DbTableAttribute"/>).
    /// </summary>
    public sealed class RawSqlRequest
    {
        /// <summary>Tên bảng đã schema-qualified nếu cần (VD "sales.orders" hoặc "customers").</summary>
        public string QualifiedTable { get; set; }

        /// <summary>Tên cột khóa chính (mặc định "id").</summary>
        public string KeyColumn { get; set; }

        /// <summary>Các cột đọc ra (SELECT). Đã lọc Ignore.</summary>
        public IList<string> SelectColumns { get; set; }

        /// <summary>Các cột được phép ghi (INSERT/UPDATE). Đã lọc Ignore + ReadOnly.</summary>
        public IList<string> WritableColumns { get; set; }

        /// <summary>
        /// (Tùy chọn) SQL override cho từng action ở chế độ Hybrid. Null = dùng SQL tự sinh.
        /// </summary>
        public ISqlOverrideProvider Overrides { get; set; }

        public RawSqlRequest()
        {
            KeyColumn = "id";
            SelectColumns = new List<string>();
            WritableColumns = new List<string>();
        }
    }

    /// <summary>
    /// Escape hatch cho chế độ <see cref="DbCommandMode.Hybrid"/>: cho phép cung cấp SQL
    /// tùy biến thay cho SQL tự sinh cho từng action. Trả null để dùng mặc định.
    ///
    /// SQL trả về PHẢI dùng named parameter (VD :id, :p_filter) và KHÔNG được nối chuỗi
    /// giá trị người dùng. Framework sẽ bind các tham số chuẩn tương ứng.
    /// </summary>
    public interface ISqlOverrideProvider
    {
        /// <summary>SQL cho Get. Tham số: :id. Trả null để dùng SQL tự sinh.</summary>
        string GetSql(RawSqlRequest request);

        /// <summary>SQL cho List. Tham số: :p_filter (jsonb text). Trả null để dùng mặc định.</summary>
        string ListSql(RawSqlRequest request);

        /// <summary>SQL cho Upsert. Tham số: :p_payload (jsonb text). Trả null để dùng mặc định.</summary>
        string UpsertSql(RawSqlRequest request);

        /// <summary>SQL cho Delete. Tham số: :id. Trả null để dùng mặc định.</summary>
        string DeleteSql(RawSqlRequest request);
    }

    /// <summary>
    /// Kiểu dữ liệu của một tham số filter, giúp client (Npgsql) chọn NpgsqlDbType phù hợp.
    ///
    /// Ví dụ: chuỗi filter dùng <see cref="Text"/> để client bind `NpgsqlDbType.Text`.
    /// </summary>
    public enum FilterParamKind
    {
        /// <summary>Chuỗi / ngày dạng text (dùng cho ILIKE hoặc so khớp text).</summary>
        Text = 0,
        /// <summary>Số nguyên (bind bigint).</summary>
        Integer = 1,
        /// <summary>Số thực (bind double).</summary>
        Float = 2,
        /// <summary>Boolean.</summary>
        Bool = 3
    }

    /// <summary>
    /// Một tham số WHERE động do <see cref="PostgresRawSqlBuilder"/> sinh ra khi dựng list SQL
    /// từ filter. Tên tham số (VD "f0") tương ứng với ":f0" trong SQL; giá trị LUÔN được bind
    /// qua NpgsqlParameter, không bao giờ nối chuỗi.
    ///
    /// Ví dụ:
    /// <code>
    /// var p = new FilterParam("f0", "%an%", FilterParamKind.Text);
    /// // SQL tương ứng: WHERE "customer_name" ILIKE :f0
    /// </code>
    /// </summary>
    public sealed class FilterParam
    {
        /// <summary>Tên tham số (không có dấu ":"). VD "f0".</summary>
        public string Name { get; private set; }

        /// <summary>Giá trị bind. Không bao giờ null (null đã xử lý bằng IS NULL trong SQL).</summary>
        public object Value { get; private set; }

        /// <summary>Kiểu dữ liệu để chọn NpgsqlDbType.</summary>
        public FilterParamKind Kind { get; private set; }

        /// <summary>
        /// Tạo tham số filter để client bind vào SQL động.
        /// Ví dụ: <c>new FilterParam("f0", true, FilterParamKind.Bool)</c>.
        /// </summary>
        public FilterParam(string name, object value, FilterParamKind kind)
        {
            Name = name;
            Value = value;
            Kind = kind;
        }
    }
}
