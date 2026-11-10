using System;
using System.Windows.Forms;
using karaoke.Services;

namespace karaoke
{
    public partial class FrmQuanLyDichVu : Form
    {
        private DichVuService dvService = new DichVuService();

        public FrmQuanLyDichVu()
        {
            InitializeComponent();

            this.Load += FrmQuanLyDichVu_Load;
            this.dgvDichVu.CellClick += dgvDichVu_CellClick;
            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnXoa.Click += btnXoa_Click;
            this.btnLamMoi.Click += btnLamMoi_Click;

            ConfigureUI();
        }

        private void ConfigureUI()
        {
            this.Text = "Quản lý Dịch vụ";
            this.btnThem.Text = "Thêm";
            this.btnSua.Text = "Sửa";
            this.btnXoa.Text = "Xóa";
            this.btnLamMoi.Text = "Làm mới";
            this.chkTrangThai.Text = "Đang kinh doanh";

            dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDichVu.MultiSelect = false;
            dgvDichVu.ReadOnly = true;
        }

        private void FrmQuanLyDichVu_Load(object sender, EventArgs e)
        {
            try {
                bool admin = AuthService.HasPermission("DICHVU_SUA");
                btnThem.Enabled = btnSua.Enabled = admin;
                btnXoa.Enabled = admin;
            } catch (Exception ex) { MessageBox.Show(ex.Message); Close(); return; }
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvDichVu.DataSource = dvService.List();

                if (dgvDichVu.Columns["MaDV"] != null) dgvDichVu.Columns["MaDV"].HeaderText = "Mã dịch vụ";
                if (dgvDichVu.Columns["TenDV"] != null) dgvDichVu.Columns["TenDV"].HeaderText = "Tên dịch vụ";
                if (dgvDichVu.Columns["DonViTinh"] != null) dgvDichVu.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
                if (dgvDichVu.Columns["DonGia"] != null)
                {
                    dgvDichVu.Columns["DonGia"].HeaderText = "Đơn giá";
                    dgvDichVu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                }
                if (dgvDichVu.Columns["TrangThai"] != null) dgvDichVu.Columns["TrangThai"].HeaderText = "Trạng thái";

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách dịch vụ: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtMaDV.Clear();
            txtTenDV.Clear();
            txtDonViTinh.Clear();
            txtDonGia.Clear();
            chkTrangThai.Checked = true;

            txtMaDV.Enabled = true;
            txtMaDV.Focus();

            dgvDichVu.ClearSelection();
        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDichVu.Rows[e.RowIndex];
                txtMaDV.Text = row.Cells["MaDV"].Value?.ToString();
                txtTenDV.Text = row.Cells["TenDV"].Value?.ToString();
                txtDonViTinh.Text = row.Cells["DonViTinh"].Value?.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value?.ToString();

                if (row.Cells["TrangThai"].Value != null)
                {
                    chkTrangThai.Checked = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                }

                txtMaDV.Enabled = false;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text))
            {
                MessageBox.Show("Mã dịch vụ không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Tên dịch vụ không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDV.Focus();
                return false;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là một số hợp lệ và lớn hơn hoặc bằng 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                decimal donGia = decimal.Parse(txtDonGia.Text.Trim());

                dvService.Save(
                    txtMaDV.Text.Trim(),
                    txtTenDV.Text.Trim(),
                    txtDonViTinh.Text.Trim(),
                    donGia,
                    chkTrangThai.Checked,
                    true 
                );

                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaDV.Enabled)
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ cần sửa từ danh sách bên dưới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput()) return;

                decimal donGia = decimal.Parse(txtDonGia.Text.Trim());

                dvService.Save(
                    txtMaDV.Text.Trim(),
                    txtTenDV.Text.Trim(),
                    txtDonViTinh.Text.Trim(),
                    donGia,
                    chkTrangThai.Checked,
                    false
                );

                MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDV.Text))
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ cần xóa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ [{txtMaDV.Text} - {txtTenDV.Text}] không?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    dvService.Delete(txtMaDV.Text.Trim());
                    MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }


    }
}