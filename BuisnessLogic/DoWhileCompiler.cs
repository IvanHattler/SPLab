using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace BuisnessLogic
{
    /// <summary>
    /// Класс для разбора конструкций do-while и while
    /// </summary>
    public class DoWhileCompiler
    {
        /// <summary>
        /// True - распознавать do-while, False - распознавать while
        /// </summary>
        public bool IsDoWhile { get; set; }
        /// <summary>
        /// Входная конструкция для разбора
        /// </summary>
        public string Phrase { get; set; }
        public DoWhileCompiler(string phrase, bool isDoWhile)
        {
            Phrase = phrase;
            IsDoWhile = isDoWhile;
        }
        private string Modify(string phrase)
        {
            string tempVar = $"tmp_qrwer";
            string res = @"namespace Namespace1
{
    class Program
    {
        static int Main(string[] args)
        {
                        int " + tempVar + "= 0;" +
                       phrase + @"
                        return " + tempVar +@";
        }
    }
}";
            SyntaxTree tree = CSharpSyntaxTree.ParseText(res);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            dynamic cycle;
            if (IsDoWhile)
            {
                cycle = (from methodDeclaration in root.DescendantNodes()
                                        .OfType<DoStatementSyntax>()
                         select methodDeclaration).First();
                res = res.Insert(cycle.Statement.FullSpan.Start + 1, $"{tempVar}++;");
            }
            else
            {
                cycle = (from methodDeclaration in root.DescendantNodes()
                                        .OfType<WhileStatementSyntax>()
                         select methodDeclaration).First();
                res = res.Insert(cycle.Statement.FullSpan.Start + 1, $"{tempVar}++;");
            }
            return res;
        }
        /// <summary>
        /// Метод для определения, выполнится ли цикл больше 1 раза
        /// </summary>
        /// <returns></returns>
        public bool? CheckPhrase()
        {
            bool? result = null;
            int count = CheckCount();
            if (count > 1)
                result = true;
            else if (count >= 0)
                result = false;
            return result;
        }
        /// <summary>
        /// Метод для определения количества итераций цикла
        /// </summary>
        /// <returns></returns>
        public int CheckCount()
        {
            int result = 0;
            string programText = Modify(Phrase);
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = $"temp_{Guid.NewGuid()}.exe",
            };
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, programText);
            if (results.Errors.Count == 0)
            {
                using (Process pr = new Process())
                {
                    pr.StartInfo.FileName = parameters.OutputAssembly;
                    pr.StartInfo.UseShellExecute = false;
                    pr.Start();
                    pr.WaitForExit(2000);
                    if (!pr.HasExited)
                        pr.Kill();
                    result = pr.ExitCode;
                }
            }
            else
            {
                throw new Infrastructure.CompilationException(results.Errors[0]);
            }
            return result;
        }
    }
}
