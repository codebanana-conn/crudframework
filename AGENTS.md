# AGENTS.md — Quy tắc làm việc cho AI Agent trên repo CrudFramework

Tài liệu này quy định cách mọi AI Agent (CodeBanana và các agent khác) làm việc trên
repository này. Đọc kỹ trước khi bắt đầu bất kỳ tác vụ nào.

---

## 1. Báo cáo & Kế hoạch theo session

- Mọi phiên làm việc lớn phải có **file kế hoạch** đặt trong thư mục `docs/reports/`.
- Đặt tên file theo quy ước: `plan-<yyyy-MM-dd-HHmm>.md` (giờ theo Asia/Bangkok).
- File kế hoạch **bắt buộc** gồm các mục:
  1. **Prompt / Yêu cầu của user** — chép nguyên văn hoặc tóm tắt đầy đủ.
  2. **Kế hoạch thực hiện** — với mỗi hạng mục/checklist:
     - Vấn đề cần xử lý là gì
     - Cách giải quyết
     - Kết quả
     - Trạng thái (☐ chưa làm / ⏳ đang làm / ✅ xong)
     - Đề xuất cải tiến cho lần sau (tối ưu hệ thống, giúp agent khác làm tốt hơn)
  3. **Nhật ký thực hiện** — cập nhật liên tục theo tiến độ.

## 2. Quy tắc cập nhật trạng thái

- **Xong bất kỳ checklist nào → cập nhật ngay trạng thái** trong file plan tương ứng.
- Không để trạng thái lệch với thực tế code.

## 3. Quy tắc commit (TỰ ĐỘNG — KHÔNG HỎI LẠI)

- Khi hoàn thành một checklist/hạng mục, **tự đánh giá** thay đổi có làm hỏng build không.
- Nếu **an toàn cho build** → **tự commit và push lên remote `origin` ngay**, KHÔNG hỏi lại user.
- Commit message rõ ràng, tiếng Việt, theo mẫu:
  `[Hạng mục N] <tóm tắt thay đổi>` hoặc `[docs] ...`, `[chore] ...`.
- Nếu thay đổi **có nguy cơ hỏng build** hoặc còn dở dang → KHÔNG commit, ghi rõ lý do vào plan.
- Trước khi push, chạy `update_git_token` để tránh token hết hạn.

## 4. Quyền tự chủ thực hiện

- Agent **tự thực hiện toàn bộ kế hoạch**, KHÔNG hỏi lại từng bước.
- Chỉ dừng hỏi khi: gặp quyết định phá vỡ tương thích ngược nghiêm trọng, hoặc thao tác
  không thể khôi phục nằm ngoài phạm vi yêu cầu.

## 5. Quy tắc kỹ thuật của repo (BẮT BUỘC)

- **Target .NET Framework 4.5, C# 5/6** — KHÔNG dùng feature mới hơn C# 6.0.
- Giữ nguyên style code hiện tại: namespace, XML-doc comment tiếng Việt.
- KHÔNG thêm NuGet package mới nếu không thật sự cần.
- **KHÔNG để Form có Designer kế thừa trực tiếp base class generic** (`CrudFormBase<TEntity>`)
  — Designer sẽ lỗi. Nếu cần generic + Designer, tạo lớp trung gian non-generic.
- Data layer: khi `DbCommandMode = Function`, luôn gọi qua `SELECT fn_xxx(...)`, KHÔNG build SQL động.
- Raw SQL (nếu có): luôn tham số hóa bằng `NpgsqlParameter`; tên bảng/cột/schema chỉ được
  ghép sau khi qua whitelist ký tự `[a-z0-9_]`.
- Mọi API public mới phải có XML-doc comment tiếng Việt kèm ví dụ sử dụng.
- Không được để `CrudFramework.Sample` lỗi biên dịch sau mỗi thay đổi.

## 6. Môi trường build

- Repo phát triển trên máy không có .NET Framework 4.5 + DevExpress → khi không build được
  thực tế, phải **rà soát kỹ bằng mắt** để đảm bảo không lỗi biên dịch trước khi commit.
