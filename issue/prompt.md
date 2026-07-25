# Prompt: Bổ sung Form "Khám sức khỏe theo Thông tư 25/2026/TT-BYT" vào CrudFramework.Sample

## Bối cảnh

Repo CrudFramework (CrudFramework.Core / CrudFramework.WinForms / CrudFramework.Sample,
.NET Framework 4.5, C# 5/6, Newtonsoft.Json, Npgsql 2.2.3, DevExpress v17.1) đã có sẵn hạ tầng
CRUD dùng PostgreSQL function (`fn_<entity>_get/list/upsert/delete`), entity POCO gắn
`[DbTable]`/`[DbColumn]`, `CrudFormBase`, `EntityBindingProvider`. Đọc kỹ `AGENTS.md` và
`docs/developments.md` trước khi bắt đầu — làm đúng pattern đã có, không bịa API mới.

Tài liệu nghiệp vụ đính kèm: `Thong-mo-ta-bo-sung-chuc-nang-mau-giay-ksk-tt25-2026.md`
(mô tả bổ sung mẫu Khám sức khỏe (KSK) theo TT 25/2026/TT-BYT, gồm 4 loại mẫu: trẻ em <6 tuổi,
6–<18 tuổi, ≥18 tuổi, khám tâm thần — dùng chung 1 bảng `pskhamsuckhoe`).

## Việc cần làm (thực hiện tuần tự, tự chủ theo AGENTS.md, có plan + commit từng hạng mục)

### 1. SQL — bổ sung bảng & cột (idempotent)

- Vì đây là bảng nghiệp vụ của hệ thống gốc (`current.pskhamsuckhoe`, `dmbenhnhan`, `psdangky`,
  `khambenh`...) không tồn tại trong sample repo này, hãy tạo file SQL demo mới
  `sql/03_kham_suc_khoe.sql` theo đúng convention của `sql/01_customers.sql`/`sql/02_products.sql`
  (schema `public`, không dùng schema `current` như tài liệu gốc, để chạy được độc lập trong demo).
- Tạo bảng tối giản đủ dùng cho demo: `ksk_benhnhan` (rút gọn từ `dmbenhnhan`), và bảng chính
  `ksk_phieu` (rút gọn/gộp `pskhamsuckhoe` + phần liên quan `psdangky`/`khambenh` cần cho form)
  — CHỈ lấy các cột thật sự cần cho form demo, không cần bê nguyên 200+ cột gốc.
- Với các cột liệt kê trong mục "II. CÁC GIÁ TRỊ CHƯA CÓ TRƯỜNG LƯU TRỮ" của tài liệu (4 mẫu:
  trẻ em <6 tuổi, 6–<18 tuổi, ≥18 tuổi, tâm thần): thêm **từng cột một câu ALTER TABLE riêng**,
  dùng `ADD COLUMN IF NOT EXISTS` để chạy lại an toàn — nếu cột đã tồn tại thì bỏ qua, không lỗi,
  các cột khác vẫn được thêm bình thường. Giữ tên cột + kiểu dữ liệu + comment (COMMENT ON COLUMN)
  đúng như tài liệu mô tả (numeric(1,0), numeric(10,2), varchar(255|500)...).
- Viết 4 stored function theo đúng contract đã dùng trong `sql/01_customers.sql`:
  - `fn_ksk_phieu_get(p_id int) RETURNS jsonb`
  - `fn_ksk_phieu_list(p_filter jsonb) RETURNS jsonb`
  - `fn_ksk_phieu_upsert(p_payload jsonb) RETURNS jsonb` — validate tối thiểu (bắt buộc chọn
    loại mẫu KSK, họ tên, ngày sinh)
  - `fn_ksk_phieu_delete(p_id int) RETURNS jsonb`
- Thêm dữ liệu mẫu tối thiểu (1–2 dòng) để test form.

### 2. Entity C#

- Tạo `CrudFramework.Sample/KskPhieu.cs`: entity `KskPhieu : EntityBase`, gắn
  `[DbTable("ksk_phieu", FunctionPrefix = "fn_")]`, mỗi property gắn `[DbColumn]` đúng convention
  (Caption tiếng Việt, Order, Format nếu cần, ReadOnly cho `id`/`created_at`).
- Nhóm property theo 4 nhóm mẫu KSK (dùng vùng comment `// ---- Trẻ em <6 tuổi ----`,
  `// ---- 6-18 tuổi ----`, `// ---- ≥18 tuổi ----`, `// ---- Tâm thần ----`) để dễ map sang tab.
- Property "loại mẫu KSK đang áp dụng" (VD `LoaiMauKsk`: 1=trẻ em, 2=6-18, 3=≥18, 4=tâm thần)
  dùng để form show/hide đúng tab.

### 3. Form UI — dựa theo ảnh mẫu

- **Bắt buộc**: trước khi thiết kế, `web_fetch` ảnh `https://i.vgy.me/F39mfC.png` (link trong
  mục cuối tài liệu, đoạn "Trên nút `In phiếu kết quả`, bổ sung thêm các tùy chọn in..."). Nếu
  ảnh đã hết hạn (vgy.me tự xóa sau 6 tháng không xem) hoặc không tải được, dừng lại hỏi người
  dùng gửi trực tiếp ảnh vào conversation rồi mới thiết kế tiếp — KHÔNG đoán bừa layout.
- Sau khi có ảnh, phân tích bố cục thật (vị trí tab, nhóm field, nút bấm) rồi thiết kế
  `KskPhieuForm` bám sát ảnh, dùng đúng theo yêu cầu nghiệp vụ:
  - Kế thừa `CrudFormBase` (non-generic) — theo checklist Designer trong
    `docs/CrudFramework.WinForms/README.md` mục 2.
  - Dùng `DevExpress.XtraTab.XtraTabControl`: 1 tab "Tổng hợp" (toàn bộ field) + các tab con
    theo từng loại khám ("Trẻ em <6 tuổi", "6–18 tuổi", "Người lớn ≥18 tuổi", "Tâm thần") —
    đúng yêu cầu "Chia nội dung ra thành các tab riêng biệt... tab tổng hợp... và các tab con".
  - Binding qua `EntityBindingProvider` (`SetBindingMember`) như các form demo khác
    (`ProductEditForm`, `CustomerDetailForm`).
  - Các trường `numeric(1,0)` (0/1) → `CheckEdit`; các trường liệt kê nhiều lựa chọn có
    comment dạng "0:...;1:...;2:..." → `ComboBoxEdit`/`LookUpEdit` với danh sách value hiển thị
    đúng nghĩa (không hiện số thô 0/1/2 cho người dùng).
  - Nút **"In phiếu kết quả"** dùng `DevExpress.XtraBars` `BarButtonItem` hoặc
    `DropDownButton`/`ButtonEdit` kiểu split-button với menu con liệt kê từng mẫu in
    (VD "In mẫu KSK trẻ em <6 tuổi", "In mẫu 6-18 tuổi", "In mẫu người lớn", "In mẫu tâm thần")
    — placeholder xử lý (`MessageBox` báo "chưa cài đặt in mẫu X") vì in ấn nằm ngoài phạm vi
    CRUD framework, không cần triển khai thật.
  - Đăng ký `.cs` + `.Designer.cs` vào `CrudFramework.Sample.csproj`, thêm nút mở form vào
    `DemoLauncherForm`.

### 4. Data layer

- Dùng `DbCommandMode.Function` (không cần RawSql/Hybrid cho form này) — gán `Client`
  (`IDbFunctionClient`) như các form Function mode khác.

### 5. Cập nhật docs (bắt buộc theo AGENTS.md mục 7)

- Thêm dòng vào bảng danh mục demo forms (`docs/developments.md` mục 8).
- Cập nhật `docs/CrudFramework.Sample/README.md` + `CHANGELOG.md`.
- Tạo `docs/reports/plan-<yyyy-MM-dd-HHmm>.md` theo mẫu AGENTS.md mục 1, cập nhật trạng thái
  từng hạng mục khi hoàn thành, tự commit theo AGENTS.md mục 3 (không hỏi lại).

### 6. Kiểm tra trước khi commit

- Chạy `bash tools/check-all.sh` (build thật Core + syntax-check WinForms/Sample), sửa hết lỗi
  trước khi commit, theo đúng AGENTS.md mục 6.

## Ràng buộc bắt buộc

- Không build SQL động cho phần Function mode — chỉ `SELECT fn_xxx(...)`.
- Không phá vỡ demo hiện có (`CustomerCombinedForm`, `ProductEditForm`...).
- C# 5/6 only, XML-doc tiếng Việt cho mọi class/property public mới.
- Nếu không tải được ảnh mẫu, DỪNG và hỏi lại — không tự suy đoán bố cục UI.
