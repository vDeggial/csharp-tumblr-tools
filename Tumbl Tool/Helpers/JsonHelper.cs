/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: November, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.IO.Compression;

namespace Tumblr_Tool.Helpers
{
    /// <summary>
    /// Helper for Json related functionality
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Parses json string into dynamic object
        /// </summary>
        /// <param name="jsonString">Json string representation</param>
        /// <returns> Json object</returns>
        public static JObject GetDynamicObjectFromString(string jsonString)
        {
            if (!string.IsNullOrEmpty(jsonString))
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
                        using (FileStream fs = new FileStream(@filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (TextReader reader = new StreamReader(fs))
                            {
                                fs.Position = 0;
                                using (var jsonReader = new JsonTextReader(reader))
                                {
                                    jsonReader.CloseInput = false;
                                    var serializer = JsonSerializer.CreateDefault(
                                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include });

                                    return serializer.Deserialize<T>(jsonReader);
                                }
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
                        var fileContents = FileHelper.ReadFileAsString(@filePath);
                        return JsonConvert.DeserializeObject<T>(fileContents, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include });
                    }
                    catch
                    {
                        return default(T);
                    }
            }
            return default(T);
        }

        /// <summary>
        /// Read object from Json compressed file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="filePath">File path</param>
        /// <returns> Object of type T</returns>
        public static T ReadObjectFromFileCompressed<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return DeserializeCompressedFile<T>(fs,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include });
        }

        /// <summary>
        /// Saves object of type as json document
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="filePath">File location path</param>
        /// <param name="objectToWrite">Object to writeto file</param>
        /// <returns>True if save succeeds, false otherwise</returns>
        public static bool SaveObjectToFile<T>(string filePath, T objectToWrite)
        {
                    try
                    {
                        using (FileStream fs = new FileStream(@filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                        {
                            using (TextWriter writer = new StreamWriter(fs))
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
                    }
                    catch
                    {
                        return false;
                    }
        }

        /// <summary>
        /// Saves Object to json compressed file
        /// </summary>
        /// <param name="filePath"> File path</param>
        /// <param name="objectToWrite"> Object to save</param>
        /// <returns>True on success, false otherwise</returns>
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

        /// <summary>
        /// Deserialized compressed json file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="stream">File stream</param>
        /// <param name="settings">Deserializer settings</param>
        /// <returns>Object of type T</returns>
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

        /// <summary>
        /// Serializes object to compressed file
        /// </summary>
        /// <param name="value">Object to save</param>
        /// <param name="stream">File stream</param>
        /// <param name="settings">Compression settings</param>
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
