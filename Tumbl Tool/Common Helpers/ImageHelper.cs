using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Tumbl_Tool.Common_Helpers
{
    public static class ImageHelper
    {
        public static void addImageDescription(string path, string desc)
        {
            string fileDirectory = Path.GetDirectoryName(path);
            string _FileName = Path.GetFileNameWithoutExtension(path);
            string fileExt = Path.GetExtension(path);

            BitmapMetadata _metadata = null;
            BitmapDecoder _decoder = null;
            BitmapEncoder _encoder = null;
            FileInfo originalImage = File.Exists(path) ? new FileInfo(path) : null;
            string tempLocation = fileDirectory + @"\" + "temp.jpg";
            FileInfo tempImage;
            BitmapFrame _fileFrame;
            Image newImage;
            bool added = false;

            string _mimeType = CommonHelper.getMimeType(Path.GetExtension(path));

            if (File.Exists(path))
            {
                while (!added)
                {
                    try
                    {
                        if (File.Exists(tempLocation))
                            File.Delete(tempLocation);

                        originalImage.CopyTo(tempLocation, true);
                        tempImage = new FileInfo(tempLocation);

                        using (Stream fileStream = new System.IO.FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            newImage = Image.FromStream(fileStream);
                            fileStream.Seek(0, SeekOrigin.Begin);

                            switch (_mimeType) //find mime type of image based on extension
                            {
                                case "image/jpeg":
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

                                case "image/png":
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

                            if (_mimeType == "image/jpeg")
                            {
                                _metadata.Comment = desc;
                                _metadata.SetQuery("/xmp/dc:description", desc);
                            }
                            else if (_mimeType == "image/png")
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
                                        fileStreamOut.Close();
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
}