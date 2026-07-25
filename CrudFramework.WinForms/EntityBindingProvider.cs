using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CrudFramework.Core.Json;
using CrudFramework.WinForms.Binding;

namespace CrudFramework.WinForms
{
    /// <summary>
    /// Cách (A) trong spec — thành phần cốt lõi cho binding kéo-thả KHÔNG viết code.
    ///
    /// Kéo 1 lần vào Form, set <see cref="EntityType"/> = typeof(TEntity). Component này
    /// "mở rộng" (IExtenderProvider) thêm property <b>BindingMember</b> cho MỌI control trên form
    /// (giống cách ErrorProvider gắn property "Error" cho từng control). Trong Properties Grid,
    /// property BindingMember hiện dropdown danh sách property của entity nhờ
    /// <see cref="BindingMemberTypeConverter"/>.
    ///
    /// Runtime:
    ///  - <see cref="Bind"/>: với mỗi control có BindingMember, gọi Control.DataBindings.Add(...) chuẩn
    ///    (binding 2 chiều thật), và lưu Dictionary&lt;Control,string&gt; (control ↔ tên cột DB).
    ///  - <see cref="GetControlColumnMap"/>: trả map dùng cho error mapping (SetError đúng control).
    ///
    /// Ghi chú kỹ thuật DevExpress: các editor DevExpress (TextEdit, DateEdit, SpinEdit, CheckEdit,
    /// LookUpEdit) đều expose property "EditValue" là điểm bind chuẩn. Với TextEdit ta có thể bind
    /// "EditValue" (khuyến nghị) — DataSourceUpdateMode.OnPropertyChanged để đồng bộ real-time.
    /// </summary>
    [ProvideProperty("BindingMember", typeof(Control))]
    [ToolboxItem(true)]
    [DesignerSerializer(typeof(CodeDomSerializer), typeof(CodeDomSerializer))]
    public class EntityBindingProvider : Component, IExtenderProvider, ISupportInitialize
    {
        // Lưu tên property entity mà mỗi control được gán (design-time + runtime).
        private readonly Dictionary<Control, string> _bindingMembers = new Dictionary<Control, string>();
        // Sau khi Bind(): map control -> tên cột DB (snake_case) để phục vụ error mapping.
        private readonly Dictionary<Control, string> _controlToColumn = new Dictionary<Control, string>();

        private Type _entityType;
        private object _dataSource;
        private string _bindProperty = "EditValue";
        private bool _initializing;
        private bool _useAdapters = true;
        private ControlValueAdapterRegistry _adapterRegistry;

        public EntityBindingProvider() { }
        public EntityBindingProvider(IContainer container)
        {
            if (container != null) container.Add(this);
        }

        /// <summary>Kiểu entity — nguồn để liệt kê danh sách BindingMember trong dropdown design-time.</summary>
        [Category("CrudFramework")]
        [Description("Kiểu entity (POCO có [DbColumn]). Danh sách property của kiểu này hiện trong dropdown BindingMember của mỗi control.")]
        [DefaultValue(null)]
        [TypeConverter(typeof(EntityTypeConverter))]
        public Type EntityType
        {
            get { return _entityType; }
            set { _entityType = value; }
        }

        /// <summary>
        /// Tên property của control dùng để bind (mặc định "EditValue" — chuẩn cho editor DevExpress).
        /// Với control WinForms thuần có thể đổi thành "Text".
        /// </summary>
        [Category("CrudFramework")]
        [Description("Tên property trên control dùng để bind. DevExpress editor: 'EditValue'. WinForms thuần: 'Text'.")]
        [DefaultValue("EditValue")]
        public string BindProperty
        {
            get { return _bindProperty; }
            set { _bindProperty = string.IsNullOrEmpty(value) ? "EditValue" : value; }
        }

        /// <summary>
        /// Nếu true (mặc định): tự phát hiện property bind của MỖI control qua
        /// <see cref="IControlValueAdapter"/> (TextBox→"Text", CheckBox→"Checked",
        /// DevExpress→"EditValue"...), giúp bind được control WinForms thuần lẫn DevExpress
        /// mà không cần set <see cref="BindProperty"/> thủ công.
        /// Nếu false: dùng cứng <see cref="BindProperty"/> cho mọi control (hành vi cũ).
        /// </summary>
        [Category("CrudFramework")]
        [Description("Tự phát hiện property bind theo loại control (WinForms/DevExpress) qua adapter. Tắt để dùng cứng BindProperty.")]
        [DefaultValue(true)]
        public bool UseAdapters
        {
            get { return _useAdapters; }
            set { _useAdapters = value; }
        }

        /// <summary>
        /// Registry adapter dùng để phát hiện property bind. Mặc định
        /// <see cref="ControlValueAdapterRegistry.Default"/>. Gán registry riêng nếu cần
        /// adapter tùy biến. Không serialize ra Designer.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ControlValueAdapterRegistry AdapterRegistry
        {
            get { return _adapterRegistry ?? ControlValueAdapterRegistry.Default; }
            set { _adapterRegistry = value; }
        }

        /// <summary>Entity instance đang được chỉnh sửa (gán ở runtime, thường bởi CrudFormBase).</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        // ---------------- IExtenderProvider ----------------

        /// <summary>Chỉ mở rộng cho Control (không mở rộng cho chính component/form).</summary>
        public bool CanExtend(object extendee)
        {
            return extendee is Control && !(extendee is Form);
        }

        /// <summary>Getter cho property mở rộng "BindingMember" của mỗi control.</summary>
        [Category("CrudFramework")]
        [Description("Tên property của entity mà control này bind tới. Chọn từ dropdown.")]
        [DefaultValue("")]
        [Editor(typeof(BindingMemberUIEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(BindingMemberTypeConverter))]
        public string GetBindingMember(Control control)
        {
            string v;
            return _bindingMembers.TryGetValue(control, out v) ? v : string.Empty;
        }

        /// <summary>Setter cho property mở rộng "BindingMember".</summary>
        public void SetBindingMember(Control control, string value)
        {
            if (string.IsNullOrEmpty(value))
                _bindingMembers.Remove(control);
            else
                _bindingMembers[control] = value;
        }

        // ---------------- runtime binding ----------------

        /// <summary>
        /// Thiết lập binding 2 chiều thật cho tất cả control đã khai BindingMember.
        /// Gọi khi form load xong entity (CrudFormBase làm việc này).
        /// </summary>
        public void Bind(object entity)
        {
            _dataSource = entity;
            _controlToColumn.Clear();
            if (entity == null) return;

            var lookup = _entityType != null
                ? EntityJsonMapper.GetColumnLookup(_entityType)
                : null;

            // đảo lookup: propertyName -> columnName
            var propToCol = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (_entityType != null)
                foreach (var c in EntityJsonMapper.GetColumns(_entityType))
                    if (!c.Ignore && !propToCol.ContainsKey(c.PropertyName))
                        propToCol[c.PropertyName] = c.ColumnName;

            foreach (var kv in _bindingMembers)
            {
                var control = kv.Key;
                var member = kv.Value;
                if (control == null || string.IsNullOrEmpty(member)) continue;

                control.DataBindings.Clear();

                // Xác định property bind + update mode: ưu tiên adapter (nếu bật), fallback BindProperty.
                string bindProp = _bindProperty;
                var updateMode = DataSourceUpdateMode.OnPropertyChanged;
                if (_useAdapters)
                {
                    var adapter = AdapterRegistry.Resolve(control);
                    if (adapter != null)
                    {
                        bindProp = adapter.GetBindProperty(control);
                        updateMode = adapter.GetUpdateMode(control);
                    }
                }

                var binding = new System.Windows.Forms.Binding(bindProp, entity, member, true, updateMode);
                control.DataBindings.Add(binding);

                string col;
                if (!propToCol.TryGetValue(member, out col))
                    col = EntityJsonMapper.ToSnakeCase(member);
                _controlToColumn[control] = col;
            }
        }

        /// <summary>Map control -> tên cột DB, dùng cho error mapping (DXErrorProvider.SetError).</summary>
        public IDictionary<Control, string> GetControlColumnMap()
        {
            return new Dictionary<Control, string>(_controlToColumn);
        }

        /// <summary>Tìm control tương ứng với 1 tên cột lỗi (đảo map). Trả null nếu không có.</summary>
        public Control FindControlByColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) return null;
            foreach (var kv in _controlToColumn)
                if (string.Equals(kv.Value, columnName, StringComparison.OrdinalIgnoreCase))
                    return kv.Key;
            return null;
        }

        // ---------------- ISupportInitialize ----------------
        public void BeginInit() { _initializing = true; }
        public void EndInit() { _initializing = false; }

        internal IEnumerable<string> GetEntityMemberNames()
        {
            if (_entityType == null) return Enumerable.Empty<string>();
            return EntityJsonMapper.GetColumns(_entityType)
                .Where(c => !c.Ignore)
                .Select(c => c.PropertyName);
        }
    }
}
