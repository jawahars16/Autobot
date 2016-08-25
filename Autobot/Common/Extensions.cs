using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public static string ToReadableFormat(this DateTime date)
        {
            return string.Format("{0:MMMM d, yyyy}", date);
        }

        public static string ToReadableFormat(this TimeSpan time)
        {
            return $"{(time.Hours > 12 ? time.Hours - 12 : time.Hours)}:{time.Minutes} {(time.Hours > 12 ? "PM" : "AM")}";
        }

        public static void AddProperty(this ExpandoObject expando, string property, object value)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(property))
                expandoDict[property.ToLower()] = value;
            else
                expandoDict.Add(property.ToLower(), value);
        }
    }
}
