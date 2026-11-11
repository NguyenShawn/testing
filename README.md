# Karaoke404

Ứng dụng quản lý karaoke trên Windows, hỗ trợ quy trình từ đặt phòng đến thanh toán và báo cáo doanh thu.

## Chức năng

- Quản lý phòng, loại phòng, dịch vụ và nhân viên.
- Đặt, nhận phòng; kiểm tra trùng lịch và theo dõi phiên hát.
- Quản lý tồn kho, tự trừ khi gọi dịch vụ, tính tiền phòng và lưu hóa đơn bằng transaction.
- Xem/in hóa đơn RDLC, lọc doanh thu theo ngày, biểu đồ và xuất CSV.
- Quản lý tài khoản, phân quyền Admin/Manager/Staff và bảo mật mật khẩu bằng BCrypt.
- Hiển thị VietQR chuyển khoản khi đã cấu hình tài khoản nhận tiền.

## Công nghệ

C# · Windows Forms · .NET Framework 4.7.2 · Entity Framework 6 · SQL Server LocalDB · Guna.UI2 · ReportViewer RDLC

## Cài đặt và chạy

**Yêu cầu:** Windows, Visual Studio hỗ trợ `.slnx` với workload **.NET Desktop Development**, .NET Framework 4.7.2 Targeting Pack và SQL Server Express LocalDB.

1. Clone repository và mở `karaoke.slnx`.
2. Trong SSMS, kết nối `(localdb)\MSSQLLocalDB` bằng Windows Authentication và chạy toàn bộ **[Karaoke404_Setup.sql](Karaoke404_Setup.sql)**.
3. Khôi phục NuGet packages, chọn **Build Solution** rồi chạy dự án `karaoke`.
4. Với database mới, đăng nhập bằng `admin` / `AdminKaraoke2026!` và đổi mật khẩu ngay.

Có thể chạy cùng file SQL tổng bằng PowerShell tại thư mục repository thay cho SSMS:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\SetupLocalDb.ps1
```

**Chỉ cần một file SQL:** `Karaoke404_Setup.sql` gồm cấu trúc, Admin, phân quyền, tồn kho và dữ liệu demo cho database trống. Đổi `TaoDuLieuMau` từ `1` thành `0` ở đầu file nếu chỉ cần cấu trúc. Chạy lại giữ mật khẩu, tồn kho và giao dịch hiện có; demo tự bỏ qua khi đã có dữ liệu nghiệp vụ.

Kết nối được cấu hình trong `karaoke/App.config`. Sao lưu database trước khi nâng cấp bằng file tổng. Dịch vụ cũ mới bổ sung cột tồn kho có giá trị 0; người có quyền quản lý dịch vụ nhập số lượng thực tế tại **Dịch vụ**. Nếu chưa chạy SQL, ứng dụng tự tạo database trống khi chưa tồn tại, dùng phần cấu trúc nhúng từ cùng file SQL tổng.

## Cấu hình VietQR

Điền `VietQR_BIN`, `VietQR_AccountNumber` và `VietQR_ReceiverName` trong phần `appSettings` của `karaoke/App.config`, rồi build lại. Khi chạy bản đã build, sửa `karaoke.exe.config` cạnh file EXE và khởi động lại ứng dụng.

Để trống cấu hình sẽ ẩn mã QR. Việc tạo mã cần kết nối internet; nhân viên xác nhận chuyển khoản trước khi hoàn tất thanh toán.

## Kiểm thử

Sau khi build **Debug**, chạy tại thư mục repository:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\Tests\RunInternalAccountTests.ps1
powershell -NoProfile -ExecutionPolicy Bypass -File .\Tests\RunInternalAccountTests.ps1 -Integration
powershell -NoProfile -ExecutionPolicy Bypass -File .\Tests\RunAccountProfileTests.ps1
```

Các bài test tích hợp cần LocalDB, quyền tạo/xóa database và phiên Windows desktop. Chúng sử dụng database thử nghiệm riêng và mở một số cửa sổ giao diện.

Khi bàn giao bản chạy, giữ đầy đủ DLL và file cấu hình trong thư mục build cùng `karaoke.exe`.
