# tools/ — Bộ công cụ kiểm tra biên dịch cross-platform

Thư mục này chứa lưới an toàn (safety net) bắt lỗi biên dịch **trước khi commit**, dùng được
trên máy Linux/CI **không có** Visual Studio, .NET Framework 4.5 và DevExpress v17.1.

> **Quan trọng — trung thực về giới hạn:** Bộ công cụ này **KHÔNG thay thế** build thật trên
> Windows + DevExpress v17.1. Nó chỉ bổ sung một lớp kiểm tra sớm. Xem bảng dưới.

---

## Phạm vi kiểm tra thực tế

| Project | Kiểm tra được trên Linux/CI | Lý do |
|---|---|---|
| **CrudFramework.Core** | ✅ **Biên dịch NGỮ NGHĨA thật** (Roslyn `csc` + `Libraries/*.dll`) | Chỉ phụ thuộc BCL + Npgsql/Newtonsoft/Mono.Security (có DLL thật trong `Libraries/`) |
| **CrudFramework.WinForms** | ⚠️ **Chỉ RÀ SOÁT CÚ PHÁP** (Roslyn parse, C#6) | Thiếu DevExpress v17.1 + Windows Desktop reference pack → không resolve được kiểu |
| **CrudFramework.Sample** | ⚠️ **Chỉ RÀ SOÁT CÚ PHÁP** (Roslyn parse, C#6) | Như trên |

- **Biên dịch ngữ nghĩa** = bắt lỗi kiểu, thiếu `using`, sai chữ ký hàm, dùng feature > C#6...
- **Rà soát cú pháp** = bắt lỗi thiếu `;`, ngoặc lệch, khai báo sai cú pháp, feature > C#6...
  KHÔNG bắt được lỗi kiểu/thiếu tham chiếu (vì không có DevExpress để so kiểu).

---

## Yêu cầu môi trường

- **Linux/macOS/CI:** `bash` + `curl` (hoặc `wget`). Script tự cài **.NET SDK 8** vào
  `~/.dotnet` (**không cần root**) nếu chưa có. Lần chạy đầu cần mạng để tải SDK + NuGet
  package `Microsoft.CodeAnalysis.CSharp` (đã cache cho lần sau).
- **Windows dev:** dùng `build-core.ps1` với MSBuild (build target net45 thật; có DevExpress
  thì build được cả solution — xem `-All`).

---

## Cách dùng

```bash
# Chạy toàn bộ (Core build thật + WinForms/Sample syntax-check) — chạy TRƯỚC mỗi commit:
bash tools/check-all.sh

# Chỉ biên dịch Core:
bash tools/build-core.sh

# Chỉ rà soát cú pháp (mặc định WinForms + Sample; hoặc truyền path/file cụ thể):
bash tools/syntax-check.sh
bash tools/syntax-check.sh CrudFramework.Sample/ProductEditForm.cs
```

Trên Windows (PowerShell), build thật bằng MSBuild:

```powershell
pwsh tools/build-core.ps1        # build CrudFramework.Core (net45)
pwsh tools/build-core.ps1 -All   # build cả solution (cần DevExpress v17.1)
```

Mã thoát: `0` = tất cả pass; khác `0` = có lỗi (chi tiết in ra stderr theo dạng
`file(dòng,cột): error CSxxxx: mô tả`).

---

## Danh sách file

| File | Vai trò |
|---|---|
| `dotnet-env.sh` | Phát hiện / tự cài .NET SDK user-space; resolve `dotnet`, `csc.dll`, ref net8.0. Các script khác `source` file này. |
| `build-core.sh` | Biên dịch **thật** `CrudFramework.Core` bằng Roslyn `csc` (langversion 6) + `Libraries/*.dll`. |
| `build-core.ps1` | Build Windows bằng MSBuild (net45 thật; `-All` build cả solution). |
| `syntax-check.sh` | Wrapper gọi tool Roslyn parse WinForms/Sample. |
| `SyntaxCheck/` | Tool C# (net8.0) dùng `Microsoft.CodeAnalysis.CSharp` parse cú pháp C#6. |
| `check-all.sh` | Chạy tuần tự build-core + syntax-check; in tổng kết. **Lệnh chuẩn trước commit.** |

Kết quả build tạm ghi vào `tmp/out/` (đã bị `.gitignore` bỏ qua).
