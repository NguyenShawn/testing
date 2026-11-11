param([string]$Server = '(localdb)\MSSQLLocalDB', [switch]$ResetAdminPassword)
$ErrorActionPreference = 'Stop'
$scriptPath = Join-Path $PSScriptRoot 'Karaoke404_Setup.sql'
if (!(Test-Path -LiteralPath $scriptPath)) { throw 'Không tìm thấy script khởi tạo CSDL trong project.' }
if ($Server -eq '(localdb)\MSSQLLocalDB') {
    & sqllocaldb start MSSQLLocalDB
    if ($LASTEXITCODE -ne 0) { throw 'Không khởi động được LocalDB. Hãy cài SQL Server Express LocalDB rồi thử lại.' }
}
$connection = New-Object System.Data.SqlClient.SqlConnection "Server=$Server;Database=master;Integrated Security=True;Connect Timeout=10"
try {
    $connection.Open()
    $command = $connection.CreateCommand(); $command.CommandTimeout = 120
    $sql = Get-Content -LiteralPath $scriptPath -Raw -Encoding UTF8
    foreach ($batch in [regex]::Split($sql, '(?im)^\s*GO\s*\r?$')) {
        if (![string]::IsNullOrWhiteSpace($batch)) { $command.CommandText = $batch; [void]$command.ExecuteNonQuery() }
    }
    if ($ResetAdminPassword) {
        $match = [regex]::Match($sql, "DECLARE\s+@Hash\s+varchar\(255\)\s*=\s*'([^']+)'", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
        if (!$match.Success) { throw 'Không tìm thấy mật khẩu khởi tạo Admin trong script SQL; không đặt lại mật khẩu.' }
        $command.Parameters.Clear()
        [void]$command.Parameters.Add('@hash', [System.Data.SqlDbType]::VarChar, 255)
        $command.Parameters['@hash'].Value = $match.Groups[1].Value
        $command.CommandText = @"
UPDATE dbo.TAIKHOAN
SET MatKhauHash = @hash, SoLanDangNhapSai = 0, KhoaDen = NULL,
    PhienBanBaoMat = PhienBanBaoMat + 1
WHERE TenDangNhap = 'admin' AND VaiTro = 'Admin';
SELECT @@ROWCOUNT;
"@
        if ([int]$command.ExecuteScalar() -ne 1) { throw 'Không tìm thấy tài khoản admin có vai trò Admin; không đặt lại mật khẩu.' }
        Write-Output 'Đã đặt lại mật khẩu admin, xóa khóa do đăng nhập sai và đăng xuất các phiên cũ.'
    }
    Write-Output 'Đã chạy file SQL tổng Karaoke404_Setup.sql (cấu trúc, phân quyền, tồn kho và demo nếu database trống).'
    Write-Output 'Database mới: admin / AdminKaraoke2026!; hãy đổi mật khẩu sau khi đăng nhập.'
    if (!$ResetAdminPassword) { Write-Output 'Nếu tài khoản admin đã tồn tại, mật khẩu hiện có được giữ nguyên.' }
}
finally { $connection.Dispose() }
