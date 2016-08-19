using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Common
{
    public static class Extensions
    {
        public static string RemoveFromEnd(this string s, string suffix)
        {
            if (s.EndsWith(suffix))
            {
                return s.Substring(0, s.Length - suffix.Length);
            }
            else
            {
                return s;
            }
        }

        public static void PrintStackTrace(this Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
        }
    }
}
