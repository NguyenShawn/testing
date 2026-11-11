-- Chạy trên database QuanLyKaraoke sau khi sao lưu.
-- Dịch vụ hiện có được khởi tạo tồn kho 0 để nhập lại số lượng thực tế.
USE QuanLyKaraoke;
GO
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    IF OBJECT_ID(N'dbo.DICHVU', N'U') IS NULL
        THROW 51001, N'Chưa có bảng DICHVU. Hãy chạy script khởi tạo database trước.', 1;

    IF COL_LENGTH('dbo.DICHVU', 'TonKho') IS NULL
        EXEC(N'ALTER TABLE dbo.DICHVU ADD TonKho INT NOT NULL CONSTRAINT DF_DICHVU_TONKHO DEFAULT(0) WITH VALUES;');

    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.DICHVU') AND name = N'CK_DICHVU_TONKHO')
        EXEC(N'ALTER TABLE dbo.DICHVU WITH CHECK ADD CONSTRAINT CK_DICHVU_TONKHO CHECK (TonKho >= 0);');
    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    THROW;
END CATCH;
GO
