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
        private const string _APIKEY = "SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY";
        private const string _APIURL = "https://api.tumblr.com/v2/blog/{0}/{1}?api_key={2}{3}{4}";
        private const string _AVATARQUERY = "avatar";
        private const string _AVATARSIZE = "128";

        private const string _INFOQUERY = "info";
        private const string _LIMIT = "&limit={0}";
        private const string _OFFSET = "&offset={0}";
        private const string _POSTQUERY = "posts";

        public static string GenerateQueryString(string tumblrDomain, string type, int start = 0, int maxNumPosts = 0)
        {
            string query;

            tumblrDomain = CommonHelper.FixURL(tumblrDomain);

            string postQuery = _POSTQUERY;
            if (type != TumblrPostTypes.empty.ToString())
            {
                postQuery += "/" + type;
            }

            query = string.Format(_APIURL, tumblrDomain, postQuery, _APIKEY, string.Format(_OFFSET, start.ToString()), string.Format(_LIMIT, maxNumPosts.ToString()));

            return query;
        }

        public static string GenerateInfoQueryString(string tumblrDomain)
        {
            string query;

            tumblrDomain = CommonHelper.FixURL(tumblrDomain);

            query = string.Format(_APIURL, tumblrDomain, _INFOQUERY, _APIKEY);

            return query;
        }

        public static string GetAvatarQueryString(string tumblrDomain)
        {
            tumblrDomain = CommonHelper.GetDomainName(tumblrDomain);
            string query;

            query = string.Format(_APIURL, tumblrDomain, _AVATARQUERY + "/" + _AVATARSIZE, _APIKEY, string.Empty, string.Empty);
            return query;
        }

        public static JObject GetObject(string url)
        {
            string result = GetString(url);

            if (result != null)
            {
                return JObject.Parse(result);
            }
            else
            {
                return null;
            }
        }

        public static string GetString(string url)
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

        public static T ReadObject<T>(string filePath) where T : new()
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

        public static bool SaveObject<T>(string filePath, T objectToWrite, bool append = false) where T : new()
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

                if (writer != null)
                    writer.Close();

                return true;
            }
            catch
            {
                if (writer != null)
                    writer.Close();
                return false;
            }
        }
    }
}