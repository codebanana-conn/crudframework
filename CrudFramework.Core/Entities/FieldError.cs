using System;

namespace CrudFramework.Core.Entities
{
    /// <summary>
    /// Đại diện cho một lỗi validate ở cấp field, khớp với phần tử trong mảng
    /// "errors": [{"field": "...", "message": "..."}] do function Postgres trả về.
    /// </summary>
    [Serializable]
    public sealed class FieldError
    {
        /// <summary>Tên cột DB/JSON bị lỗi (VD "customer_name"). Có thể null nếu là lỗi chung.</summary>
        public string Field { get; set; }

        /// <summary>Thông điệp lỗi hiển thị cho người dùng (tiếng Việt).</summary>
        public string Message { get; set; }

        public FieldError() { }

        public FieldError(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Field) ? Message : (Field + ": " + Message);
        }
    }
}
