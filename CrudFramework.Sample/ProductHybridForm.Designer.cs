namespace CrudFramework.Sample
{
    partial class ProductHybridForm
    {
        private System.ComponentModel.IContainer components = null;

        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.SpinEdit spPrice;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblPrice;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDelete;

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
            this.spPrice = new DevExpress.XtraEditors.SpinEdit();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblPrice = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.entityBindingProvider1 = new CrudFramework.WinForms.EntityBindingProvider(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).BeginInit();
            this.SuspendLayout();

            this.entityBindingProvider1.EntityType = typeof(CrudFramework.Sample.Product);
            this.entityBindingProvider1.BindProperty = "EditValue";

            this.lblCode.Location = new System.Drawing.Point(24, 27); this.lblCode.Text = "Mã SP:";
            this.txtCode.Location = new System.Drawing.Point(130, 24); this.txtCode.Size = new System.Drawing.Size(200, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtCode, "ProductCode");

            this.lblName.Location = new System.Drawing.Point(24, 61); this.lblName.Text = "Tên SP:";
            this.txtName.Location = new System.Drawing.Point(130, 58); this.txtName.Size = new System.Drawing.Size(300, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtName, "ProductName");

            this.lblPrice.Location = new System.Drawing.Point(24, 95); this.lblPrice.Text = "Giá:";
            this.spPrice.Location = new System.Drawing.Point(130, 92); this.spPrice.Size = new System.Drawing.Size(200, 20);
            this.spPrice.Properties.IsFloatValue = true;
            this.spPrice.Properties.DisplayFormat.FormatString = "#,##0.00";
            this.entityBindingProvider1.SetBindingMember(this.spPrice, "Price");

            this.btnSave.Location = new System.Drawing.Point(130, 135); this.btnSave.Size = new System.Drawing.Size(90, 30); this.btnSave.Text = "Lưu";
            this.btnDelete.Location = new System.Drawing.Point(240, 135); this.btnDelete.Size = new System.Drawing.Size(90, 30); this.btnDelete.Text = "Xóa";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 180);
            this.Controls.Add(this.lblCode); this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName); this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPrice); this.Controls.Add(this.spPrice);
            this.Controls.Add(this.btnSave); this.Controls.Add(this.btnDelete);
            this.Name = "ProductHybridForm";
            this.Text = "Product — Hybrid (override ListSql)";

            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
    }
}
