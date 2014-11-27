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
            //this.blog = new Tumblr();
            //this.blog.cname = blog.cname;
            //this.blog.description = blog.description;
            //this.blog.name = blog.name;
            //this.blog.posts = new List<TumblrPost>();
            //this.blog.posts.AddRange(blog.posts);
            //this.blog.timezone = blog.timezone;
            //this.blog.title = blog.title;
            //this.blog.totalPosts = blog.totalPosts;

            this.blog = blog.Clone();
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