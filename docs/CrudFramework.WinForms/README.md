# CrudFramework.WinForms

Tầng **UI WinForms** của CrudFramework: form base CRUD, binding qua Properties Grid
(design-time), binding độc lập DevExpress qua adapter, và ánh xạ lỗi field → control.

- **Target:** .NET Framework 4.5, C# 5/6
- **Phụ thuộc:** `CrudFramework.Core`, WinForms, DevExpress v17.1 (tùy chọn ở tầng binding)

---

## 1. Cấu trúc thư mục

| File | Vai trò |
| --- | --- |
| `CrudFormBase.cs` | Base form edit CRUD (generic `CrudFormBase<TEntity>` + non-generic `CrudFormBase`). |
| `CrudListFormBase.cs` | Base form danh sách + grid tự sinh cột từ `[DbColumn]`. |
| `EntityBindingProvider.cs` | `IExtenderProvider` thêm property `BindingMember` cho mọi control. |
| `EntityBindingSource.cs` | Nguồn dữ liệu binding cho 1 entity. |
| `DesignTimeSupport.cs` | `BindingMemberTypeConverter` + `BindingMemberUIEditor` (dropdown design-time). |
| `Binding/IControlValueAdapter.cs` | Trừu tượng "property nào của control dùng để bind". |
| `Binding/BuiltInControlValueAdapters.cs` | Adapter WinForms chuẩn + DevExpress (duck-typing). |
| `Binding/ControlValueAdapterRegistry.cs` | Chọn adapter theo thứ tự ưu tiên. |
| `ErrorMapping/` | Map `FieldError` từ DB → control tương ứng. |

---

## 2. ⚠️ Designer + base class generic (QUAN TRỌNG)

Windows Forms Designer **KHÔNG** load được Form kế thừa **trực tiếp** một base generic
(`CrudFormBase<TEntity>`) — báo lỗi *"base class ... could not be loaded"*.

**Giải pháp chuẩn:** chèn 1 lớp trung gian non-generic:

```
CrudFormBase (non-generic)
   └─ CrudFormBase<Customer> (generic — KHÔNG cho Form Designer kế thừa trực tiếp)
         └─ CustomerFormBase (non-generic — Designer load OK)   ← lớp trung gian
               └─ CustomerEditForm (partial, có Designer)        ← form thực tế
```

```csharp
// Lớp trung gian: non-generic, KHÔNG có Designer riêng.
public abstract class CustomerFormBase : CrudFormBase<Customer> { }

// Form thực tế: kế thừa lớp trung gian -> Designer mở bình thường.
public partial class CustomerEditForm : CustomerFormBase { ... }
```

### Checklist "Tạo Form mới trong CrudFramework"

1. **Base class:** Form CÓ Designer phải kế thừa `CrudFormBase` (non-generic) HOẶC lớp
   trung gian non-generic (`XxxFormBase : CrudFormBase<TEntity> {}`). **KHÔNG** kế thừa
   `CrudFormBase<TEntity>` trực tiếp.
2. **Constructor:** base class cần constructor không tham số (Designer yêu cầu).
3. **DevExpress version** phải khớp (v17.1) giữa các project, nếu không Designer lỗi load assembly.
4. Set `EntityType`, `Client`/`EntityData`, `BindingProvider`, `ErrorProvider` trong
   constructor (sau `InitializeComponent()`).
5. Bind control qua Properties Grid (`BindingMember`) hoặc `SetBindingMember(...)`.
6. Đăng ký `.cs` + `.Designer.cs` vào `.csproj` với `<SubType>Form</SubType>` + `<DependentUpon>`.

---

## 3. Binding qua Properties Grid — `EntityBindingProvider`

Kéo `EntityBindingProvider` vào Form (1 lần), set `EntityType = typeof(TEntity)`. Component
"mở rộng" thêm property **`BindingMember`** cho MỌI control (giống `ErrorProvider` gắn `Error`).
Trong Properties Grid, `BindingMember` hiện **dropdown** danh sách property của entity.

- Design-time: chọn property từ dropdown, lưu ra `.Designer.cs`.
- Runtime: `Bind()` tạo `Control.DataBindings.Add(...)` **2 chiều thật** cho mọi control có `BindingMember`.

---

## 4. Binding độc lập DevExpress — Control Value Adapter

`Bind()` không còn hard-code 1 property duy nhất. `EntityBindingProvider.UseAdapters`
(mặc định `true`) dùng `ControlValueAdapterRegistry` để phát hiện property bind theo từng control:

| Adapter | Control | Property bind |
| --- | --- | --- |
| `DevExpressEditorAdapter` (ưu tiên) | Editor DevExpress (có `EditValue`, nhận qua reflection) | `EditValue` |
| `StandardWinFormsControlAdapter` | CheckBox/RadioButton | `Checked` |
| | DateTimePicker / NumericUpDown | `Value` |
| | ComboBox (có DataSource) | `SelectedValue` (else `Text`) |
| | TextBox & mặc định | `Text` |

Nhờ duck-typing (`EditValue` qua reflection), tầng binding **không cần** tham chiếu
compile-time tới DevExpress — Form dùng control WinForms thuần vẫn bind được.

### Đăng ký adapter tùy biến

```csharp
ControlValueAdapterRegistry.Default.Register(new MyColorPickerAdapter()); // ưu tiên cao nhất
// hoặc gán registry riêng cho 1 provider:
entityBindingProvider1.AdapterRegistry = myRegistry;
```

---

## 5. Xem thêm

- Chi tiết thay đổi: [CHANGELOG.md](./CHANGELOG.md)
- Tầng lõi: [../CrudFramework.Core/README.md](../CrudFramework.Core/README.md)
- Ví dụ chạy được: [../CrudFramework.Sample/README.md](../CrudFramework.Sample/README.md)
