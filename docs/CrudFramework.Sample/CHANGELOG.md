# CHANGELOG — CrudFramework.Sample

Định dạng theo [Keep a Changelog](https://keepachangelog.com/vi/). Ngày theo Asia/Bangkok.

## [Unreleased]

### Added
- **`CustomerFormBase` + `CustomerEditForm`** — demo pattern "generic base + lớp trung gian
  non-generic" để Windows Forms Designer load được form kế thừa `CrudFormBase<Customer>`.
- Đăng ký `CustomerEditForm.cs` + `.Designer.cs` vào `CrudFramework.Sample.csproj`
  (`<SubType>Form</SubType>` + `<DependentUpon>`).

### Docs
- Tạo `docs/CrudFramework.Sample/README.md`: hướng dẫn chạy demo, các pattern minh họa.

### Notes
- Không breaking change: `Form1` / `CustomerDetailForm` (non-generic) vẫn hoạt động như trước.

## [0.1.0] — 2026-07-25

### Added
- Khởi tạo demo: `Customer` entity, `CustomerCombinedForm`, `CustomerListForm`,
  `CustomerDetailForm`, `Program.cs` với `NpgsqlFunctionClient`.
