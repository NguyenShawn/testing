# Karaoke404

Ứng dụng WinForms dùng .NET Framework 4.7.2, Entity Framework 6 và SQL Server LocalDB. Các chức năng gồm quản lý phòng/dịch vụ, đặt và nhận phòng, gọi dịch vụ, thanh toán, báo cáo doanh thu, quản lý nhân viên, tài khoản và nhóm quyền.

## Chuẩn bị

- Windows, Visual Studio với workload phát triển .NET Desktop và .NET Framework 4.7.2 targeting pack.
- SQL Server Express LocalDB và công cụ chạy SQL hỗ trợ các batch `GO`, ví dụ SQL Server Management Studio (SSMS).
- Mở `karaoke.slnx`, khôi phục NuGet packages nếu Visual Studio yêu cầu, rồi Build. Guna.UI2 và Microsoft ReportViewer được khai báo trong `karaoke/packages.config`.

Kết nối mặc định trong `karaoke/App.config` là `(localdb)\MSSQLLocalDB`, database `QuanLyKaraoke`, xác thực Windows. Nếu dùng instance khác, sửa **provider connection string** của `QuanLyKaraokeEntities` rồi build lại. LocalDB và database là dữ liệu cục bộ của từng máy, không được tạo tự động chỉ bằng cách pull code.

## Tạo database mới

Có thể mở PowerShell **trên máy đang chạy ứng dụng**, tại thư mục repo, rồi chạy `powershell -NoProfile -ExecutionPolicy Bypass -File .\SetupLocalDb.ps1`. Script tạo cấu trúc, không tự thêm dữ liệu demo. Nếu thích dùng SSMS, làm theo các bước dưới đây.

1. Kết nối SSMS đến `(localdb)\MSSQLLocalDB` bằng Windows Authentication.
2. Chạy toàn bộ `karaoke/Sql/Setup_QuanLyKaraoke.sql` để tạo database, bảng nghiệp vụ, nhóm quyền và Admin.
3. Tài khoản Admin **chỉ được tạo nếu chưa tồn tại**: `admin` / `AdminKaraoke2026!`. Đăng nhập và đổi mật khẩu ngay. Script không thay đổi mật khẩu Admin hiện có.
4. Nếu cần dữ liệu demo, chạy `karaoke/Sql/02_SampleData.sql` **trên database demo**, không chạy tùy tiện trên dữ liệu đang vận hành: script tạo phòng, phiên hát và hóa đơn mẫu.
5. Mở ứng dụng, chọn **Nhân viên** để tạo nhân viên; trong **Tài khoản**, tạo tài khoản gắn với nhân viên đó và gán nhóm quyền phù hợp.

## Đặt lại mật khẩu Admin

Nếu quên mật khẩu tài khoản `admin` trên máy này, chạy tại thư mục repo:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\SetupLocalDb.ps1 -ResetAdminPassword
```

Lệnh đặt lại mật khẩu về `AdminKaraoke2026!`, xóa khóa do đăng nhập sai và vô hiệu hóa các phiên cũ. Nó không mở khóa tài khoản bị vô hiệu hóa hoặc đổi trạng thái nhân viên. Chỉ chạy khi cần khôi phục, vì mật khẩu mặc định được công khai; đổi mật khẩu ngay sau khi đăng nhập. Với instance khác, thêm `-Server 'ten-instance'`.

## Nâng cấp database cũ

Sao lưu trước khi nâng cấp. Nếu database đã có đầy đủ bảng nghiệp vụ và các cột bảo mật, chọn đúng database `QuanLyKaraoke` trong SSMS rồi chạy toàn bộ `karaoke/Sql/04_PhanQuyen_NangCap.sql` (bỏ vùng bôi chọn trước khi chạy). Có thể thêm `USE QuanLyKaraoke;` và `GO` ở đầu file nếu muốn chỉ rõ database. Script bổ sung bảng nhóm quyền và ánh xạ tài khoản hiện có, không đặt lại mật khẩu. Chạy lại không khôi phục các quyền đã bị quản trị viên chủ động bỏ.

Nếu database thiếu bảng nghiệp vụ hoặc các cột bảo mật, kiểm tra cấu trúc hiện có và chạy `Setup_QuanLyKaraoke.sql`. Script này tạo các thành phần còn thiếu, giữ bảng nhật ký cũ và không đổi Admin đã tồn tại. Nó không tự sửa các schema cũ có cấu trúc khác biệt; kiểm tra lỗi SQL trước khi chạy ứng dụng.

Nhóm **Admin** và **Nhân viên** ban đầu là nhóm hệ thống. Admin có thể tạo nhóm tùy chỉnh trong **Nhóm quyền** và gán cho nhân viên trong **Tài khoản**. Khi đổi nhóm, nhân viên cần đăng nhập lại. Quyền được kiểm tra tại service; menu cũng bật/tắt theo quyền.

## Kiểm thử

Sau khi build Debug, trong PowerShell tại thư mục repo:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File Tests\RunInternalAccountTests.ps1
powershell -NoProfile -ExecutionPolicy Bypass -File Tests\RunInternalAccountTests.ps1 -Integration -Server '(localdb)\MSSQLLocalDB'
```

Lệnh thứ hai cần quyền tạo/xóa database thử nghiệm trên LocalDB và một phiên Windows desktop. Test tạo DB tên `KaraokePasswordTest_<id>`, chạy kiểm thử nghiệp vụ và form, rồi xóa **chỉ DB thử nghiệm đó**. Không chạy test tích hợp trên database thật. Test giao diện sẽ mở các cửa sổ trong thời gian ngắn.

Mở **Thanh toán** để xem/in hóa đơn bằng ReportViewer RDLC. Trong **Thống kê**, nút **Báo cáo** cho phép xem/in RDLC theo khoảng ngày hoặc xuất CSV. Máy chạy ứng dụng cần đủ các DLL NuGet trong thư mục build; không chỉ copy riêng `karaoke.exe`.

## Phần nhóm cần hoàn thiện khi nộp

Đề yêu cầu báo cáo học thuật, WBS ghi người thực hiện và tỷ lệ đóng góp, link code và video demo. Nhóm tự chuẩn bị các tài liệu đó theo quy định của giảng viên; repo không thể xác nhận thay nhóm các thông tin phân công hoặc video.
