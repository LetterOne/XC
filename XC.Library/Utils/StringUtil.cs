using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XC.Library.Utils
{
    public class StringUtil
    {
        public static string SubstringBefore(string str, string separator)
        {
            int index = str.IndexOf(separator);
            string matchTypeStr = str.Substring(0, index);
            return matchTypeStr;
        }

        public static string SubstringAfter(string str, string separator)
        {
            int index = str.IndexOf(separator);
            string matchTypeStr = str.Substring(index+1);
            return matchTypeStr;
        }

    }
}