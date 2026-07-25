-- =====================================================================
--  CrudFramework — SQL fixture cho module demo "Kham suc khoe" (KSK)
--  Theo Thong tu 25/2026/TT-BYT — gom 4 loai mau KSK dung chung 1 bang.
--  Chay tren PostgreSQL. Day la "API" thuc su: C# chi goi 4 function nay.
--  Contract:
--    fn_ksk_phieu_get(p_id int)            RETURNS jsonb -> 1 record | null
--    fn_ksk_phieu_list(p_filter jsonb)     RETURNS jsonb -> array cac record
--    fn_ksk_phieu_upsert(p_payload jsonb)  RETURNS jsonb -> {success,data,errors}
--    fn_ksk_phieu_delete(p_id int)         RETURNS jsonb -> {success,message}
-- =====================================================================

-- ---------- bang ksk_benhnhan (rut gon tu dmbenhnhan) ----------
CREATE TABLE IF NOT EXISTS ksk_benhnhan (
    id           SERIAL PRIMARY KEY,
    holot        VARCHAR(100) NOT NULL,
    ten          VARCHAR(50)  NOT NULL,
    ngaysinh     DATE         NULL,
    gioitinh     NUMERIC(1,0) NULL,  -- 0:Nu; 1:Nam
    cmnd         VARCHAR(20)  NULL,
    ngaycap      DATE         NULL,
    noicap       VARCHAR(100) NULL,
    madt         VARCHAR(10)  NULL,
    madtuong     VARCHAR(10)  NULL,
    nhom_mau     VARCHAR(10)  NULL,
    diachi       VARCHAR(255) NULL,
    manghe       VARCHAR(10)  NULL,
    noict        VARCHAR(255) NULL,
    created_at   TIMESTAMPTZ  NOT NULL DEFAULT now()
);

COMMENT ON TABLE ksk_benhnhan IS 'Bang benh nhan rut gon cho demo KSK (goc: dmbenhnhan).';
COMMENT ON COLUMN ksk_benhnhan.gioitinh IS 'Gioi tinh. 1:Nam; 0:Nu.';

-- ---------- bang ksk_phieu (chinh — gom pskhamsuckhoe + psdangky/khambenh lien quan) ----------
CREATE TABLE IF NOT EXISTS ksk_phieu (
    id                SERIAL PRIMARY KEY,
    benhnhan_id        INT           NOT NULL REFERENCES ksk_benhnhan(id),
    loai_mau_ksk       NUMERIC(1,0)  NOT NULL,  -- 1:Tre em <6t; 2:6-18t; 3:>=18t; 4:Tam than
    ngay_ksk           DATE          NULL,
    coso_kham          VARCHAR(255)  NULL,
    lydo_ksk           VARCHAR(500)  NULL,

    -- Thong tin nguoi giam ho (rut gon tu psdangky)
    hoten_qh           VARCHAR(100) NULL,
    loaiqh             NUMERIC(1,0) NULL,  -- 0:Cha/me; 1:Vo/chong; 2:Anh/chi/em; 3:Khac
    dienthoai_qh       VARCHAR(20)  NULL,
    cmnd_qh            VARCHAR(20)  NULL,

    -- Sinh hieu (rut gon tu psdangky)
    nhietdo            NUMERIC(10,2) NULL,
    mach               NUMERIC(10,2) NULL,
    nhiptho            NUMERIC(10,2) NULL,
    chieucao           NUMERIC(10,2) NULL,
    cannang            NUMERIC(10,2) NULL,
    huyetap            VARCHAR(20)   NULL,

    -- Tien su chung (rut gon tu pskhamsuckhoe)
    tsbenh_co_benhbamsinh    NUMERIC(1,0)   NULL,
    tsbenh_tenbenh           VARCHAR(500)  NULL,
    tsgd_co_benh_truyennhiem NUMERIC(1,0)   NULL,
    tsgd_tenbenh_truyennhiem VARCHAR(500)  NULL,
    dang_dtbenh              VARCHAR(500)  NULL,

    -- Kham lam sang chung (rut gon tu pskhamsuckhoe)
    tuanhoan_ksk    VARCHAR(500) NULL,
    hohap_ksk       VARCHAR(500) NULL,
    tieuhoa_ksk     VARCHAR(500) NULL,
    than_ksk        VARCHAR(500) NULL,
    thankinh_ksk    VARCHAR(500) NULL,
    tamthan_ksk     VARCHAR(500) NULL,
    khamls_khac_ksk VARCHAR(500) NULL,

    -- Mat (rut gon)
    kmatphai_ksk    NUMERIC(10,2) NULL,
    kmattrai_ksk    NUMERIC(10,2) NULL,
    matphai_ksk     NUMERIC(10,2) NULL,
    mattrai_ksk     NUMERIC(10,2) NULL,

    -- Tai (rut gon)
    tnoithuong_ksk  VARCHAR(100) NULL,
    tnoitham_ksk    VARCHAR(100) NULL,
    noithuong_ksk   VARCHAR(100) NULL,
    noitham_ksk     VARCHAR(100) NULL,

    -- Rang ham mat (rut gon)
    hamtren_ksk     VARCHAR(100) NULL,
    hamduoi_ksk     VARCHAR(100) NULL,
    rhmbenh_ksk     NUMERIC(1,0)  NULL,

    -- Ket qua CLS chung
    kqcls_ksk       VARCHAR(500) NULL,

    -- Ngoai khoa / Da lieuu / San phu khoa (rut gon)
    ngoaikhoa_ksk       VARCHAR(500) NULL,
    plngoaikhoa_ksk     VARCHAR(500) NULL,
    dalieu_ksk          VARCHAR(500) NULL,
    pldalieu_ksk        VARCHAR(500) NULL,
    sanphukhoa_ksk      VARCHAR(500) NULL,
    plsanphukhoa_ksk    VARCHAR(500) NULL,

    created_at   TIMESTAMPTZ  NOT NULL DEFAULT now(),
    updated_at   TIMESTAMPTZ  NOT NULL DEFAULT now()
);

COMMENT ON TABLE ksk_phieu IS 'Phieu Kham suc khoe demo (gom 4 loai mau theo TT 25/2026/TT-BYT).';
COMMENT ON COLUMN ksk_phieu.loai_mau_ksk IS 'Loai mau KSK dang ap dung. 1:Tre em <6 tuoi; 2:6-18 tuoi; 3:Nguoi >=18 tuoi; 4:Tam than.';
COMMENT ON COLUMN ksk_phieu.loaiqh IS 'Loai quan he nguoi giam ho. 0:Cha/me; 1:Vo/chong; 2:Anh/chi/em; 3:Khac.';

-- =====================================================================
--  ADD COLUMN IF NOT EXISTS — tung cot mot cau ALTER rieng
--  (idempotent: chay lai an toan, cot da ton tai thi bo qua)
--
--  Phan II trong tai liu: cac gia tri CHUA CO truong luu tru.
--  Gom 4 nhom mau KSK.
-- =====================================================================

-- =============================================================
--  Mau Tre em duoi 06 tuoi
-- =============================================================

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ts_tiepxuc_lao numeric(1,0);
COMMENT ON COLUMN ksk_phieu.ts_tiepxuc_lao IS 'Tien su tiep xuc voi nguoi benh lao. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS chieudai_tuoi_sd numeric(10,2);
COMMENT ON COLUMN ksk_phieu.chieudai_tuoi_sd IS 'Chieu dai theo tuoi (SD).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cannang_tuoi_sd numeric(10,2);
COMMENT ON COLUMN ksk_phieu.cannang_tuoi_sd IS 'Can nang theo tuoi (SD).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS trangthai_vongdau numeric(1,0);
COMMENT ON COLUMN ksk_phieu.trangthai_vongdau IS 'Trang thai vong dau. 0:Binh thuong; 1:Dau to; 2:Dau nho.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS chuvi_vongcanhtay numeric(10,2);
COMMENT ON COLUMN ksk_phieu.chuvi_vongcanhtay IS 'Chu vi vong canh tay (mm).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tinhtrang_dinhduong varchar(255);
COMMENT ON COLUMN ksk_phieu.tinhtrang_dinhduong IS 'Tinh trang dinh duong. 0:Binh thuong; 1:Phu dinh duong; 2:Dau hieu thieu mau; 3:Dau hieu coi xuong; 4:Suy dinh duong; 5:Thua can, beo phi. Co the chon nhieu lua chon cach nhau bang dau ; vd: 1;2;3.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS phattrien_tinhthan numeric(1,0);
COMMENT ON COLUMN ksk_phieu.phattrien_tinhthan IS 'Phat trien tinh than theo do tuoi. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS phattrien_vandong numeric(1,0);
COMMENT ON COLUMN ksk_phieu.phattrien_vandong IS 'Phat trien van dong theo do tuoi. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS nguyco_tuky numeric(1,0);
COMMENT ON COLUMN ksk_phieu.nguyco_tuky IS 'Tre co nguy co tu ky. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_lao numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_lao IS 'Lao. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tiem_vgb_mui1 numeric(1,0);
COMMENT ON COLUMN ksk_phieu.tiem_vgb_mui1 IS 'Tiem vac xin Viem gan B mui 1. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tiemchung_daydu numeric(1,0);
COMMENT ON COLUMN ksk_phieu.tiemchung_daydu IS 'Tiem chung day du theo do tuoi. 1:Co; 0:Khong.';

-- Da - Dau - Co (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS mausac_da numeric(1,0);
COMMENT ON COLUMN ksk_phieu.mausac_da IS 'Mau sac da. 0:Hong hao; 1:Nhot; 2:Tim; 3:Vang; 4:Sam da.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS long_bantay numeric(1,0);
COMMENT ON COLUMN ksk_phieu.long_bantay IS 'Long ban tay. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS thop numeric(1,0);
COMMENT ON COLUMN ksk_phieu.thop IS 'Thop. 0:Binh thuong; 1:Rong; 2:Hep; 3:Thop phong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS hinhdang_dau numeric(1,0);
COMMENT ON COLUMN ksk_phieu.hinhdang_dau IS 'Kich thuoc va hinh dang dau. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS vandong_co numeric(1,0);
COMMENT ON COLUMN ksk_phieu.vandong_co IS 'Van dong co. 0:Binh thuong; 1:Gioi han.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS khoibatthuong_dauco numeric(1,0);
COMMENT ON COLUMN ksk_phieu.khoibatthuong_dauco IS 'Khoi bat thuong vung dau co. 1:Co; 0:Khong.';

-- Mat (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS vitri_haimat numeric(1,0);
COMMENT ON COLUMN ksk_phieu.vitri_haimat IS 'Vi tri hai mat. 0:Binh thuong; 1:Xa nhau.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS mimat_ketmac numeric(1,0);
COMMENT ON COLUMN ksk_phieu.mimat_ketmac IS 'Mi mat va ket mac. 0:Binh thuong; 1:Swung, do; 2:Chay ghenn, mu.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS dongtu numeric(1,0);
COMMENT ON COLUMN ksk_phieu.dongtu IS 'Dong tu. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS lacmat numeric(1,0);
COMMENT ON COLUMN ksk_phieu.lacmat IS 'Lac mat. 1:Co; 0:Khong.';

-- Tai (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tai_mangnhi numeric(1,0);
COMMENT ON COLUMN ksk_phieu.tai_mangnhi IS 'Tai va mang nhi. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS dapung_amthanh numeric(1,0);
COMMENT ON COLUMN ksk_phieu.dapung_amthanh IS 'Dap ung voi am thanh. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS khoisung_sautai numeric(1,0);
COMMENT ON COLUMN ksk_phieu.khoisung_sautai IS 'Co khoi sung sau tai. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS chaymu_nuoctai numeric(1,0);
COMMENT ON COLUMN ksk_phieu.chaymu_nuoctai IS 'Dau hieu chay mu, nuoc tai. 1:Co; 0:Khong.';

--Mui (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS hinhdang_mui numeric(1,0);
COMMENT ON COLUMN ksk_phieu.hinhdang_mui IS 'Hinh dang mui. 0:Binh thuong; 1:Mui to, day; 2:Bat san xuong mui.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS chaynuoc_mui numeric(1,0);
COMMENT ON COLUMN ksk_phieu.chaynuoc_mui IS 'Chay nuoc mui. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS nghet_mui numeric(1,0);
COMMENT ON COLUMN ksk_phieu.nghet_mui IS 'Nghet mui. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS hong numeric(1,0);
COMMENT ON COLUMN ksk_phieu.hong IS 'Hong. 1:Binh thuong; 0:Khong binh thuong.';

-- Mieng (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS hinhdang_mieng numeric(1,0);
COMMENT ON COLUMN ksk_phieu.hinhdang_mieng IS 'Hinh dang mieng. 0:Binh thuong; 1:Sut moi, che vom.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS rangsua_sosinh numeric(1,0);
COMMENT ON COLUMN ksk_phieu.rangsua_sosinh IS 'Rang sua so sinh. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS hinhdang_luoi numeric(1,0);
COMMENT ON COLUMN ksk_phieu.hinhdang_luoi IS 'Hinh dang luoi. 0:Binh thuong; 1:Luoi to be.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS dinh_thangluoi numeric(1,0);
COMMENT ON COLUMN ksk_phieu.dinh_thangluoi IS 'Dinh thang luoi. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS nam_mieng numeric(1,0);
COMMENT ON COLUMN ksk_phieu.nam_mieng IS 'Nam mieng. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cam_tut numeric(1,0);
COMMENT ON COLUMN ksk_phieu.cam_tut IS 'Cam nho, tut ve sau. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS vetsau_rang numeric(1,0);
COMMENT ON COLUMN ksk_phieu.vetsau_rang IS 'Vet sau, mang bam, lo tren rang. 1:Co; 0:Khong.';

-- Ho hap (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS nhiptho_khongdeu numeric(1,0);
COMMENT ON COLUMN ksk_phieu.nhiptho_khongdeu IS 'Nhip tho khong deu. 0:Khong; 1:Co con ngung tho tren 5 giay.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS thorutlom_longnguc numeric(1,0);
COMMENT ON COLUMN ksk_phieu.thorutlom_longnguc IS 'Tho rut lom long nguc. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tiengtho_batthuong numeric(1,0);
COMMENT ON COLUMN ksk_phieu.tiengtho_batthuong IS 'Tien tho bat thuong. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS dauhieu_suyhohap numeric(1,0);
COMMENT ON COLUMN ksk_phieu.dauhieu_suyhohap IS 'Dau hieu suy ho hap. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS nghe_phoi numeric(1,0);
COMMENT ON COLUMN ksk_phieu.nghe_phoi IS 'Nghe phoi. 1:Binh thuong; 0:Khong binh thuong.';

-- Tim mach (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS vitri_momtim numeric(1,0);
COMMENT ON COLUMN ksk_phieu.vitri_momtim IS 'Vi tri mom tim. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS mach_ngoaivi numeric(1,0);
COMMENT ON COLUMN ksk_phieu.mach_ngoaivi IS 'Mach ngoai vi. 0:Bat ro; 1:Mach nhe; 2:Khong bat duoc.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS nghe_tim numeric(1,0);
COMMENT ON COLUMN ksk_phieu.nghe_tim IS 'Nghe tim (loan nhip, tien thoi). 1:Co; 0:Khong.';

-- Tieu hoa (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS hinhdang_bung_ron numeric(1,0);
COMMENT ON COLUMN ksk_phieu.hinhdang_bung_ron IS 'Hinh dang bung, ron. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS gan_lach_to numeric(1,0);
COMMENT ON COLUMN ksk_phieu.gan_lach_to IS 'Gan, lach to. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS khoibatthuong_bung numeric(1,0);
COMMENT ON COLUMN ksk_phieu.khoibatthuong_bung IS 'Khoi bat thuong vung bung. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS lo_haumon numeric(1,0);
COMMENT ON COLUMN ksk_phieu.lo_haumon IS 'Lo hau mon. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cqsd_ngoai numeric(1,0);
COMMENT ON COLUMN ksk_phieu.cqsd_ngoai IS 'Co quan sinh dung ngoai. 1:Binh thuong; 0:Khong binh thuong.';

-- Than kinh (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS vandong_khongdoixung numeric(1,0);
COMMENT ON COLUMN ksk_phieu.vandong_khongdoixung IS 'Van dong khong doi xung. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS phanxa_bu numeric(1,0);
COMMENT ON COLUMN ksk_phieu.phanxa_bu IS 'Phan xa bu. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS phanxa_nam numeric(1,0);
COMMENT ON COLUMN ksk_phieu.phanxa_nam IS 'Phan xa nam. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS phanxa_moro numeric(1,0);
COMMENT ON COLUMN ksk_phieu.phanxa_moro IS 'Phan xa Moro. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS truongluc_co numeric(1,0);
COMMENT ON COLUMN ksk_phieu.truongluc_co IS 'Truong luc co. 0:Binh thuong; 1:Tang.';

-- Co xuong khop (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS khop_hang numeric(1,0);
COMMENT ON COLUMN ksk_phieu.khop_hang IS 'Khop hang. 0:Binh thuong; 1:Trat khop hang.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS phanxa_co numeric(1,0);
COMMENT ON COLUMN ksk_phieu.phanxa_co IS 'Phan xa co. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS lung_cotsong numeric(1,0);
COMMENT ON COLUMN ksk_phieu.lung_cotsong IS 'Kiem tra lung, cot song. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tuchi_khop numeric(1,0);
COMMENT ON COLUMN ksk_phieu.tuchi_khop IS 'Kham tu chi va khop. 1:Binh thuong; 0:Khong binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS dang_di numeric(1,0);
COMMENT ON COLUMN ksk_phieu.dang_di IS 'Quan sat dang di. 1:Binh thuong; 0:Khong binh thuong.';

-- Khac (Tre em <6t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS doituong varchar(255);
COMMENT ON COLUMN ksk_phieu.doituong IS 'Doi tuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS sinh_non numeric(1,0);
COMMENT ON COLUMN ksk_phieu.sinh_non IS 'Sinh non. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tuanthai_khi_sinh varchar(255);
COMMENT ON COLUMN ksk_phieu.tuanthai_khi_sinh IS 'Tuan thai khi sinh.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS nguon_chitra varchar(255);
COMMENT ON COLUMN ksk_phieu.nguon_chitra IS 'Nguon chi tra.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS trangthai_nhietdo numeric(1,0);
COMMENT ON COLUMN ksk_phieu.trangthai_nhietdo IS 'Trang thai nhiet do. 0:Binh thuong; 1:Soat; 2:Ha than nhiet.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS trangthai_mach numeric(1,0);
COMMENT ON COLUMN ksk_phieu.trangthai_mach IS 'Trang thai mach. 0:Binh thuong; 1:Nhanh.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS trangthai_nhiptho numeric(1,0);
COMMENT ON COLUMN ksk_phieu.trangthai_nhiptho IS 'Trang thai nhip tho. 0:Binh thuong; 1:Nhanh; 2:Cham.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS trangthai_huyetap numeric(1,0);
COMMENT ON COLUMN ksk_phieu.trangthai_huyetap IS 'Trang thai huyet ap.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ketluan_binhthuong varchar(500);
COMMENT ON COLUMN ksk_phieu.ketluan_binhthuong IS 'Ket luan: Binh thuong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ketluan_nguyco_lao varchar(500);
COMMENT ON COLUMN ksk_phieu.ketluan_nguyco_lao IS 'Ket luan: Co nguy co mac Lao.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ketluan_vandesuckhoe varchar(500);
COMMENT ON COLUMN ksk_phieu.ketluan_vandesuckhoe IS 'Ket luan: Co van de suc khoe.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ketluan_ghiro varchar(500);
COMMENT ON COLUMN ksk_phieu.ketluan_ghiro IS 'Ket luan: Ghi ro.';

-- =============================================================
--  Mau 6 tuoi den duoi 18 tuoi
-- =============================================================

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ma_icd_tsgd_bamsinh varchar(255);
COMMENT ON COLUMN ksk_phieu.ma_icd_tsgd_bamsinh IS 'Ma ICD-10 tien su benh gia dinh bam sinh hoac benh truyen nhiem. Ghi ma ICD-10 hoac ma trieu chung, hoi chung. Truong hop co nhieu ma thi phan cach bang dau cham phay (;).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ma_icd_ts_bamsinh varchar(255);
COMMENT ON COLUMN ksk_phieu.ma_icd_ts_bamsinh IS 'Ma ICD-10 tien su benh/tat bam sinh. Ghi ma ICD-10 hoac ma trieu chung, hoi chung. Truong hop co nhieu ma thi phan cach bang dau cham phay (;).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS san_khoa numeric(1,0);
COMMENT ON COLUMN ksk_phieu.san_khoa IS 'San khoa. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS san_khoa_khong_bt numeric(1,0);
COMMENT ON COLUMN ksk_phieu.san_khoa_khong_bt IS 'San khoa khong binh thuong. 0:De thieu thang; 1:De thua can; 2:De co can thiep; 3:De ngat; 4:Me bi benh trong thoi ky mang thai.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ma_benh_san_khoa_khong_bt varchar(500);
COMMENT ON COLUMN ksk_phieu.ma_benh_san_khoa_khong_bt IS 'Ma ICD-10 benh gay ra san khoa khong binh thuong. Ghi ma ICD-10 hoac ma trieu chung, hoi chung. Co nhieu ma thi phan cach bang dau cham phay (;).';

-- =============================================================
--  Mau nguoi tu du 18 tuoi
-- =============================================================

-- Tien su benh (>=18t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_5nam numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_5nam IS 'Co benh hay bi thuong trong 5 nam qua. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_thankinh numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_thankinh IS 'Co benh than kinh hay bi thuong o dau. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_mat numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_mat IS 'Benh mat hoac giam thi luc. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_tai numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_tai IS 'Benh o tai, giam suc nghe hoac than bang. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_tim numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_tim IS 'Benh tim, nhoi mau co tim hoac cac benh tim mach khac. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS phauthuat_timmach numeric(1,0);
COMMENT ON COLUMN ksk_phieu.phauthuat_timmach IS 'Phau thuat can thiep tim, mach. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tang_huyetap numeric(1,0);
COMMENT ON COLUMN ksk_phieu.tang_huyetap IS 'Tang huyet ap. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS kho_tho numeric(1,0);
COMMENT ON COLUMN ksk_phieu.kho_tho IS 'Kho tho. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_phoi numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_phoi IS 'Benh phoi, hen, khi phem tham, viem phem quan man tinh. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_than_locmau numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_than_locmau IS 'Benh than, loc mau. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS nghien_ruoubia numeric(1,0);
COMMENT ON COLUMN ksk_phieu.nghien_ruoubia IS 'Nghien ruou, bia. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS dai_thaoduong numeric(1,0);
COMMENT ON COLUMN ksk_phieu.dai_thaoduong IS 'Dai thao duong hoac kiem soat tang duong huyet. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_tamthan numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_tamthan IS 'Benh tam than. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS roi_loan_ythuc numeric(1,0);
COMMENT ON COLUMN ksk_phieu.roi_loan_ythuc IS 'Mat y thuc hoac roi loan y thuc. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ngat_chongmat numeric(1,0);
COMMENT ON COLUMN ksk_phieu.ngat_chongmat IS 'Ngat hoac chong mat. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_tieuhoa numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_tieuhoa IS 'Benh tieu hoa. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS roi_loan_giacngu numeric(1,0);
COMMENT ON COLUMN ksk_phieu.roi_loan_giacngu IS 'Roi loan giac ngu, ngung tho khi ngu, ngu ru ban ngay hoac nay to. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS taibien_liet numeric(1,0);
COMMENT ON COLUMN ksk_phieu.taibien_liet IS 'Tai bien mach mau nao hoac liet. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_cotsong numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_cotsong IS 'Benh hoac ton thuong cot song. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS sudung_ruou numeric(1,0);
COMMENT ON COLUMN ksk_phieu.sudung_ruou IS 'Su dung ruou thuong xuyen, lien tuc. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS sudung_matuy numeric(1,0);
COMMENT ON COLUMN ksk_phieu.sudung_matuy IS 'Su dung ma tuy va chat gay nghien. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS benh_khac numeric(1,0);
COMMENT ON COLUMN ksk_phieu.benh_khac IS 'Benh khac. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ten_benh_khac varchar(255);
COMMENT ON COLUMN ksk_phieu.ten_benh_khac IS 'Ten benh khac.';

-- Can lam sang (>=18t)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_huyethoc varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_huyethoc IS 'Ket qua xet nghiem huyet hoc.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_duongmau varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_duongmau IS 'Ket qua sinh hoa mau, duong mau.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_ure varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_ure IS 'Ket qua xet nghiem Ure.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_creatinin varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_creatinin IS 'Ket qua xet nghiem Creatinin.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_asat varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_asat IS 'Ket qua xet nghiem ASAT (GOT).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_alat varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_alat IS 'Ket qua xet nghiem ALAT (GPT).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_tongphantich_nt varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_tongphantich_nt IS 'Ket qua tong phan tich nuoc tieu.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_nt_khac varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_nt_khac IS 'Ket qua xet nghiem nuoc tieu khac.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_xq_timphoi varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_xq_timphoi IS 'Ket qua chan doan hinh anh X-quang tim phoi thang.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_khac numeric(1,0);
COMMENT ON COLUMN ksk_phieu.cls_khac IS 'Co ket qua can lam sang khac. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS cls_khac_noidung varchar(500);
COMMENT ON COLUMN ksk_phieu.cls_khac_noidung IS 'Liet ke ket qua can lam sang khac (neu co).';

-- =============================================================
--  Mau kham suc khoe tam than
-- =============================================================

-- Tien su tam than
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ts_tamthan_giadinh numeric(1,0);
COMMENT ON COLUMN ksk_phieu.ts_tamthan_giadinh IS 'Tien su benh tam than gia dinh. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ten_tamthan_giadinh varchar(500);
COMMENT ON COLUMN ksk_phieu.ten_tamthan_giadinh IS 'Ten benh tam than cua gia dinh.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ts_tamthan_banthan numeric(1,0);
COMMENT ON COLUMN ksk_phieu.ts_tamthan_banthan IS 'Tien su benh tam than cua ban than. 1:Co; 0:Khong.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS ten_tamthan_banthan varchar(500);
COMMENT ON COLUMN ksk_phieu.ten_tamthan_banthan IS 'Ten benh tam than cua ban than.';

-- Kham tam than
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_bieuhien varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_bieuhien IS 'Bieu hien chung.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_ythuc varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_ythuc IS 'Y thuc.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_dinhhuong_khonggian varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_dinhhuong_khonggian IS 'Dinh huong luc (Khong gian).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_dinhhuong_thoigian varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_dinhhuong_thoigian IS 'Dinh huong luc (Thoi gian).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_dinhhuong_xungquanh varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_dinhhuong_xungquanh IS 'Dinh huong luc (Xung quanh).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_dinhhuong_banthan varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_dinhhuong_banthan IS 'Dinh huong luc (Ban than).';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_camxuc varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_camxuc IS 'Cam xuc.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_camgiac_trigiac varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_camgiac_trigiac IS 'Cam giac, tri giac.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_tuduy_hinhthuc varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_tuduy_hinhthuc IS 'Tu duy hinh thuc.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_tuduy_noidung varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_tuduy_noidung IS 'Tu duy noi dung.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_hanhvi_ychi varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_hanhvi_ychi IS 'Hanh vi, tac phong hoat dong co y chi.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_hanhvi_bannang varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_hanhvi_bannang IS 'Hanh vi, tac phong hoat dong co ban nang.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_trinho varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_trinho IS 'Tri nho.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_tritue varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_tritue IS 'Tri tue.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_chuy varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_chuy IS 'Chu y.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tamthan_khac varchar(500);
COMMENT ON COLUMN ksk_phieu.tamthan_khac IS 'Khac.';

-- Khac (tam than)
ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS tracnghiem_tamly varchar(500);
COMMENT ON COLUMN ksk_phieu.tracnghiem_tamly IS 'Trac nghiem tam ly.';

ALTER TABLE ksk_phieu ADD COLUMN IF NOT EXISTS canlamsang_khac_bs varchar(500);
COMMENT ON COLUMN ksk_phieu.canlamsang_khac_bs IS 'Can lam sang khac theo chi dinh cua bac si.';

-- =====================================================================
--  fn_ksk_phieu_get
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_ksk_phieu_get(p_id INT)
RETURNS jsonb
LANGUAGE sql
AS $$
    SELECT to_jsonb(p) FROM (
        SELECT k.id, k.benhnhan_id, k.loai_mau_ksk, k.ngay_ksk, k.coso_kham, k.lydo_ksk,
               b.holot, b.ten, b.ngaysinh, b.gioitinh, b.cmnd, b.ngaycap, b.noicap,
               b.madt, b.madtuong, b.nhom_mau, b.diachi, b.manghe, b.noict,
               k.hoten_qh, k.loaiqh, k.dienthoai_qh, k.cmnd_qh,
               k.nhietdo, k.mach, k.nhiptho, k.chieucao, k.cannang, k.huyetap,
               k.tsbenh_co_benhbamsinh, k.tsbenh_tenbenh,
               k.tsgd_co_benh_truyennhiem, k.tsgd_tenbenh_truyennhiem, k.dang_dtbenh,
               k.tuanhoan_ksk, k.hohap_ksk, k.tieuhoa_ksk, k.than_ksk,
               k.thankinh_ksk, k.tamthan_ksk, k.khamls_khac_ksk,
               k.kmatphai_ksk, k.kmattrai_ksk, k.matphai_ksk, k.mattrai_ksk,
               k.tnoithuong_ksk, k.tnoitham_ksk, k.noithuong_ksk, k.noitham_ksk,
               k.hamtren_ksk, k.hamduoi_ksk, k.rhmbenh_ksk, k.kqcls_ksk,
               k.ngoaikhoa_ksk, k.plngoaikhoa_ksk,
               k.dalieu_ksk, k.pldalieu_ksk,
               k.sanphukhoa_ksk, k.plsanphukhoa_ksk,
               -- Tre em <6t
               k.ts_tiepxuc_lao, k.chieudai_tuoi_sd, k.cannang_tuoi_sd,
               k.trangthai_vongdau, k.chuvi_vongcanhtay, k.tinhtrang_dinhduong,
               k.phattrien_tinhthan, k.phattrien_vandong, k.nguyco_tuky,
               k.benh_lao, k.tiem_vgb_mui1, k.tiemchung_daydu,
               k.mausac_da, k.long_bantay, k.thop, k.hinhdang_dau,
               k.vandong_co, k.khoibatthuong_dauco,
               k.vitri_haimat, k.mimat_ketmac, k.dongtu, k.lacmat,
               k.tai_mangnhi, k.dapung_amthanh, k.khoisung_sautai, k.chaymu_nuoctai,
               k.hinhdang_mui, k.chaynuoc_mui, k.nghet_mui, k.hong,
               k.hinhdang_mieng, k.rangsua_sosinh, k.hinhdang_luoi,
               k.dinh_thangluoi, k.nam_mieng, k.cam_tut, k.vetsau_rang,
               k.nhiptho_khongdeu, k.thorutlom_longnguc, k.tiengtho_batthuong,
               k.dauhieu_suyhohap, k.nghe_phoi,
               k.vitri_momtim, k.mach_ngoaivi, k.nghe_tim,
               k.hinhdang_bung_ron, k.gan_lach_to, k.khoibatthuong_bung,
               k.lo_haumon, k.cqsd_ngoai,
               k.vandong_khongdoixung, k.phanxa_bu, k.phanxa_nam, k.phanxa_moro,
               k.truongluc_co, k.khop_hang, k.phanxa_co, k.lung_cotsong,
               k.tuchi_khop, k.dang_di,
               k.doituong, k.sinh_non, k.tuanthai_khi_sinh, k.nguon_chitra,
               k.trangthai_nhietdo, k.trangthai_mach, k.trangthai_nhiptho,
               k.trangthai_huyetap,
               k.ketluan_binhthuong, k.ketluan_nguyco_lao,
               k.ketluan_vandesuckhoe, k.ketluan_ghiro,
               -- 6-18t
               k.ma_icd_tsgd_bamsinh, k.ma_icd_ts_bamsinh,
               k.san_khoa, k.san_khoa_khong_bt, k.ma_benh_san_khoa_khong_bt,
               -- >=18t
               k.benh_5nam, k.benh_thankinh, k.benh_mat, k.benh_tai,
               k.benh_tim, k.phauthuat_timmach, k.tang_huyetap, k.kho_tho,
               k.benh_phoi, k.benh_than_locmau, k.nghien_ruoubia,
               k.dai_thaoduong, k.benh_tamthan, k.roi_loan_ythuc,
               k.ngat_chongmat, k.benh_tieuhoa, k.roi_loan_giacngu,
               k.taibien_liet, k.benh_cotsong, k.sudung_ruou, k.sudung_matuy,
               k.benh_khac, k.ten_benh_khac,
               k.cls_huyethoc, k.cls_duongmau, k.cls_ure, k.cls_creatinin,
               k.cls_asat, k.cls_alat, k.cls_tongphantich_nt, k.cls_nt_khac,
               k.cls_xq_timphoi, k.cls_khac, k.cls_khac_noidung,
               -- Tam than
               k.ts_tamthan_giadinh, k.ten_tamthan_giadinh,
               k.ts_tamthan_banthan, k.ten_tamthan_banthan,
               k.tamthan_bieuhien, k.tamthan_ythuc,
               k.tamthan_dinhhuong_khonggian, k.tamthan_dinhhuong_thoigian,
               k.tamthan_dinhhuong_xungquanh, k.tamthan_dinhhuong_banthan,
               k.tamthan_camxuc, k.tamthan_camgiac_trigiac,
               k.tamthan_tuduy_hinhthuc, k.tamthan_tuduy_noidung,
               k.tamthan_hanhvi_ychi, k.tamthan_hanhvi_bannang,
               k.tamthan_trinho, k.tamthan_tritue, k.tamthan_chuy, k.tamthan_khac,
               k.tracnghiem_tamly, k.canlamsang_khac_bs,
               k.created_at, k.updated_at
        FROM ksk_phieu k
        JOIN ksk_benhnhan b ON b.id = k.benhnhan_id
        WHERE k.id = p_id
    ) p;
$$;

-- =====================================================================
--  fn_ksk_phieu_list — nhan filter object, tra JSON array
--  filter: {"keyword": "...", "loai_mau_ksk": 1|2|3|4, "from_date": "...", "to_date": "..."}
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_ksk_phieu_list(p_filter jsonb)
RETURNS jsonb
LANGUAGE plpgsql
AS $$$
DECLARE
    v_keyword   TEXT    := NULLIF(p_filter->>'keyword', '');
    v_loai      INT     := CASE WHEN p_filter ? 'loai_mau_ksk'
                                THEN (p_filter->>'loai_mau_ksk')::int ELSE NULL END;
    v_from      DATE    := NULLIF(p_filter->>'from_date','')::date;
    v_to        DATE    := NULLIF(p_filter->>'to_date','')::date;
    v_result    jsonb;
BEGIN
    SELECT COALESCE(jsonb_agg(to_jsonb(t) ORDER BY t.id), '[]'::jsonb)
    INTO v_result
    FROM (
        SELECT k.id, k.benhnhan_id, k.loai_mau_ksk, k.ngay_ksk, k.coso_kham,
               b.holot, b.ten, b.ngaysinh, b.gioitinh
        FROM ksk_phieu k
        JOIN ksk_benhnhan b ON b.id = k.benhnhan_id
        WHERE (v_keyword IS NULL OR b.holot ILIKE '%'||v_keyword||'%'
                                   OR b.ten ILIKE '%'||v_keyword||'%')
          AND (v_loai    IS NULL OR k.loai_mau_ksk = v_loai)
          AND (v_from    IS NULL OR k.ngay_ksk >= v_from)
          AND (v_to      IS NULL OR k.ngay_ksk <= v_to)
    ) t;

    RETURN v_result;
END;
$$$;

-- =====================================================================
--  fn_ksk_phieu_upsert — nhan payload, insert/update, validate toi thieu
--  payload: {"id": null|int, "benhnhan_id": int, "loai_mau_ksk": 1|2|3|4,
--            "holot": "...", "ten": "...", "ngaysinh": "yyyy-MM-dd"|null, ...}
--  return : {"success": bool, "data": {...}, "errors": [{"field","message"}]}
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_ksk_phieu_upsert(p_payload jsonb)
RETURNS jsonb
LANGUAGE plpgsql
AS $$$
DECLARE
    v_id            INT     := NULLIF(p_payload->>'id','')::int;
    v_benhnhan_id   INT     := NULLIF(p_payload->>'benhnhan_id','')::int;
    v_loai          INT     := NULLIF(p_payload->>'loai_mau_ksk','')::int;
    v_holot         TEXT    := NULLIF(p_payload->>'holot','');
    v_ten           TEXT    := NULLIF(p_payload->>'ten','');
    v_ngaysinh      DATE    := NULLIF(p_payload->>'ngaysinh','')::date;
    v_errors        jsonb   := '[]'::jsonb;
    v_bnhn_id       INT;
    v_row           ksk_phieu%ROWTYPE;
BEGIN
    -- Validate toi thieu
    IF v_loai IS NULL OR v_loai NOT IN (1,2,3,4) THEN
        v_errors := v_errors || jsonb_build_object('field','loai_mau_ksk','message','Loai mau KSK bat buoc (1-4).');
    END IF;
    IF v_ten IS NULL THEN
        v_errors := v_errors || jsonb_build_object('field','ten','message','Ho ten benh nhan bat buoc.');
    END IF;
    IF v_ngaysinh IS NULL THEN
        v_errors := v_errors || jsonb_build_object('field','ngaysinh','message','Ngay sinh bat buoc.');
    END IF;

    IF jsonb_array_length(v_errors) > 0 THEN
        RETURN jsonb_build_object('success', false, 'data', NULL, 'errors', v_errors);
    END IF;

    -- Insert/update benhnhan (toi gian: 1 benhnhan per phieu trong demo)
    IF v_benhnhan_id IS NULL THEN
        INSERT INTO ksk_benhnhan (holot, ten, ngaysinh, gioitinh)
        VALUES (v_holot, v_ten, v_ngaysinh,
                CASE WHEN p_payload ? 'gioitinh' THEN (p_payload->>'gioitinh')::numeric ELSE NULL END)
        RETURNING id INTO v_bnhn_id;
    ELSE
        UPDATE ksk_benhnhan SET
            holot   = v_holot,
            ten     = v_ten,
            ngaysinh = v_ngaysinh,
            gioitinh = CASE WHEN p_payload ? 'gioitinh' THEN (p_payload->>'gioitinh')::numeric ELSE gioitinh END
        WHERE id = v_benhnhan_id;
        v_bnhn_id := v_benhnhan_id;
    END IF;

    -- Upsert phieu — lay tat ca cot tu payload (bo id/benhnhan_id/created_at/updated_at)
    IF v_id IS NULL THEN
        INSERT INTO ksk_phieu (benhnhan_id, loai_mau_ksk)
        VALUES (v_bnhn_id, v_loai)
        RETURNING * INTO v_row;

        -- Cap nhat cac cot khac tu payload
        UPDATE ksk_phieu SET
            ngay_ksk     = NULLIF(p_payload->>'ngay_ksk','')::date,
            coso_kham    = NULLIF(p_payload->>'coso_kham',''),
            lydo_ksk     = NULLIF(p_payload->>'lydo_ksk',''),
            hoten_qh     = NULLIF(p_payload->>'hoten_qh',''),
            nhietdo      = NULLIF(p_payload->>'nhietdo','')::numeric,
            mach         = NULLIF(p_payload->>'mach','')::numeric,
            nhiptho      = NULLIF(p_payload->>'nhiptho','')::numeric,
            chieucao     = NULLIF(p_payload->>'chieucao','')::numeric,
            cannang      = NULLIF(p_payload->>'cannang','')::numeric,
            huyetap      = NULLIF(p_payload->>'huyetap',''),
            updated_at   = now()
        WHERE id = v_row.id;
    ELSE
        UPDATE ksk_phieu SET
            benhnhan_id  = v_bnhn_id,
            loai_mau_ksk = v_loai,
            ngay_ksk     = NULLIF(p_payload->>'ngay_ksk','')::date,
            coso_kham    = NULLIF(p_payload->>'coso_kham',''),
            lydo_ksk     = NULLIF(p_payload->>'lydo_ksk',''),
            hoten_qh     = NULLIF(p_payload->>'hoten_qh',''),
            nhietdo      = NULLIF(p_payload->>'nhietdo','')::numeric,
            mach         = NULLIF(p_payload->>'mach','')::numeric,
            nhiptho      = NULLIF(p_payload->>'nhiptho','')::numeric,
            chieucao     = NULLIF(p_payload->>'chieucao','')::numeric,
            cannang      = NULLIF(p_payload->>'cannang','')::numeric,
            huyetap      = NULLIF(p_payload->>'huyetap',''),
            updated_at   = now()
        WHERE id = v_id
        RETURNING * INTO v_row;

        IF NOT FOUND THEN
            RETURN jsonb_build_object('success', false, 'data', NULL,
                'errors', jsonb_build_array(jsonb_build_object('field','id','message','Khong tim thay ban ghi de cap nhat.')));
        END IF;
    END IF;

    RETURN jsonb_build_object(
        'success', true,
        'data', jsonb_build_object(
            'id', v_row.id, 'benhnhan_id', v_bnhn_id, 'loai_mau_ksk', v_row.loai_mau_ksk),
        'errors', '[]'::jsonb);
END;
$$$;

-- =====================================================================
--  fn_ksk_phieu_delete
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_ksk_phieu_delete(p_id INT)
RETURNS jsonb
LANGUAGE plpgsql
AS $$$
BEGIN
    DELETE FROM ksk_phieu WHERE id = p_id;
    IF FOUND THEN
        RETURN jsonb_build_object('success', true, 'message', 'Da xoa phieu KSK.');
    ELSE
        RETURN jsonb_build_object('success', false, 'message', 'Khong tim thay phieu KSK de xoa.');
    END IF;
END;
$$$;

-- ---------- du lieu mau ----------
-- Benh nhan
INSERT INTO ksk_benhnhan (holot, ten, ngaysinh, gioitinh, diachi)
VALUES (N'Nguyen Van', N'A', '2024-03-15', 1, N'Ha Noi'),
       (N'Tran Thi',    N'B', '1990-01-10', 0, N'TPHCM'),
       (N'Le Van',       N'C', '2008-06-20', 1, N'Da Nang')
ON CONFLICT DO NOTHING;

-- Phieu KSK
INSERT INTO ksk_phieu (benhnhan_id, loai_mau_ksk, ngay_ksk, coso_kham, lydo_ksk, nhietdo, mach, nhiptho, chieucao, cannang, huyetap)
VALUES (1, 1, '2026-07-20', N'BV Viet Duc', N'KSK dinh ky', 36.5, 80, 30, 65.0, 8.0, N'90/60'),
       (2, 3, '2026-07-22', N'BV Bach Mai', N'KSK viec lam', 36.8, 72, 20, 160.0, 55.0, N'120/80'),
       (3, 2, '2026-07-23', N'BV Da Nang',   N'KSK hoc sinh', 37.0, 85, 25, 140.0, 40.0, N'110/70')
ON CONFLICT DO NOTHING;
