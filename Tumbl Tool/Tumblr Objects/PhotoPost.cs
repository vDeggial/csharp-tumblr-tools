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
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tumblr_Tool.Tumblr_Objects
{
    [Serializable()]
    public class PhotoPost : TumblrPost
    {
        public PhotoPost(string url = "", string caption = "")
            : base()
        {
            this.type = "photo";
            this.format = "html";
            this.caption = caption;
            this.imageURL = url;
        }

        public PhotoPost()
        {
            this.type = "photo";
            this.format = "html";
        }

        [XmlElement("caption")]
        public override string caption { get; set; }

        public override string fileName { get; set; }

        [XmlElement("imageURL")]
        public override string imageURL { get; set; }

        public override List<PhotoSetImage> photoset { get; set; }

        public override void addImageToPhotoSet(PhotoSetImage image)
        {
            if (photoset == null)
                photoset = new List<PhotoSetImage>();

            photoset.Add(image);
        }

        public override bool isPhotoset()
        {
            return photoset != null && photoset.Count != 0;
        }
    }
}