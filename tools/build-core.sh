#!/usr/bin/env bash
# tools/build-core.sh — Biên dịch THẬT CrudFramework.Core bằng Roslyn (csc) trên Linux/CI.
#
# CrudFramework.Core chỉ phụ thuộc BCL cơ bản + 3 DLL trong Libraries/ (Npgsql 2.2.3,
# Newtonsoft.Json, Mono.Security) => biên dịch được cross-platform bằng Roslyn với
# reference assemblies net8.0. Đây là BIÊN DỊCH NGỮ NGHĨA THẬT (bắt lỗi kiểu, thiếu using,
# sai chữ ký...) chứ không chỉ syntax-check.
#
# LƯU Ý: dùng LanguageVersion 6 (-langversion:6) đúng chuẩn repo (AGENTS.md mục 5).
# Kết quả .dll ghi vào tmp/out/CrudFramework.Core.dll (không commit).
#
# Thoát 0 nếu biên dịch thành công; khác 0 nếu có lỗi.

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

# shellcheck source=tools/dotnet-env.sh
source "$SCRIPT_DIR/dotnet-env.sh"

OUT_DIR="$REPO_ROOT/tmp/out"
mkdir -p "$OUT_DIR"
OUT_DLL="$OUT_DIR/CrudFramework.Core.dll"

echo "[build-core] Thu thập reference (net8.0 BCL + Libraries/*.dll) ..."
REFS=()
for d in "$NETREF_DIR"/*.dll; do REFS+=("-r:$d"); done
for d in "$REPO_ROOT/Libraries"/*.dll; do REFS+=("-r:$d"); done

echo "[build-core] Thu thập source CrudFramework.Core/*.cs ..."
mapfile -t SRC < <(find "$REPO_ROOT/CrudFramework.Core" -name '*.cs' ! -path '*/obj/*' ! -path '*/bin/*' | sort)
echo "[build-core] Số file: ${#SRC[@]}"

echo "[build-core] Biên dịch (langversion 6, target library) ..."
set +e
"$DOTNET" "$CSC_DLL" -nologo -langversion:6 -target:library -nostdlib \
    "${REFS[@]}" -out:"$OUT_DLL" "${SRC[@]}"
RC=$?
set -e

if [[ $RC -eq 0 ]]; then
    echo "[build-core] ✅ Biên dịch Core THÀNH CÔNG -> $OUT_DLL ($(wc -c < "$OUT_DLL") bytes)"
else
    echo "[build-core] ❌ Biên dịch Core THẤT BẠI (mã $RC)" >&2
fi
exit $RC
