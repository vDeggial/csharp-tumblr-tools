/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2016
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.IO;
using System.Xml.Serialization;

namespace Tumblr_Tool.Helpers
{
    public static class XmlHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T ReadObject<T>(string filename) where T : new()
        {
            try
            {
                XmlSerializer reader =
            new XmlSerializer(typeof(T));
                StreamReader file = new StreamReader(
                    @filename);
                var obj = (T)reader.Deserialize(file.BaseStream);
                return obj;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool SaveObject<T>(string filename, T obj) where T : new()
        {
            try
            {
                using (StreamWriter myWriter = new StreamWriter(filename, false))
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(T));
                    mySerializer.Serialize(myWriter.BaseStream, obj);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}