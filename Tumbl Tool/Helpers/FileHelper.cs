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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Objects;

namespace Tumblr_Tool.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Save file format
        /// </summary>
        private static string saveFileFormat = saveFileFormats.JSON.ToString();

        /// <summary>
        /// Add .jpg extension to file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string AddJpgExt(string filename)
        {
            if (!Path.HasExtension(filename))
            {
                filename += ".jpg";
            }

            return filename;
        }

        /// <summary>
        /// Checks if file exists
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool FileExists(string file)
        {
            return File.Exists(file);
        }

        /// <summary>
        /// Find file
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FindFile(string dir, string name)
        {
            return Directory.GetFiles(@dir, name + ".*").FirstOrDefault();
        }

        /// <summary>
        /// Generate full local path from url and lovalc path
        /// </summary>
        /// <param name="url"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GenerateLocalPathToFile(string url, string location)
        {
            return @location + @"\" + Path.GetFileName(url);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="location"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GenerateLocalPathToFile(string url, string location, string prefix)
        {
            return @location + @"\" + prefix + Path.GetFileName(url);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static HashSet<string> GenerateFolderImageList(string location)
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="location"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static SaveFile ReadTumblrFile(string location, string format)
        {
            switch (format)
            {
                case "BIN":
                    return BinaryHelper.ReadObject<SaveFile>(location);

                case "XML":
                    return XmlHelper.ReadObject<SaveFile>(location);

                case "JSON":
                    return JsonHelper.ReadObject<SaveFile>(location);

                default:
                    return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static SaveFile ReadTumblrFile(string location)
        {
            SaveFile saveFile = ReadTumblrFile(location, saveFileFormats.BIN.ToString());

            if (saveFile == null)
                saveFile = ReadTumblrFile(location, saveFileFormats.XML.ToString());

            if (saveFile == null)
                saveFile = ReadTumblrFile(location, saveFileFormats.JSON.ToString());

            return saveFile;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static SaveFile ReadTumblrFileFromJSON(string location)
        {
            return JsonHelper.ReadObject<SaveFile>(location);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static SaveFile ReadTumblrFileFromXML(string location)
        {
            return XmlHelper.ReadObject<SaveFile>(location);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="location"></param>
        /// <param name="saveFile"></param>
        /// <returns></returns>
        public static bool SaveTumblrFile(string location, SaveFile saveFile)
        {
            return FileHelper.SaveTumblrFile(location, saveFile, saveFileFormat);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="location"></param>
        /// <param name="file"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool SaveTumblrFile(string location, SaveFile file, string format)
        {
            file.AddDate();
            switch (format)
            {
                case "BIN":
                    return BinaryHelper.SaveObject(location, file);

                case "XML":
                    return XmlHelper.SaveObject(location, file);

                case "JSON":
                    return JsonHelper.SaveObject(location, file);

                default:
                    return false;
            }
        }
    }
}