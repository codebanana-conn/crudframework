# CHANGELOG — CrudFramework.Sample

Định dạng theo [Keep a Changelog](https://keepachangelog.com/vi/). Ngày theo Asia/Bangkok.

### Added
- **`KskPhieu.cs` + `KskPhieuForm`** — demo phiếu Khám sức khỏe (KSK) theo Thông tư
  25/2026/TT-BYT. Entity ~130 cột chia 4 nhóm mẫu (trẻ em <6t, 6-18t, ≥18t, tâm thần)
  dùng chung bảng `ksk_phieu`. Form `KskPhieuForm` kế thừa `CrudFormBase` (non-generic),
  `XtraTabControl` 5 tab (Tổng hợp + 4 tab con theo loại mẫu), show/hide tab con theo
  `LoaiMauKsk`, `CheckEdit` cho cột numeric(1,0), `ComboBoxEdit` cho cột có bảng mã,
  `DropDownButton` "In phiếu kết quả" với menu 4 mẫu in (placeholder MessageBox).
- SQL `sql/03_kham_suc_khoe.sql`: bảng `ksk_benhnhan` + `ksk_phieu`, ~130 cột
  `ALTER TABLE ... ADD COLUMN IF NOT EXISTS` từng câu riêng + `COMMENT ON COLUMN`,
  4 function `fn_ksk_phieu_get/list/upsert/delete`, dữ liệu mẫu.
- Đăng ký `KskPhieu.cs` + `KskPhieuForm.cs/.Designer.cs` vào `.csproj`
  (`<SubType>Form</SubType>` + `<DependentUpon>`).
- Thêm nút "10. KSK" vào `DemoLauncherForm`.

### Docs
- Cập nhật `docs/developments.md` mục 8 (bảng danh mục demo forms).
- Cập nhật `docs/CrudFramework.Sample/README.md` + `CHANGELOG.md`.

### Notes
- Không breaking change: các form demo hiện có vẫn hoạt động.
- `check-all.sh` PASS (Core biên dịch thật + WinForms/Sample syntax-check 41 file, 0 lỗi).

## [Unreleased]

### Added
- **`CustomerPlainWinFormsForm`** — demo binding bằng control WinForms chuẩn (`TextBox`,
  `NumericUpDown`, `CheckBox`) qua `EntityBindingProvider.UseAdapters = true`.
- **`CustomerFormBase` + `CustomerEditForm`** — demo pattern "generic base + lớp trung gian
  non-generic" để Windows Forms Designer load được form kế thừa `CrudFormBase<Customer>`.
- Đăng ký `CustomerEditForm.cs` + `.Designer.cs` vào `CrudFramework.Sample.csproj`
  và `CustomerPlainWinFormsForm.cs` + `.Designer.cs` vào `CrudFramework.Sample.csproj`
  (`<SubType>Form</SubType>` + `<DependentUpon>`).

### Docs
- Tạo `docs/CrudFramework.Sample/README.md`: hướng dẫn chạy demo, các pattern minh họa.

### Notes
- Không breaking change: `Form1` / `CustomerDetailForm` (non-generic) vẫn hoạt động như trước.

## [0.1.0] — 2026-07-25

### Added
- Khởi tạo demo: `Customer` entity, `CustomerCombinedForm`, `CustomerListForm`,
  `CustomerDetailForm`, `Program.cs` với `NpgsqlFunctionClient`.
