/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: December, 2014
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tumblr_Tool.Common_Helpers
{
    public static class CommonHelper
    {
        private static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string fixURL(string url)
        {
            try
            {
                if (url.EndsWith("/"))
                {
                    url = url.Remove(url.Length - 1);
                }

                return url;
            }

            catch
            {
                return null;
            }
        }

        public static string getDomainName(string url)
        {
            return new Uri(url) != null ? new Uri(url).Host : null;
        }

        public static string NewLineToBreak(string input, string strToReplace)
        {
            if (input != null)
                return input.Replace(strToReplace, "\n\r");
            else
                return input;
        }

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTags(string source)
        {
            if (source != null)
                return _htmlRegex.Replace(source, string.Empty);
            else
                return source;
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}