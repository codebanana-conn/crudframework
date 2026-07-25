namespace CrudFramework.Sample
{
    partial class ErrorMappingDemoForm
    {
        private System.ComponentModel.IContainer components = null;

        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblHint;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnHint;

        private CrudFramework.WinForms.EntityBindingProvider entityBindingProvider1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblHint = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnHint = new DevExpress.XtraEditors.SimpleButton();
            this.entityBindingProvider1 = new CrudFramework.WinForms.EntityBindingProvider(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).BeginInit();
            this.SuspendLayout();

            this.entityBindingProvider1.EntityType = typeof(CrudFramework.Sample.Customer);
            this.entityBindingProvider1.BindProperty = "EditValue";

            this.lblCode.Location = new System.Drawing.Point(24, 27); this.lblCode.Text = "Mã KH:";
            this.txtCode.Location = new System.Drawing.Point(130, 24); this.txtCode.Size = new System.Drawing.Size(200, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtCode, "CustomerCode");

            this.lblName.Location = new System.Drawing.Point(24, 61); this.lblName.Text = "Tên KH:";
            this.txtName.Location = new System.Drawing.Point(130, 58); this.txtName.Size = new System.Drawing.Size(300, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtName, "CustomerName");

            this.lblHint.Location = new System.Drawing.Point(24, 95);
            this.lblHint.Text = "Gõ mã KH001 rồi bấm Lưu để xem lỗi hiển thị đỏ trên ô Mã KH.";

            this.btnHint.Location = new System.Drawing.Point(130, 130); this.btnHint.Size = new System.Drawing.Size(90, 30); this.btnHint.Text = "Hướng dẫn";
            this.btnSave.Location = new System.Drawing.Point(240, 130); this.btnSave.Size = new System.Drawing.Size(90, 30); this.btnSave.Text = "Lưu";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 180);
            this.Controls.Add(this.lblCode); this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName); this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnSave); this.Controls.Add(this.btnHint);
            this.Name = "ErrorMappingDemoForm";
            this.Text = "Demo error-mapping (DxErrorProviderAdapter)";

            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
    }
}
