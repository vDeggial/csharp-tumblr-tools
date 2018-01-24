/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2018
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tumblr_Tool.Helpers
{
    /// <summary>
    /// Helper for Binary operations
    /// </summary>
    public static class BinaryHelper
    {
        /// <summary>
        /// Read in object from a file
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="filePath"> File location</param>
        /// <returns>Object of class T</returns>
        public static T ReadObjectFromFile<T>(string filePath) where T : new()
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    stream.Position = 0;

                    T saveFile = (T)new BinaryFormatter().Deserialize(stream);
                    return saveFile;
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Save Object to file
        /// </summary>
        /// <typeparam name="T"> Object type</typeparam>
        /// <param name="filePath">Save location</param>
        /// <param name="obj">Object</param>
        /// <returns>True if save was success, false otherwise</returns>
        public static bool SaveObjectToFile<T>(string filePath, T obj) where T : new()
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(stream, obj);
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