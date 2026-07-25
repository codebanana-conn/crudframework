using System;
using System.ComponentModel;
using System.Windows.Forms;
using CrudFramework.Core.Json;

namespace CrudFramework.WinForms
{
    /// <summary>
    /// BindingSource chuyên dụng: dùng khi cần DataSource THẬT (LookUpEdit cần danh sách lookup,
    /// GridControl con trong form master-detail...). Bổ sung tiện ích lấy metadata cột từ [DbColumn].
    ///
    /// Đây là cách (B) trong spec — tồn tại song song với EntityBindingProvider (cách A), không loại trừ.
    /// </summary>
    [ToolboxItem(true)]
    public class EntityBindingSource : BindingSource
    {
        private Type _entityType;

        public EntityBindingSource() { }
        public EntityBindingSource(IContainer container) : base(container) { }

        /// <summary>
        /// Kiểu entity mà binding source này phục vụ. Khi set, tự khởi tạo DataSource
        /// rỗng (typeof để designer/binding biết danh sách property) nếu chưa có.
        /// </summary>
        [Category("CrudFramework")]
        [Description("Kiểu entity (POCO có [DbColumn]) mà binding source phục vụ.")]
        [DefaultValue(null)]
        [TypeConverter(typeof(EntityTypeConverter))]
        public Type EntityType
        {
            get { return _entityType; }
            set
            {
                _entityType = value;
                if (value != null && DataSource == null)
                {
                    // Gán typeof để BindingSource expose danh sách property cho binding/design-time.
                    DataSource = value;
                }
            }
        }

        /// <summary>Trả về metadata cột (đã cache) của EntityType — tiện cho auto-gen cột grid con.</summary>
        public ColumnMap[] GetColumns()
        {
            if (_entityType == null) return new ColumnMap[0];
            return EntityJsonMapper.GetColumns(_entityType);
        }
    }
}
