using System;
using CrudFramework.Core.Attributes;
using CrudFramework.Core.Entities;

namespace CrudFramework.Sample
{
    /// <summary>
    /// Entity demo phieu Kham suc khoe (KSK) theo Thong tu 25/2026/TT-BYT.
    /// Gom 4 loai mau: Tre em &lt;6 tuoi (loai=1), 6-18 tuoi (loai=2), &gt;=18 tuoi (loai=3), Tam than (loai=4).
    /// Dung chung 1 bang ksk_phieu, loai_mau_ksk de form show/hide tab con.
    /// </summary>
    [DbTable("ksk_phieu", FunctionPrefix = "fn_")]
    public class KskPhieu : EntityBase
    {
        // ---- Cot chung (id, benhnhan_id, loai_mau_ksk, ngay_ksk...) ----

        private int _id;
        private int _benhnhanId;
        private int _loaiMauKsk;
        private DateTime? _ngayKsk;
        private string _cosoKham;
        private string _lydoKsk;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        /// <summary>Mau phieu (PK, chi doc).</summary>
        [DbColumn("id", Caption = "Mau phieu", Width = 60, ReadOnly = true, Order = 1)]
        public int Id { get { return _id; } set { SetField(ref _id, value); } }

        /// <summary>Ma benh nhan (FK ksk_benhnhan).</summary>
        [DbColumn("benhnhan_id", Caption = "Ma BN", Width = 80, HiddenInGrid = true, Order = 2)]
        public int BenhnhanId { get { return _benhnhanId; } set { SetField(ref _benhnhanId, value); } }

        /// <summary>Loai mau KSK dang ap dung. 1:Tre em &lt;6t; 2:6-18t; 3:&gt;=18t; 4:Tam than.</summary>
        /// <remarks>Dung de form show/hide tab con theo loai mau.</remarks>
        [DbColumn("loai_mau_ksk", Caption = "Loai mau KSK", Width = 120, Order = 3)]
        public int LoaiMauKsk { get { return _loaiMauKsk; } set { SetField(ref _loaiMauKsk, value); } }

        /// <summary>Ngay kham suc khoe.</summary>
        [DbColumn("ngay_ksk", Caption = "Ngay KSK", Width = 110, Format = "dd/MM/yyyy", Order = 4)]
        public DateTime? NgayKsk { get { return _ngayKsk; } set { SetField(ref _ngayKsk, value); } }

        /// <summary>Co so kham suc khoe.</summary>
        [DbColumn("coso_kham", Caption = "Co so kham", Width = 200, Order = 5)]
        public string CosoKham { get { return _cosoKham; } set { SetField(ref _cosoKham, value); } }

        /// <summary>Ly do kham suc khoe.</summary>
        [DbColumn("lydo_ksk", Caption = "Ly do KSK", Width = 200, Order = 6)]
        public string LydoKsk { get { return _lydoKsk; } set { SetField(ref _lydoKsk, value); } }

        [DbColumn("created_at", Caption = "Tao luc", Width = 110, ReadOnly = true, Format = "dd/MM/yyyy HH:mm", Order = 200)]
        public DateTime CreatedAt { get { return _createdAt; } set { SetField(ref _createdAt, value); } }

        [DbColumn("updated_at", Caption = "Cap nhat", Width = 110, ReadOnly = true, Format = "dd/MM/yyyy HH:mm", Order = 201)]
        public DateTime UpdatedAt { get { return _updatedAt; } set { SetField(ref _updatedAt, value); } }

        // ---- Thong tin benh nhan (tu ksk_benhnhan — flat de bind truc tiep) ----

        private string _holot;
        private string _ten;
        private DateTime? _ngaysinh;
        private int _gioitinh;
        private string _cmnd;
        private DateTime? _ngaycap;
        private string _noicap;
        private string _diachi;

        /// <summary>Ho lot benh nhan.</summary>
        [DbColumn("holot", Caption = "Ho lot", Width = 120, Order = 10)]
        public string Holot { get { return _holot; } set { SetField(ref _holot, value); } }

        /// <summary>Ten benh nhan.</summary>
        [DbColumn("ten", Caption = "Ten", Width = 100, Order = 11)]
        public string Ten { get { return _ten; } set { SetField(ref _ten, value); } }

        /// <summary>Ngay sinh.</summary>
        [DbColumn("ngaysinh", Caption = "Ngay sinh", Width = 110, Format = "dd/MM/yyyy", Order = 12)]
        public DateTime? Ngaysinh { get { return _ngaysinh; } set { SetField(ref _ngaysinh, value); } }

        /// <summary>Gioi tinh. 1:Nam; 0:Nu.</summary>
        [DbColumn("gioitinh", Caption = "Gioi tinh", Width = 80, Order = 13)]
        public int Gioitinh { get { return _gioitinh; } set { SetField(ref _gioitinh, value); } }

        /// <summary>So CCCD/CMND.</summary>
        [DbColumn("cmnd", Caption = "CCCD", Width = 100, Order = 14)]
        public string Cmnd { get { return _cmnd; } set { SetField(ref _cmnd, value); } }

        /// <summary>Ngay cap CCCD.</summary>
        [DbColumn("ngaycap", Caption = "Ngay cap", Width = 110, Format = "dd/MM/yyyy", Order = 15)]
        public DateTime? Ngaycap { get { return _ngaycap; } set { SetField(ref _ngaycap, value); } }

        /// <summary>Noi cap CCCD.</summary>
        [DbColumn("noicap", Caption = "Noi cap", Width = 120, Order = 16)]
        public string Noicap { get { return _noicap; } set { SetField(ref _noicap, value); } }

        /// <summary>Dia chi.</summary>
        [DbColumn("diachi", Caption = "Dia chi", Width = 220, Order = 17)]
        public string Diachi { get { return _diachi; } set { SetField(ref _diachi, value); } }

        // ---- Nguoi giam ho ----

        private string _hotenQh;
        private int _loaiqh;
        private string _dienthoaiQh;
        private string _cmndQh;

        /// <summary>Ho ten nguoi giam ho.</summary>
        [DbColumn("hoten_qh", Caption = "Nguoi giam ho", Width = 160, Order = 20)]
        public string HotenQh { get { return _hotenQh; } set { SetField(ref _hotenQh, value); } }

        /// <summary>Loai quan he nguoi giam ho. 0:Cha/me; 1:Vo/chong; 2:Anh chi em; 3:Khac.</summary>
        [DbColumn("loaiqh", Caption = "Loai QH", Width = 100, Order = 21)]
        public int Loaiqh { get { return _loaiqh; } set { SetField(ref _loaiqh, value); } }

        /// <summary>SDT nguoi giam ho.</summary>
        [DbColumn("dienthoai_qh", Caption = "SDT QH", Width = 100, Order = 22)]
        public string DienthoaiQh { get { return _dienthoaiQh; } set { SetField(ref _dienthoaiQh, value); } }

        /// <summary>Ma dinh danh nguoi giam ho.</summary>
        [DbColumn("cmnd_qh", Caption = "CCCD QH", Width = 100, Order = 23)]
        public string CmndQh { get { return _cmndQh; } set { SetField(ref _cmndQh, value); } }

        // ---- Sinh hieu ----

        private decimal _nhietdo;
        private decimal _mach;
        private decimal _nhiptho;
        private decimal _chieucao;
        private decimal _cannang;
        private string _huyetap;

        /// <summary>Nhiet do.</summary>
        [DbColumn("nhietdo", Caption = "Nhiet do", Width = 80, Format = "#,##0.0", Order = 30)]
        public decimal Nhietdo { get { return _nhietdo; } set { SetField(ref _nhietdo, value); } }

        /// <summary>Mach.</summary>
        [DbColumn("mach", Caption = "Mach", Width = 80, Format = "n0", Order = 31)]
        public decimal Mach { get { return _mach; } set { SetField(ref _mach, value); } }

        /// <summary>Nhip tho.</summary>
        [DbColumn("nhiptho", Caption = "Nhip tho", Width = 80, Format = "n0", Order = 32)]
        public decimal Nhiptho { get { return _nhiptho; } set { SetField(ref _nhiptho, value); } }

        /// <summary>Chieu cao (cm).</summary>
        [DbColumn("chieucao", Caption = "Chieu cao", Width = 80, Format = "#,##0.0", Order = 33)]
        public decimal Chieucao { get { return _chieucao; } set { SetField(ref _chieucao, value); } }

        /// <summary>Can nang (kg).</summary>
        [DbColumn("cannang", Caption = "Can nang", Width = 80, Format = "#,##0.0", Order = 34)]
        public decimal Cannang { get { return _cannang; } set { SetField(ref _cannang, value); } }

        /// <summary>Huyet ap (VD "120/80").</summary>
        [DbColumn("huyetap", Caption = "Huyet ap", Width = 80, Order = 35)]
        public string Huyetap { get { return _huyetap; } set { SetField(ref _huyetap, value); } }

        // ---- Tien su chung ----

        private int _tsbenhCoBenhbamsinh;
        private string _tsbenhTenbenh;
        private int _tsgdCoBenhTruyennhiem;
        private string _tsgdTenbenhTruyennhiem;
        private string _dangDtbenh;

        /// <summary>Tien su benh bam sinh (co/khong).</summary>
        [DbColumn("tsbenh_co_benhbamsinh", Caption = "TS bam sinh", Width = 80, Order = 40)]
        public int TsbenhCoBenhbamsinh { get { return _tsbenhCoBenhbamsinh; } set { SetField(ref _tsbenhCoBenhbamsinh, value); } }

        /// <summary>Ten benh bam sinh.</summary>
        [DbColumn("tsbenh_tenbenh", Caption = "Ten TS bam sinh", Width = 200, Order = 41)]
        public string TsbenhTenbenh { get { return _tsbenhTenbenh; } set { SetField(ref _tsbenhTenbenh, value); } }

        /// <summary>Tien su GD benh truyen nhiem (co/khong).</summary>
        [DbColumn("tsgd_co_benh_truyennhiem", Caption = "TS truyen nhiem", Width = 80, Order = 42)]
        public int TsgdCoBenhTruyennhiem { get { return _tsgdCoBenhTruyennhiem; } set { SetField(ref _tsgdCoBenhTruyennhiem, value); } }

        /// <summary>Ten benh truyen nhiem GD.</summary>
        [DbColumn("tsgd_tenbenh_truyennhiem", Caption = "Ten TS truyen nhiem", Width = 200, Order = 43)]
        public string TsgdTenbenhTruyennhiem { get { return _tsgdTenbenhTruyennhiem; } set { SetField(ref _tsgdTenbenhTruyennhiem, value); } }

        /// <summary>Hien tai dang dieu tri benh.</summary>
        [DbColumn("dang_dtbenh", Caption = "Dang DT benh", Width = 200, Order = 44)]
        public string DangDtbenh { get { return _dangDtbenh; } set { SetField(ref _dangDtbenh, value); } }

        // ---- Kham lam sang chung ----

        private string _tuanhoanKsk;
        private string _hohapKsk;
        private string _tieuhoaKsk;
        private string _thanKsk;
        private string _thankinhKsk;
        private string _tamthanKsk;
        private string _khamlsKhacKsk;

        /// <summary>Tuan hoan.</summary>
        [DbColumn("tuanhoan_ksk", Caption = "Tuan hoan", Width = 200, Order = 50)]
        public string TuanhoanKsk { get { return _tuanhoanKsk; } set { SetField(ref _tuanhoanKsk, value); } }

        /// <summary>Ho hap.</summary>
        [DbColumn("hohap_ksk", Caption = "Ho hap", Width = 200, Order = 51)]
        public string HohapKsk { get { return _hohapKsk; } set { SetField(ref _hohapKsk, value); } }

        /// <summary>Tieu hoa.</summary>
        [DbColumn("tieuhoa_ksk", Caption = "Tieu hoa", Width = 200, Order = 52)]
        public string TieuhoaKsk { get { return _tieuhoaKsk; } set { SetField(ref _tieuhoaKsk, value); } }

        /// <summary>Than - Tiet nieu - Sinh dung.</summary>
        [DbColumn("than_ksk", Caption = "Than-TN-SD", Width = 200, Order = 53)]
        public string ThanKsk { get { return _thanKsk; } set { SetField(ref _thanKsk, value); } }

        /// <summary>Than kinh.</summary>
        [DbColumn("thankinh_ksk", Caption = "Than kinh", Width = 200, Order = 54)]
        public string ThankinhKsk { get { return _thankinhKsk; } set { SetField(ref _thankinhKsk, value); } }

        /// <summary>Tam than.</summary>
        [DbColumn("tamthan_ksk", Caption = "Tam than", Width = 200, Order = 55)]
        public string TamthanKsk { get { return _tamthanKsk; } set { SetField(ref _tamthanKsk, value); } }

        /// <summary>Kham lam sang khac.</summary>
        [DbColumn("khamls_khac_ksk", Caption = "LS khac", Width = 200, Order = 56)]
        public string KhamlsKhacKsk { get { return _khamlsKhacKsk; } set { SetField(ref _khamlsKhacKsk, value); } }

        // ---- Mat chung ----

        private decimal _kmatphaiKsk;
        private decimal _kmattraiKsk;
        private decimal _matphaiKsk;
        private decimal _mattraiKsk;

        /// <summary>Mat khong kinh - mat phai.</summary>
        [DbColumn("kmatphai_ksk", Caption = "K kinh (P)", Width = 80, Format = "#,##0.0", Order = 60)]
        public decimal KmatphaiKsk { get { return _kmatphaiKsk; } set { SetField(ref _kmatphaiKsk, value); } }

        /// <summary>Mat khong kinh - mat trai.</summary>
        [DbColumn("kmattrai_ksk", Caption = "K kinh (T)", Width = 80, Format = "#,##0.0", Order = 61)]
        public decimal KmattraiKsk { get { return _kmattraiKsk; } set { SetField(ref _kmattraiKsk, value); } }

        /// <summary>Mat co kinh - mat phai.</summary>
        [DbColumn("matphai_ksk", Caption = "Co kinh (P)", Width = 80, Format = "#,##0.0", Order = 62)]
        public decimal MatphaiKsk { get { return _matphaiKsk; } set { SetField(ref _matphaiKsk, value); } }

        /// <summary>Mat co kinh - mat trai.</summary>
        [DbColumn("mattrai_ksk", Caption = "Co kinh (T)", Width = 80, Format = "#,##0.0", Order = 63)]
        public decimal MattraiKsk { get { return _mattraiKsk; } set { SetField(ref _mattraiKsk, value); } }

        // ---- Tai chung ----

        private string _tnoithuongKsk;
        private string _tnoithamKsk;
        private string _noithuongKsk;
        private string _noithamKsk;

        /// <summary>Tai noi thuong - phai.</summary>
        [DbColumn("tnoithuong_ksk", Caption = "Tai noi (P)", Width = 80, Order = 64)]
        public string TnoithuongKsk { get { return _tnoithuongKsk; } set { SetField(ref _tnoithuongKsk, value); } }

        /// <summary>Tai noi tham - phai.</summary>
        [DbColumn("tnoitham_ksk", Caption = "Tai noi (T-P)", Width = 80, Order = 65)]
        public string TnoithamKsk { get { return _tnoithamKsk; } set { SetField(ref _tnoithamKsk, value); } }

        /// <summary>Noi thuong - phai.</summary>
        [DbColumn("noithuong_ksk", Caption = "Noi (P)", Width = 80, Order = 66)]
        public string NoithuongKsk { get { return _noithuongKsk; } set { SetField(ref _noithuongKsk, value); } }

        /// <summary>Noi tham - phai.</summary>
        [DbColumn("noitham_ksk", Caption = "Noi (T-P)", Width = 80, Order = 67)]
        public string NoithamKsk { get { return _noithamKsk; } set { SetField(ref _noithamKsk, value); } }

        // ---- Rang ham mat chung ----

        private string _hamtrenKsk;
        private string _hamduoiKsk;
        private int _rhmbenhKsk;

        /// <summary>Rang ham tren.</summary>
        [DbColumn("hamtren_ksk", Caption = "Ham tren", Width = 100, Order = 68)]
        public string HamtrenKsk { get { return _hamtrenKsk; } set { SetField(ref _hamtrenKsk, value); } }

        /// <summary>Rang ham duoi.</summary>
        [DbColumn("hamduoi_ksk", Caption = "Ham duoi", Width = 100, Order = 69)]
        public string HamduoiKsk { get { return _hamduoiKsk; } set { SetField(ref _hamduoiKsk, value); } }

        /// <summary>Co benh RHM. 1:Co; 0:Khong.</summary>
        [DbColumn("rhmbenh_ksk", Caption = "Benh RHM", Width = 80, Order = 70)]
        public int RhmbenhKsk { get { return _rhmbenhKsk; } set { SetField(ref _rhmbenhKsk, value); } }

        // ---- Ket qua CLS chung ----

        private string _kqclsKsk;

        /// <summary>Ket qua can lam sang.</summary>
        [DbColumn("kqcls_ksk", Caption = "KQ CLS", Width = 200, Order = 71)]
        public string KqclsKsk { get { return _kqclsKsk; } set { SetField(ref _kqclsKsk, value); } }

        // ---- Ngoai khoa / Da lieu / San phu khoa chung ----

        private string _ngoaikhoaKsk;
        private string _plngoaikhoaKsk;
        private string _dalieuKsk;
        private string _pldalieuKsk;
        private string _sanphukhoaKsk;
        private string _plsanphukhoaKsk;

        /// <summary>Ngoai khoa.</summary>
        [DbColumn("ngoaikhoa_ksk", Caption = "Ngoai khoa", Width = 200, Order = 72)]
        public string NgoaikhoaKsk { get { return _ngoaikhoaKsk; } set { SetField(ref _ngoaikhoaKsk, value); } }

        /// <summary>Phan loai ngoai khoa.</summary>
        [DbColumn("plngoaikhoa_ksk", Caption = "PL ngoai khoa", Width = 200, Order = 73)]
        public string PlngoaikhoaKsk { get { return _plngoaikhoaKsk; } set { SetField(ref _plngoaikhoaKsk, value); } }

        /// <summary>Da lieu.</summary>
        [DbColumn("dalieu_ksk", Caption = "Da lieu", Width = 200, Order = 74)]
        public string DalieuKsk { get { return _dalieuKsk; } set { SetField(ref _dalieuKsk, value); } }

        /// <summary>Phan loai da lieu.</summary>
        [DbColumn("pldalieu_ksk", Caption = "PL da lieu", Width = 200, Order = 75)]
        public string PldalieuKsk { get { return _pldalieuKsk; } set { SetField(ref _pldalieuKsk, value); } }

        /// <summary>San phu khoa.</summary>
        [DbColumn("sanphukhoa_ksk", Caption = "San phu khoa", Width = 200, Order = 76)]
        public string SanphukhoaKsk { get { return _sanphukhoaKsk; } set { SetField(ref _sanphukhoaKsk, value); } }

        /// <summary>Phan loai san phu khoa.</summary>
        [DbColumn("plsanphukhoa_ksk", Caption = "PL san phu khoa", Width = 200, Order = 77)]
        public string PlsanphukhoaKsk { get { return _plsanphukhoaKsk; } set { SetField(ref _plsanphukhoaKsk, value); } }

        // ============================================================
        // ---- Tre em <6 tuoi ----
        // ============================================================

        private int _tsTiepxucLao;
        private decimal _chieudaiTuoiSd;
        private decimal _cannangTuoiSd;
        private int _trangthaiVongdau;
        private decimal _chuviVongcanhtay;
        private string _tinhtrangDinhduong;
        private int _phattrienTinhthan;
        private int _phattrienVandong;
        private int _nguycoTuky;
        private int _benhLao;
        private int _tiemVgbMui1;
        private int _tiemchungDaydu;

        /// <summary>Tien su tiep xuc nguoi benh lao. 1:Co; 0:Khong.</summary>
        [DbColumn("ts_tiepxuc_lao", Caption = "TS tiep xuc lao", Width = 80, Order = 100)]
        public int TsTiepxucLao { get { return _tsTiepxucLao; } set { SetField(ref _tsTiepxucLao, value); } }

        /// <summary>Chieu dai theo tuoi (SD).</summary>
        [DbColumn("chieudai_tuoi_sd", Caption = "CD/tuoi (SD)", Width = 80, Format = "#,##0.00", Order = 101)]
        public decimal ChieudaiTuoiSd { get { return _chieudaiTuoiSd; } set { SetField(ref _chieudaiTuoiSd, value); } }

        /// <summary>Can nang theo tuoi (SD).</summary>
        [DbColumn("cannang_tuoi_sd", Caption = "CN/tuoi (SD)", Width = 80, Format = "#,##0.00", Order = 102)]
        public decimal CannangTuoiSd { get { return _cannangTuoiSd; } set { SetField(ref _cannangTuoiSd, value); } }

        /// <summary>Trang thai vong dau. 0:BT; 1:Dau to; 2:Dau nho.</summary>
        [DbColumn("trangthai_vongdau", Caption = "Vong dau", Width = 80, Order = 103)]
        public int TrangthaiVongdau { get { return _trangthaiVongdau; } set { SetField(ref _trangthaiVongdau, value); } }

        /// <summary>Chu vi vong canh tay (mm).</summary>
        [DbColumn("chuvi_vongcanhtay", Caption = "CV canh tay", Width = 80, Format = "#,##0.00", Order = 104)]
        public decimal ChuviVongcanhtay { get { return _chuviVongcanhtay; } set { SetField(ref _chuviVongcanhtay, value); } }

        /// <summary>Tinh trang dinh duong. 0:BT; 1:Phu; 2:Thieu mau; 3:Coi; 4:Suy DD; 5:Thua can.</summary>
        [DbColumn("tinhtrang_dinhduong", Caption = "Dinh duong", Width = 120, Order = 105)]
        public string TinhtrangDinhduong { get { return _tinhtrangDinhduong; } set { SetField(ref _tinhtrangDinhduong, value); } }

        /// <summary>Phat trien tinh than theo do tuoi. 1:Co; 0:Khong.</summary>
        [DbColumn("phattrien_tinhthan", Caption = "PT tinh than", Width = 80, Order = 106)]
        public int PhattrienTinhthan { get { return _phattrienTinhthan; } set { SetField(ref _phattrienTinhthan, value); } }

        /// <summary>Phat trien van dong theo do tuoi. 1:Co; 0:Khong.</summary>
        [DbColumn("phattrien_vandong", Caption = "PT van dong", Width = 80, Order = 107)]
        public int PhattrienVandong { get { return _phattrienVandong; } set { SetField(ref _phattrienVandong, value); } }

        /// <summary>Nguy co tu ky. 1:Co; 0:Khong.</summary>
        [DbColumn("nguyco_tuky", Caption = "Nguy co tu ky", Width = 80, Order = 108)]
        public int NguycoTuky { get { return _nguycoTuky; } set { SetField(ref _nguycoTuky, value); } }

        /// <summary>Lao. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_lao", Caption = "Lao", Width = 80, Order = 109)]
        public int BenhLao { get { return _benhLao; } set { SetField(ref _benhLao, value); } }

        /// <summary>Tiem VGB mui 1. 1:Co; 0:Khong.</summary>
        [DbColumn("tiem_vgb_mui1", Caption = "Tiem VGB m1", Width = 80, Order = 110)]
        public int TiemVgbMui1 { get { return _tiemVgbMui1; } set { SetField(ref _tiemVgbMui1, value); } }

        /// <summary>Tiem chung day du. 1:Co; 0:Khong.</summary>
        [DbColumn("tiemchung_daydu", Caption = "Tiem chung DD", Width = 80, Order = 111)]
        public int TiemchungDaydu { get { return _tiemchungDaydu; } set { SetField(ref _tiemchungDaydu, value); } }

        // ---- Tre em <6t: Da - Dau - Co ----

        private int _mausacDa;
        private int _longBantay;
        private int _thop;
        private int _hinhdangDau;
        private int _vandongCo;
        private int _khoibatthuongDauco;

        /// <summary>Mau sac da. 0:Hong hao; 1:Nhot; 2:Tim; 3:Vang; 4:Sam.</summary>
        [DbColumn("mausac_da", Caption = "Mau sac da", Width = 80, Order = 112)]
        public int MausacDa { get { return _mausacDa; } set { SetField(ref _mausacDa, value); } }

        /// <summary>Long ban tay. 1:BT; 0:Khong BT.</summary>
        [DbColumn("long_bantay", Caption = "Long ban tay", Width = 80, Order = 113)]
        public int LongBantay { get { return _longBantay; } set { SetField(ref _longBantay, value); } }

        /// <summary>Thop. 0:BT; 1:Rong; 2:Hep; 3:Phong.</summary>
        [DbColumn("thop", Caption = "Thop", Width = 80, Order = 114)]
        public int Thop { get { return _thop; } set { SetField(ref _thop, value); } }

        /// <summary>Kich thuoc va hinh dang dau. 1:BT; 0:Khong BT.</summary>
        [DbColumn("hinhdang_dau", Caption = "Hinh dang dau", Width = 80, Order = 115)]
        public int HinhdangDau { get { return _hinhdangDau; } set { SetField(ref _hinhdangDau, value); } }

        /// <summary>Van dong co. 0:BT; 1:Gioi han.</summary>
        [DbColumn("vandong_co", Caption = "VD co", Width = 80, Order = 116)]
        public int VandongCo { get { return _vandongCo; } set { SetField(ref _vandongCo, value); } }

        /// <summary>Khoi bat thuong vung dau co. 1:Co; 0:Khong.</summary>
        [DbColumn("khoibatthuong_dauco", Caption = "Khoi BT dau-co", Width = 80, Order = 117)]
        public int KhoibatthuongDauco { get { return _khoibatthuongDauco; } set { SetField(ref _khoibatthuongDauco, value); } }

        // ---- Tre em <6t: Mat ----

        private int _vitriHaimat;
        private int _mimatKetmac;
        private int _dongtu;
        private int _lacmat;

        /// <summary>Vi tri hai mat. 0:BT; 1:Xa nhau.</summary>
        [DbColumn("vitri_haimat", Caption = "Vi tri 2 mat", Width = 80, Order = 118)]
        public int VitriHaimat { get { return _vitriHaimat; } set { SetField(ref _vitriHaimat, value); } }

        /// <summary>Mi mat va ket mac. 0:BT; 1:Swung do; 2:Chay ghenn.</summary>
        [DbColumn("mimat_ketmac", Caption = "Mi mat-KM", Width = 80, Order = 119)]
        public int MimatKetmac { get { return _mimatKetmac; } set { SetField(ref _mimatKetmac, value); } }

        /// <summary>Dong tu. 1:BT; 0:Khong BT.</summary>
        [DbColumn("dongtu", Caption = "Dong tu", Width = 80, Order = 120)]
        public int Dongtu { get { return _dongtu; } set { SetField(ref _dongtu, value); } }

        /// <summary>Lac mat. 1:Co; 0:Khong.</summary>
        [DbColumn("lacmat", Caption = "Lac mat", Width = 80, Order = 121)]
        public int Lacmat { get { return _lacmat; } set { SetField(ref _lacmat, value); } }

        // ---- Tre em <6t: Tai ----

        private int _taiMangnhi;
        private int _dapungAmthanh;
        private int _khoisungSautai;
        private int _chaymuNuoctai;

        /// <summary>Tai va mang nhi. 1:BT; 0:Khong BT.</summary>
        [DbColumn("tai_mangnhi", Caption = "Tai-mang nhi", Width = 80, Order = 122)]
        public int TaiMangnhi { get { return _taiMangnhi; } set { SetField(ref _taiMangnhi, value); } }

        /// <summary>Dap ung am thanh. 1:BT; 0:Khong BT.</summary>
        [DbColumn("dapung_amthanh", Caption = "Dap ung AT", Width = 80, Order = 123)]
        public int DapungAmthanh { get { return _dapungAmthanh; } set { SetField(ref _dapungAmthanh, value); } }

        /// <summary>Khoi sung sau tai. 1:Co; 0:Khong.</summary>
        [DbColumn("khoisung_sautai", Caption = "Khoi sung tai", Width = 80, Order = 124)]
        public int KhoisungSautai { get { return _khoisungSautai; } set { SetField(ref _khoisungSautai, value); } }

        /// <summary>Dau hieu chay mu, nuoc tai. 1:Co; 0:Khong.</summary>
        [DbColumn("chaymu_nuoctai", Caption = "Chay mu tai", Width = 80, Order = 125)]
        public int ChaymuNuoctai { get { return _chaymuNuoctai; } set { SetField(ref _chaymuNuoctai, value); } }

        // ---- Tre em <6t:Mui ----

        private int _hinhdangMui;
        private int _chaynuocMui;
        private int _nghetMui;
        private int _hong;

        /// <summary>Hinh dang mui. 0:BT; 1:To day; 2:Bat san.</summary>
        [DbColumn("hinhdang_mui", Caption = "Hinh dang mui", Width = 80, Order = 126)]
        public int HinhdangMui { get { return _hinhdangMui; } set { SetField(ref _hinhdangMui, value); } }

        /// <summary>Chay nuoc mui. 1:Co; 0:Khong.</summary>
        [DbColumn("chaynuoc_mui", Caption = "Chay NM", Width = 80, Order = 127)]
        public int ChaynuocMui { get { return _chaynuocMui; } set { SetField(ref _chaynuocMui, value); } }

        /// <summary>Nghet mui. 1:Co; 0:Khong.</summary>
        [DbColumn("nghet_mui", Caption = "Nghet mui", Width = 80, Order = 128)]
        public int NghetMui { get { return _nghetMui; } set { SetField(ref _nghetMui, value); } }

        /// <summary>Hong. 1:BT; 0:Khong BT.</summary>
        [DbColumn("hong", Caption = "Hong", Width = 80, Order = 129)]
        public int Hong { get { return _hong; } set { SetField(ref _hong, value); } }

        // ---- Tre em <6t: Mieng ----

        private int _hinhdangMieng;
        private int _rangsuaSosinh;
        private int _hinhdangLuoi;
        private int _dinhThangluoi;
        private int _namMieng;
        private int _camTut;
        private int _vetsauRang;

        /// <summary>Hinh dang mieng. 0:BT; 1:Sut moi che vom.</summary>
        [DbColumn("hinhdang_mieng", Caption = "Hinh dang mieng", Width = 80, Order = 130)]
        public int HinhdangMieng { get { return _hinhdangMieng; } set { SetField(ref _hinhdangMieng, value); } }

        /// <summary>Rang sua so sinh. 1:Co; 0:Khong.</summary>
        [DbColumn("rangsua_sosinh", Caption = "Rang sua", Width = 80, Order = 131)]
        public int RangsuaSosinh { get { return _rangsuaSosinh; } set { SetField(ref _rangsuaSosinh, value); } }

        /// <summary>Hinh dang luoi. 0:BT; 1:To be.</summary>
        [DbColumn("hinhdang_luoi", Caption = "Hinh dang luoi", Width = 80, Order = 132)]
        public int HinhdangLuoi { get { return _hinhdangLuoi; } set { SetField(ref _hinhdangLuoi, value); } }

        /// <summary>Dinh thang luoi. 1:Co; 0:Khong.</summary>
        [DbColumn("dinh_thangluoi", Caption = "Dinh thang luoi", Width = 80, Order = 133)]
        public int DinhThangluoi { get { return _dinhThangluoi; } set { SetField(ref _dinhThangluoi, value); } }

        /// <summary>Nam mieng. 1:Co; 0:Khong.</summary>
        [DbColumn("nam_mieng", Caption = "Nam mieng", Width = 80, Order = 134)]
        public int NamMieng { get { return _namMieng; } set { SetField(ref _namMieng, value); } }

        /// <summary>Cam nho, tut ve sau. 1:Co; 0:Khong.</summary>
        [DbColumn("cam_tut", Caption = "Cam nho tut", Width = 80, Order = 135)]
        public int CamTut { get { return _camTut; } set { SetField(ref _camTut, value); } }

        /// <summary>Vet sau, mang bam, lo tren rang. 1:Co; 0:Khong.</summary>
        [DbColumn("vetsau_rang", Caption = "Vet sau rang", Width = 80, Order = 136)]
        public int VetsauRang { get { return _vetsauRang; } set { SetField(ref _vetsauRang, value); } }

        // ---- Tre em <6t: Ho hap ----

        private int _nhipthoKhongdeu;
        private int _thorutlomLongnguc;
        private int _tiengthoBatthuong;
        private int _dauhieuSuyhohap;
        private int _nghePhoi;

        /// <summary>Nhip tho khong deu. 0:Khong; 1:Co con ngung tho >5s.</summary>
        [DbColumn("nhiptho_khongdeu", Caption = "NT khong deu", Width = 80, Order = 137)]
        public int NhipthoKhongdeu { get { return _nhipthoKhongdeu; } set { SetField(ref _nhipthoKhongdeu, value); } }

        /// <summary>Tho rut lom long nguc. 1:Co; 0:Khong.</summary>
        [DbColumn("thorutlom_longnguc", Caption = "Tho rut lom", Width = 80, Order = 138)]
        public int ThorutlomLongnguc { get { return _thorutlomLongnguc; } set { SetField(ref _thorutlomLongnguc, value); } }

        /// <summary>Tien tho bat thuong. 1:Co; 0:Khong.</summary>
        [DbColumn("tiengtho_batthuong", Caption = "Tien tho BT", Width = 80, Order = 139)]
        public int TiengthoBatthuong { get { return _tiengthoBatthuong; } set { SetField(ref _tiengthoBatthuong, value); } }

        /// <summary>Dau hieu suy ho hap. 1:Co; 0:Khong.</summary>
        [DbColumn("dauhieu_suyhohap", Caption = "Suy ho hap", Width = 80, Order = 140)]
        public int DauhieuSuyhohap { get { return _dauhieuSuyhohap; } set { SetField(ref _dauhieuSuyhohap, value); } }

        /// <summary>Nghe phoi. 1:BT; 0:Khong BT.</summary>
        [DbColumn("nghe_phoi", Caption = "Nghe phoi", Width = 80, Order = 141)]
        public int NghePhoi { get { return _nghePhoi; } set { SetField(ref _nghePhoi, value); } }

        // ---- Tre em <6t: Tim mach ----

        private int _vitriMomtim;
        private int _machNgoaivi;
        private int _ngheTim;

        /// <summary>Vi tri mom tim. 1:BT; 0:Khong BT.</summary>
        [DbColumn("vitri_momtim", Caption = "Mom tim", Width = 80, Order = 142)]
        public int VitriMomtim { get { return _vitriMomtim; } set { SetField(ref _vitriMomtim, value); } }

        /// <summary>Mach ngoai vi. 0:Bat ro; 1:Nhe; 2:Khong bat duoc.</summary>
        [DbColumn("mach_ngoaivi", Caption = "Mach ngoai vi", Width = 80, Order = 143)]
        public int MachNgoaivi { get { return _machNgoaivi; } set { SetField(ref _machNgoaivi, value); } }

        /// <summary>Nghe tim (loan nhip, tien thoi). 1:Co; 0:Khong.</summary>
        [DbColumn("nghe_tim", Caption = "Nghe tim", Width = 80, Order = 144)]
        public int NgheTim { get { return _ngheTim; } set { SetField(ref _ngheTim, value); } }

        // ---- Tre em <6t: Tieu hoa ----

        private int _hinhdangBungRon;
        private int _ganLachTo;
        private int _khoibatthuongBung;
        private int _loHaumon;
        private int _cqsdNgoai;

        /// <summary>Hinh dang bung, ron. 1:BT; 0:Khong BT.</summary>
        [DbColumn("hinhdang_bung_ron", Caption = "Bung-ron", Width = 80, Order = 145)]
        public int HinhdangBungRon { get { return _hinhdangBungRon; } set { SetField(ref _hinhdangBungRon, value); } }

        /// <summary>Gan, lach to. 1:Co; 0:Khong.</summary>
        [DbColumn("gan_lach_to", Caption = "Gan-lach to", Width = 80, Order = 146)]
        public int GanLachTo { get { return _ganLachTo; } set { SetField(ref _ganLachTo, value); } }

        /// <summary>Khoi bat thuong vung bung. 1:Co; 0:Khong.</summary>
        [DbColumn("khoibatthuong_bung", Caption = "Khoi BT bung", Width = 80, Order = 147)]
        public int KhoibatthuongBung { get { return _khoibatthuongBung; } set { SetField(ref _khoibatthuongBung, value); } }

        /// <summary>Lo hau mon. 1:BT; 0:Khong BT.</summary>
        [DbColumn("lo_haumon", Caption = "Lo hau mon", Width = 80, Order = 148)]
        public int LoHaumon { get { return _loHaumon; } set { SetField(ref _loHaumon, value); } }

        /// <summary>Co quan sinh dung ngoai. 1:BT; 0:Khong BT.</summary>
        [DbColumn("cqsd_ngoai", Caption = "CQSD ngoai", Width = 80, Order = 149)]
        public int CqsdNgoai { get { return _cqsdNgoai; } set { SetField(ref _cqsdNgoai, value); } }

        // ---- Tre em <6t: Than kinh ----

        private int _vandongKhongdoixung;
        private int _phanxaBu;
        private int _phanxaNam;
        private int _phanxaMoro;
        private int _truonglucCo;

        /// <summary>Van dong khong doi xung. 1:Co; 0:Khong.</summary>
        [DbColumn("vandong_khongdoixung", Caption = "VD khong DX", Width = 80, Order = 150)]
        public int VandongKhongdoixung { get { return _vandongKhongdoixung; } set { SetField(ref _vandongKhongdoixung, value); } }

        /// <summary>Phan xa bu. 1:Co; 0:Khong.</summary>
        [DbColumn("phanxa_bu", Caption = "Phan xa bu", Width = 80, Order = 151)]
        public int PhanxaBu { get { return _phanxaBu; } set { SetField(ref _phanxaBu, value); } }

        /// <summary>Phan xa nam. 1:Co; 0:Khong.</summary>
        [DbColumn("phanxa_nam", Caption = "Phan xa nam", Width = 80, Order = 152)]
        public int PhanxaNam { get { return _phanxaNam; } set { SetField(ref _phanxaNam, value); } }

        /// <summary>Phan xa Moro. 1:Co; 0:Khong.</summary>
        [DbColumn("phanxa_moro", Caption = "Phan xa Moro", Width = 80, Order = 153)]
        public int PhanxaMoro { get { return _phanxaMoro; } set { SetField(ref _phanxaMoro, value); } }

        /// <summary>Truong luc co. 0:BT; 1:Tang.</summary>
        [DbColumn("truongluc_co", Caption = "Truong luc co", Width = 80, Order = 154)]
        public int TruonglucCo { get { return _truonglucCo; } set { SetField(ref _truonglucCo, value); } }

        // ---- Tre em <6t: Co xuong khop ----

        private int _khopHang;
        private int _phanxaCo;
        private int _lungCotsong;
        private int _tuchiKhop;
        private int _dangDi;

        /// <summary>Khop hang. 0:BT; 1:Trat khop hang.</summary>
        [DbColumn("khop_hang", Caption = "Khop hang", Width = 80, Order = 155)]
        public int KhopHang { get { return _khopHang; } set { SetField(ref _khopHang, value); } }

        /// <summary>Phan xa co. 1:BT; 0:Khong BT.</summary>
        [DbColumn("phanxa_co", Caption = "Phan xa co", Width = 80, Order = 156)]
        public int PhanxaCo { get { return _phanxaCo; } set { SetField(ref _phanxaCo, value); } }

        /// <summary>Lung, cot song. 1:BT; 0:Khong BT.</summary>
        [DbColumn("lung_cotsong", Caption = "Lung-CS", Width = 80, Order = 157)]
        public int LungCotsong { get { return _lungCotsong; } set { SetField(ref _lungCotsong, value); } }

        /// <summary>Tu chi va khop. 1:BT; 0:Khong BT.</summary>
        [DbColumn("tuchi_khop", Caption = "Tu chi-khop", Width = 80, Order = 158)]
        public int TuchiKhop { get { return _tuchiKhop; } set { SetField(ref _tuchiKhop, value); } }

        /// <summary>Quan sat dang di. 1:BT; 0:Khong BT.</summary>
        [DbColumn("dang_di", Caption = "Dang di", Width = 80, Order = 159)]
        public int DangDi { get { return _dangDi; } set { SetField(ref _dangDi, value); } }

        // ---- Tre em <6t: Khac ----

        private string _doituong;
        private int _sinhNon;
        private string _tuanthaiKhiSinh;
        private string _nguonChitra;
        private int _trangthaiNhietdo;
        private int _trangthaiMach;
        private int _trangthaiNhiptho;
        private int _trangthaiHuyetap;
        private string _ketluanBinhthuong;
        private string _ketluanNguycoLao;
        private string _ketluanVandesuckhoe;
        private string _ketluanGhiro;

        /// <summary>Doi tuong.</summary>
        [DbColumn("doituong", Caption = "Doi tuong", Width = 120, Order = 160)]
        public string Doituong { get { return _doituong; } set { SetField(ref _doituong, value); } }

        /// <summary>Sinh non. 1:Co; 0:Khong.</summary>
        [DbColumn("sinh_non", Caption = "Sinh non", Width = 80, Order = 161)]
        public int SinhNon { get { return _sinhNon; } set { SetField(ref _sinhNon, value); } }

        /// <summary>Tuan thai khi sinh.</summary>
        [DbColumn("tuanthai_khi_sinh", Caption = "Tuan thai KS", Width = 80, Order = 162)]
        public string TuanthaiKhiSinh { get { return _tuanthaiKhiSinh; } set { SetField(ref _tuanthaiKhiSinh, value); } }

        /// <summary>Nguon chi tra.</summary>
        [DbColumn("nguon_chitra", Caption = "Nguon chi tra", Width = 120, Order = 163)]
        public string NguonChitra { get { return _nguonChitra; } set { SetField(ref _nguonChitra, value); } }

        /// <summary>Trang thai nhiet do. 0:BT; 1:Soat; 2:Ha than nhiet.</summary>
        [DbColumn("trangthai_nhietdo", Caption = "TT nhiet do", Width = 80, Order = 164)]
        public int TrangthaiNhietdo { get { return _trangthaiNhietdo; } set { SetField(ref _trangthaiNhietdo, value); } }

        /// <summary>Trang thai mach. 0:BT; 1:Nhanh.</summary>
        [DbColumn("trangthai_mach", Caption = "TT mach", Width = 80, Order = 165)]
        public int TrangthaiMach { get { return _trangthaiMach; } set { SetField(ref _trangthaiMach, value); } }

        /// <summary>Trang thai nhip tho. 0:BT; 1:Nhanh; 2:Cham.</summary>
        [DbColumn("trangthai_nhiptho", Caption = "TT nhip tho", Width = 80, Order = 166)]
        public int TrangthaiNhiptho { get { return _trangthaiNhiptho; } set { SetField(ref _trangthaiNhiptho, value); } }

        /// <summary>Trang thai huyet ap.</summary>
        [DbColumn("trangthai_huyetap", Caption = "TT huyet ap", Width = 80, Order = 167)]
        public int TrangthaiHuyetap { get { return _trangthaiHuyetap; } set { SetField(ref _trangthaiHuyetap, value); } }

        /// <summary>Ket luan: Binh thuong.</summary>
        [DbColumn("ketluan_binhthuong", Caption = "KL: BT", Width = 200, Order = 168)]
        public string KetluanBinhthuong { get { return _ketluanBinhthuong; } set { SetField(ref _ketluanBinhthuong, value); } }

        /// <summary>Ket luan: Co nguy co mac Lao.</summary>
        [DbColumn("ketluan_nguyco_lao", Caption = "KL: Nguy co lao", Width = 200, Order = 169)]
        public string KetluanNguycoLao { get { return _ketluanNguycoLao; } set { SetField(ref _ketluanNguycoLao, value); } }

        /// <summary>Ket luan: Co van de suc khoe.</summary>
        [DbColumn("ketluan_vandesuckhoe", Caption = "KL: Van de SK", Width = 200, Order = 170)]
        public string KetluanVandesuckhoe { get { return _ketluanVandesuckhoe; } set { SetField(ref _ketluanVandesuckhoe, value); } }

        /// <summary>Ket luan: Ghi ro.</summary>
        [DbColumn("ketluan_ghiro", Caption = "KL: Ghi ro", Width = 200, Order = 171)]
        public string KetluanGhiro { get { return _ketluanGhiro; } set { SetField(ref _ketluanGhiro, value); } }

        // ============================================================
        // ---- 6-18 tuoi ----
        // ============================================================

        private string _maIcdTsgdBamsinh;
        private string _maIcdTsBamsinh;
        private int _sanKhoa;
        private int _sanKhoaKhongBt;
        private string _maBenhSanKhoaKhongBt;

        /// <summary>Ma ICD-10 tien su benh GD bam sinh.</summary>
        [DbColumn("ma_icd_tsgd_bamsinh", Caption = "ICD-10 TS GD bam sinh", Width = 120, Order = 300)]
        public string MaIcdTsgdBamsinh { get { return _maIcdTsgdBamsinh; } set { SetField(ref _maIcdTsgdBamsinh, value); } }

        /// <summary>Ma ICD-10 tien su benh/tat bam sinh.</summary>
        [DbColumn("ma_icd_ts_bamsinh", Caption = "ICD-10 TS bam sinh", Width = 120, Order = 301)]
        public string MaIcdTsBamsinh { get { return _maIcdTsBamsinh; } set { SetField(ref _maIcdTsBamsinh, value); } }

        /// <summary>San khoa. 1:Co; 0:Khong.</summary>
        [DbColumn("san_khoa", Caption = "San khoa", Width = 80, Order = 302)]
        public int SanKhoa { get { return _sanKhoa; } set { SetField(ref _sanKhoa, value); } }

        /// <summary>San khoa khong BT. 0:De thieu thang; 1:De thua can; 2:De can thiep; 3:De ngat; 4:Me bi benh.</summary>
        [DbColumn("san_khoa_khong_bt", Caption = "SK khong BT", Width = 80, Order = 303)]
        public int SanKhoaKhongBt { get { return _sanKhoaKhongBt; } set { SetField(ref _sanKhoaKhongBt, value); } }

        /// <summary>Ma ICD-10 benh gay ra SK khong BT.</summary>
        [DbColumn("ma_benh_san_khoa_khong_bt", Caption = "ICD-10 SK khong BT", Width = 120, Order = 304)]
        public string MaBenhSanKhoaKhongBt { get { return _maBenhSanKhoaKhongBt; } set { SetField(ref _maBenhSanKhoaKhongBt, value); } }

        // ============================================================
        // ---- >=18 tuoi ----
        // ============================================================

        // ---- Tien su benh (>=18t) ----

        private int _benh5nam;
        private int _benhThankinh;
        private int _benhMat;
        private int _benhTai;
        private int _benhTim;
        private int _phauthuatTimmach;
        private int _tangHuyetap;
        private int _khoTho;
        private int _benhPhoi;
        private int _benhThanLocmau;
        private int _nghienRuoubia;
        private int _daiThaoduong;
        private int _benhTamthan;
        private int _roiLoanYthuc;
        private int _ngatChongmat;
        private int _benhTieuhoa;
        private int _roiLoanGiacngu;
        private int _taibienLiet;
        private int _benhCotsong;
        private int _sudungRuou;
        private int _sudungMatuy;
        private int _benhKhac;
        private string _tenBenhKhac;

        /// <summary>Co benh hay bi thuong trong 5 nam. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_5nam", Caption = "Benh 5 nam", Width = 80, Order = 400)]
        public int Benh5nam { get { return _benh5nam; } set { SetField(ref _benh5nam, value); } }

        /// <summary>Benh than kinh/chong thuong dau. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_thankinh", Caption = "Benh TN", Width = 80, Order = 401)]
        public int BenhThankinh { get { return _benhThankinh; } set { SetField(ref _benhThankinh, value); } }

        /// <summary>Benh mat/giam thi luc. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_mat", Caption = "Benh mat", Width = 80, Order = 402)]
        public int BenhMat { get { return _benhMat; } set { SetField(ref _benhMat, value); } }

        /// <summary>Benh tai/giam nghe. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_tai", Caption = "Benh tai", Width = 80, Order = 403)]
        public int BenhTai { get { return _benhTai; } set { SetField(ref _benhTai, value); } }

        /// <summary>Benh tim. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_tim", Caption = "Benh tim", Width = 80, Order = 404)]
        public int BenhTim { get { return _benhTim; } set { SetField(ref _benhTim, value); } }

        /// <summary>Phau thuat can thiep tim mach. 1:Co; 0:Khong.</summary>
        [DbColumn("phauthuat_timmach", Caption = "PT tim mach", Width = 80, Order = 405)]
        public int PhauthuatTimmach { get { return _phauthuatTimmach; } set { SetField(ref _phauthuatTimmach, value); } }

        /// <summary>Tang huyet ap. 1:Co; 0:Khong.</summary>
        [DbColumn("tang_huyetap", Caption = "Tang HA", Width = 80, Order = 406)]
        public int TangHuyetap { get { return _tangHuyetap; } set { SetField(ref _tangHuyetap, value); } }

        /// <summary>Kho tho. 1:Co; 0:Khong.</summary>
        [DbColumn("kho_tho", Caption = "Kho tho", Width = 80, Order = 407)]
        public int KhoTho { get { return _khoTho; } set { SetField(ref _khoTho, value); } }

        /// <summary>Benh phoi. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_phoi", Caption = "Benh phoi", Width = 80, Order = 408)]
        public int BenhPhoi { get { return _benhPhoi; } set { SetField(ref _benhPhoi, value); } }

        /// <summary>Benh than, loc mau. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_than_locmau", Caption = "Benh than-LM", Width = 80, Order = 409)]
        public int BenhThanLocmau { get { return _benhThanLocmau; } set { SetField(ref _benhThanLocmau, value); } }

        /// <summary>Nghien ruou bia. 1:Co; 0:Khong.</summary>
        [DbColumn("nghien_ruoubia", Caption = "Nghien ruou", Width = 80, Order = 410)]
        public int NghienRuoubia { get { return _nghienRuoubia; } set { SetField(ref _nghienRuoubia, value); } }

        /// <summary>Dai thao duong. 1:Co; 0:Khong.</summary>
        [DbColumn("dai_thaoduong", Caption = "Dai thao duong", Width = 80, Order = 411)]
        public int DaiThaoduong { get { return _daiThaoduong; } set { SetField(ref _daiThaoduong, value); } }

        /// <summary>Benh tam than. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_tamthan", Caption = "Benh tam than", Width = 80, Order = 412)]
        public int BenhTamthan { get { return _benhTamthan; } set { SetField(ref _benhTamthan, value); } }

        /// <summary>Roi loan y thuc. 1:Co; 0:Khong.</summary>
        [DbColumn("roi_loan_ythuc", Caption = "RL y thuc", Width = 80, Order = 413)]
        public int RoiLoanYthuc { get { return _roiLoanYthuc; } set { SetField(ref _roiLoanYthuc, value); } }

        /// <summary>Ngat chong mat. 1:Co; 0:Khong.</summary>
        [DbColumn("ngat_chongmat", Caption = "Ngat-chong mat", Width = 80, Order = 414)]
        public int NgatChongmat { get { return _ngatChongmat; } set { SetField(ref _ngatChongmat, value); } }

        /// <summary>Benh tieu hoa. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_tieuhoa", Caption = "Benh tieu hoa", Width = 80, Order = 415)]
        public int BenhTieuhoa { get { return _benhTieuhoa; } set { SetField(ref _benhTieuhoa, value); } }

        /// <summary>Roi loan giac ngu. 1:Co; 0:Khong.</summary>
        [DbColumn("roi_loan_giacngu", Caption = "RL giac ngu", Width = 80, Order = 416)]
        public int RoiLoanGiacngu { get { return _roiLoanGiacngu; } set { SetField(ref _roiLoanGiacngu, value); } }

        /// <summary>Tai bien MMN hoac liet. 1:Co; 0:Khong.</summary>
        [DbColumn("taibien_liet", Caption = "Tai bien-liet", Width = 80, Order = 417)]
        public int TaibienLiet { get { return _taibienLiet; } set { SetField(ref _taibienLiet, value); } }

        /// <summary>Benh cot song. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_cotsong", Caption = "Benh CS", Width = 80, Order = 418)]
        public int BenhCotsong { get { return _benhCotsong; } set { SetField(ref _benhCotsong, value); } }

        /// <summary>Su dung ruou thuong xuyen. 1:Co; 0:Khong.</summary>
        [DbColumn("sudung_ruou", Caption = "Dung ruou", Width = 80, Order = 419)]
        public int SudungRuou { get { return _sudungRuou; } set { SetField(ref _sudungRuou, value); } }

        /// <summary>Su dung ma tuy. 1:Co; 0:Khong.</summary>
        [DbColumn("sudung_matuy", Caption = "Dung ma tuy", Width = 80, Order = 420)]
        public int SudungMatuy { get { return _sudungMatuy; } set { SetField(ref _sudungMatuy, value); } }

        /// <summary>Benh khac. 1:Co; 0:Khong.</summary>
        [DbColumn("benh_khac", Caption = "Benh khac", Width = 80, Order = 421)]
        public int BenhKhac { get { return _benhKhac; } set { SetField(ref _benhKhac, value); } }

        /// <summary>Ten benh khac.</summary>
        [DbColumn("ten_benh_khac", Caption = "Ten benh khac", Width = 200, Order = 422)]
        public string TenBenhKhac { get { return _tenBenhKhac; } set { SetField(ref _tenBenhKhac, value); } }

        // ---- Can lam sang (>=18t) ----

        private string _clsHuyethoc;
        private string _clsDuongmau;
        private string _clsUre;
        private string _clsCreatinin;
        private string _clsAsat;
        private string _clsAlat;
        private string _clsTongphantichNt;
        private string _clsNtKhac;
        private string _clsXqTimphoi;
        private int _clsKhac;
        private string _clsKhacNoidung;

        /// <summary>Xet nghiem huyet hoc.</summary>
        [DbColumn("cls_huyethoc", Caption = "CLS huyet hoc", Width = 200, Order = 430)]
        public string ClsHuyethoc { get { return _clsHuyethoc; } set { SetField(ref _clsHuyethoc, value); } }

        /// <summary>Sinh hoa mau, duong mau.</summary>
        [DbColumn("cls_duongmau", Caption = "CLS duong mau", Width = 200, Order = 431)]
        public string ClsDuongmau { get { return _clsDuongmau; } set { SetField(ref _clsDuongmau, value); } }

        /// <summary>Xet nghiem Ure.</summary>
        [DbColumn("cls_ure", Caption = "CLS Ure", Width = 200, Order = 432)]
        public string ClsUre { get { return _clsUre; } set { SetField(ref _clsUre, value); } }

        /// <summary>Xet nghiem Creatinin.</summary>
        [DbColumn("cls_creatinin", Caption = "CLS Creatinin", Width = 200, Order = 433)]
        public string ClsCreatinin { get { return _clsCreatinin; } set { SetField(ref _clsCreatinin, value); } }

        /// <summary>Xet nghiem ASAT (GOT).</summary>
        [DbColumn("cls_asat", Caption = "CLS ASAT", Width = 200, Order = 434)]
        public string ClsAsat { get { return _clsAsat; } set { SetField(ref _clsAsat, value); } }

        /// <summary>Xet nghiem ALAT (GPT).</summary>
        [DbColumn("cls_alat", Caption = "CLS ALAT", Width = 200, Order = 435)]
        public string ClsAlat { get { return _clsAlat; } set { SetField(ref _clsAlat, value); } }

        /// <summary>Tong phan tich nuoc tieu.</summary>
        [DbColumn("cls_tongphantich_nt", Caption = "CLS TPNT", Width = 200, Order = 436)]
        public string ClsTongphantichNt { get { return _clsTongphantichNt; } set { SetField(ref _clsTongphantichNt, value); } }

        /// <summary>Xet nghiem NT khac.</summary>
        [DbColumn("cls_nt_khac", Caption = "CLS NT khac", Width = 200, Order = 437)]
        public string ClsNtKhac { get { return _clsNtKhac; } set { SetField(ref _clsNtKhac, value); } }

        /// <summary>X-quang tim phoi thang.</summary>
        [DbColumn("cls_xq_timphoi", Caption = "XQ tim phoi", Width = 200, Order = 438)]
        public string ClsXqTimphoi { get { return _clsXqTimphoi; } set { SetField(ref _clsXqTimphoi, value); } }

        /// <summary>Co CLS khac. 1:Co; 0:Khong.</summary>
        [DbColumn("cls_khac", Caption = "CLS khac", Width = 80, Order = 439)]
        public int ClsKhac { get { return _clsKhac; } set { SetField(ref _clsKhac, value); } }

        /// <summary>Noi dung CLS khac.</summary>
        [DbColumn("cls_khac_noidung", Caption = "ND CLS khac", Width = 200, Order = 440)]
        public string ClsKhacNoidung { get { return _clsKhacNoidung; } set { SetField(ref _clsKhacNoidung, value); } }

        // ============================================================
        // ---- Tam than ----
        // ============================================================

        // ---- Tien su tam than ----

        private int _tsTamthanGiadinh;
        private string _tenTamthanGiadinh;
        private int _tsTamthanBanthan;
        private string _tenTamthanBanthan;

        /// <summary>Tien su benh tam than GD. 1:Co; 0:Khong.</summary>
        [DbColumn("ts_tamthan_giadinh", Caption = "TS tam than GD", Width = 80, Order = 500)]
        public int TsTamthanGiadinh { get { return _tsTamthanGiadinh; } set { SetField(ref _tsTamthanGiadinh, value); } }

        /// <summary>Ten benh tam than GD.</summary>
        [DbColumn("ten_tamthan_giadinh", Caption = "Ten TT GD", Width = 200, Order = 501)]
        public string TenTamthanGiadinh { get { return _tenTamthanGiadinh; } set { SetField(ref _tenTamthanGiadinh, value); } }

        /// <summary>Tien su benh tam than ban than. 1:Co; 0:Khong.</summary>
        [DbColumn("ts_tamthan_banthan", Caption = "TS tam than BT", Width = 80, Order = 502)]
        public int TsTamthanBanthan { get { return _tsTamthanBanthan; } set { SetField(ref _tsTamthanBanthan, value); } }

        /// <summary>Ten benh tam than ban than.</summary>
        [DbColumn("ten_tamthan_banthan", Caption = "Ten TT BT", Width = 200, Order = 503)]
        public string TenTamthanBanthan { get { return _tenTamthanBanthan; } set { SetField(ref _tenTamthanBanthan, value); } }

        // ---- Kham tam than ----

        private string _tamthanBieuhien;
        private string _tamthanYthuc;
        private string _tamthanDinhhuongKhonggian;
        private string _tamthanDinhhuongThoigian;
        private string _tamthanDinhhuongXungquanh;
        private string _tamthanDinhhuongBanthan;
        private string _tamthanCamxuc;
        private string _tamthanCamgiacTrigiac;
        private string _tamthanTuduyHinhthuc;
        private string _tamthanTuduyNoidung;
        private string _tamthanHanhviYchi;
        private string _tamthanHanhviBannang;
        private string _tamthanTrinho;
        private string _tamthanTritue;
        private string _tamthanChuy;
        private string _tamthanKhac;

        /// <summary>Bieu hien chung.</summary>
        [DbColumn("tamthan_bieuhien", Caption = "Bieu hien chung", Width = 200, Order = 510)]
        public string TamthanBieuhien { get { return _tamthanBieuhien; } set { SetField(ref _tamthanBieuhien, value); } }

        /// <summary>Y thuc.</summary>
        [DbColumn("tamthan_ythuc", Caption = "Y thuc", Width = 200, Order = 511)]
        public string TamthanYthuc { get { return _tamthanYthuc; } set { SetField(ref _tamthanYthuc, value); } }

        /// <summary>Dinh huong luc (Khong gian).</summary>
        [DbColumn("tamthan_dinhhuong_khonggian", Caption = "DH Khong gian", Width = 200, Order = 512)]
        public string TamthanDinhhuongKhonggian { get { return _tamthanDinhhuongKhonggian; } set { SetField(ref _tamthanDinhhuongKhonggian, value); } }

        /// <summary>Dinh huong luc (Thoi gian).</summary>
        [DbColumn("tamthan_dinhhuong_thoigian", Caption = "DH Thoi gian", Width = 200, Order = 513)]
        public string TamthanDinhhuongThoigian { get { return _tamthanDinhhuongThoigian; } set { SetField(ref _tamthanDinhhuongThoigian, value); } }

        /// <summary>Dinh huong luc (Xung quanh).</summary>
        [DbColumn("tamthan_dinhhuong_xungquanh", Caption = "DH Xung quanh", Width = 200, Order = 514)]
        public string TamthanDinhhuongXungquanh { get { return _tamthanDinhhuongXungquanh; } set { SetField(ref _tamthanDinhhuongXungquanh, value); } }

        /// <summary>Dinh huong luc (Ban than).</summary>
        [DbColumn("tamthan_dinhhuong_banthan", Caption = "DH Ban than", Width = 200, Order = 515)]
        public string TamthanDinhhuongBanthan { get { return _tamthanDinhhuongBanthan; } set { SetField(ref _tamthanDinhhuongBanthan, value); } }

        /// <summary>Cam xuc.</summary>
        [DbColumn("tamthan_camxuc", Caption = "Cam xuc", Width = 200, Order = 516)]
        public string TamthanCamxuc { get { return _tamthanCamxuc; } set { SetField(ref _tamthanCamxuc, value); } }

        /// <summary>Cam giac, tri giac.</summary>
        [DbColumn("tamthan_camgiac_trigiac", Caption = "Cam giac-TG", Width = 200, Order = 517)]
        public string TamthanCamgiacTrigiac { get { return _tamthanCamgiacTrigiac; } set { SetField(ref _tamthanCamgiacTrigiac, value); } }

        /// <summary>Tu duy hinh thuc.</summary>
        [DbColumn("tamthan_tuduy_hinhthuc", Caption = "TD hinh thuc", Width = 200, Order = 518)]
        public string TamthanTuduyHinhthuc { get { return _tamthanTuduyHinhthuc; } set { SetField(ref _tamthanTuduyHinhthuc, value); } }

        /// <summary>Tu duy noi dung.</summary>
        [DbColumn("tamthan_tuduy_noidung", Caption = "TD noi dung", Width = 200, Order = 519)]
        public string TamthanTuduyNoidung { get { return _tamthanTuduyNoidung; } set { SetField(ref _tamthanTuduyNoidung, value); } }

        /// <summary>Hanh vi, tac phong co y chi.</summary>
        [DbColumn("tamthan_hanhvi_ychi", Caption = "HV y chi", Width = 200, Order = 520)]
        public string TamthanHanhviYchi { get { return _tamthanHanhviYchi; } set { SetField(ref _tamthanHanhviYchi, value); } }

        /// <summary>Hanh vi, tac phong co ban nang.</summary>
        [DbColumn("tamthan_hanhvi_bannang", Caption = "HV ban nang", Width = 200, Order = 521)]
        public string TamthanHanhviBannang { get { return _tamthanHanhviBannang; } set { SetField(ref _tamthanHanhviBannang, value); } }

        /// <summary>Tri nho.</summary>
        [DbColumn("tamthan_trinho", Caption = "Tri nho", Width = 200, Order = 522)]
        public string TamthanTrinho { get { return _tamthanTrinho; } set { SetField(ref _tamthanTrinho, value); } }

        /// <summary>Tri tue.</summary>
        [DbColumn("tamthan_tritue", Caption = "Tri tue", Width = 200, Order = 523)]
        public string TamthanTritue { get { return _tamthanTritue; } set { SetField(ref _tamthanTritue, value); } }

        /// <summary>Chu y.</summary>
        [DbColumn("tamthan_chuy", Caption = "Chu y", Width = 200, Order = 524)]
        public string TamthanChuy { get { return _tamthanChuy; } set { SetField(ref _tamthanChuy, value); } }

        /// <summary>Khac.</summary>
        [DbColumn("tamthan_khac", Caption = "TT Khac", Width = 200, Order = 525)]
        public string TamthanKhac { get { return _tamthanKhac; } set { SetField(ref _tamthanKhac, value); } }

        // ---- Tam than: Khac ----

        private string _tracnghiemTamly;
        private string _canlamsangKhacBs;

        /// <summary>Trac nghiem tam ly.</summary>
        [DbColumn("tracnghiem_tamly", Caption = "TN tam ly", Width = 200, Order = 526)]
        public string TracnghiemTamly { get { return _tracnghiemTamly; } set { SetField(ref _tracnghiemTamly, value); } }

        /// <summary>Can lam sang khac theo chi dinh BS.</summary>
        [DbColumn("canlamsang_khac_bs", Caption = "CLS khac BS", Width = 200, Order = 527)]
        public string CanlamsangKhacBs { get { return _canlamsangKhacBs; } set { SetField(ref _canlamsangKhacBs, value); } }
    }
}
