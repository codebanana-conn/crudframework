#!/usr/bin/env bash
# tools/syntax-check.sh — Rà soát CÚ PHÁP C# 5/6 cho WinForms + Sample bằng Roslyn.
#
# Vì máy Linux/CI KHÔNG có DevExpress v17.1 và Windows Desktop reference pack, hai project
# CrudFramework.WinForms và CrudFramework.Sample KHÔNG thể biên dịch ngữ nghĩa ở đây.
# Script này chỉ PARSE (bắt lỗi cú pháp: thiếu ';', ngoặc lệch, dùng feature > C#6...).
# KHÔNG thay thế build Windows + DevExpress.
#
# Mặc định kiểm tra CrudFramework.WinForms và CrudFramework.Sample; có thể truyền path khác.

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

# shellcheck source=tools/dotnet-env.sh
source "$SCRIPT_DIR/dotnet-env.sh"

TARGETS=("$@")
if [[ ${#TARGETS[@]} -eq 0 ]]; then
    TARGETS=("CrudFramework.WinForms" "CrudFramework.Sample")
fi

echo "[syntax-check] Parse (Roslyn, C#6): ${TARGETS[*]}"
"$DOTNET" run --project "$SCRIPT_DIR/SyntaxCheck" -c Release -- "${TARGETS[@]}"
