using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CrudFramework.Core.Entities;
using DevExpress.XtraEditors.DXErrorProvider;

namespace CrudFramework.WinForms.ErrorMapping
{
    /// <summary>
    /// Cầu nối giữa danh sách FieldError (từ JSON lỗi của function) và DXErrorProvider trên form.
    /// Dùng map control↔column do EntityBindingProvider cung cấp để SetError đúng control.
    /// </summary>
    public class DxErrorProviderAdapter
    {
        private readonly DXErrorProvider _provider;
        private readonly EntityBindingProvider _binding;

        public DxErrorProviderAdapter(DXErrorProvider provider, EntityBindingProvider binding)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            if (binding == null) throw new ArgumentNullException("binding");
            _provider = provider;
            _binding = binding;
        }

        /// <summary>Xóa toàn bộ lỗi đang hiển thị.</summary>
        public void Clear()
        {
            _provider.ClearErrors();
        }

        /// <summary>
        /// Đổ danh sách lỗi lên đúng control. Trả về danh sách lỗi KHÔNG map được control nào
        /// (VD lỗi chung không có field, hoặc field không có control) để caller hiển thị dạng message box.
        /// </summary>
        public IList<FieldError> Apply(IEnumerable<FieldError> errors)
        {
            _provider.ClearErrors();
            var unmapped = new List<FieldError>();
            if (errors == null) return unmapped;

            foreach (var err in errors)
            {
                Control control = string.IsNullOrEmpty(err.Field)
                    ? null
                    : _binding.FindControlByColumn(err.Field);

                if (control != null)
                    _provider.SetError(control, err.Message ?? "Lỗi không xác định.");
                else
                    unmapped.Add(err);
            }
            return unmapped;
        }
    }
}
