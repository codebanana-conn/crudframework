using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CrudFramework.Core.Attributes;
using CrudFramework.Core.Data;
using CrudFramework.Core.Entities;
using CrudFramework.Core.Json;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;

namespace CrudFramework.WinForms
{
    /// <summary>
    /// Base cho List Form (màn hình danh sách) dùng DevExpress GridControl.
    ///
    /// - LoadListAsync(filter): gọi fn_&lt;entity&gt;_list -> JArray -> BindingList&lt;TEntity&gt; -> Grid.DataSource.
    /// - AutoGenerateColumns(): tự sinh GridColumn từ [DbColumn] (caption VN, width, format, order),
    ///   bỏ qua Ignore/HiddenInGrid. Dev vẫn có thể override cột thủ công sau đó.
    /// - DeleteSelectedAsync(): gọi fn_&lt;entity&gt;_delete cho dòng đang chọn (kèm xác nhận), refresh grid.
    ///
    /// Form con chỉ cần: gán Grid + GridView (kéo-thả), gán Client, gọi InitializeGrid() 1 lần
    /// rồi LoadListAsync(). Mở Detail Form tùy dự án (override OpenDetail).
    /// </summary>
    public class CrudListFormBase<TEntity> : XtraForm where TEntity : EntityBase, new()
    {
        private readonly DbTableAttribute _table;

        public IDbFunctionClient Client { get; set; }
        public GridControl Grid { get; set; }
        public GridView View { get; set; }

        /// <summary>Nếu true (mặc định) sẽ tự sinh cột từ [DbColumn] khi InitializeGrid().</summary>
        public bool AutoColumns { get; set; }

        /// <summary>Nguồn dữ liệu hiện tại (2 chiều — sửa trực tiếp trên grid nếu cần).</summary>
        public BindingList<TEntity> DataSource { get; private set; }

        public CrudListFormBase()
        {
            AutoColumns = true;
            _table = (DbTableAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(DbTableAttribute));
            if (_table == null)
                throw new InvalidOperationException(
                    "Entity " + typeof(TEntity).Name + " thiếu [DbTable].");
        }

        /// <summary>Gọi 1 lần sau khi Grid/View đã gán. Sinh cột tự động nếu AutoColumns.</summary>
        public virtual void InitializeGrid()
        {
            if (Grid == null || View == null)
                throw new InvalidOperationException("Cần gán Grid và View trước khi InitializeGrid().");

            View.OptionsBehavior.Editable = false; // list mặc định read-only
            View.OptionsView.ColumnAutoWidth = false;
            View.DoubleClick += View_DoubleClick;

            if (AutoColumns)
                AutoGenerateColumns();
        }

        /// <summary>Tự sinh GridColumn từ metadata [DbColumn]. Chỉ tạo cột chưa tồn tại (cho phép override tay).</summary>
        public virtual void AutoGenerateColumns()
        {
            View.Columns.Clear();
            int visibleIndex = 0;
            foreach (var c in EntityJsonMapper.GetColumns(typeof(TEntity)))
            {
                if (c.Ignore || c.HiddenInGrid) continue;

                var col = View.Columns.AddVisible(c.PropertyName);
                col.Caption = c.Caption;
                col.VisibleIndex = visibleIndex++;
                if (c.Width > 0) col.Width = c.Width;
                if (!string.IsNullOrEmpty(c.Format))
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    col.DisplayFormat.FormatString = c.Format;
                }
                col.OptionsColumn.AllowEdit = false;
            }
        }

        /// <summary>Gọi fn_list với filter -> đổ vào grid.</summary>
        public virtual async Task LoadListAsync(JObject filter = null)
        {
            if (Client == null) throw new InvalidOperationException("Client chưa được gán.");
            var arr = await Client.ListAsync(_table.GetFunctionName("list"), filter).ConfigureAwait(true);
            var list = EntityJsonMapper.FromJArray<TEntity>(arr);
            DataSource = new BindingList<TEntity>(list);
            Grid.DataSource = DataSource;
        }

        /// <summary>Lấy entity ở dòng đang focus (null nếu không có).</summary>
        public TEntity GetFocusedEntity()
        {
            if (View == null) return null;
            return View.GetFocusedRow() as TEntity;
        }

        /// <summary>Xóa dòng đang chọn (kèm hộp xác nhận), refresh grid sau khi xóa.</summary>
        public virtual async Task DeleteSelectedAsync()
        {
            var entity = GetFocusedEntity();
            if (entity == null) return;

            int id = GetEntityId(entity);
            if (id <= 0) return;

            var confirm = XtraMessageBox.Show(
                "Bạn có chắc muốn xóa bản ghi này?", "Xác nhận",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question);
            if (confirm != System.Windows.Forms.DialogResult.Yes) return;

            var result = await Client.DeleteAsync(_table.GetFunctionName("delete"), id).ConfigureAwait(true);
            bool ok = result != null && result.Value<bool?>("success") == true;
            if (!ok)
            {
                XtraMessageBox.Show(result != null ? (string)result["message"] : "Xóa thất bại.",
                    "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            await LoadListAsync().ConfigureAwait(true);
        }

        /// <summary>Đọc giá trị Id của entity (property tên "Id" hoặc cột "id").</summary>
        protected virtual int GetEntityId(TEntity entity)
        {
            var prop = typeof(TEntity).GetProperty("Id");
            if (prop != null)
            {
                var v = prop.GetValue(entity, null);
                if (v != null) return Convert.ToInt32(v);
            }
            return 0;
        }

        // ---- điều hướng sang Detail Form: form con override ----

        /// <summary>Double-click 1 dòng -> mở Detail theo id.</summary>
        protected virtual void View_DoubleClick(object sender, EventArgs e)
        {
            var entity = GetFocusedEntity();
            if (entity == null) return;
            OpenDetail(GetEntityId(entity));
        }

        /// <summary>Mở Detail Form. id null = thêm mới. Form con triển khai (mở CrudFormBase tương ứng).</summary>
        public virtual void OpenDetail(int? id)
        {
            // Mặc định để trống — form con override để mở Detail Form cụ thể rồi refresh grid sau khi đóng.
        }

        /// <summary>Tiện ích: nút "Thêm mới".</summary>
        public void AddNew() { OpenDetail(null); }
    }
}
