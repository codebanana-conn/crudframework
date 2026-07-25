using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Core.Data
{
    /// <summary>
    /// Hợp đồng gọi 4 loại function CRUD trong PostgreSQL. Mọi giao tiếp DB đi qua đây.
    /// KHÔNG có bất kỳ SQL INSERT/UPDATE động nào — chỉ gọi function với tham số jsonb/int.
    /// </summary>
    public interface IDbFunctionClient
    {
        /// <summary>fn_&lt;entity&gt;_get(p_id int) RETURNS jsonb -> 1 record (JObject) hoặc null.</summary>
        Task<JObject> GetAsync(string functionName, int? id, CancellationToken ct = default(CancellationToken));

        /// <summary>fn_&lt;entity&gt;_list(p_filter jsonb) RETURNS jsonb -> JSON array (JArray).</summary>
        Task<JArray> ListAsync(string functionName, JObject filter, CancellationToken ct = default(CancellationToken));

        /// <summary>fn_&lt;entity&gt;_upsert(p_payload jsonb) RETURNS jsonb -> {success,data,errors}.</summary>
        Task<JObject> UpsertAsync(string functionName, JObject payload, CancellationToken ct = default(CancellationToken));

        /// <summary>fn_&lt;entity&gt;_delete(p_id int) RETURNS jsonb -> {success,message}.</summary>
        Task<JObject> DeleteAsync(string functionName, int id, CancellationToken ct = default(CancellationToken));
    }
}
