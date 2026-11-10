param([string]$Server = '(localdb)\MSSQLLocalDB', [string]$AppDirectory = '')
$ErrorActionPreference = 'Stop'
$repo = Split-Path -Parent $PSScriptRoot
$app = if ($AppDirectory) { $AppDirectory } else { Join-Path $repo 'karaoke\bin\Debug' }
$exe = Join-Path $app 'PasswordChangeTests.exe'
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
& $compiler /nologo "/out:$exe" "/r:$app\karaoke.exe" "/r:$app\EntityFramework.dll" "/r:$app\BCrypt-Net-Next.dll" /r:System.Data.dll /r:System.Core.dll (Join-Path $PSScriptRoot 'PasswordChangeTests.cs')
if ($LASTEXITCODE -ne 0) { throw 'Test compilation failed.' }
$integrationExe = Join-Path $app 'IntegrationTests.exe'
& $compiler /nologo "/out:$integrationExe" "/r:$app\karaoke.exe" "/r:$app\EntityFramework.dll" /r:System.Data.dll /r:System.Core.dll (Join-Path $PSScriptRoot 'IntegrationTests.cs')
if ($LASTEXITCODE -ne 0) { throw 'Integration test compilation failed.' }
$uiExe = Join-Path $app 'AdminUiTests.exe'
& $compiler /nologo "/out:$uiExe" "/r:$app\karaoke.exe" "/r:$app\EntityFramework.dll" "/r:$app\Microsoft.ReportViewer.WinForms.dll" "/r:$app\Microsoft.ReportViewer.Common.dll" /r:System.Data.dll /r:System.Drawing.dll /r:System.Windows.Forms.dll /r:System.Core.dll (Join-Path $PSScriptRoot 'AdminUiTests.cs')
if ($LASTEXITCODE -ne 0) { throw 'UI test compilation failed.' }
$testDb = 'KaraokePasswordTest_' + [Guid]::NewGuid().ToString('N')
$connection = New-Object System.Data.SqlClient.SqlConnection "Server=$Server;Database=master;Integrated Security=True;Connect Timeout=5;ConnectRetryCount=0"
$created = $false
try {
    $connection.Open(); $command = $connection.CreateCommand()
    $command.CommandText = "CREATE DATABASE [$testDb]"; [void]$command.ExecuteNonQuery(); $created = $true
    $connection.ChangeDatabase($testDb)
    $sql = (Get-Content -LiteralPath (Join-Path $repo 'karaoke\Sql\Setup_QuanLyKaraoke.sql') -Raw -Encoding UTF8).Replace('QuanLyKaraoke', $testDb)
    foreach ($batch in [regex]::Split($sql, '(?im)^\s*GO\s*\r?$')) {
        if (![string]::IsNullOrWhiteSpace($batch)) { $command.CommandText = $batch; [void]$command.ExecuteNonQuery() }
    }
    $migration = Get-Content -LiteralPath (Join-Path $repo 'karaoke\Sql\04_PhanQuyen_NangCap.sql') -Raw -Encoding UTF8
    $command.CommandText = $migration; [void]$command.ExecuteNonQuery()
    $command.CommandText = "SELECT COUNT(*) FROM dbo.TAIKHOAN_NHOMQUYEN"; if ([int]$command.ExecuteScalar() -ne 1) { throw 'Existing admin was not assigned a group.' }
    $command.CommandText = "DELETE c FROM dbo.NHOMQUYEN_CHITIET c JOIN dbo.NHOMQUYEN n ON n.MaNhom=c.MaNhom WHERE n.TenNhom=N'Nhân viên' AND c.MaQuyen='GOIDICHVU'"; [void]$command.ExecuteNonQuery()
    $command.CommandText = $migration; [void]$command.ExecuteNonQuery()
    $command.CommandText = "SELECT COUNT(*) FROM dbo.NHOMQUYEN_CHITIET c JOIN dbo.NHOMQUYEN n ON n.MaNhom=c.MaNhom WHERE n.TenNhom=N'Nhân viên' AND c.MaQuyen='GOIDICHVU'"
    if ([int]$command.ExecuteScalar() -ne 0) { throw 'Migration reset a customized permission.' }
    Write-Output 'PASS: role migration assigns existing admin and preserves custom permissions on rerun'
    $command.CommandText = "CREATE TABLE dbo.NHATKYTAIKHOAN (Sentinel int NOT NULL); INSERT dbo.NHATKYTAIKHOAN VALUES (42); SELECT MatKhauHash FROM dbo.TAIKHOAN WHERE TenDangNhap='admin'"
    $originalHash = [string]$command.ExecuteScalar()
    foreach ($batch in [regex]::Split($sql, '(?im)^\s*GO\s*\r?$')) {
        if (![string]::IsNullOrWhiteSpace($batch)) { $command.CommandText = $batch; [void]$command.ExecuteNonQuery() }
    }
    $command.CommandText = "SELECT COUNT(*) FROM dbo.NHATKYTAIKHOAN WHERE Sentinel=42"
    if ([int]$command.ExecuteScalar() -ne 1) { throw 'Setup removed or changed the existing audit table.' }
    $command.CommandText = "SELECT MatKhauHash FROM dbo.TAIKHOAN WHERE TenDangNhap='admin'"
    if ([string]$command.ExecuteScalar() -ne $originalHash) { throw 'Setup changed the existing admin password.' }
    $command.CommandText = "SELECT COUNT(*) FROM dbo.NHOMQUYEN_CHITIET c JOIN dbo.NHOMQUYEN n ON n.MaNhom=c.MaNhom WHERE n.TenNhom=N'Nhân viên' AND c.MaQuyen='GOIDICHVU'"
    if ([int]$command.ExecuteScalar() -ne 0) { throw 'Setup reset a customized permission.' }
    Write-Output 'PASS: rerunning setup preserves audit data, admin password, and custom permissions'
    [xml]$config = Get-Content -LiteralPath (Join-Path $app 'karaoke.exe.config') -Raw
    $entry = $config.configuration.connectionStrings.add | Where-Object name -EQ 'QuanLyKaraokeEntities'
    $provider = "Server=$Server;Database=$testDb;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=5;ConnectRetryCount=0"
    $entityConnection = 'metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string="' + $provider + '"'
    $entry.SetAttribute('connectionString', $entityConnection); $config.Save("$exe.config")
    & $exe
    if ($LASTEXITCODE -ne 0) { throw 'Password-change tests failed.' }
    $config.Save("$integrationExe.config")
    & $integrationExe
    if ($LASTEXITCODE -ne 0) { throw 'Booking/service integration tests failed.' }
    $sample = (Get-Content -LiteralPath (Join-Path $repo 'karaoke\Sql\02_SampleData.sql') -Raw -Encoding UTF8).Replace('QuanLyKaraoke', $testDb)
    foreach ($iteration in 1..2) {
        foreach ($batch in [regex]::Split($sample, '(?im)^\s*GO\s*\r?$')) {
            if (![string]::IsNullOrWhiteSpace($batch)) { $command.CommandText = $batch; [void]$command.ExecuteNonQuery() }
        }
        $command.CommandText = "SELECT COUNT(*) FROM dbo.HOADON WHERE MaNV='NV001'"
        $count = [int]$command.ExecuteScalar()
        if ($count -ne 7) { throw "Expected seven sample invoices, got $count on run $iteration." }
        $command.CommandText = "SELECT COUNT(*) FROM dbo.HOADON h WHERE h.MaNV='NV001' AND h.TienDichVu <> COALESCE((SELECT SUM(c.SoLuong*c.DonGia) FROM dbo.CHITIETDICHVU c WHERE c.MaPhien=h.MaPhien),0)"
        if ([int]$command.ExecuteScalar() -ne 0) { throw "Sample invoice service totals do not match line items on run $iteration." }
    }
    Write-Output 'PASS: sample invoices match their service lines and seeding is repeatable'
    $config.Save("$uiExe.config")
    & $uiExe
    if ($LASTEXITCODE -ne 0) { throw 'Admin UI tests failed.' }
}
finally {
    if ($created) {
        $connection.ChangeDatabase('master')
        $command.CommandText = "ALTER DATABASE [$testDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$testDb]"
        [void]$command.ExecuteNonQuery()
    }
    $connection.Dispose()
}
