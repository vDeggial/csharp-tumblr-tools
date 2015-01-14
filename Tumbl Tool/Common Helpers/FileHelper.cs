/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tumblr_Tool.Common_Helpers
{
    public static class FileHelper
    {
        private static string saveFileFormat = "JSON";

        public static bool FileExists(string file)
        {
            return File.Exists(file);
        }

        public static string FindFile(string dir, string name)
        {
            return Directory.GetFiles(@dir, name + ".*").FirstOrDefault();
        }

        public static string FixFileName(string filename)
        {
            if (!Path.HasExtension(filename))
            {
                filename += ".jpg";
            }

            return filename;
        }

        public static string GetFullFilePath(string url, string location)
        {
            return @location + @"\" + Path.GetFileName(url);
        }

        public static string GetFullFilePath(string url, string location, string prefix)
        {
            return @location + @"\" + prefix + Path.GetFileName(url);
        }

        public static HashSet<string> GetImageListFromDir(string location)
        {
            HashSet<string> imagesList = new HashSet<string>();
            string[] extensionArray = { ".jpg", ".jpeg", ".gif", ".png" };
            DirectoryInfo di = new DirectoryInfo(location);
            HashSet<string> allowedExtensions = new HashSet<string>(extensionArray, StringComparer.OrdinalIgnoreCase);
            FileInfo[] files = Array.FindAll(di.GetFiles(), f => allowedExtensions.Contains(f.Extension));

            imagesList = (from f in files select f.Name).ToHashSet();

            //foreach (FileInfo f in files)
            //{
            //    imagesList.Add(f.Name);
            //}

            return imagesList;
        }

        public static bool IsFileLocked(FileInfo file)
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

        public static SaveFile ReadTumblrFile(string location, string format)
        {
            switch (format)
            {
                case "BIN":
                    return ReadTumblrFileFromBin(location);

                case "XML":
                    return (SaveFile)XMLHelper.ReadObject<SaveFile>(location);

                case "JSON":
                    return JSONHelper.ReadObject<SaveFile>(location);

                default:
                    return null;
            }
        }

        public static SaveFile ReadTumblrFileFromBin(string location)
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

        public static SaveFile ReadTumblrFile(string location)
        {
            SaveFile saveFile = ReadTumblrFile(location, "BIN");

            if (saveFile == null)
                saveFile = ReadTumblrFile(location, "XML");

            if (saveFile == null)
                saveFile = ReadTumblrFile(location, "JSON");

            return saveFile;
        }

        public static bool SaveTumblrFile(string location, SaveFile saveFile)
        {
            return FileHelper.SaveTumblrFile(location, saveFile, saveFileFormat);
        }

        public static SaveFile ReadTumblrFileFromJSON(string location)
        {
            return (SaveFile)JSONHelper.ReadObject<SaveFile>(location);
        }

        public static SaveFile ReadTumblrFileFromXML(string location)
        {
            return (SaveFile)XMLHelper.ReadObject<SaveFile>(location);
        }

        public static bool SaveFileAsBin(string location, SaveFile file)
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

        public static bool SaveTumblrFile(string location, SaveFile file, string format)
        {
            file.AddDate();
            switch (format)
            {
                case "BIN":
                    return SaveFileAsBin(location, file);

                case "XML":
                    return XMLHelper.SaveObject<SaveFile>(location, file);

                case "JSON":
                    return JSONHelper.SaveObject<SaveFile>(location, file);

                default:
                    return false;
            }
        }
    }
}