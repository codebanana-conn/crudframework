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

            // Client cho mode Function (gọi fn_xxx).
            var functionClient = new NpgsqlFunctionClient(connectionString)
            {
                CommandTimeoutSeconds = 30
            };

            // Client cho mode RawSql/Hybrid (build SQL tham số hoá).
            var sqlClient = new NpgsqlSqlCommandClient(connectionString)
            {
                CommandTimeoutSeconds = 30
            };

            // Launcher gom mọi demo. Muốn mở thẳng 1 form -> thay bằng, ví dụ:
            //   Application.Run(new CustomerCombinedForm(functionClient));
            Application.Run(new DemoLauncherForm(functionClient, sqlClient));
        }
    }
}
