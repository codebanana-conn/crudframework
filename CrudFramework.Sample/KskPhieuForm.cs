using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudFramework.Core.Data;
using CrudFramework.WinForms;
using DevExpress.XtraEditors;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Demo phieu Kham suc khoe (KSK) theo Thong tu 25/2026/TT-BYT.
    /// Ke thua CrudFormBase (non-generic) — dung EntityType = typeof(KskPhieu).
    /// XtraTabControl: tab Tong hop + 4 tab con theo loai mau KSK.
    /// Show/hide tab con theo LoaiMauKsk (1:Tre em, 2:6-18t, 3:&gt;=18t, 4:Tam than).
    /// DropDownButton "In phieu ket qua" co menu con 4 mau in (placeholder MessageBox).
    /// </summary>
    public partial class KskPhieuForm : CrudFormBase
    {
        private IDbFunctionClient _client;

        /// <summary>
        /// Khoi tao form KSK voi client Function mode.
        /// </summary>
        /// <param name="client">IDbFunctionClient goi fn_ksk_phieu_*.</param>
        /// <param name="id">Ma phieu (null = tao moi).</param>
        public KskPhieuForm(IDbFunctionClient client, int? id)
        {
            InitializeComponent();

            EntityType = typeof(KskPhieu);
            Client = client;
            BindingProvider = entityBindingProvider1;
            ErrorProvider = dxErrorProvider1;
            _client = client;

            InitComponents();

            // Gan su kien nut
            btnSave.Click += async (s, e) => await SaveAndCloseAsync();
            btnDelete.Click += async (s, e) => await DeleteAndCloseAsync();

            Load += async (s, e) => await InitAndLoadAsync(id);
        }

        // ===================================================================
        //  InitComponents — tao + xep layout + gan binding cho tat ca control
        //  (Vi khong dung Designer drag-drop, "new" va set property bang code.)
        // ===================================================================

        private void InitComponents()
        {
            // EntityBindingProvider
            entityBindingProvider1.EntityType = typeof(KskPhieu);
            entityBindingProvider1.BindProperty = "EditValue";

            // Button panel (dock bottom)
            var btnPanel = new Panel();
            btnPanel.Dock = DockStyle.Bottom;
            btnPanel.Height = 44;

            btnSave.Text = "Luu";
            btnSave.Size = new Size(90, 32);
            btnSave.Location = new Point(16, 6);

            btnDelete.Text = "Xoa";
            btnDelete.Size = new Size(90, 32);
            btnDelete.Location = new Point(120, 6);

            btnInPhieu.Text = "In phieu ket qua";
            btnInPhieu.Size = new Size(160, 32);
            btnInPhieu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnDelete);
            btnPanel.Controls.Add(btnInPhieu);

            Controls.Add(btnPanel);
            btnPanel.BringToFront();

            BuildTongHopTab();
            BuildTreEmTab();
            Build6_18Tab();
            Build18Tab();
            BuildTamThanTab();
        }

        // ===========================================================
        //  TAB TONG HOP
        // ===========================================================
        private void BuildTongHopTab()
        {
            int x1 = 20, x2 = 420, y = 20, dy = 28, lblW = 100;

            lblLoaiMauKsk.Text = "Loại mẫu KSK:";
            lblLoaiMauKsk.Location = new Point(x1, y);
            comboLoaiMauKsk.Location = new Point(x1 + lblW, y);
            comboLoaiMauKsk.Size = new Size(280, 20);
            entityBindingProvider1.SetBindingMember(comboLoaiMauKsk, "LoaiMauKsk");

            y += dy;
            lblHoTen.Text = "Họ lót:";
            lblHoTen.Location = new Point(x1, y);
            txtHolot.Location = new Point(x1 + lblW, y);
            txtHolot.Size = new Size(180, 20);
            entityBindingProvider1.SetBindingMember(txtHolot, "Holot");
            txtTen.Location = new Point(x1 + lblW + 190, y);
            txtTen.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(txtTen, "Ten");

            y += dy;
            lblNgaySinh.Text = "Ngày sinh:";
            lblNgaySinh.Location = new Point(x1, y);
            dtNgaysinh.EditValue = null;
            dtNgaysinh.Location = new Point(x1 + lblW, y);
            dtNgaysinh.Size = new Size(150, 20);
            dtNgaysinh.Properties.Mask.EditMask = "dd/MM/yyyy";
            entityBindingProvider1.SetBindingMember(dtNgaysinh, "Ngaysinh");
            lblGioitinh.Text = "Giới tính:";
            lblGioitinh.Location = new Point(x2, y);
            comboGioitinh.Location = new Point(x2 + lblW, y);
            comboGioitinh.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboGioitinh, "Gioitinh");

            y += dy;
            lblCccd.Text = "CCCD:";
            lblCccd.Location = new Point(x1, y);
            txtCmnd.Location = new Point(x1 + lblW, y);
            txtCmnd.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(txtCmnd, "Cmnd");
            lblNgayCap.Text = "Ngày cấp:";
            lblNgayCap.Location = new Point(x2, y);
            dtNgaycap.EditValue = null;
            dtNgaycap.Location = new Point(x2 + lblW, y);
            dtNgaycap.Size = new Size(150, 20);
            dtNgaycap.Properties.Mask.EditMask = "dd/MM/yyyy";
            entityBindingProvider1.SetBindingMember(dtNgaycap, "Ngaycap");

            y += dy;
            lblNoiCap.Text = "Nơi cấp:";
            lblNoiCap.Location = new Point(x1, y);
            txtNoicap.Location = new Point(x1 + lblW, y);
            txtNoicap.Size = new Size(200, 20);
            entityBindingProvider1.SetBindingMember(txtNoicap, "Noicap");
            lblDiachi.Text = "Địa chỉ:";
            lblDiachi.Location = new Point(x2, y);
            txtDiachi.Location = new Point(x2 + lblW, y);
            txtDiachi.Size = new Size(250, 20);
            entityBindingProvider1.SetBindingMember(txtDiachi, "Diachi");

            y += dy;
            lblNgayKsk.Text = "Ngày KSK:";
            lblNgayKsk.Location = new Point(x1, y);
            dtNgayKsk.EditValue = null;
            dtNgayKsk.Location = new Point(x1 + lblW, y);
            dtNgayKsk.Size = new Size(150, 20);
            dtNgayKsk.Properties.Mask.EditMask = "dd/MM/yyyy";
            entityBindingProvider1.SetBindingMember(dtNgayKsk, "NgayKsk");
            lblCosoKham.Text = "Cơ sở khám:";
            lblCosoKham.Location = new Point(x2, y);
            txtCosoKham.Location = new Point(x2 + lblW, y);
            txtCosoKham.Size = new Size(250, 20);
            entityBindingProvider1.SetBindingMember(txtCosoKham, "CosoKham");

            y += dy;
            lblLydoKsk.Text = "Lý do KSK:";
            lblLydoKsk.Location = new Point(x1, y);
            txtLydoKsk.Location = new Point(x1 + lblW, y);
            txtLydoKsk.Size = new Size(300, 20);
            entityBindingProvider1.SetBindingMember(txtLydoKsk, "LydoKsk");

            y += dy;
            lblNhietdo.Text = "Nhiệt độ:";
            lblNhietdo.Location = new Point(x1, y);
            spNhietdo.Location = new Point(x1 + lblW, y);
            spNhietdo.Size = new Size(80, 20);
            spNhietdo.Properties.IsFloatValue = true;
            spNhietdo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            spNhietdo.Properties.DisplayFormat.FormatString = "#,##0.0";
            entityBindingProvider1.SetBindingMember(spNhietdo, "Nhietdo");
            lblMach.Text = "Mạch:";
            lblMach.Location = new Point(x2, y);
            spMach.Location = new Point(x2 + lblW, y);
            spMach.Size = new Size(80, 20);
            spMach.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spMach, "Mach");
            lblNhiptho.Text = "Nhịp thở:";
            lblNhiptho.Location = new Point(x2 + 200, y);
            spNhiptho.Location = new Point(x2 + 200 + lblW, y);
            spNhiptho.Size = new Size(80, 20);
            spNhiptho.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spNhiptho, "Nhiptho");

            y += dy;
            lblChieucao.Text = "Chiều cao:";
            lblChieucao.Location = new Point(x1, y);
            spChieucao.Location = new Point(x1 + lblW, y);
            spChieucao.Size = new Size(80, 20);
            spChieucao.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spChieucao, "Chieucao");
            lblCannang.Text = "Cân nặng:";
            lblCannang.Location = new Point(x2, y);
            spCannang.Location = new Point(x2 + lblW, y);
            spCannang.Size = new Size(80, 20);
            spCannang.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spCannang, "Cannang");
            lblHuyetap.Text = "Huyết áp:";
            lblHuyetap.Location = new Point(x2 + 200, y);
            txtHuyetap.Location = new Point(x2 + 200 + lblW, y);
            txtHuyetap.Size = new Size(80, 20);
            entityBindingProvider1.SetBindingMember(txtHuyetap, "Huyetap");

            y += dy;
            lblHotenQh.Text = "Người GH:";
            lblHotenQh.Location = new Point(x1, y);
            txtHotenQh.Location = new Point(x1 + lblW, y);
            txtHotenQh.Size = new Size(200, 20);
            entityBindingProvider1.SetBindingMember(txtHotenQh, "HotenQh");
            lblLoaiqh.Text = "Loại QH:";
            lblLoaiqh.Location = new Point(x2, y);
            comboLoaiqh.Location = new Point(x2 + lblW, y);
            comboLoaiqh.Size = new Size(160, 20);
            entityBindingProvider1.SetBindingMember(comboLoaiqh, "Loaiqh");

            y += dy;
            lblDienthoaiQh.Text = "SĐT QH:";
            lblDienthoaiQh.Location = new Point(x1, y);
            txtDienthoaiQh.Location = new Point(x1 + lblW, y);
            txtDienthoaiQh.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(txtDienthoaiQh, "DienthoaiQh");
            lblCmndQh.Text = "CCCD QH:";
            lblCmndQh.Location = new Point(x2, y);
            txtCmndQh.Location = new Point(x2 + lblW, y);
            txtCmndQh.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(txtCmndQh, "CmndQh");

            y += dy;
            lblDangDtbenh.Text = "Đang DT bệnh:";
            lblDangDtbenh.Location = new Point(x1, y);
            txtDangDtbenh.Location = new Point(x1 + lblW, y);
            txtDangDtbenh.Size = new Size(300, 20);
            entityBindingProvider1.SetBindingMember(txtDangDtbenh, "DangDtbenh");

            y += dy;
            chkTsbenhCoBenhbamsinh.Text = "TS bẩm sinh";
            chkTsbenhCoBenhbamsinh.Location = new Point(x1, y);
            chkTsbenhCoBenhbamsinh.Size = new Size(100, 20);
            entityBindingProvider1.SetBindingMember(chkTsbenhCoBenhbamsinh, "TsbenhCoBenhbamsinh");
            txtTsbenhTenbenh.Location = new Point(x1 + 110, y);
            txtTsbenhTenbenh.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtTsbenhTenbenh, "TsbenhTenbenh");
            chkTsgdCoBenhTruyennhiem.Text = "TS truyền nhiễm GD";
            chkTsgdCoBenhTruyennhiem.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkTsgdCoBenhTruyennhiem, "TsgdCoBenhTruyennhiem");
            txtTsgdTenbenhTruyennhiem.Location = new Point(x2 + 160, y);
            txtTsgdTenbenhTruyennhiem.Size = new Size(200, 20);
            entityBindingProvider1.SetBindingMember(txtTsgdTenbenhTruyennhiem, "TsgdTenbenhTruyennhiem");

            y += dy;
            lblTuanhoanKsk.Text = "Tuần hoàn:";
            lblTuanhoanKsk.Location = new Point(x1, y);
            txtTuanhoanKsk.Location = new Point(x1 + lblW, y);
            txtTuanhoanKsk.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtTuanhoanKsk, "TuanhoanKsk");
            lblHohapKsk.Text = "Hô hấp:";
            lblHohapKsk.Location = new Point(x2, y);
            txtHohapKsk.Location = new Point(x2 + lblW, y);
            txtHohapKsk.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtHohapKsk, "HohapKsk");

            y += dy;
            lblTieuhoaKsk.Text = "Tiêu hóa:";
            lblTieuhoaKsk.Location = new Point(x1, y);
            txtTieuhoaKsk.Location = new Point(x1 + lblW, y);
            txtTieuhoaKsk.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtTieuhoaKsk, "TieuhoaKsk");
            lblThanKsk.Text = "Thận-TN-SD:";
            lblThanKsk.Location = new Point(x2, y);
            txtThanKsk.Location = new Point(x2 + lblW, y);
            txtThanKsk.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtThanKsk, "ThanKsk");

            y += dy;
            lblThankinhKsk.Text = "Thần kinh:";
            lblThankinhKsk.Location = new Point(x1, y);
            txtThankinhKsk.Location = new Point(x1 + lblW, y);
            txtThankinhKsk.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtThankinhKsk, "ThankinhKsk");
            lblTamthanKsk.Text = "Tâm thần:";
            lblTamthanKsk.Location = new Point(x2, y);
            txtTamthanKsk.Location = new Point(x2 + lblW, y);
            txtTamthanKsk.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtTamthanKsk, "TamthanKsk");

            y += dy;
            lblKhamlsKhacKsk.Text = "LS khác:";
            lblKhamlsKhacKsk.Location = new Point(x1, y);
            txtKhamlsKhacKsk.Location = new Point(x1 + lblW, y);
            txtKhamlsKhacKsk.Size = new Size(300, 20);
            entityBindingProvider1.SetBindingMember(txtKhamlsKhacKsk, "KhamlsKhacKsk");
            lblKqclsKsk.Text = "KQ CLS:";
            lblKqclsKsk.Location = new Point(x2, y);
            txtKqclsKsk.Location = new Point(x2 + lblW, y);
            txtKqclsKsk.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtKqclsKsk, "KqclsKsk");

            y += dy;
            lblKmatphai.Text = "K kính (P):";
            lblKmatphai.Location = new Point(x1, y);
            spKmatphai.Location = new Point(x1 + lblW, y);
            spKmatphai.Size = new Size(60, 20);
            spKmatphai.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spKmatphai, "KmatphaiKsk");
            lblKmattrai.Text = "K kính (T):";
            lblKmattrai.Location = new Point(x1 + 200, y);
            spKmattrai.Location = new Point(x1 + 200 + lblW, y);
            spKmattrai.Size = new Size(60, 20);
            spKmattrai.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spKmattrai, "KmattraiKsk");
            lblMatphai.Text = "C kính (P):";
            lblMatphai.Location = new Point(x2, y);
            spMatphai.Location = new Point(x2 + lblW, y);
            spMatphai.Size = new Size(60, 20);
            spMatphai.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spMatphai, "MatphaiKsk");
            lblMattrai.Text = "C kính (T):";
            lblMattrai.Location = new Point(x2 + 200, y);
            spMattrai.Location = new Point(x2 + 200 + lblW, y);
            spMattrai.Size = new Size(60, 20);
            spMattrai.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spMattrai, "MattraiKsk");

            y += dy;
            lblHamtren.Text = "Hàm trên:";
            lblHamtren.Location = new Point(x1, y);
            txtHamtren.Location = new Point(x1 + lblW, y);
            txtHamtren.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(txtHamtren, "HamtrenKsk");
            lblHamduoi.Text = "Hàm dưới:";
            lblHamduoi.Location = new Point(x2, y);
            txtHamduoi.Location = new Point(x2 + lblW, y);
            txtHamduoi.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(txtHamduoi, "HamduoiKsk");
            chkRhmbenh.Text = "Bệnh RHM";
            chkRhmbenh.Location = new Point(x2 + 250, y);
            entityBindingProvider1.SetBindingMember(chkRhmbenh, "RhmbenhKsk");

            y += dy;
            lblNgoaikhoa.Text = "Ngoại khoa:";
            lblNgoaikhoa.Location = new Point(x1, y);
            txtNgoaikhoa.Location = new Point(x1 + lblW, y);
            txtNgoaikhoa.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtNgoaikhoa, "NgoaikhoaKsk");
            lblPlngoaikhoa.Text = "PL NK:";
            lblPlngoaikhoa.Location = new Point(x2, y);
            txtPlngoaikhoa.Location = new Point(x2 + lblW, y);
            txtPlngoaikhoa.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtPlngoaikhoa, "PlngoaikhoaKsk");

            y += dy;
            lblDalieu.Text = "Da liễu:";
            lblDalieu.Location = new Point(x1, y);
            txtDalieu.Location = new Point(x1 + lblW, y);
            txtDalieu.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtDalieu, "DalieuKsk");
            lblPldalieu.Text = "PL DL:";
            lblPldalieu.Location = new Point(x2, y);
            txtPldalieu.Location = new Point(x2 + lblW, y);
            txtPldalieu.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtPldalieu, "PldalieuKsk");

            y += dy;
            lblSanphukhoa.Text = "Sản phụ khoa:";
            lblSanphukhoa.Location = new Point(x1, y);
            txtSanphukhoa.Location = new Point(x1 + lblW, y);
            txtSanphukhoa.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtSanphukhoa, "SanphukhoaKsk");
            lblPlsanphukhoa.Text = "PL SPK:";
            lblPlsanphukhoa.Location = new Point(x2, y);
            txtPlsanphukhoa.Location = new Point(x2 + lblW, y);
            txtPlsanphukhoa.Size = new Size(220, 20);
            entityBindingProvider1.SetBindingMember(txtPlsanphukhoa, "PlsanphukhoaKsk");

        }

        // ===========================================================
        //  TAB TRE EM <6t
        // ===========================================================
        private void BuildTreEmTab()
        {
            int y = 20, x1 = 20, x2 = 420, lblW = 120, dy = 28;

            chkTsTiepxucLao.Text = "TS tiếp xúc lao";
            chkTsTiepxucLao.Location = new Point(x1, y);
            chkTsTiepxucLao.Size = new Size(130, 20);
            entityBindingProvider1.SetBindingMember(chkTsTiepxucLao, "TsTiepxucLao");
            lblChieudaiTuoiSd.Text = "CD/tuổi (SD):";
            lblChieudaiTuoiSd.Location = new Point(x2, y);
            spChieudaiTuoiSd.Location = new Point(x2 + lblW, y);
            spChieudaiTuoiSd.Size = new Size(80, 20);
            spChieudaiTuoiSd.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spChieudaiTuoiSd, "ChieudaiTuoiSd");

            y += dy;
            lblCannangTuoiSd.Text = "CN/tuổi (SD):";
            lblCannangTuoiSd.Location = new Point(x1, y);
            spCannangTuoiSd.Location = new Point(x1 + lblW, y);
            spCannangTuoiSd.Size = new Size(80, 20);
            spCannangTuoiSd.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spCannangTuoiSd, "CannangTuoiSd");
            lblTrangthaiVongdau.Text = "Vòng đầu:";
            lblTrangthaiVongdau.Location = new Point(x2, y);
            comboTrangthaiVongdau.Location = new Point(x2 + lblW, y);
            comboTrangthaiVongdau.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboTrangthaiVongdau, "TrangthaiVongdau");

            y += dy;
            lblChuviVongcanhtay.Text = "CV cánh tay:";
            lblChuviVongcanhtay.Location = new Point(x1, y);
            spChuviVongcanhtay.Location = new Point(x1 + lblW, y);
            spChuviVongcanhtay.Size = new Size(80, 20);
            spChuviVongcanhtay.Properties.IsFloatValue = true;
            entityBindingProvider1.SetBindingMember(spChuviVongcanhtay, "ChuviVongcanhtay");
            lblTinhtrangDinhduong.Text = "Dinh dưỡng:";
            lblTinhtrangDinhduong.Location = new Point(x2, y);
            txtTinhtrangDinhduong.Location = new Point(x2 + lblW, y);
            txtTinhtrangDinhduong.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(txtTinhtrangDinhduong, "TinhtrangDinhduong");

            y += dy;
            chkPhattrienTinhthan.Text = "PT tinh thần";
            chkPhattrienTinhthan.Location = new Point(x1, y);
            entityBindingProvider1.SetBindingMember(chkPhattrienTinhthan, "PhattrienTinhthan");
            chkPhattrienVandong.Text = "PT vận động";
            chkPhattrienVandong.Location = new Point(x1 + 150, y);
            entityBindingProvider1.SetBindingMember(chkPhattrienVandong, "PhattrienVandong");
            chkNguycoTuky.Text = "Nguy cơ tự kỷ";
            chkNguycoTuky.Location = new Point(x1 + 300, y);
            entityBindingProvider1.SetBindingMember(chkNguycoTuky, "NguycoTuky");

            y += dy;
            chkBenhLao.Text = "Lao";
            chkBenhLao.Location = new Point(x1, y);
            entityBindingProvider1.SetBindingMember(chkBenhLao, "BenhLao");
            chkTiemVgbMui1.Text = "Tiêm VGB mũi 1";
            chkTiemVgbMui1.Location = new Point(x1 + 150, y);
            entityBindingProvider1.SetBindingMember(chkTiemVgbMui1, "TiemVgbMui1");
            chkTiemchungDaydu.Text = "Tiêm chủng đầy đủ";
            chkTiemchungDaydu.Location = new Point(x1 + 300, y);
            entityBindingProvider1.SetBindingMember(chkTiemchungDaydu, "TiemchungDaydu");

            y += dy;
            lblMausacDa.Text = "Màu sắc da:";
            lblMausacDa.Location = new Point(x1, y);
            comboMausacDa.Location = new Point(x1 + lblW, y);
            comboMausacDa.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboMausacDa, "MausacDa");
            chkLongBantay.Text = "Lòng bàn tay BT";
            chkLongBantay.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkLongBantay, "LongBantay");

            y += dy;
            lblThop.Text = "Thóp:";
            lblThop.Location = new Point(x1, y);
            comboThop.Location = new Point(x1 + lblW, y);
            comboThop.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboThop, "Thop");
            lblHinhdangDau.Text = "Hình dáng đầu:";
            lblHinhdangDau.Location = new Point(x2, y);
            comboHinhdangDau.Location = new Point(x2 + lblW, y);
            comboHinhdangDau.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboHinhdangDau, "HinhdangDau");

            y += dy;
            lblVandongCo.Text = "Vận động cổ:";
            lblVandongCo.Location = new Point(x1, y);
            comboVandongCo.Location = new Point(x1 + lblW, y);
            comboVandongCo.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboVandongCo, "VandongCo");
            chkKhoibatthuongDauco.Text = "Khối BT đầu cổ";
            chkKhoibatthuongDauco.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkKhoibatthuongDauco, "KhoibatthuongDauco");

            y += dy;
            lblVitriHaimat.Text = "Vị trí 2 mắt:";
            lblVitriHaimat.Location = new Point(x1, y);
            comboVitriHaimat.Location = new Point(x1 + lblW, y);
            comboVitriHaimat.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboVitriHaimat, "VitriHaimat");
            lblMimatKetmac.Text = "Mí mắt-Kết mạc:";
            lblMimatKetmac.Location = new Point(x2, y);
            comboMimatKetmac.Location = new Point(x2 + lblW, y);
            comboMimatKetmac.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboMimatKetmac, "MimatKetmac");

            y += dy;
            lblDongtu.Text = "Đồng tử:";
            lblDongtu.Location = new Point(x1, y);
            comboDongtu.Location = new Point(x1 + lblW, y);
            comboDongtu.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboDongtu, "Dongtu");
            chkLacmat.Text = "Lác mắt";
            chkLacmat.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkLacmat, "Lacmat");

            y += dy;
            lblTaiMangnhi.Text = "Tai-màng nhĩ:";
            lblTaiMangnhi.Location = new Point(x1, y);
            comboTaiMangnhi.Location = new Point(x1 + lblW, y);
            comboTaiMangnhi.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboTaiMangnhi, "TaiMangnhi");
            lblDapungAmthanh.Text = "Đáp ứng AT:";
            lblDapungAmthanh.Location = new Point(x2, y);
            comboDapungAmthanh.Location = new Point(x2 + lblW, y);
            comboDapungAmthanh.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboDapungAmthanh, "DapungAmthanh");

            y += dy;
            chkKhoisungSautai.Text = "Khối sưng sau tai";
            chkKhoisungSautai.Location = new Point(x1, y);
            entityBindingProvider1.SetBindingMember(chkKhoisungSautai, "KhoisungSautai");
            chkChaymuNuoctai.Text = "Chảy mủ/nước tai";
            chkChaymuNuoctai.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkChaymuNuoctai, "ChaymuNuoctai");

            y += dy;
            lblHinhdangMui.Text = "Hình dạng mũi:";
            lblHinhdangMui.Location = new Point(x1, y);
            comboHinhdangMui.Location = new Point(x1 + lblW, y);
            comboHinhdangMui.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboHinhdangMui, "HinhdangMui");
            chkChaynuocMui.Text = "Chảy nước mũi";
            chkChaynuocMui.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkChaynuocMui, "ChaynuocMui");
            chkNghetMui.Text = "Nghẹt mũi";
            chkNghetMui.Location = new Point(x2 + 150, y);
            entityBindingProvider1.SetBindingMember(chkNghetMui, "NghetMui");

            y += dy;
            lblHong.Text = "Họng:";
            lblHong.Location = new Point(x1, y);
            comboHong.Location = new Point(x1 + lblW, y);
            comboHong.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboHong, "Hong");
            lblHinhdangMieng.Text = "Hình dạng miệng:";
            lblHinhdangMieng.Location = new Point(x2, y);
            comboHinhdangMieng.Location = new Point(x2 + lblW, y);
            comboHinhdangMieng.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboHinhdangMieng, "HinhdangMieng");

            y += dy;
            chkRangsuaSosinh.Text = "Răng sữa sơ sinh";
            chkRangsuaSosinh.Location = new Point(x1, y);
            entityBindingProvider1.SetBindingMember(chkRangsuaSosinh, "RangsuaSosinh");
            lblHinhdangLuoi.Text = "Hình dạng lưỡi:";
            lblHinhdangLuoi.Location = new Point(x2, y);
            comboHinhdangLuoi.Location = new Point(x2 + lblW, y);
            comboHinhdangLuoi.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboHinhdangLuoi, "HinhdangLuoi");

            y += dy;
            chkDinhThangluoi.Text = "Dính thắng lưỡi";
            chkDinhThangluoi.Location = new Point(x1, y);
            entityBindingProvider1.SetBindingMember(chkDinhThangluoi, "DinhThangluoi");
            chkNamMieng.Text = "Nấm miệng";
            chkNamMieng.Location = new Point(x1 + 150, y);
            entityBindingProvider1.SetBindingMember(chkNamMieng, "NamMieng");
            chkCamTut.Text = "Cằm nhỏ tụt";
            chkCamTut.Location = new Point(x1 + 300, y);
            entityBindingProvider1.SetBindingMember(chkCamTut, "CamTut");
            chkVetsauRang.Text = "Vết sâu răng";
            chkVetsauRang.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkVetsauRang, "VetsauRang");

            y += dy;
            lblNhipthoKhongdeu.Text = "NT không đều:";
            lblNhipthoKhongdeu.Location = new Point(x1, y);
            comboNhipthoKhongdeu.Location = new Point(x1 + lblW, y);
            comboNhipthoKhongdeu.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboNhipthoKhongdeu, "NhipthoKhongdeu");
            chkThorutlomLongnguc.Text = "Thở rút lõm ngực";
            chkThorutlomLongnguc.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkThorutlomLongnguc, "ThorutlomLongnguc");

            y += dy;
            chkTiengthoBatthuong.Text = "Tiếng thở BT";
            chkTiengthoBatthuong.Location = new Point(x1, y);
            entityBindingProvider1.SetBindingMember(chkTiengthoBatthuong, "TiengthoBatthuong");
            chkDauhieuSuyhohap.Text = "Suy hô hấp";
            chkDauhieuSuyhohap.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkDauhieuSuyhohap, "DauhieuSuyhohap");
            lblNghePhoi.Text = "Nghe phổi:";
            lblNghePhoi.Location = new Point(x2 + 200, y);
            comboNghePhoi.Location = new Point(x2 + 200 + lblW, y);
            comboNghePhoi.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboNghePhoi, "NghePhoi");

            y += dy;
            lblVitriMomtim.Text = "Mỏm tim:";
            lblVitriMomtim.Location = new Point(x1, y);
            comboVitriMomtim.Location = new Point(x1 + lblW, y);
            comboVitriMomtim.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboVitriMomtim, "VitriMomtim");
            lblMachNgoaivi.Text = "Mạch ngoại vi:";
            lblMachNgoaivi.Location = new Point(x2, y);
            comboMachNgoaivi.Location = new Point(x2 + lblW, y);
            comboMachNgoaivi.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboMachNgoaivi, "MachNgoaivi");
            chkNgheTim.Text = "Nghe tim";
            chkNgheTim.Location = new Point(x2 + 280, y);
            entityBindingProvider1.SetBindingMember(chkNgheTim, "NgheTim");

            y += dy;
            lblHinhdangBungRon.Text = "Bung-rốn:";
            lblHinhdangBungRon.Location = new Point(x1, y);
            comboHinhdangBungRon.Location = new Point(x1 + lblW, y);
            comboHinhdangBungRon.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboHinhdangBungRon, "HinhdangBungRon");
            chkGanLachTo.Text = "Gan lách to";
            chkGanLachTo.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkGanLachTo, "GanLachTo");
            chkKhoibatthuongBung.Text = "Khối BT bụng";
            chkKhoibatthuongBung.Location = new Point(x2 + 150, y);
            entityBindingProvider1.SetBindingMember(chkKhoibatthuongBung, "KhoibatthuongBung");

            y += dy;
            lblLoHaumon.Text = "Lỗ hậu môn:";
            lblLoHaumon.Location = new Point(x1, y);
            comboLoHaumon.Location = new Point(x1 + lblW, y);
            comboLoHaumon.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboLoHaumon, "LoHaumon");
            lblCqsdNgoai.Text = "CQSD ngoài:";
            lblCqsdNgoai.Location = new Point(x2, y);
            comboCqsdNgoai.Location = new Point(x2 + lblW, y);
            comboCqsdNgoai.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboCqsdNgoai, "CqsdNgoai");

            y += dy;
            chkVandongKhongdoixung.Text = "VD không đối xứng";
            chkVandongKhongdoixung.Location = new Point(x1, y);
            entityBindingProvider1.SetBindingMember(chkVandongKhongdoixung, "VandongKhongdoixung");
            chkPhanxaBu.Text = "Phản xạ bú";
            chkPhanxaBu.Location = new Point(x1 + 180, y);
            entityBindingProvider1.SetBindingMember(chkPhanxaBu, "PhanxaBu");
            chkPhanxaNam.Text = "Phản xạ nắm";
            chkPhanxaNam.Location = new Point(x1 + 300, y);
            entityBindingProvider1.SetBindingMember(chkPhanxaNam, "PhanxaNam");
            chkPhanxaMoro.Text = "Phản xạ Moro";
            chkPhanxaMoro.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkPhanxaMoro, "PhanxaMoro");

            y += dy;
            lblTruonglucCo.Text = "Trương lực cơ:";
            lblTruonglucCo.Location = new Point(x1, y);
            comboTruonglucCo.Location = new Point(x1 + lblW, y);
            comboTruonglucCo.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboTruonglucCo, "TruonglucCo");
            lblKhopHang.Text = "Khớp háng:";
            lblKhopHang.Location = new Point(x2, y);
            comboKhopHang.Location = new Point(x2 + lblW, y);
            comboKhopHang.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboKhopHang, "KhopHang");

            y += dy;
            lblPhanxaCo.Text = "Phản xạ cơ:";
            lblPhanxaCo.Location = new Point(x1, y);
            comboPhanxaCo.Location = new Point(x1 + lblW, y);
            comboPhanxaCo.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboPhanxaCo, "PhanxaCo");
            lblLungCotsong.Text = "Lưng-Cột sống:";
            lblLungCotsong.Location = new Point(x2, y);
            comboLungCotsong.Location = new Point(x2 + lblW, y);
            comboLungCotsong.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboLungCotsong, "LungCotsong");

            y += dy;
            lblTuchiKhop.Text = "Tứ chi-khớp:";
            lblTuchiKhop.Location = new Point(x1, y);
            comboTuchiKhop.Location = new Point(x1 + lblW, y);
            comboTuchiKhop.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboTuchiKhop, "TuchiKhop");
            lblDangDi.Text = "Dáng đi:";
            lblDangDi.Location = new Point(x2, y);
            comboDangDi.Location = new Point(x2 + lblW, y);
            comboDangDi.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboDangDi, "DangDi");

            y += dy;
            lblDoituong.Text = "Đối tượng:";
            lblDoituong.Location = new Point(x1, y);
            txtDoituong.Location = new Point(x1 + lblW, y);
            txtDoituong.Size = new Size(200, 20);
            entityBindingProvider1.SetBindingMember(txtDoituong, "Doituong");
            chkSinhNon.Text = "Sinh non";
            chkSinhNon.Location = new Point(x2, y);
            entityBindingProvider1.SetBindingMember(chkSinhNon, "SinhNon");

            y += dy;
            lblTuanthaiKhiSinh.Text = "Tuần thai KS:";
            lblTuanthaiKhiSinh.Location = new Point(x1, y);
            txtTuanthaiKhiSinh.Location = new Point(x1 + lblW, y);
            txtTuanthaiKhiSinh.Size = new Size(100, 20);
            entityBindingProvider1.SetBindingMember(txtTuanthaiKhiSinh, "TuanthaiKhiSinh");
            lblNguonChitra.Text = "Nguồn chi trả:";
            lblNguonChitra.Location = new Point(x2, y);
            txtNguonChitra.Location = new Point(x2 + lblW, y);
            txtNguonChitra.Size = new Size(200, 20);
            entityBindingProvider1.SetBindingMember(txtNguonChitra, "NguonChitra");

            y += dy;
            lblTrangthaiNhietdo.Text = "TT nhiệt độ:";
            lblTrangthaiNhietdo.Location = new Point(x1, y);
            comboTrangthaiNhietdo.Location = new Point(x1 + lblW, y);
            comboTrangthaiNhietdo.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboTrangthaiNhietdo, "TrangthaiNhietdo");
            lblTrangthaiMach.Text = "TT mạch:";
            lblTrangthaiMach.Location = new Point(x2, y);
            comboTrangthaiMach.Location = new Point(x2 + lblW, y);
            comboTrangthaiMach.Size = new Size(120, 20);
            entityBindingProvider1.SetBindingMember(comboTrangthaiMach, "TrangthaiMach");

            y += dy;
            lblTrangthaiNhiptho.Text = "TT nhịp thở:";
            lblTrangthaiNhiptho.Location = new Point(x1, y);
            comboTrangthaiNhiptho.Location = new Point(x1 + lblW, y);
            comboTrangthaiNhiptho.Size = new Size(150, 20);
            entityBindingProvider1.SetBindingMember(comboTrangthaiNhiptho, "TrangthaiNhiptho");

            y += dy;
            lblKetluanBinhthuong.Text = "KL: Bình thường:";
            lblKetluanBinhthuong.Location = new Point(x1, y);
            memKetluanBinhthuong.Location = new Point(x1 + lblW, y);
            memKetluanBinhthuong.Size = new Size(300, 40);
            entityBindingProvider1.SetBindingMember(memKetluanBinhthuong, "KetluanBinhthuong");

            y += 48;
            lblKetluanNguycoLao.Text = "KL: Nguy cơ Lao:";
            lblKetluanNguycoLao.Location = new Point(x1, y);
            memKetluanNguycoLao.Location = new Point(x1 + lblW, y);
            memKetluanNguycoLao.Size = new Size(300, 40);
            entityBindingProvider1.SetBindingMember(memKetluanNguycoLao, "KetluanNguycoLao");

            y += 48;
            lblKetluanVandesuckhoe.Text = "KL: Vấn đề SK:";
            lblKetluanVandesuckhoe.Location = new Point(x1, y);
            memKetluanVandesuckhoe.Location = new Point(x1 + lblW, y);
            memKetluanVandesuckhoe.Size = new Size(300, 40);
            entityBindingProvider1.SetBindingMember(memKetluanVandesuckhoe, "KetluanVandesuckhoe");

            y += 48;
            lblKetluanGhiro.Text = "KL: Ghi rõ:";
            lblKetluanGhiro.Location = new Point(x1, y);
            memKetluanGhiro.Location = new Point(x1 + lblW, y);
            memKetluanGhiro.Size = new Size(300, 40);
            entityBindingProvider1.SetBindingMember(memKetluanGhiro, "KetluanGhiro");

        }

        // ===========================================================
        //  TAB 6-18t
        // ===========================================================
        private void Build6_18Tab()
        {
            int y = 20, lblW = 160, dy = 28;
            lblMaIcdTsgdBamsinh.Text = "ICD-10 TS GD bẩm sinh:";
            lblMaIcdTsgdBamsinh.Location = new Point(20, y);
            txtMaIcdTsgdBamsinh.Location = new Point(20 + lblW, y);
            txtMaIcdTsgdBamsinh.Size = new Size(250, 20);
            entityBindingProvider1.SetBindingMember(txtMaIcdTsgdBamsinh, "MaIcdTsgdBamsinh");

            y += dy;
            lblMaIcdTsBamsinh.Text = "ICD-10 TS bẩm sinh:";
            lblMaIcdTsBamsinh.Location = new Point(20, y);
            txtMaIcdTsBamsinh.Location = new Point(20 + lblW, y);
            txtMaIcdTsBamsinh.Size = new Size(250, 20);
            entityBindingProvider1.SetBindingMember(txtMaIcdTsBamsinh, "MaIcdTsBamsinh");

            y += dy;
            chkSanKhoa.Text = "Sản khoa";
            chkSanKhoa.Location = new Point(20, y);
            entityBindingProvider1.SetBindingMember(chkSanKhoa, "SanKhoa");
            lblSanKhoaKhongBt.Text = "SK không BT:";
            lblSanKhoaKhongBt.Location = new Point(180, y);
            comboSanKhoaKhongBt.Location = new Point(280, y);
            comboSanKhoaKhongBt.Size = new Size(200, 20);
            entityBindingProvider1.SetBindingMember(comboSanKhoaKhongBt, "SanKhoaKhongBt");

            y += dy;
            lblMaBenhSanKhoaKhongBt.Text = "ICD-10 bệnh SK không BT:";
            lblMaBenhSanKhoaKhongBt.Location = new Point(20, y);
            txtMaBenhSanKhoaKhongBt.Location = new Point(180, y);
            txtMaBenhSanKhoaKhongBt.Size = new Size(250, 20);
            entityBindingProvider1.SetBindingMember(txtMaBenhSanKhoaKhongBt, "MaBenhSanKhoaKhongBt");
        }

        // ===========================================================
        //  TAB >=18t
        // ===========================================================
        private void Build18Tab()
        {
            int y = 20, x1 = 20, x2 = 250, x3 = 480, dy = 28;

            chkBenh5nam.Text = "Bệnh 5 năm"; chkBenh5nam.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkBenh5nam, "Benh5nam");
            chkBenhThankinh.Text = "Thần kinh"; chkBenhThankinh.Location = new Point(x2, y); entityBindingProvider1.SetBindingMember(chkBenhThankinh, "BenhThankinh");
            chkBenhMat.Text = "Mắt"; chkBenhMat.Location = new Point(x3, y); entityBindingProvider1.SetBindingMember(chkBenhMat, "BenhMat");
            y += dy;
            chkBenhTai.Text = "Tai"; chkBenhTai.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkBenhTai, "BenhTai");
            chkBenhTim.Text = "Tim"; chkBenhTim.Location = new Point(x2, y); entityBindingProvider1.SetBindingMember(chkBenhTim, "BenhTim");
            chkPhauthuatTimmach.Text = "PT tim mạch"; chkPhauthuatTimmach.Location = new Point(x3, y); entityBindingProvider1.SetBindingMember(chkPhauthuatTimmach, "PhauthuatTimmach");
            y += dy;
            chkTangHuyetap.Text = "Tăng HA"; chkTangHuyetap.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkTangHuyetap, "TangHuyetap");
            chkKhoTho.Text = "Khó thở"; chkKhoTho.Location = new Point(x2, y); entityBindingProvider1.SetBindingMember(chkKhoTho, "KhoTho");
            chkBenhPhoi.Text = "Bệnh phổi"; chkBenhPhoi.Location = new Point(x3, y); entityBindingProvider1.SetBindingMember(chkBenhPhoi, "BenhPhoi");
            y += dy;
            chkBenhThanLocmau.Text = "Thận lọc máu"; chkBenhThanLocmau.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkBenhThanLocmau, "BenhThanLocmau");
            chkNghienRuoubia.Text = "Nghiện rượu bia"; chkNghienRuoubia.Location = new Point(x2, y); entityBindingProvider1.SetBindingMember(chkNghienRuoubia, "NghienRuoubia");
            chkDaiThaoduong.Text = "Đái tháo đường"; chkDaiThaoduong.Location = new Point(x3, y); entityBindingProvider1.SetBindingMember(chkDaiThaoduong, "DaiThaoduong");
            y += dy;
            chkBenhTamthan.Text = "Bệnh tâm thần"; chkBenhTamthan.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkBenhTamthan, "BenhTamthan");
            chkRoiLoanYthuc.Text = "RL ý thức"; chkRoiLoanYthuc.Location = new Point(x2, y); entityBindingProvider1.SetBindingMember(chkRoiLoanYthuc, "RoiLoanYthuc");
            chkNgatChongmat.Text = "Ngất chóng mặt"; chkNgatChongmat.Location = new Point(x3, y); entityBindingProvider1.SetBindingMember(chkNgatChongmat, "NgatChongmat");
            y += dy;
            chkBenhTieuhoa.Text = "Bệnh tiêu hóa"; chkBenhTieuhoa.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkBenhTieuhoa, "BenhTieuhoa");
            chkRoiLoanGiacngu.Text = "RL giấc ngủ"; chkRoiLoanGiacngu.Location = new Point(x2, y); entityBindingProvider1.SetBindingMember(chkRoiLoanGiacngu, "RoiLoanGiacngu");
            chkTaibienLiet.Text = "Tai biến liệt"; chkTaibienLiet.Location = new Point(x3, y); entityBindingProvider1.SetBindingMember(chkTaibienLiet, "TaibienLiet");
            y += dy;
            chkBenhCotsong.Text = "Bệnh cột sống"; chkBenhCotsong.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkBenhCotsong, "BenhCotsong");
            chkSudungRuou.Text = "Dùng rượu"; chkSudungRuou.Location = new Point(x2, y); entityBindingProvider1.SetBindingMember(chkSudungRuou, "SudungRuou");
            chkSudungMatuy.Text = "Dùng ma túy"; chkSudungMatuy.Location = new Point(x3, y); entityBindingProvider1.SetBindingMember(chkSudungMatuy, "SudungMatuy");
            y += dy;
            chkBenhKhac.Text = "Bệnh khác"; chkBenhKhac.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkBenhKhac, "BenhKhac");
            lblTenBenhKhac.Text = "Tên bệnh khác:"; lblTenBenhKhac.Location = new Point(x2, y);
            txtTenBenhKhac.Location = new Point(x2 + 110, y); txtTenBenhKhac.Size = new Size(250, 20); entityBindingProvider1.SetBindingMember(txtTenBenhKhac, "TenBenhKhac");
            y += dy + 10;

            lblClsHuyethoc.Text = "Huyết học:"; lblClsHuyethoc.Location = new Point(x1, y);
            txtClsHuyethoc.Location = new Point(x1 + 100, y); txtClsHuyethoc.Size = new Size(180, 20); entityBindingProvider1.SetBindingMember(txtClsHuyethoc, "ClsHuyethoc");
            lblClsDuongmau.Text = "Đường máu:"; lblClsDuongmau.Location = new Point(x3, y);
            txtClsDuongmau.Location = new Point(x3 + 100, y); txtClsDuongmau.Size = new Size(180, 20); entityBindingProvider1.SetBindingMember(txtClsDuongmau, "ClsDuongmau");
            y += dy;
            lblClsUre.Text = "Urê:"; lblClsUre.Location = new Point(x1, y);
            txtClsUre.Location = new Point(x1 + 100, y); txtClsUre.Size = new Size(180, 20); entityBindingProvider1.SetBindingMember(txtClsUre, "ClsUre");
            lblClsCreatinin.Text = "Creatinin:"; lblClsCreatinin.Location = new Point(x3, y);
            txtClsCreatinin.Location = new Point(x3 + 100, y); txtClsCreatinin.Size = new Size(180, 20); entityBindingProvider1.SetBindingMember(txtClsCreatinin, "ClsCreatinin");
            y += dy;
            lblClsAsat.Text = "ASAT (GOT):"; lblClsAsat.Location = new Point(x1, y);
            txtClsAsat.Location = new Point(x1 + 100, y); txtClsAsat.Size = new Size(180, 20); entityBindingProvider1.SetBindingMember(txtClsAsat, "ClsAsat");
            lblClsAlat.Text = "ALAT (GPT):"; lblClsAlat.Location = new Point(x3, y);
            txtClsAlat.Location = new Point(x3 + 100, y); txtClsAlat.Size = new Size(180, 20); entityBindingProvider1.SetBindingMember(txtClsAlat, "ClsAlat");
            y += dy;
            lblClsTongphantichNt.Text = "TPNT:"; lblClsTongphantichNt.Location = new Point(x1, y);
            txtClsTongphantichNt.Location = new Point(x1 + 100, y); txtClsTongphantichNt.Size = new Size(180, 20); entityBindingProvider1.SetBindingMember(txtClsTongphantichNt, "ClsTongphantichNt");
            lblClsNtKhac.Text = "NT khác:"; lblClsNtKhac.Location = new Point(x3, y);
            txtClsNtKhac.Location = new Point(x3 + 100, y); txtClsNtKhac.Size = new Size(180, 20); entityBindingProvider1.SetBindingMember(txtClsNtKhac, "ClsNtKhac");
            y += dy;
            lblClsXqTimphoi.Text = "XQ tim phổi:"; lblClsXqTimphoi.Location = new Point(x1, y);
            txtClsXqTimphoi.Location = new Point(x1 + 100, y); txtClsXqTimphoi.Size = new Size(300, 20); entityBindingProvider1.SetBindingMember(txtClsXqTimphoi, "ClsXqTimphoi");
            y += dy;
            chkClsKhac.Text = "Có CLS khác"; chkClsKhac.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkClsKhac, "ClsKhac");
            lblClsKhacNoidung.Text = "ND CLS khác:"; lblClsKhacNoidung.Location = new Point(x2, y);
            txtClsKhacNoidung.Location = new Point(x2 + 110, y); txtClsKhacNoidung.Size = new Size(250, 20); entityBindingProvider1.SetBindingMember(txtClsKhacNoidung, "ClsKhacNoidung");
        }

        // ===========================================================
        //  TAB TAM THAN
        // ===========================================================
        private void BuildTamThanTab()
        {
            int y = 20, x1 = 20, lblW = 160, dy = 28;

            chkTsTamthanGiadinh.Text = "TS tâm thần gia đình"; chkTsTamthanGiadinh.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkTsTamthanGiadinh, "TsTamthanGiadinh");
            lblTenTamthanGiadinh.Text = "Tên:"; lblTenTamthanGiadinh.Location = new Point(x1 + 200, y);
            txtTenTamthanGiadinh.Location = new Point(x1 + 240, y); txtTenTamthanGiadinh.Size = new Size(250, 20); entityBindingProvider1.SetBindingMember(txtTenTamthanGiadinh, "TenTamthanGiadinh");
            y += dy;
            chkTsTamthanBanthan.Text = "TS tâm thần bản thân"; chkTsTamthanBanthan.Location = new Point(x1, y); entityBindingProvider1.SetBindingMember(chkTsTamthanBanthan, "TsTamthanBanthan");
            lblTenTamthanBanthan.Text = "Tên:"; lblTenTamthanBanthan.Location = new Point(x1 + 200, y);
            txtTenTamthanBanthan.Location = new Point(x1 + 240, y); txtTenTamthanBanthan.Size = new Size(250, 20); entityBindingProvider1.SetBindingMember(txtTenTamthanBanthan, "TenTamthanBanthan");
            y += dy + 10;

            lblTamthanBieuhien.Text = "Biểu hiện chung:"; lblTamthanBieuhien.Location = new Point(x1, y);
            memTamthanBieuhien.Location = new Point(x1 + lblW, y); memTamthanBieuhien.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanBieuhien, "TamthanBieuhien");
            lblTamthanYthuc.Text = "Ý thức:"; lblTamthanYthuc.Location = new Point(x1 + 420, y);
            memTamthanYthuc.Location = new Point(x1 + 420 + lblW, y); memTamthanYthuc.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanYthuc, "TamthanYthuc");
            y += 48;
            lblTamthanDinhhuongKhonggian.Text = "DH không gian:"; lblTamthanDinhhuongKhonggian.Location = new Point(x1, y);
            txtTamthanDinhhuongKhonggian.Location = new Point(x1 + lblW, y); txtTamthanDinhhuongKhonggian.Size = new Size(250, 20); entityBindingProvider1.SetBindingMember(txtTamthanDinhhuongKhonggian, "TamthanDinhhuongKhonggian");
            lblTamthanDinhhuongThoigian.Text = "DH thời gian:"; lblTamthanDinhhuongThoigian.Location = new Point(x1 + 420, y);
            txtTamthanDinhhuongThoigian.Location = new Point(x1 + 420 + lblW, y); txtTamthanDinhhuongThoigian.Size = new Size(250, 20); entityBindingProvider1.SetBindingMember(txtTamthanDinhhuongThoigian, "TamthanDinhhuongThoigian");
            y += dy;
            lblTamthanDinhhuongXungquanh.Text = "DH xung quanh:"; lblTamthanDinhhuongXungquanh.Location = new Point(x1, y);
            txtTamthanDinhhuongXungquanh.Location = new Point(x1 + lblW, y); txtTamthanDinhhuongXungquanh.Size = new Size(250, 20); entityBindingProvider1.SetBindingMember(txtTamthanDinhhuongXungquanh, "TamthanDinhhuongXungquanh");
            lblTamthanDinhhuongBanthan.Text = "DH bản thân:"; lblTamthanDinhhuongBanthan.Location = new Point(x1 + 420, y);
            txtTamthanDinhhuongBanthan.Location = new Point(x1 + 420 + lblW, y); txtTamthanDinhhuongBanthan.Size = new Size(250, 20); entityBindingProvider1.SetBindingMember(txtTamthanDinhhuongBanthan, "TamthanDinhhuongBanthan");
            y += dy + 10;
            lblTamthanCamxuc.Text = "Cảm xúc:"; lblTamthanCamxuc.Location = new Point(x1, y);
            memTamthanCamxuc.Location = new Point(x1 + lblW, y); memTamthanCamxuc.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanCamxuc, "TamthanCamxuc");
            lblTamthanCamgiacTrigiac.Text = "Cảm giác-TG:"; lblTamthanCamgiacTrigiac.Location = new Point(x1 + 420, y);
            memTamthanCamgiacTrigiac.Location = new Point(x1 + 420 + lblW, y); memTamthanCamgiacTrigiac.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanCamgiacTrigiac, "TamthanCamgiacTrigiac");
            y += 48;
            lblTamthanTuduyHinhthuc.Text = "TD hình thức:"; lblTamthanTuduyHinhthuc.Location = new Point(x1, y);
            memTamthanTuduyHinhthuc.Location = new Point(x1 + lblW, y); memTamthanTuduyHinhthuc.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanTuduyHinhthuc, "TamthanTuduyHinhthuc");
            lblTamthanTuduyNoidung.Text = "TD nội dung:"; lblTamthanTuduyNoidung.Location = new Point(x1 + 420, y);
            memTamthanTuduyNoidung.Location = new Point(x1 + 420 + lblW, y); memTamthanTuduyNoidung.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanTuduyNoidung, "TamthanTuduyNoidung");
            y += 48;
            lblTamthanHanhviYchi.Text = "HV ý chí:"; lblTamthanHanhviYchi.Location = new Point(x1, y);
            memTamthanHanhviYchi.Location = new Point(x1 + lblW, y); memTamthanHanhviYchi.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanHanhviYchi, "TamthanHanhviYchi");
            lblTamthanHanhviBannang.Text = "HV bản năng:"; lblTamthanHanhviBannang.Location = new Point(x1 + 420, y);
            memTamthanHanhviBannang.Location = new Point(x1 + 420 + lblW, y); memTamthanHanhviBannang.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanHanhviBannang, "TamthanHanhviBannang");
            y += 48;
            lblTamthanTrinho.Text = "Trí nhớ:"; lblTamthanTrinho.Location = new Point(x1, y);
            memTamthanTrinho.Location = new Point(x1 + lblW, y); memTamthanTrinho.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanTrinho, "TamthanTrinho");
            lblTamthanTritue.Text = "Trí tuệ:"; lblTamthanTritue.Location = new Point(x1 + 420, y);
            memTamthanTritue.Location = new Point(x1 + 420 + lblW, y); memTamthanTritue.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanTritue, "TamthanTritue");
            y += 48;
            lblTamthanChuy.Text = "Chú ý:"; lblTamthanChuy.Location = new Point(x1, y);
            memTamthanChuy.Location = new Point(x1 + lblW, y); memTamthanChuy.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanChuy, "TamthanChuy");
            lblTamthanKhac.Text = "Khác:"; lblTamthanKhac.Location = new Point(x1 + 420, y);
            memTamthanKhac.Location = new Point(x1 + 420 + lblW, y); memTamthanKhac.Size = new Size(250, 40); entityBindingProvider1.SetBindingMember(memTamthanKhac, "TamthanKhac");
            y += 48;
            lblTracnghiemTamly.Text = "Trắc nghiệm TL:"; lblTracnghiemTamly.Location = new Point(x1, y);
            txtTracnghiemTamly.Location = new Point(x1 + lblW, y); txtTracnghiemTamly.Size = new Size(250, 20); entityBindingProvider1.SetBindingMember(txtTracnghiemTamly, "TracnghiemTamly");
            lblCanlamsangKhacBs.Text = "CLS BS:"; lblCanlamsangKhacBs.Location = new Point(x1 + 420, y);
            txtCanlamsangKhacBs.Location = new Point(x1 + 420 + lblW, y); txtCanlamsangKhacBs.Size = new Size(250, 20);             entityBindingProvider1.SetBindingMember(txtCanlamsangKhacBs, "CanlamsangKhacBs");
        }

        // ===========================================================
        //  Init data — combo mau KSK, enum combos, print menu
        // ===========================================================

        private async Task InitAndLoadAsync(int? id)
        {
            InitLoaiMauKskCombo();
            InitEnumCombos();
            SetupPrintMenu();

            await LoadDataAsync(id);
            UpdateTabVisibility();
        }

        /// <summary>
        /// Khoi tao ComboBoxEdit cho LoaiMauKsk (1:Tre em, 2:6-18t, 3:&gt;=18t, 4:Tam than).
        /// </summary>
        private void InitLoaiMauKskCombo()
        {
            comboLoaiMauKsk.Properties.Items.Clear();
            comboLoaiMauKsk.Properties.Items.AddRange(new object[] {
                new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Trẻ em <6 tuổi", 1, -1),
                new DevExpress.XtraEditors.Controls.ImageComboBoxItem("6–18 tuổi", 2, -1),
                new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Người lớn ≥18 tuổi", 3, -1),
                new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tâm thần", 4, -1)
            });
            comboLoaiMauKsk.SelectedIndexChanged += (s, e) => UpdateTabVisibility();
        }

        /// <summary>
        /// Khoi tao cac ComboBoxEdit cho cot co bang ma (0:..; 1:..; 2:..).
        /// </summary>
        private void InitEnumCombos()
        {
            AddEnumItems(comboGioitinh, new[] { "Nữ (0)", "Nam (1)" }, new[] { 0, 1 });
            AddEnumItems(comboLoaiqh, new[] { "Cha/mẹ (0)", "Vợ/chồng (1)", "Anh chị em (2)", "Khác (3)" }, new[] { 0, 1, 2, 3 });
            AddEnumItems(comboTrangthaiVongdau, new[] { "Bình thường (0)", "Đầu to (1)", "Đầu nhỏ (2)" }, new[] { 0, 1, 2 });
            AddEnumItems(comboMausacDa, new[] { "Hồng hào (0)", "Nhợt (1)", "Tím (2)", "Vàng (3)", "Sạm (4)" }, new[] { 0, 1, 2, 3, 4 });
            AddEnumItems(comboThop, new[] { "Bình thường (0)", "Rộng (1)", "Hẹp (2)", "Phồng (3)" }, new[] { 0, 1, 2, 3 });
            AddEnumItems(comboHinhdangDau, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboVandongCo, new[] { "Bình thường (0)", "Giới hạn (1)" }, new[] { 0, 1 });
            AddEnumItems(comboVitriHaimat, new[] { "Bình thường (0)", "Xa nhau (1)" }, new[] { 0, 1 });
            AddEnumItems(comboMimatKetmac, new[] { "Bình thường (0)", "Sưng đỏ (1)", "Chảy ghèn (2)" }, new[] { 0, 1, 2 });
            AddEnumItems(comboDongtu, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboTaiMangnhi, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboDapungAmthanh, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboHinhdangMui, new[] { "Bình thường (0)", "To dày (1)", "Bất sản (2)" }, new[] { 0, 1, 2 });
            AddEnumItems(comboHinhdangMieng, new[] { "Bình thường (0)", "Sứt môi chẻ vòm (1)" }, new[] { 0, 1 });
            AddEnumItems(comboHinhdangLuoi, new[] { "Bình thường (0)", "To bè (1)" }, new[] { 0, 1 });
            AddEnumItems(comboHong, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboNghePhoi, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboVitriMomtim, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboMachNgoaivi, new[] { "Bắt rõ (0)", "Mạch nhẹ (1)", "Không bắt được (2)" }, new[] { 0, 1, 2 });
            AddEnumItems(comboHinhdangBungRon, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboLoHaumon, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboCqsdNgoai, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboTruonglucCo, new[] { "Bình thường (0)", "Tăng (1)" }, new[] { 0, 1 });
            AddEnumItems(comboKhopHang, new[] { "Bình thường (0)", "Trật khớp (1)" }, new[] { 0, 1 });
            AddEnumItems(comboPhanxaCo, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboLungCotsong, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboTuchiKhop, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboDangDi, new[] { "Bình thường (1)", "Không BT (0)" }, new[] { 1, 0 });
            AddEnumItems(comboTrangthaiNhietdo, new[] { "Bình thường (0)", "Sốt (1)", "Hạ thân nhiệt (2)" }, new[] { 0, 1, 2 });
            AddEnumItems(comboTrangthaiMach, new[] { "Bình thường (0)", "Nhanh (1)" }, new[] { 0, 1 });
            AddEnumItems(comboTrangthaiNhiptho, new[] { "Bình thường (0)", "Nhanh (1)", "Chậm (2)" }, new[] { 0, 1, 2 });
            AddEnumItems(comboSanKhoaKhongBt, new[] { "Đẻ thiếu tháng (0)", "Đẻ thừa cân (1)", "Đẻ can thiệp (2)", "Đẻ ngạt (3)", "Mẹ bị bệnh (4)" }, new[] { 0, 1, 2, 3, 4 });
        }

        private static void AddEnumItems(ComboBoxEdit combo, string[] labels, int[] values)
        {
            combo.Properties.Items.Clear();
            for (int i = 0; i < labels.Length; i++)
                combo.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(labels[i], values[i], -1));
        }

        /// <summary>
        /// Show/hide tab con theo LoaiMauKsk. Tab Tong hop luon hien.
        /// </summary>
        private void UpdateTabVisibility()
        {
            var loai = GetSelectedLoaiMauKsk();
            tabKsk.TabPages[1].PageVisible = (loai == 1);
            tabKsk.TabPages[2].PageVisible = (loai == 2);
            tabKsk.TabPages[3].PageVisible = (loai == 3);
            tabKsk.TabPages[4].PageVisible = (loai == 4);
        }

        private int GetSelectedLoaiMauKsk()
        {
            if (comboLoaiMauKsk.EditValue != null)
                return Convert.ToInt32(comboLoaiMauKsk.EditValue);
            return 1;
        }

        /// <summary>
        /// Tao menu con cho nut "In phieu ket qua" — placeholder MessageBox cho tung mau.
        /// </summary>
        private void SetupPrintMenu()
        {
            var menu = new DevExpress.XtraBars.PopupMenu();
            string[] mauTen = { "trẻ em <6 tuổi", "6–18 tuổi", "người lớn ≥18 tuổi", "tâm thần" };
            for (int i = 0; i < 4; i++)
            {
                var item = new DevExpress.XtraBars.BarButtonItem(barManager1,
                    string.Format("In mẫu KSK {0}", mauTen[i]));
                int idx = i;
                item.ItemClick += (s, e) =>
                {
                    XtraMessageBox.Show(this,
                        string.Format("Chưa cài đặt in mẫu KSK {0}.", mauTen[idx]),
                        "In phiếu kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                menu.ItemLinks.Add(item);
            }
            btnInPhieu.DropDownControl = menu;
        }

        protected override void OnAfterLoad(object data)
        {
            base.OnAfterLoad(data);
            UpdateTabVisibility();
        }

        private async Task SaveAndCloseAsync()
        {
            var ok = await SaveAsync();
            if (ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private async Task DeleteAndCloseAsync()
        {
            if (!CurrentId.HasValue) { Close(); return; }
            var confirm = XtraMessageBox.Show("Xóa phiếu KSK này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            await DeleteAsync(CurrentId.Value);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
