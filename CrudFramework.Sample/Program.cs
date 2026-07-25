using System;
using System.Windows.Forms;
using CrudFramework.Core.Data;

namespace CrudFramework.Sample
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ===== THAY CONNECTION STRING CHO PHÙ HỢP =====
            var connectionString =
                "Host=localhost;Port=5432;Database=binh_tamphuc_loi_sa_emr;Username=postgres;Password=123456;";

            var client = new NpgsqlFunctionClient(connectionString)
            {
                CommandTimeoutSeconds = 30
            };

            // Form mặc định: gộp grid + detail trên cùng 1 form.
            // Muốn tách list/detail riêng -> đổi thành: Application.Run(new CustomerListForm(client));
            Application.Run(new CustomerCombinedForm(client));
        }
    }
}
