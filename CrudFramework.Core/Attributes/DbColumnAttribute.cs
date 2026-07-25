using System;

namespace CrudFramework.Core.Attributes
{
    /// <summary>
    /// Ánh xạ một property của entity tới một cột trong JSON/DB, đồng thời khai báo
    /// metadata để tự sinh cột lưới (GridControl) khi tạo List Form.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class DbColumnAttribute : Attribute
    {
        /// <summary>Tên cột trong JSON/DB (snake_case). Nếu null sẽ suy ra từ tên property.</summary>
        public string Name { get; set; }

        /// <summary>Caption tiếng Việt hiển thị trên header lưới / label.</summary>
        public string Caption { get; set; }

        /// <summary>Độ rộng cột mặc định trên GridControl (0 = auto).</summary>
        public int Width { get; set; }

        /// <summary>Format string cho DisplayFormat (VD "n0", "dd/MM/yyyy", "#,##0.00").</summary>
        public string Format { get; set; }

        /// <summary>Thứ tự hiển thị cột trên lưới (nhỏ hơn hiện trước).</summary>
        public int Order { get; set; }

        /// <summary>Nếu true: KHÔNG serialize vào JSON và KHÔNG tự sinh cột lưới.</summary>
        public bool Ignore { get; set; }

        /// <summary>Nếu true: cột chỉ đọc (không sinh vào JSON upsert nhưng vẫn hiện trên lưới).</summary>
        public bool ReadOnly { get; set; }

        /// <summary>Ẩn cột này khỏi lưới (nhưng vẫn serialize vào JSON). VD khóa ngoại kỹ thuật.</summary>
        public bool HiddenInGrid { get; set; }

        public DbColumnAttribute()
        {
            Order = int.MaxValue;
        }

        public DbColumnAttribute(string name)
            : this()
        {
            Name = name;
        }
    }
}
