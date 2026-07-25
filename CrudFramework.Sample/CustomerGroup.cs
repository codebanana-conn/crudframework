using System.Collections.Generic;

namespace CrudFramework.Sample
{
    /// <summary>
    /// DTO nhóm khách hàng dùng cho demo ComboBox (SelectedValue binding).
    /// KHÔNG phải entity (không có [DbTable]/[DbColumn]) — chỉ là lookup list cục bộ.
    /// </summary>
    public class CustomerGroup
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public CustomerGroup() { }
        public CustomerGroup(string code, string name) { Code = code; Name = name; }

        public static List<CustomerGroup> DefaultGroups()
        {
            return new List<CustomerGroup>
            {
                new CustomerGroup("VIP", "Khách VIP"),
                new CustomerGroup("REG", "Khách thường"),
                new CustomerGroup("NEW", "Khách mới")
            };
        }
    }
}
