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
    /// Demo DbCommandMode.RawSql: cùng entity Product nhưng chạy qua NpgsqlSqlCommandClient
    /// + PostgresRawSqlBuilder (không cần SQL function), có filter WHERE động.
    /// </summary>
    public partial class ProductRawSqlForm : CrudFormBase
    {
        private ISqlCommandClient _sqlClient;

        public ProductRawSqlForm(ISqlCommandClient sqlClient, int? id)
        {
            InitializeComponent();

            EntityType = typeof(Product);
            EntityData = new EntityDataClient(typeof(Product), DbCommandMode.RawSql, null, sqlClient);
            BindingProvider = entityBindingProvider1;
            ErrorProvider = dxErrorProvider1;
            _sqlClient = sqlClient;

            btnSave.Click += async (s, e) => await SaveAndCloseAsync();
            btnDelete.Click += async (s, e) => await DeleteAndCloseAsync();
            btnSearch.Click += async (s, e) => await SearchAsync();
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

        /// <summary>Demo filter WHERE động: nhập keyword → ListAsync(filter).</summary>
        private async Task SearchAsync()
        {
            var keyword = txtSearch.Text;
            var filter = new JObject();
            if (!string.IsNullOrEmpty(keyword))
                filter["product_name"] = keyword;

            var arr = await EntityData.ListAsync(filter);
            var count = arr != null ? arr.Count : 0;
            XtraMessageBox.Show(string.Format("Tìm thấy {0} sản phẩm.", count), "Kết quả");
        }
    }
}
