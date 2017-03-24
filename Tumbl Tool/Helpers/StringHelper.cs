using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tumblr_Tool.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Get array of bytes from string
        /// </summary>
        /// <param name="str">String text</param>
        /// <returns>Array of bytes representing original string</returns>
        public static byte[] GetBytes(string str)
        {
            try
            {
                byte[] bytes = new byte[str.Length * sizeof(char)];
                Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert array of bytes to string
        /// </summary>
        /// <param name="bytes">Array of bytes</param>
        /// <returns>String representation of array of bytes</returns>
        public static string GetString(byte[] bytes)
        {
            try
            {
                char[] chars = new char[bytes.Length / sizeof(char)];
                Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
                return new string(chars);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Replace part of the string with other
        /// </summary>
        /// <param name="input">Original string</param>
        /// <param name="strToReplace"> String to replace</param>
        /// <param name="strWith">String to replace with</param>
        /// <returns>String with replaced value</returns>
        public static string ReplaceInString(string input, string strToReplace, string strWith)
        {
            try
            {
                return input?.Replace(strToReplace, strWith);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Replace part of string with "\r\n"
        /// </summary>
        /// <param name="input">Original string</param>
        /// <param name="strToReplace">String to replace</param>
        /// <returns>String with replaced newline break chars</returns>
        public static string ReplaceInStringToNewline(string input, string strToReplace)
        {
            try
            {
                return input?.Replace(strToReplace, "\n\r");
            }
            catch
            {
                return null;
            }
        }

        public static bool StringExistsInHash(HashSet<string> sourceSet, string fileName, bool useFullString = false, char cutOffChar = '_')
        {
            try
            {
                switch (useFullString)
                {
                    case false:
                        return Convert.ToBoolean((from p in sourceSet
                                                  where p.ToLower().Contains(fileName.Substring(0, fileName.LastIndexOf(cutOffChar)).ToLower())
                                                  select p).Count());

                    case true:
                        return Convert.ToBoolean((from p in sourceSet
                                                  where p.ToLower().Contains(fileName.ToLower())
                                                  select p).Count());
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Generate slug from a string
        /// </summary>
        /// <param name="text">Text to generate slug from</param>
        /// <returns></returns>
        public static string ToSlug(this string text)
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                foreach (char c in text)
                    if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                        builder.Append(c);

                byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);

                var value = Regex.Replace(Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", "_");

                return value.ToLowerInvariant();
            }
            catch
            {
                return null;
            }
        }
    }
}