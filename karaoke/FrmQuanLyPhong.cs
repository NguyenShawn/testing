using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace karaoke
{
    public partial class FrmQuanLyPhong : Form
    {
        private readonly string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=QuanLyKaraoke;Integrated Security=True;";

        private int selectedMaPhong = -1; 

        public FrmQuanLyPhong()
        {
            InitializeComponent();
            this.Load += FrmQuanLyPhong_Load;
        }

        private void FrmQuanLyPhong_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTrangThaiComboBox();
                LoadDanhSachPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu phòng: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadTrangThaiComboBox()
        {
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.Add("Trống");
            cbTrangThai.Items.Add("Đang sử dụng");
            cbTrangThai.Items.Add("Bảo trì");
            cbTrangThai.SelectedIndex = 0;
        }

        private void LoadDanhSachPhong()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT MaPhong, TenPhong, LoaiPhong, DonGia, TrangThai FROM dbo.PHONG";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridViewPhong.DataSource = dt;

                    if (dataGridViewPhong.Columns["MaPhong"] != null)
                    {
                        dataGridViewPhong.Columns["MaPhong"].HeaderText = "Mã Phòng";
                        dataGridViewPhong.Columns["TenPhong"].HeaderText = "Tên Phòng";
                        dataGridViewPhong.Columns["LoaiPhong"].HeaderText = "Loại Phòng";
                        dataGridViewPhong.Columns["DonGia"].HeaderText = "Đơn Giá (VNĐ)";
                        dataGridViewPhong.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                        dataGridViewPhong.Columns["TrangThai"].HeaderText = "Trạng Thái";
                    }

                    HienThiCardPhong(dt);
                }
            }
        }

        private void HienThiCardPhong(DataTable dt)
        {
            flowLayoutPanelPhong.Controls.Clear();

            foreach (DataRow row in dt.Rows)
            {
                int maPhong = Convert.ToInt32(row["MaPhong"]);
                string tenPhong = row["TenPhong"].ToString();
                string loaiPhong = row["LoaiPhong"].ToString();
                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                string trangThai = row["TrangThai"].ToString();

                Button btnPhong = new Button();
                btnPhong.Width = 140;
                btnPhong.Height = 100;
                btnPhong.Tag = maPhong; 
                btnPhong.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btnPhong.TextAlign = ContentAlignment.MiddleCenter;
                btnPhong.Cursor = Cursors.Hand;

                if (trangThai == "Trống")
                {
                    btnPhong.BackColor = Color.LightGreen;
                    btnPhong.ForeColor = Color.DarkGreen;
                }
                else if (trangThai == "Đang sử dụng")
                {
                    btnPhong.BackColor = Color.LightCoral;
                    btnPhong.ForeColor = Color.DarkRed;
                }
                else 
                {
                    btnPhong.BackColor = Color.LightGray;
                    btnPhong.ForeColor = Color.DimGray;
                }

                btnPhong.Text = $"{tenPhong}\n({loaiPhong})\n{trangThai}";

                btnPhong.Click += (s, e) =>
                {
                    selectedMaPhong = maPhong;
                    txtMaPhong.Text = maPhong.ToString();
                    txtTenPhong.Text = tenPhong;
                    txtLoaiPhong.Text = loaiPhong;
                    txtDonGia.Text = donGia.ToString("0");
                    cbTrangThai.SelectedItem = trangThai;
                };

                flowLayoutPanelPhong.Controls.Add(btnPhong);
            }
        }

        private void dataGridViewPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewPhong.Rows[e.RowIndex];
                selectedMaPhong = Convert.ToInt32(row.Cells["MaPhong"].Value);
                txtTenPhong.Text = row.Cells["TenPhong"].Value.ToString();
                txtLoaiPhong.Text = row.Cells["LoaiPhong"].Value.ToString();
                txtDonGia.Text = Convert.ToDecimal(row.Cells["DonGia"].Value).ToString("0");
                cbTrangThai.SelectedItem = row.Cells["TrangThai"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhong.Text) || string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Tên phòng và Đơn giá!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO dbo.PHONG (MaPhong, TenPhong, LoaiPhong, DonGia, TrangThai) VALUES (@MaPhong, @TenPhong, @LoaiPhong, @DonGia, @TrangThai)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", txtMaPhong.Text.Trim());
                        cmd.Parameters.AddWithValue("@TenPhong", txtTenPhong.Text.Trim());
                        cmd.Parameters.AddWithValue("@LoaiPhong", txtLoaiPhong.Text.Trim());
                        cmd.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(txtDonGia.Text));
                        cmd.Parameters.AddWithValue("@TrangThai", cbTrangThai.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachPhong();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaPhong == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng cần sửa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE dbo.PHONG SET TenPhong = @TenPhong, LoaiPhong = @LoaiPhong, DonGia = @DonGia, TrangThai = @TrangThai WHERE MaPhong = @MaPhong";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@MaPhong", selectedMaPhong);
                        cmd.Parameters.AddWithValue("@TenPhong", txtTenPhong.Text.Trim());
                        cmd.Parameters.AddWithValue("@LoaiPhong", txtLoaiPhong.Text.Trim());
                        cmd.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(txtDonGia.Text));
                        cmd.Parameters.AddWithValue("@TrangThai", cbTrangThai.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachPhong();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaPhong == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string sql = "DELETE FROM dbo.PHONG WHERE MaPhong = @MaPhong";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaPhong", selectedMaPhong);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Xóa phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachPhong();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa phòng này vì có dữ liệu liên quan (Hóa đơn, v.v.): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachPhong();
        }

        private void ClearForm()
        {
            selectedMaPhong = -1;
            txtMaPhong.Clear();
            txtTenPhong.Clear();
            txtLoaiPhong.Clear();
            txtDonGia.Clear();
            if (cbTrangThai.Items.Count > 0) cbTrangThai.SelectedIndex = 0;
            txtTenPhong.Focus();
        }
    }
}