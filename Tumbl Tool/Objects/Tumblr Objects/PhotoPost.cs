/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: November, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable]
    public class PhotoPost : TumblrPost
    {
        /// <summary>
        ///
        /// </summary>
        public PhotoPost()
        {
            Type = TumblrPostTypes.Photo.ToString().ToLower();
            Format = "html";
        }

        [XmlElement("caption")]
        public override string Caption { get; set; }

        public override HashSet<PhotoPostImage> Photos { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        public override void AddImageToPhotoSet(PhotoPostImage image)
        {
            if (Photos == null)
                Photos = new HashSet<PhotoPostImage>();

            Photos.Add(image);
        }
    }
}