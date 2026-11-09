using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karaoke.Helpers
{
    public static class UserSession
    {
        public static int MaTK { get; set; }
        public static int PhienBanBaoMat { get; set; }
        public static string MaNV { get; set; }
        public static string TenDangNhap { get; set; }
        // Display only. Services reload the real account from SQL before protected operations.
        public static string VaiTro { get; set; }

        public static bool IsLoggedIn
        {
            get { return MaTK > 0; }
        }

        public static void Clear()
        {
            MaTK = 0;
            PhienBanBaoMat = 0;
            MaNV = null;
            TenDangNhap = null;
            VaiTro = null;
        }
    }
}
