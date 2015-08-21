/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: August, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tumblr_Tool.Helpers
{
    public static class BinaryHelper
    {
        public static T ReadObject<T>(string location) where T : new()
        {
            try
            {
                using (Stream stream = File.Open(location, FileMode.Open))
                {
                    stream.Position = 0;
                    BinaryFormatter bformatter = new BinaryFormatter();

                    T saveFile = (T)bformatter.Deserialize(stream);
                    return saveFile;
                }
            }
            catch (Exception e)
            {
                string s = e.Message;
                return default(T);
            }
        }

        public static bool SaveObject<T>(string location, T file) where T : new()
        {
            try
            {
                using (Stream stream = File.Open(location, FileMode.Create))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    bformatter.Serialize(stream, file);
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