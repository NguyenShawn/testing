param([string]$Server = '(localdb)\MSSQLLocalDB')
$ErrorActionPreference = 'Stop'
$scriptPath = Join-Path $PSScriptRoot 'karaoke\Sql\Setup_QuanLyKaraoke.sql'
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
    Write-Output 'Đã kiểm tra và khởi tạo cấu trúc QuanLyKaraoke.'
    Write-Output 'Database mới: admin / AdminKaraoke2026!; hãy đổi mật khẩu sau khi đăng nhập.'
    Write-Output 'Nếu tài khoản admin đã tồn tại, mật khẩu hiện có được giữ nguyên.'
}
finally { $connection.Dispose() }
