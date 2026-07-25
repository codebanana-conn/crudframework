using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CrudFramework.Core.Attributes;
using CrudFramework.Core.Entities;

namespace CrudFramework.WinForms
{
    /// <summary>
    /// TypeConverter cho property mở rộng "BindingMember": cung cấp danh sách property của
    /// EntityType (khai trên EntityBindingProvider) để Properties Grid hiện dạng dropdown.
    /// </summary>
    public class BindingMemberTypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        // Cho phép gõ tay nếu muốn (không ép buộc chỉ chọn trong danh sách).
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var provider = ResolveProvider(context);
            if (provider == null)
                return new StandardValuesCollection(new string[0]);

            var names = provider.GetEntityMemberNames().OrderBy(n => n).ToList();
            names.Insert(0, string.Empty); // cho phép bỏ chọn
            return new StandardValuesCollection(names);
        }

        internal static EntityBindingProvider ResolveProvider(ITypeDescriptorContext context)
        {
            if (context == null) return null;

            // context.Instance có thể là Control đang được chỉnh, hoặc mảng nhiều control.
            // Ta cần tìm EntityBindingProvider trên cùng container/form.
            var host = context.GetService(typeof(System.ComponentModel.Design.IDesignerHost))
                       as System.ComponentModel.Design.IDesignerHost;
            if (host != null && host.Container != null)
            {
                foreach (var comp in host.Container.Components)
                {
                    var p = comp as EntityBindingProvider;
                    if (p != null) return p;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// UITypeEditor kiểu dropdown (giống chọn màu/anchor) cho property BindingMember —
    /// bổ trợ cho TypeConverter, cho trải nghiệm chọn 1 property của entity trực quan.
    /// </summary>
    public class BindingMemberUIEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var edSvc = provider != null
                ? provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService
                : null;
            var bindingProvider = BindingMemberTypeConverter.ResolveProvider(context);
            if (edSvc == null || bindingProvider == null)
                return value;

            var list = new ListBox { BorderStyle = BorderStyle.None, IntegralHeight = true };
            list.Items.Add("(none)");
            foreach (var name in bindingProvider.GetEntityMemberNames().OrderBy(n => n))
                list.Items.Add(name);

            string current = value as string;
            if (!string.IsNullOrEmpty(current))
            {
                int idx = list.Items.IndexOf(current);
                if (idx >= 0) list.SelectedIndex = idx;
            }

            string picked = current;
            list.Click += (s, e) =>
            {
                var sel = list.SelectedItem as string;
                picked = (sel == "(none)" || sel == null) ? string.Empty : sel;
                edSvc.CloseDropDown();
            };

            edSvc.DropDownControl(list);
            return picked ?? string.Empty;
        }
    }

    /// <summary>
    /// TypeConverter cho property EntityType: hỗ trợ nhập/hiển thị Type.
    /// Trong Properties Grid có thể chọn entity từ dropdown nếu entity nằm trong assembly đã load.
    ///
    /// Ví dụ: kéo EntityBindingProvider lên Form, chọn EntityType = Customer,
    /// sau đó chọn BindingMember trên từng TextBox/CheckBox từ dropdown.
    /// </summary>
    public class EntityTypeConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(DesignTimeTypeResolver.GetEntityTypes(context).ToArray());
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value)
        {
            var text = value as string;
            if (text != null)
            {
                if (string.IsNullOrWhiteSpace(text)) return null;
                foreach (Type t in DesignTimeTypeResolver.GetEntityTypes(context))
                    if (string.Equals(t.Name, text, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(t.FullName, text, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(t.AssemblyQualifiedName, text, StringComparison.OrdinalIgnoreCase))
                        return t;
                return DesignTimeTypeResolver.FindType(text);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var t = value as Type;
                return t != null ? t.Name : string.Empty;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    /// <summary>
    /// Converter chuỗi cho EntityTypeName: dropdown trả về FullName để Designer serialize ổn định.
    /// </summary>
    public class EntityTypeNameConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(DesignTimeTypeResolver.GetEntityTypes(context)
                .Select(t => t.FullName)
                .ToArray());
        }
    }

    /// <summary>
    /// UITypeEditor kiểu dropdown cho property EntityType / EntityTypeName — hiện danh sách entity
    /// type (FullName) trực quan trong Properties Grid. Tương tự BindingMemberUIEditor nhưng
    /// cho phép chọn entity type thay vì property.
    /// </summary>
    public class EntityTypeUIEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var edSvc = provider != null
                ? provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService
                : null;
            if (edSvc == null)
                return value;

            var types = DesignTimeTypeResolver.GetEntityTypes(context);
            var list = new ListBox { BorderStyle = BorderStyle.None, IntegralHeight = true };
            list.Items.Add("(none)");

            // Nếu property là Type (EntityType) -> hiển thị Name, giá trị trả Type.
            // Nếu property là string (EntityTypeName) -> hiển thị FullName, giá trị trả FullName.
            bool isTypeProperty = value is Type;
            foreach (var t in types.OrderBy(t => t.FullName))
            {
                if (isTypeProperty)
                    list.Items.Add(t);
                else
                    list.Items.Add(t.FullName);
            }

            if (value != null)
            {
                int idx = list.Items.IndexOf(value);
                if (idx >= 0) list.SelectedIndex = idx;
                else
                {
                    // Fallback: tìm theo Name/FullName
                    var currentName = isTypeProperty ? ((Type)value).FullName : (string)value;
                    for (int i = 0; i < list.Items.Count; i++)
                    {
                        var itemStr = isTypeProperty
                            ? (list.Items[i] is Type ? ((Type)list.Items[i]).FullName : null)
                            : list.Items[i] as string;
                        if (itemStr != null && string.Equals(itemStr, currentName, StringComparison.OrdinalIgnoreCase))
                        { idx = i; break; }
                    }
                    if (idx >= 0) list.SelectedIndex = idx;
                }
            }

            object picked = value;
            list.Click += (s, e) =>
            {
                var sel = list.SelectedItem;
                if (sel == null || (sel is string && (string)sel == "(none)"))
                    picked = isTypeProperty ? (object)null : string.Empty;
                else
                    picked = sel;
                edSvc.CloseDropDown();
            };

            edSvc.DropDownControl(list);
            return picked ?? (isTypeProperty ? (object)null : string.Empty);
        }
    }

    internal static class DesignTimeTypeResolver
    {
        public static IList<Type> GetEntityTypes(ITypeDescriptorContext context)
        {
            var types = new List<Type>();

            var discovery = context != null
                ? context.GetService(typeof(ITypeDiscoveryService)) as ITypeDiscoveryService
                : null;
            if (discovery != null)
            {
                ICollection discovered;
                try { discovered = discovery.GetTypes(typeof(object), false); }
                catch { discovered = null; }
                if (discovered != null)
                    foreach (Type t in discovered)
                        AddEntityType(types, t);
            }

            var host = context != null
                ? context.GetService(typeof(IDesignerHost)) as IDesignerHost
                : null;
            var rootType = host != null && host.RootComponent != null
                ? host.RootComponent.GetType()
                : null;
            if (rootType != null)
                AddTypesFromAssembly(types, rootType.Assembly);

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                AddTypesFromAssembly(types, asm);

            return types
                .Distinct()
                .OrderBy(t => t.FullName)
                .ToList();
        }

        public static Type FindType(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            foreach (var t in GetEntityTypes(null))
                if (string.Equals(t.Name, text, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(t.FullName, text, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(t.AssemblyQualifiedName, text, StringComparison.OrdinalIgnoreCase))
                    return t;
            return Type.GetType(text, false);
        }

        private static void AddTypesFromAssembly(IList<Type> types, System.Reflection.Assembly asm)
        {
            Type[] asmTypes;
            try { asmTypes = asm.GetTypes(); }
            catch { return; }

            foreach (var t in asmTypes)
                AddEntityType(types, t);
        }

        private static void AddEntityType(IList<Type> types, Type t)
        {
            if (t == null || !t.IsClass || t.IsAbstract) return;
            if (!IsEntityType(t)) return;
            if (!types.Contains(t)) types.Add(t);
        }

        private static bool IsEntityType(Type t)
        {
            if (typeof(EntityBase).IsAssignableFrom(t)) return true;

            for (var b = t.BaseType; b != null; b = b.BaseType)
                if (b.FullName == "CrudFramework.Core.Entities.EntityBase")
                    return true;

            try
            {
                if (Attribute.GetCustomAttribute(t, typeof(DbTableAttribute)) != null)
                    return true;
            }
            catch { }

            foreach (var a in t.GetCustomAttributes(false))
                if (a != null && a.GetType().FullName == "CrudFramework.Core.Attributes.DbTableAttribute")
                    return true;

            return false;
        }
    }
}
