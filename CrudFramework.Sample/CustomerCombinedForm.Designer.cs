namespace CrudFramework.Sample
{
    partial class CustomerCombinedForm
    {
        private System.ComponentModel.IContainer components = null;

        // Grid
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;

        // Detail labels
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblBirth;
        private DevExpress.XtraEditors.LabelControl lblBalance;

        // Detail editors
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.DateEdit dtBirth;
        private DevExpress.XtraEditors.SpinEdit spBalance;
        private DevExpress.XtraEditors.CheckEdit chkActive;

        // Buttons
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnOpenDetailForm;

        // Status
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.SeparatorControl separatorTop;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.separatorTop = new DevExpress.XtraEditors.SeparatorControl();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblBirth = new DevExpress.XtraEditors.LabelControl();
            this.lblBalance = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.dtBirth = new DevExpress.XtraEditors.DateEdit();
            this.spBalance = new DevExpress.XtraEditors.SpinEdit();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenDetailForm = new DevExpress.XtraEditors.SimpleButton();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();

            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBirth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBirth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spBalance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            this.SuspendLayout();

            // ================================================================
            //  GRID (phần trên — chiếm khoảng 60% chiều cao form)
            // ================================================================
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.Size = new System.Drawing.Size(900, 300);
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView1 });
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.Height = 300;

            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

            // ================================================================
            //  SEPARATOR
            // ================================================================
            this.separatorTop.Location = new System.Drawing.Point(0, 302);
            this.separatorTop.Size = new System.Drawing.Size(900, 5);

            // ================================================================
            //  DETAIL (phần dưới)
            // ================================================================

            int yBase = 315;
            int yGap = 32;
            int xLabel = 15;
            int xEditor = 120;

            // ---- lblCode ----
            this.lblCode.Location = new System.Drawing.Point(xLabel, yBase + 2);
            this.lblCode.Text = "Mã KH:";

            // ---- txtCode ----
            this.txtCode.Location = new System.Drawing.Point(xEditor, yBase);
            this.txtCode.Size = new System.Drawing.Size(200, 20);

            // ---- lblName ----
            this.lblName.Location = new System.Drawing.Point(xLabel, yBase + yGap + 2);
            this.lblName.Text = "Tên khách hàng:";

            // ---- txtName ----
            this.txtName.Location = new System.Drawing.Point(xEditor, yBase + yGap);
            this.txtName.Size = new System.Drawing.Size(300, 20);

            // ---- lblBirth ----
            this.lblBirth.Location = new System.Drawing.Point(xLabel, yBase + yGap * 2 + 2);
            this.lblBirth.Text = "Ngày sinh:";

            // ---- dtBirth ----
            this.dtBirth.EditValue = null;
            this.dtBirth.Location = new System.Drawing.Point(xEditor, yBase + yGap * 2);
            this.dtBirth.Size = new System.Drawing.Size(200, 20);
            this.dtBirth.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.dtBirth.Properties.Mask.EditMask = "dd/MM/yyyy";

            // ---- lblBalance ----
            this.lblBalance.Location = new System.Drawing.Point(xLabel, yBase + yGap * 3 + 2);
            this.lblBalance.Text = "Số dư:";

            // ---- spBalance ----
            this.spBalance.Location = new System.Drawing.Point(xEditor, yBase + yGap * 3);
            this.spBalance.Size = new System.Drawing.Size(200, 20);
            this.spBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spBalance.Properties.DisplayFormat.FormatString = "n0";

            // ---- chkActive ----
            this.chkActive.Location = new System.Drawing.Point(xEditor, yBase + yGap * 4 + 2);
            this.chkActive.Text = "Đang hoạt động";
            this.chkActive.Size = new System.Drawing.Size(200, 20);

            // ================================================================
            //  BUTTONS (hàng ngang dưới cùng detail)
            // ================================================================
            int yBtn = yBase + yGap * 5 + 10;
            int xBtn = xEditor;
            int btnW = 85;
            int btnGap = 6;

            this.btnAdd.Location = new System.Drawing.Point(xBtn, yBtn);
            this.btnAdd.Size = new System.Drawing.Size(btnW, 28);
            this.btnAdd.Text = "Thêm mới";

            this.btnSave.Location = new System.Drawing.Point(xBtn + (btnW + btnGap), yBtn);
            this.btnSave.Size = new System.Drawing.Size(btnW, 28);
            this.btnSave.Text = "Lưu";

            this.btnDelete.Location = new System.Drawing.Point(xBtn + (btnW + btnGap) * 2, yBtn);
            this.btnDelete.Size = new System.Drawing.Size(btnW, 28);
            this.btnDelete.Text = "Xóa";

            this.btnRefresh.Location = new System.Drawing.Point(xBtn + (btnW + btnGap) * 3, yBtn);
            this.btnRefresh.Size = new System.Drawing.Size(btnW, 28);
            this.btnRefresh.Text = "Reload";

            this.btnOpenDetailForm.Location = new System.Drawing.Point(xBtn + (btnW + btnGap) * 4, yBtn);
            this.btnOpenDetailForm.Size = new System.Drawing.Size(btnW + 30, 28);
            this.btnOpenDetailForm.Text = "Mở Detail Form";

            // ---- lblStatus ----
            this.lblStatus.Location = new System.Drawing.Point(xBtn + (btnW + btnGap) * 4 + btnW + 50, yBtn + 6);
            this.lblStatus.Text = "";
            this.lblStatus.Appearance.ForeColor = System.Drawing.Color.Gray;

            // ================================================================
            //  FORM
            // ================================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 460);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.separatorTop);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblBirth);
            this.Controls.Add(this.dtBirth);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.spBalance);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnOpenDetailForm);
            this.Controls.Add(this.lblStatus);
            this.Name = "CustomerCombinedForm";
            this.Text = "CrudFramework Sample - CRUD trên cùng 1 form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(800, 400);

            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBirth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBirth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spBalance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
