using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tumbl_Tool.Tumblr_Objects;
using System.Xml.Serialization;

namespace Tumbl_Tool.Common_Helpers
{
    [Serializable()]
    public class SaveFile
    {
        [XmlElement("filename")]
        public string fileName;
        [XmlElement("blog")]
        public Tumblr blog;

        [XmlElement("date")]
        public string date;


        public SaveFile(string fileName = "",Tumblr blog = null)
        {
            this.fileName = fileName;
            this.blog = blog;

        }

        public SaveFile()
        {
            this.fileName = null; ;
            this.blog = null;

        }


        public void setFileName(string name)
        {
            this.fileName = name;

        }


        public string getFileName()
        {
            return this.fileName;

        }


        public void setBlog(Tumblr blog)
        {
            this.blog = blog;

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
    }
}
