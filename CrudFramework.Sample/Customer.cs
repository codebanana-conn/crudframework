using System;
using CrudFramework.Core.Attributes;
using CrudFramework.Core.Entities;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Entity demo. Có [DbTable("customers")] -> framework tự gọi
    /// fn_customers_get / _list / _upsert / _delete.
    /// Các property có [DbColumn] -> serialize JSON + auto sinh cột grid.
    /// </summary>
    [DbTable("customers", FunctionPrefix = "fn_")]
    public class Customer : EntityBase
    {
        private int _id;
        private string _customerCode;
        private string _customerName;
        private DateTime? _birthDate;
        private decimal _balance;
        private bool _isActive;

        [DbColumn("id", Caption = "Mã", Width = 60, ReadOnly = true, Order = 1)]
        public int Id { get { return _id; } set { SetField(ref _id, value); } }

        [DbColumn("customer_code", Caption = "Mã KH", Width = 100, Order = 2)]
        public string CustomerCode { get { return _customerCode; } set { SetField(ref _customerCode, value); } }

        [DbColumn("customer_name", Caption = "Tên khách hàng", Width = 220, Order = 3)]
        public string CustomerName { get { return _customerName; } set { SetField(ref _customerName, value); } }

        [DbColumn("birth_date", Caption = "Ngày sinh", Width = 110, Format = "dd/MM/yyyy", Order = 4)]
        public DateTime? BirthDate { get { return _birthDate; } set { SetField(ref _birthDate, value); } }

        [DbColumn("balance", Caption = "Số dư", Width = 120, Format = "n0", Order = 5)]
        public decimal Balance { get { return _balance; } set { SetField(ref _balance, value); } }

        [DbColumn("is_active", Caption = "Hoạt động", Width = 90, Order = 6)]
        public bool IsActive { get { return _isActive; } set { SetField(ref _isActive, value); } }
    }
}
