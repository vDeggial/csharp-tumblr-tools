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
using System.Xml.Serialization;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Objects
{
    [Serializable]
    public class SaveFile
    {
        [XmlElement("blog")]
        public TumblrBlog Blog { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("filename")]
        public string Filename { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="blog"></param>
        public SaveFile(string fileName = "", TumblrBlog blog = null)
        {
            Filename = fileName;
            //this.blog = new Tumblr();
            //this.blog.cname = blog.cname;
            //this.blog.description = blog.description;
            //this.blog.name = blog.name;
            //this.blog.posts = new HashSet<TumblrPost>();
            //this.blog.posts.AddRange(blog.posts);
            //this.blog.timezone = blog.timezone;
            //this.blog.title = blog.title;
            //this.blog.totalPosts = blog.totalPosts;

            Blog = blog.Clone();
            AddDate();
        }

        /// <summary>
        ///
        /// </summary>
        public SaveFile()
        {
            Filename = null;
            Blog = null;
            AddDate();
        }

        /// <summary>
        ///
        /// </summary>
        public void AddDate()
        {
            string datePatt = @"yyyy-MM-dd HH:mm:ss zzz";

            Date = DateTime.Now.ToString(datePatt);
        }
    }
}