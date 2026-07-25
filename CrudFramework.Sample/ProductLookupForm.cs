using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Data;
using CrudFramework.Core.Json;
using CrudFramework.WinForms;
using DevExpress.XtraEditors;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Demo EntityBindingSource cho LookUpEdit: Product có CategoryId →
    /// LookUpEdit lấy danh sách Category qua EntityBindingSource (Cách B).
    /// </summary>
    public partial class ProductLookupForm : CrudFormBase
    {
        private IDbFunctionClient _client;

        public ProductLookupForm(IDbFunctionClient client, int? id)
        {
            InitializeComponent();

            EntityType = typeof(Product);
            Client = client;
            BindingProvider = entityBindingProvider1;
            ErrorProvider = dxErrorProvider1;
            _client = client;

            btnSave.Click += async (s, e) => await SaveAndCloseAsync();
            btnDelete.Click += async (s, e) => await DeleteAndCloseAsync();
            Load += async (s, e) => await InitAndLoadAsync(id);
        }

        private async Task InitAndLoadAsync(int? id)
        {
            // Load danh sách Category vào EntityBindingSource (Cách B)
            var catArr = await _client.ListAsync("fn_categories_list", new Newtonsoft.Json.Linq.JObject());
            var catList = EntityJsonMapper.FromJArray<Category>(catArr);
            categoryBindingSource.DataSource = new BindingList<Category>(catList);

            // Bind LookUpEdit
            lookUpEditCategory.Properties.DataSource = categoryBindingSource;
            lookUpEditCategory.Properties.DisplayMember = "CategoryName";
            lookUpEditCategory.Properties.ValueMember = "Id";

            await LoadDataAsync(id);
        }

        private async Task SaveAndCloseAsync()
        {
            var ok = await SaveAsync();
            if (ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private async Task DeleteAndCloseAsync()
        {
            if (!CurrentId.HasValue) { Close(); return; }
            var confirm = XtraMessageBox.Show("Xóa sản phẩm?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            await DeleteAsync(CurrentId.Value);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
