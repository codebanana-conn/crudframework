# Tài liệu CrudFramework

Solution gồm 3 project (target .NET Framework 4.5, C# 5/6):

| Project | Vai trò | Tài liệu |
| --- | --- | --- |
| **CrudFramework.Core** | Lõi portable: attribute metadata, entity base, JSON mapping, data layer (Function/RawSql/Hybrid). | [README](./CrudFramework.Core/README.md) · [CHANGELOG](./CrudFramework.Core/CHANGELOG.md) |
| **CrudFramework.WinForms** | Tầng UI: form base CRUD, binding Properties Grid, binding độc lập DevExpress. | [README](./CrudFramework.WinForms/README.md) · [CHANGELOG](./CrudFramework.WinForms/CHANGELOG.md) |
| **CrudFramework.Sample** | Ứng dụng demo chạy được. | [README](./CrudFramework.Sample/README.md) · [CHANGELOG](./CrudFramework.Sample/CHANGELOG.md) |

## Kiến trúc tổng quan

```
Entity ([DbTable]/[DbColumn], EntityBase)
        │
        ▼
CrudFramework.Core ── IEntityDataClient (facade)
        │                 ├─ Function  → IDbFunctionClient  (fn_x_get/list/upsert/delete)
        │                 ├─ RawSql    → ISqlCommandClient  (PostgresRawSqlBuilder)
        │                 └─ Hybrid    → RawSql + ISqlOverrideProvider
        ▼
CrudFramework.WinForms ── CrudFormBase / CrudListFormBase
        │                    ├─ EntityBindingProvider (BindingMember, design-time)
        │                    └─ IControlValueAdapter (WinForms chuẩn / DevExpress)
        ▼
CrudFramework.Sample (demo)
```

## Nguyên tắc an toàn (xuyên suốt)

- Giá trị người dùng **luôn** qua `NpgsqlParameter` — không nối chuỗi SQL.
- Tên bảng/cột/schema chỉ ghép sau khi qua whitelist `[a-z0-9_]`, bọc `"..."`.
- Form có Designer **không** kế thừa base generic trực tiếp — dùng lớp trung gian non-generic.

> Quy tắc làm việc cho AI Agent trên repo: xem [`../AGENTS.md`](../AGENTS.md).
> Kế hoạch & nhật ký theo session: [`./reports/`](./reports/).
