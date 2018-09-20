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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Objects;

namespace Tumblr_Tool.Helpers
{
    /// <summary>
    /// Local File Operations Helper 
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Save file format
        /// </summary>
        private static SaveFileFormat _saveFileFormat = SaveFileFormat.Json;

        /// <summary>
        /// Add file extension to filename
        /// </summary>
        /// <param name="filename">File to add extension to</param>
        /// <param name="extension">File Extension</param>
        /// <returns>Filename with extension</returns>
        public static string AddFileExtension(string filename, string extension)
        {
            try
            {
                return (!string.IsNullOrEmpty(filename) && !Path.HasExtension(filename)) ? new StringBuilder(filename).Append(".").Append(extension).ToString() : filename;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  Check if file exists
        /// </summary>
        /// <param name="path">Local path</param>
        /// <param name="fileName">File name</param>
        /// <param name="useFullString">True if checking full file name, false if partial</param>
        /// <param name="cutOffChar">Cutoff char if partial match</param>
        /// <returns>True if file was found, false otherwise</returns>
        public static bool FileExists(string path, string fileName, bool useFullString = false, char cutOffChar = '_')
        {
            try
            {
                switch (useFullString)
                {
                    case false:
                        return Directory.EnumerateFiles(path, fileName.Substring(0, fileName.LastIndexOf(cutOffChar)).ToLower() + "*").Any();

                    case true:
                        return Directory.EnumerateFiles(path, fileName).Any();
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if file exists
        /// </summary>
        /// <param name="filePath">Filename</param>
        /// <returns>True if file exists, false otherwise</returns>
        public static bool FileExists(string filePath)
        {
            try
            {
                return !string.IsNullOrEmpty(filePath) && FileExists(Path.GetDirectoryName(filePath), Path.GetFileName(filePath), true);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Generates list of image files in folder
        /// </summary>
        /// <param name="folderPath">Folder path</param>
        /// <returns>Hashset of image files in specified folder</returns>
        public static HashSet<string> GenerateFolderImageList(string folderPath)
        {
            try
            {
                string[] extensionArray = { ".jpg", ".jpeg", ".gif", ".png" };
                DirectoryInfo di = new DirectoryInfo(folderPath);
                HashSet<string> allowedExtensions = new HashSet<string>(extensionArray, StringComparer.OrdinalIgnoreCase);
                FileInfo[] files = Array.FindAll(di.GetFiles(), f => allowedExtensions.Contains(f.Extension));

                return (from f in files select f.Name).ToHashSet();
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
        /// <param name="localPath">Local path</param>
        /// <param name="prefix">File prefix</param>
        /// <returns>Local path to file as a string</returns>
        public static string GenerateLocalPathToFile(string url, string localPath, string prefix = "")
        {
            try
            {
                return new StringBuilder(localPath).Append(@"\").Append(prefix).Append(Path.GetFileName(url)).ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets image from file
        /// </summary>
        /// <param name="filePath">Filename</param>
        /// <returns>Bitmap image from the file</returns>
        public static Bitmap GetImageFromFile(string filePath)
        {
            try
            {
                using (Bitmap bm = new Bitmap(filePath))
                {
                    return new Bitmap(bm);
                }
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
        ///  Reads file contents into a string
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Contents of the file as string</returns>
        public static string ReadFileAsString(string filePath)
        {
            using (FileStream fs = new FileStream(@filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (TextReader reader = new StreamReader(fs))
                {
                    fs.Position = 0;
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Read tumblr save file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="format">Save file format</param>
        /// <returns>Tumblr Savefile object</returns>
        public static SaveFile ReadTumblrFile(string filePath, SaveFileFormat format)
        {
            try
            {
                switch (format)
                {
                    case SaveFileFormat.Bin:
                        return BinaryHelper.ReadObjectFromFile<SaveFile>(filePath);

                    case SaveFileFormat.Xml:
                        return XmlHelper.ReadObjectFromFile<SaveFile>(filePath);

                    case SaveFileFormat.Json:
                        return JsonHelper.ReadObjectFromFile<SaveFile>(filePath);

                    case SaveFileFormat.JsonCompressed:
                        return JsonHelper.ReadObjectFromFileCompressed<SaveFile>(filePath);

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
        /// <param name="filePath">File path</param>
        /// <returns>Savefile object or null if cannot load</returns>
        public static SaveFile ReadTumblrFile(string filePath)
        {
            try
            {
                SaveFile saveFile = ReadTumblrFile(filePath, SaveFileFormat.Bin) ??
                                     ReadTumblrFile(filePath, SaveFileFormat.Xml) ??
                                    ReadTumblrFile(filePath, SaveFileFormat.Json) ??
                                    ReadTumblrFile(filePath, SaveFileFormat.JsonCompressed);

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
        /// <param name="filePath">File path</param>
        /// <param name="saveFile">Savefile object</param>
        /// <returns>True if success saving, false otherwise</returns>
        public static bool SaveTumblrFile(string filePath, SaveFile saveFile)
        {
            try
            {
                return SaveTumblrFile(filePath, saveFile, _saveFileFormat);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Saves the save file
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <param name="saveFile">Savefile object</param>
        /// <param name="saveFileFormat">Savefile format</param>
        /// <returns>True if success saving, false otherwise</returns>
        public static bool SaveTumblrFile(string filePath, SaveFile saveFile, SaveFileFormat saveFileFormat)
        {
            try
            {
                saveFile.AddDate();
                switch (saveFileFormat)
                {
                    case SaveFileFormat.Bin:
                        return BinaryHelper.SaveObjectToFile(filePath, saveFile);

                    case SaveFileFormat.Xml:
                        return XmlHelper.SaveObjectToFile(filePath, saveFile);

                    case SaveFileFormat.JsonCompressed:
                        return JsonHelper.SaveObjectToFileCompressed(filePath, saveFile);

                    case SaveFileFormat.Json:
                        return JsonHelper.SaveObjectToFile(filePath, saveFile);

                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}