using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.Infrastructure
{
    /// <summary>
    /// Класс исключений при компиляции
    /// </summary>
    public class CompilationException: ApplicationException
    {
        public CompilationException(CompilerError error):base(
            $"{error.ErrorText} ({error.Line},{error.Column})")
        {}
    }
}
