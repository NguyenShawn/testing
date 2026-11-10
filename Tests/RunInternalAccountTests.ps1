param([switch]$Integration, [string]$Server = '(localdb)\MSSQLLocalDB')
$ErrorActionPreference = 'Stop'
$repo = Split-Path -Parent $PSScriptRoot
$app = Join-Path $repo 'karaoke\bin\Debug'
$exe = Join-Path $app 'InternalAccountTests.exe'
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
& $compiler /nologo "/out:$exe" "/r:$app\karaoke.exe" "/r:$app\EntityFramework.dll" "/r:$app\BCrypt-Net-Next.dll" /r:System.Data.dll /r:System.Core.dll (Join-Path $PSScriptRoot 'InternalAccountTests.cs')
if ($LASTEXITCODE -ne 0) { throw 'Test compilation failed.' }
Copy-Item -LiteralPath (Join-Path $app 'karaoke.exe.config') -Destination "$exe.config" -Force
& $exe
if ($LASTEXITCODE -ne 0) { throw 'Offline tests failed.' }
if ($Integration) {
    & (Join-Path $PSScriptRoot 'RunPasswordChangeTests.ps1') -Server $Server
    if ($LASTEXITCODE -ne 0) { throw 'Integration tests failed.' }
}
