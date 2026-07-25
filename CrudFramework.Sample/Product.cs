using System;
using CrudFramework.Core.Attributes;
using CrudFramework.Core.Entities;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Entity demo phủ đủ kiểu dữ liệu SQL/DbColumn: int, string, decimal, bool, DateTime?,
    /// cột ReadOnly, HiddenInGrid, Ignore, Format.
    /// </summary>
    [DbTable("products", FunctionPrefix = "fn_")]
    public class Product : EntityBase
    {
        private int _id;
        private string _productCode;
        private string _productName;
        private decimal _price;
        private int _stockQuantity;
        private bool _isAvailable;
        private DateTime? _manufacturedDate;
        private string _description;
        private int _categoryId;
        private string _internalNote;
        private DateTime _createdAt;

        [DbColumn("id", Caption = "Mã SP", Width = 60, ReadOnly = true, Order = 1)]
        public int Id { get { return _id; } set { SetField(ref _id, value); } }

        [DbColumn("product_code", Caption = "Mã sản phẩm", Width = 100, Order = 2)]
        public string ProductCode { get { return _productCode; } set { SetField(ref _productCode, value); } }

        [DbColumn("product_name", Caption = "Tên sản phẩm", Width = 220, Order = 3)]
        public string ProductName { get { return _productName; } set { SetField(ref _productName, value); } }

        [DbColumn("price", Caption = "Giá", Width = 120, Format = "#,##0.00", Order = 4)]
        public decimal Price { get { return _price; } set { SetField(ref _price, value); } }

        [DbColumn("stock_quantity", Caption = "Số lượng", Width = 90, Format = "n0", Order = 5)]
        public int StockQuantity { get { return _stockQuantity; } set { SetField(ref _stockQuantity, value); } }

        [DbColumn("is_available", Caption = "Còn hàng", Width = 80, Order = 6)]
        public bool IsAvailable { get { return _isAvailable; } set { SetField(ref _isAvailable, value); } }

        [DbColumn("manufactured_date", Caption = "Ngày SX", Width = 110, Format = "dd/MM/yyyy", Order = 7)]
        public DateTime? ManufacturedDate { get { return _manufacturedDate; } set { SetField(ref _manufacturedDate, value); } }

        [DbColumn("description", Caption = "Mô tả", Width = 200, Order = 8)]
        public string Description { get { return _description; } set { SetField(ref _description, value); } }

        [DbColumn("category_id", Caption = "Loại", Width = 80, HiddenInGrid = true, Order = 9)]
        public int CategoryId { get { return _categoryId; } set { SetField(ref _categoryId, value); } }

        // Ignore: không serialize, không hiện grid, chỉ dùng nội bộ
        [DbColumn("internal_note", Ignore = true)]
        public string InternalNote { get { return _internalNote; } set { SetField(ref _internalNote, value); } }

        [DbColumn("created_at", Caption = "Tạo lúc", Width = 110, ReadOnly = true, Format = "dd/MM/yyyy HH:mm", Order = 10)]
        public DateTime CreatedAt { get { return _createdAt; } set { SetField(ref _createdAt, value); } }
    }

    /// <summary>
    /// Entity lookup cho danh sách Category (dùng trong LookUpEdit / EntityBindingSource).
    /// </summary>
    [DbTable("categories", FunctionPrefix = "fn_")]
    public class Category : EntityBase
    {
        private int _id;
        private string _categoryName;

        [DbColumn("id", Caption = "Mã loại", Width = 60, ReadOnly = true, Order = 1)]
        public int Id { get { return _id; } set { SetField(ref _id, value); } }

        [DbColumn("category_name", Caption = "Tên loại", Width = 200, Order = 2)]
        public string CategoryName { get { return _categoryName; } set { SetField(ref _categoryName, value); } }
    }
}
