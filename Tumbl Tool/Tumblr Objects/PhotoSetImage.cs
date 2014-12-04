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
using System.IO;

namespace Tumblr_Tool.Tumblr_Objects
{
    [Serializable()]
    public class PhotoSetImage : ICloneable
    {
        public PhotoSetImage()
        {
        }

        public PhotoSetImage(string url, string caption, string width, string height, string offset)
        {
            this.caption = caption;
            this.imageURL = url;
            this.width = width;
            this.height = height;
            this.offset = offset;
            this.filename = !string.IsNullOrEmpty(this.imageURL) ? Path.GetFileName(this.imageURL) : null;
        }

        public string caption { get; set; }

        public string filename { get; set; }

        public string height { get; set; }

        public string imageURL { get; set; }

        public string offset { get; set; }

        public string width { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}