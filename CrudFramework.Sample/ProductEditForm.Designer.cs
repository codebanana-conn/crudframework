namespace CrudFramework.Sample
{
    partial class ProductEditForm
    {
        private System.ComponentModel.IContainer components = null;

        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.SpinEdit spPrice;
        private DevExpress.XtraEditors.SpinEdit spStock;
        private DevExpress.XtraEditors.CheckEdit chkAvailable;
        private DevExpress.XtraEditors.DateEdit dtMfg;
        private DevExpress.XtraEditors.MemoEdit memDescription;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblPrice;
        private DevExpress.XtraEditors.LabelControl lblStock;
        private DevExpress.XtraEditors.LabelControl lblMfg;
        private DevExpress.XtraEditors.LabelControl lblDesc;
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
            this.spStock = new DevExpress.XtraEditors.SpinEdit();
            this.chkAvailable = new DevExpress.XtraEditors.CheckEdit();
            this.dtMfg = new DevExpress.XtraEditors.DateEdit();
            this.memDescription = new DevExpress.XtraEditors.MemoEdit();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblPrice = new DevExpress.XtraEditors.LabelControl();
            this.lblStock = new DevExpress.XtraEditors.LabelControl();
            this.lblMfg = new DevExpress.XtraEditors.LabelControl();
            this.lblDesc = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.entityBindingProvider1 = new CrudFramework.WinForms.EntityBindingProvider(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAvailable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMfg.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMfg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).BeginInit();
            this.SuspendLayout();

            // EntityBindingProvider
            this.entityBindingProvider1.EntityType = typeof(CrudFramework.Sample.Product);
            this.entityBindingProvider1.BindProperty = "EditValue";

            // lblCode / txtCode
            this.lblCode.Location = new System.Drawing.Point(24, 27);
            this.lblCode.Text = "Mã SP:";
            this.txtCode.Location = new System.Drawing.Point(130, 24);
            this.txtCode.Size = new System.Drawing.Size(200, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtCode, "ProductCode");

            // lblName / txtName
            this.lblName.Location = new System.Drawing.Point(24, 61);
            this.lblName.Text = "Tên SP:";
            this.txtName.Location = new System.Drawing.Point(130, 58);
            this.txtName.Size = new System.Drawing.Size(300, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtName, "ProductName");

            // lblPrice / spPrice — decimal, Format="#,##0.00"
            this.lblPrice.Location = new System.Drawing.Point(24, 95);
            this.lblPrice.Text = "Giá:";
            this.spPrice.Location = new System.Drawing.Point(130, 92);
            this.spPrice.Size = new System.Drawing.Size(200, 20);
            this.spPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spPrice.Properties.DisplayFormat.FormatString = "#,##0.00";
            this.spPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spPrice.Properties.EditFormat.FormatString = "#,##0.00";
            this.spPrice.Properties.IsFloatValue = true;
            this.spPrice.Properties.MinValue = 0;
            this.spPrice.Properties.MaxValue = 999999999;
            this.entityBindingProvider1.SetBindingMember(this.spPrice, "Price");

            // lblStock / spStock — int, Format="n0"
            this.lblStock.Location = new System.Drawing.Point(24, 129);
            this.lblStock.Text = "Số lượng:";
            this.spStock.Location = new System.Drawing.Point(130, 126);
            this.spStock.Size = new System.Drawing.Size(200, 20);
            this.spStock.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spStock.Properties.DisplayFormat.FormatString = "n0";
            this.spStock.Properties.IsFloatValue = false;
            this.spStock.Properties.MinValue = 0;
            this.spStock.Properties.MaxValue = 1000000000;
            this.entityBindingProvider1.SetBindingMember(this.spStock, "StockQuantity");

            // chkAvailable — bool
            this.chkAvailable.Location = new System.Drawing.Point(130, 160);
            this.chkAvailable.Text = "Còn hàng";
            this.chkAvailable.Size = new System.Drawing.Size(200, 20);
            this.entityBindingProvider1.SetBindingMember(this.chkAvailable, "IsAvailable");

            // lblMfg / dtMfg — DateTime?, Format="dd/MM/yyyy"
            this.lblMfg.Location = new System.Drawing.Point(24, 194);
            this.lblMfg.Text = "Ngày SX:";
            this.dtMfg.EditValue = null;
            this.dtMfg.Location = new System.Drawing.Point(130, 191);
            this.dtMfg.Size = new System.Drawing.Size(200, 20);
            this.dtMfg.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.entityBindingProvider1.SetBindingMember(this.dtMfg, "ManufacturedDate");

            // lblDesc / memDescription — string (MemoEdit)
            this.lblDesc.Location = new System.Drawing.Point(24, 228);
            this.lblDesc.Text = "Mô tả:";
            this.memDescription.Location = new System.Drawing.Point(130, 225);
            this.memDescription.Size = new System.Drawing.Size(300, 60);
            this.entityBindingProvider1.SetBindingMember(this.memDescription, "Description");

            // btnSave / btnDelete
            this.btnSave.Location = new System.Drawing.Point(130, 295);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "Lưu";
            this.btnDelete.Location = new System.Drawing.Point(240, 295);
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.Text = "Xóa";

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 340);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.spPrice);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.spStock);
            this.Controls.Add(this.chkAvailable);
            this.Controls.Add(this.lblMfg);
            this.Controls.Add(this.dtMfg);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.memDescription);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Name = "ProductEditForm";
            this.Text = "Sửa sản phẩm (demo đủ kiểu dữ liệu)";

            ((System.ComponentModel.ISupportInitialize)(this.dtMfg.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMfg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAvailable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
