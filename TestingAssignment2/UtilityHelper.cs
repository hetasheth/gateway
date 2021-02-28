using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TestingAssignment2
{
    public static class UtilityHelper
    {
        /// <summary>
        /// Que 1 and 2
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string ConvertCase(this string inputString)
        {
            if (inputString.Length > 0)
            {
                char[] cArray = inputString.ToCharArray();
                for (int i = 0; i < inputString.Length; i++)
                {
                    cArray[i] = char.IsUpper(cArray[i]) ? char.ToLower(cArray[i]) : char.ToUpper(cArray[i]);
                }
                return new string(cArray);
            }
            return inputString;
        }

        /// <summary>
        /// Que 3
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string ConvertToTitleCase(this string inputString)
        {
            if (inputString.Length > 0)
            {
                string result = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(inputString.ToLower());
                return result;
            }
            return inputString;
        }

        /// <summary>
        /// Que 4
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static bool CheckLowerCase(this string inputString)
        {
            int c = 0;
            int l = inputString.Length;
            if (inputString.Length > 0)
            {
                char[] cArray = inputString.ToCharArray();
                for (int i = 0; i < inputString.Length; i++)
                {
                    if (char.IsLower(cArray[i]) || char.IsWhiteSpace(cArray[i]))
                    {
                        c++;
                    }
                }
                if (c == l)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Que 5
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string ConvertToCapitalCase(this string inputString)
        {
            if (inputString.Length > 0)
            {
                char[] cArray = inputString.ToCharArray();
                cArray[0] = char.ToUpper(cArray[0]);
                for (int i = 1; i < inputString.Length; i++)
                {
                    cArray[i] = char.ToLower(cArray[i]);
                }
                return new string(cArray);
            }
            return inputString;
        }

        /// <summary>
        /// Que 6
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static bool CheckUpperCase(this string inputString)
        {
            int c = 0;
            int l = inputString.Length;
            if (inputString.Length > 0)
            {
                char[] cArray = inputString.ToCharArray();
                for (int i = 0; i < inputString.Length; i++)
                {
                    if (char.IsUpper(cArray[i]) || char.IsWhiteSpace(cArray[i]))
                    {
                        c++;
                    }
                }
                if (c == l)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Que 7
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static bool IsValidNumeric(this string inputString)
        {
            if (inputString.Length > 0)
            {
                int no;
                return int.TryParse(inputString, out no);
            }
            return false;
        }


        /// <summary>
        /// Que 8
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveLastCharacter(this string inputString)
        {
            if (inputString.Length > 0)
            {
                return inputString.Substring(0, inputString.Length - 1);
            }
            return inputString;
        }

        /// <summary>
        /// Que 9
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static int WordCount(this string inputString)
        {
            if (inputString.Length > 0)
            {
                string[] words = inputString.Split(' ');
                return words.Length;
            }
            return 0;
        }

        public static int ConvertToInt(this string inputString)
        {
            int x = 0;
            if (inputString.Length > 0)
            {
                Int32.TryParse(inputString, out x);
            }
            return x;
        }
    }
}
