#!/usr/bin/env bash
# tools/dotnet-env.sh — Thiết lập & phát hiện .NET SDK (Roslyn) dùng chung cho các script build.
#
# Script này KHÔNG chạy trực tiếp; các script khác `source` nó để có sẵn:
#   $DOTNET_ROOT   : thư mục cài .NET SDK
#   $DOTNET        : đường dẫn tới lệnh `dotnet`
#   $CSC_DLL       : đường dẫn tới Roslyn csc.dll
#   $NETREF_DIR    : thư mục reference assemblies net8.0 (cho -nostdlib)
#
# Nếu chưa có SDK, script tự tải dotnet-install.sh và cài vào "$HOME/.dotnet"
# (KHÔNG cần quyền root). Máy dev Windows có sẵn msbuild thì không dùng file này.

set -euo pipefail

# 1) Xác định DOTNET_ROOT: ưu tiên biến môi trường, rồi ~/.dotnet, rồi dotnet trong PATH.
if [[ -n "${DOTNET_ROOT:-}" && -x "${DOTNET_ROOT}/dotnet" ]]; then
    :
elif [[ -x "$HOME/.dotnet/dotnet" ]]; then
    export DOTNET_ROOT="$HOME/.dotnet"
elif command -v dotnet >/dev/null 2>&1; then
    export DOTNET_ROOT="$(dirname "$(command -v dotnet)")"
else
    export DOTNET_ROOT="$HOME/.dotnet"
fi

DOTNET="${DOTNET_ROOT}/dotnet"

# 2) Nếu chưa có dotnet -> cài user-space (không cần root).
if [[ ! -x "$DOTNET" ]]; then
    echo "[dotnet-env] Chưa có .NET SDK, đang cài vào $DOTNET_ROOT ..."
    _tmp_installer="$(mktemp)"
    if command -v curl >/dev/null 2>&1; then
        curl -sSL https://dot.net/v1/dotnet-install.sh -o "$_tmp_installer"
    elif command -v wget >/dev/null 2>&1; then
        wget -qO "$_tmp_installer" https://dot.net/v1/dotnet-install.sh
    else
        echo "[dotnet-env] LỖI: cần curl hoặc wget để tải .NET SDK." >&2
        exit 1
    fi
    bash "$_tmp_installer" --channel 8.0 --install-dir "$DOTNET_ROOT"
    rm -f "$_tmp_installer"
fi

export PATH="$DOTNET_ROOT:$PATH"
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_NOLOGO=1

# 3) Tìm Roslyn csc.dll trong SDK.
CSC_DLL="$(find "$DOTNET_ROOT/sdk" -name csc.dll -path '*Roslyn*' 2>/dev/null | sort | tail -1 || true)"
if [[ -z "$CSC_DLL" ]]; then
    echo "[dotnet-env] LỖI: không tìm thấy Roslyn csc.dll trong $DOTNET_ROOT/sdk" >&2
    exit 1
fi

# 4) Tìm reference assemblies net8.0 (dùng khi compile Core với -nostdlib).
NETREF_DIR="$(find "$DOTNET_ROOT/packs/Microsoft.NETCore.App.Ref" -type d -path '*ref/net*' 2>/dev/null | sort | tail -1 || true)"
if [[ -z "$NETREF_DIR" ]]; then
    echo "[dotnet-env] LỖI: không tìm thấy reference assemblies net8.0" >&2
    exit 1
fi

export DOTNET CSC_DLL NETREF_DIR
