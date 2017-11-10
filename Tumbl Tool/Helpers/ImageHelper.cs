/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: August, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System.Drawing;
using System.Drawing.Imaging;

namespace Tumblr_Tool.Helpers
{
    public static class ImageHelper
    {
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