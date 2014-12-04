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
    public class Tumblr
    {
        public Tumblr(string name = "", string cname = "", string title = "", string description = "")
        {
            this.cname = cname;
            this.name = name;
            this.title = title;
            this.description = description;
            posts = new List<TumblrPost>();
        }

        public Tumblr()
        {
        }

        [XmlElement("cname")]
        public string cname { get; set; }

        [XmlElement("description")]
        public string description { get; set; }

        [XmlElement("name")]
        public string name { get; set; }

        //[XmlIgnoreAttribute]
        public List<TumblrPost> posts { get; set; }

        [XmlElement("timezone")]
        public string timezone { get; set; }

        [XmlElement("title")]
        public string title { get; set; }

        [XmlElement("totalPosts")]
        public int totalPosts { get; set; }
    }
}