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
- **TODO:** RawSql `ListAsync` chưa dịch `filter` jsonb thành `WHERE` động — dự kiến bổ sung
  bộ dịch filter an toàn (khóa theo cột whitelist) ở bản sau.

## [0.1.0] — 2026-07-25

### Added
- Khởi tạo dự án: `[DbTable]`/`[DbColumn]`, `EntityBase`, `EntityJsonMapper`,
  `IDbFunctionClient` + `NpgsqlFunctionClient`.
