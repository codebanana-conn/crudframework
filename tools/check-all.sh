#!/usr/bin/env bash
# tools/check-all.sh — Lưới an toàn kiểm tra biên dịch trước khi commit (AGENTS.md mục 6).
#
# Chạy tuần tự:
#   1) build-core.sh   -> BIÊN DỊCH THẬT CrudFramework.Core (Roslyn + Libraries/*.dll).
#   2) syntax-check.sh -> RÀ SOÁT CÚ PHÁP WinForms + Sample (Roslyn parse, C#6).
#
# GIỚI HẠN (trung thực): KHÔNG thay thế build Windows + DevExpress v17.1. WinForms/Sample
# chỉ được kiểm cú pháp, không kiểm ngữ nghĩa (thiếu DevExpress + Windows Desktop pack).
#
# Thoát 0 nếu tất cả pass; khác 0 nếu bất kỳ bước nào lỗi.

set -uo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "=================================================================="
echo " CrudFramework — check-all"
echo "=================================================================="

FAIL=0

echo
echo ">>> [1/2] Biên dịch thật CrudFramework.Core"
if bash "$SCRIPT_DIR/build-core.sh"; then
    CORE_OK=1
else
    CORE_OK=0; FAIL=1
fi

echo
echo ">>> [2/2] Rà soát cú pháp CrudFramework.WinForms + CrudFramework.Sample"
if bash "$SCRIPT_DIR/syntax-check.sh"; then
    SYN_OK=1
else
    SYN_OK=0; FAIL=1
fi

echo
echo "=================================================================="
echo " TỔNG KẾT"
echo "   Core (biên dịch thật)        : $([[ $CORE_OK -eq 1 ]] && echo '✅ PASS' || echo '❌ FAIL')"
echo "   WinForms+Sample (syntax-check): $([[ $SYN_OK -eq 1 ]] && echo '✅ PASS' || echo '❌ FAIL')"
echo "   Lưu ý: syntax-check KHÔNG thay build Windows + DevExpress."
echo "=================================================================="

if [[ $FAIL -eq 0 ]]; then
    echo "[check-all] ✅ Tất cả kiểm tra PASS."
else
    echo "[check-all] ❌ Có kiểm tra THẤT BẠI — sửa trước khi commit." >&2
fi
exit $FAIL
