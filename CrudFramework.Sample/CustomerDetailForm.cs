using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Data;
using CrudFramework.Core.Entities;
using CrudFramework.WinForms;
using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Detail Form demo cho Customer. Kế thừa CrudFormBase (non-generic, designer-friendly).
    ///
    /// Bước 4 (checklist): chứng minh pipeline Load -> Collect -> Save -> Delete end-to-end.
    /// Ở đây minh họa CÁCH A (EntityBindingProvider) — binding kéo-thả không code:
    ///   trong Designer đã kéo 1 EntityBindingProvider (entityBindingProvider1), set
    ///   EntityType = typeof(Customer), và set BindingMember cho từng editor.
    ///
    /// Đồng thời minh họa hook OnBeforeSave: thêm field tính toán "code_upper" trước khi gửi JSON.
    /// </summary>
    public partial class CustomerDetailForm : CrudFormBase
    {
        public CustomerDetailForm(IDbFunctionClient client, int? id)
        {
            InitializeComponent();

            EntityType = typeof(Customer);
            Client = client;
            BindingProvider = entityBindingProvider1;
            ErrorProvider = dxErrorProvider1;

            btnSave.Click += async (s, e) => await OnSaveClick();
            btnDelete.Click += async (s, e) => await OnDeleteClick();

            AfterSave += (s, e) =>
            {
                XtraMessageBox.Show("Đã lưu thành công (event AfterSave).", "Thông báo");
            };

            Load += async (s, e) => await LoadDataAsync(id);
        }

        private async Task OnSaveClick()
        {
            var ok = await SaveAsync();
            if (ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private async Task OnDeleteClick()
        {
            if (!CurrentId.HasValue) { Close(); return; }
            var confirm = XtraMessageBox.Show("Xóa khách hàng này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            await DeleteAsync(CurrentId.Value);
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void OnBeforeSave(JObject json)
        {
            var code = (string)json["customer_code"];
            if (!string.IsNullOrEmpty(code))
                json["code_upper"] = code.ToUpperInvariant();

            base.OnBeforeSave(json);
        }

        protected override void OnBeforeCollectToJson(JObject json)
        {
            base.OnBeforeCollectToJson(json);
        }

        protected override void OnAfterLoad(object data)
        {
            var customer = data as Customer;
            if (customer != null && !CurrentId.HasValue)
                customer.IsActive = true;
            base.OnAfterLoad(data);
        }
    }
}
