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
        private static SaveFileFormat _saveFileFormat = SaveFileFormat.Json;

        /// <summary>
        /// Add .jpg extension to file
        /// </summary>
        /// <param name="filename">File to add extension to</param>
        /// <returns>Filename with .jpg extension</returns>
        public static string AddJpgExt(string filename)
        {
            try
            {
                if (!Path.HasExtension(filename))
                {
                    filename += ".jpg";
                }

                return filename;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if file exists
        /// </summary>
        /// <param name="file">Filename</param>
        /// <returns>True if file exists, false otherwise</returns>
        public static bool FileExistsOnHdd(string file)
        {
            try
            {
                return File.Exists(file);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Find file
        /// </summary>
        /// <param name="dir">Directory</param>
        /// <param name="fileName">Filename</param>
        /// <returns></returns>
        public static string FindFile(string dir, string fileName)
        {
            try
            {
                return Directory.GetFiles(dir, fileName + ".*").FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Generate full local file path from url and local directory path with prefix
        /// </summary>
        /// <param name="url">Remote url</param>
        /// <param name="location">Local path</param>
        /// <param name="prefix">File prefix</param>
        /// <returns></returns>
        public static string GenerateLocalPathToFile(string url, string location, string prefix = "")
        {
            try
            {
                return location + @"\" + prefix + Path.GetFileName(url);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Generates list of image files in folder
        /// </summary>
        /// <param name="location">Folder path</param>
        /// <returns>Hashset of image files in specified folder</returns>
        public static HashSet<string> GenerateFolderImageList(string location)
        {
            try
            {


                string[] extensionArray = { ".jpg", ".jpeg", ".gif", ".png" };
                DirectoryInfo di = new DirectoryInfo(location);
                HashSet<string> allowedExtensions = new HashSet<string>(extensionArray, StringComparer.OrdinalIgnoreCase);
                FileInfo[] files = Array.FindAll(di.GetFiles(), f => allowedExtensions.Contains(f.Extension));

                var imagesList = (from f in files select f.Name).ToHashSet();


                return imagesList;
            }
            catch
            {
                return null;
            }
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
        public static SaveFile ReadTumblrFile(string fileLocation, SaveFileFormat format)
        {
            try
            {
                switch (format)
                {
                    case SaveFileFormat.Bin:
                        return BinaryHelper.ReadObject<SaveFile>(fileLocation);

                    case SaveFileFormat.Xml:
                        return XmlHelper.ReadObject<SaveFile>(fileLocation);

                    case SaveFileFormat.Json:
                        return JsonHelper.ReadObject<SaveFile>(fileLocation);

                    default:
                        return null;
                }
            }
            catch
            {
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
            try
            {
                SaveFile saveFile = ReadTumblrFile(fileLocation, SaveFileFormat.Bin) ??
                                     ReadTumblrFile(fileLocation, SaveFileFormat.Xml) ??
                                    ReadTumblrFile(fileLocation, SaveFileFormat.Json);

                return saveFile;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Saves the save file
        /// </summary>
        /// <param name="fileLocation">File path</param>
        /// <param name="saveFile">Savefile object</param>
        /// <returns>True if success saving, false otherwise</returns>
        public static bool SaveTumblrFile(string fileLocation, SaveFile saveFile)
        {
            try
            {
                return SaveTumblrFile(fileLocation, saveFile, _saveFileFormat);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Saves the save file
        /// </summary>
        /// <param name="fileLocation">File Path</param>
        /// <param name="saveFile">Savefile object</param>
        /// <param name="saveFileFormat">Savefile format</param>
        /// <returns>True if success saving, false otherwise</returns>
        public static bool SaveTumblrFile(string fileLocation, SaveFile saveFile, SaveFileFormat saveFileFormat)
        {
            try
            {
                saveFile.AddDate();
                switch (saveFileFormat)
                {
                    case SaveFileFormat.Bin:
                        return BinaryHelper.SaveObject(fileLocation, saveFile);

                    case SaveFileFormat.Xml:
                        return XmlHelper.SaveObject(fileLocation, saveFile);

                    case SaveFileFormat.Json:
                        return JsonHelper.SaveObject(fileLocation, saveFile);

                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the file name exosts in the directory listing Hashset already
        /// </summary>
        /// <param name="sourceSet"></param>
        /// <param name="fileName"></param>
        /// <param name="useFullString"></param>
        /// <param name="cutOffChar"></param>
        /// <returns></returns>
        public static bool IsExistingFile(HashSet<string> sourceSet, string fileName, bool useFullString = false, char cutOffChar = '_')
        {
            try
            {
                HashSet<string> list;
                if (!useFullString)
                {
                    list = (from p in sourceSet
                            where p.ToLower().Contains(fileName.Substring(0, fileName.LastIndexOf(cutOffChar)).ToLower())
                            select p).ToHashSet();
                }
                else
                {
                    list = (from p in sourceSet
                            where p.ToLower().Contains(fileName.ToLower())
                            select p).ToHashSet();
                }
                return Convert.ToBoolean(list.Count);
            }
            catch
            {
                return false;
            }
        }
            

    }
}