param([string]$Server = '(localdb)\MSSQLLocalDB')
$ErrorActionPreference = 'Stop'
$repo = Split-Path -Parent $PSScriptRoot
$testDb = 'KaraokeResetTest_' + [Guid]::NewGuid().ToString('N')
$testFolder = Join-Path (Split-Path -Parent $repo) $testDb
$connection = New-Object System.Data.SqlClient.SqlConnection "Server=$Server;Database=master;Integrated Security=True;Connect Timeout=10"
try {
    New-Item -ItemType Directory -Path $testFolder -Force | Out-Null
    Copy-Item -LiteralPath (Join-Path $repo 'SetupLocalDb.ps1') -Destination (Join-Path $testFolder 'SetupLocalDb.ps1')
    $sql = (Get-Content -LiteralPath (Join-Path $repo 'Karaoke404_Setup.sql') -Raw -Encoding UTF8).Replace('QuanLyKaraoke', $testDb)
    Set-Content -LiteralPath (Join-Path $testFolder 'Karaoke404_Setup.sql') -Value $sql -Encoding UTF8
    & (Join-Path $testFolder 'SetupLocalDb.ps1') -Server $Server
    $connection.Open(); $connection.ChangeDatabase($testDb); $cmd = $connection.CreateCommand()
    $cmd.CommandText = "SELECT MatKhauHash FROM dbo.TAIKHOAN WHERE TenDangNhap='admin' AND VaiTro='Admin'"
    $original = [string]$cmd.ExecuteScalar()
    if (!$original) { throw 'Fresh setup did not create the admin account.' }
    $cmd.CommandText = "UPDATE dbo.TAIKHOAN SET MatKhauHash='changed', SoLanDangNhapSai=5, KhoaDen=DATEADD(minute,15,GETUTCDATE()), PhienBanBaoMat=7 WHERE TenDangNhap='admin'"
    [void]$cmd.ExecuteNonQuery()
    & (Join-Path $testFolder 'SetupLocalDb.ps1') -Server $Server
    $cmd.CommandText = "SELECT MatKhauHash FROM dbo.TAIKHOAN WHERE TenDangNhap='admin'"
    if ([string]$cmd.ExecuteScalar() -ne 'changed') { throw 'Normal setup reset the password.' }
    & (Join-Path $testFolder 'SetupLocalDb.ps1') -Server $Server -ResetAdminPassword
    $cmd.CommandText = "SELECT COUNT(*) FROM dbo.TAIKHOAN WHERE TenDangNhap='admin' AND MatKhauHash=@hash AND SoLanDangNhapSai=0 AND KhoaDen IS NULL AND PhienBanBaoMat=8"
    [void]$cmd.Parameters.AddWithValue('@hash', $original)
    if ([int]$cmd.ExecuteScalar() -ne 1) { throw 'Reset did not restore hash, clear lockout and invalidate old sessions.' }
    Write-Output 'PASS: fresh setup, normal rerun, and explicit admin password reset.'
}
finally {
    if ($connection.State -ne [System.Data.ConnectionState]::Open) { $connection.Open() }
    $connection.ChangeDatabase('master'); $cmd = $connection.CreateCommand()
    $cmd.CommandText = "IF DB_ID(N'$testDb') IS NOT NULL BEGIN ALTER DATABASE [$testDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$testDb]; END"
    [void]$cmd.ExecuteNonQuery(); $connection.Dispose()
    if (Test-Path -LiteralPath $testFolder) { Remove-Item -LiteralPath $testFolder -Recurse -Force }
}
