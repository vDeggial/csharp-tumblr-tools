using System;
using System.Xml.Serialization;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Common_Helpers
{
    [Serializable()]
    public class SaveFile
    {
        [XmlElement("blog")]
        public Tumblr blog;

        [XmlElement("date")]
        public string date;

        [XmlElement("filename")]
        public string fileName;

        public SaveFile(string fileName = "", Tumblr blog = null)
        {
            this.fileName = fileName;
            this.blog = blog;
        }

        public SaveFile()
        {
            this.fileName = null; ;
            this.blog = null;
        }

        public string getBlogURL()
        {
            return blog.cname;
        }

        public string getFileName()
        {
            return this.fileName;
        }
    }
}