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
using System.IO;
using System.Net;
using System.Text;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Common_Helpers
{
    public static class JSONHelper
    {
        private static string apiKey = "SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY";
        private static string jsonAvatarQuery = "avatar";
        private static string jsonAvatarSize = "128";

        // private static string jsonBlogInfoQuery = "info";
        private static string jsonPostQuery = "posts";

        private static string jsonAPIURL = "https://api.tumblr.com/v2/blog/{0}/{1}?api_key={2}{3}{4}";

        private static string offsetString = "&offset={0}";
        private static string limitString = "&limit={0}";

        public static string getAvatarQueryString(string tumblrDomain)
        {
            tumblrDomain = CommonHelper.getDomainName(tumblrDomain);
            string query;
            // query += "/" + tumblrDomain + "/" + jsonAvatarQuery + "/" + jsonAvatarSize + "?api_key=" + apiKey;

            query = string.Format(jsonAPIURL, tumblrDomain, jsonAvatarQuery + "/" + jsonAvatarSize, apiKey,string.Empty,string.Empty);
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
            string query;


            tumblrDomain = CommonHelper.fixURL(tumblrDomain);

            string postQuery = jsonPostQuery;
            if (type != tumblrPostTypes.empty.ToString())
            {
                postQuery += "/" + type;
            }

            query = string.Format(jsonAPIURL, tumblrDomain, postQuery, apiKey, string.Format(offsetString,start.ToString()), string.Format(limitString,maxNumPosts.ToString()));

            return query;
        }

        public static bool saveObjectAsJSON<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);

                return true;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static T readFromJSON<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}