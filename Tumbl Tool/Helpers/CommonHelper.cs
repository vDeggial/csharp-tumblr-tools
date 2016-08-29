/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: August, 2016
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tumblr_Tool.Helpers
{
    /// <summary>
    /// Common helper
    /// </summary>
    public static class CommonHelper
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
        public static HashSet<T> ReverseHashSet<T>(this HashSet<T> set)
        {
            try
            {
                return set.Reverse().ToHashSet();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert to Hashset
        /// </summary>
        /// <typeparam name="T">Type for hashset items</typeparam>
        /// <param name="obj">Object to convert to hashset</param>
        /// <returns>Hasheset of objects of the type T</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> obj)
        {
            try
            {
                return new HashSet<T>(obj);
            }
            catch
            {
                return null;
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

        /// <summary>
        /// Convert Unix timestamp to date/time
        /// </summary>
        /// <param name="unixTimeStamp">Unix timestamp</param>
        /// <returns>DateTime representation of Unix timestamp</returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            try
            {
                // Unix timestamp is seconds past epoch
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}