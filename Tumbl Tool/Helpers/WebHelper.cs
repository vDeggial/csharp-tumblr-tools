/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Helpers
{
    public static class WebHelper
    {
        private const string UnreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~.-_";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static bool CheckForInternetConnection()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        public static string DecodeUrl(string value)
        {
            if (value == null)
                return String.Empty;

            char[] chars = value.ToCharArray();

            List<byte> buffer = new List<byte>(chars.Length);
            for (int i = 0; i < chars.Length; i++)
            {
                if (value[i] == '%')
                {
                    byte decodedChar = (byte)Convert.ToInt32(new string(chars, i + 1, 2), 16);
                    buffer.Add(decodedChar);

                    i += 2;
                }
                else
                {
                    buffer.Add((byte)value[i]);
                }
            }

            return System.Text.Encoding.UTF8.GetString(buffer.ToArray(), 0, buffer.Count);
        }

        public static string EncodeUrl(string value)
        {
            if (value == null)
                return String.Empty;

            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (byte b in bytes)
            {
                char c = (char)b;
                if (UnreservedChars.IndexOf(c) >= 0)
                    sb.Append(c);
                else
                    sb.Append($"%{b:X2}");
            }

            return sb.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetDomainName(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttp ? uriResult.Host : null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetRemoteDocumentAsString(string url)
        {
            try
            {
                Uri uri = new Uri(url);

                var docStr = new RestClient(string.Concat(uri.Scheme + Uri.SchemeDelimiter, uri.Host))
                    .Execute(new RestRequest(uri.PathAndQuery, Method.GET)).Content;

                return !string.IsNullOrEmpty(docStr) ? docStr : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsValidUrl(this string source)
        {
            return Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) && Uri.CheckSchemeName(uriResult.Scheme) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string RemoveTrailingBackslash(string url)
        {
            try
            {
                return url.EndsWith("/") ? url.Remove(url.Length - 1) : url;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string StripHtmlTags(string html)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));
            // Removes tags from passed HTML

            return new Regex("<[^>]*>").Replace(html, "");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static bool TumblrExists(this string url)
        {
            try
            {
                dynamic jsonObject = JsonHelper.GetDynamicObjectFromString(GetRemoteDocumentAsString(url));
                return (jsonObject != null && jsonObject.meta != null && jsonObject.meta.status == ((int)TumblrApiResponse.Ok).ToString());
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool UrlExists(string url)
        {
            try
            {
                Uri uri = new Uri(url);

                return (new RestClient(string.Concat(uri.Scheme + Uri.SchemeDelimiter, uri.Host))
                    .Execute(new RestRequest(uri.PathAndQuery, Method.HEAD)).StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }
    }
}