using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Data;
using CrudFramework.WinForms;
using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Demo binding bằng control WinForms chuẩn (TextBox, NumericUpDown, CheckBox), không dùng
    /// editor DevExpress. Mục tiêu: chứng minh EntityBindingProvider tự chọn property bind qua
    /// adapter (Text/Value/Checked) thay vì phụ thuộc EditValue.
    /// </summary>
    public partial class CustomerPlainWinFormsForm : CrudFormBase
    {
        public CustomerPlainWinFormsForm(IDbFunctionClient client, int? id)
        {
            InitializeComponent();

            EntityType = typeof(Customer);
            Client = client;
            BindingProvider = entityBindingProvider1;
            ErrorProvider = dxErrorProvider1;

            btnSave.Click += async (s, e) => await SaveAndCloseAsync();
            btnCancel.Click += (s, e) => Close();
            Load += async (s, e) => await LoadDataAsync(id);
        }

        private async Task SaveAndCloseAsync()
        {
            var ok = await SaveAsync();
            if (!ok) return;
            XtraMessageBox.Show("Đã lưu bằng control WinForms chuẩn.", "Thông báo");
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void OnAfterLoad(object data)
        {
            var customer = data as Customer;
            if (customer != null && !CurrentId.HasValue)
                customer.IsActive = true;
            base.OnAfterLoad(data);
        }

        protected override void OnBeforeSave(JObject json)
        {
            var code = (string)json["customer_code"];
            if (!string.IsNullOrEmpty(code))
                json["code_upper"] = code.ToUpperInvariant();
            base.OnBeforeSave(json);
        }
    }
}
