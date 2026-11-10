-- Nâng cấp phân quyền cho CSDL QuanLyKaraoke hiện có. Chạy trên đúng database.
-- Không xóa hay sửa tài khoản, mật khẩu, lịch đặt, hóa đơn hiện có.
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
BEGIN TRY
    BEGIN TRANSACTION;
    IF OBJECT_ID(N'dbo.TAIKHOAN', N'U') IS NULL
        THROW 51000, N'Chưa có bảng TAIKHOAN. Hãy chọn CSDL Karaoke đã cài đặt.', 1;

    DECLARE @firstInstall bit = CASE WHEN OBJECT_ID(N'dbo.NHOMQUYEN', N'U') IS NULL THEN 1 ELSE 0 END;
    IF OBJECT_ID(N'dbo.NHOMQUYEN', N'U') IS NULL
        CREATE TABLE dbo.NHOMQUYEN (
            MaNhom int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NHOMQUYEN PRIMARY KEY,
            TenNhom nvarchar(100) NOT NULL CONSTRAINT UQ_NHOMQUYEN_TEN UNIQUE,
            LaHeThong bit NOT NULL CONSTRAINT DF_NHOMQUYEN_LAHETHONG DEFAULT (0)
        );
    IF OBJECT_ID(N'dbo.QUYEN', N'U') IS NULL
        CREATE TABLE dbo.QUYEN (
            MaQuyen varchar(50) NOT NULL CONSTRAINT PK_QUYEN PRIMARY KEY,
            TenQuyen nvarchar(100) NOT NULL
        );
    IF OBJECT_ID(N'dbo.NHOMQUYEN_CHITIET', N'U') IS NULL
        CREATE TABLE dbo.NHOMQUYEN_CHITIET (
            MaNhom int NOT NULL CONSTRAINT FK_NQCT_NHOM REFERENCES dbo.NHOMQUYEN(MaNhom),
            MaQuyen varchar(50) NOT NULL CONSTRAINT FK_NQCT_QUYEN REFERENCES dbo.QUYEN(MaQuyen),
            CONSTRAINT PK_NHOMQUYEN_CHITIET PRIMARY KEY (MaNhom, MaQuyen)
        );
    IF OBJECT_ID(N'dbo.TAIKHOAN_NHOMQUYEN', N'U') IS NULL
        CREATE TABLE dbo.TAIKHOAN_NHOMQUYEN (
            MaTK int NOT NULL CONSTRAINT PK_TAIKHOAN_NHOMQUYEN PRIMARY KEY CONSTRAINT FK_TKNQ_TK REFERENCES dbo.TAIKHOAN(MaTK),
            MaNhom int NOT NULL CONSTRAINT FK_TKNQ_NHOM REFERENCES dbo.NHOMQUYEN(MaNhom)
        );
    DECLARE @permissions TABLE (MaQuyen varchar(50) PRIMARY KEY, TenQuyen nvarchar(100));
    INSERT @permissions VALUES
      ('PHONG_XEM', N'Xem phòng'), ('PHONG_SUA', N'Quản lý phòng'),
      ('DICHVU_XEM', N'Xem dịch vụ'), ('DICHVU_SUA', N'Quản lý dịch vụ'),
      ('DATPHONG', N'Đặt và nhận phòng'), ('GOIDICHVU', N'Gọi dịch vụ'),
      ('THANHTOAN', N'Thanh toán'), ('THONGKE', N'Xem doanh thu'),
      ('NHANVIEN', N'Quản lý nhân viên'), ('TAIKHOAN', N'Quản lý tài khoản'),
      ('NHOMQUYEN', N'Quản lý nhóm quyền'), ('LOAIPHONG', N'Quản lý loại phòng');
    INSERT dbo.QUYEN(MaQuyen, TenQuyen)
      SELECT p.MaQuyen, p.TenQuyen FROM @permissions p WHERE NOT EXISTS (SELECT 1 FROM dbo.QUYEN q WHERE q.MaQuyen=p.MaQuyen);
    IF @firstInstall = 1
    BEGIN
        INSERT dbo.NHOMQUYEN(TenNhom, LaHeThong) VALUES (N'Admin', 1), (N'Nhân viên', 1);
        INSERT dbo.NHOMQUYEN_CHITIET(MaNhom, MaQuyen)
          SELECT n.MaNhom, q.MaQuyen FROM dbo.NHOMQUYEN n CROSS JOIN dbo.QUYEN q WHERE n.TenNhom=N'Admin';
        INSERT dbo.NHOMQUYEN_CHITIET(MaNhom, MaQuyen)
          SELECT n.MaNhom, q.MaQuyen FROM dbo.NHOMQUYEN n CROSS JOIN dbo.QUYEN q
          WHERE n.TenNhom=N'Nhân viên' AND q.MaQuyen IN ('PHONG_XEM','DICHVU_XEM','DATPHONG','GOIDICHVU','THANHTOAN');
        INSERT dbo.TAIKHOAN_NHOMQUYEN(MaTK, MaNhom)
          SELECT t.MaTK, n.MaNhom FROM dbo.TAIKHOAN t JOIN dbo.NHOMQUYEN n
            ON n.TenNhom=CASE WHEN t.VaiTro='Admin' THEN N'Admin' ELSE N'Nhân viên' END;
    END;
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
