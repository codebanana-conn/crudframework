# CrudFramework.Sample

Ứng dụng WinForms **demo chạy được** minh họa cách dùng CrudFramework: entity với
metadata, list form + grid tự sinh cột, detail/edit form, và pattern generic base + lớp
trung gian cho Designer.

- **Target:** .NET Framework 4.5, C# 5/6
- **Phụ thuộc:** `CrudFramework.Core`, `CrudFramework.WinForms`, DevExpress v17.1, Npgsql 2.2.3

---

## 1. Thành phần chính

| File | Vai trò |
| --- | --- |
| `Program.cs` | Entry point: tạo `NpgsqlFunctionClient`, chạy `CustomerCombinedForm`. |
| `Customer.cs` | Entity demo `[DbTable("customers")]` + các `[DbColumn]`. |
| `CustomerCombinedForm.*` | Form gộp grid + detail trên 1 màn hình (form mặc định). |
| `CustomerListForm.*` | List form riêng: grid tự sinh cột, filter keyword, thêm/sửa/xóa. |
| `CustomerDetailForm.*` | Detail form (non-generic base `CrudFormBase`). |
| `CustomerEditForm.*` | **Demo pattern generic base + lớp trung gian** (`CustomerFormBase`). |
| `CustomerPlainWinFormsForm.*` | Demo binding bằng control WinForms chuẩn (`TextBox`, `NumericUpDown`, `CheckBox`). |
| `KskPhieu.cs` | Entity demo phiếu KSK theo TT 25/2026/TT-BYT (4 mẫu, ~130 cột). |
| `KskPhieuForm.*` | Demo form KSK: `XtraTabControl` (tab Tổng hợp + 4 tab con theo loại mẫu), show/hide tab theo `LoaiMauKsk`, `DropDownButton` "In phiếu kết quả" 4 mẫu (placeholder). |
| `Form1.*` | Form khởi tạo mẫu. |

---

## 2. Chạy demo

1. Mở `CrudFramework.sln` bằng Visual Studio (có DevExpress v17.1 cài đặt).
2. Sửa connection string trong `Program.cs` cho đúng PostgreSQL của bạn:
   ```csharp
   var connectionString =
       "Host=localhost;Port=5432;Database=...;Username=postgres;Password=...;";
   ```
3. Đảm bảo DB đã có các function `fn_customers_get/list/upsert/delete` (xem thư mục `sql/`).
4. Chạy project `CrudFramework.Sample`.

> Muốn tách list/detail riêng: đổi `Application.Run(new CustomerCombinedForm(client))`
> thành `Application.Run(new CustomerListForm(client))`.

---

## 3. Pattern quan trọng minh họa

### 3.1 Generic base + lớp trung gian (Designer-safe)

`CustomerEditForm.cs` minh họa cách để Designer mở được form kế thừa base generic:

```csharp
// Lớp trung gian non-generic — Designer load OK, KHÔNG có Designer riêng.
public abstract class CustomerFormBase : CrudFormBase<Customer> { }

// Form thực tế kế thừa lớp trung gian; vẫn hưởng typed Current kiểu Customer.
public partial class CustomerEditForm : CustomerFormBase
{
    public CustomerEditForm(IDbFunctionClient client, int? id)
    {
        InitializeComponent();
        Client = client;                        // EntityType đã tự set = typeof(Customer)
        BindingProvider = entityBindingProvider1;
        ErrorProvider   = dxErrorProvider1;
        // ...
    }
}
```

> ⚠️ **KHÔNG** để form có Designer kế thừa `CrudFormBase<Customer>` trực tiếp — Designer sẽ lỗi.

### 3.2 List form + grid tự sinh cột

`CustomerListForm` gọi `InitializeGrid()` để sinh cột tự động từ `[DbColumn]`
(Caption, Width, Format, Order), có filter keyword gửi vào `fn_customers_list` qua `p_filter`.

### 3.3 Entity + binding 2 chiều

`Customer : EntityBase` dùng `SetField(...)` trong setter → `INotifyPropertyChanged` → binding
2 chiều real-time với control.

### 3.4 Binding bằng control WinForms chuẩn

`CustomerPlainWinFormsForm` dùng `TextBox`, `NumericUpDown`, `CheckBox` thay cho editor DevExpress.
`EntityBindingProvider.UseAdapters = true` tự chọn property bind theo control:

```csharp
entityBindingProvider1.SetBindingMember(txtCode, "CustomerCode"); // TextBox -> Text
entityBindingProvider1.SetBindingMember(numBalance, "Balance");   // NumericUpDown -> Value
entityBindingProvider1.SetBindingMember(chkActive, "IsActive");   // CheckBox -> Checked
```

---

## 4. Xem thêm

- Chi tiết thay đổi: [CHANGELOG.md](./CHANGELOG.md)
- Tầng lõi: [../CrudFramework.Core/README.md](../CrudFramework.Core/README.md)
- Tầng UI/binding: [../CrudFramework.WinForms/README.md](../CrudFramework.WinForms/README.md)
