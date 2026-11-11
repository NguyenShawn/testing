param([string]$Server = '(localdb)\MSSQLLocalDB', [string]$AppDirectory = '')
$ErrorActionPreference = 'Stop'

function Assert-SqlSafety {
    param(
        [Parameter(Mandatory = $true)]
        [string]$SqlText,
        [Parameter(Mandatory = $true)]
        [string]$ExpectedDatabase
    )
    if ([string]::IsNullOrWhiteSpace($SqlText)) { return }
    $cleanSql = [regex]::Replace($SqlText, '(?m)--.*?$', '')
    $cleanSql = [regex]::Replace($cleanSql, '(?s)/\*.*?\*/', '')

    $useKeywordMatches = [regex]::Matches($cleanSql, '(?im)\bUSE\b')
    foreach ($useMatch in $useKeywordMatches) {
        $startIndex = $useMatch.Index + $useMatch.Length
        $rest = $cleanSql.Substring($startIndex)

        if ($rest -notmatch '^\s+') {
            throw 'Database safety violation: Unsupported USE syntax (missing whitespace after USE).'
        }

        $trimmedRest = $rest.TrimStart()

        if ($trimmedRest.StartsWith('[')) {
            $m = [regex]::Match($trimmedRest, '^\[(?<db>[^\]\r\n]+)\](?:\s*;|\s+|$)')
            if (-not $m.Success) {
                throw 'Database safety violation: Unsupported USE syntax (malformed bracketed identifier).'
            }
            $targetDb = $m.Groups['db'].Value.Trim()
        }
        elseif ($trimmedRest.StartsWith('"')) {
            $m = [regex]::Match($trimmedRest, '^"(?<db>[^"\r\n]+)"(?:\s*;|\s+|$)')
            if (-not $m.Success) {
                throw 'Database safety violation: Unsupported USE syntax (malformed double-quoted identifier).'
            }
            $targetDb = $m.Groups['db'].Value.Trim()
        }
        elseif ($trimmedRest -match '^(?<db>[a-zA-Z0-9_#$]+)(?:\s*;|\s+|$)') {
            $targetDb = $Matches['db'].Trim()
        }
        else {
            $badSnippet = [regex]::Match($trimmedRest, '^[^\r\n;]*').Value.Trim()
            throw "Database safety violation: Unsupported USE syntax: '$badSnippet'."
        }

        if ($targetDb -ne $ExpectedDatabase) {
            throw "Database safety violation: SQL contains 'USE $targetDb' which does not match expected test database '$ExpectedDatabase'."
        }
    }
}

function Assert-ActiveDatabase {
    param(
        [Parameter(Mandatory = $true)]
        [System.Data.SqlClient.SqlCommand]$Command,
        [Parameter(Mandatory = $true)]
        [string]$ExpectedDatabase
    )
    $Command.CommandText = 'SELECT DB_NAME()'
    $activeDb = [string]$Command.ExecuteScalar()
    if ($activeDb -ne $ExpectedDatabase) {
        throw "Database safety violation: active database is '$activeDb', expected '$ExpectedDatabase'."
    }
}

function Invoke-TestDbBatchSafe {
    param(
        [Parameter(Mandatory = $true)]
        [System.Data.SqlClient.SqlCommand]$Command,
        [Parameter(Mandatory = $true)]
        [string]$BatchSql,
        [Parameter(Mandatory = $true)]
        [string]$ExpectedDatabase
    )
    if ([string]::IsNullOrWhiteSpace($BatchSql)) { return }
    Assert-SqlSafety -SqlText $BatchSql -ExpectedDatabase $ExpectedDatabase
    Assert-ActiveDatabase -Command $Command -ExpectedDatabase $ExpectedDatabase
    $Command.CommandText = $BatchSql
    [void]$Command.ExecuteNonQuery()
    Assert-ActiveDatabase -Command $Command -ExpectedDatabase $ExpectedDatabase
}

function Invoke-TestDbScriptSafe {
    param(
        [Parameter(Mandatory = $true)]
        [System.Data.SqlClient.SqlCommand]$Command,
        [Parameter(Mandatory = $true)]
        [string]$ScriptSql,
        [Parameter(Mandatory = $true)]
        [string]$ExpectedDatabase
    )
    Assert-SqlSafety -SqlText $ScriptSql -ExpectedDatabase $ExpectedDatabase
    $batches = [regex]::Split($ScriptSql, '(?im)^\s*GO\s*\r?$')
    foreach ($batch in $batches) {
        if (![string]::IsNullOrWhiteSpace($batch)) {
            Invoke-TestDbBatchSafe -Command $Command -BatchSql $batch -ExpectedDatabase $ExpectedDatabase
        }
    }
}

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
    Assert-ActiveDatabase -Command $command -ExpectedDatabase 'master'
    $command.CommandText = "CREATE DATABASE [$testDb]"; [void]$command.ExecuteNonQuery(); $created = $true
    $connection.ChangeDatabase($testDb)
    Assert-ActiveDatabase -Command $command -ExpectedDatabase $testDb

    $sql = (Get-Content -LiteralPath (Join-Path $repo 'Karaoke404_Setup.sql') -Raw -Encoding UTF8) -replace 'QuanLyKaraoke', $testDb
    Invoke-TestDbScriptSafe -Command $command -ScriptSql $sql -ExpectedDatabase $testDb

    $migration = $sql
    Invoke-TestDbScriptSafe -Command $command -ScriptSql $migration -ExpectedDatabase $testDb

    Assert-ActiveDatabase -Command $command -ExpectedDatabase $testDb
    $command.CommandText = "SELECT COUNT(*) FROM dbo.TAIKHOAN_NHOMQUYEN"
    if ([int]$command.ExecuteScalar() -ne 1) { throw 'Existing admin was not assigned a group.' }

    Invoke-TestDbBatchSafe -Command $command -BatchSql "DELETE c FROM dbo.NHOMQUYEN_CHITIET c JOIN dbo.NHOMQUYEN n ON n.MaNhom=c.MaNhom WHERE n.TenNhom=N'Nhân viên' AND c.MaQuyen='GOIDICHVU'" -ExpectedDatabase $testDb

    Invoke-TestDbScriptSafe -Command $command -ScriptSql $migration -ExpectedDatabase $testDb

    Assert-ActiveDatabase -Command $command -ExpectedDatabase $testDb
    $command.CommandText = "SELECT COUNT(*) FROM dbo.NHOMQUYEN_CHITIET c JOIN dbo.NHOMQUYEN n ON n.MaNhom=c.MaNhom WHERE n.TenNhom=N'Nhân viên' AND c.MaQuyen='GOIDICHVU'"
    if ([int]$command.ExecuteScalar() -ne 0) { throw 'Migration reset a customized permission.' }
    Write-Output 'PASS: role migration assigns existing admin and preserves custom permissions on rerun'

    Invoke-TestDbBatchSafe -Command $command -BatchSql "CREATE TABLE dbo.NHATKYTAIKHOAN (Sentinel int NOT NULL); INSERT dbo.NHATKYTAIKHOAN VALUES (42)" -ExpectedDatabase $testDb
    Assert-ActiveDatabase -Command $command -ExpectedDatabase $testDb
    $command.CommandText = "SELECT MatKhauHash FROM dbo.TAIKHOAN WHERE TenDangNhap='admin'"
    $originalHash = [string]$command.ExecuteScalar()

    Invoke-TestDbScriptSafe -Command $command -ScriptSql $sql -ExpectedDatabase $testDb

    Assert-ActiveDatabase -Command $command -ExpectedDatabase $testDb
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

    $sample = $sql
    foreach ($iteration in 1..2) {
        Invoke-TestDbScriptSafe -Command $command -ScriptSql $sample -ExpectedDatabase $testDb
        Assert-ActiveDatabase -Command $command -ExpectedDatabase $testDb
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
        if ([string]::IsNullOrWhiteSpace($testDb) -or $testDb -eq 'QuanLyKaraoke' -or -not ($testDb -like 'KaraokePasswordTest_*')) {
            throw "Database safety violation: Refusing to drop invalid or protected database '$testDb'."
        }
        try {
            if ($connection) {
                if ($connection.State.ToString() -ne 'Open') {
                    $connection.Open()
                }
                $connection.ChangeDatabase('master')
                Assert-ActiveDatabase -Command $command -ExpectedDatabase 'master'
                $command.CommandText = "ALTER DATABASE [$testDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$testDb]"
                [void]$command.ExecuteNonQuery()
            }
        }
        catch {
            Write-Warning "Cleanup failed for test database '$testDb': $_"
        }
    }
    if ($connection) {
        $connection.Dispose()
    }
}
