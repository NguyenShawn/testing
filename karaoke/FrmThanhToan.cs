using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using QRCoder;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public partial class FrmThanhToan : Form
    {
        // =========================================================================
        // CẤU HÌNH TÀI KHOẢN NGÂN HÀNG NHẬN THANH TOÁN QR (VIETQR)
        // Ghi chú các Mã BIN ngân hàng phổ biến tại Việt Nam để dễ dàng thay đổi:
        // - VietinBank : 970415 | Vietcombank: 970436 | BIDV       : 970418
        // - Agribank   : 970405 | MB Bank    : 970422 | Techcombank: 970407
        // - ACB        : 970416 | VPBank     : 970432 | TPBank     : 970423
        // - Sacombank  : 970403 | VIB        : 970441 | HDBank     : 970437
        // =========================================================================
        private string BANK_BIN = "123456"; // Ví dụ: Đổi thành "970415" (VietinBank) hoặc mã BIN ngân hàng thực tế
        private string BANK_ACC_NUM = "123456789"; // Số tài khoản nhận tiền
        private string ACCOUNT_NAME = "HO KINH DOANH KARAOKE"; // Tên chủ tài khoản viết không dấu

        private static readonly HttpClient qrHttpClient = CreateQrHttpClient();
        private readonly PaymentService paymentService = new PaymentService();
        private int currentMaPhien = 0;
        private InvoiceBillingDetail currentBilling = null;
        private int qrRequestVersion = 0;

        private static HttpClient CreateQrHttpClient()
        {
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            }
            catch
            {
                // Bỏ qua nếu hệ điều hành không hỗ trợ cấu hình TLS
            }

            var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(8)
            };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("KaraokePOS/1.0");
            return client;
        }

        public FrmThanhToan()
        {
            InitializeComponent();

            foreach (var btn in new[] { btnLamMoiPhong, btnThanhToan, btnInHoaDon, btnTimLichSu, btnInLaiHD })
            {
                ButtonActive.RoundButton(btn);
            }

            this.Load += FrmThanhToan_Load;
        }

        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            InitGrids();
            cboPhuongThuc.SelectedIndex = 0;

            // Bộ lọc ngày mặc định: 30 ngày qua
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
            dtpDenNgay.Value = DateTime.Today;

            LoadActiveSessions();
            LoadInvoiceHistory();
        }

        private void InitGrids()
        {
            // Grid phòng đang hát
            dgvPhongDangHat.AutoGenerateColumns = false;

            FormatGridStyle(dgvPhongDangHat);

            // Grid dịch vụ
            dgvDichVu.AutoGenerateColumns = false;

            FormatGridStyle(dgvDichVu);

            // Grid lịch sử hóa đơn
            dgvLichSu.AutoGenerateColumns = false;

            FormatGridStyle(dgvLichSu);
        }

        private void FormatGridStyle(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 101, 52);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grid.ColumnHeadersHeight = 32;
            grid.RowTemplate.Height = 28;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 247, 208);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(22, 101, 52);
        }

        /// <summary>
        /// Nạp danh sách các phòng đang hát chờ thanh toán
        /// </summary>
        private void LoadActiveSessions()
        {
            try
            {
                var sessions = paymentService.GetActiveSessions();
                dgvPhongDangHat.DataSource = sessions;

                if (sessions.Count == 0)
                {
                    ResetBillingUI();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPhongDangHat_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhongDangHat.SelectedRows.Count == 0) return;

            var row = dgvPhongDangHat.SelectedRows[0].DataBoundItem as PaymentSessionRow;
            if (row == null) return;

            currentMaPhien = row.MaPhien;
            LoadBillingForSession(currentMaPhien);
        }

        private void LoadBillingForSession(int maPhien)
        {
            try
            {
                decimal giamGia = numGiamGia.Value;
                currentBilling = paymentService.CalculateBillingDetail(maPhien, DateTime.Now, giamGia);

                // Cập nhật thông tin phòng
                lblTenPhong.Text = string.Format("Phòng: {0} ({1})", currentBilling.TenPhong, currentBilling.LoaiPhong);
                lblGioVao.Text = string.Format("Giờ bắt đầu: {0:dd/MM/yyyy HH:mm}", currentBilling.GioBatDau);
                lblGioRa.Text = string.Format("Giờ kết thúc: {0:dd/MM/yyyy HH:mm}", currentBilling.GioKetThuc);
                lblThoiGianHat.Text = string.Format("Thời gian hát: {0} giờ {1} phút ({2:F2}h)",
                    (int)currentBilling.ThoiGianHat.TotalHours,
                    currentBilling.ThoiGianHat.Minutes,
                    currentBilling.TongGioHat);
                lblDonGia.Text = string.Format("Đơn giá giờ: {0:N0} đ/h", currentBilling.DonGiaGio);
                lblTienPhongUocTinh.Text = string.Format("Tiền phòng: {0:N0} đ", currentBilling.TienPhong);

                // Cập nhật danh sách dịch vụ
                dgvDichVu.DataSource = currentBilling.DanhSachDichVu;
                lblTongTienDVGrid.Text = string.Format("Tổng tiền dịch vụ: {0:N0} đ", currentBilling.TienDichVu);

                // Cập nhật thanh toán
                lblSumTienPhongVal.Text = currentBilling.TienPhong.ToString("N0") + " đ";
                lblSumTienDVVal.Text = currentBilling.TienDichVu.ToString("N0") + " đ";
                lblThanhTienVal.Text = currentBilling.ThanhTien.ToString("N0") + " đ";

                TinhTienThua();

                // Chỉ hiển thị và tạo mã QR khi phương thức thanh toán là Chuyển khoản
                RefreshQRCodeByPaymentMethod();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính tiền phiên hát: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboPhuongThuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshQRCodeByPaymentMethod();
        }

        private bool IsBankTransferSelected()
        {
            string phuongThuc = cboPhuongThuc.SelectedItem?.ToString() ?? string.Empty;
            return string.Equals(phuongThuc, "Chuyển khoản", StringComparison.OrdinalIgnoreCase);
        }

        private void RefreshQRCodeByPaymentMethod()
        {
            if (IsBankTransferSelected())
            {
                panelQRCode.Visible = true;
                decimal soTien = currentBilling != null ? currentBilling.ThanhTien : 0m;
                string maHD = currentBilling != null ? currentBilling.MaPhien.ToString() : "0";
                _ = UpdateQRCodeAsync(soTien, maHD);
            }
            else
            {
                qrRequestVersion++;
                panelQRCode.Visible = false;
                SetQRCodeImage(null);
            }
        }

        private void numGiamGia_ValueChanged(object sender, EventArgs e)
        {
            if (currentMaPhien > 0)
            {
                LoadBillingForSession(currentMaPhien);
            }
        }

        private void txtTienKhachDua_TextChanged(object sender, EventArgs e)
        {
            TinhTienThua();
        }

        private void TinhTienThua()
        {
            if (currentBilling == null)
            {
                lblTienThuaVal.Text = "0 đ";
                return;
            }

            string text = txtTienKhachDua.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(text, out decimal tienDua))
            {
                decimal tienThua = tienDua - currentBilling.ThanhTien;
                if (tienThua >= 0)
                {
                    lblTienThuaVal.ForeColor = Color.FromArgb(22, 101, 52);
                    lblTienThuaVal.Text = tienThua.ToString("N0") + " đ";
                }
                else
                {
                    lblTienThuaVal.ForeColor = Color.FromArgb(220, 38, 38);
                    lblTienThuaVal.Text = string.Format("Còn thiếu {0:N0} đ", Math.Abs(tienThua));
                }
            }
            else
            {
                lblTienThuaVal.Text = "0 đ";
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (currentMaPhien <= 0 || currentBilling == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string phuongThuc = cboPhuongThuc.SelectedItem?.ToString() ?? "Tiền mặt";
            decimal giamGia = numGiamGia.Value;

            var confirm = MessageBox.Show(
                string.Format("Xác nhận thanh toán cho phòng '{0}'?\n\n- Tiền phòng: {1:N0} đ\n- Tiền dịch vụ: {2:N0} đ\n- Giảm giá: {3:N0} đ\n- TỔNG THANH TOÁN: {4:N0} đ\n- Hình thức: {5}",
                currentBilling.TenPhong,
                currentBilling.TienPhong,
                currentBilling.TienDichVu,
                giamGia,
                currentBilling.ThanhTien,
                phuongThuc),
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                int maHD = paymentService.CompletePayment(currentMaPhien, giamGia, phuongThuc, currentBilling.GioKetThuc);

                MessageBox.Show(
                    string.Format("Thanh toán thành công! Mã hóa đơn: HD{0:D6}", maHD),
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Mở cửa sổ in hóa đơn cho khách
                using (var frmIn = new FrmInHoaDon(currentBilling, maHD))
                {
                    frmIn.ShowDialog(this);
                }

                // Reset và tải lại
                ResetBillingUI();
                LoadActiveSessions();
                LoadInvoiceHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực hiện thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (currentBilling == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xem hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frmIn = new FrmInHoaDon(currentBilling, null))
            {
                frmIn.ShowDialog(this);
            }
        }

        private void btnLamMoiPhong_Click(object sender, EventArgs e)
        {
            LoadActiveSessions();
        }

        private void ResetBillingUI()
        {
            currentMaPhien = 0;
            currentBilling = null;
            lblTenPhong.Text = "Phòng: (Chưa chọn phòng)";
            lblGioVao.Text = "Giờ bắt đầu: -";
            lblGioRa.Text = "Giờ kết thúc: -";
            lblThoiGianHat.Text = "Thời gian hát: -";
            lblDonGia.Text = "Đơn giá giờ: -";
            lblTienPhongUocTinh.Text = "Tiền phòng: -";
            dgvDichVu.DataSource = null;
            lblTongTienDVGrid.Text = "Tổng tiền dịch vụ: 0 đ";
            lblSumTienPhongVal.Text = "0 đ";
            lblSumTienDVVal.Text = "0 đ";
            lblThanhTienVal.Text = "0 đ";
            numGiamGia.Value = 0;
            txtTienKhachDua.Clear();
            lblTienThuaVal.Text = "0 đ";
            lblQuetMaQR.Text = "Quét mã để thanh toán nhanh";

            qrRequestVersion++;
            if (cboPhuongThuc.Items.Count > 0)
            {
                cboPhuongThuc.SelectedIndex = 0;
            }
            panelQRCode.Visible = false;
            SetQRCodeImage(null);
        }

        // ==========================================
        // LOGIC TẠO MÃ QR THANH TOÁN (VIETQR + QRCODER FALLBACK)
        // ==========================================

        /// <summary>
        /// Tự động tạo mã VietQR bất đồng bộ theo Tổng tiền và Mã hóa đơn.
        /// Nếu mất mạng hoặc API VietQR lỗi, tự động fallback sang QRCoder offline.
        /// </summary>
        public async Task UpdateQRCodeAsync(decimal soTien, string maHoaDon)
        {
            int requestId = ++qrRequestVersion;
            long amount = (long)Math.Max(0m, Math.Round(soTien, 0));
            string cleanMaHoaDon = string.IsNullOrWhiteSpace(maHoaDon) ? "0" : maHoaDon.Trim();
            string transferContent = string.Format("THANH TOAN HOA DON {0}", cleanMaHoaDon);

            try
            {
                string encodedInfo = Uri.EscapeDataString(transferContent);
                string encodedAccountName = Uri.EscapeDataString(ACCOUNT_NAME ?? string.Empty);

                string vietQrUrl = string.Format(
                    "https://img.vietqr.io/image/{0}-{1}-compact2.png?amount={2}&addInfo={3}&accountName={4}",
                    (BANK_BIN ?? string.Empty).Trim(),
                    (BANK_ACC_NUM ?? string.Empty).Trim(),
                    amount,
                    encodedInfo,
                    encodedAccountName);

                byte[] responseBytes = await qrHttpClient.GetByteArrayAsync(vietQrUrl).ConfigureAwait(false);
                Bitmap onlineQrBitmap = await Task.Run(() => DecodeVietQrResponse(responseBytes)).ConfigureAwait(false);

                if (onlineQrBitmap == null)
                {
                    throw new InvalidOperationException("Không thể giải mã ảnh từ VietQR API.");
                }

                ApplyQRCodeResult(requestId, onlineQrBitmap);
            }
            catch (Exception)
            {
                // Fallback sang tạo mã QR tĩnh bằng thư viện QRCoder offline khi không có mạng hoặc API lỗi
                try
                {
                    Bitmap offlineQrBitmap = await Task.Run(() => GenerateOfflineQRCode(amount, transferContent)).ConfigureAwait(false);
                    ApplyQRCodeResult(requestId, offlineQrBitmap);
                }
                catch
                {
                    // Đảm bảo không bao giờ gây crash giao diện WinForms
                }
            }
        }

        private void ApplyQRCodeResult(int requestId, Bitmap bitmap)
        {
            if (bitmap == null) return;

            if (this.IsDisposed || (picQRCode != null && picQRCode.IsDisposed))
            {
                bitmap.Dispose();
                return;
            }

            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new Action(() => ApplyQRCodeResult(requestId, bitmap)));
                }
                catch
                {
                    bitmap.Dispose();
                }
                return;
            }

            if (requestId == qrRequestVersion && !this.IsDisposed && !picQRCode.IsDisposed)
            {
                SetQRCodeImage(bitmap);
                lblQuetMaQR.Text = "Quét mã để thanh toán nhanh";
            }
            else
            {
                bitmap.Dispose();
            }
        }

        /// <summary>
        /// Tạo mã QR chứa thông tin chuyển khoản tĩnh bằng thư viện QRCoder offline.
        /// </summary>
        private Bitmap GenerateOfflineQRCode(decimal soTien, string transferContent)
        {
            long amount = (long)Math.Max(0m, Math.Round(soTien, 0));
            string payload = string.Format(
                "NGAN HANG (BIN): {0}\nSO TK: {1}\nCHU TK: {2}\nSO TIEN: {3}\nNOI DUNG: {4}",
                BANK_BIN,
                BANK_ACC_NUM,
                ACCOUNT_NAME,
                amount,
                transferContent);

            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrCodeData))
            {
                return qrCode.GetGraphic(20);
            }
        }

        /// <summary>
        /// Giải mã dữ liệu trả về từ VietQR API (hỗ trợ cả ảnh PNG/JPG trực tiếp và định dạng SVG nhúng của VietQR).
        /// </summary>
        private static Bitmap DecodeVietQrResponse(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;

            // Kiểm tra nếu phản hồi là SVG XML từ img.vietqr.io
            string head = Encoding.UTF8.GetString(bytes, 0, Math.Min(bytes.Length, 256)).TrimStart();
            if (head.StartsWith("<svg", StringComparison.OrdinalIgnoreCase) ||
                head.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase))
            {
                string svgContent = Encoding.UTF8.GetString(bytes);
                return RenderVietQrSvgToBitmap(svgContent);
            }

            using (var ms = new MemoryStream(bytes))
            using (var img = Image.FromStream(ms))
            {
                return new Bitmap(img);
            }
        }

        /// <summary>
        /// Chuyển đổi tài liệu SVG của VietQR thành Bitmap sắc nét cho PictureBox WinForms.
        /// </summary>
        private static Bitmap RenderVietQrSvgToBitmap(string svgContent)
        {
            var doc = XDocument.Parse(svgContent);
            var root = doc.Root;
            if (root == null) return null;

            int width = ParseIntAttr(root, "width", 540);
            int height = ParseIntAttr(root, "height", 640);

            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // 1. Vẽ ảnh nền khung VietQR (nếu có)
                foreach (var imgEl in root.Elements().Where(e => e.Name.LocalName == "image"))
                {
                    DrawSvgBase64Image(g, imgEl, 0f, 0f, 1f);
                }

                // 2. Vẽ cụm mã QR (<g transform="translate(tx ty) scale(s)">)
                foreach (var groupEl in root.Elements().Where(e => e.Name.LocalName == "g"))
                {
                    float tx = 0f, ty = 0f, scale = 1f;
                    string transform = (string)groupEl.Attribute("transform") ?? string.Empty;

                    var trMatch = Regex.Match(transform, @"translate\(\s*([0-9.\-]+)[\s,]+([0-9.\-]+)\s*\)");
                    if (trMatch.Success)
                    {
                        float.TryParse(trMatch.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out tx);
                        float.TryParse(trMatch.Groups[2].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out ty);
                    }

                    var scMatch = Regex.Match(transform, @"scale\(\s*([0-9.\-]+)\s*\)");
                    if (scMatch.Success)
                    {
                        float.TryParse(scMatch.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out scale);
                    }

                    var prevSmooth = g.SmoothingMode;
                    var prevOffset = g.PixelOffsetMode;
                    g.SmoothingMode = SmoothingMode.None;
                    g.PixelOffsetMode = PixelOffsetMode.Half;

                    foreach (var child in groupEl.Elements())
                    {
                        string localName = child.Name.LocalName;
                        if (localName == "rect")
                        {
                            float rx = ParseFloatAttr(child, "x", 0f);
                            float ry = ParseFloatAttr(child, "y", 0f);
                            float rw = ParseFloatAttr(child, "width", 0f);
                            float rh = ParseFloatAttr(child, "height", 0f);
                            Color fill = ParseSvgColor((string)child.Attribute("fill"), Color.White);
                            using (var brush = new SolidBrush(fill))
                            {
                                g.FillRectangle(brush, tx + rx * scale, ty + ry * scale, rw * scale, rh * scale);
                            }
                        }
                        else if (localName == "path")
                        {
                            string d = (string)child.Attribute("d") ?? string.Empty;
                            Color fill = ParseSvgColor((string)child.Attribute("fill"), Color.Black);
                            using (var brush = new SolidBrush(fill))
                            {
                                foreach (Match m in Regex.Matches(d, @"M(\d+)\s+(\d+)h(\d+)v(\d+)"))
                                {
                                    float px = float.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
                                    float py = float.Parse(m.Groups[2].Value, CultureInfo.InvariantCulture);
                                    float pw = float.Parse(m.Groups[3].Value, CultureInfo.InvariantCulture);
                                    float ph = float.Parse(m.Groups[4].Value, CultureInfo.InvariantCulture);
                                    g.FillRectangle(brush, tx + px * scale, ty + py * scale, pw * scale, ph * scale);
                                }
                            }
                        }
                        else if (localName == "image")
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            DrawSvgBase64Image(g, child, tx, ty, scale);
                            g.SmoothingMode = SmoothingMode.None;
                        }
                    }

                    g.SmoothingMode = prevSmooth;
                    g.PixelOffsetMode = prevOffset;
                }

                // 3. Vẽ các dòng chữ thông tin chủ tài khoản / số tiền
                foreach (var textEl in root.Elements().Where(e => e.Name.LocalName == "text"))
                {
                    string text = (textEl.Value ?? string.Empty).Trim();
                    if (string.IsNullOrEmpty(text)) continue;

                    float x = ParseFloatAttr(textEl, "x", width / 2f);
                    float y = ParseFloatAttr(textEl, "y", height - 30f);
                    float fontSize = ParseFloatAttr(textEl, "font-size", 15f);
                    string fontWeight = (string)textEl.Attribute("font-weight") ?? string.Empty;
                    FontStyle style = fontWeight.IndexOf("bold", StringComparison.OrdinalIgnoreCase) >= 0
                        ? FontStyle.Bold
                        : FontStyle.Regular;
                    Color fill = ParseSvgColor((string)textEl.Attribute("fill"), Color.FromArgb(38, 38, 38));

                    using (var font = new Font("Segoe UI", fontSize, style, GraphicsUnit.Pixel))
                    using (var brush = new SolidBrush(fill))
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far })
                    {
                        g.DrawString(text, font, brush, new PointF(x, y + 3f), sf);
                    }
                }
            }

            return bmp;
        }

        private static void DrawSvgBase64Image(Graphics g, XElement imgEl, float tx, float ty, float scale)
        {
            string href = (string)imgEl.Attribute("href")
                       ?? (string)imgEl.Attributes().FirstOrDefault(a => a.Name.LocalName == "href");
            if (string.IsNullOrEmpty(href)) return;

            int commaIdx = href.IndexOf(',');
            if (commaIdx < 0 || commaIdx >= href.Length - 1) return;

            string base64 = href.Substring(commaIdx + 1);
            byte[] imgBytes = Convert.FromBase64String(base64);
            float ix = ParseFloatAttr(imgEl, "x", 0f);
            float iy = ParseFloatAttr(imgEl, "y", 0f);
            float iw = ParseFloatAttr(imgEl, "width", 0f);
            float ih = ParseFloatAttr(imgEl, "height", 0f);

            using (var ms = new MemoryStream(imgBytes))
            using (var embeddedImg = Image.FromStream(ms))
            {
                g.DrawImage(embeddedImg, tx + ix * scale, ty + iy * scale, iw * scale, ih * scale);
            }
        }

        private static int ParseIntAttr(XElement el, string attrName, int defaultVal)
        {
            string val = (string)el.Attribute(attrName);
            return int.TryParse(val, NumberStyles.Integer, CultureInfo.InvariantCulture, out int r) ? r : defaultVal;
        }

        private static float ParseFloatAttr(XElement el, string attrName, float defaultVal)
        {
            string val = (string)el.Attribute(attrName);
            return float.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out float r) ? r : defaultVal;
        }

        private static Color ParseSvgColor(string hex, Color defaultColor)
        {
            if (string.IsNullOrWhiteSpace(hex)) return defaultColor;
            try
            {
                return ColorTranslator.FromHtml(hex.Trim());
            }
            catch
            {
                return defaultColor;
            }
        }

        private void SetQRCodeImage(Image newImage)
        {
            if (picQRCode == null || picQRCode.IsDisposed)
            {
                newImage?.Dispose();
                return;
            }

            var oldImage = picQRCode.Image;
            picQRCode.Image = newImage;
            if (oldImage != null && !ReferenceEquals(oldImage, newImage))
            {
                oldImage.Dispose();
            }
        }

        // ==========================
        // TAB LỊCH SỬ HÓA ĐƠN
        // ==========================

        private void LoadInvoiceHistory()
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;
                string tuKhoa = txtTimKiemHD.Text.Trim();

                var list = paymentService.GetInvoiceHistory(tuNgay, denNgay, null, tuKhoa);
                dgvLichSu.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimLichSu_Click(object sender, EventArgs e)
        {
            LoadInvoiceHistory();
        }

        private void btnInLaiHD_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn trong danh sách để xem/in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var item = dgvLichSu.SelectedRows[0].DataBoundItem as InvoiceHistoryRow;
            if (item == null) return;

            try
            {
                var detail = paymentService.GetSavedInvoiceDetail(item.MaHD);
                using (var frmIn = new FrmInHoaDon(detail, item.MaHD))
                {
                    frmIn.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
