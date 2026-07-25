using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Data;
using CrudFramework.WinForms;
using DevExpress.XtraEditors;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Demo DbCommandMode.Hybrid: RawSql + override ListSql qua ISqlOverrideProvider.
    /// Action khác (Get/Upsert/Delete) dùng SQL tự sinh từ metadata.
    /// </summary>
    public partial class ProductHybridForm : CrudFormBase
    {
        /// <summary>
        /// Override provider: List dùng view riêng, các action khác dùng SQL tự sinh (trả null).
        /// </summary>
        private class ProductOverrides : ISqlOverrideProvider
        {
            public string GetSql(RawSqlRequest r) { return null; }
            // List: dùng view riêng thay vì SELECT * FROM products
            public string ListSql(RawSqlRequest r) { return "SELECT * FROM products WHERE is_available = true"; }
            public string UpsertSql(RawSqlRequest r) { return null; }
            public string DeleteSql(RawSqlRequest r) { return null; }
        }

        public ProductHybridForm(ISqlCommandClient sqlClient, int? id)
        {
            InitializeComponent();

            EntityType = typeof(Product);
            EntityData = new EntityDataClient(typeof(Product), DbCommandMode.Hybrid,
                null, sqlClient, new ProductOverrides());
            BindingProvider = entityBindingProvider1;
            ErrorProvider = dxErrorProvider1;

            btnSave.Click += async (s, e) => await SaveAndCloseAsync();
            btnDelete.Click += async (s, e) => await DeleteAndCloseAsync();
            Load += async (s, e) => await LoadDataAsync(id);
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
