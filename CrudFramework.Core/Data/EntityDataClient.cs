using System;
using System.Threading;
using System.Threading.Tasks;
using CrudFramework.Core.Attributes;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Core.Data
{
    /// <summary>
    /// Facade CRUD hợp nhất cho một entity type, ẩn đi việc dữ liệu đến từ stored function
    /// (<see cref="DbCommandMode.Function"/>) hay SQL thô (<see cref="DbCommandMode.RawSql"/> /
    /// <see cref="DbCommandMode.Hybrid"/>). Tầng UI (CrudFormBase) chỉ cần làm việc với interface
    /// <see cref="IEntityDataClient"/> này, không quan tâm chế độ bên dưới.
    ///
    /// Bề mặt method giống <see cref="IDbFunctionClient"/> nhưng KHÔNG cần truyền tên function/bảng —
    /// vì đã gắn với 1 entity type khi khởi tạo.
    /// </summary>
    public interface IEntityDataClient
    {
        /// <summary>Đọc 1 record theo id (null -> null).</summary>
        Task<JObject> GetAsync(int? id, CancellationToken ct = default(CancellationToken));

        /// <summary>Liệt kê theo filter jsonb.</summary>
        Task<JArray> ListAsync(JObject filter, CancellationToken ct = default(CancellationToken));

        /// <summary>Thêm/sửa theo payload -> {success,data,errors}.</summary>
        Task<JObject> UpsertAsync(JObject payload, CancellationToken ct = default(CancellationToken));

        /// <summary>Xóa theo id -> {success,message}.</summary>
        Task<JObject> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));
    }

    /// <summary>
    /// Triển khai <see cref="IEntityDataClient"/> định tuyến theo <see cref="DbCommandMode"/>.
    ///
    /// Ví dụ dùng:
    /// <code>
    /// // Function mode (mặc định) — dùng lại client function sẵn có:
    /// var fnClient = new NpgsqlFunctionClient(connStr);
    /// IEntityDataClient data = new EntityDataClient(typeof(Customer), DbCommandMode.Function, fnClient, null);
    ///
    /// // RawSql mode — tự sinh SQL từ metadata:
    /// var sqlClient = new NpgsqlSqlCommandClient(connStr);
    /// IEntityDataClient data = new EntityDataClient(typeof(Customer), DbCommandMode.RawSql, null, sqlClient);
    ///
    /// // Hybrid — RawSql + override 1 vài câu lệnh:
    /// var req = PostgresRawSqlBuilder.BuildRequest(typeof(Customer), myOverrides);
    /// IEntityDataClient data = new EntityDataClient(typeof(Customer), DbCommandMode.Hybrid, null, sqlClient, myOverrides);
    /// </code>
    /// </summary>
    public sealed class EntityDataClient : IEntityDataClient
    {
        private readonly DbCommandMode _mode;
        private readonly IDbFunctionClient _functionClient;
        private readonly ISqlCommandClient _sqlClient;
        private readonly DbTableAttribute _table;
        private readonly RawSqlRequest _rawRequest; // chỉ dùng cho RawSql/Hybrid

        public EntityDataClient(
            Type entityType,
            DbCommandMode mode,
            IDbFunctionClient functionClient,
            ISqlCommandClient sqlClient,
            ISqlOverrideProvider overrides = null)
        {
            if (entityType == null) throw new ArgumentNullException("entityType");
            _mode = mode;

            _table = (DbTableAttribute)Attribute.GetCustomAttribute(entityType, typeof(DbTableAttribute));
            if (_table == null)
                throw new InvalidOperationException("Entity " + entityType.Name + " thiếu [DbTable].");

            if (mode == DbCommandMode.Function)
            {
                if (functionClient == null)
                    throw new ArgumentNullException("functionClient", "Function mode cần IDbFunctionClient.");
                _functionClient = functionClient;
            }
            else
            {
                if (sqlClient == null)
                    throw new ArgumentNullException("sqlClient", "RawSql/Hybrid mode cần ISqlCommandClient.");
                _sqlClient = sqlClient;
                var eff = mode == DbCommandMode.Hybrid ? overrides : null;
                _rawRequest = PostgresRawSqlBuilder.BuildRequest(entityType, eff);
            }
        }

        public Task<JObject> GetAsync(int? id, CancellationToken ct = default(CancellationToken))
        {
            return _mode == DbCommandMode.Function
                ? _functionClient.GetAsync(_table.GetFunctionName("get"), id, ct)
                : _sqlClient.GetAsync(_rawRequest, id, ct);
        }

        public Task<JArray> ListAsync(JObject filter, CancellationToken ct = default(CancellationToken))
        {
            return _mode == DbCommandMode.Function
                ? _functionClient.ListAsync(_table.GetFunctionName("list"), filter, ct)
                : _sqlClient.ListAsync(_rawRequest, filter, ct);
        }

        public Task<JObject> UpsertAsync(JObject payload, CancellationToken ct = default(CancellationToken))
        {
            return _mode == DbCommandMode.Function
                ? _functionClient.UpsertAsync(_table.GetFunctionName("upsert"), payload, ct)
                : _sqlClient.UpsertAsync(_rawRequest, payload, ct);
        }

        public Task<JObject> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return _mode == DbCommandMode.Function
                ? _functionClient.DeleteAsync(_table.GetFunctionName("delete"), id, ct)
                : _sqlClient.DeleteAsync(_rawRequest, id, ct);
        }
    }
}
