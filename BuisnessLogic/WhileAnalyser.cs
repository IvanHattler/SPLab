using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic
{
    public class WhileAnalyzer
    {
        private string phrase;

        private int count;

        private string Iter;

        public string Phrase { get => phrase; set => phrase = value; }
        public int Count { get => count; set => count = value; }
        public string Iter1 { get => Iter; set => Iter = value; }

        //string WhileAnalyzer(string InputStream)
        //{
        //    string i = "while";
        //    for (int i = 0; i < InputStream.Length; i += 5)
        //    {
        //        if (i != InputStream.Substring(i, 5))
        //    }
        //}
    }
}
