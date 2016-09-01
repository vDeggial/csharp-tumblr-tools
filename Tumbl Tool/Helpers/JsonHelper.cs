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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.IO.Compression;
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
            if (postType != TumblrPostType.All.ToString().ToLower())
            {
                postQuery += "/" + postType;
            }

            var query = string.Format(ApiUrl, tumblrDomain, postQuery, ApiKey, string.Format(Offset, offset.ToString()), string.Format(Limit, limit.ToString()));

            return query;
        }

        public static JObject GetObjectFromString(string jsonString)
        {
            if (jsonString != null)
            {
                return JObject.Parse(jsonString);
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
        public static T ReadObjectFromFile<T>(string filePath, int method = 1)
        {
            switch (method)
            {
                case 1:
                    try
                    {
                        using (TextReader reader = new StreamReader(filePath))
                        {
                            using (var fileContents = new JsonTextReader(reader))
                            {
                                var serializer = JsonSerializer.CreateDefault(
                                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include });

                                return serializer.Deserialize<T>(fileContents);
                            }
                        }
                    }
                    catch
                    {
                        return default(T);
                    }
                case 2:
                    try
                    {
                        var fileContents = FileHelper.ReadFileAsString(filePath);
                        return JsonConvert.DeserializeObject<T>(fileContents, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include });
                    }
                    catch
                    {
                        return default(T);
                    }
            }
            return default(T);
        }

        public static T ReadObjectFromFileCompressed<T>(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return DeserializeCompressedFile<T>(fs,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include });
        }

        /// <summary>
        /// Saves object of type as json document
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="filePath">File location path</param>
        /// <param name="objectToWrite">Object to writeto file</param>
        /// <param name="append">Append to file?</param>
        /// <returns>True if save succeeds, false otherwise</returns>
        public static bool SaveObjectToFile<T>(string filePath, T objectToWrite, int method = 1, bool append = false)
        {
            switch (method)
            {
                case 1:
                    try
                    {
                        using (TextWriter writer = new StreamWriter(filePath, append))
                        {
                            var serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                TypeNameHandling = TypeNameHandling.None,
                                DefaultValueHandling = DefaultValueHandling.Include,
                                Formatting = Formatting.Indented
                            });
                            serializer.Serialize(writer, objectToWrite);

                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                case 2:
                    try
                    {
                        using (TextWriter writer = new StreamWriter(filePath, append))
                        {
                            string contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented, new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                TypeNameHandling = TypeNameHandling.None,
                                DefaultValueHandling = DefaultValueHandling.Include
                            });
                            writer.Write(contentsToWriteToFile);

                            writer.Close();

                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
            }
            return false;
        }

        public static bool SaveObjectToFileCompressed(string filePath, object objectToWrite)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                SerializeCompressedFile(objectToWrite, fs, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None,
                    DefaultValueHandling = DefaultValueHandling.Include
                });
            return true;
        }

        private static T DeserializeCompressedFile<T>(Stream stream, JsonSerializerSettings settings = null)
        {
            using (var compressor = new GZipStream(stream, CompressionMode.Decompress))
            using (var reader = new StreamReader(compressor))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = JsonSerializer.CreateDefault(settings);
                return serializer.Deserialize<T>(jsonReader);
            }
        }

        private static void SerializeCompressedFile(object value, Stream stream, JsonSerializerSettings settings = null)
        {
            using (var compressor = new GZipStream(stream, CompressionMode.Compress))
            using (var writer = new StreamWriter(compressor))
            {
                var serializer = JsonSerializer.CreateDefault(settings);
                serializer.Serialize(writer, value);
            }
        }
    }
}