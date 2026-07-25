using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrudFramework.WinForms.Binding
{
    /// <summary>
    /// Registry chọn <see cref="IControlValueAdapter"/> phù hợp cho một control theo thứ tự
    /// ưu tiên. Mặc định: thử <see cref="DevExpressEditorAdapter"/> trước (control có "EditValue"),
    /// sau đó tới <see cref="StandardWinFormsControlAdapter"/> (WinForms thuần).
    ///
    /// Có thể tùy biến: <see cref="Register"/> thêm adapter riêng (chèn lên đầu để ưu tiên cao),
    /// hoặc <see cref="Default"/> để dùng registry dùng chung toàn ứng dụng.
    ///
    /// Ví dụ đăng ký adapter tùy biến:
    /// <code>
    /// ControlValueAdapterRegistry.Default.Register(new MyColorPickerAdapter());
    /// </code>
    /// </summary>
    public class ControlValueAdapterRegistry
    {
        private readonly List<IControlValueAdapter> _adapters = new List<IControlValueAdapter>();

        /// <summary>Registry dùng chung mặc định (đã đăng ký DevExpress + WinForms chuẩn).</summary>
        public static ControlValueAdapterRegistry Default { get; private set; }

        static ControlValueAdapterRegistry()
        {
            Default = CreateDefault();
        }

        /// <summary>Tạo một registry mới đã nạp sẵn các adapter tích hợp.</summary>
        public static ControlValueAdapterRegistry CreateDefault()
        {
            var r = new ControlValueAdapterRegistry();
            // Ưu tiên DevExpress trước (nếu control là editor DevExpress -> "EditValue"),
            // fallback về WinForms chuẩn cho mọi control còn lại.
            r._adapters.Add(new DevExpressEditorAdapter());
            r._adapters.Add(new StandardWinFormsControlAdapter());
            return r;
        }

        /// <summary>Thêm adapter vào ĐẦU danh sách (ưu tiên cao nhất).</summary>
        public void Register(IControlValueAdapter adapter)
        {
            if (adapter == null) throw new ArgumentNullException("adapter");
            _adapters.Insert(0, adapter);
        }

        /// <summary>Xóa toàn bộ adapter (dùng khi muốn cấu hình lại từ đầu).</summary>
        public void Clear()
        {
            _adapters.Clear();
        }

        /// <summary>
        /// Tìm adapter đầu tiên xử lý được control. Trả null nếu không có (thực tế luôn có
        /// vì <see cref="StandardWinFormsControlAdapter"/> nhận mọi control).
        /// </summary>
        public IControlValueAdapter Resolve(Control control)
        {
            if (control == null) return null;
            foreach (var a in _adapters)
                if (a.CanHandle(control))
                    return a;
            return null;
        }

        /// <summary>
        /// Tiện ích: trả tên property bind cho control (theo adapter phù hợp), hoặc
        /// <paramref name="fallback"/> nếu không tìm được adapter.
        /// </summary>
        public string ResolveBindProperty(Control control, string fallback)
        {
            var a = Resolve(control);
            return a != null ? a.GetBindProperty(control) : fallback;
        }
    }
}
