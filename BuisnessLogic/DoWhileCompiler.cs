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
    public class DoWhileCompiler
    {
        private bool? isExecuteMoreOne = null;
        public string Phrase { get; set; }
        public DoWhileCompiler(string phrase)
        {
            Phrase = phrase;
        }
        public bool? IsExecuteMoreOne
        {
            get
            {
                return isExecuteMoreOne ?? (isExecuteMoreOne = CheckPhrase());
            }
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
            var cycle = (from methodDeclaration in root.DescendantNodes()
                                        .OfType<DoStatementSyntax>()
                         select methodDeclaration).First();
            res = res.Insert(cycle.Statement.FullSpan.Start+1, $"{tempVar}++;");
            return res;
        }
        private bool? CheckPhrase()
        {
            bool? result = null;
            string tempVar = $"t_{Guid.NewGuid()}";
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
                    pr.WaitForExit();
                    if (pr.ExitCode > 1)
                        result = true;
                    else
                        result = false;
                }
            }
            return result;
        }
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
                    pr.WaitForExit();
                    result = pr.ExitCode;
                }
            }
            return result;
        }
    }
}
