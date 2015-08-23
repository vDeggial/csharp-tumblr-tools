/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: August, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable()]
    public class PhotoPost : TumblrPost
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="caption"></param>
        public PhotoPost(string url = "", string caption = "")
            : base()
        {
            this.type = TumblrPostTypes.photo.ToString();
            this.format = "html";
            this.caption = caption;
        }

        /// <summary>
        ///
        /// </summary>
        public PhotoPost()
        {
            this.type = TumblrPostTypes.photo.ToString();
            this.format = "html";
        }

        [XmlElement("caption")]
        public override string caption { get; set; }

        public override HashSet<PhotoPostImage> photos { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        public override void AddImageToPhotoSet(PhotoPostImage image)
        {
            if (photos == null)
                photos = new HashSet<PhotoPostImage>();

            photos.Add(image);
        }
    }
}