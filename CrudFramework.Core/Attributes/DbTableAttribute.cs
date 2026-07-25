using System;

namespace CrudFramework.Core.Attributes
{
    /// <summary>
    /// Đánh dấu một entity class ánh xạ tới một bảng/entity trong PostgreSQL và
    /// khai báo prefix tên function (fn_&lt;entity&gt;_get/list/upsert/delete).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class DbTableAttribute : Attribute
    {
        /// <summary>Tên logic của entity, dùng để suy ra tên function. VD: "customers".</summary>
        public string Name { get; private set; }

        /// <summary>
        /// Prefix cho các function. Mặc định "fn_". Function sẽ là
        /// {FunctionPrefix}{Name}_get / _list / _upsert / _delete.
        /// </summary>
        public string FunctionPrefix { get; set; }

        public DbTableAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Table/entity name must not be empty.", "name");
            Name = name;
            FunctionPrefix = "fn_";
        }

        public string GetFunctionName(string action)
        {
            return FunctionPrefix + Name + "_" + action;
        }
    }
}
