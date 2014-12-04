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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tumblr_Tool.Common_Helpers
{
    public class FileHelper
    {
        public static bool fileExists(string fullPath)
        {
            return File.Exists(fullPath);
        }

        public static string findFile(string dir, string name)
        {
            return Directory.GetFiles(@dir, name + ".*").FirstOrDefault();
        }

        public static string fixFileName(string filename)
        {
            if (!Path.HasExtension(filename))
            {
                filename += ".jpg";
            }

            return filename;
        }

        public static string fixURL(string url)
        {
            if (url.EndsWith("/"))
            {
                url = url.Remove(url.LastIndexOf("/"));
            }

            return url;
        }

        public static string getFullFilePath(string url, string location)
        {
            return @location + @"\" + Path.GetFileName(url);
        }

        public static string getFullFilePath(string url, string location, string prefix)
        {
            return @location + @"\" + prefix + Path.GetFileName(url);
        }

        public static List<string> getImageListFromDir(string location)
        {
            List<string> imagesList = new List<string>();
            string[] extensionArray = { ".jpg", ".jpeg", ".gif", ".png" };
            DirectoryInfo di = new DirectoryInfo(location);
            HashSet<string> allowedExtensions = new HashSet<string>(extensionArray, StringComparer.OrdinalIgnoreCase);
            FileInfo[] files = Array.FindAll(di.GetFiles(), f => allowedExtensions.Contains(f.Extension));

            imagesList = (from f in files select f.Name).ToList();

            //foreach (FileInfo f in files)
            //{
            //    imagesList.Add(f.Name);
            //}

            return imagesList;
        }

        public static bool isFileInUse(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public static SaveFile readTumblrFile(string location, string format)
        {
            switch (format)
            {
                case "BIN":
                    return readTumblrFileFromBin(location);

                case "XML":
                    return readTumblrFileFromXML(location);

                default:
                    return null;
            }
        }

        public static SaveFile readTumblrFileFromBin(string location)
        {
            try
            {
                using (Stream stream = File.Open(location, FileMode.Open))
                {
                    stream.Position = 0;
                    BinaryFormatter bformatter = new BinaryFormatter();

                    SaveFile saveFile = (SaveFile)bformatter.Deserialize(stream);
                    return saveFile;
                }
            }
            catch (Exception e)
            {
                string s = e.Message;
                return null;
            }
        }

        public static SaveFile readTumblrFileFromXML(string location)
        {
            return (SaveFile)XMLHelper.readObjectAsXML<SaveFile>(location);
        }

        public static bool saveFileAsBin(string location, SaveFile file)
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

        public static bool saveFileAsXML(string location, SaveFile file)
        {
            return XMLHelper.saveObjectAsXML<SaveFile>(location, file);
        }

        public static bool saveTumblrFile(string location, SaveFile file, string format)
        {
            switch (format)
            {
                case "BIN":
                    return saveFileAsBin(location, file);

                case "XML":
                    return saveFileAsXML(location, file);

                default:
                    return false;
            }
        }
    }
}