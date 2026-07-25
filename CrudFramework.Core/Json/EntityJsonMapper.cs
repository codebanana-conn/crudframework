using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CrudFramework.Core.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrudFramework.Core.Json
{
    /// <summary>
    /// Metadata của một property đã được resolve từ [DbColumn] — cache lại để tránh reflection lặp.
    /// </summary>
    public sealed class ColumnMap
    {
        public PropertyInfo Property { get; internal set; }
        /// <summary>Tên cột JSON/DB (đã resolve, luôn khác null).</summary>
        public string ColumnName { get; internal set; }
        public string Caption { get; internal set; }
        public int Width { get; internal set; }
        public string Format { get; internal set; }
        public int Order { get; internal set; }
        public bool Ignore { get; internal set; }
        public bool ReadOnly { get; internal set; }
        public bool HiddenInGrid { get; internal set; }

        public string PropertyName { get { return Property.Name; } }
    }

    /// <summary>
    /// Chịu trách nhiệm chuyển đổi 2 chiều giữa entity (POCO có [DbColumn]) và JSON (Newtonsoft JObject/JArray).
    /// KHÔNG phụ thuộc UI, KHÔNG phụ thuộc DB — có thể unit test độc lập bằng entity giả.
    ///
    /// Quy tắc:
    ///  - Chỉ property có [DbColumn] (và Ignore = false) mới được serialize.
    ///  - Property có [DbColumn(ReadOnly = true)] được đọc từ JSON (deserialize) nhưng KHÔNG ghi ra JSON upsert.
    ///  - Tên cột: DbColumn.Name nếu có, ngược lại chuyển PascalCase -> snake_case.
    /// </summary>
    public static class EntityJsonMapper
    {
        private static readonly Dictionary<Type, ColumnMap[]> _cache = new Dictionary<Type, ColumnMap[]>();
        private static readonly object _lock = new object();

        /// <summary>Lấy (và cache) danh sách ColumnMap của một entity type, đã sắp theo Order.</summary>
        public static ColumnMap[] GetColumns(Type entityType)
        {
            if (entityType == null) throw new ArgumentNullException("entityType");

            lock (_lock)
            {
                ColumnMap[] cached;
                if (_cache.TryGetValue(entityType, out cached))
                    return cached;

                var list = new List<ColumnMap>();
                var props = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var p in props)
                {
                    var attr = p.GetCustomAttribute<DbColumnAttribute>(true);
                    if (attr == null)
                        continue; // Không có [DbColumn] -> bỏ qua hoàn toàn.

                    var map = new ColumnMap
                    {
                        Property = p,
                        ColumnName = !string.IsNullOrWhiteSpace(attr.Name) ? attr.Name : ToSnakeCase(p.Name),
                        Caption = attr.Caption ?? p.Name,
                        Width = attr.Width,
                        Format = attr.Format,
                        Order = attr.Order,
                        Ignore = attr.Ignore,
                        ReadOnly = attr.ReadOnly,
                        HiddenInGrid = attr.HiddenInGrid
                    };
                    list.Add(map);
                }

                var arr = list.OrderBy(c => c.Order).ThenBy(c => c.PropertyName).ToArray();
                _cache[entityType] = arr;
                return arr;
            }
        }

        /// <summary>Map từ tên cột JSON/DB -> ColumnMap. Dùng cho error mapping (field -> property/control).</summary>
        public static IReadOnlyDictionary<string, ColumnMap> GetColumnLookup(Type entityType)
        {
            var dict = new Dictionary<string, ColumnMap>(StringComparer.OrdinalIgnoreCase);
            foreach (var c in GetColumns(entityType))
            {
                if (c.Ignore) continue;
                if (!dict.ContainsKey(c.ColumnName))
                    dict.Add(c.ColumnName, c);
            }
            return dict;
        }

        /// <summary>
        /// Serialize entity -> JObject theo [DbColumn]. Bỏ qua Ignore và (nếu forUpsert) bỏ qua ReadOnly.
        /// </summary>
        public static JObject ToJObject(object entity, bool forUpsert = true)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var jo = new JObject();
            foreach (var c in GetColumns(entity.GetType()))
            {
                if (c.Ignore) continue;
                if (forUpsert && c.ReadOnly) continue;

                var value = c.Property.GetValue(entity, null);
                jo[c.ColumnName] = value == null ? JValue.CreateNull() : JToken.FromObject(value);
            }
            return jo;
        }

        /// <summary>Deserialize một JSON object (record) -> entity mới kiểu TEntity.</summary>
        public static TEntity FromJObject<TEntity>(JObject json) where TEntity : new()
        {
            var entity = new TEntity();
            PopulateFromJObject(entity, json);
            return entity;
        }

        /// <summary>Deserialize JSON -> entity mới theo entityType (non-generic, dùng runtime reflection).</summary>
        public static object FromJObject(JObject json, Type entityType)
        {
            var entity = Activator.CreateInstance(entityType);
            PopulateFromJObject(entity, json);
            return entity;
        }

        /// <summary>Đổ dữ liệu từ JSON object vào một entity đã có (giữ nguyên instance để không phá vỡ binding).</summary>
        public static void PopulateFromJObject(object entity, JObject json)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            if (json == null) return;

            foreach (var c in GetColumns(entity.GetType()))
            {
                if (c.Ignore) continue;
                JToken token;
                if (!json.TryGetValue(c.ColumnName, StringComparison.OrdinalIgnoreCase, out token))
                    continue;
                if (token == null || token.Type == JTokenType.Null)
                {
                    if (IsNullableTarget(c.Property.PropertyType))
                        c.Property.SetValue(entity, null, null);
                    continue;
                }
                var converted = token.ToObject(c.Property.PropertyType);
                c.Property.SetValue(entity, converted, null);
            }
        }

        /// <summary>Deserialize JSON array -> danh sách entity (nguồn cho GridControl).</summary>
        public static List<TEntity> FromJArray<TEntity>(JArray array) where TEntity : new()
        {
            var result = new List<TEntity>();
            if (array == null) return result;
            foreach (var item in array)
            {
                var obj = item as JObject;
                if (obj != null)
                    result.Add(FromJObject<TEntity>(obj));
            }
            return result;
        }

        /// <summary>Parse mảng "errors" trong kết quả upsert thành danh sách FieldError.</summary>
        public static List<FieldErrorDto> ParseErrors(JToken errorsToken)
        {
            var list = new List<FieldErrorDto>();
            var arr = errorsToken as JArray;
            if (arr == null) return list;
            foreach (var e in arr)
            {
                var o = e as JObject;
                if (o == null) continue;
                list.Add(new FieldErrorDto
                {
                    Field = (string)o["field"],
                    Message = (string)o["message"]
                });
            }
            return list;
        }

        // ---- helpers ----

        internal static bool IsNullableTarget(Type t)
        {
            return !t.IsValueType || (Nullable.GetUnderlyingType(t) != null);
        }

        /// <summary>PascalCase/camelCase -> snake_case. VD "CustomerName" -> "customer_name", "ID" -> "id".</summary>
        public static string ToSnakeCase(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;
            var sb = new System.Text.StringBuilder(name.Length + 8);
            for (int i = 0; i < name.Length; i++)
            {
                char ch = name[i];
                if (char.IsUpper(ch))
                {
                    bool prevLower = i > 0 && char.IsLower(name[i - 1]);
                    bool nextLower = i + 1 < name.Length && char.IsLower(name[i + 1]);
                    if (i > 0 && (prevLower || nextLower))
                        sb.Append('_');
                    sb.Append(char.ToLowerInvariant(ch));
                }
                else
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }
    }

    /// <summary>DTO nội bộ cho phần Core (tránh phụ thuộc ngược vào Entities namespace nếu cần tách).</summary>
    public sealed class FieldErrorDto
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}
