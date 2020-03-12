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

        public int getindex(int i,string lastword,string phr)
        {
            int k = 0;
            int ans = 0;
            while (k<phr.Length)
            {
                if (phr.Substring(k, lastword.Length) == lastword)
                {
                   ans = i + lastword.Length;
                }
                else
                    k += lastword.Length;
            }
            return ans;
        }

        string WhileAnalyzer(string InputStream)
        {
            string word = "while";
            int i = 0;
            bool flag = true;
            int k = 0;
            while (i < InputStream.Length&&flag)
            {
                if(InputStream.Substring(i,5)==word)
                {
                    i += 5;
                    flag = false;
                }
                i += 5;
            }
            while(i< InputStream.Length && flag2)
            {
                if (InputStream.Substring(i, 1) =='(')
                {
                    k++;
                }
            }

            //string body = InputStream.Substring(i, InputStream.Length - i);
        }
    }
}
