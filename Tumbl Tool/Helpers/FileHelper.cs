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
        private static SaveFileFormats SaveFileFormat = SaveFileFormats.Json;

        /// <summary>
        /// Add .jpg extension to file
        /// </summary>
        /// <param name="filename">File to add extension to</param>
        /// <returns>Filename with .jpg extension</returns>
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
        /// <param name="file">Filename</param>
        /// <returns>True if file exists, false otherwise</returns>
        public static bool FileExists(string file)
        {
            return File.Exists(file);
        }

        /// <summary>
        /// Find file
        /// </summary>
        /// <param name="dir">Directory</param>
        /// <param name="name">Filename</param>
        /// <returns></returns>
        public static string FindFile(string dir, string fileName)
        {
            return Directory.GetFiles(@dir, fileName + ".*").FirstOrDefault();
        }

        /// <summary>
        /// Generate full local file path from url and local directory path
        /// </summary>
        /// <param name="url">Remote Url Path</param>
        /// <param name="location">Local path</param>
        /// <returns></returns>
        public static string GenerateLocalPathToFile(string url, string location)
        {
            return @location + @"\" + Path.GetFileName(url);
        }

        /// <summary>
        /// Generate full local file path from url and local directory path with prefix
        /// </summary>
        /// <param name="url">Remote url</param>
        /// <param name="location">Local path</param>
        /// <param name="prefix">File prefix</param>
        /// <returns></returns>
        public static string GenerateLocalPathToFile(string url, string location, string prefix)
        {
            return @location + @"\" + prefix + Path.GetFileName(url);
        }

        /// <summary>
        /// Generates list of image files in folder
        /// </summary>
        /// <param name="location">Folder path</param>
        /// <returns>Hashset of image files in specified folder</returns>
        public static HashSet<string> GenerateFolderImageList(string location)
        {
            string[] extensionArray = { ".jpg", ".jpeg", ".gif", ".png" };
            DirectoryInfo di = new DirectoryInfo(location);
            HashSet<string> allowedExtensions = new HashSet<string>(extensionArray, StringComparer.OrdinalIgnoreCase);
            FileInfo[] files = Array.FindAll(di.GetFiles(), f => allowedExtensions.Contains(f.Extension));

            var imagesList = (from f in files select f.Name).ToHashSet();

            //foreach (FileInfo f in files)
            //{
            //    imagesList.Add(f.Name);
            //}

            return imagesList;
        }

        /// <summary>
        /// Determine if file is in use
        /// </summary>
        /// <param name="file">Filename</param>
        /// <returns>True if file is locked, false otherwise</returns>
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
                stream?.Close();
            }

            //file is not locked
            return false;
        }

        /// <summary>
        /// Read tumblr save file
        /// </summary>
        /// <param name="fileLocation">File path</param>
        /// <param name="format">Save file format</param>
        /// <returns></returns>
        public static SaveFile ReadTumblrFile(string fileLocation, SaveFileFormats format)
        {
            switch (format)
            {
                case SaveFileFormats.Bin:
                    return BinaryHelper.ReadObject<SaveFile>(fileLocation);

                case SaveFileFormats.Xml:
                    return XmlHelper.ReadObject<SaveFile>(fileLocation);

                case SaveFileFormats.Json:
                    return JsonHelper.ReadObject<SaveFile>(fileLocation);

                default:
                    return null;
            }
        }

        ///  <summary>
        /// Read tumblr save file
        ///  </summary>
        /// <param name="fileLocation">File path</param>
        /// <returns>Savefile object or null if cannot load</returns>
        public static SaveFile ReadTumblrFile(string fileLocation)
        {
            SaveFile saveFile = ReadTumblrFile(fileLocation, SaveFileFormats.Bin) ??
                                 ReadTumblrFile(fileLocation, SaveFileFormats.Xml) ??
                                ReadTumblrFile(fileLocation, SaveFileFormats.Json);

            return saveFile;
        }

        /// <summary>
        /// Saves the save file
        /// </summary>
        /// <param name="fileLocation">File path</param>
        /// <param name="saveFile">Savefile object</param>
        /// <returns>True if success saving, false otherwise</returns>
        public static bool SaveTumblrFile(string fileLocation, SaveFile saveFile)
        {
            return SaveTumblrFile(fileLocation, saveFile, SaveFileFormat);
        }

        /// <summary>
        /// Saves the save file
        /// </summary>
        /// <param name="fileLocation">File Path</param>
        /// <param name="saveFile">Savefile object</param>
        /// <param name="saveFileFormat">Savefile format</param>
        /// <returns>True if success saving, false otherwise</returns>
        public static bool SaveTumblrFile(string fileLocation, SaveFile saveFile, SaveFileFormats saveFileFormat)
        {
            saveFile.AddDate();
            switch (saveFileFormat)
            {
                case SaveFileFormats.Bin:
                    return BinaryHelper.SaveObject(fileLocation, saveFile);

                case SaveFileFormats.Xml:
                    return XmlHelper.SaveObject(fileLocation, saveFile);

                case SaveFileFormats.Json:
                    return JsonHelper.SaveObject(fileLocation, saveFile);

                default:
                    return false;
            }
        }
    }
}