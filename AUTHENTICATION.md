# Đăng nhập nội bộ

Chỉ Admin và Nhân viên đăng nhập. Khách hàng không có tài khoản; nhân viên ghi tên/SDT của khách trong DATPHONG.

## Khởi tạo database

Chạy toàn bộ `karaoke/Sql/Setup_QuanLyKaraoke.sql` trong SSMS. File gộp SQL 01, 03 và phần khởi tạo Admin, bao gồm các cột bảo mật đang được code sử dụng. Chạy lại giữ nguyên mật khẩu và trạng thái Admin đã có. Dữ liệu mẫu ở `02_SampleData.sql` chạy riêng nếu cần.

Đã bỏ bảng nhật ký tài khoản: file Setup không tạo/ghi nhật ký và sẽ xóa bảng NHATKYTAIKHOAN cũ nếu tồn tại. Bước này xóa dữ liệu nhật ký cũ; không xóa tài khoản, hồ sơ nhân viên, booking, phiên hát hoặc hóa đơn.

9 bảng cần dùng: NHANVIEN, TAIKHOAN, LOAIPHONG, PHONG, DICHVU, DATPHONG, PHIENHAT, CHITIETDICHVU, HOADON.

## Đăng nhập

Mật khẩu lưu bằng BCrypt. Tài khoản phải đang hoạt động, vai trò Admin/NhanVien và hồ sơ nhân viên đang làm việc. Sai mật khẩu 5 lần liên tiếp khóa đăng nhập 15 phút. Giữ các cột SoLanDangNhapSai, KhoaDen và PhienBanBaoMat trong TAIKHOAN.

Đăng nhập thành công đóng/giải phóng FrmLogin rồi mở FrmMain. Đăng xuất xóa phiên và tạo Login mới; đóng Main thoát ứng dụng.

## Phạm vi hiện tại

Tập trung hoàn thiện đăng nhập. Các form tạo tài khoản, đổi mật khẩu và thanh toán đã được người dùng xóa; chưa triển khai lại các tính năng này. Nút Thanh toán vẫn hiển thị và bật nhưng chưa có xử lý khi bấm. Không có đăng ký khách hàng, OTP/email hoặc phân quyền tùy chỉnh.
