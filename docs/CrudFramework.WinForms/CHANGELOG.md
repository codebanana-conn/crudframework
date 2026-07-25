# CHANGELOG — CrudFramework.WinForms

Định dạng theo [Keep a Changelog](https://keepachangelog.com/vi/). Ngày theo Asia/Bangkok.

## [Unreleased]

### Added
- **`CrudFormBase.EntityData`** (`IEntityDataClient`) — Load/Save/Delete ưu tiên dùng
  `EntityData` nếu được gán (hỗ trợ Function/RawSql/Hybrid); giữ `Client` function-only
  để tương thích ngược.
- **`Binding/IControlValueAdapter.cs`** — trừu tượng "property nào của control dùng để bind".
- **`Binding/BuiltInControlValueAdapters.cs`** — `StandardWinFormsControlAdapter`
  (TextBox→Text, CheckBox→Checked, DateTimePicker/NumericUpDown→Value,
  ComboBox→SelectedValue/Text) + `DevExpressEditorAdapter` (duck-typing `EditValue`).
- **`Binding/ControlValueAdapterRegistry.cs`** — chọn adapter theo ưu tiên (DevExpress →
  WinForms chuẩn); `Register` thêm adapter tùy biến; `Default` dùng chung.
- **`EntityBindingProvider.UseAdapters`** (mặc định `true`) + **`AdapterRegistry`** —
  `Bind()` tự phát hiện property bind + update mode theo từng control.

### Changed
- `EntityBindingProvider.Bind()` không còn hard-code 1 `BindProperty` — chuyển sang dùng
  adapter, giúp bind control WinForms thuần mà không đụng DevExpress.

### Docs
- XML-doc tiếng Việt cảnh báo **generic base + Designer**; ví dụ pattern lớp trung gian.
- Tạo `docs/CrudFramework.WinForms/README.md`: checklist tạo Form, binding Properties Grid,
  binding độc lập DevExpress.

### Notes
- Không breaking change: Form dùng DevExpress vẫn bind vào `EditValue` như trước; `Client`
  (function-only) vẫn hoạt động.

## [0.1.0] — 2026-07-25

### Added
- Khởi tạo: `CrudFormBase` / `CrudFormBase<TEntity>`, `CrudListFormBase`,
  `EntityBindingProvider` + `BindingMember` (design-time), error mapping field → control.
