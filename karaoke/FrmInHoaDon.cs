using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public partial class FrmInHoaDon : Form
    {
        private readonly InvoiceBillingDetail billingDetail;
        private readonly int? maHoaDon;
        private PrintDocument printDoc;

        public FrmInHoaDon(InvoiceBillingDetail detail, int? maHD = null)
        {
            InitializeComponent();
            this.billingDetail = detail;
            this.maHoaDon = maHD;

            ButtonActive.RoundButton(btnIn);
            ButtonActive.RoundButton(btnDong);

            GenerateInvoiceText();
        }

        private void GenerateInvoiceText()
        {
            if (billingDetail == null) return;

            var sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("            QUÁN KARAOKE GIAI ĐIỆU VÀNG           ");
            sb.AppendLine("      Địa chỉ: 123 Đường Âm Nhạc, TP. Hồ Chí Minh ");
            sb.AppendLine("            Điện thoại: (028) 3888 9999           ");
            sb.AppendLine("==================================================");
            sb.AppendLine("                HÓA ĐƠN THANH TOÁN                ");
            if (maHoaDon.HasValue)
            {
                sb.AppendLine(string.Format("  Mã hóa đơn: HD{0:D6}", maHoaDon.Value));
            }
            sb.AppendLine(string.Format("  Mã phiên hát: #{0}", billingDetail.MaPhien));
            sb.AppendLine(string.Format("  Thời gian in: {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now));
            sb.AppendLine(string.Format("  Thu ngân:     {0}", billingDetail.TenNV ?? "NV"));
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine(string.Format("  Phòng:        {0} ({1})", billingDetail.TenPhong, billingDetail.LoaiPhong));
            sb.AppendLine(string.Format("  Giờ vào:      {0:dd/MM/yyyy HH:mm}", billingDetail.GioBatDau));
            sb.AppendLine(string.Format("  Giờ ra:       {0:dd/MM/yyyy HH:mm}", billingDetail.GioKetThuc));
            sb.AppendLine(string.Format("  Thời gian:    {0} giờ {1} phút ({2:F2}h)",
                (int)billingDetail.ThoiGianHat.TotalHours,
                billingDetail.ThoiGianHat.Minutes,
                billingDetail.TongGioHat));
            sb.AppendLine(string.Format("  Đơn giá giờ:  {0:N0} đ/h", billingDetail.DonGiaGio));
            sb.AppendLine(string.Format("  Tiền phòng:   {0:N0} đ", billingDetail.TienPhong));
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine("  CHI TIẾT DỊCH VỤ SỬ DỤNG:");
            sb.AppendLine(string.Format("  {0,-18} {1,4} {2,10} {3,12}", "Tên DV", "SL", "Đơn giá", "Thành tiền"));
            sb.AppendLine("  ------------------------------------------------");

            if (billingDetail.DanhSachDichVu != null && billingDetail.DanhSachDichVu.Count > 0)
            {
                foreach (var item in billingDetail.DanhSachDichVu)
                {
                    string ten = item.TenDV;
                    if (ten.Length > 18) ten = ten.Substring(0, 15) + "...";
                    sb.AppendLine(string.Format("  {0,-18} {1,4} {2,10:N0} {3,12:N0}",
                        ten,
                        item.SoLuong,
                        item.DonGia,
                        item.ThanhTien));
                }
            }
            else
            {
                sb.AppendLine("  (Không có dịch vụ phát sinh)");
            }

            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine(string.Format("  Cộng tiền phòng:   {0,25:N0} đ", billingDetail.TienPhong));
            sb.AppendLine(string.Format("  Cộng tiền dịch vụ: {0,25:N0} đ", billingDetail.TienDichVu));
            if (billingDetail.GiamGia > 0)
            {
                sb.AppendLine(string.Format("  Giảm giá:         -{0,25:N0} đ", billingDetail.GiamGia));
            }
            sb.AppendLine("==================================================");
            sb.AppendLine(string.Format("  TỔNG CỘNG PHẢI TRẢ:{0,25:N0} đ", billingDetail.ThanhTien));
            sb.AppendLine("==================================================");
            sb.AppendLine();
            sb.AppendLine("        CẢM ƠN QUÝ KHÁCH VÀ HẸN GẶP LẠI!         ");
            sb.AppendLine("==================================================");

            rtbHoaDon.Text = sb.ToString();
            int widestLine = 0;
            foreach (string line in rtbHoaDon.Lines)
            {
                int width = TextRenderer.MeasureText(line, rtbHoaDon.Font,
                    Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine).Width;
                widestLine = Math.Max(widestLine, width);
            }
            int maxWidth = Screen.FromControl(this).WorkingArea.Width - (Width - ClientSize.Width) - 20;
            ClientSize = new Size(Math.Max(300, Math.Min(maxWidth, widestLine + SystemInformation.VerticalScrollBarWidth + 12)), ClientSize.Height);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                printDoc = new PrintDocument();
                printDoc.PrintPage += PrintDoc_PrintPage;

                using (var printDlg = new PrintDialog())
                {
                    printDlg.Document = printDoc;
                    if (printDlg.ShowDialog() == DialogResult.OK)
                    {
                        printDoc.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (var font = new Font("Consolas", 9, FontStyle.Regular))
            {
                e.Graphics.DrawString(rtbHoaDon.Text, font, Brushes.Black, new PointF(40, 40));
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
