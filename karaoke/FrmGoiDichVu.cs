using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using karaoke.Services;

namespace karaoke
{
    public partial class FrmGoiDichVu : Form
    {
        private readonly ServiceOrderService orderService = new ServiceOrderService();
        private readonly BindingList<CartItemInput> unconfirmedCart = new BindingList<CartItemInput>();
        private int currentMaPhien = 0;
        private int previousSelectedMaPhien = 0;

        public FrmGoiDichVu()
        {
            InitializeComponent();
            this.Load += FrmGoiDichVu_Load;
        }

        private void FrmGoiDichVu_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            try
            {
                AuthService.RequireCurrentUser();
                btnXacNhanGoi.Enabled = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); Close(); return; }

            InitGrids();
            dgvGioHang.DataSource = unconfirmedCart;
            unconfirmedCart.ListChanged += (s, ev) => UpdateCartTotal();

            LoadActiveSessions();
            LoadCatalog();
        }

        private void InitGrids()
        {
            // Menu Catalog Grid
            dgvMenu.AutoGenerateColumns = false;
            dgvMenu.Columns.Clear();
            dgvMenu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaDV",
                HeaderText = "Mã",
                Width = 65
            });
            dgvMenu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDV",
                HeaderText = "Tên dịch vụ",
                Width = 160
            });
            dgvMenu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonViTinh",
                HeaderText = "ĐVT",
                Width = 65
            });
            dgvMenu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn giá",
                DefaultCellStyle = { Format = "N0" },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            FormatGridStyle(dgvMenu);

            // Unconfirmed Cart Grid
            dgvGioHang.AutoGenerateColumns = false;
            dgvGioHang.Columns.Clear();
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDV",
                HeaderText = "Mặt hàng",
                Width = 160
            });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonViTinh",
                HeaderText = "ĐVT",
                Width = 60
            });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn giá",
                DefaultCellStyle = { Format = "N0" },
                Width = 90
            });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = "SL",
                Width = 55
            });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhTien",
                HeaderText = "Thành tiền",
                DefaultCellStyle = { Format = "N0" },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            FormatGridStyle(dgvGioHang);

            // Confirmed Orders Grid
            dgvDaGoi.AutoGenerateColumns = false;
            dgvDaGoi.Columns.Clear();
            dgvDaGoi.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDV",
                HeaderText = "Tên món",
                Width = 150
            });
            dgvDaGoi.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonViTinh",
                HeaderText = "ĐVT",
                Width = 60
            });
            dgvDaGoi.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn giá",
                DefaultCellStyle = { Format = "N0" },
                Width = 85
            });
            dgvDaGoi.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = "SL",
                Width = 50
            });
            dgvDaGoi.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhTien",
                HeaderText = "Thành tiền",
                DefaultCellStyle = { Format = "N0" },
                Width = 95
            });
            dgvDaGoi.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThoiGianGoi",
                HeaderText = "Thời gian gọi",
                DefaultCellStyle = { Format = "HH:mm" },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            FormatGridStyle(dgvDaGoi);
        }

        private void FormatGridStyle(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 101, 52);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(16, 185, 129);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void LoadActiveSessions()
        {
            try
            {
                var sessions = orderService.GetActiveSessions();

                cbPhienHat.DisplayMember = "DisplayText";
                cbPhienHat.ValueMember = "MaPhien";

                var comboList = sessions.Select(s => new
                {
                    s.MaPhien,
                    DisplayText = string.Format("{0} - {1} (Bắt đầu: {2:HH:mm})", s.TenPhong, s.LoaiPhong, s.GioBatDau)
                }).ToList();

                int previousSession = currentMaPhien;
                cbPhienHat.DataSource = comboList;

                if (comboList.Count > 0)
                {
                    if (previousSession > 0 && comboList.Any(s => s.MaPhien == previousSession))
                        cbPhienHat.SelectedValue = previousSession;
                    else
                    {
                        if (previousSession > 0 && unconfirmedCart.Count > 0)
                        {
                            unconfirmedCart.Clear();
                            MessageBox.Show("Phiên hát trước không còn hoạt động. Giỏ chưa xác nhận đã được xóa; vui lòng kiểm tra phòng trước khi gọi lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        cbPhienHat.SelectedIndex = 0;
                    }
                    currentMaPhien = (int)cbPhienHat.SelectedValue;
                    previousSelectedMaPhien = currentMaPhien;
                    LoadSessionDetails(currentMaPhien);
                }
                else
                {
                    if (previousSession > 0 && unconfirmedCart.Count > 0)
                    {
                        unconfirmedCart.Clear();
                        MessageBox.Show("Phiên hát trước không còn hoạt động. Giỏ chưa xác nhận đã được xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    currentMaPhien = 0;
                    previousSelectedMaPhien = 0;
                    lblSessionDetail.Text = "Hiện không có phòng nào đang có phiên hát.";
                    dgvDaGoi.DataSource = null;
                    lblTongTienDichVu.Text = "Tiền dịch vụ đã gọi: 0 đ";
                    lblTienPhongTamTinh.Text = "Tiền giờ hát tạm tính: 0 đ";
                    lblTongCongTamTinh.Text = "TỔNG CỘNG TẠM TÍNH: 0 đ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phiên hát: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbPhienHat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbPhienHat.SelectedValue == null) return;
            int newMaPhien = (int)cbPhienHat.SelectedValue;

            if (newMaPhien == currentMaPhien) return;

            // Strict policy: Clear unsent cart only with confirmation when changing rooms
            if (unconfirmedCart.Count > 0)
            {
                var confirm = MessageBox.Show(
                    "Giỏ hàng dịch vụ hiện tại chưa được xác nhận gửi.\nBạn có chắc chắn muốn chuyển sang phòng khác và hủy bỏ giỏ hàng này?",
                    "Xác nhận chuyển phòng",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirm != DialogResult.Yes)
                {
                    // Revert selection back to previous
                    cbPhienHat.SelectedValue = previousSelectedMaPhien;
                    return;
                }

                unconfirmedCart.Clear();
            }

            currentMaPhien = newMaPhien;
            previousSelectedMaPhien = newMaPhien;
            LoadSessionDetails(currentMaPhien);
        }

        private void LoadSessionDetails(int maPhien)
        {
            if (maPhien <= 0) return;

            try
            {
                var orders = orderService.GetSessionOrders(maPhien);
                dgvDaGoi.DataSource = orders;

                var summary = orderService.GetSessionTotal(maPhien);
                lblSessionDetail.Text = string.Format(
                    "Phòng: {0} | Bắt đầu: {1:HH:mm dd/MM} | Đã hát: {2}h{3:D2}p | Giá phòng: {4:N0} đ/h",
                    summary.TenPhong,
                    summary.GioBatDau,
                    (int)summary.ThoiGianHat.TotalHours,
                    summary.ThoiGianHat.Minutes,
                    summary.DonGiaGio
                );

                lblTongTienDichVu.Text = string.Format("Tiền dịch vụ đã gọi: {0:N0} đ", summary.TienDichVu);
                lblTienPhongTamTinh.Text = string.Format("Tiền giờ hát tạm tính: {0:N0} đ", summary.TienPhongTamTinh);
                lblTongCongTamTinh.Text = string.Format("TỔNG CỘNG TẠM TÍNH: {0:N0} đ", summary.TongCongTamTinh);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCatalog()
        {
            try
            {
                string search = txtTimDichVu.Text.Trim();
                var items = orderService.GetActiveServices(search);
                dgvMenu.DataSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải menu dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimDichVu_TextChanged(object sender, EventArgs e)
        {
            LoadCatalog();
        }

        private void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            if (currentMaPhien <= 0)
            {
                MessageBox.Show("Vui lòng chọn một phòng đang sử dụng trước khi gọi dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvMenu.CurrentRow == null || !(dgvMenu.CurrentRow.DataBoundItem is ServiceItemRow item))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ muốn thêm từ danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int qty = (int)numSoLuong.Value;
            if (qty <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existing = unconfirmedCart.FirstOrDefault(x => x.MaDV == item.MaDV);
            if (existing != null)
            {
                existing.SoLuong += qty;
                unconfirmedCart.ResetBindings();
            }
            else
            {
                unconfirmedCart.Add(new CartItemInput
                {
                    MaDV = item.MaDV,
                    TenDV = item.TenDV,
                    DonViTinh = item.DonViTinh,
                    DonGia = item.DonGia,
                    SoLuong = qty
                });
            }

            numSoLuong.Value = 1;
            UpdateCartTotal();
        }

        private void dgvMenu_DoubleClick(object sender, EventArgs e)
        {
            btnThemVaoGio_Click(sender, e);
        }

        private void btnXoaMonGio_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow != null && dgvGioHang.CurrentRow.DataBoundItem is CartItemInput item)
            {
                unconfirmedCart.Remove(item);
                UpdateCartTotal();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món cần xóa khỏi giỏ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoaHetGio_Click(object sender, EventArgs e)
        {
            if (unconfirmedCart.Count == 0) return;

            if (MessageBox.Show("Xóa toàn bộ các món trong giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                unconfirmedCart.Clear();
                UpdateCartTotal();
            }
        }

        private void UpdateCartTotal()
        {
            decimal total = unconfirmedCart.Sum(x => x.ThanhTien);
            lblTongTienGio.Text = string.Format("Tổng giỏ: {0:N0} đ", total);
        }

        private void btnXacNhanGoi_Click(object sender, EventArgs e)
        {
            if (currentMaPhien <= 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần phục vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (unconfirmedCart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống. Vui lòng thêm dịch vụ vào giỏ trước khi xác nhận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                string.Format("Xác nhận gửi {0} món vào phòng này?\nTổng tiền giỏ: {1:N0} đ", unconfirmedCart.Count, unconfirmedCart.Sum(x => x.ThanhTien)),
                "Xác nhận gọi dịch vụ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                orderService.OrderServices(currentMaPhien, unconfirmedCart.ToList());
                MessageBox.Show("Đã xác nhận gọi dịch vụ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                unconfirmedCart.Clear();
                UpdateCartTotal();
                LoadSessionDetails(currentMaPhien);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đặt dịch vụ: " + ex.GetBaseException().Message, "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadActiveSessions();
            LoadCatalog();
        }
    }
}
