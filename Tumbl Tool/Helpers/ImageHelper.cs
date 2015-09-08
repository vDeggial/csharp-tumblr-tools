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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Tumblr_Tool.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="desc"></param>
        public static void AddImageDescription(string path, string desc)
        {
            string _FileDirectory = Path.GetDirectoryName(path);
            string _FileName = Path.GetFileNameWithoutExtension(path);
            string _FileExt = Path.GetExtension(path);

            BitmapMetadata _metadata = null;
            BitmapDecoder _decoder = null;
            BitmapEncoder _encoder = null;
            FileInfo originalImage;
            string tempLocation = _FileDirectory + @"\" + "temp.jpg";
            FileInfo tempImage;
            BitmapFrame _fileFrame;
            Image newImage;
            bool added = false;
            ImageFormat imageFormat = ImageFormat.Jpeg;
            string _mimeType = "";
            string filePath = path;

            if (_FileExt == null || _FileExt == "")
            {
                filePath = FileHelper.FindFile(_FileDirectory, _FileName);
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    path = filePath;
                    _FileName = Path.GetFileNameWithoutExtension(filePath);
                    _FileExt = Path.GetExtension(filePath);
                    imageFormat = GetImageFormat(Image.FromFile(path));

                    _mimeType = imageFormat.ToString();

                    while (!added)
                    {
                        try
                        {
                            using (Stream fileStream = new System.IO.FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                            {
                                originalImage = File.Exists(path) ? new FileInfo(path) : null;
                                if (File.Exists(tempLocation))
                                    File.Delete(tempLocation);

                                originalImage.CopyTo(tempLocation, true);
                                tempImage = new FileInfo(tempLocation);
                                newImage = Image.FromStream(fileStream);
                                fileStream.Seek(0, SeekOrigin.Begin);

                                switch (_mimeType) //find mime type of image based on extension
                                {
                                    case "Jpeg":
                                        try
                                        {
                                            _decoder = new JpegBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                                            _encoder = new JpegBitmapEncoder();
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                fileStream.Seek(0, SeekOrigin.Begin);
                                                _decoder = new JpegBitmapDecoder(fileStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                                                _encoder = new JpegBitmapEncoder();
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    fileStream.Seek(0, SeekOrigin.Begin);
                                                    _decoder = new PngBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                                                    _encoder = new PngBitmapEncoder();
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        fileStream.Seek(0, SeekOrigin.Begin);
                                                        _decoder = new PngBitmapDecoder(fileStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                                                        _encoder = new PngBitmapEncoder();
                                                    }
                                                    catch
                                                    {
                                                        _decoder = null;
                                                        added = true;
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    case "Png":
                                        try
                                        {
                                            _decoder = new PngBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                                            _encoder = new PngBitmapEncoder();
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                fileStream.Seek(0, SeekOrigin.Begin);
                                                _decoder = new PngBitmapDecoder(fileStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                                                _encoder = new PngBitmapEncoder();
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    fileStream.Seek(0, SeekOrigin.Begin);
                                                    _decoder = new JpegBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                                                    _encoder = new JpegBitmapEncoder();
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        fileStream.Seek(0, SeekOrigin.Begin);
                                                        _decoder = new JpegBitmapDecoder(fileStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                                                        _encoder = new JpegBitmapEncoder();
                                                    }
                                                    catch
                                                    {
                                                        _decoder = null;
                                                        added = true;
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    default: // Not jpeg or png file - dont add the comments
                                        _decoder = null;
                                        added = true;
                                        break;
                                }
                            }

                            if (_decoder != null && desc != null)
                            {
                                _metadata = (BitmapMetadata)_decoder.Frames[0].Metadata.Clone();

                                if (_mimeType == "Jpeg")
                                {
                                    _metadata.Comment = desc;
                                    _metadata.SetQuery("/xmp/dc:description", desc);
                                }
                                else if (_mimeType == "Png")
                                {
                                    _metadata.SetQuery("/xmp/dc:description", desc);
                                }

                                _fileFrame = BitmapFrame.Create(_decoder.Frames[0], _decoder.Frames[0].Thumbnail, _metadata, _decoder.Frames[0].ColorContexts);
                                _encoder.Frames.Add(_fileFrame);

                                using (Stream fileStreamOut = new FileStream(path, FileMode.Create))
                                {
                                    try
                                    {
                                        if (_encoder != null)
                                            _encoder.Save(fileStreamOut);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            //fileStreamOut.Close();
                                            tempImage.CopyTo(path, true);

                                            added = true;
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                added = true;
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
        ///
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
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
            else
            {
                img.Dispose();
                return ImageFormat.Wmf;
            }
        }
    }
}