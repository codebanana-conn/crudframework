namespace CrudFramework.Sample
{
    partial class CustomerListForm
    {
        private System.ComponentModel.IContainer components = null;

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtKeyword;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl lblKeyword;

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
            this.txtKeyword = new DevExpress.XtraEditors.TextEdit();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lblKeyword = new DevExpress.XtraEditors.LabelControl();

            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyword.Properties)).BeginInit();
            this.SuspendLayout();

            // ---- lblKeyword ----
            this.lblKeyword.Location = new System.Drawing.Point(12, 15);
            this.lblKeyword.Text = "Tìm kiếm:";

            // ---- txtKeyword ----
            this.txtKeyword.Location = new System.Drawing.Point(80, 12);
            this.txtKeyword.Size = new System.Drawing.Size(250, 20);
            this.txtKeyword.Properties.NullValuePrompt = "Nhập mã hoặc tên khách hàng...";

            // ---- btnSearch ----
            this.btnSearch.Location = new System.Drawing.Point(340, 11);
            this.btnSearch.Size = new System.Drawing.Size(80, 23);
            this.btnSearch.Text = "Tìm";

            // ---- btnAdd ----
            this.btnAdd.Location = new System.Drawing.Point(440, 11);
            this.btnAdd.Size = new System.Drawing.Size(80, 23);
            this.btnAdd.Text = "Thêm";

            // ---- btnEdit ----
            this.btnEdit.Location = new System.Drawing.Point(530, 11);
            this.btnEdit.Size = new System.Drawing.Size(80, 23);
            this.btnEdit.Text = "Sửa";

            // ---- btnDelete ----
            this.btnDelete.Location = new System.Drawing.Point(620, 11);
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.Text = "Xóa";

            // ---- gridControl1 ----
            this.gridControl1.Location = new System.Drawing.Point(0, 48);
            this.gridControl1.Size = new System.Drawing.Size(800, 412);
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView1 });
            this.gridControl1.MainView = this.gridView1;

            // ---- gridView1 ----
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;

            // ---- Form ----
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 460);
            this.Controls.Add(this.lblKeyword);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.gridControl1);
            this.Name = "CustomerListForm";
            this.Text = "Danh sách khách hàng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
