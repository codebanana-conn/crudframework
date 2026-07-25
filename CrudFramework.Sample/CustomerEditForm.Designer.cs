namespace CrudFramework.Sample
{
    partial class CustomerEditForm
    {
        private System.ComponentModel.IContainer components = null;

        // Editors DevExpress (kéo-thả trên Designer)
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.DateEdit dtBirth;
        private DevExpress.XtraEditors.SpinEdit spBalance;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblBirth;
        private DevExpress.XtraEditors.LabelControl lblBalance;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDelete;

        // Thành phần framework (binding kéo-thả) + error provider
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
            this.dtBirth = new DevExpress.XtraEditors.DateEdit();
            this.spBalance = new DevExpress.XtraEditors.SpinEdit();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblBirth = new DevExpress.XtraEditors.LabelControl();
            this.lblBalance = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.entityBindingProvider1 = new CrudFramework.WinForms.EntityBindingProvider(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBirth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBirth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spBalance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).BeginInit();
            this.SuspendLayout();

            // ---- EntityBindingProvider: khai 1 lần, mở rộng property BindingMember cho mọi control ----
            this.entityBindingProvider1.EntityType = typeof(CrudFramework.Sample.Customer);
            this.entityBindingProvider1.BindProperty = "EditValue";

            // ---- lblCode ----
            this.lblCode.Location = new System.Drawing.Point(24, 27);
            this.lblCode.Text = "Mã KH:";
            // ---- txtCode ----
            this.txtCode.Location = new System.Drawing.Point(130, 24);
            this.txtCode.Size = new System.Drawing.Size(200, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtCode, "CustomerCode");

            // ---- lblName ----
            this.lblName.Location = new System.Drawing.Point(24, 61);
            this.lblName.Text = "Tên khách hàng:";
            // ---- txtName ----
            this.txtName.Location = new System.Drawing.Point(130, 58);
            this.txtName.Size = new System.Drawing.Size(300, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtName, "CustomerName");

            // ---- lblBirth ----
            this.lblBirth.Location = new System.Drawing.Point(24, 95);
            this.lblBirth.Text = "Ngày sinh:";
            // ---- dtBirth ----
            this.dtBirth.EditValue = null;
            this.dtBirth.Location = new System.Drawing.Point(130, 92);
            this.dtBirth.Size = new System.Drawing.Size(200, 20);
            this.dtBirth.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.dtBirth.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.entityBindingProvider1.SetBindingMember(this.dtBirth, "BirthDate");

            // ---- lblBalance ----
            this.lblBalance.Location = new System.Drawing.Point(24, 129);
            this.lblBalance.Text = "Số dư:";
            // ---- spBalance ----
            this.spBalance.Location = new System.Drawing.Point(130, 126);
            this.spBalance.Size = new System.Drawing.Size(200, 20);
            this.spBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spBalance.Properties.DisplayFormat.FormatString = "n0";
            this.entityBindingProvider1.SetBindingMember(this.spBalance, "Balance");

            // ---- chkActive ----
            this.chkActive.Location = new System.Drawing.Point(130, 160);
            this.chkActive.Text = "Đang hoạt động";
            this.chkActive.Size = new System.Drawing.Size(200, 20);
            this.entityBindingProvider1.SetBindingMember(this.chkActive, "IsActive");

            // ---- btnSave ----
            this.btnSave.Location = new System.Drawing.Point(130, 200);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "Lưu";
            // ---- btnDelete ----
            this.btnDelete.Location = new System.Drawing.Point(240, 200);
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.Text = "Xóa";

            // ---- Form ----
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 260);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblBirth);
            this.Controls.Add(this.dtBirth);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.spBalance);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Name = "CustomerEditForm";
            this.Text = "Sửa khách hàng (demo generic base + lớp trung gian)";

            ((System.ComponentModel.ISupportInitialize)(this.dtBirth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBirth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spBalance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
