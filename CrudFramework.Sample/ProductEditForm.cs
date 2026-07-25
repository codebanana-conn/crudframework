using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Data;
using CrudFramework.WinForms;
using DevExpress.XtraEditors;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Lớp trung gian non-generic để Designer load được form kế thừa CrudFormBase&lt;Product&gt;.
    /// </summary>
    public abstract class ProductFormBase : CrudFormBase<Product> { }

    /// <summary>
    /// Demo đủ kiểu dữ liệu SQL/DbColumn: int, string, decimal, bool, DateTime?,
    /// ReadOnly, HiddenInGrid, Ignore, Format. Sử dụng các editor DevExpress tương ứng:
    /// TextEdit, SpinEdit, DateEdit, CheckEdit, MemoEdit, LookUpEdit.
    /// </summary>
    public partial class ProductEditForm : ProductFormBase
    {
        public ProductEditForm(IDbFunctionClient client, int? id)
        {
            InitializeComponent();

            Client = client;
            BindingProvider = entityBindingProvider1;
            ErrorProvider = dxErrorProvider1;

            btnSave.Click += async (s, e) => await OnSaveClick();
            btnDelete.Click += async (s, e) => await OnDeleteClick();

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
            var confirm = XtraMessageBox.Show("Xóa sản phẩm này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            await DeleteAsync(CurrentId.Value);
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void OnAfterLoad(Product data)
        {
            if (data != null && !CurrentId.HasValue)
                data.IsAvailable = true;
            base.OnAfterLoad(data);
        }
    }
}
