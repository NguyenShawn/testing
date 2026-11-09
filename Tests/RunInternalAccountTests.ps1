param([switch]$Integration, [string]$Server = '(localdb)\MSSQLLocalDB')
$ErrorActionPreference = 'Stop'
$repo = Split-Path -Parent $PSScriptRoot
$app = Join-Path $repo 'karaoke\bin\Debug'
$exe = Join-Path $app 'InternalAccountTests.exe'
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
& $compiler /nologo "/out:$exe" "/r:$app\karaoke.exe" "/r:$app\EntityFramework.dll" "/r:$app\BCrypt-Net-Next.dll" /r:System.Data.dll /r:System.Core.dll (Join-Path $PSScriptRoot 'InternalAccountTests.cs')
if ($LASTEXITCODE -ne 0) { throw 'Test compilation failed.' }
[xml]$config = Get-Content -LiteralPath (Join-Path $app 'karaoke.exe.config') -Raw
if (!$Integration) {
    $config.Save("$exe.config"); & $exe
    if ($LASTEXITCODE -ne 0) { throw 'Offline tests failed.' }; return
}
$testDb = 'KaraokeInternalTest_' + [Guid]::NewGuid().ToString('N')
$connection = New-Object System.Data.SqlClient.SqlConnection "Server=$Server;Database=master;Integrated Security=True;Connect Timeout=5;ConnectRetryCount=0"
$created = $false
try {
    $connection.Open(); $command = $connection.CreateCommand()
    $command.CommandText = "CREATE DATABASE [$testDb]"; [void]$command.ExecuteNonQuery(); $created = $true
    $connection.ChangeDatabase($testDb)
    foreach ($name in @('Setup_QuanLyKaraoke.sql','Setup_QuanLyKaraoke.sql')) {
        $sql = (Get-Content -LiteralPath (Join-Path $repo "karaoke\Sql\$name") -Raw -Encoding UTF8).Replace('QuanLyKaraoke', $testDb)
        foreach ($batch in [regex]::Split($sql, '(?im)^\s*GO\s*\r?$')) {
            if (![string]::IsNullOrWhiteSpace($batch)) { $command.CommandText = $batch; [void]$command.ExecuteNonQuery() }
        }
    }
    $entry = $config.configuration.connectionStrings.add | Where-Object name -EQ 'QuanLyKaraokeEntities'
    $builder = New-Object System.Data.Common.DbConnectionStringBuilder
    $builder.ConnectionString = $entry.connectionString
    $builder['provider connection string'] = "Server=$Server;Database=$testDb;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=5;ConnectRetryCount=0"
    $entry.connectionString = $builder.ConnectionString; $config.Save("$exe.config")
    & $exe --database
    if ($LASTEXITCODE -ne 0) { throw 'Integration tests failed.' }
}
finally {
    if ($created) {
        $command.CommandText='IF @@TRANCOUNT>0 ROLLBACK TRANSACTION'; [void]$command.ExecuteNonQuery()
        $connection.ChangeDatabase('master')
        $command.CommandText="ALTER DATABASE [$testDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$testDb]"
        [void]$command.ExecuteNonQuery()
    }
    $connection.Dispose()
    Copy-Item -LiteralPath (Join-Path $app 'karaoke.exe.config') -Destination "$exe.config" -Force
}
