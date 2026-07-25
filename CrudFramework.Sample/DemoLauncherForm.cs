using System;
using System.Windows.Forms;
using CrudFramework.Core.Data;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Form khởi chạy (launcher) liệt kê tất cả demo của CrudFramework.Sample.
    ///
    /// Dùng khi nào: chạy ứng dụng để duyệt nhanh từng kịch bản demo (Function/RawSql/Hybrid/
    /// LookUpEdit/error-mapping/control WinForms chuẩn) mà không cần sửa <see cref="Program"/>.
    ///
    /// Vì sao: gom mọi form demo vào 1 điểm vào (entry point) trực quan, mỗi nút mở 1 form demo
    /// với đúng client (IDbFunctionClient cho mode Function; ISqlCommandClient cho RawSql/Hybrid).
    ///
    /// Lưu ý: cố tình KHÔNG có file Designer + KHÔNG kế thừa base DevExpress — chỉ là
    /// <see cref="Form"/> WinForms thuần, dựng control bằng code → tránh phụ thuộc Designer/licenses.
    /// </summary>
    public class DemoLauncherForm : Form
    {
        private readonly IDbFunctionClient _functionClient;
        private readonly ISqlCommandClient _sqlClient;

        /// <summary>
        /// Khởi tạo launcher với 2 client dùng chung cho mọi demo.
        /// </summary>
        /// <param name="functionClient">Client gọi SQL function (mode Function) — bắt buộc.</param>
        /// <param name="sqlClient">Client raw SQL (mode RawSql/Hybrid) — bắt buộc cho demo Product RawSql/Hybrid.</param>
        public DemoLauncherForm(IDbFunctionClient functionClient, ISqlCommandClient sqlClient)
        {
            if (functionClient == null) throw new ArgumentNullException("functionClient");
            _functionClient = functionClient;
            _sqlClient = sqlClient;

            InitializeLayout();
        }

        private void InitializeLayout()
        {
            Text = "CrudFramework — Demo Launcher";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new System.Drawing.Size(460, 470);

            var lblTitle = new Label
            {
                Text = "Chọn demo để mở:",
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Location = new System.Drawing.Point(16, 14)
            };
            Controls.Add(lblTitle);

            var panel = new FlowLayoutPanel
            {
                Location = new System.Drawing.Point(16, 48),
                Size = new System.Drawing.Size(428, 408),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };
            Controls.Add(panel);

            // Mỗi nút mô tả rõ mode/kịch bản để người xem demo hiểu ngay.
            AddButton(panel, "1. Customer — Combined (grid + detail)",
                () => new CustomerCombinedForm(_functionClient));

            AddButton(panel, "2. Customer — List + Detail tách rời",
                () => new CustomerListForm(_functionClient));

            AddButton(panel, "3. Customer — Edit Form (mode Function)",
                () => new CustomerEditForm(_functionClient, null));

            AddButton(panel, "4. Customer — Control WinForms chuẩn (ComboBox/TextBox…)",
                () => new CustomerPlainWinFormsForm(_functionClient, null));

            AddButton(panel, "5. Customer — Demo error-mapping (lỗi field → control)",
                () => new ErrorMappingDemoForm(_functionClient, null));

            AddButton(panel, "6. Product — Edit Form (generic base qua lớp trung gian)",
                () => new ProductEditForm(_functionClient, null));

            AddButton(panel, "7. Product — LookUpEdit (EntityBindingSource / Cách B)",
                () => new ProductLookupForm(_functionClient, null));

            AddButton(panel, "8. Product — RawSql (NpgsqlSqlCommandClient)",
                () => CreateSqlDemo(id => new ProductRawSqlForm(_sqlClient, id)));

            AddButton(panel, "9. Product — Hybrid (RawSql + override ListSql)",
                () => CreateSqlDemo(id => new ProductHybridForm(_sqlClient, id)));
        }

        /// <summary>
        /// Thêm 1 nút mở form demo; factory tạo form khi bấm (không tạo sẵn để tiết kiệm tài nguyên).
        /// </summary>
        private void AddButton(FlowLayoutPanel panel, string caption, Func<Form> factory)
        {
            var btn = new Button
            {
                Text = caption,
                Width = 400,
                Height = 34,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 0, 8)
            };
            btn.Click += (s, e) =>
            {
                try
                {
                    var frm = factory();
                    if (frm == null) return;
                    frm.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this,
                        "Không mở được demo:\r\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            panel.Controls.Add(btn);
        }

        /// <summary>
        /// Bảo vệ demo cần <see cref="ISqlCommandClient"/>: nếu null thì báo rõ thay vì ném lỗi.
        /// </summary>
        private Form CreateSqlDemo(Func<int?, Form> factory)
        {
            if (_sqlClient == null)
            {
                MessageBox.Show(this,
                    "Demo này cần ISqlCommandClient (NpgsqlSqlCommandClient) nhưng chưa được cấu hình.",
                    "Thiếu cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return factory(null);
        }
    }
}
