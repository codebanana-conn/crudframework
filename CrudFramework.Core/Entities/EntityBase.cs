using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CrudFramework.Core.Entities
{
    /// <summary>
    /// Base class cho mọi entity. Cung cấp INotifyPropertyChanged để data binding 2 chiều
    /// (WinForms/DevExpress) hoạt động real-time: khi user gõ trên control, property đổi giá trị
    /// và raise PropertyChanged; ngược lại khi property đổi ở code, control cập nhật theo.
    /// </summary>
    [Serializable]
    public abstract class EntityBase : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Set giá trị field và raise PropertyChanged nếu giá trị thay đổi.
        /// Dùng trong setter của property: set { SetField(ref _name, value); }
        /// </summary>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
