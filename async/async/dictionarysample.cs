using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace async
{
    public class dictionarysample
    {

        public void Execute()
        {
            //hashtable
            var dic = new Dictionary<string, string>();
            dic["name"] = "Hordor";

            //threadSafe
            var concDic = new ConcurrentDictionary<string, string>();
            
            //immutable
            //low perfomance
            //uses AVL tree
            var imutDic = ImmutableDictionary<string, string>.Empty;
            var newImutDic = imutDic.Add("key", "value");

        }

        public void InterateList()
        {
            //Parallel.ForEach();

        }

        public void RegexTest(string regex)
        {
            new Regex("")
        }
    }

}
