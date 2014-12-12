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
using System.Xml.Serialization;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Common_Helpers
{
    [Serializable()]
    public class SaveFile
    {
        [XmlElement("blog")]
        public TumblrBlog blog;

        [XmlElement("date")]
        public string date;

        [XmlElement("filename")]
        public string fileName;

        public SaveFile(string fileName = "", TumblrBlog blog = null)
        {
            this.fileName = fileName;
            //this.blog = new Tumblr();
            //this.blog.cname = blog.cname;
            //this.blog.description = blog.description;
            //this.blog.name = blog.name;
            //this.blog.posts = new HashSet<TumblrPost>();
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
            return blog.url;
        }

        public string getFileName()
        {
            return this.fileName;
        }
    }
}