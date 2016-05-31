/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: April, 2016
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

        /// <summary>
        /// Tumblr blog avatar query string
        /// </summary>
        private const string AvatarQuery = "avatar";

        /// <summary>
        /// Tumblr blog avatar size
        /// </summary>
        private const string AvatarSize = "128";

        /// <summary>
        /// Tumblr blog info query string
        /// </summary>
        private const string InfoQuery = "info";

        /// <summary>
        /// Tumblr API response posts limit
        /// </summary>
        private const string Limit = "&limit={0}";

        /// <summary>
        /// Tumblr API posts offset (start)
        /// </summary>
        private const string Offset = "&offset={0}";

        /// <summary>
        /// Tumblr blog posts query string
        /// </summary>
        private const string PostQuery = "posts";

        /// <summary>
        /// Generate Tumblr blog avatar query string
        /// </summary>
        /// <param name="tumblrDomain">Tumblr Url</param>
        /// <param name="avatarSize">Avatar image size</param>
        /// <returns>Tumblr API url for blog avatar</returns>
        public static string GenerateAvatarQueryString(string tumblrDomain, string avatarSize = AvatarSize)
        {
            tumblrDomain = WebHelper.GetDomainName(tumblrDomain);

            var query = string.Format(ApiUrl, tumblrDomain, AvatarQuery + "/" + avatarSize, ApiKey, string.Empty, string.Empty);
            return query;
        }

        /// <summary>
        /// Generates blog info query string
        /// </summary>
        /// <param name="tumblrDomain">Tumblr blog domain</param>
        /// <returns>Full API blog info query string</returns>
        public static string GenerateInfoQueryString(string tumblrDomain)
        {
            tumblrDomain = WebHelper.RemoveTrailingBackslash(tumblrDomain);

            var query = string.Format(ApiUrl, tumblrDomain, InfoQuery, ApiKey, null, null);

            return query;
        }

        /// <summary>
        /// Generate Tumblr API query string for posts
        /// </summary>
        /// <param name="tumblrDomain">Tumblr domain</param>
        /// <param name="postType">Tumblr post type</param>
        /// <param name="offset">Tumblr posts offset</param>
        /// <param name="limit">Tumblr post limit per document</param>
        /// <returns>Tumblr API query string for blog posts</returns>
        public static string GeneratePostQueryString(string tumblrDomain, string postType, int offset = 0, int limit = (int)NumberOfPostsPerApiDocument.ApiV2)
        {
            tumblrDomain = WebHelper.RemoveTrailingBackslash(tumblrDomain);

            string postQuery = PostQuery;
            if (postType != TumblrPostTypes.All.ToString().ToLower())
            {
                postQuery += "/" + postType;
            }

            var query = string.Format(ApiUrl, tumblrDomain, postQuery, ApiKey, string.Format(Offset, offset.ToString()), string.Format(Limit, limit.ToString()));

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
        /// Reads in json object as specified class
        /// </summary>
        /// <typeparam name="T"> Object class</typeparam>
        /// <param name="filePath">File location path</param>
        /// <returns>Object of type T read in from file</returns>
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
        /// Saves object of type as json document
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="filePath">File location path</param>
        /// <param name="objectToWrite">Object to writeto file</param>
        /// <param name="append">Append to file?</param>
        /// <returns>True if save succeeds, false otherwise</returns>
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