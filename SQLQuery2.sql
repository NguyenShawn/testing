
IF DB_ID(N'QuanLyKaraoke') IS NULL
    THROW 50001, N'Chay 01_CreateTables.sql truoc.', 1;
GO
USE QuanLyKaraoke;
GO
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    IF NOT EXISTS (SELECT 1 FROM dbo.LOAIPHONG WHERE MaLoaiPhong = 1)
        INSERT INTO dbo.LOAIPHONG(MaLoaiPhong,TenLoaiPhong,DonGiaGio)
        VALUES(1,N'Phòng thường',80000);
    IF NOT EXISTS (SELECT 1 FROM dbo.LOAIPHONG WHERE MaLoaiPhong = 2)
        INSERT INTO dbo.LOAIPHONG(MaLoaiPhong,TenLoaiPhong,DonGiaGio)
        VALUES(2,N'Phòng VIP',150000);

    IF NOT EXISTS (SELECT 1 FROM dbo.NHANVIEN WHERE MaNV = 'NV001')
        INSERT INTO dbo.NHANVIEN(MaNV,HoTen,NgaySinh,SDT,ChucVu,TrangThai)
        VALUES('NV001',N'Nhân viên mẫu',NULL,NULL,N'Quản lý',N'Đang làm việc');

    DECLARE @i INT = 1;
    DECLARE @ma VARCHAR(20);
    WHILE @i <= 20
    BEGIN
        SET @ma = 'P' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3);
        IF NOT EXISTS (SELECT 1 FROM dbo.PHONG WHERE MaPhong=@ma)
        BEGIN
            INSERT INTO dbo.PHONG(MaPhong,TenPhong,MaLoaiPhong,SucChua,TrangThai)
            VALUES (
                @ma,
                N'Phòng ' + CAST(100+@i AS NVARCHAR(10)),
                CASE WHEN @i % 4 = 0 THEN 2 ELSE 1 END,
                CASE WHEN @i % 4 = 0 THEN 12 ELSE 6 END,
                CASE WHEN @i IN (2,5,9) THEN N'Đang sử dụng' ELSE N'Trống' END
            );
        END;
        SET @i += 1;
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.DICHVU WHERE MaDV='DV001')
        INSERT INTO dbo.DICHVU(MaDV,TenDV,DonViTinh,DonGia,TrangThai)
        VALUES ('DV001',N'Nước suối',N'Chai',10000,1);
    IF NOT EXISTS (SELECT 1 FROM dbo.DICHVU WHERE MaDV='DV002')
        INSERT INTO dbo.DICHVU(MaDV,TenDV,DonViTinh,DonGia,TrangThai)
        VALUES ('DV002',N'Coca-Cola',N'Lon',15000,1);
    IF NOT EXISTS (SELECT 1 FROM dbo.DICHVU WHERE MaDV='DV003')
        INSERT INTO dbo.DICHVU(MaDV,TenDV,DonViTinh,DonGia,TrangThai)
        VALUES ('DV003',N'Trái cây',N'Đĩa',50000,1);
    IF NOT EXISTS (SELECT 1 FROM dbo.DICHVU WHERE MaDV='DV004')
        INSERT INTO dbo.DICHVU(MaDV,TenDV,DonViTinh,DonGia,TrangThai)
        VALUES ('DV004',N'Khô bò',N'Gói',35000,1);
    IF NOT EXISTS (SELECT 1 FROM dbo.DICHVU WHERE MaDV='DV005')
        INSERT INTO dbo.DICHVU(MaDV,TenDV,DonViTinh,DonGia,TrangThai)
        VALUES ('DV005',N'Bia',N'Lon',25000,1);

    -- 3 phien dang hat theo 3 phong da dat TrangThai = 'Dang su dung'.
    DECLARE @now DATETIME2 = SYSDATETIME();
    DECLARE @activeRoom VARCHAR(20);
    DECLARE active_cursor CURSOR LOCAL FAST_FORWARD FOR
        SELECT MaPhong FROM (VALUES('P002'),('P005'),('P009')) A(MaPhong);
    OPEN active_cursor;
    FETCH NEXT FROM active_cursor INTO @activeRoom;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF EXISTS (SELECT 1 FROM dbo.PHONG WHERE MaPhong = @activeRoom AND TrangThai = N'Đang sử dụng')
           AND NOT EXISTS (SELECT 1 FROM dbo.PHIENHAT WHERE MaPhong = @activeRoom AND TrangThai = 'DangHat')
        BEGIN
            INSERT INTO dbo.PHIENHAT (MaPhong,MaNV,GioBatDau,GioKetThuc,DonGiaGio,TrangThai)
            SELECT @activeRoom,'NV001',DATEADD(MINUTE,-45,@now),NULL,lp.DonGiaGio,'DangHat'
            FROM dbo.PHONG p INNER JOIN dbo.LOAIPHONG lp ON p.MaLoaiPhong=lp.MaLoaiPhong
            WHERE p.MaPhong=@activeRoom;
        END;
        FETCH NEXT FROM active_cursor INTO @activeRoom;
    END;
    CLOSE active_cursor;
    DEALLOCATE active_cursor;

    -- 6 hoa don thuoc 6 thang truoc; ngay 15 moi thang. Khong tao ngay tuong lai.
    DECLARE @offset INT = 6;
    DECLARE @start DATETIME2;
    DECLARE @end DATETIME2;
    DECLARE @maPhien INT;
    DECLARE @dichVu DECIMAL(18,2);
    WHILE @offset >= 1
    BEGIN
        SET @start = DATEADD(HOUR,18,DATEADD(DAY,14,DATEADD(MONTH,-@offset,DATEADD(MONTH,DATEDIFF(MONTH,0,@now),0))));
        SET @end = DATEADD(HOUR,2,@start);
        SET @dichVu = CAST((7-@offset)*50000 AS DECIMAL(18,2));
        SET @maPhien = NULL;
        SELECT @maPhien = MaPhien FROM dbo.PHIENHAT WHERE MaPhong='P001' AND GioBatDau=@start;
        IF @maPhien IS NULL
        BEGIN
            INSERT INTO dbo.PHIENHAT(MaPhong,MaNV,GioBatDau,GioKetThuc,DonGiaGio,TrangThai)
            VALUES('P001','NV001',@start,@end,80000,'DaKetThuc');
            SET @maPhien = CONVERT(INT,SCOPE_IDENTITY());
            INSERT INTO dbo.CHITIETDICHVU(MaPhien,MaDV,SoLuong,DonGia,ThoiGianGoi)
            VALUES(@maPhien,'DV003',7-@offset,50000,@start);
            INSERT INTO dbo.HOADON(MaPhien,MaNV,NgayThanhToan,TienPhong,TienDichVu,GiamGia,ThanhTien,PhuongThucTT)
            VALUES(@maPhien,'NV001',@end,160000,@dichVu,0,160000+@dichVu,N'Tiền mặt');
        END;
        SET @offset -= 1;
    END;

    -- Hoa don hom nay: moc dau ngay co dinh de chay lai khong bi trung.
    -- Ket thuc tai thoi diem chay, khong tao thoi gian trong tuong lai.
    DECLARE @todayStart DATETIME2 = CAST(CAST(@now AS DATE) AS DATETIME2);
    DECLARE @todayEnd DATETIME2 = @now;
    DECLARE @todayHours DECIMAL(18,2) = ROUND(DATEDIFF(SECOND, @todayStart, @todayEnd) / 3600.0, 2);
    IF @todayHours < 0.25 SET @todayHours = 0.25;
    DECLARE @todayRoom DECIMAL(18,2) = ROUND(@todayHours * 80000, 0);
    SET @maPhien = NULL;
    SELECT @maPhien = MaPhien FROM dbo.PHIENHAT
    WHERE MaPhong='P003' AND GioBatDau=@todayStart;
    IF @maPhien IS NULL
    BEGIN
        INSERT INTO dbo.PHIENHAT(MaPhong,MaNV,GioBatDau,GioKetThuc,DonGiaGio,TrangThai)
        VALUES('P003','NV001',@todayStart,@todayEnd,80000,'DaKetThuc');
        SET @maPhien = CONVERT(INT,SCOPE_IDENTITY());
        INSERT INTO dbo.CHITIETDICHVU(MaPhien,MaDV,SoLuong,DonGia,ThoiGianGoi)
        VALUES(@maPhien,'DV003',13,50000,@todayStart), (@maPhien,'DV001',4,10000,@todayStart);
        INSERT INTO dbo.HOADON(MaPhien,MaNV,NgayThanhToan,TienPhong,TienDichVu,GiamGia,ThanhTien,PhuongThucTT)
        VALUES(@maPhien,'NV001',@now,@todayRoom,690000,0,@todayRoom+690000,N'Tiền mặt');
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

-- Kiem tra nhanh Dashboard
SELECT COUNT(*) AS TongPhong,
       SUM(CASE WHEN TrangThai=N'Đang sử dụng' THEN 1 ELSE 0 END) AS DangSuDung,
       SUM(CASE WHEN TrangThai=N'Trống' THEN 1 ELSE 0 END) AS PhongTrong
FROM dbo.PHONG;
SELECT ISNULL(SUM(ThanhTien),0) AS DoanhThuHomNay FROM dbo.HOADON
WHERE NgayThanhToan >= CAST(GETDATE() AS DATE)
  AND NgayThanhToan < DATEADD(DAY,1,CAST(GETDATE() AS DATE));
SELECT MONTH(NgayThanhToan) AS Thang, SUM(ThanhTien) AS DoanhThu
FROM dbo.HOADON WHERE YEAR(NgayThanhToan)=YEAR(GETDATE())
GROUP BY MONTH(NgayThanhToan) ORDER BY Thang;
GO
