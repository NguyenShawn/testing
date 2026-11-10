using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms;
using karaoke;
using karaoke.Models;
using karaoke.Services;

class MergeRegression
{
    static object Field(object target, string name) { return target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(target); }
    static void Invoke(object target, string name) { target.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(target, null); }
    static void Check(bool ok, string message) { if (!ok) throw new Exception(message); Console.WriteLine("PASS: " + message); }
    [STAThread]
    static int Main()
    {
        try
        {
            LicenseManager.CurrentContext = new DesigntimeLicenseContext();
            using (var form = new FrmQuanLyPhong())
            {
                foreach (var name in new[] { "panelHeader", "lblHeader", "panelLocPhong", "lblTimPhong", "txtTimPhong", "lblLocTrangThai", "cboLocTrangThai", "lblSoPhong" })
                    Check(Field(form, name) != null, "merged control initialized: " + name);
                var type = (ComboBox)Field(form, "cbLoaiPhong");
                var price = (TextBox)Field(form, "txtDonGia");
                type.DisplayMember = "TenLoaiPhong";
                type.DataSource = new List<LOAIPHONG> {
                    new LOAIPHONG { MaLoaiPhong = 7, TenLoaiPhong = "Standard", DonGiaGio = 99000 },
                    new LOAIPHONG { MaLoaiPhong = 8, TenLoaiPhong = "Premium", DonGiaGio = 210000 }
                };
                ((ComboBox)Field(form, "cbTrangThai")).Items.Add("Trống");
                form.Show(); Application.DoEvents();
                Check(price.Text == "99000", "initial catalog price, not hardcoded price");
                type.SelectedIndex = 1;
                Check(price.Text == "210000", "price follows selected type");
                Invoke(form, "ClearForm"); Invoke(form, "ClearForm");
                Check(price.Text == "99000" && price.ReadOnly, "repeated reset preserves default read-only price");
                var rooms = new List<RoomRow>();
                for (int i = 1; i <= 300; i++) rooms.Add(new RoomRow { MaPhong = "P" + i.ToString("000"), TenPhong = "Room " + i, LoaiPhong = "Standard", DonGia = 99000, SucChua = 6, TrangThai = "Trống" });
                form.GetType().GetField("allRooms", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(form, rooms);
                Invoke(form, "ApplyRoomFilter");
                var grid = (DataGridView)Field(form, "dataGridViewPhong");
                var panel = (FlowLayoutPanel)Field(form, "flowLayoutPanelPhong");
                Check(grid.Rows.Count == 300 && panel.Controls.Count == 300, "all 300 rooms retained");
                foreach (var size in new[] { new System.Drawing.Size(1100, 720), new System.Drawing.Size(1700, 1000) })
                {
                    form.ClientSize = size; form.PerformLayout(); Application.DoEvents();
                    panel.ScrollControlIntoView(panel.Controls[299]);
                    Check(panel.AutoScroll && panel.Controls[299].Bottom <= panel.ClientSize.Height, "last room reachable at " + size);
                }
                ((TextBox)Field(form, "txtTimPhong")).Text = "P300";
                Invoke(form, "ApplyRoomFilter");
                Check(grid.Rows.Count == 1, "search still works after merge");
                form.Close();
            }
            Console.WriteLine("All merge regression checks passed; no database writes."); return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
    }
}
