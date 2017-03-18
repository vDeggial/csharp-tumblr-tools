/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Tumblr_Tool.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        /// Add text comment to image file
        /// </summary>
        /// <param name="path">Image file path</param>
        /// <param name="desc">Text to add as comment</param>
        public static void AddImageDescription(string path, string desc)
        {
            string fileDirectory = Path.GetDirectoryName(path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            string fileExt = Path.GetExtension(path);

            BitmapEncoder encoder = null;
            string tempLocation = fileDirectory + @"\" + "temp.jpg";
            bool added = false;
            string filePath = path;

            if (string.IsNullOrEmpty(fileExt))
            {
                filePath = FileHelper.FindFile(fileDirectory, fileName);
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    path = filePath;
                    var imageFormat = GetImageFormat(Image.FromFile(path));

                    var mimeType = imageFormat.ToString();

                    while (!added)
                    {
                        try
                        {
                            BitmapDecoder decoder;
                            FileInfo tempImage;
                            using (Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                            {
                                var originalImage = File.Exists(path) ? new FileInfo(path) : null;
                                if (File.Exists(tempLocation))
                                    File.Delete(tempLocation);

                                originalImage.CopyTo(tempLocation, true);
                                tempImage = new FileInfo(tempLocation);
                                fileStream.Seek(0, SeekOrigin.Begin);

                                switch (mimeType) //find mime type of image based on extension
                                {
                                    case "Jpeg":
                                        try
                                        {
                                            decoder = new JpegBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                                            encoder = new JpegBitmapEncoder();
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                fileStream.Seek(0, SeekOrigin.Begin);
                                                decoder = new JpegBitmapDecoder(fileStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                                                encoder = new JpegBitmapEncoder();
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    fileStream.Seek(0, SeekOrigin.Begin);
                                                    decoder = new PngBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                                                    encoder = new PngBitmapEncoder();
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        fileStream.Seek(0, SeekOrigin.Begin);
                                                        decoder = new PngBitmapDecoder(fileStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                                                        encoder = new PngBitmapEncoder();
                                                    }
                                                    catch
                                                    {
                                                        decoder = null;
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    case "Png":
                                        try
                                        {
                                            decoder = new PngBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                                            encoder = new PngBitmapEncoder();
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                fileStream.Seek(0, SeekOrigin.Begin);
                                                decoder = new PngBitmapDecoder(fileStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                                                encoder = new PngBitmapEncoder();
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    fileStream.Seek(0, SeekOrigin.Begin);
                                                    decoder = new JpegBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                                                    encoder = new JpegBitmapEncoder();
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        fileStream.Seek(0, SeekOrigin.Begin);
                                                        decoder = new JpegBitmapDecoder(fileStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                                                        encoder = new JpegBitmapEncoder();
                                                    }
                                                    catch
                                                    {
                                                        decoder = null;
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    default: // Not jpeg or png file - dont add the comments
                                        decoder = null;
                                        break;
                                }
                            }

                            if (decoder != null && desc != null)
                            {
                                var metadata = (BitmapMetadata)decoder.Frames[0].Metadata.Clone();

                                if (mimeType == "Jpeg")
                                {
                                    metadata.Comment = desc;
                                    metadata.SetQuery("/xmp/dc:description", desc);
                                }
                                else if (mimeType == "Png")
                                {
                                    metadata.SetQuery("/xmp/dc:description", desc);
                                }

                                var fileFrame = BitmapFrame.Create(decoder.Frames[0], decoder.Frames[0].Thumbnail, metadata, decoder.Frames[0].ColorContexts);
                                encoder.Frames.Add(fileFrame);

                                using (Stream fileStreamOut = new FileStream(path, FileMode.Create))
                                {
                                    try
                                    {
                                        encoder?.Save(fileStreamOut);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            //fileStreamOut.Close();
                                            tempImage.CopyTo(path, true);
                                        }
                                        catch
                                        {
                                            // ignored
                                        }
                                    }
                                }
                            }
                            added = true;
                            File.Delete(tempLocation);
                        }
                        catch (NotSupportedException)
                        {
                            added = true;
                        }
                        catch (FileFormatException)
                        {
                            added = true;
                        }
                        catch (IOException)
                        {
                            added = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get image format
        /// </summary>
        /// <param name="img">Image object</param>
        /// <returns>Image format</returns>
        public static ImageFormat GetImageFormat(this Image img)
        {
            if (img.RawFormat.Equals(ImageFormat.Jpeg))
            {
                img.Dispose();
                return ImageFormat.Jpeg;
            }
            if (img.RawFormat.Equals(ImageFormat.Bmp))
            {
                img.Dispose();
                return ImageFormat.Bmp;
            }
            if (img.RawFormat.Equals(ImageFormat.Png))
            {
                img.Dispose();
                return ImageFormat.Png;
            }
            if (img.RawFormat.Equals(ImageFormat.Emf))
            {
                img.Dispose();
                return ImageFormat.Emf;
            }
            if (img.RawFormat.Equals(ImageFormat.Exif))
            {
                img.Dispose();
                return ImageFormat.Exif;
            }
            if (img.RawFormat.Equals(ImageFormat.Gif))
            {
                img.Dispose();
                return ImageFormat.Gif;
            }
            if (img.RawFormat.Equals(ImageFormat.Icon))
            {
                img.Dispose();
                return ImageFormat.Icon;
            }
            if (img.RawFormat.Equals(ImageFormat.MemoryBmp))
            {
                img.Dispose();
                return ImageFormat.MemoryBmp;
            }
            if (img.RawFormat.Equals(ImageFormat.Tiff))
            {
                img.Dispose();
                return ImageFormat.Tiff;
            }
            img.Dispose();
            return ImageFormat.Wmf;
        }
    }
}