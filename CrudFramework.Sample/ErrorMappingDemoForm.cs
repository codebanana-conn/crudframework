using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Data;
using CrudFramework.WinForms;
using DevExpress.XtraEditors;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Demo error-mapping: cố tình trigger lỗi validate (trùng mã KH001) để thấy
    /// DxErrorProviderAdapter set lỗi đúng control (txtCode). Save → DB trả lỗi
    /// {field: customer_code, message: "Mã khách hàng đã tồn tại."} → hiển thị đỏ trên txtCode.
    /// </summary>
    public partial class ErrorMappingDemoForm : CrudFormBase
    {
        public ErrorMappingDemoForm(IDbFunctionClient client, int? id)
        {
            InitializeComponent();

            EntityType = typeof(Customer);
            Client = client;
            BindingProvider = entityBindingProvider1;
            ErrorProvider = dxErrorProvider1;

            btnSave.Click += async (s, e) => await OnSaveClick();
            btnHint.Click += (s, e) => XtraMessageBox.Show(
                "Demo: gõ mã KH001 (đã tồn tại trong DB) rồi bấm Lưu.\n" +
                "Bạn sẽ thấy lỗi hiển thị đỏ ngay trên ô Mã KH — đó là DxErrorProviderAdapter " +
                "map lỗi field customer_code → control txtCode.",
                "Hướng dẫn");
            Load += async (s, e) => await LoadDataAsync(id);
        }

        private async Task OnSaveClick()
        {
            var ok = await SaveAsync();
            if (ok)
                XtraMessageBox.Show("Lưu thành công.", "Thông báo");
        }
    }
}
