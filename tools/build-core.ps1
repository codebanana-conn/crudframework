<#
    tools/build-core.ps1 — Build CrudFramework.Core trên máy Windows dev (dùng MSBuild thật).

    Trên Windows có Visual Studio / Build Tools, cách chính xác nhất để build là MSBuild —
    build đúng target .NET Framework v4.5 với reference thật trong Libraries/.

    Script này build project Core (và có thể cả solution) bằng MSBuild. Trên Windows có
    DevExpress v17.1 cài sẵn, bạn có thể build cả CrudFramework.sln (WinForms + Sample) —
    xem tham số -All.

    Cách dùng:
      pwsh tools/build-core.ps1            # build CrudFramework.Core
      pwsh tools/build-core.ps1 -All       # build toàn solution (cần DevExpress v17.1)
#>
[CmdletBinding()]
param(
    [switch]$All,
    [string]$Configuration = "Debug"
)

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent $PSScriptRoot

function Resolve-MSBuild {
    $candidates = @()
    $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vswhere) {
        $path = & $vswhere -latest -requires Microsoft.Component.MSBuild `
            -find "MSBuild\**\Bin\MSBuild.exe" 2>$null | Select-Object -First 1
        if ($path) { $candidates += $path }
    }
    $cmd = Get-Command msbuild -ErrorAction SilentlyContinue
    if ($cmd) { $candidates += $cmd.Source }
    if ($candidates.Count -eq 0) {
        throw "Khong tim thay MSBuild. Cai Visual Studio hoac Build Tools for VS."
    }
    return $candidates[0]
}

$msbuild = Resolve-MSBuild
Write-Host "[build-core] MSBuild: $msbuild"

if ($All) {
    $target = Join-Path $repoRoot "CrudFramework.sln"
    Write-Host "[build-core] Build TOAN SOLUTION (can DevExpress v17.1): $target"
} else {
    $target = Join-Path $repoRoot "CrudFramework.Core\CrudFramework.Core.csproj"
    Write-Host "[build-core] Build CrudFramework.Core: $target"
}

& $msbuild $target /nologo /verbosity:minimal "/p:Configuration=$Configuration"
$rc = $LASTEXITCODE
if ($rc -eq 0) {
    Write-Host "[build-core] Build THANH CONG."
} else {
    Write-Error "[build-core] Build THAT BAI (ma $rc)."
}
exit $rc
