using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BuisnessLogic
{
    public class DoWhileAnalyzer 
    {
        private bool? isExecuteMoreOne = null;
        public string Phrase { get; set; }
        public DoWhileAnalyzer(string phrase)
        {
            Phrase = phrase;
        }
        public bool? IsExecuteMoreOne
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
            dynamic value = 0;
            string ident = "";
            bool? result = null;

            foreach (var statement in main.Body.Statements)
            {
                if (statement is DoStatementSyntax doStatementSyntax)
                {
                    var condition = doStatementSyntax.Condition;
                    var body = doStatementSyntax.Statement;
                    foreach (var item in body.ChildNodes())
                    {
                        value = ApplyExpr(value, ident, item);
                    }
                    //if condition == true then result = true;
                }
                else if (statement is LocalDeclarationStatementSyntax lDecl)
                {
                    var decl = lDecl.Declaration;
                    var item = decl.Variables.First();
                    //foreach (var item in decl.Variables)
                    //{
                        ident = item.Identifier.ValueText;
                        value = item.Initializer.Value.GetFirstToken().Value;
                    //}
                }
            }
            return result;
        }
        //Сделать binaryExpr case
        private static dynamic ApplyExpr(dynamic value, string ident, SyntaxNode item)
        {
            switch ((item as ExpressionStatementSyntax).Expression)
            {
                case PostfixUnaryExpressionSyntax postUExpr:
                    string operandName = (postUExpr.Operand
                        as IdentifierNameSyntax)
                        .Identifier.ValueText;
                    if (operandName == ident)
                    {
                        var oper = postUExpr.OperatorToken.ValueText;
                        switch (oper)
                        {
                            case "++":
                                value++;
                                break;
                            case "--":
                                value--;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case PrefixUnaryExpressionSyntax preUExpr:
                    string operandName1 = (preUExpr.Operand
                        as IdentifierNameSyntax)
                        .Identifier.ValueText;
                    if (operandName1 == ident)
                    {
                        var oper = preUExpr.OperatorToken.ValueText;
                        switch (oper)
                        {
                            case "++":
                                value++;
                                break;
                            case "--":
                                value--;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case BinaryExpressionSyntax binaryExpressionSyntax:
                    var left = (binaryExpressionSyntax.Left as IdentifierNameSyntax)
                        .Identifier.ValueText;
                    //var right = (binaryExpressionSyntax.Right as)
                    //    .Identifier.ValueText;
                    //if (left == ident || right == ident)
                    //{
                    //    
                    //}
                    var oper1 = binaryExpressionSyntax.OperatorToken.ValueText;
                    switch (oper1)
                    {
                        case "+":
                            break;
                        case "-":
                            break;
                        case "*":
                            break;
                        case "/":
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            return value;
        }
    }
}
