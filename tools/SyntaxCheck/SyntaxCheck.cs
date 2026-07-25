// SyntaxCheck.cs — Công cụ rà soát CÚ PHÁP C# 5/6 cho CrudFramework.
//
// MỤC ĐÍCH
//   Máy CI/agent chạy trên Linux KHÔNG có DevExpress v17.1 và không có Windows Desktop
//   reference pack (System.Windows.Forms/System.Drawing đầy đủ), nên KHÔNG thể biên dịch
//   ngữ nghĩa (semantic) hai project CrudFramework.WinForms và CrudFramework.Sample.
//
//   Tool này dùng Roslyn (Microsoft.CodeAnalysis.CSharp — đi kèm .NET SDK) để PARSE từng
//   file .cs với LanguageVersion = CSharp6 và báo mọi lỗi CÚ PHÁP (missing ';', ngoặc lệch,
//   dùng feature > C#6, v.v.). Đây KHÔNG phải build đầy đủ — chỉ là lưới an toàn bắt lỗi
//   cú pháp trước khi commit. Build ngữ nghĩa thật vẫn cần Windows + DevExpress.
//
// CÁCH DÙNG
//   dotnet run --project tools/SyntaxCheck -- <thư-mục-hoặc-file> [<thư-mục-hoặc-file> ...]
//   Thoát mã 0 nếu không có lỗi cú pháp; 1 nếu có lỗi; 2 nếu tham số sai.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace SyntaxCheck
{
    internal static class Program
    {
        // Ép cú pháp về đúng chuẩn repo: C# 6.0 (khớp AGENTS.md mục 5).
        private static readonly CSharpParseOptions ParseOptions =
            new CSharpParseOptions(LanguageVersion.CSharp6);

        private static int Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.Error.WriteLine(
                    "Cách dùng: dotnet run --project tools/SyntaxCheck -- <đường-dẫn> [<đường-dẫn> ...]");
                return 2;
            }

            var files = new List<string>();
            foreach (var arg in args)
            {
                if (Directory.Exists(arg))
                {
                    files.AddRange(Directory.EnumerateFiles(arg, "*.cs", SearchOption.AllDirectories));
                }
                else if (File.Exists(arg))
                {
                    files.Add(arg);
                }
                else
                {
                    Console.Error.WriteLine("[BỎ QUA] Không tìm thấy: " + arg);
                }
            }

            // Loại trừ file sinh tự động / thư mục build.
            files = files
                .Where(f => !f.Replace('\\', '/').Contains("/obj/"))
                .Where(f => !f.Replace('\\', '/').Contains("/bin/"))
                .Distinct()
                .OrderBy(f => f, StringComparer.Ordinal)
                .ToList();

            if (files.Count == 0)
            {
                Console.Error.WriteLine("Không có file .cs nào để kiểm tra.");
                return 2;
            }

            var totalErrors = 0;
            var checkedCount = 0;

            foreach (var file in files)
            {
                string source;
                try
                {
                    source = File.ReadAllText(file);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("[LỖI ĐỌC] " + file + ": " + ex.Message);
                    totalErrors++;
                    continue;
                }

                var tree = CSharpSyntaxTree.ParseText(
                    SourceText.From(source), ParseOptions, path: file);

                var diagnostics = tree.GetDiagnostics()
                    .Where(d => d.Severity == DiagnosticSeverity.Error)
                    .ToList();

                checkedCount++;

                foreach (var d in diagnostics)
                {
                    totalErrors++;
                    var span = d.Location.GetLineSpan();
                    var line = span.StartLinePosition.Line + 1;
                    var col = span.StartLinePosition.Character + 1;
                    Console.Error.WriteLine(
                        string.Format("{0}({1},{2}): error {3}: {4}",
                            file, line, col, d.Id, d.GetMessage()));
                }
            }

            Console.WriteLine(string.Format(
                "[SyntaxCheck] Đã kiểm tra {0} file, phát hiện {1} lỗi cú pháp.",
                checkedCount, totalErrors));

            return totalErrors == 0 ? 0 : 1;
        }
    }
}
