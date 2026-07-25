using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Core.Data
{
    /// <summary>
    /// Triển khai IDbFunctionClient trên Npgsql 2.2.3 (net45).
    ///
    /// Ghi chú phiên bản QUAN TRỌNG:
    ///  - Npgsql 2.2.3 KHÔNG có NpgsqlDbType.Jsonb (jsonb chỉ có từ Npgsql 3.x).
    ///    => Tham số JSON được truyền dạng TEXT và ép kiểu ngay trong câu gọi bằng CAST(:p AS jsonb).
    ///       Function phía Postgres vẫn khai báo tham số jsonb theo đúng spec.
    ///  - API async của 2.2.3 không đầy đủ/không đáng tin, nên các method async ở đây
    ///    bọc lời gọi đồng bộ trong Task.Run để cung cấp bề mặt async đúng như framework yêu cầu,
    ///    đồng thời hỗ trợ hủy (CancellationToken) ở mức tốt nhất driver cho phép.
    ///
    /// TUYỆT ĐỐI không build SQL INSERT/UPDATE theo field. Chỉ gọi: SELECT fn_xxx(&lt;params&gt;).
    /// </summary>
    public class NpgsqlFunctionClient : IDbFunctionClient
    {
        private readonly string _connectionString;

        /// <summary>Timeout cho mỗi command (giây). Mặc định 30s.</summary>
        public int CommandTimeoutSeconds { get; set; }

        public NpgsqlFunctionClient(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string must not be empty.", "connectionString");
            _connectionString = connectionString;
            CommandTimeoutSeconds = 30;
        }

        public Task<JObject> GetAsync(string functionName, int? id, CancellationToken ct = default(CancellationToken))
        {
            ValidateFn(functionName);
            return Task.Run(() =>
            {
                var scalar = ExecuteScalarText(
                    "SELECT " + functionName + "(:p_id)",
                    cmd => AddIntParam(cmd, "p_id", id),
                    ct);
                return ParseObjectOrNull(scalar);
            }, ct);
        }

        public Task<JArray> ListAsync(string functionName, JObject filter, CancellationToken ct = default(CancellationToken))
        {
            ValidateFn(functionName);
            return Task.Run(() =>
            {
                var json = (filter ?? new JObject()).ToString(Newtonsoft.Json.Formatting.None);
                var scalar = ExecuteScalarText(
                    "SELECT " + functionName + "(CAST(:p_filter AS jsonb))",
                    cmd => AddJsonParam(cmd, "p_filter", json),
                    ct);
                return ParseArrayOrEmpty(scalar);
            }, ct);
        }

        public Task<JObject> UpsertAsync(string functionName, JObject payload, CancellationToken ct = default(CancellationToken))
        {
            ValidateFn(functionName);
            if (payload == null) throw new ArgumentNullException("payload");
            return Task.Run(() =>
            {
                var json = payload.ToString(Newtonsoft.Json.Formatting.None);
                var scalar = ExecuteScalarText(
                    "SELECT " + functionName + "(CAST(:p_payload AS jsonb))",
                    cmd => AddJsonParam(cmd, "p_payload", json),
                    ct);
                return ParseObjectOrNull(scalar) ?? Failure("Function returned NULL.");
            }, ct);
        }

        public Task<JObject> DeleteAsync(string functionName, int id, CancellationToken ct = default(CancellationToken))
        {
            ValidateFn(functionName);
            return Task.Run(() =>
            {
                var scalar = ExecuteScalarText(
                    "SELECT " + functionName + "(:p_id)",
                    cmd => AddIntParam(cmd, "p_id", id),
                    ct);
                return ParseObjectOrNull(scalar) ?? Failure("Function returned NULL.");
            }, ct);
        }

        // ---------------- infrastructure ----------------

        private string ExecuteScalarText(string sql, Action<NpgsqlCommand> bind, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = CommandTimeoutSeconds;
                        bind(cmd);

                        // Cho phép hủy giữa chừng ở mức tốt nhất driver cũ cho phép.
                        using (ct.Register(() => { try { cmd.Cancel(); } catch { } }))
                        {
                            var result = cmd.ExecuteScalar();
                            ct.ThrowIfCancellationRequested();
                            if (result == null || result == DBNull.Value)
                                return null;
                            return result.ToString();
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NpgsqlException ex)
            {
                throw new DbFunctionException(
                    "Lỗi khi gọi function PostgreSQL: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new DbFunctionException(
                    "Lỗi không xác định khi gọi DB: " + ex.Message, ex);
            }
        }

        private static void AddIntParam(NpgsqlCommand cmd, string name, int? value)
        {
            var p = new NpgsqlParameter(name, NpgsqlDbType.Integer);
            p.Value = value.HasValue ? (object)value.Value : DBNull.Value;
            cmd.Parameters.Add(p);
        }

        private static void AddJsonParam(NpgsqlCommand cmd, string name, string jsonText)
        {
            // 2.2.3: truyền text, ép jsonb bằng CAST trong SQL.
            var p = new NpgsqlParameter(name, NpgsqlDbType.Text);
            p.Value = jsonText ?? (object)DBNull.Value;
            cmd.Parameters.Add(p);
        }

        private static void ValidateFn(string functionName)
        {
            if (string.IsNullOrWhiteSpace(functionName))
                throw new ArgumentException("Function name must not be empty.", "functionName");
            // Chặn injection: tên function chỉ cho phép [a-z0-9_.] (schema.function).
            foreach (var ch in functionName)
            {
                if (!(char.IsLetterOrDigit(ch) || ch == '_' || ch == '.'))
                    throw new ArgumentException("Invalid function name: " + functionName, "functionName");
            }
        }

        private static JObject ParseObjectOrNull(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            var token = JToken.Parse(text);
            return token as JObject; // nếu function trả 'null'::jsonb -> JValue null -> trả null
        }

        private static JArray ParseArrayOrEmpty(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return new JArray();
            var token = JToken.Parse(text);
            return (token as JArray) ?? new JArray();
        }

        private static JObject Failure(string message)
        {
            return new JObject
            {
                ["success"] = false,
                ["message"] = message,
                ["errors"] = new JArray()
            };
        }
    }

    /// <summary>Exception thống nhất cho tầng gọi function.</summary>
    [Serializable]
    public class DbFunctionException : Exception
    {
        public DbFunctionException(string message, Exception inner) : base(message, inner) { }
        public DbFunctionException(string message) : base(message) { }
        protected DbFunctionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
