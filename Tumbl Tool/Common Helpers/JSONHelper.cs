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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Common_Helpers
{
    public static class JSONHelper
    {
        private static string apiKey = "SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY";
        private static string jsonAvatarQuery = "avatar";
        private static string jsonAvatarSize = "128";
        private static string jsonBlogInfoQuery = "info";
        private static string jsonPostQuery = "posts";
        private static string jsonURL = "http://api.tumblr.com/v2/blog";

        public static string getAvatarQueryString(string tumblrDomain)
        {
            tumblrDomain = CommonHelper.getDomainName(tumblrDomain);
            string query = string.Copy(jsonURL);
            query += "/" + tumblrDomain + "/" + jsonAvatarQuery + "/" + jsonAvatarSize + "?api_key=" + apiKey;
            return query;
        }

        public static JObject getJSONObject(string url)
        {
            string result = getJSONString(url);

            if (result != null)
            {
                return JObject.Parse(result);
            }
            else
            {
                return null;
            }
        }

        public static string getJSONString(string url)
        {
            try
            {
                string jsonStr;

                using (var wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    jsonStr = wc.DownloadString(url);
                }

                return jsonStr;
            }
            catch
            {
                return null;
            }
        }

        public static string getQueryString(string tumblrDomain, string type, int start = 0, int maxNumPosts = 0)
        {
            string query = string.Copy(jsonURL);

            query += "/" + CommonHelper.fixURL(tumblrDomain) + "/" + jsonPostQuery;

            if (type != tumblrPostTypes.empty.ToString())
            {
                query += "/" + type;
            }

            query += "?api_key=" + apiKey;

            if (start != 0)
            {
                query += "&offset=" + start.ToString();
            }

            if (maxNumPosts != 0)
            {
                query += "&limit=" + maxNumPosts.ToString();
            }

            return query;
        }

        public static XDocument jsonToXML(string json)
        {
            return JsonConvert.DeserializeXNode(json, "tumblr");
        }
    }
}