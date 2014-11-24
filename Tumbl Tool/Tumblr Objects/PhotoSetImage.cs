using System.IO;

namespace Tumbl_Tool.Tumblr_Objects
{
    public class PhotoSetImage
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
    }
}