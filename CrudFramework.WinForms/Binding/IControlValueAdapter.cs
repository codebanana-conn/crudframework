using System;
using System.Windows.Forms;

namespace CrudFramework.WinForms.Binding
{
    /// <summary>
    /// Trừu tượng hóa "property nào của control dùng để bind giá trị" — cho phép binding
    /// hoạt động với cả control WinForms chuẩn (TextBox, CheckBox, DateTimePicker, NumericUpDown...)
    /// LẪN editor DevExpress (TextEdit, CheckEdit, DateEdit, SpinEdit...) mà KHÔNG cần tham chiếu
    /// trực tiếp DevExpress trong tầng binding.
    ///
    /// Ý tưởng: mỗi adapter tự nhận biết loại control nó hỗ trợ (<see cref="CanHandle"/>) và
    /// trả về tên property dùng để tạo <see cref="System.Windows.Forms.Binding"/>
    /// (<see cref="GetBindProperty"/>). <see cref="ControlValueAdapterRegistry"/> chọn adapter
    /// phù hợp theo thứ tự ưu tiên.
    ///
    /// Nhờ vậy Form dùng control WinForms thuần vẫn bind được mà không đụng tới DevExpress,
    /// còn Form dùng DevExpress vẫn bind vào "EditValue" như trước.
    /// </summary>
    public interface IControlValueAdapter
    {
        /// <summary>Adapter này có xử lý được control đã cho không?</summary>
        bool CanHandle(Control control);

        /// <summary>
        /// Tên property trên control dùng để bind (VD "Text", "Checked", "Value", "EditValue").
        /// Chỉ gọi khi <see cref="CanHandle"/> trả true.
        /// </summary>
        string GetBindProperty(Control control);

        /// <summary>
        /// Chế độ cập nhật nguồn (mặc định OnPropertyChanged để đồng bộ real-time).
        /// Một số control chỉ nên cập nhật khi Validating (VD tránh raise liên tục) có thể đổi tại đây.
        /// </summary>
        DataSourceUpdateMode GetUpdateMode(Control control);
    }
}
