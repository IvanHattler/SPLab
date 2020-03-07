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
            var compilation = CSharpCompilation.Create("Namespace1")
                .AddReferences(MetadataReference.CreateFromFile(
                    typeof(string).Assembly.Location))
                .AddSyntaxTrees(tree);
            SemanticModel model = compilation.GetSemanticModel(tree);
            //var main = (from methodDeclaration in root.DescendantNodes()
            //                            .OfType<MethodDeclarationSyntax>()
            //                where methodDeclaration.Identifier.ValueText == "Main"
            //                select methodDeclaration).First();
            //dynamic value;
            //SyntaxToken ident;
            //foreach (var statement in main.Body.Statements)
            //{
            //    if (statement is DoStatementSyntax doStatementSyntax)
            //    {
            //        var condition = doStatementSyntax.Condition;
            //        var body = doStatementSyntax.Statement;
            //        foreach (var item in body.ChildNodes())
            //        {

            //        }
            //    }
            //    else if (statement is LocalDeclarationStatementSyntax lDecl)
            //    {
            //        var decl = lDecl.Declaration;
            //        //var type = decl.Type;
            //        foreach (var item in decl.Variables)
            //        {
            //            ident = item.Identifier;
            //            value = item.Initializer.Value.GetFirstToken().Value;
            //        }
            //    }
            //}
            return null;
        }
    }
}
