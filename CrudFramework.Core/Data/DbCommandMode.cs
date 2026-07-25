using System;

namespace CrudFramework.Core.Data
{
    /// <summary>
    /// Chế độ sinh lệnh giao tiếp DB cho một entity. Cho phép chuyển đổi giữa
    /// gọi stored function (mặc định, an toàn nhất) và sinh SQL thô có tham số hóa.
    ///
    /// Áp dụng ở cấp entity qua <see cref="Attributes.DbTableAttribute"/> hoặc chỉ định
    /// khi khởi tạo client. KHÔNG bao giờ build SQL bằng cách nối chuỗi giá trị người dùng —
    /// mọi giá trị đều truyền qua <c>NpgsqlParameter</c>; chỉ tên bảng/cột/schema mới được
    /// ghép sau khi đã qua whitelist [a-z0-9_].
    /// </summary>
    public enum DbCommandMode
    {
        /// <summary>
        /// (Mặc định) Gọi 4 stored function: fn_&lt;entity&gt;_get/list/upsert/delete.
        /// Toàn bộ logic nằm ở PostgreSQL. An toàn nhất, không sinh SQL động.
        /// </summary>
        Function = 0,

        /// <summary>
        /// Tự sinh SQL thô (SELECT/INSERT/UPDATE/DELETE) từ metadata [DbTable]/[DbColumn].
        /// Giá trị luôn tham số hóa; tên bảng/cột/schema đi qua whitelist. Dùng khi
        /// DB không có sẵn stored function.
        /// </summary>
        RawSql = 1,

        /// <summary>
        /// Kết hợp: mặc định sinh SQL thô, nhưng cho phép override từng câu lệnh bằng SQL
        /// tùy biến (escape hatch) qua <see cref="ISqlOverrideProvider"/>. Dùng khi đa số
        /// thao tác theo pattern chuẩn nhưng một vài trường hợp cần SQL đặc thù.
        /// </summary>
        Hybrid = 2
    }
}
