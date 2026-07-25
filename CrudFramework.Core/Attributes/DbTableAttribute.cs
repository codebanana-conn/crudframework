using System;

namespace CrudFramework.Core.Attributes
{
    /// <summary>
    /// Đánh dấu một entity class ánh xạ tới một bảng/entity trong PostgreSQL và
    /// khai báo prefix tên function (fn_&lt;entity&gt;_get/list/upsert/delete).
    ///
    /// Hỗ trợ đa schema PostgreSQL: nếu <see cref="Schema"/> được set (VD "sales"),
    /// tên function trả về sẽ được schema-qualified: <c>sales.fn_customers_get</c>.
    /// Nếu <see cref="Schema"/> để trống -> dùng search_path mặc định của kết nối
    /// (thường là "public"), function trả về không có prefix schema.
    ///
    /// Ví dụ:
    /// <code>
    /// // Entity ở schema mặc định (public):
    /// [DbTable("customers")]
    /// public class Customer : EntityBase { ... }
    /// // -> GetFunctionName("get") = "fn_customers_get"
    ///
    /// // Entity ở schema "sales":
    /// [DbTable("orders", Schema = "sales")]
    /// public class Order : EntityBase { ... }
    /// // -> GetFunctionName("get") = "sales.fn_orders_get"
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class DbTableAttribute : Attribute
    {
        private string _schema;

        /// <summary>Tên logic của entity, dùng để suy ra tên function. VD: "customers".</summary>
        public string Name { get; private set; }

        /// <summary>
        /// Prefix cho các function. Mặc định "fn_". Function sẽ là
        /// {FunctionPrefix}{Name}_get / _list / _upsert / _delete.
        /// </summary>
        public string FunctionPrefix { get; set; }

        /// <summary>
        /// Schema PostgreSQL chứa function (VD "sales", "hr"). Mặc định null/empty
        /// -> không schema-qualify, dùng search_path của kết nối.
        ///
        /// Chỉ chấp nhận identifier hợp lệ [a-z0-9_], bắt đầu bằng chữ cái hoặc '_'.
        /// Nếu set giá trị không hợp lệ sẽ ném <see cref="ArgumentException"/> để
        /// chặn triệt để nguy cơ SQL injection qua tên schema.
        /// </summary>
        public string Schema
        {
            get { return _schema; }
            set { _schema = ValidateIdentifierOrNull(value, "Schema"); }
        }

        public DbTableAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Table/entity name must not be empty.", "name");
            Name = name;
            FunctionPrefix = "fn_";
        }

        /// <summary>
        /// Trả về tên function cho một action ("get" | "list" | "upsert" | "delete").
        /// Nếu <see cref="Schema"/> có giá trị -> schema-qualified ("schema.fn_name_action").
        /// </summary>
        public string GetFunctionName(string action)
        {
            var fn = FunctionPrefix + Name + "_" + action;
            return string.IsNullOrEmpty(_schema) ? fn : _schema + "." + fn;
        }

        /// <summary>
        /// Kiểm tra một identifier PostgreSQL (schema/tên) chỉ gồm [a-z0-9_] và không
        /// bắt đầu bằng chữ số. Trả về null nếu đầu vào rỗng; ném exception nếu không hợp lệ.
        /// Đây là whitelist an toàn — mọi ghép chuỗi vào SQL phải đi qua đây.
        /// </summary>
        internal static string ValidateIdentifierOrNull(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            var v = value.Trim();

            char first = v[0];
            if (!(char.IsLetter(first) || first == '_'))
                throw new ArgumentException(
                    "Identifier phải bắt đầu bằng chữ cái hoặc '_': " + value, paramName);

            foreach (var ch in v)
            {
                // Chỉ cho phép chữ thường/hoa ASCII, chữ số và gạch dưới.
                bool ok = (ch >= 'a' && ch <= 'z')
                          || (ch >= 'A' && ch <= 'Z')
                          || (ch >= '0' && ch <= '9')
                          || ch == '_';
                if (!ok)
                    throw new ArgumentException(
                        "Identifier chỉ được chứa [a-z0-9_]: " + value, paramName);
            }
            return v;
        }
    }
}
