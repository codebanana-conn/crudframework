using System;
using System.Windows.Forms;

namespace CrudFramework.WinForms.Binding
{
    /// <summary>
    /// Adapter cho các control WinForms chuẩn phổ biến — KHÔNG phụ thuộc DevExpress.
    /// Ánh xạ loại control -> property bind hợp lý:
    ///  - CheckBox / RadioButton -> "Checked"
    ///  - DateTimePicker         -> "Value"
    ///  - NumericUpDown          -> "Value"
    ///  - ComboBox (DropDownList)-> "SelectedValue" (nếu có DataSource) hoặc "Text"
    ///  - TextBoxBase & mặc định -> "Text"
    /// </summary>
    public class StandardWinFormsControlAdapter : IControlValueAdapter
    {
        public bool CanHandle(Control control)
        {
            // Adapter mặc định: xử lý mọi control WinForms không phải editor DevExpress.
            // Registry sẽ ưu tiên DevExpress adapter trước nên ở đây trả true cho phần còn lại.
            return control != null;
        }

        public string GetBindProperty(Control control)
        {
            if (control is CheckBox || control is RadioButton) return "Checked";
            if (control is DateTimePicker) return "Value";
            if (control is NumericUpDown) return "Value";

            var combo = control as ComboBox;
            if (combo != null)
                return combo.DataSource != null ? "SelectedValue" : "Text";

            // TextBox, RichTextBox, Label, Button... mặc định "Text".
            return "Text";
        }

        public DataSourceUpdateMode GetUpdateMode(Control control)
        {
            // Text bind real-time gây khó chịu khi gõ giữa chừng với một số kiểu dữ liệu;
            // nhưng để đồng nhất trải nghiệm 2 chiều, dùng OnPropertyChanged.
            return DataSourceUpdateMode.OnPropertyChanged;
        }
    }

    /// <summary>
    /// Adapter cho editor DevExpress (TextEdit, CheckEdit, DateEdit, SpinEdit, LookUpEdit...).
    /// Nhận diện bằng cách kiểm tra control có property "EditValue" (duck-typing qua reflection)
    /// nên KHÔNG cần tham chiếu compile-time tới DevExpress trong lớp này — vẫn hoạt động khi
    /// control là BaseEdit của DevExpress.
    /// </summary>
    public class DevExpressEditorAdapter : IControlValueAdapter
    {
        public bool CanHandle(Control control)
        {
            if (control == null) return false;
            // Editor DevExpress đều có property "EditValue" (object) đọc/ghi được.
            var pi = control.GetType().GetProperty("EditValue");
            return pi != null && pi.CanRead && pi.CanWrite;
        }

        public string GetBindProperty(Control control)
        {
            return "EditValue";
        }

        public DataSourceUpdateMode GetUpdateMode(Control control)
        {
            return DataSourceUpdateMode.OnPropertyChanged;
        }
    }
}
