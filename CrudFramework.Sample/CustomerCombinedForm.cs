using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Data;
using CrudFramework.Core.Entities;
using CrudFramework.Core.Json;
using CrudFramework.WinForms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Form gộp: Grid danh sách + Detail fields在同一 form.
    /// Không tách List/Detail — chọn dòng trên grid -> hiển thị xuống phía dưới,
    /// save/delete ngay trên cùng form. Phù hợp màn hình CRUD đơn giản.
    /// </summary>
    public partial class CustomerCombinedForm : XtraForm
    {
        private readonly IDbFunctionClient _client;
        private Customer _current;
        private int? _currentId;
        private BindingList<Customer> _dataSource;

        public CustomerCombinedForm(IDbFunctionClient client)
        {
            InitializeComponent();
            _client = client;

            // Grid events
            gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;

            // Button events
            btnAdd.Click += (s, e) => ClearDetail();
            btnSave.Click += async (s, e) => await SaveAsync();
            btnDelete.Click += async (s, e) => await DeleteAsync();
            btnRefresh.Click += async (s, e) => await LoadListAsync();
            btnOpenDetailForm.Click += (s, e) => OpenDetailForm();

            Load += async (s, e) => await LoadListAsync();
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var entity = gridView1.GetFocusedRow() as Customer;
            if (entity == null) return;
            ShowDetail(entity);
        }

        // ==================== LIST ====================

        private async Task LoadListAsync()
        {
            var arr = await _client.ListAsync("fn_customers_list", new JObject());
            var list = EntityJsonMapper.FromJArray<Customer>(arr);
            _dataSource = new BindingList<Customer>(list);
            gridControl1.DataSource = _dataSource;
        }

        // ==================== DETAIL ====================

        private void ShowDetail(Customer entity)
        {
            _current = entity;
            _currentId = entity.Id > 0 ? (int?)entity.Id : null;

            txtCode.EditValue = entity.CustomerCode;
            txtName.EditValue = entity.CustomerName;
            dtBirth.EditValue = entity.BirthDate;
            spBalance.EditValue = entity.Balance;
            chkActive.EditValue = entity.IsActive;

            lblStatus.Text = _currentId.HasValue
                ? string.Format("Đang sửa: ID {0}", _currentId.Value)
                : "Thêm mới";
        }

        private void ClearDetail()
        {
            _current = new Customer();
            _currentId = null;

            txtCode.EditValue = null;
            txtName.EditValue = null;
            dtBirth.EditValue = null;
            spBalance.EditValue = 0;
            chkActive.EditValue = true;

            txtCode.Focus();
            lblStatus.Text = "Thêm mới";
        }

        // ==================== SAVE ====================

        private async Task SaveAsync()
        {
            // Thu thập từ control
            var entity = _current ?? new Customer();
            entity.CustomerCode = (string)txtCode.EditValue;
            entity.CustomerName = (string)txtName.EditValue;
            entity.BirthDate = (DateTime?)dtBirth.EditValue;
            entity.Balance = Convert.ToDecimal(spBalance.EditValue ?? 0m);
            entity.IsActive = (bool)(chkActive.EditValue ?? true);

            if (_currentId.HasValue)
                entity.Id = _currentId.Value;

            // Serialize theo [DbColumn]
            var json = EntityJsonMapper.ToJObject(entity, forUpsert: true);

            // Hook: thêm field tính toán (demo OnBeforeSave)
            var code = (string)json["customer_code"];
            if (!string.IsNullOrEmpty(code))
                json["code_upper"] = code.ToUpperInvariant();

            var result = await _client.UpsertAsync("fn_customers_upsert", json);

            bool success = result != null && result.Value<bool?>("success") == true;
            if (!success)
            {
                var errors = EntityJsonMapper.ParseErrors(result?["errors"]);
                var msg = string.Empty;
                foreach (var e in errors) msg += e.Field + ": " + e.Message + "\n";
                XtraMessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật Id mới nếu insert
            var data = result["data"] as JObject;
            if (data != null)
            {
                var newId = data.Value<int?>("id");
                if (newId.HasValue)
                {
                    entity.Id = newId.Value;
                    _currentId = newId;
                }
            }

            XtraMessageBox.Show("Đã lưu thành công.", "Thông báo");
            await LoadListAsync();
        }

        // ==================== DELETE ====================

        private async Task DeleteAsync()
        {
            if (!_currentId.HasValue)
            {
                XtraMessageBox.Show("Chọn bản ghi cần xóa.", "Thông báo");
                return;
            }

            var confirm = XtraMessageBox.Show("Xóa khách hàng này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var result = await _client.DeleteAsync("fn_customers_delete", _currentId.Value);
            bool ok = result != null && result.Value<bool?>("success") == true;
            if (!ok)
            {
                XtraMessageBox.Show(result != null ? (string)result["message"] : "Xóa thất bại.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClearDetail();
            await LoadListAsync();
        }

        // ==================== MỞ DETAIL FORM RIÊNG ====================

        private void OpenDetailForm()
        {
            using (var frm = new CustomerDetailForm(_client, _currentId))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    var _ = LoadListAsync();
                }
            }
        }
    }
}
