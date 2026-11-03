
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace karaoke
{
    public partial class FrmMain : Form
    {
        private readonly string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=QuanLyKaraoke;Integrated Security=True;";

        public FrmMain()
        {
            InitializeComponent();

            this.Load += FrmMain_Load;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDashboard();
                LoadChartDoanhThu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu Dashboard: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadDashboard()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlPhong = @"
                    SELECT
                        COUNT(*) AS TongPhong,
                        COUNT(CASE
                            WHEN TrangThai = N'Đang sử dụng' THEN 1
                        END) AS DangSuDung,
                        COUNT(CASE
                            WHEN TrangThai = N'Trống' THEN 1
                        END) AS PhongTrong
                    FROM dbo.PHONG";

                using (SqlCommand cmd = new SqlCommand(sqlPhong, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblTongPhong.Text = reader["TongPhong"].ToString();
                        lblDangSuDung.Text = reader["DangSuDung"].ToString();
                        lblPhongTrong.Text = reader["PhongTrong"].ToString();
                    }
                }

                string sqlDoanhThu = @"
                    SELECT ISNULL(SUM(ThanhTien), 0)
                    FROM dbo.HOADON
                    WHERE NgayThanhToan >= @TuNgay
                      AND NgayThanhToan < @DenNgay";

                using (SqlCommand cmd = new SqlCommand(sqlDoanhThu, conn))
                {
                    cmd.Parameters.Add("@TuNgay", SqlDbType.DateTime2)
                        .Value = DateTime.Today;

                    cmd.Parameters.Add("@DenNgay", SqlDbType.DateTime2)
                        .Value = DateTime.Today.AddDays(1);

                    decimal doanhThu = Convert.ToDecimal(
                        cmd.ExecuteScalar()
                    );

                    lblDoanhThu.Text = doanhThu.ToString("N0") + "đ";
                }
            }
        }

        private void LoadChartDoanhThu()
        {
            int nam = DateTime.Today.Year;
            decimal[] doanhThuThang = new decimal[13];

            string sql = @"
                SELECT
                    MONTH(NgayThanhToan) AS Thang,
                    SUM(ThanhTien) AS DoanhThu
                FROM dbo.HOADON
                WHERE NgayThanhToan >= @TuNgay
                  AND NgayThanhToan < @DenNgay
                GROUP BY MONTH(NgayThanhToan)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@TuNgay", SqlDbType.DateTime2)
                        .Value = new DateTime(nam, 1, 1);

                    cmd.Parameters.Add("@DenNgay", SqlDbType.DateTime2)
                        .Value = new DateTime(nam + 1, 1, 1);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int thang = Convert.ToInt32(reader["Thang"]);

                            decimal doanhThu =
                                Convert.ToDecimal(reader["DoanhThu"]);

                            doanhThuThang[thang] = doanhThu;
                        }
                    }
                }
            }

            chart1.Series.Clear();

            Series series = new Series("DoanhThu");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.SkyBlue;
            series.IsVisibleInLegend = false;

            for (int thang = 1; thang <= 12; thang++)
            {
                series.Points.AddXY(
                    thang,
                    (double)(doanhThuThang[thang] / 1000000m)
                );
            }

            chart1.Series.Add(series);

            ChartArea area = chart1.ChartAreas[0];

            area.AxisX.Minimum = 1;
            area.AxisX.Maximum = 12;
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Format = "0";
            area.AxisX.Title = "Tháng";
            area.AxisX.MajorGrid.Enabled = false;


            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = double.NaN;
            area.AxisY.Interval = 0;
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisY.Title = "Doanh thu (triệu đồng)";
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
        }
    }
}
