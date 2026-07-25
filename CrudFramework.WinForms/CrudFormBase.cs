using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Attributes;
using CrudFramework.Core.Data;
using CrudFramework.Core.Entities;
using CrudFramework.Core.Json;
using CrudFramework.WinForms.ErrorMapping;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Newtonsoft.Json.Linq;

namespace CrudFramework.WinForms
{
    /// <summary>
    /// Base class cho Detail Form — designer-friendly (không generic).
    /// Chứa TOÀN BỘ logic CRUD: Load, Collect, Save, Delete.
    ///
    /// Cách dùng trong Designer:
    ///   Form1 : CrudFormBase → kế thừa class này (non-generic)
    ///   Trong constructor/Load: set EntityType, Client, BindingProvider, ErrorProvider
    ///   Gọi LoadDataAsync / SaveAsync / DeleteAsync trực tiếp.
    ///
    /// Vòng đời (virtual method trước, event sau):
    ///   LoadDataAsync   : OnBeforeLoad → (get) → OnAfterLoad → Bind
    ///   CollectFormToJsonAsync : serialize [DbColumn] → OnBeforeCollectToJson
    ///   SaveAsync       : Collect → OnBeforeSave → (upsert) → OnValidationFailed / OnAfterSave
    ///   DeleteAsync     : OnBeforeDelete → (delete) → OnAfterDelete
    /// </summary>
    public class CrudFormBase : XtraForm
    {
        private DbTableAttribute _table;

        /// <summary>Client gọi function DB.</summary>
        public IDbFunctionClient Client { get; set; }

        /// <summary>Provider binding kéo-thả.</summary>
        public EntityBindingProvider BindingProvider { get; set; }

        /// <summary>DXErrorProvider để hiển thị lỗi validate.</summary>
        public DXErrorProvider ErrorProvider { get; set; }

        /// <summary>Kiểu entity (POCO có [DbTable]). Set khi khởi tạo form.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type EntityType
        {
            get { return _entityType; }
            set
            {
                _entityType = value;
                if (value != null)
                    _table = (DbTableAttribute)Attribute.GetCustomAttribute(value, typeof(DbTableAttribute));
            }
        }
        private Type _entityType;

        /// <summary>Entity hiện tại đang chỉnh sửa.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Current { get; set; }

        /// <summary>Id đang mở (null = thêm mới).</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? CurrentId { get; set; }

        private DxErrorProviderAdapter _errorAdapter;
        protected DxErrorProviderAdapter ErrorAdapter => _errorAdapter;

        protected void EnsureErrorAdapter()
        {
            if (_errorAdapter == null && ErrorProvider != null && BindingProvider != null)
                _errorAdapter = new DxErrorProviderAdapter(ErrorProvider, BindingProvider);
        }

        // ==================== LOAD ====================
        public virtual async Task LoadDataAsync(int? id)
        {
            if (_entityType == null)
                throw new InvalidOperationException("Cần set EntityType trước khi gọi LoadDataAsync.");

            CurrentId = id;
            OnBeforeLoad(id);
            RaiseBeforeLoad(id);

            object entity;
            if (id.HasValue && Client != null)
            {
                var json = await Client.GetAsync(_table.GetFunctionName("get"), id).ConfigureAwait(true);
                entity = json != null ? EntityJsonMapper.FromJObject(json, _entityType) : CreateEntity();
            }
            else
            {
                entity = CreateEntity();
            }

            Current = entity;
            OnAfterLoad(entity);
            RaiseAfterLoad(entity);

            if (BindingProvider != null)
                BindingProvider.Bind(entity);

            EnsureErrorAdapter();
        }

        private object CreateEntity()
        {
            return Activator.CreateInstance(_entityType);
        }

        // ==================== COLLECT -> JSON ====================
        public virtual Task<JObject> CollectFormToJsonAsync()
        {
            if (Current == null) Current = CreateEntity();

            var json = EntityJsonMapper.ToJObject(Current, forUpsert: true);

            OnBeforeCollectToJson(json);
            RaiseBeforeCollectToJson(json);

            return Task.FromResult(json);
        }

        // ==================== SAVE ====================
        public virtual async Task<bool> SaveAsync()
        {
            if (Client == null) throw new InvalidOperationException("Client chưa được gán.");
            EnsureErrorAdapter();
            if (ErrorAdapter != null) ErrorAdapter.Clear();

            var json = await CollectFormToJsonAsync().ConfigureAwait(true);

            OnBeforeSave(json);
            RaiseBeforeSave(json);

            var result = await Client.UpsertAsync(_table.GetFunctionName("upsert"), json).ConfigureAwait(true);

            bool success = result != null && result.Value<bool?>("success") == true;
            if (!success)
            {
                var dtos = EntityJsonMapper.ParseErrors(result != null ? result["errors"] : null);
                var errors = new List<FieldError>();
                foreach (var d in dtos) errors.Add(new FieldError(d.Field, d.Message));

                OnValidationFailed(errors);
                RaiseValidationFailed(errors);

                IList<FieldError> unmapped = ErrorAdapter != null
                    ? ErrorAdapter.Apply(errors)
                    : errors;

                if (unmapped != null && unmapped.Count > 0)
                {
                    var msg = string.Empty;
                    foreach (var e in unmapped) msg += "• " + e.Message + Environment.NewLine;
                    if (!string.IsNullOrEmpty(msg))
                        XtraMessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            var data = result["data"] as JObject;
            if (data != null && Current != null)
                EntityJsonMapper.PopulateFromJObject(Current, data);

            OnAfterSave(result);
            RaiseAfterSave(result);
            return true;
        }

        // ==================== DELETE ====================
        public virtual async Task DeleteAsync(int id)
        {
            if (Client == null) throw new InvalidOperationException("Client chưa được gán.");

            OnBeforeDelete(id);
            RaiseBeforeDelete(id);

            var result = await Client.DeleteAsync(_table.GetFunctionName("delete"), id).ConfigureAwait(true);
            bool success = result != null && result.Value<bool?>("success") == true;
            if (!success)
            {
                var m = result != null ? (string)result["message"] : "Xóa thất bại.";
                XtraMessageBox.Show(m, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OnAfterDelete();
            RaiseAfterDelete();
        }

        // ==================== VIRTUAL HOOKS ====================
        protected virtual void OnBeforeLoad(int? id) { }
        protected virtual void OnAfterLoad(object data) { }
        protected virtual void OnBeforeCollectToJson(JObject json) { }
        protected virtual void OnBeforeSave(JObject json) { }
        protected virtual void OnAfterSave(JObject result) { }
        protected virtual void OnBeforeDelete(int id) { }
        protected virtual void OnAfterDelete() { }
        protected virtual void OnValidationFailed(IEnumerable<FieldError> errors) { }

        // ==================== EVENTS ====================
        public event EventHandler<CrudEventArgs<int?>> BeforeLoad;
        public event EventHandler<CrudEventArgs<object>> AfterLoad;
        public event EventHandler<CrudEventArgs<JObject>> BeforeCollectToJson;
        public event EventHandler<CrudEventArgs<JObject>> BeforeSave;
        public event EventHandler<CrudEventArgs<JObject>> AfterSave;
        public event EventHandler<CrudEventArgs<int>> BeforeDelete;
        public event EventHandler AfterDelete;
        public event EventHandler<CrudEventArgs<IEnumerable<FieldError>>> ValidationFailed;

        private void RaiseBeforeLoad(int? id) { var h = BeforeLoad; if (h != null) h(this, new CrudEventArgs<int?>(id)); }
        private void RaiseAfterLoad(object e) { var h = AfterLoad; if (h != null) h(this, new CrudEventArgs<object>(e)); }
        private void RaiseBeforeCollectToJson(JObject j) { var h = BeforeCollectToJson; if (h != null) h(this, new CrudEventArgs<JObject>(j)); }
        private void RaiseBeforeSave(JObject j) { var h = BeforeSave; if (h != null) h(this, new CrudEventArgs<JObject>(j)); }
        private void RaiseAfterSave(JObject j) { var h = AfterSave; if (h != null) h(this, new CrudEventArgs<JObject>(j)); }
        private void RaiseBeforeDelete(int id) { var h = BeforeDelete; if (h != null) h(this, new CrudEventArgs<int>(id)); }
        private void RaiseAfterDelete() { var h = AfterDelete; if (h != null) h(this, EventArgs.Empty); }
        private void RaiseValidationFailed(IEnumerable<FieldError> e) { var h = ValidationFailed; if (h != null) h(this, new CrudEventArgs<IEnumerable<FieldError>>(e)); }
    }

    /// <summary>
    /// Generic convenience wrapper — kế thừa CrudFormBase, thêm typed Current.
    /// KHÔNG dùng cho form cần designer (dùng CrudFormBase non-generic).
    /// Dùng cho code-only / non-designer forms.
    /// </summary>
    public class CrudFormBase<TEntity> : CrudFormBase where TEntity : EntityBase, new()
    {
        public new TEntity Current
        {
            get { return (TEntity)base.Current; }
            set { base.Current = value; }
        }

        public CrudFormBase()
        {
            EntityType = typeof(TEntity);
        }

        protected override void OnAfterLoad(object data)
        {
            OnAfterLoad((TEntity)data);
        }

        protected virtual void OnAfterLoad(TEntity data) { }
    }

    /// <summary>EventArgs generic mang payload cho các event vòng đời.</summary>
    public class CrudEventArgs<T> : EventArgs
    {
        public T Data { get; private set; }
        public CrudEventArgs(T data) { Data = data; }
    }
}
