using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return null;
            //ААААА РАСССЕЕЕЕЕЯЯЯЯЯЯ
        }
    }
}
