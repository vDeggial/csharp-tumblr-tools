/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: September, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Helpers
{
    /// <summary>
    /// Helper for Json related functionality
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Tumblr API key
        /// </summary>
        private const string ApiKey = "SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY";

        /// <summary>
        /// Tumblr API url scheme
        /// </summary>
        private const string ApiUrl = "https://api.tumblr.com/v2/blog/{0}/{1}?api_key={2}{3}{4}";

        private const string AvatarQuery = "avatar";
        private const string AvatarSize = "128";

        private const string InfoQuery = "info";
        private const string Limit = "&limit={0}";
        private const string Offset = "&offset={0}";
        private const string PostQuery = "posts";

        /// <summary>
        ///
        /// </summary>
        /// <param name="tumblrDomain"></param>
        /// <returns></returns>
        public static string GenerateInfoQueryString(string tumblrDomain)
        {
            tumblrDomain = WebHelper.RemoveTrailingBackslash(tumblrDomain);

            var query = string.Format(ApiUrl, tumblrDomain, InfoQuery, ApiKey, null, null);

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tumblrDomain"></param>
        /// <param name="type"></param>
        /// <param name="start"></param>
        /// <param name="maxNumPosts"></param>
        /// <returns></returns>
        public static string GenerateQueryString(string tumblrDomain, string type, int start = 0, int maxNumPosts = 0)
        {
            tumblrDomain = WebHelper.RemoveTrailingBackslash(tumblrDomain);

            string postQuery = PostQuery;
            if (type != TumblrPostTypes.All.ToString().ToLower())
            {
                postQuery += "/" + type;
            }

            var query = string.Format(ApiUrl, tumblrDomain, postQuery, ApiKey, string.Format(Offset, start.ToString()), string.Format(Limit, maxNumPosts.ToString()));

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tumblrDomain"></param>
        /// <returns></returns>
        public static string GetAvatarQueryString(string tumblrDomain)
        {
            tumblrDomain = WebHelper.GetDomainName(tumblrDomain);

            var query = string.Format(ApiUrl, tumblrDomain, AvatarQuery + "/" + AvatarSize, ApiKey, string.Empty, string.Empty);
            return query;
        }

        public static JObject GetObject(string url)
        {
            string result = WebHelper.GetRemoteDocumentAsString(url);

            if (result != null)
            {
                return JObject.Parse(result);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T ReadObject<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include });
            }
            catch
            {
                return default(T);
            }
            finally
            {
                reader?.Close();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="objectToWrite"></param>
        /// <param name="append"></param>
        /// <returns></returns>
        public static bool SaveObject<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None,
                    DefaultValueHandling = DefaultValueHandling.Include
                });
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);

                writer.Close();

                return true;
            }
            catch
            {
                writer?.Close();
                return false;
            }
        }
    }
}