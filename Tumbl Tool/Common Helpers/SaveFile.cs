using System;
using System.Xml.Serialization;
using Tumbl_Tool.Tumblr_Objects;

namespace Tumbl_Tool.Common_Helpers
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

        public Tumblr getBlog()
        {
            return this.blog;
        }

        public string getBlogName()
        {
            return blog.name;
        }

        public string getBlogURL()
        {
            return blog.cname;
        }

        public string getFileName()
        {
            return this.fileName;
        }

        public void setBlog(Tumblr blog)
        {
            this.blog = blog;
        }

        public void setFileName(string name)
        {
            this.fileName = name;
        }
    }
}