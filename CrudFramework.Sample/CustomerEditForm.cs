using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Data;
using CrudFramework.WinForms;
using DevExpress.XtraEditors;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Lớp trung gian non-generic để Windows Forms Designer LOAD ĐƯỢC form kế thừa
    /// <see cref="CrudFormBase{Customer}"/>.
    ///
    /// Vì sao cần lớp này?
    ///   Designer KHÔNG hỗ trợ form kế thừa TRỰC TIẾP một base class generic
    ///   (CrudFormBase&lt;Customer&gt;) — sẽ báo lỗi "base class ... could not be loaded".
    ///   Chèn 1 lớp trung gian non-generic vào giữa là cách chuẩn để né giới hạn này:
    ///
    ///     CrudFormBase (non-generic)
    ///        └─ CrudFormBase&lt;Customer&gt; (generic — KHÔNG cho Form Designer kế thừa trực tiếp)
    ///              └─ CustomerFormBase (non-generic — Designer load OK)   ← lớp này
    ///                    └─ CustomerEditForm (partial, có Designer)        ← form thực tế
    ///
    /// Lưu ý: lớp trung gian phải là non-generic và KHÔNG có Designer riêng
    /// (không cần file .Designer.cs cho chính nó).
    /// </summary>
    public abstract class CustomerFormBase : CrudFormBase<Customer>
    {
    }

    /// <summary>
    /// Form demo chứng minh pattern "generic base class + lớp trung gian non-generic".
    /// Kế thừa <see cref="CustomerFormBase"/> (non-generic) nên Designer mở được bình thường,
    /// đồng thời vẫn hưởng typed <c>Current</c> kiểu <see cref="Customer"/> từ generic base.
    ///
    /// Cách dùng:
    ///   using (var frm = new CustomerEditForm(client, id)) { frm.ShowDialog(); }
    /// </summary>
    public partial class CustomerEditForm : CustomerFormBase
    {
        /// <summary>
        /// Khởi tạo form. EntityType đã được generic base tự set = typeof(Customer),
        /// nên ở đây chỉ cần gán Client + BindingProvider + ErrorProvider.
        /// </summary>
        /// <param name="client">Client gọi function/SQL DB.</param>
        /// <param name="id">Id bản ghi cần mở (null = thêm mới).</param>
        public CustomerEditForm(IDbFunctionClient client, int? id)
        {
            InitializeComponent();

            // EntityType = typeof(Customer) đã được set sẵn trong constructor của CrudFormBase<Customer>.
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
            var confirm = XtraMessageBox.Show("Xóa khách hàng này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            await DeleteAsync(CurrentId.Value);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>Ví dụ dùng typed Current từ generic base: bật IsActive mặc định khi thêm mới.</summary>
        protected override void OnAfterLoad(Customer data)
        {
            if (data != null && !CurrentId.HasValue)
                data.IsActive = true;
            base.OnAfterLoad(data);
        }
    }
}
