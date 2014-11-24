using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tumbl_Tool.Tumblr_Objects
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