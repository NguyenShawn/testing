param([string]$Server = '(localdb)\MSSQLLocalDB')
$ErrorActionPreference = 'Stop'
$repo = Split-Path -Parent $PSScriptRoot
$app = Join-Path $repo 'karaoke\bin\Debug'
$exe = Join-Path $app 'AccountProfileTests.exe'
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
& $compiler /nologo "/out:$exe" "/r:$app\karaoke.exe" "/r:$app\Guna.UI2.dll" "/r:$app\EntityFramework.dll" /r:System.Data.dll /r:System.Drawing.dll /r:System.Windows.Forms.dll /r:System.Core.dll (Join-Path $PSScriptRoot 'AccountProfileTests.cs')
if ($LASTEXITCODE -ne 0) { throw 'Không biên dịch được kiểm thử trang tài khoản.' }
$testDb = 'KaraokeProfileTest_' + [Guid]::NewGuid().ToString('N')
$connection = New-Object System.Data.SqlClient.SqlConnection "Server=$Server;Database=master;Integrated Security=True;Connect Timeout=5;ConnectRetryCount=0"
$created = $false
try {
    $connection.Open(); $command = $connection.CreateCommand(); $command.CommandTimeout = 120
    $command.CommandText = "CREATE DATABASE [$testDb]"; [void]$command.ExecuteNonQuery(); $created = $true
    $connection.ChangeDatabase($testDb)
    $sql = (Get-Content -LiteralPath (Join-Path $repo 'karaoke\Sql\Setup_QuanLyKaraoke.sql') -Raw -Encoding UTF8).Replace('QuanLyKaraoke', $testDb)
    foreach ($batch in [regex]::Split($sql, '(?im)^\s*GO\s*\r?$')) { if (![string]::IsNullOrWhiteSpace($batch)) { $command.CommandText = $batch; [void]$command.ExecuteNonQuery() } }
    [xml]$config = Get-Content -LiteralPath (Join-Path $app 'karaoke.exe.config') -Raw
    $entry = $config.configuration.connectionStrings.add | Where-Object name -EQ 'QuanLyKaraokeEntities'
    $provider = "Server=$Server;Database=$testDb;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=5;ConnectRetryCount=0"
    $entry.SetAttribute('connectionString', 'metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string="' + $provider + '"')
    $config.Save("$exe.config")
    & $exe
    if ($LASTEXITCODE -ne 0) { throw 'Kiểm thử trang tài khoản thất bại.' }
}
finally {
    if ($created) {
        $connection.ChangeDatabase('master')
        $command.CommandText = "ALTER DATABASE [$testDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$testDb]"
        [void]$command.ExecuteNonQuery()
    }
    $connection.Dispose()
}
