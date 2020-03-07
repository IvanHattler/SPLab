using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BuisnessLogic
{
    public class DoWhileAnalizer
    {
        private bool? isExecuteMoreOne = null;
        public string Phrase { get; set; }
        public DoWhileAnalizer(string phrase)
        {
            Phrase = phrase;
        }
        public bool? IsExecute
        {
            get
            {
                return isExecuteMoreOne ?? (isExecuteMoreOne = CheckPhrase(Phrase));
            }
        }
        private bool? CheckPhrase(string phrase)
        {
            string programText =
            @"using System;
            using System.Collections;
            using System.Linq;
            using System.Text;
             
            namespace Namespace1
            {
                class Program
                {
                    static void Main(string[] args)
                    {
                        " + phrase + @"
                    }
                }
            }";
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            var main = (from methodDeclaration in root.DescendantNodes()
                                        .OfType<MethodDeclarationSyntax>()
                            where methodDeclaration.Identifier.ValueText == "Main"
                            select methodDeclaration).First();
            foreach(var statement in main.Body.Statements)
            {
                if (statement is DoStatementSyntax)
                {
                    var condition = (statement as DoStatementSyntax).Condition as ConditionalExpressionSyntax;
                    //var expSynt = condition.WhenTrue;
                }
                else if (statement is LocalDeclarationStatementSyntax)
                {
                    var decl = (statement as LocalDeclarationStatementSyntax).Declaration;
                    var type = decl.Type;
                    var vars = decl.Variables;
                    var str = type.GetFirstToken().Value;//.ToString();
                    foreach (var item in vars)
                    {
                        var ident = item.Identifier;
                        var value = item.Initializer.Value.GetFirstToken().Value;
                    }
                }
            }
            return null;
        }
    }
}
