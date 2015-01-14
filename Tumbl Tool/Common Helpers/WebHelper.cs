/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Common_Helpers
{
    public static class WebHelper
    {
        public static bool CheckForInternetConnection()
        {
            return new Ping().Send("www.google.com").Status == IPStatus.Success;
        }

        public static string FixURL(string url)
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

        public static string GetDocumentAsString(string url)
        {
            try
            {
                string docStr;

                using (var wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    docStr = wc.DownloadString(url);
                }

                return docStr;
            }
            catch
            {
                return null;
            }
        }

        public static string GetDomainName(string url)
        {
            return new Uri(url) != null ? new Uri(url).Host : null;
        }

        public static bool IsValidTumblr(this string url, string mode)
        {
            try
            {
                if (mode == ApiModeEnum.v2JSON.ToString())
                {
                    dynamic jsonObject = JSONHelper.GetObject(url);
                    return (jsonObject != null && jsonObject.meta != null && jsonObject.meta.status == ((int)TumblrAPIResponseEnum.OK).ToString());
                }
                else
                    return XMLHelper.GetDocument(url) != null;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidUrl(this string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }

        public static string StripHTMLTags(string HTML)
        {
            // Removes tags from passed HTML
            System.Text.RegularExpressions.Regex objRegEx = new System.Text.RegularExpressions.Regex("<[^>]*>");

            return objRegEx.Replace(HTML, "");
        }

        public static bool UrlExists(string url)
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead(url))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}