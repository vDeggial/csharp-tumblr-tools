/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: June, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.IO;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable()]
    public class PhotoPostImage
    {
        public PhotoPostImage()
        {
        }

        public PhotoPostImage(string url, string caption, string width, string height)
        {
            this.caption = caption;
            this.url = url;
            this.width = width;
            this.height = height;
            this.filename = !string.IsNullOrEmpty(this.url) ? Path.GetFileName(this.url) : null;
        }

        public string caption { get; set; }

        public string filename { get; set; }

        public string height { get; set; }

        public string url { get; set; }

        public string width { get; set; }

        public bool downloaded { get; set; }

        public string parentPostID { get; set; }
    }
}