<div align="center">

`Công ty TNHH Giải Pháp Kỹ Thuật Số DH - Mẫu: DH-02: Mô tả thay đổi hệ thống DHG.Hospital 3.1`

</div>
<div align="center">
  <h1>PHIẾU MÔ TẢ THAY ĐỔI HỆ THỐNG</h1>  
</div>
<div align="center">

#### CHỦ ĐỀ: BỔ SUNG MẪU KSK VÀ KSK ĐỊNH KỲ THEO THÔNG TƯ SỐ 25/2026/TT-BYT

</div>

###### :eight_spoked_asterisk: Người lập: [**Lê Quốc Thống**](https://github.com/lequocthong29)

###### :eight_spoked_asterisk: Ngày lập: **22/07/2026**

###### :eight_spoked_asterisk: Phân tích dữ liệu.
I. CÁC GIÁ TRỊ ĐÃ CÓ TRƯỜNG LƯU TRỮ.

| STT | Trường thông tin            | Bảng/Cột lưu trữ                                                                                                         |
| --: | --------------------------- | ------------------------------------------------------------------------------------------------------------------------ |
|   1 | Họ và tên                   | `dmbenhnhan.holot` + `dmbenhnhan.ten`                                                                                    |
|   2 | Ngày sinh                   | `dmbenhnhan.ngaysinh`                                                                                                    |
|   3 | CCCD                        | `dmbenhnhan.cmnd`                                                                                                        |
|   4 | Ngày cấp                    | `dmbenhnhan.ngaycap`                                                                                                     |
|   5 | Nơi cấp                     | `dmbenhnhan.noicap`                                                                                                      |
|   6 | Giới tính                   | `dmbenhnhan.gioitinh`                                                                                                    |
|   7 | Dân tộc                     | `dmbenhnhan.madt`                                                                                                        |
|   8 | Đối tượng                   | `dmbenhnhan.madtuong`                                                                                                    |
|   9 | Nhóm máu                    | `dmbenhnhan.nhom_mau`                                                                                                    |
|  10 | Địa chỉ                     | `dmbenhnhan.diachi`                                                                                                      |
|  11 | Nghề nghiệp                 | `dmbenhnhan.manghe`                                                                                                      |
|  12 | Nơi học tập, làm việc       | `dmbenhnhan.noict`                                                                                                       |
|  13 | Người giám hộ               | `psdangky.hotenqh`                                                                                                       |
|  14 | Loại quan hệ                | `psdangky.loaiqh`                                                                                                        |
|  15 | SĐT người giám hộ           | `psdangky.dienthoaiqh`                                                                                                   |
|  16 | Mã định danh người giám hộ  | `psdangky.cmndqh`                                                                                                        |
|  17 | Ngày khám                   | `khambenh.ngaykcb`                                                                                                       |
|  18 | Cơ sở khám                  | `tenbv`                                                                                                                  |
|  19 | Nhiệt độ                    | `psdangky.nhietdo`                                                                                                       |
|  20 | Mạch                        | `psdangky.mach`                                                                                                          |
|  21 | Nhịp thở                    | `psdangky.nhiptho`                                                                                                       |
|  22 | Lý do khám sức khỏe         | `pskhamsuckhoe.lydo_ksk`                                                                                                 |
|  23 | Tiền sử gia đình            | `pskhamsuckhoe.tsbenh_co_benhbamsinh`, `pskhamsuckhoe.tsbenh_tenbenh`                                                    |
|  24 | Tiền sử bản thân            | `pskhamsuckhoe.tsgd_co_benh_truyennhiem`, `pskhamsuckhoe.tsgd_tenbenh_truyennhiem`                                       |
|  25 | Hiện tại đang điều trị bệnh | `pskhamsuckhoe.dang_dtbenh`                                                                                              |
|  26 | Chiều cao                   | `psdangky.chieucao`                                                                                                      |
|  27 | Cân nặng                    | `psdangky.cannang`                                                                                                       |
|  28 | Huyết áp                    | `psdangky.huyetap`                                                                                                       |
|  29 | Tuần hoàn                   | `pskhamsuckhoe.tuanhoan_ksk`                                                                                             |
|  30 | Hô hấp                      | `pskhamsuckhoe.hohap_ksk`                                                                                                |
|  31 | Tiêu hóa                    | `pskhamsuckhoe.tieuhoa_ksk`                                                                                              |
|  32 | Thận - Tiết niệu - Sinh dục | `pskhamsuckhoe.than_ksk`                                                                                                 |
|  33 | Thần kinh                   | `pskhamsuckhoe.thankinh_ksk`                                                                                             |
|  34 | Tâm thần                    | `pskhamsuckhoe.tamthan_ksk`                                                                                              |
|  35 | Khám lâm sàng khác          | `pskhamsuckhoe.khamls_khac_ksk`                                                                                          |
|  36 | Mắt không kính              | `pskhamsuckhoe.kmatphai_ksk`, `pskhamsuckhoe.kmattrai_ksk`                                                               |
|  37 | Mắt có kính                 | `pskhamsuckhoe.matphai_ksk`, `pskhamsuckhoe.mattrai_ksk`                                                                 |
|  38 | Tai                         | `pskhamsuckhoe.tnoithuong_ksk`, `pskhamsuckhoe.tnoitham_ksk`, `pskhamsuckhoe.noithuong_ksk`, `pskhamsuckhoe.noitham_ksk` |
|  39 | Răng                        | `pskhamsuckhoe.hamtren_ksk`, `pskhamsuckhoe.hamduoi_ksk`                                                                 |
|  40 | Có bệnh RHM                 | `pskhamsuckhoe.rhmbenh_ksk`                                                                                              |
|  41 | Kết quả CLS                 | `pskhamsuckhoe.kqcls_ksk`                                                                                                |
|  42 | Ngoại khoa                  | `pskhamsuckhoe.ngoaikhoa_ksk`, `pskhamsuckhoe.plngoaikhoa_ksk`                                                           |
|  43 | Da liễu                     | `pskhamsuckhoe.dalieu_ksk`, `pskhamsuckhoe.pldalieu_ksk`                                                                 |
|  44 | Sản phụ khoa                | `pskhamsuckhoe.sanphukhoa_ksk`, `pskhamsuckhoe.plsanphukhoa_ksk`                                                         |

II. CÁC GIÁ TRỊ CHƯA CÓ TRƯỜNG LƯU TRỮ 
  1. Mẫu KSK trẻ em dưới 06 tuổi

| STT | Trường thông tin                              |  Kiểu dữ liệu |
| --: | --------------------------------------------- | :-----------: |
|   1 | Tiền sử tiếp xúc với người bệnh lao           |  numeric(1,0) |
|   2 | Chiều dài/tuổi (SD)                           | numeric(10,2) |
|   3 | Cân nặng/tuổi (SD)                            | numeric(10,2) |
|   4 | Trạng thái vòng đầu                           |  numeric(1,0) |
|   5 | Chu vi vòng cánh tay (mm)                     | numeric(10,2) |
|   6 | Tình trạng dinh dưỡng                         |  varchar(255) |
|   7 | Phát triển tinh thần bình thường theo độ tuổi |  numeric(1,0) |
|   8 | Phát triển vận động bình thường theo độ tuổi  |  numeric(1,0) |
|   9 | Trẻ có nguy cơ tự kỷ                          |  numeric(1,0) |
|  10 | Lao                                           |  numeric(1,0) |
|  11 | Viêm gan B mũi 1                              |  numeric(1,0) |
|  12 | Tiêm chủng đầy đủ theo độ tuổi                |  numeric(1,0) |
|  13 | Màu sắc da                                    |  numeric(1,0) |
|  14 | Lòng bàn tay                                  |  numeric(1,0) |
|  15 | Thóp                                          |  numeric(1,0) |
|  16 | Kích thước và hình dáng đầu                   |  numeric(1,0) |
|  17 | Vận động cổ                                   |  numeric(1,0) |
|  18 | Khối bất thường                               |  numeric(1,0) |
|  19 | Vị trí 2 mắt                                  |  numeric(1,0) |
|  20 | Mí mắt và kết mạc                             |  numeric(1,0) |
|  21 | Đồng tử                                       |  numeric(1,0) |
|  22 | Lác mắt                                       |  numeric(1,0) |
|  23 | Tai và màng nhĩ                               |  numeric(1,0) |
|  24 | Đáp ứng với âm thanh                          |  numeric(1,0) |
|  25 | Có khối sưng sau tai                          |  numeric(1,0) |
|  26 | Dấu hiệu chảy mủ, nước tai                    |  numeric(1,0) |
|  27 | Hình dạng mũi                                 |  numeric(1,0) |
|  28 | Chảy nước mũi                                 |  numeric(1,0) |
|  29 | Nghẹt mũi                                     |  numeric(1,0) |
|  30 | Họng                                          |  numeric(1,0) |
|  31 | Hình dạng miệng                  | numeric(1,0) |
|  32 | Răng sữa sơ sinh                 | numeric(1,0) |
|  33 | Hình dạng lưỡi                   | numeric(1,0) |
|  34 | Dính thắng lưỡi                  | numeric(1,0) |
|  35 | Nấm miệng                        | numeric(1,0) |
|  36 | Cằm nhỏ, tụt về sau              | numeric(1,0) |
|  37 | Vết sâu, mảng bám, lỗ trên răng  | numeric(1,0) |
|  38 | Nhịp thở không đều               | numeric(1,0) |
|  39 | Thở rút lõm lồng ngực            | numeric(1,0) |
|  40 | Tiếng thở bất thường             | numeric(1,0) |
|  41 | Dấu hiệu suy hô hấp              | numeric(1,0) |
|  42 | Nghe phổi                        | numeric(1,0) |
|  43 | Vị trí mỏm tim                   | numeric(1,0) |
|  44 | Mạch ngoại vi                    | numeric(1,0) |
|  45 | Nghe tim (loạn nhịp, tiếng thổi) | numeric(1,0) |
|  46 | Hình dáng bụng, rốn              | numeric(1,0) |
|  47 | Gan, lách to                     | numeric(1,0) |
|  48 | Khối bất thường vùng bụng        | numeric(1,0) |
|  49 | Lỗ hậu môn                       | numeric(1,0) |
|  50 | Cơ quan sinh dục ngoài           | numeric(1,0) |
|  51 | Vận động không đối xứng          | numeric(1,0) |
|  52 | Phản xạ bú                       | numeric(1,0) |
|  53 | Phản xạ nắm                      | numeric(1,0) |
|  54 | Phản xạ Moro                     | numeric(1,0) |
|  55 | Trương lực cơ                    | numeric(1,0) |
|  56 | Khớp háng                        | numeric(1,0) |
|  57 | Phản xạ cơ                       | numeric(1,0) |
|  58 | Kiểm tra lưng, cột sống          | numeric(1,0) |
|  59 | Khám tứ chi và khớp              | numeric(1,0) |
|  60 | Quan sát dáng đi                 | numeric(1,0) |
|  61 | Đối tượng                        | varchar(255) |
|  62 | Sinh non                         | numeric(1,0) |
|  63 | Tuần thai khi sinh               | varchar(255) |
|  64 | Nguồn chi trả                    | varchar(255) |
|  65 | trạng thái nhiệt đô              | numeric(1,0) |
|  66 | trạng thái mạch                  | numeric(1,0) |
|  67 | trạng thái nhịp thở              | numeric(1,0) |
|  68 | trạng thái mạch                  | numeric(1,0) |
|  69 | Kết luận - bình thường           | varchar(500) |
|  70 | Kết luận - Có nguy cơ mắc Lao    | varchar(500) |
|  71 | Kết luận - Có vấn đề sức khỏe    | varchar(500) |
|  72 | Kết luận - Ghi rõ                | varchar(500) |

  2. Mẫu KSK người từ 6 tuổi đến dưới 18 tuổi.

| STT | Trường thông tin                      | Kiểu dữ liệu  |
| --: | ------------------------------------- | ------------- |
|   1 | Mã ICD-10 theo tiểu sử bệnh gia đình bẩm sinh | varchar(255)  |
|   2 | Mã ICD-10 theo tiểu sử bệnh tật bẩm sinh      | varchar(255)  |
|   3 | Sản khoa                                      | numeric(1,0)  |
|   4 | Sản khoa không bình thường                    | numeric(1,0)  |
|   5 | Mã bệnh sản khoa không bình thường            | varchar(500)  |

  3. Mẫu KSK người từ đủ 18 tuổi.
     
| STT | Trường thông tin                      | Kiểu dữ liệu  |
| --: | ------------------------------------- | ------------- |
|   1 | Có bệnh hay bị thương trong 5 năm qua | numeric(1,0)  |
|   2 | Bệnh thần kinh hoặc chấn thương đầu   | numeric(1,0)  |
|   3 | Bệnh mắt hoặc giảm thị lực            | numeric(1,0)  |
|   4 | Bệnh tai, giảm sức nghe               | numeric(1,0)  |
|   5 | Bệnh tim                              | numeric(1,0)  |
|   6 | Phẫu thuật can thiệp tim mạch         | numeric(1,0)  |
|   7 | Tăng huyết áp                         | numeric(1,0)  |
|   8 | Khó thở                               | numeric(1,0)  |
|   9 | Bệnh phổi                             | numeric(1,0)  |
|  10 | Bệnh thận, lọc máu                    | numeric(1,0)  |
|  11 | Nghiện rượu, bia                      | numeric(1,0)  |
|  12 | Đái tháo đường                        | numeric(1,0)  |
|  13 | Bệnh tâm thần                         | numeric(1,0)  |
|  14 | Rối loạn ý thức                       | numeric(1,0)  |
|  15 | Ngất, chóng mặt                       | numeric(1,0)  |
|  16 | Bệnh tiêu hóa                         | numeric(1,0)  |
|  17 | Rối loạn giấc ngủ                     | numeric(1,0)  |
|  18 | Tai biến mạch máu não                 | numeric(1,0)  |
|  19 | Bệnh cột sống                         | numeric(1,0)  |
|  20 | Sử dụng rượu thường xuyên             | numeric(1,0)  |
|  21 | Sử dụng ma túy                        | numeric(1,0)  |
|  22 | Bệnh khác                             | numeric(1,0)  |
|  23 | Tên bệnh khác                         | varchar(500) |
|  24 | Huyết học                             | varchar(500) |
|  25 | Sinh hóa máu, đường máu               | varchar(500) |
|  26 | Urê                                   | varchar(500) |
|  27 | Creatinin                             | varchar(500) |
|  28 | ASAT (GOT)                            | varchar(500) |
|  29 | ALAT (GPT)                            | varchar(500) |
|  30 | Tổng phân tích nước tiểu              | varchar(500) |
|  31 | Xét nghiệm nước tiểu khác             | varchar(500) |
|  32 | XQ tim phổi thẳng                     | varchar(500) |
|  33 | Có CLS khác                           | numeric(1,0)  |
|  34 | Nội dung CLS khác                     | varchar(500) |

4. Mẫu khám sức khỏe tâm thần.

| STT | Trường thông tin                   | Kiểu dữ liệu  |
| --: | ---------------------------------- | ------------- |
|   1 | Tiền sử bệnh tâm thần gia đình     | numeric(1,0)  |
|   2 | Tên bệnh tâm thần gia đình         | varchar(500) |
|   3 | Tiền sử bệnh tâm thần bản thân     | numeric(1,0)  |
|   4 | Tên bệnh tâm thần bản thân         | varchar(500) |
|   5 | Biểu hiện chung                    | varchar(500) |
|   6 | Ý thức                             | varchar(500) |
|   7 | Định hướng lực (Không gian)        | varchar(500) |
|   8 | Định hướng lực (Thời gian)         | varchar(500) |
|   9 | Định hướng lực (Xung quanh)        | varchar(500) |
|  10 | Định hướng lực (Bản thân)          | varchar(500) |
|  11 | Cảm xúc                            | varchar(500) |
|  12 | Cảm giác, tri giác                 | varchar(500) |
|  13 | Tư duy hình thức                   | varchar(500) |
|  14 | Tư duy nội dung                    | varchar(500) |
|  15 | Hành vi, tác phong có ý chí        | varchar(500) |
|  16 | Hành vi, tác phong có bản năng     | varchar(500) |
|  17 | Trí nhớ                            | varchar(500) |
|  18 | Trí tuệ                            | varchar(500) |
|  19 | Chú ý                              | varchar(500) |
|  20 | Khác                               | varchar(500) |
|  21 | Trắc nghiệm tâm lý                 | varchar(500) |
|  22 | Cận lâm sàng khác theo chỉ định BS | varchar(500) |

:blue_book: Cập nhật cấu trúc
###### :eight_spoked_asterisk: Bổ sung thêm các tường còn thiếu vào bảng `current.pskhamsuckhoe`
- Add các trường còn thiếu của mẫu khám sức khỏe và khám sức khoẻ định kỳ dùng cho trẻ em dưới 06 tuổi:
```SQL
ALTER TABLE current.pskhamsuckhoe

-- Tiền sử
ADD COLUMN ts_tiepxuc_lao numeric(1,0),
ADD COLUMN chieudai_tuoi_sd numeric(10,2),
ADD COLUMN cannang_tuoi_sd numeric(10,2),
ADD COLUMN trangthai_vongdau numeric(1,0),
ADD COLUMN chuvi_vongcanhtay numeric(10,2),
ADD COLUMN tinhtrang_dinhduong varchar(255),
ADD COLUMN phattrien_tinhthan numeric(1,0),
ADD COLUMN phattrien_vandong numeric(1,0),
ADD COLUMN nguyco_tuky numeric(1,0),
ADD COLUMN benh_lao numeric(1,0),
ADD COLUMN tiem_vgb_mui1 numeric(1,0),
ADD COLUMN tiemchung_daydu numeric(1,0),

-- Da - Đầu - Cổ
ADD COLUMN mausac_da numeric(1,0),
ADD COLUMN long_bantay numeric(1,0),
ADD COLUMN thop numeric(1,0),
ADD COLUMN hinhdang_dau numeric(1,0),
ADD COLUMN vandong_co numeric(1,0),
ADD COLUMN khoibatthuong_dauco numeric(1,0),

-- Mắt
ADD COLUMN vitri_haimat numeric(1,0),
ADD COLUMN mimat_ketmac numeric(1,0),
ADD COLUMN dongtu numeric(1,0),
ADD COLUMN lacmat numeric(1,0),

-- Tai
ADD COLUMN tai_mangnhi numeric(1,0),
ADD COLUMN dapung_amthanh numeric(1,0),
ADD COLUMN khoisung_sautai numeric(1,0),
ADD COLUMN chaymu_nuoctai numeric(1,0),

-- Mũi
ADD COLUMN hinhdang_mui numeric(1,0),
ADD COLUMN chaynuoc_mui numeric(1,0),
ADD COLUMN nghet_mui numeric(1,0),

-- Miệng
ADD COLUMN hong numeric(1,0),
ADD COLUMN hinhdang_mieng numeric(1,0),
ADD COLUMN rangsua_sosinh numeric(1,0),
ADD COLUMN hinhdang_luoi numeric(1,0),
ADD COLUMN dinh_thangluoi numeric(1,0),
ADD COLUMN nam_mieng numeric(1,0),
ADD COLUMN cam_tut numeric(1,0),
ADD COLUMN vetsau_rang numeric(1,0),

-- Hô hấp
ADD COLUMN nhiptho_khongdeu numeric(1,0),
ADD COLUMN thorutlom_longnguc numeric(1,0),
ADD COLUMN tiengtho_batthuong numeric(1,0),
ADD COLUMN dauhieu_suyhohap numeric(1,0),
ADD COLUMN nghe_phoi numeric(1,0),

-- Tim mạch
ADD COLUMN vitri_momtim numeric(1,0),
ADD COLUMN mach_ngoaivi numeric(1,0),
ADD COLUMN nghe_tim numeric(1,0),

-- Tiêu hóa
ADD COLUMN hinhdang_bung_ron numeric(1,0),
ADD COLUMN gan_lach_to numeric(1,0),
ADD COLUMN khoibatthuong_bung numeric(1,0),
ADD COLUMN lo_haumon numeric(1,0),
ADD COLUMN cqsd_ngoai numeric(1,0),

-- Thần kinh
ADD COLUMN vandong_khongdoixung numeric(1,0),
ADD COLUMN phanxa_bu numeric(1,0),
ADD COLUMN phanxa_nam numeric(1,0),
ADD COLUMN phanxa_moro numeric(1,0),
ADD COLUMN truongluc_co numeric(1,0),

-- Cơ xương khớp
ADD COLUMN khop_hang numeric(1,0),
ADD COLUMN phanxa_co numeric(1,0),
ADD COLUMN lung_cotsong numeric(1,0),
ADD COLUMN tuchi_khop numeric(1,0),
ADD COLUMN dang_di numeric(1,0);

ADD COLUMN doituong varchar(255),
ADD COLUMN sinh_non numeric(1,0),
ADD COLUMN tuanthai_khi_sinh varchar(255),
ADD COLUMN nguon_chitra varchar(255),

ADD COLUMN trangthai_nhietdo numeric(1,0),
ADD COLUMN trangthai_mach numeric(1,0),
ADD COLUMN trangthai_nhiptho numeric(1,0),
ADD COLUMN trangthai_huyetap numeric(1,0),

ADD COLUMN ketluan_binhthuong varchar(500),
ADD COLUMN ketluan_nguyco_lao varchar(500),
ADD COLUMN ketluan_vandesuckhoe varchar(500),
ADD COLUMN ketluan_ghiro varchar(500);


COMMENT ON COLUMN current.pskhamsuckhoe.ts_tiepxuc_lao IS
'Tiền sử tiếp xúc với người bệnh lao. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.chieudai_tuoi_sd IS
'Chiều dài theo tuổi (SD).';

COMMENT ON COLUMN current.pskhamsuckhoe.cannang_tuoi_sd IS
'Cân nặng theo tuổi (SD).';

COMMENT ON COLUMN current.pskhamsuckhoe.trangthai_vongdau IS
'Trạng thái vòng đầu. 0:Bình thường; 1:Đầu to; 2:Đầu nhỏ.';

COMMENT ON COLUMN current.pskhamsuckhoe.chuvi_vongcanhtay IS
'Chu vi vòng cánh tay (mm).';

COMMENT ON COLUMN current.pskhamsuckhoe.tinhtrang_dinhduong IS
'Tình trạng dinh dưỡng. 0:Bình thường; 1:Phù dinh dưỡng; 2:Dấu hiệu thiếu máu; 3:Dấu hiệu còi xương; 4:Suy dinh dưỡng; 5:Thừa cân, béo phì.\n Có thể chọn nhìu lựa chọn, cách nhau bởi dấu ;\n ví dụ: 1;2;3';

COMMENT ON COLUMN current.pskhamsuckhoe.phattrien_tinhthan IS
'Phát triển tinh thần theo độ tuổi. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.phattrien_vandong IS
'Phát triển vận động theo độ tuổi. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.nguyco_tuky IS
'Trẻ có nguy cơ tự kỷ. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_lao IS
'Lao. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.tiem_vgb_mui1 IS
'Tiêm vắc xin Viêm gan B mũi 1. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.tiemchung_daydu IS
'Tiêm chủng đầy đủ theo độ tuổi. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.mausac_da IS
'Màu sắc da. 0:Hồng hào; 1:Nhợt; 2:Tím; 3:Vàng; 4:Sạm da.';

COMMENT ON COLUMN current.pskhamsuckhoe.long_bantay IS
'Lòng bàn tay. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.thop IS
'Thóp. 0:Bình thường; 1:Rộng; 2:Hẹp; 3:Thóp phồng.';

COMMENT ON COLUMN current.pskhamsuckhoe.hinhdang_dau IS
'Kích thước và hình dáng đầu. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.vandong_co IS
'Vận động cổ. 0:Bình thường; 1:Giới hạn.';

COMMENT ON COLUMN current.pskhamsuckhoe.khoibatthuong_dauco IS
'Khối bất thường vùng đầu cổ. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.vitri_haimat IS
'Vị trí hai mắt. 0:Bình thường; 1:Xa nhau.';

COMMENT ON COLUMN current.pskhamsuckhoe.mimat_ketmac IS
'Mí mắt và kết mạc. 0:Bình thường; 1:Sưng, đỏ; 2:Chảy ghèn, mủ.';

COMMENT ON COLUMN current.pskhamsuckhoe.dongtu IS
'Đồng tử. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.lacmat IS
'Lác mắt. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.tai_mangnhi IS
'Tai và màng nhĩ. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.dapung_amthanh IS
'Đáp ứng với âm thanh. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.khoisung_sautai IS
'Có khối sưng sau tai. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.chaymu_nuoctai IS
'Dấu hiệu chảy mủ, nước tai. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.hinhdang_mui IS
'Hình dạng mũi. 0:Bình thường; 1:Mũi to, dày; 2:Bất sản xương mũi.';

COMMENT ON COLUMN current.pskhamsuckhoe.chaynuoc_mui IS
'Chảy nước mũi. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.nghet_mui IS
'Nghẹt mũi. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.hong IS
'Họng. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.hinhdang_mieng IS
'Hình dạng miệng. 0:Bình thường; 1:Sứt môi, chẻ vòm.';

COMMENT ON COLUMN current.pskhamsuckhoe.rangsua_sosinh IS
'Răng sữa sơ sinh. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.hinhdang_luoi IS
'Hình dạng lưỡi. 0:Bình thường; 1:Lưỡi to bè.';

COMMENT ON COLUMN current.pskhamsuckhoe.dinh_thangluoi IS
'Dính thắng lưỡi. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.nam_mieng IS
'Nấm miệng. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.cam_tut IS
'Cằm nhỏ, tụt về sau. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.vetsau_rang IS
'Vết sâu, mảng bám, lỗ trên răng. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.nhiptho_khongdeu IS
'Nhịp thở không đều. 0:Không; 1:Có cơn ngưng thở trên 5 giây.';

COMMENT ON COLUMN current.pskhamsuckhoe.thorutlom_longnguc IS
'Thở rút lõm lồng ngực. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.tiengtho_batthuong IS
'Tiếng thở bất thường. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.dauhieu_suyhohap IS
'Dấu hiệu suy hô hấp. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.nghe_phoi IS
'Nghe phổi. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.vitri_momtim IS
'Vị trí mỏm tim. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.mach_ngoaivi IS
'Mạch ngoại vi. 0:Bắt rõ; 1:Mạch nhẹ; 2:Không bắt được.';

COMMENT ON COLUMN current.pskhamsuckhoe.nghe_tim IS
'Nghe tim (loạn nhịp, tiếng thổi). 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.hinhdang_bung_ron IS
'Hình dáng bụng, rốn. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.gan_lach_to IS
'Gan, lách to. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.khoibatthuong_bung IS
'Khối bất thường vùng bụng. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.lo_haumon IS
'Lỗ hậu môn. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.cqsd_ngoai IS
'Cơ quan sinh dục ngoài. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.vandong_khongdoixung IS
'Vận động không đối xứng. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.phanxa_bu IS
'Phản xạ bú. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.phanxa_nam IS
'Phản xạ nắm. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.phanxa_moro IS
'Phản xạ Moro. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.truongluc_co IS
'Trương lực cơ. 0:Bình thường; 1:Tăng.';

COMMENT ON COLUMN current.pskhamsuckhoe.khop_hang IS
'Khớp háng. 0:Bình thường; 1:Trật khớp háng.';

COMMENT ON COLUMN current.pskhamsuckhoe.phanxa_co IS
'Phản xạ cơ. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.lung_cotsong IS
'Kiểm tra lưng, cột sống. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.tuchi_khop IS
'Khám tứ chi và khớp. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.dang_di IS
'Quan sát dáng đi. 1:Bình thường; 0:Không bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.sinh_non IS
'Sinh non. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.tuanthai_khi_sinh IS
'Tuần thai khi sinh.';

COMMENT ON COLUMN current.pskhamsuckhoe.nguon_chitra IS
'Nguồn chi trả.';

COMMENT ON COLUMN current.pskhamsuckhoe.trangthai_nhietdo IS
'Trạng thái nhiệt độ. 0:Bình thường; 1:Sốt; 2:Hạ thân nhiệt.';

COMMENT ON COLUMN current.pskhamsuckhoe.trangthai_mach IS
'Trạng thái mạch. 0:Bình thường; 1:Nhanh.';

COMMENT ON COLUMN current.pskhamsuckhoe.trangthai_nhiptho IS
'Trạng thái nhịp thở. 0:Bình thường; 1:Nhanh; 2:Chậm.';

COMMENT ON COLUMN current.pskhamsuckhoe.trangthai_huyetap IS
'Trạng thái huyết áp.';

COMMENT ON COLUMN current.pskhamsuckhoe.ketluan_binhthuong IS
'Kết luận: Bình thường.';

COMMENT ON COLUMN current.pskhamsuckhoe.ketluan_nguyco_lao IS
'Kết luận: Có nguy cơ mắc Lao.';

COMMENT ON COLUMN current.pskhamsuckhoe.ketluan_vandesuckhoe IS
'Kết luận: Có vấn đề sức khỏe.';

COMMENT ON COLUMN current.pskhamsuckhoe.ketluan_ghiro IS
'Kết luận: Ghi rõ.';
```

- Add các trường còn thiếu của Mẫu từ 6 tuổi đến dưới 18 tuổi
```SQL
  ALTER TABLE current.pskhamsuckhoe

  ADD COLUMN ma_icd_tsgd_bamsinh varchar(255),
  ADD COLUMN ma_icd_ts_bamsinh varchar(255),
  ADD COLUMN san_khoa numeric(1,0),
  ADD COLUMN san_khoa_khong_bt numeric(1,0),
  ADD COLUMN ma_benh_san_khoa_khong_bt varchar(500);

  COMMENT ON COLUMN current.pskhamsuckhoe.ma_icd_tsgd_bamsinh IS
'Mã ICD-10 tiền sử bệnh gia đình bẩm sinh hoặc bệnh truyền nhiễm. Ghi mã ICD-10 hoặc mã triệu chứng, hội chứng. Trường hợp có nhiều mã thì phân cách bằng dấu chấm phẩy (;).';

COMMENT ON COLUMN current.pskhamsuckhoe.ma_icd_ts_bamsinh IS
'Mã ICD-10 tiền sử bệnh/tật bẩm sinh. Ghi mã ICD-10 hoặc mã triệu chứng, hội chứng. Trường hợp có nhiều mã thì phân cách bằng dấu chấm phẩy (;).';

COMMENT ON COLUMN current.pskhamsuckhoe.san_khoa IS
'Sản khoa. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.san_khoa_khong_bt IS
'Sản khoa không bình thường. 0:Đẻ thiếu tháng; 1:Đẻ thừa cân; 2:Đẻ có can thiệp; 3:Đẻ ngạt; 4:Mẹ bị bệnh trong thời kỳ mang thai.';

COMMENT ON COLUMN current.pskhamsuckhoe.ma_benh_san_khoa_khong_bt IS
'Mã ICD-10 bệnh gây ra sản khoa không bình thường. Ghi mã ICD-10 hoặc mã triệu chứng, hội chứng. Trường hợp có nhiều mã thì phân cách bằng dấu chấm phẩy (;).';
```

- Add các trường còn thiếu của Mẫu Giấy khám sức khỏe và khám sức khỏe định kỳ dùng cho người từ đủ 18 tuổi trở lên:
```SQL
ALTER TABLE current.pskhamsuckhoe

-- Tiền sử bệnh
ADD COLUMN benh_5nam numeric(1,0),
ADD COLUMN benh_thankinh numeric(1,0),
ADD COLUMN benh_mat numeric(1,0),
ADD COLUMN benh_tai numeric(1,0),
ADD COLUMN benh_tim numeric(1,0),
ADD COLUMN phauthuat_timmach numeric(1,0),
ADD COLUMN tang_huyetap numeric(1,0),
ADD COLUMN kho_tho numeric(1,0),
ADD COLUMN benh_phoi numeric(1,0),
ADD COLUMN benh_than_locmau numeric(1,0),
ADD COLUMN nghien_ruoubia numeric(1,0),
ADD COLUMN dai_thaoduong numeric(1,0),
ADD COLUMN benh_tamthan numeric(1,0),
ADD COLUMN roi_loan_ythuc numeric(1,0),
ADD COLUMN ngat_chongmat numeric(1,0),
ADD COLUMN benh_tieuhoa numeric(1,0),
ADD COLUMN roi_loan_giacngu numeric(1,0),
ADD COLUMN taibien_liet numeric(1,0),
ADD COLUMN benh_cotsong numeric(1,0),
ADD COLUMN sudung_ruou numeric(1,0),
ADD COLUMN sudung_matuy numeric(1,0),
ADD COLUMN benh_khac numeric(1,0),
ADD COLUMN ten_benh_khac varchar(255),

-- Cận lâm sàng
ADD COLUMN cls_huyethoc varchar(500),
ADD COLUMN cls_duongmau varchar(500),
ADD COLUMN cls_ure varchar(500),
ADD COLUMN cls_creatinin varchar(500),
ADD COLUMN cls_asat varchar(500),
ADD COLUMN cls_alat varchar(500),
ADD COLUMN cls_tongphantich_nt varchar(500),
ADD COLUMN cls_nt_khac varchar(500),
ADD COLUMN cls_xq_timphoi varchar(500),
ADD COLUMN cls_khac numeric(1,0),
ADD COLUMN cls_khac_noidung varchar(500);

COMMENT ON COLUMN current.pskhamsuckhoe.benh_5nam IS
'Có bệnh hay bị thương trong 5 năm qua. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_thankinh IS
'Có bệnh thần kinh hay bị thương ở đầu. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_mat IS
'Bệnh mắt hoặc giảm thị lực. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_tai IS
'Bệnh ở tai, giảm sức nghe hoặc thăng bằng. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_tim IS
'Bệnh tim, nhồi máu cơ tim hoặc các bệnh tim mạch khác. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.phauthuat_timmach IS
'Phẫu thuật can thiệp tim, mạch. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.tang_huyetap IS
'Tăng huyết áp. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.kho_tho IS
'Khó thở. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_phoi IS
'Bệnh phổi, hen, khí phế thũng, viêm phế quản mạn tính. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_than_locmau IS
'Bệnh thận, lọc máu. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.nghien_ruoubia IS
'Nghiện rượu, bia. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.dai_thaoduong IS
'Đái tháo đường hoặc kiểm soát tăng đường huyết. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_tamthan IS
'Bệnh tâm thần. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.roi_loan_ythuc IS
'Mất ý thức hoặc rối loạn ý thức. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.ngat_chongmat IS
'Ngất hoặc chóng mặt. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_tieuhoa IS
'Bệnh tiêu hóa. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.roi_loan_giacngu IS
'Rối loạn giấc ngủ, ngừng thở khi ngủ, ngủ rũ ban ngày hoặc ngáy to. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.taibien_liet IS
'Tai biến mạch máu não hoặc liệt. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_cotsong IS
'Bệnh hoặc tổn thương cột sống. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.sudung_ruou IS
'Sử dụng rượu thường xuyên, liên tục. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.sudung_matuy IS
'Sử dụng ma túy và chất gây nghiện. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.benh_khac IS
'Bệnh khác. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.ten_benh_khac IS
'Tên bệnh khác.';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_huyethoc IS
'Kết quả xét nghiệm huyết học.';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_duongmau IS
'Kết quả sinh hóa máu, đường máu.';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_ure IS
'Kết quả xét nghiệm Urê.';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_creatinin IS
'Kết quả xét nghiệm Creatinin.';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_asat IS
'Kết quả xét nghiệm ASAT (GOT).';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_alat IS
'Kết quả xét nghiệm ALAT (GPT).';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_tongphantich_nt IS
'Kết quả tổng phân tích nước tiểu.';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_nt_khac IS
'Kết quả xét nghiệm nước tiểu khác.';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_xq_timphoi IS
'Kết quả chẩn đoán hình ảnh X-quang tim phổi thẳng.';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_khac IS
'Có kết quả cận lâm sàng khác. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.cls_khac_noidung IS
'Liệt kê kết quả cận lâm sàng khác (nếu có).';

```
- Add các trường còn thiếu của Mẫu Giấy khám sức khỏe tâm thần - gia đình:
```SQL
ALTER TABLE current.pskhamsuckhoe

-- Tiền sử tâm thần
ADD COLUMN ts_tamthan_giadinh numeric(1,0),
ADD COLUMN ten_tamthan_giadinh varchar(500),
ADD COLUMN ts_tamthan_banthan numeric(1,0),
ADD COLUMN ten_tamthan_banthan varchar(500),

-- Khám tâm thần
ADD COLUMN tamthan_bieuhien varchar(500),
ADD COLUMN tamthan_ythuc varchar(500),
ADD COLUMN tamthan_dinhhuong_khonggian varchar(500),
ADD COLUMN tamthan_dinhhuong_thoigian varchar(500),
ADD COLUMN tamthan_dinhhuong_xungquanh varchar(500),
ADD COLUMN tamthan_dinhhuong_banthan varchar(500),
ADD COLUMN tamthan_camxuc varchar(500),
ADD COLUMN tamthan_camgiac_trigiac varchar(500),
ADD COLUMN tamthan_tuduy_hinhthuc varchar(500),
ADD COLUMN tamthan_tuduy_noidung varchar(500),
ADD COLUMN tamthan_hanhvi_ychi varchar(500),
ADD COLUMN tamthan_hanhvi_bannang varchar(500),
ADD COLUMN tamthan_trinho varchar(500),
ADD COLUMN tamthan_tritue varchar(500),
ADD COLUMN tamthan_chuy varchar(500),
ADD COLUMN tamthan_khac varchar(500),

-- Khác
ADD COLUMN tracnghiem_tamly varchar(500),
ADD COLUMN canlamsang_khac_bs varchar(500);

COMMENT ON COLUMN current.pskhamsuckhoe.ts_tamthan_giadinh IS
'Tiền sử bệnh tâm thần gia đình. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.ten_tamthan_giadinh IS
'Tên bệnh tâm thần của gia đình.';

COMMENT ON COLUMN current.pskhamsuckhoe.ts_tamthan_banthan IS
'Tiền sử bệnh tâm thần của bản thân. 1:Có; 0:Không.';

COMMENT ON COLUMN current.pskhamsuckhoe.ten_tamthan_banthan IS
'Tên bệnh tâm thần của bản thân.';


COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_bieuhien IS
'Biểu hiện chung.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_ythuc IS
'Ý thức.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_dinhhuong_khonggian IS
'Định hướng lực (Không gian).';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_dinhhuong_thoigian IS
'Định hướng lực (Thời gian).';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_dinhhuong_xungquanh IS
'Định hướng lực (Xung quanh).';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_dinhhuong_banthan IS
'Định hướng lực (Bản thân).';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_camxuc IS
'Cảm xúc.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_camgiac_trigiac IS
'Cảm giác, tri giác.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_tuduy_hinhthuc IS
'Tư duy hình thức.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_tuduy_noidung IS
'Tư duy nội dung.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_hanhvi_ychi IS
'Hành vi, tác phong hoạt động có ý chí.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_hanhvi_bannang IS
'Hành vi, tác phong hoạt động có bản năng.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_trinho IS
'Trí nhớ.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_tritue IS
'Trí tuệ.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_chuy IS
'Chú ý.';

COMMENT ON COLUMN current.pskhamsuckhoe.tamthan_khac IS
'Khác.';

COMMENT ON COLUMN current.pskhamsuckhoe.tracnghiem_tamly IS
'Trắc nghiệm tâm lý.';

COMMENT ON COLUMN current.pskhamsuckhoe.canlamsang_khac_bs IS
'Cận lâm sàng khác theo chỉ định của bác sĩ.';
```
:blue_book: Cập nhật module Presgription
- Tại form Khám sức khỏe hợp đồng, bổ sung thêm các thông tin còn thiếu để người dùng nhập liệu.
- Chia nội dung ra thành các tab riêng biệt, bao gồm tab tổng hợp chứa tất cã thông tin và các tab con chứa từng phần thông tin được phân loại theo loại khám.
- Trên nút `In phiếu kết quả`, bổ sung thêm các tùy chọn in theo từng mẫu khác nhau.
  ![](https://i.vgy.me/F39mfC.png)
:blue_book: 
