using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UCHET.Classes.Common
{
    public class CheckRegex
    { 
        public static bool Match(string Pattern, string Input)
        {
            Match m = Regex.Match(Input, Pattern);
            return m.Success;
        }
    }
}
