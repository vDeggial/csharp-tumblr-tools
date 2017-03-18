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
using System.IO;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable]
    public class PhotoPostImage
    {
        /// <summary>
        ///
        /// </summary>
        public PhotoPostImage()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public PhotoPostImage(string url, string caption, string width, string height)
        {
            Caption = caption;
            Url = url;
            Width = width;
            Height = height;
            Filename = !string.IsNullOrEmpty(Url) ? Path.GetFileName(Url) : null;
        }

        public string Aperture { get; set; }
        public string Camera { get; set; }
        public string Caption { get; set; }

        public bool Downloaded { get; set; }
        public string Exposure { get; set; }
        public string Filename { get; set; }

        public string FocalLength { get; set; }
        public string Height { get; set; }

        public string ISO { get; set; }
        public string ParentPostId { get; set; }
        public string Url { get; set; }

        public string Width { get; set; }
    }
}