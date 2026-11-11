using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using karaoke.Models;

namespace karaoke.Services
{
    internal static class LocalDatabaseSetup
    {
        // Only provision the application's default LocalDB database. Existing databases
        // and custom connection strings are left to their administrator.
        public static bool EnsureCreated()
        {
            string provider;
            using (var db = new QuanLyKaraokeEntities()) provider = db.Database.Connection.ConnectionString;
            var settings = new SqlConnectionStringBuilder(provider);
            if (!settings.DataSource.StartsWith(@"(localdb)\", StringComparison.OrdinalIgnoreCase) ||
                !settings.InitialCatalog.Equals("QuanLyKaraoke", StringComparison.OrdinalIgnoreCase)) return false;
            settings.InitialCatalog = "master";
            settings.ConnectTimeout = 10;
            using (var connection = new SqlConnection(settings.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandTimeout = 120;
                    command.CommandText = "DECLARE @r int; EXEC @r = sp_getapplock @Resource=N'Karaoke404InitialSetup', @LockMode='Exclusive', @LockOwner='Session', @LockTimeout=120000; SELECT @r;";
                    if (Convert.ToInt32(command.ExecuteScalar()) < 0) throw new InvalidOperationException("Không thể khóa quá trình tạo database.");
                    command.CommandText = "SELECT DB_ID(N'QuanLyKaraoke')";
                    if (command.ExecuteScalar() != DBNull.Value) return false;
                    using (var stream = typeof(LocalDatabaseSetup).Assembly.GetManifestResourceStream("karaoke.Sql.Setup_QuanLyKaraoke.sql"))
                    {
                        if (stream == null) throw new InvalidOperationException("Thiếu dữ liệu khởi tạo database trong ứng dụng.");
                        string sql;
                        using (var reader = new StreamReader(stream)) sql = reader.ReadToEnd();
                        foreach (string batch in Regex.Split(sql, @"(?im)^\s*GO\s*\r?$"))
                        {
                            if (string.IsNullOrWhiteSpace(batch)) continue;
                            command.CommandText = batch;
                            command.ExecuteNonQuery();
                        }
                    }
                    return true;
                }
            }
        }
    }
}
