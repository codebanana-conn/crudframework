namespace CrudFramework.Sample
{
    partial class KskPhieuForm
    {
        private System.ComponentModel.IContainer components = null;

        // Binding + Error + Bar
        private CrudFramework.WinForms.EntityBindingProvider entityBindingProvider1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraBars.BarManager barManager1;

        // Tab control + pages
        private DevExpress.XtraTab.XtraTabControl tabKsk;
        private DevExpress.XtraTab.XtraTabPage tabTongHop;
        private DevExpress.XtraTab.XtraTabPage tabTreEm;
        private DevExpress.XtraTab.XtraTabPage tab6_18;
        private DevExpress.XtraTab.XtraTabPage tab18;
        private DevExpress.XtraTab.XtraTabPage tabTamThan;

        // Buttons
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.DropDownButton btnInPhieu;

        // ---- Tong hop: thong tin chung ----
        private DevExpress.XtraEditors.LabelControl lblLoaiMauKsk;
        private DevExpress.XtraEditors.ComboBoxEdit comboLoaiMauKsk;
        private DevExpress.XtraEditors.LabelControl lblHoTen;
        private DevExpress.XtraEditors.TextEdit txtHolot;
        private DevExpress.XtraEditors.TextEdit txtTen;
        private DevExpress.XtraEditors.LabelControl lblNgaySinh;
        private DevExpress.XtraEditors.DateEdit dtNgaysinh;
        private DevExpress.XtraEditors.LabelControl lblGioitinh;
        private DevExpress.XtraEditors.ComboBoxEdit comboGioitinh;
        private DevExpress.XtraEditors.LabelControl lblCccd;
        private DevExpress.XtraEditors.TextEdit txtCmnd;
        private DevExpress.XtraEditors.LabelControl lblNgayCap;
        private DevExpress.XtraEditors.DateEdit dtNgaycap;
        private DevExpress.XtraEditors.LabelControl lblNoiCap;
        private DevExpress.XtraEditors.TextEdit txtNoicap;
        private DevExpress.XtraEditors.LabelControl lblDiachi;
        private DevExpress.XtraEditors.TextEdit txtDiachi;
        private DevExpress.XtraEditors.LabelControl lblNgayKsk;
        private DevExpress.XtraEditors.DateEdit dtNgayKsk;
        private DevExpress.XtraEditors.LabelControl lblCosoKham;
        private DevExpress.XtraEditors.TextEdit txtCosoKham;
        private DevExpress.XtraEditors.LabelControl lblLydoKsk;
        private DevExpress.XtraEditors.TextEdit txtLydoKsk;
        private DevExpress.XtraEditors.LabelControl lblNhietdo;
        private DevExpress.XtraEditors.SpinEdit spNhietdo;
        private DevExpress.XtraEditors.LabelControl lblMach;
        private DevExpress.XtraEditors.SpinEdit spMach;
        private DevExpress.XtraEditors.LabelControl lblNhiptho;
        private DevExpress.XtraEditors.SpinEdit spNhiptho;
        private DevExpress.XtraEditors.LabelControl lblChieucao;
        private DevExpress.XtraEditors.SpinEdit spChieucao;
        private DevExpress.XtraEditors.LabelControl lblCannang;
        private DevExpress.XtraEditors.SpinEdit spCannang;
        private DevExpress.XtraEditors.LabelControl lblHuyetap;
        private DevExpress.XtraEditors.TextEdit txtHuyetap;
        private DevExpress.XtraEditors.LabelControl lblHotenQh;
        private DevExpress.XtraEditors.TextEdit txtHotenQh;
        private DevExpress.XtraEditors.LabelControl lblLoaiqh;
        private DevExpress.XtraEditors.ComboBoxEdit comboLoaiqh;
        private DevExpress.XtraEditors.LabelControl lblDienthoaiQh;
        private DevExpress.XtraEditors.TextEdit txtDienthoaiQh;
        private DevExpress.XtraEditors.LabelControl lblCmndQh;
        private DevExpress.XtraEditors.TextEdit txtCmndQh;
        private DevExpress.XtraEditors.LabelControl lblDangDtbenh;
        private DevExpress.XtraEditors.TextEdit txtDangDtbenh;
        private DevExpress.XtraEditors.CheckEdit chkTsbenhCoBenhbamsinh;
        private DevExpress.XtraEditors.TextEdit txtTsbenhTenbenh;
        private DevExpress.XtraEditors.CheckEdit chkTsgdCoBenhTruyennhiem;
        private DevExpress.XtraEditors.TextEdit txtTsgdTenbenhTruyennhiem;
        private DevExpress.XtraEditors.LabelControl lblTuanhoanKsk;
        private DevExpress.XtraEditors.TextEdit txtTuanhoanKsk;
        private DevExpress.XtraEditors.LabelControl lblHohapKsk;
        private DevExpress.XtraEditors.TextEdit txtHohapKsk;
        private DevExpress.XtraEditors.LabelControl lblTieuhoaKsk;
        private DevExpress.XtraEditors.TextEdit txtTieuhoaKsk;
        private DevExpress.XtraEditors.LabelControl lblThanKsk;
        private DevExpress.XtraEditors.TextEdit txtThanKsk;
        private DevExpress.XtraEditors.LabelControl lblThankinhKsk;
        private DevExpress.XtraEditors.TextEdit txtThankinhKsk;
        private DevExpress.XtraEditors.LabelControl lblTamthanKsk;
        private DevExpress.XtraEditors.TextEdit txtTamthanKsk;
        private DevExpress.XtraEditors.LabelControl lblKhamlsKhacKsk;
        private DevExpress.XtraEditors.TextEdit txtKhamlsKhacKsk;
        private DevExpress.XtraEditors.LabelControl lblKqclsKsk;
        private DevExpress.XtraEditors.TextEdit txtKqclsKsk;
        private DevExpress.XtraEditors.LabelControl lblKmatphai;
        private DevExpress.XtraEditors.SpinEdit spKmatphai;
        private DevExpress.XtraEditors.LabelControl lblKmattrai;
        private DevExpress.XtraEditors.SpinEdit spKmattrai;
        private DevExpress.XtraEditors.LabelControl lblMatphai;
        private DevExpress.XtraEditors.SpinEdit spMatphai;
        private DevExpress.XtraEditors.LabelControl lblMattrai;
        private DevExpress.XtraEditors.SpinEdit spMattrai;
        private DevExpress.XtraEditors.LabelControl lblHamtren;
        private DevExpress.XtraEditors.TextEdit txtHamtren;
        private DevExpress.XtraEditors.LabelControl lblHamduoi;
        private DevExpress.XtraEditors.TextEdit txtHamduoi;
        private DevExpress.XtraEditors.CheckEdit chkRhmbenh;
        private DevExpress.XtraEditors.LabelControl lblNgoaikhoa;
        private DevExpress.XtraEditors.TextEdit txtNgoaikhoa;
        private DevExpress.XtraEditors.LabelControl lblPlngoaikhoa;
        private DevExpress.XtraEditors.TextEdit txtPlngoaikhoa;
        private DevExpress.XtraEditors.LabelControl lblDalieu;
        private DevExpress.XtraEditors.TextEdit txtDalieu;
        private DevExpress.XtraEditors.LabelControl lblPldalieu;
        private DevExpress.XtraEditors.TextEdit txtPldalieu;
        private DevExpress.XtraEditors.LabelControl lblSanphukhoa;
        private DevExpress.XtraEditors.TextEdit txtSanphukhoa;
        private DevExpress.XtraEditors.LabelControl lblPlsanphukhoa;
        private DevExpress.XtraEditors.TextEdit txtPlsanphukhoa;

        // ---- Tre em <6t ----
        private DevExpress.XtraEditors.CheckEdit chkTsTiepxucLao;
        private DevExpress.XtraEditors.LabelControl lblChieudaiTuoiSd;
        private DevExpress.XtraEditors.SpinEdit spChieudaiTuoiSd;
        private DevExpress.XtraEditors.LabelControl lblCannangTuoiSd;
        private DevExpress.XtraEditors.SpinEdit spCannangTuoiSd;
        private DevExpress.XtraEditors.LabelControl lblTrangthaiVongdau;
        private DevExpress.XtraEditors.ComboBoxEdit comboTrangthaiVongdau;
        private DevExpress.XtraEditors.LabelControl lblChuviVongcanhtay;
        private DevExpress.XtraEditors.SpinEdit spChuviVongcanhtay;
        private DevExpress.XtraEditors.LabelControl lblTinhtrangDinhduong;
        private DevExpress.XtraEditors.TextEdit txtTinhtrangDinhduong;
        private DevExpress.XtraEditors.CheckEdit chkPhattrienTinhthan;
        private DevExpress.XtraEditors.CheckEdit chkPhattrienVandong;
        private DevExpress.XtraEditors.CheckEdit chkNguycoTuky;
        private DevExpress.XtraEditors.CheckEdit chkBenhLao;
        private DevExpress.XtraEditors.CheckEdit chkTiemVgbMui1;
        private DevExpress.XtraEditors.CheckEdit chkTiemchungDaydu;
        private DevExpress.XtraEditors.LabelControl lblMausacDa;
        private DevExpress.XtraEditors.ComboBoxEdit comboMausacDa;
        private DevExpress.XtraEditors.CheckEdit chkLongBantay;
        private DevExpress.XtraEditors.LabelControl lblThop;
        private DevExpress.XtraEditors.ComboBoxEdit comboThop;
        private DevExpress.XtraEditors.LabelControl lblHinhdangDau;
        private DevExpress.XtraEditors.ComboBoxEdit comboHinhdangDau;
        private DevExpress.XtraEditors.LabelControl lblVandongCo;
        private DevExpress.XtraEditors.ComboBoxEdit comboVandongCo;
        private DevExpress.XtraEditors.CheckEdit chkKhoibatthuongDauco;
        private DevExpress.XtraEditors.LabelControl lblVitriHaimat;
        private DevExpress.XtraEditors.ComboBoxEdit comboVitriHaimat;
        private DevExpress.XtraEditors.LabelControl lblMimatKetmac;
        private DevExpress.XtraEditors.ComboBoxEdit comboMimatKetmac;
        private DevExpress.XtraEditors.LabelControl lblDongtu;
        private DevExpress.XtraEditors.ComboBoxEdit comboDongtu;
        private DevExpress.XtraEditors.CheckEdit chkLacmat;
        private DevExpress.XtraEditors.LabelControl lblTaiMangnhi;
        private DevExpress.XtraEditors.ComboBoxEdit comboTaiMangnhi;
        private DevExpress.XtraEditors.LabelControl lblDapungAmthanh;
        private DevExpress.XtraEditors.ComboBoxEdit comboDapungAmthanh;
        private DevExpress.XtraEditors.CheckEdit chkKhoisungSautai;
        private DevExpress.XtraEditors.CheckEdit chkChaymuNuoctai;
        private DevExpress.XtraEditors.LabelControl lblHinhdangMui;
        private DevExpress.XtraEditors.ComboBoxEdit comboHinhdangMui;
        private DevExpress.XtraEditors.CheckEdit chkChaynuocMui;
        private DevExpress.XtraEditors.CheckEdit chkNghetMui;
        private DevExpress.XtraEditors.LabelControl lblHong;
        private DevExpress.XtraEditors.ComboBoxEdit comboHong;
        private DevExpress.XtraEditors.LabelControl lblHinhdangMieng;
        private DevExpress.XtraEditors.ComboBoxEdit comboHinhdangMieng;
        private DevExpress.XtraEditors.CheckEdit chkRangsuaSosinh;
        private DevExpress.XtraEditors.LabelControl lblHinhdangLuoi;
        private DevExpress.XtraEditors.ComboBoxEdit comboHinhdangLuoi;
        private DevExpress.XtraEditors.CheckEdit chkDinhThangluoi;
        private DevExpress.XtraEditors.CheckEdit chkNamMieng;
        private DevExpress.XtraEditors.CheckEdit chkCamTut;
        private DevExpress.XtraEditors.CheckEdit chkVetsauRang;
        private DevExpress.XtraEditors.LabelControl lblNhipthoKhongdeu;
        private DevExpress.XtraEditors.ComboBoxEdit comboNhipthoKhongdeu;
        private DevExpress.XtraEditors.CheckEdit chkThorutlomLongnguc;
        private DevExpress.XtraEditors.CheckEdit chkTiengthoBatthuong;
        private DevExpress.XtraEditors.CheckEdit chkDauhieuSuyhohap;
        private DevExpress.XtraEditors.LabelControl lblNghePhoi;
        private DevExpress.XtraEditors.ComboBoxEdit comboNghePhoi;
        private DevExpress.XtraEditors.LabelControl lblVitriMomtim;
        private DevExpress.XtraEditors.ComboBoxEdit comboVitriMomtim;
        private DevExpress.XtraEditors.LabelControl lblMachNgoaivi;
        private DevExpress.XtraEditors.ComboBoxEdit comboMachNgoaivi;
        private DevExpress.XtraEditors.CheckEdit chkNgheTim;
        private DevExpress.XtraEditors.LabelControl lblHinhdangBungRon;
        private DevExpress.XtraEditors.ComboBoxEdit comboHinhdangBungRon;
        private DevExpress.XtraEditors.CheckEdit chkGanLachTo;
        private DevExpress.XtraEditors.CheckEdit chkKhoibatthuongBung;
        private DevExpress.XtraEditors.LabelControl lblLoHaumon;
        private DevExpress.XtraEditors.ComboBoxEdit comboLoHaumon;
        private DevExpress.XtraEditors.LabelControl lblCqsdNgoai;
        private DevExpress.XtraEditors.ComboBoxEdit comboCqsdNgoai;
        private DevExpress.XtraEditors.CheckEdit chkVandongKhongdoixung;
        private DevExpress.XtraEditors.CheckEdit chkPhanxaBu;
        private DevExpress.XtraEditors.CheckEdit chkPhanxaNam;
        private DevExpress.XtraEditors.CheckEdit chkPhanxaMoro;
        private DevExpress.XtraEditors.LabelControl lblTruonglucCo;
        private DevExpress.XtraEditors.ComboBoxEdit comboTruonglucCo;
        private DevExpress.XtraEditors.LabelControl lblKhopHang;
        private DevExpress.XtraEditors.ComboBoxEdit comboKhopHang;
        private DevExpress.XtraEditors.LabelControl lblPhanxaCo;
        private DevExpress.XtraEditors.ComboBoxEdit comboPhanxaCo;
        private DevExpress.XtraEditors.LabelControl lblLungCotsong;
        private DevExpress.XtraEditors.ComboBoxEdit comboLungCotsong;
        private DevExpress.XtraEditors.LabelControl lblTuchiKhop;
        private DevExpress.XtraEditors.ComboBoxEdit comboTuchiKhop;
        private DevExpress.XtraEditors.LabelControl lblDangDi;
        private DevExpress.XtraEditors.ComboBoxEdit comboDangDi;
        private DevExpress.XtraEditors.LabelControl lblDoituong;
        private DevExpress.XtraEditors.TextEdit txtDoituong;
        private DevExpress.XtraEditors.CheckEdit chkSinhNon;
        private DevExpress.XtraEditors.LabelControl lblTuanthaiKhiSinh;
        private DevExpress.XtraEditors.TextEdit txtTuanthaiKhiSinh;
        private DevExpress.XtraEditors.LabelControl lblNguonChitra;
        private DevExpress.XtraEditors.TextEdit txtNguonChitra;
        private DevExpress.XtraEditors.LabelControl lblTrangthaiNhietdo;
        private DevExpress.XtraEditors.ComboBoxEdit comboTrangthaiNhietdo;
        private DevExpress.XtraEditors.LabelControl lblTrangthaiMach;
        private DevExpress.XtraEditors.ComboBoxEdit comboTrangthaiMach;
        private DevExpress.XtraEditors.LabelControl lblTrangthaiNhiptho;
        private DevExpress.XtraEditors.ComboBoxEdit comboTrangthaiNhiptho;
        private DevExpress.XtraEditors.LabelControl lblKetluanBinhthuong;
        private DevExpress.XtraEditors.MemoEdit memKetluanBinhthuong;
        private DevExpress.XtraEditors.LabelControl lblKetluanNguycoLao;
        private DevExpress.XtraEditors.MemoEdit memKetluanNguycoLao;
        private DevExpress.XtraEditors.LabelControl lblKetluanVandesuckhoe;
        private DevExpress.XtraEditors.MemoEdit memKetluanVandesuckhoe;
        private DevExpress.XtraEditors.LabelControl lblKetluanGhiro;
        private DevExpress.XtraEditors.MemoEdit memKetluanGhiro;

        // ---- 6-18t ----
        private DevExpress.XtraEditors.LabelControl lblMaIcdTsgdBamsinh;
        private DevExpress.XtraEditors.TextEdit txtMaIcdTsgdBamsinh;
        private DevExpress.XtraEditors.LabelControl lblMaIcdTsBamsinh;
        private DevExpress.XtraEditors.TextEdit txtMaIcdTsBamsinh;
        private DevExpress.XtraEditors.CheckEdit chkSanKhoa;
        private DevExpress.XtraEditors.LabelControl lblSanKhoaKhongBt;
        private DevExpress.XtraEditors.ComboBoxEdit comboSanKhoaKhongBt;
        private DevExpress.XtraEditors.LabelControl lblMaBenhSanKhoaKhongBt;
        private DevExpress.XtraEditors.TextEdit txtMaBenhSanKhoaKhongBt;

        // ---- >=18t ----
        private DevExpress.XtraEditors.CheckEdit chkBenh5nam;
        private DevExpress.XtraEditors.CheckEdit chkBenhThankinh;
        private DevExpress.XtraEditors.CheckEdit chkBenhMat;
        private DevExpress.XtraEditors.CheckEdit chkBenhTai;
        private DevExpress.XtraEditors.CheckEdit chkBenhTim;
        private DevExpress.XtraEditors.CheckEdit chkPhauthuatTimmach;
        private DevExpress.XtraEditors.CheckEdit chkTangHuyetap;
        private DevExpress.XtraEditors.CheckEdit chkKhoTho;
        private DevExpress.XtraEditors.CheckEdit chkBenhPhoi;
        private DevExpress.XtraEditors.CheckEdit chkBenhThanLocmau;
        private DevExpress.XtraEditors.CheckEdit chkNghienRuoubia;
        private DevExpress.XtraEditors.CheckEdit chkDaiThaoduong;
        private DevExpress.XtraEditors.CheckEdit chkBenhTamthan;
        private DevExpress.XtraEditors.CheckEdit chkRoiLoanYthuc;
        private DevExpress.XtraEditors.CheckEdit chkNgatChongmat;
        private DevExpress.XtraEditors.CheckEdit chkBenhTieuhoa;
        private DevExpress.XtraEditors.CheckEdit chkRoiLoanGiacngu;
        private DevExpress.XtraEditors.CheckEdit chkTaibienLiet;
        private DevExpress.XtraEditors.CheckEdit chkBenhCotsong;
        private DevExpress.XtraEditors.CheckEdit chkSudungRuou;
        private DevExpress.XtraEditors.CheckEdit chkSudungMatuy;
        private DevExpress.XtraEditors.CheckEdit chkBenhKhac;
        private DevExpress.XtraEditors.LabelControl lblTenBenhKhac;
        private DevExpress.XtraEditors.TextEdit txtTenBenhKhac;
        private DevExpress.XtraEditors.LabelControl lblClsHuyethoc;
        private DevExpress.XtraEditors.TextEdit txtClsHuyethoc;
        private DevExpress.XtraEditors.LabelControl lblClsDuongmau;
        private DevExpress.XtraEditors.TextEdit txtClsDuongmau;
        private DevExpress.XtraEditors.LabelControl lblClsUre;
        private DevExpress.XtraEditors.TextEdit txtClsUre;
        private DevExpress.XtraEditors.LabelControl lblClsCreatinin;
        private DevExpress.XtraEditors.TextEdit txtClsCreatinin;
        private DevExpress.XtraEditors.LabelControl lblClsAsat;
        private DevExpress.XtraEditors.TextEdit txtClsAsat;
        private DevExpress.XtraEditors.LabelControl lblClsAlat;
        private DevExpress.XtraEditors.TextEdit txtClsAlat;
        private DevExpress.XtraEditors.LabelControl lblClsTongphantichNt;
        private DevExpress.XtraEditors.TextEdit txtClsTongphantichNt;
        private DevExpress.XtraEditors.LabelControl lblClsNtKhac;
        private DevExpress.XtraEditors.TextEdit txtClsNtKhac;
        private DevExpress.XtraEditors.LabelControl lblClsXqTimphoi;
        private DevExpress.XtraEditors.TextEdit txtClsXqTimphoi;
        private DevExpress.XtraEditors.CheckEdit chkClsKhac;
        private DevExpress.XtraEditors.LabelControl lblClsKhacNoidung;
        private DevExpress.XtraEditors.TextEdit txtClsKhacNoidung;

        // ---- Tam than ----
        private DevExpress.XtraEditors.CheckEdit chkTsTamthanGiadinh;
        private DevExpress.XtraEditors.LabelControl lblTenTamthanGiadinh;
        private DevExpress.XtraEditors.TextEdit txtTenTamthanGiadinh;
        private DevExpress.XtraEditors.CheckEdit chkTsTamthanBanthan;
        private DevExpress.XtraEditors.LabelControl lblTenTamthanBanthan;
        private DevExpress.XtraEditors.TextEdit txtTenTamthanBanthan;
        private DevExpress.XtraEditors.LabelControl lblTamthanBieuhien;
        private DevExpress.XtraEditors.MemoEdit memTamthanBieuhien;
        private DevExpress.XtraEditors.LabelControl lblTamthanYthuc;
        private DevExpress.XtraEditors.MemoEdit memTamthanYthuc;
        private DevExpress.XtraEditors.LabelControl lblTamthanDinhhuongKhonggian;
        private DevExpress.XtraEditors.TextEdit txtTamthanDinhhuongKhonggian;
        private DevExpress.XtraEditors.LabelControl lblTamthanDinhhuongThoigian;
        private DevExpress.XtraEditors.TextEdit txtTamthanDinhhuongThoigian;
        private DevExpress.XtraEditors.LabelControl lblTamthanDinhhuongXungquanh;
        private DevExpress.XtraEditors.TextEdit txtTamthanDinhhuongXungquanh;
        private DevExpress.XtraEditors.LabelControl lblTamthanDinhhuongBanthan;
        private DevExpress.XtraEditors.TextEdit txtTamthanDinhhuongBanthan;
        private DevExpress.XtraEditors.LabelControl lblTamthanCamxuc;
        private DevExpress.XtraEditors.MemoEdit memTamthanCamxuc;
        private DevExpress.XtraEditors.LabelControl lblTamthanCamgiacTrigiac;
        private DevExpress.XtraEditors.MemoEdit memTamthanCamgiacTrigiac;
        private DevExpress.XtraEditors.LabelControl lblTamthanTuduyHinhthuc;
        private DevExpress.XtraEditors.MemoEdit memTamthanTuduyHinhthuc;
        private DevExpress.XtraEditors.LabelControl lblTamthanTuduyNoidung;
        private DevExpress.XtraEditors.MemoEdit memTamthanTuduyNoidung;
        private DevExpress.XtraEditors.LabelControl lblTamthanHanhviYchi;
        private DevExpress.XtraEditors.MemoEdit memTamthanHanhviYchi;
        private DevExpress.XtraEditors.LabelControl lblTamthanHanhviBannang;
        private DevExpress.XtraEditors.MemoEdit memTamthanHanhviBannang;
        private DevExpress.XtraEditors.LabelControl lblTamthanTrinho;
        private DevExpress.XtraEditors.MemoEdit memTamthanTrinho;
        private DevExpress.XtraEditors.LabelControl lblTamthanTritue;
        private DevExpress.XtraEditors.MemoEdit memTamthanTritue;
        private DevExpress.XtraEditors.LabelControl lblTamthanChuy;
        private DevExpress.XtraEditors.MemoEdit memTamthanChuy;
        private DevExpress.XtraEditors.LabelControl lblTamthanKhac;
        private DevExpress.XtraEditors.MemoEdit memTamthanKhac;
        private DevExpress.XtraEditors.LabelControl lblTracnghiemTamly;
        private DevExpress.XtraEditors.TextEdit txtTracnghiemTamly;
        private DevExpress.XtraEditors.LabelControl lblCanlamsangKhacBs;
        private DevExpress.XtraEditors.TextEdit txtCanlamsangKhacBs;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ---- Components ----
            this.components = new System.ComponentModel.Container();
            this.entityBindingProvider1 = new CrudFramework.WinForms.EntityBindingProvider(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);

            // ---- Tab control + pages ----
            this.tabKsk = new DevExpress.XtraTab.XtraTabControl();
            this.tabTongHop = new DevExpress.XtraTab.XtraTabPage();
            this.tabTreEm = new DevExpress.XtraTab.XtraTabPage();
            this.tab6_18 = new DevExpress.XtraTab.XtraTabPage();
            this.tab18 = new DevExpress.XtraTab.XtraTabPage();
            this.tabTamThan = new DevExpress.XtraTab.XtraTabPage();

            // ---- Buttons ----
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnInPhieu = new DevExpress.XtraEditors.DropDownButton();

            // ---- Tong hop ----
            this.lblLoaiMauKsk = new DevExpress.XtraEditors.LabelControl();
            this.comboLoaiMauKsk = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblHoTen = new DevExpress.XtraEditors.LabelControl();
            this.txtHolot = new DevExpress.XtraEditors.TextEdit();
            this.txtTen = new DevExpress.XtraEditors.TextEdit();
            this.lblNgaySinh = new DevExpress.XtraEditors.LabelControl();
            this.dtNgaysinh = new DevExpress.XtraEditors.DateEdit();
            this.lblGioitinh = new DevExpress.XtraEditors.LabelControl();
            this.comboGioitinh = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblCccd = new DevExpress.XtraEditors.LabelControl();
            this.txtCmnd = new DevExpress.XtraEditors.TextEdit();
            this.lblNgayCap = new DevExpress.XtraEditors.LabelControl();
            this.dtNgaycap = new DevExpress.XtraEditors.DateEdit();
            this.lblNoiCap = new DevExpress.XtraEditors.LabelControl();
            this.txtNoicap = new DevExpress.XtraEditors.TextEdit();
            this.lblDiachi = new DevExpress.XtraEditors.LabelControl();
            this.txtDiachi = new DevExpress.XtraEditors.TextEdit();
            this.lblNgayKsk = new DevExpress.XtraEditors.LabelControl();
            this.dtNgayKsk = new DevExpress.XtraEditors.DateEdit();
            this.lblCosoKham = new DevExpress.XtraEditors.LabelControl();
            this.txtCosoKham = new DevExpress.XtraEditors.TextEdit();
            this.lblLydoKsk = new DevExpress.XtraEditors.LabelControl();
            this.txtLydoKsk = new DevExpress.XtraEditors.TextEdit();
            this.lblNhietdo = new DevExpress.XtraEditors.LabelControl();
            this.spNhietdo = new DevExpress.XtraEditors.SpinEdit();
            this.lblMach = new DevExpress.XtraEditors.LabelControl();
            this.spMach = new DevExpress.XtraEditors.SpinEdit();
            this.lblNhiptho = new DevExpress.XtraEditors.LabelControl();
            this.spNhiptho = new DevExpress.XtraEditors.SpinEdit();
            this.lblChieucao = new DevExpress.XtraEditors.LabelControl();
            this.spChieucao = new DevExpress.XtraEditors.SpinEdit();
            this.lblCannang = new DevExpress.XtraEditors.LabelControl();
            this.spCannang = new DevExpress.XtraEditors.SpinEdit();
            this.lblHuyetap = new DevExpress.XtraEditors.LabelControl();
            this.txtHuyetap = new DevExpress.XtraEditors.TextEdit();
            this.lblHotenQh = new DevExpress.XtraEditors.LabelControl();
            this.txtHotenQh = new DevExpress.XtraEditors.TextEdit();
            this.lblLoaiqh = new DevExpress.XtraEditors.LabelControl();
            this.comboLoaiqh = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDienthoaiQh = new DevExpress.XtraEditors.LabelControl();
            this.txtDienthoaiQh = new DevExpress.XtraEditors.TextEdit();
            this.lblCmndQh = new DevExpress.XtraEditors.LabelControl();
            this.txtCmndQh = new DevExpress.XtraEditors.TextEdit();
            this.lblDangDtbenh = new DevExpress.XtraEditors.LabelControl();
            this.txtDangDtbenh = new DevExpress.XtraEditors.TextEdit();
            this.chkTsbenhCoBenhbamsinh = new DevExpress.XtraEditors.CheckEdit();
            this.txtTsbenhTenbenh = new DevExpress.XtraEditors.TextEdit();
            this.chkTsgdCoBenhTruyennhiem = new DevExpress.XtraEditors.CheckEdit();
            this.txtTsgdTenbenhTruyennhiem = new DevExpress.XtraEditors.TextEdit();
            this.lblTuanhoanKsk = new DevExpress.XtraEditors.LabelControl();
            this.txtTuanhoanKsk = new DevExpress.XtraEditors.TextEdit();
            this.lblHohapKsk = new DevExpress.XtraEditors.LabelControl();
            this.txtHohapKsk = new DevExpress.XtraEditors.TextEdit();
            this.lblTieuhoaKsk = new DevExpress.XtraEditors.LabelControl();
            this.txtTieuhoaKsk = new DevExpress.XtraEditors.TextEdit();
            this.lblThanKsk = new DevExpress.XtraEditors.LabelControl();
            this.txtThanKsk = new DevExpress.XtraEditors.TextEdit();
            this.lblThankinhKsk = new DevExpress.XtraEditors.LabelControl();
            this.txtThankinhKsk = new DevExpress.XtraEditors.TextEdit();
            this.lblTamthanKsk = new DevExpress.XtraEditors.LabelControl();
            this.txtTamthanKsk = new DevExpress.XtraEditors.TextEdit();
            this.lblKhamlsKhacKsk = new DevExpress.XtraEditors.LabelControl();
            this.txtKhamlsKhacKsk = new DevExpress.XtraEditors.TextEdit();
            this.lblKqclsKsk = new DevExpress.XtraEditors.LabelControl();
            this.txtKqclsKsk = new DevExpress.XtraEditors.TextEdit();
            this.lblKmatphai = new DevExpress.XtraEditors.LabelControl();
            this.spKmatphai = new DevExpress.XtraEditors.SpinEdit();
            this.lblKmattrai = new DevExpress.XtraEditors.LabelControl();
            this.spKmattrai = new DevExpress.XtraEditors.SpinEdit();
            this.lblMatphai = new DevExpress.XtraEditors.LabelControl();
            this.spMatphai = new DevExpress.XtraEditors.SpinEdit();
            this.lblMattrai = new DevExpress.XtraEditors.LabelControl();
            this.spMattrai = new DevExpress.XtraEditors.SpinEdit();
            this.lblHamtren = new DevExpress.XtraEditors.LabelControl();
            this.txtHamtren = new DevExpress.XtraEditors.TextEdit();
            this.lblHamduoi = new DevExpress.XtraEditors.LabelControl();
            this.txtHamduoi = new DevExpress.XtraEditors.TextEdit();
            this.chkRhmbenh = new DevExpress.XtraEditors.CheckEdit();
            this.lblNgoaikhoa = new DevExpress.XtraEditors.LabelControl();
            this.txtNgoaikhoa = new DevExpress.XtraEditors.TextEdit();
            this.lblPlngoaikhoa = new DevExpress.XtraEditors.LabelControl();
            this.txtPlngoaikhoa = new DevExpress.XtraEditors.TextEdit();
            this.lblDalieu = new DevExpress.XtraEditors.LabelControl();
            this.txtDalieu = new DevExpress.XtraEditors.TextEdit();
            this.lblPldalieu = new DevExpress.XtraEditors.LabelControl();
            this.txtPldalieu = new DevExpress.XtraEditors.TextEdit();
            this.lblSanphukhoa = new DevExpress.XtraEditors.LabelControl();
            this.txtSanphukhoa = new DevExpress.XtraEditors.TextEdit();
            this.lblPlsanphukhoa = new DevExpress.XtraEditors.LabelControl();
            this.txtPlsanphukhoa = new DevExpress.XtraEditors.TextEdit();

            // ---- Tre em <6t ----
            this.chkTsTiepxucLao = new DevExpress.XtraEditors.CheckEdit();
            this.lblChieudaiTuoiSd = new DevExpress.XtraEditors.LabelControl();
            this.spChieudaiTuoiSd = new DevExpress.XtraEditors.SpinEdit();
            this.lblCannangTuoiSd = new DevExpress.XtraEditors.LabelControl();
            this.spCannangTuoiSd = new DevExpress.XtraEditors.SpinEdit();
            this.lblTrangthaiVongdau = new DevExpress.XtraEditors.LabelControl();
            this.comboTrangthaiVongdau = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblChuviVongcanhtay = new DevExpress.XtraEditors.LabelControl();
            this.spChuviVongcanhtay = new DevExpress.XtraEditors.SpinEdit();
            this.lblTinhtrangDinhduong = new DevExpress.XtraEditors.LabelControl();
            this.txtTinhtrangDinhduong = new DevExpress.XtraEditors.TextEdit();
            this.chkPhattrienTinhthan = new DevExpress.XtraEditors.CheckEdit();
            this.chkPhattrienVandong = new DevExpress.XtraEditors.CheckEdit();
            this.chkNguycoTuky = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhLao = new DevExpress.XtraEditors.CheckEdit();
            this.chkTiemVgbMui1 = new DevExpress.XtraEditors.CheckEdit();
            this.chkTiemchungDaydu = new DevExpress.XtraEditors.CheckEdit();
            this.lblMausacDa = new DevExpress.XtraEditors.LabelControl();
            this.comboMausacDa = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkLongBantay = new DevExpress.XtraEditors.CheckEdit();
            this.lblThop = new DevExpress.XtraEditors.LabelControl();
            this.comboThop = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblHinhdangDau = new DevExpress.XtraEditors.LabelControl();
            this.comboHinhdangDau = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblVandongCo = new DevExpress.XtraEditors.LabelControl();
            this.comboVandongCo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkKhoibatthuongDauco = new DevExpress.XtraEditors.CheckEdit();
            this.lblVitriHaimat = new DevExpress.XtraEditors.LabelControl();
            this.comboVitriHaimat = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblMimatKetmac = new DevExpress.XtraEditors.LabelControl();
            this.comboMimatKetmac = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDongtu = new DevExpress.XtraEditors.LabelControl();
            this.comboDongtu = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkLacmat = new DevExpress.XtraEditors.CheckEdit();
            this.lblTaiMangnhi = new DevExpress.XtraEditors.LabelControl();
            this.comboTaiMangnhi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDapungAmthanh = new DevExpress.XtraEditors.LabelControl();
            this.comboDapungAmthanh = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkKhoisungSautai = new DevExpress.XtraEditors.CheckEdit();
            this.chkChaymuNuoctai = new DevExpress.XtraEditors.CheckEdit();
            this.lblHinhdangMui = new DevExpress.XtraEditors.LabelControl();
            this.comboHinhdangMui = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkChaynuocMui = new DevExpress.XtraEditors.CheckEdit();
            this.chkNghetMui = new DevExpress.XtraEditors.CheckEdit();
            this.lblHong = new DevExpress.XtraEditors.LabelControl();
            this.comboHong = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblHinhdangMieng = new DevExpress.XtraEditors.LabelControl();
            this.comboHinhdangMieng = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkRangsuaSosinh = new DevExpress.XtraEditors.CheckEdit();
            this.lblHinhdangLuoi = new DevExpress.XtraEditors.LabelControl();
            this.comboHinhdangLuoi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkDinhThangluoi = new DevExpress.XtraEditors.CheckEdit();
            this.chkNamMieng = new DevExpress.XtraEditors.CheckEdit();
            this.chkCamTut = new DevExpress.XtraEditors.CheckEdit();
            this.chkVetsauRang = new DevExpress.XtraEditors.CheckEdit();
            this.lblNhipthoKhongdeu = new DevExpress.XtraEditors.LabelControl();
            this.comboNhipthoKhongdeu = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkThorutlomLongnguc = new DevExpress.XtraEditors.CheckEdit();
            this.chkTiengthoBatthuong = new DevExpress.XtraEditors.CheckEdit();
            this.chkDauhieuSuyhohap = new DevExpress.XtraEditors.CheckEdit();
            this.lblNghePhoi = new DevExpress.XtraEditors.LabelControl();
            this.comboNghePhoi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblVitriMomtim = new DevExpress.XtraEditors.LabelControl();
            this.comboVitriMomtim = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblMachNgoaivi = new DevExpress.XtraEditors.LabelControl();
            this.comboMachNgoaivi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkNgheTim = new DevExpress.XtraEditors.CheckEdit();
            this.lblHinhdangBungRon = new DevExpress.XtraEditors.LabelControl();
            this.comboHinhdangBungRon = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkGanLachTo = new DevExpress.XtraEditors.CheckEdit();
            this.chkKhoibatthuongBung = new DevExpress.XtraEditors.CheckEdit();
            this.lblLoHaumon = new DevExpress.XtraEditors.LabelControl();
            this.comboLoHaumon = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblCqsdNgoai = new DevExpress.XtraEditors.LabelControl();
            this.comboCqsdNgoai = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkVandongKhongdoixung = new DevExpress.XtraEditors.CheckEdit();
            this.chkPhanxaBu = new DevExpress.XtraEditors.CheckEdit();
            this.chkPhanxaNam = new DevExpress.XtraEditors.CheckEdit();
            this.chkPhanxaMoro = new DevExpress.XtraEditors.CheckEdit();
            this.lblTruonglucCo = new DevExpress.XtraEditors.LabelControl();
            this.comboTruonglucCo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblKhopHang = new DevExpress.XtraEditors.LabelControl();
            this.comboKhopHang = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblPhanxaCo = new DevExpress.XtraEditors.LabelControl();
            this.comboPhanxaCo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblLungCotsong = new DevExpress.XtraEditors.LabelControl();
            this.comboLungCotsong = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblTuchiKhop = new DevExpress.XtraEditors.LabelControl();
            this.comboTuchiKhop = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDangDi = new DevExpress.XtraEditors.LabelControl();
            this.comboDangDi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDoituong = new DevExpress.XtraEditors.LabelControl();
            this.txtDoituong = new DevExpress.XtraEditors.TextEdit();
            this.chkSinhNon = new DevExpress.XtraEditors.CheckEdit();
            this.lblTuanthaiKhiSinh = new DevExpress.XtraEditors.LabelControl();
            this.txtTuanthaiKhiSinh = new DevExpress.XtraEditors.TextEdit();
            this.lblNguonChitra = new DevExpress.XtraEditors.LabelControl();
            this.txtNguonChitra = new DevExpress.XtraEditors.TextEdit();
            this.lblTrangthaiNhietdo = new DevExpress.XtraEditors.LabelControl();
            this.comboTrangthaiNhietdo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblTrangthaiMach = new DevExpress.XtraEditors.LabelControl();
            this.comboTrangthaiMach = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblTrangthaiNhiptho = new DevExpress.XtraEditors.LabelControl();
            this.comboTrangthaiNhiptho = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblKetluanBinhthuong = new DevExpress.XtraEditors.LabelControl();
            this.memKetluanBinhthuong = new DevExpress.XtraEditors.MemoEdit();
            this.lblKetluanNguycoLao = new DevExpress.XtraEditors.LabelControl();
            this.memKetluanNguycoLao = new DevExpress.XtraEditors.MemoEdit();
            this.lblKetluanVandesuckhoe = new DevExpress.XtraEditors.LabelControl();
            this.memKetluanVandesuckhoe = new DevExpress.XtraEditors.MemoEdit();
            this.lblKetluanGhiro = new DevExpress.XtraEditors.LabelControl();
            this.memKetluanGhiro = new DevExpress.XtraEditors.MemoEdit();

            // ---- 6-18t ----
            this.lblMaIcdTsgdBamsinh = new DevExpress.XtraEditors.LabelControl();
            this.txtMaIcdTsgdBamsinh = new DevExpress.XtraEditors.TextEdit();
            this.lblMaIcdTsBamsinh = new DevExpress.XtraEditors.LabelControl();
            this.txtMaIcdTsBamsinh = new DevExpress.XtraEditors.TextEdit();
            this.chkSanKhoa = new DevExpress.XtraEditors.CheckEdit();
            this.lblSanKhoaKhongBt = new DevExpress.XtraEditors.LabelControl();
            this.comboSanKhoaKhongBt = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblMaBenhSanKhoaKhongBt = new DevExpress.XtraEditors.LabelControl();
            this.txtMaBenhSanKhoaKhongBt = new DevExpress.XtraEditors.TextEdit();

            // ---- >=18t ----
            this.chkBenh5nam = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhThankinh = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhMat = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhTai = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhTim = new DevExpress.XtraEditors.CheckEdit();
            this.chkPhauthuatTimmach = new DevExpress.XtraEditors.CheckEdit();
            this.chkTangHuyetap = new DevExpress.XtraEditors.CheckEdit();
            this.chkKhoTho = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhPhoi = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhThanLocmau = new DevExpress.XtraEditors.CheckEdit();
            this.chkNghienRuoubia = new DevExpress.XtraEditors.CheckEdit();
            this.chkDaiThaoduong = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhTamthan = new DevExpress.XtraEditors.CheckEdit();
            this.chkRoiLoanYthuc = new DevExpress.XtraEditors.CheckEdit();
            this.chkNgatChongmat = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhTieuhoa = new DevExpress.XtraEditors.CheckEdit();
            this.chkRoiLoanGiacngu = new DevExpress.XtraEditors.CheckEdit();
            this.chkTaibienLiet = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhCotsong = new DevExpress.XtraEditors.CheckEdit();
            this.chkSudungRuou = new DevExpress.XtraEditors.CheckEdit();
            this.chkSudungMatuy = new DevExpress.XtraEditors.CheckEdit();
            this.chkBenhKhac = new DevExpress.XtraEditors.CheckEdit();
            this.lblTenBenhKhac = new DevExpress.XtraEditors.LabelControl();
            this.txtTenBenhKhac = new DevExpress.XtraEditors.TextEdit();
            this.lblClsHuyethoc = new DevExpress.XtraEditors.LabelControl();
            this.txtClsHuyethoc = new DevExpress.XtraEditors.TextEdit();
            this.lblClsDuongmau = new DevExpress.XtraEditors.LabelControl();
            this.txtClsDuongmau = new DevExpress.XtraEditors.TextEdit();
            this.lblClsUre = new DevExpress.XtraEditors.LabelControl();
            this.txtClsUre = new DevExpress.XtraEditors.TextEdit();
            this.lblClsCreatinin = new DevExpress.XtraEditors.LabelControl();
            this.txtClsCreatinin = new DevExpress.XtraEditors.TextEdit();
            this.lblClsAsat = new DevExpress.XtraEditors.LabelControl();
            this.txtClsAsat = new DevExpress.XtraEditors.TextEdit();
            this.lblClsAlat = new DevExpress.XtraEditors.LabelControl();
            this.txtClsAlat = new DevExpress.XtraEditors.TextEdit();
            this.lblClsTongphantichNt = new DevExpress.XtraEditors.LabelControl();
            this.txtClsTongphantichNt = new DevExpress.XtraEditors.TextEdit();
            this.lblClsNtKhac = new DevExpress.XtraEditors.LabelControl();
            this.txtClsNtKhac = new DevExpress.XtraEditors.TextEdit();
            this.lblClsXqTimphoi = new DevExpress.XtraEditors.LabelControl();
            this.txtClsXqTimphoi = new DevExpress.XtraEditors.TextEdit();
            this.chkClsKhac = new DevExpress.XtraEditors.CheckEdit();
            this.lblClsKhacNoidung = new DevExpress.XtraEditors.LabelControl();
            this.txtClsKhacNoidung = new DevExpress.XtraEditors.TextEdit();

            // ---- Tam than ----
            this.chkTsTamthanGiadinh = new DevExpress.XtraEditors.CheckEdit();
            this.lblTenTamthanGiadinh = new DevExpress.XtraEditors.LabelControl();
            this.txtTenTamthanGiadinh = new DevExpress.XtraEditors.TextEdit();
            this.chkTsTamthanBanthan = new DevExpress.XtraEditors.CheckEdit();
            this.lblTenTamthanBanthan = new DevExpress.XtraEditors.LabelControl();
            this.txtTenTamthanBanthan = new DevExpress.XtraEditors.TextEdit();
            this.lblTamthanBieuhien = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanBieuhien = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanYthuc = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanYthuc = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanDinhhuongKhonggian = new DevExpress.XtraEditors.LabelControl();
            this.txtTamthanDinhhuongKhonggian = new DevExpress.XtraEditors.TextEdit();
            this.lblTamthanDinhhuongThoigian = new DevExpress.XtraEditors.LabelControl();
            this.txtTamthanDinhhuongThoigian = new DevExpress.XtraEditors.TextEdit();
            this.lblTamthanDinhhuongXungquanh = new DevExpress.XtraEditors.LabelControl();
            this.txtTamthanDinhhuongXungquanh = new DevExpress.XtraEditors.TextEdit();
            this.lblTamthanDinhhuongBanthan = new DevExpress.XtraEditors.LabelControl();
            this.txtTamthanDinhhuongBanthan = new DevExpress.XtraEditors.TextEdit();
            this.lblTamthanCamxuc = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanCamxuc = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanCamgiacTrigiac = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanCamgiacTrigiac = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanTuduyHinhthuc = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanTuduyHinhthuc = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanTuduyNoidung = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanTuduyNoidung = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanHanhviYchi = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanHanhviYchi = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanHanhviBannang = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanHanhviBannang = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanTrinho = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanTrinho = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanTritue = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanTritue = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanChuy = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanChuy = new DevExpress.XtraEditors.MemoEdit();
            this.lblTamthanKhac = new DevExpress.XtraEditors.LabelControl();
            this.memTamthanKhac = new DevExpress.XtraEditors.MemoEdit();
            this.lblTracnghiemTamly = new DevExpress.XtraEditors.LabelControl();
            this.txtTracnghiemTamly = new DevExpress.XtraEditors.TextEdit();
            this.lblCanlamsangKhacBs = new DevExpress.XtraEditors.LabelControl();
            this.txtCanlamsangKhacBs = new DevExpress.XtraEditors.TextEdit();

            // ---- Tab pages ----
            this.tabKsk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabKsk.Name = "tabKsk";
            this.tabKsk.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
                this.tabTongHop, this.tabTreEm, this.tab6_18, this.tab18, this.tabTamThan });
            this.tabTongHop.Text = "Tong hop";
            this.tabTreEm.Text = "Tre em <6t";
            this.tab6_18.Text = "6-18t";
            this.tab18.Text = "Nguoi lon >=18t";
            this.tabTamThan.Text = "Tam than";

            // ---- Add controls to tab Tong hop ----
            this.tabTongHop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblLoaiMauKsk, this.comboLoaiMauKsk, this.lblHoTen, this.txtHolot, this.txtTen,
                this.lblNgaySinh, this.dtNgaysinh, this.lblGioitinh, this.comboGioitinh,
                this.lblCccd, this.txtCmnd, this.lblNgayCap, this.dtNgaycap,
                this.lblNoiCap, this.txtNoicap, this.lblDiachi, this.txtDiachi,
                this.lblNgayKsk, this.dtNgayKsk, this.lblCosoKham, this.txtCosoKham,
                this.lblLydoKsk, this.txtLydoKsk,
                this.lblNhietdo, this.spNhietdo, this.lblMach, this.spMach, this.lblNhiptho, this.spNhiptho,
                this.lblChieucao, this.spChieucao, this.lblCannang, this.spCannang,
                this.lblHuyetap, this.txtHuyetap,
                this.lblHotenQh, this.txtHotenQh, this.lblLoaiqh, this.comboLoaiqh,
                this.lblDienthoaiQh, this.txtDienthoaiQh, this.lblCmndQh, this.txtCmndQh,
                this.lblDangDtbenh, this.txtDangDtbenh,
                this.chkTsbenhCoBenhbamsinh, this.txtTsbenhTenbenh,
                this.chkTsgdCoBenhTruyennhiem, this.txtTsgdTenbenhTruyennhiem,
                this.lblTuanhoanKsk, this.txtTuanhoanKsk, this.lblHohapKsk, this.txtHohapKsk,
                this.lblTieuhoaKsk, this.txtTieuhoaKsk, this.lblThanKsk, this.txtThanKsk,
                this.lblThankinhKsk, this.txtThankinhKsk, this.lblTamthanKsk, this.txtTamthanKsk,
                this.lblKhamlsKhacKsk, this.txtKhamlsKhacKsk, this.lblKqclsKsk, this.txtKqclsKsk,
                this.lblKmatphai, this.spKmatphai, this.lblKmattrai, this.spKmattrai,
                this.lblMatphai, this.spMatphai, this.lblMattrai, this.spMattrai,
                this.lblHamtren, this.txtHamtren, this.lblHamduoi, this.txtHamduoi, this.chkRhmbenh,
                this.lblNgoaikhoa, this.txtNgoaikhoa, this.lblPlngoaikhoa, this.txtPlngoaikhoa,
                this.lblDalieu, this.txtDalieu, this.lblPldalieu, this.txtPldalieu,
                this.lblSanphukhoa, this.txtSanphukhoa, this.lblPlsanphukhoa, this.txtPlsanphukhoa
            });

            // ---- Add controls to tab Tre em ----
            this.tabTreEm.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.chkTsTiepxucLao, this.lblChieudaiTuoiSd, this.spChieudaiTuoiSd,
                this.lblCannangTuoiSd, this.spCannangTuoiSd,
                this.lblTrangthaiVongdau, this.comboTrangthaiVongdau,
                this.lblChuviVongcanhtay, this.spChuviVongcanhtay,
                this.lblTinhtrangDinhduong, this.txtTinhtrangDinhduong,
                this.chkPhattrienTinhthan, this.chkPhattrienVandong, this.chkNguycoTuky,
                this.chkBenhLao, this.chkTiemVgbMui1, this.chkTiemchungDaydu,
                this.lblMausacDa, this.comboMausacDa, this.chkLongBantay,
                this.lblThop, this.comboThop, this.lblHinhdangDau, this.comboHinhdangDau,
                this.lblVandongCo, this.comboVandongCo, this.chkKhoibatthuongDauco,
                this.lblVitriHaimat, this.comboVitriHaimat, this.lblMimatKetmac, this.comboMimatKetmac,
                this.lblDongtu, this.comboDongtu, this.chkLacmat,
                this.lblTaiMangnhi, this.comboTaiMangnhi, this.lblDapungAmthanh, this.comboDapungAmthanh,
                this.chkKhoisungSautai, this.chkChaymuNuoctai,
                this.lblHinhdangMui, this.comboHinhdangMui, this.chkChaynuocMui, this.chkNghetMui,
                this.lblHong, this.comboHong, this.lblHinhdangMieng, this.comboHinhdangMieng,
                this.chkRangsuaSosinh, this.lblHinhdangLuoi, this.comboHinhdangLuoi,
                this.chkDinhThangluoi, this.chkNamMieng, this.chkCamTut, this.chkVetsauRang,
                this.lblNhipthoKhongdeu, this.comboNhipthoKhongdeu, this.chkThorutlomLongnguc,
                this.chkTiengthoBatthuong, this.chkDauhieuSuyhohap,
                this.lblNghePhoi, this.comboNghePhoi,
                this.lblVitriMomtim, this.comboVitriMomtim, this.lblMachNgoaivi, this.comboMachNgoaivi,
                this.chkNgheTim,
                this.lblHinhdangBungRon, this.comboHinhdangBungRon, this.chkGanLachTo, this.chkKhoibatthuongBung,
                this.lblLoHaumon, this.comboLoHaumon, this.lblCqsdNgoai, this.comboCqsdNgoai,
                this.chkVandongKhongdoixung, this.chkPhanxaBu, this.chkPhanxaNam, this.chkPhanxaMoro,
                this.lblTruonglucCo, this.comboTruonglucCo, this.lblKhopHang, this.comboKhopHang,
                this.lblPhanxaCo, this.comboPhanxaCo, this.lblLungCotsong, this.comboLungCotsong,
                this.lblTuchiKhop, this.comboTuchiKhop, this.lblDangDi, this.comboDangDi,
                this.lblDoituong, this.txtDoituong, this.chkSinhNon,
                this.lblTuanthaiKhiSinh, this.txtTuanthaiKhiSinh, this.lblNguonChitra, this.txtNguonChitra,
                this.lblTrangthaiNhietdo, this.comboTrangthaiNhietdo,
                this.lblTrangthaiMach, this.comboTrangthaiMach,
                this.lblTrangthaiNhiptho, this.comboTrangthaiNhiptho,
                this.lblKetluanBinhthuong, this.memKetluanBinhthuong,
                this.lblKetluanNguycoLao, this.memKetluanNguycoLao,
                this.lblKetluanVandesuckhoe, this.memKetluanVandesuckhoe,
                this.lblKetluanGhiro, this.memKetluanGhiro
            });

            // ---- Add controls to tab 6-18t ----
            this.tab6_18.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblMaIcdTsgdBamsinh, this.txtMaIcdTsgdBamsinh,
                this.lblMaIcdTsBamsinh, this.txtMaIcdTsBamsinh,
                this.chkSanKhoa, this.lblSanKhoaKhongBt, this.comboSanKhoaKhongBt,
                this.lblMaBenhSanKhoaKhongBt, this.txtMaBenhSanKhoaKhongBt
            });

            // ---- Add controls to tab >=18t ----
            this.tab18.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.chkBenh5nam, this.chkBenhThankinh, this.chkBenhMat, this.chkBenhTai, this.chkBenhTim,
                this.chkPhauthuatTimmach, this.chkTangHuyetap, this.chkKhoTho, this.chkBenhPhoi,
                this.chkBenhThanLocmau, this.chkNghienRuoubia, this.chkDaiThaoduong,
                this.chkBenhTamthan, this.chkRoiLoanYthuc, this.chkNgatChongmat,
                this.chkBenhTieuhoa, this.chkRoiLoanGiacngu, this.chkTaibienLiet,
                this.chkBenhCotsong, this.chkSudungRuou, this.chkSudungMatuy,
                this.chkBenhKhac, this.lblTenBenhKhac, this.txtTenBenhKhac,
                this.lblClsHuyethoc, this.txtClsHuyethoc, this.lblClsDuongmau, this.txtClsDuongmau,
                this.lblClsUre, this.txtClsUre, this.lblClsCreatinin, this.txtClsCreatinin,
                this.lblClsAsat, this.txtClsAsat, this.lblClsAlat, this.txtClsAlat,
                this.lblClsTongphantichNt, this.txtClsTongphantichNt,
                this.lblClsNtKhac, this.txtClsNtKhac,
                this.lblClsXqTimphoi, this.txtClsXqTimphoi,
                this.chkClsKhac, this.lblClsKhacNoidung, this.txtClsKhacNoidung
            });

            // ---- Add controls to tab Tam than ----
            this.tabTamThan.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.chkTsTamthanGiadinh, this.lblTenTamthanGiadinh, this.txtTenTamthanGiadinh,
                this.chkTsTamthanBanthan, this.lblTenTamthanBanthan, this.txtTenTamthanBanthan,
                this.lblTamthanBieuhien, this.memTamthanBieuhien,
                this.lblTamthanYthuc, this.memTamthanYthuc,
                this.lblTamthanDinhhuongKhonggian, this.txtTamthanDinhhuongKhonggian,
                this.lblTamthanDinhhuongThoigian, this.txtTamthanDinhhuongThoigian,
                this.lblTamthanDinhhuongXungquanh, this.txtTamthanDinhhuongXungquanh,
                this.lblTamthanDinhhuongBanthan, this.txtTamthanDinhhuongBanthan,
                this.lblTamthanCamxuc, this.memTamthanCamxuc,
                this.lblTamthanCamgiacTrigiac, this.memTamthanCamgiacTrigiac,
                this.lblTamthanTuduyHinhthuc, this.memTamthanTuduyHinhthuc,
                this.lblTamthanTuduyNoidung, this.memTamthanTuduyNoidung,
                this.lblTamthanHanhviYchi, this.memTamthanHanhviYchi,
                this.lblTamthanHanhviBannang, this.memTamthanHanhviBannang,
                this.lblTamthanTrinho, this.memTamthanTrinho,
                this.lblTamthanTritue, this.memTamthanTritue,
                this.lblTamthanChuy, this.memTamthanChuy,
                this.lblTamthanKhac, this.memTamthanKhac,
                this.lblTracnghiemTamly, this.txtTracnghiemTamly,
                this.lblCanlamsangKhacBs, this.txtCanlamsangKhacBs
            });

            // ---- Form ----
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 640);
            this.Controls.Add(this.tabKsk);
            this.Name = "KskPhieuForm";
            this.Text = "Phieu KSK (TT 25/2026/TT-BYT)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
