# CHANGELOG — CrudFramework.Core

Định dạng theo [Keep a Changelog](https://keepachangelog.com/vi/). Ngày theo Asia/Bangkok.

## [Unreleased]

### Added
- **`DbTableAttribute.Schema`** — hỗ trợ đa schema PostgreSQL. `GetFunctionName(action)`
  trả về tên schema-qualified khi `Schema` được set (VD `sales.fn_orders_get`).
- **`DbTableAttribute.ValidateIdentifierOrNull`** — whitelist identifier dùng chung
  (`[a-z0-9_]`, bắt đầu bằng chữ/`_`); ném `ArgumentException` với giá trị không hợp lệ.
- **`Data/DbCommandMode.cs`** — enum `Function` / `RawSql` / `Hybrid`.
- **`Data/ISqlCommandClient.cs`** — hợp đồng SQL thô, kèm `RawSqlRequest` và
  `ISqlOverrideProvider` (escape hatch cho Hybrid).
- **`Data/PostgresRawSqlBuilder.cs`** — sinh SQL tham số hóa: SELECT / UPSERT
  (`ON CONFLICT` + `jsonb_populate_record`) / DELETE; identifier qua whitelist + bọc `"..."`.
- **`Data/NpgsqlSqlCommandClient.cs`** — triển khai `ISqlCommandClient` trên Npgsql 2.2.3.
- **`Data/EntityDataClient.cs`** (`IEntityDataClient`) — facade định tuyến CRUD theo
  `DbCommandMode`, ẩn function/SQL khỏi tầng UI.

### Docs
- Thêm XML-doc tiếng Việt + ví dụ cho các API public lõi.
- Tạo `docs/CrudFramework.Core/README.md` mô tả metadata, 3 chế độ dữ liệu, nguyên tắc an toàn.

### Notes
- Không breaking change: `IDbFunctionClient` + Function mode giữ nguyên hành vi cũ.

### Improvements (2026-07-25, session cải tiến)
- **`DbTableAttribute.KeyColumn`** — khóa chính cấu hình được (mặc định `"id"`, qua whitelist
  `[a-z0-9_]`); lan tỏa vào `RawSqlRequest.KeyColumn` cho SELECT/UPSERT/DELETE.
- **RawSql `ListAsync` — WHERE động an toàn:** thêm overload
  `PostgresRawSqlBuilder.BuildListSql(req, filter, out IList<FilterParam>)` dựng `WHERE` chỉ từ
  key trùng cột đã whitelist; string→`ILIKE`, số/bool→`=`, null→`IS NULL`. Mọi giá trị tham số
  hóa (`:f0`, `:f1`, …). `NpgsqlSqlCommandClient.ListAsync` bind các tham số này.
- Thêm `FilterParam` + `FilterParamKind` trong tầng Data.

## [0.1.0] — 2026-07-25

### Added
- Khởi tạo dự án: `[DbTable]`/`[DbColumn]`, `EntityBase`, `EntityJsonMapper`,
  `IDbFunctionClient` + `NpgsqlFunctionClient`.
