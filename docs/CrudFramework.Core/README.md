# CrudFramework.Core

Thư viện **lõi (portable)** của CrudFramework: chứa attribute metadata, entity base, tầng
JSON mapping và tầng truy cập dữ liệu (Function / RawSql / Hybrid). Project này **không phụ
thuộc WinForms hay DevExpress** — có thể tái sử dụng ở tầng service/console.

- **Target:** .NET Framework 4.5, C# 5/6
- **Phụ thuộc:** Newtonsoft.Json, Npgsql 2.2.3

---

## 1. Cấu trúc thư mục

| Thư mục / File | Vai trò |
| --- | --- |
| `Attributes/DbTableAttribute.cs` | Ánh xạ entity → bảng/function; hỗ trợ đa schema; whitelist identifier. |
| `Attributes/DbColumnAttribute.cs` | Ánh xạ property → cột JSON/DB; metadata sinh cột lưới. |
| `Entities/EntityBase.cs` | Base entity, `INotifyPropertyChanged` cho binding 2 chiều. |
| `Entities/FieldError.cs` | Mô tả lỗi theo field trả về từ DB. |
| `Json/` | Ánh xạ entity ↔ JObject (`EntityJsonMapper`). |
| `Data/IDbFunctionClient.cs` | Hợp đồng gọi 4 stored function CRUD. |
| `Data/NpgsqlFunctionClient.cs` | Triển khai `IDbFunctionClient` trên Npgsql 2.2.3. |
| `Data/DbCommandMode.cs` | Enum `Function` / `RawSql` / `Hybrid`. |
| `Data/ISqlCommandClient.cs` | Hợp đồng SQL thô + `RawSqlRequest` + `ISqlOverrideProvider`. |
| `Data/PostgresRawSqlBuilder.cs` | Sinh SQL tham số hóa (SELECT/UPSERT/DELETE) từ metadata. |
| `Data/NpgsqlSqlCommandClient.cs` | Triển khai `ISqlCommandClient` trên Npgsql. |
| `Data/EntityDataClient.cs` | Facade `IEntityDataClient` định tuyến theo `DbCommandMode`. |

---

## 2. Metadata: `[DbTable]` và `[DbColumn]`

```csharp
[DbTable("customers", FunctionPrefix = "fn_")]     // schema mặc định (public)
public class Customer : EntityBase
{
    [DbColumn("id", Caption = "Mã", Width = 60, ReadOnly = true, Order = 1)]
    public int Id { get { return _id; } set { SetField(ref _id, value); } }

    [DbColumn("customer_name", Caption = "Tên khách hàng", Width = 220, Order = 3)]
    public string CustomerName { get { return _customerName; } set { SetField(ref _customerName, value); } }
    // ...
}
```

### Đa schema (mới)

```csharp
[DbTable("orders", Schema = "sales")]
public class Order : EntityBase { ... }
// GetFunctionName("get")  -> "sales.fn_orders_get"
// SQL thô table           -> "sales"."orders"
```

> **An toàn:** `Schema` và mọi identifier (tên bảng/cột) chỉ chấp nhận `[a-z0-9_]`, bắt đầu
> bằng chữ cái hoặc `_`. Giá trị không hợp lệ ném `ArgumentException` — chặn SQL injection
> qua tên định danh.

---

## 3. Ba chế độ truy cập dữ liệu — `DbCommandMode`

| Mode | Mô tả | Cần client |
| --- | --- | --- |
| `Function` (mặc định) | Gọi `fn_<entity>_get/list/upsert/delete`. An toàn nhất, logic ở DB. | `IDbFunctionClient` |
| `RawSql` | Tự sinh SQL tham số hóa từ metadata. Dùng khi DB không có stored function. | `ISqlCommandClient` |
| `Hybrid` | RawSql + override từng câu lệnh qua `ISqlOverrideProvider` (escape hatch). | `ISqlCommandClient` |

Tầng UI chỉ làm việc với facade `IEntityDataClient` — không cần biết chế độ bên dưới.

```csharp
// Function mode (mặc định)
var fnClient = new NpgsqlFunctionClient(connStr);
IEntityDataClient data = new EntityDataClient(typeof(Customer), DbCommandMode.Function, fnClient, null);

// RawSql mode — tự sinh SQL
var sqlClient = new NpgsqlSqlCommandClient(connStr);
IEntityDataClient data = new EntityDataClient(typeof(Customer), DbCommandMode.RawSql, null, sqlClient);

// Hybrid — RawSql + override vài câu lệnh
IEntityDataClient data = new EntityDataClient(
    typeof(Customer), DbCommandMode.Hybrid, null, sqlClient, myOverrides);
```

### Escape hatch — override SQL (Hybrid)

```csharp
public sealed class OrderSqlOverrides : ISqlOverrideProvider
{
    public string GetSql(RawSqlRequest r)    => null; // dùng SQL tự sinh
    public string ListSql(RawSqlRequest r)   => "SELECT * FROM sales.v_orders_active";
    public string UpsertSql(RawSqlRequest r) => null;
    public string DeleteSql(RawSqlRequest r) => null;
}
```

> SQL override **bắt buộc** dùng named parameter (`:id`, `:p_payload`, `:p_filter`) và
> **không** nối chuỗi giá trị.

---

## 4. Nguyên tắc an toàn (bắt buộc)

1. Giá trị người dùng **luôn** truyền qua `NpgsqlParameter` — không nối vào chuỗi SQL.
2. Tên bảng/cột/schema chỉ được ghép sau khi qua whitelist `[a-z0-9_]`.
3. Identifier còn được bọc `"..."` (chuẩn PostgreSQL) để an toàn với keyword.
4. `Function` mode không sinh SQL động — luôn gọi qua `SELECT fn_xxx(...)`.

---

## 5. Xem thêm

- Chi tiết thay đổi: [CHANGELOG.md](./CHANGELOG.md)
- Tầng UI/binding: [../CrudFramework.WinForms/README.md](../CrudFramework.WinForms/README.md)
- Ví dụ chạy được: [../CrudFramework.Sample/README.md](../CrudFramework.Sample/README.md)
