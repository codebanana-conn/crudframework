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
    /// List Form demo. Kế thừa CrudListFormBase&lt;Customer&gt;.
    /// Grid tự sinh cột từ [DbColumn]. Double-click / nút Sửa -> Detail. Thêm mới / Xóa.
    /// Có ô filter keyword gửi vào fn_customers_list qua p_filter (JSON).
    /// </summary>
    public partial class CustomerListForm : CrudListFormBase<Customer>
    {
        private readonly IDbFunctionClient _client;

        public CustomerListForm(IDbFunctionClient client)
        {
            InitializeComponent();
            _client = client;

            // Gán hạ tầng cho base.
            Client = client;
            Grid = gridControl1;
            View = gridView1;

            InitializeGrid(); // sinh cột tự động từ [DbColumn]

            btnAdd.Click += (s, e) => AddNew();
            btnEdit.Click += (s, e) => { var en = GetFocusedEntity(); if (en != null) OpenDetail(en.Id); };
            btnDelete.Click += async (s, e) => await DeleteSelectedAsync();
            btnSearch.Click += async (s, e) => await ReloadWithFilter();

            Load += async (s, e) => await LoadListAsync();
        }

        private async Task ReloadWithFilter()
        {
            var filter = new JObject();
            var kw = txtKeyword.Text != null ? txtKeyword.Text.Trim() : string.Empty;
            if (!string.IsNullOrEmpty(kw)) filter["keyword"] = kw;
            await LoadListAsync(filter);
        }

        /// <summary>Mở Detail Form theo id (null = thêm mới), refresh grid sau khi đóng.</summary>
        public override void OpenDetail(int? id)
        {
            using (var frm = new CustomerDetailForm(_client, id))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    // refresh (fire & forget an toàn trên UI thread)
                    var _ = LoadListAsync();
                }
            }
        }
    }
}
