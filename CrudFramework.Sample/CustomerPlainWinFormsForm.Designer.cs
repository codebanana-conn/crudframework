namespace CrudFramework.Sample
{
    partial class CustomerPlainWinFormsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown numBalance;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
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
            this.lblCode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.numBalance = new System.Windows.Forms.NumericUpDown();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.entityBindingProvider1 = new CrudFramework.WinForms.EntityBindingProvider(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).BeginInit();
            this.SuspendLayout();

            this.entityBindingProvider1.EntityType = typeof(CrudFramework.Sample.Customer);
            this.entityBindingProvider1.UseAdapters = true;

            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(24, 27);
            this.lblCode.Text = "Mã KH:";

            this.txtCode.Location = new System.Drawing.Point(130, 24);
            this.txtCode.Size = new System.Drawing.Size(200, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtCode, "CustomerCode");

            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(24, 61);
            this.lblName.Text = "Tên khách hàng:";

            this.txtName.Location = new System.Drawing.Point(130, 58);
            this.txtName.Size = new System.Drawing.Size(300, 20);
            this.entityBindingProvider1.SetBindingMember(this.txtName, "CustomerName");

            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(24, 95);
            this.lblBalance.Text = "Số dư:";

            this.numBalance.DecimalPlaces = 0;
            this.numBalance.Location = new System.Drawing.Point(130, 92);
            this.numBalance.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numBalance.Minimum = new decimal(new int[] { 1000000000, 0, 0, -2147483648 });
            this.numBalance.Size = new System.Drawing.Size(200, 20);
            this.numBalance.ThousandsSeparator = true;
            this.entityBindingProvider1.SetBindingMember(this.numBalance, "Balance");

            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(130, 126);
            this.chkActive.Text = "Đang hoạt động";
            this.entityBindingProvider1.SetBindingMember(this.chkActive, "IsActive");

            this.btnSave.Location = new System.Drawing.Point(130, 165);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;

            this.btnCancel.Location = new System.Drawing.Point(240, 165);
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 225);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.numBalance);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "CustomerPlainWinFormsForm";
            this.Text = "Customer - WinForms controls";
            ((System.ComponentModel.ISupportInitialize)(this.numBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
