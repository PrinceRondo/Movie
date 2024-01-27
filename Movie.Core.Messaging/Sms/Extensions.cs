using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Movie.Core.Messaging.Sms
{
    public static class Extensions
    {
        public static string ArrayToCommaSeparatedString(this string[] array)
        {
            var newArray = new string[array.Length];
            var i = 0;

            foreach (var s in array)
            {
                newArray.SetValue(s.SeperateWords(), i);
                i++;
            }
            return string.Join(",", newArray);
        }

        public static string SeperateWords(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            string output = "";
            char[] chars = str.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == chars.Length - 1 || i == 0 || Char.IsWhiteSpace(chars[i]))
                {
                    output += chars[i];
                    continue;
                }

                if (char.IsUpper(chars[i]) && Char.IsLower(chars[i - 1]))
                    output += " " + chars[i];
                else
                    output += chars[i];
            }

            return output;
        }

        public static string UrlEncode(this string src)
        {
            if (src == null)
                return null;
            return HttpUtility.UrlEncode(src);
        }
        /// <summary>
        /// Converts an sms number to international format. 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="usePlus"></param>
        /// <returns></returns>
        //public static string NigerianMobile(this string str, bool usePlus = false)
        //{
        //    const string naijaPrefix = "234";
        //    if (string.IsNullOrEmpty(str) || str.Length <= 10)
        //        return str;


        //    str = str.TrimStart('+');

        //    var prefix = str.Remove(3);

        //    if (prefix.Equals(naijaPrefix))
        //    {
        //        return str;
        //    }
        //    str = str.TrimStart('0');
        //    str = naijaPrefix + str;

        //    if (usePlus)
        //    {
        //        str = str.StartsWith("+") ? str : $"+{str}";
        //    }

        //    return str;
        //}

        /// <summary>
        /// Converts an sms number to international format. 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="usePlus"></param>
        /// <returns></returns>
        public static string NigerianMobile(this string str, bool usePlus = false)
        {

            string usePrefix = "234";

            if (string.IsNullOrEmpty(usePrefix))
                return str;


            if (string.IsNullOrEmpty(str))
                return str;

            usePrefix = usePrefix.Trim();

            str = str.Trim();
            str = str.TrimStart('+');
            str = str.TrimStart('0');

            if (str.Length < 11)
            {
                // has no international code yet.
                if (str.Length == 9)
                {
                    str = "233" + str;
                }
                else
                {
                    str = usePrefix + str;
                }
            }




            if (str.Substring(3, 1) == "0")
            {
                str = str.Substring(0, 3) + str.Substring(4);
            }

            if (usePlus == true)
            {
                str = '+' + str;
            }

            return str;
        }

    }
}
