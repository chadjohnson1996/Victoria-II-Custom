using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// casts a string as an int
        /// </summary>
        /// <param name="s">the string to cast</param>
        /// <returns>the string as an int</returns>
        public static int AsInt(this string s)
        {
            return int.Parse(s);
        }

        /// <summary>
        /// casts a string as a decimal
        /// </summary>
        /// <param name="s">the string to cast</param>
        /// <returns>the string cast as a decimal</returns>
        public static decimal AsDecimal(this string s)
        {
            return decimal.Parse(s);
        }

        /// <summary>
        /// casts a string as a bool
        /// </summary>
        /// <param name="s">the string to cast as a bool</param>
        /// <returns>the string as a bool</returns>
        public static bool AsBool(this string s)
        {
            switch (s)
            {
                case "no":
                    return false;
                case "yes":
                    return true;
                default:
                    return bool.Parse(s);
            }
        }
    }
}
