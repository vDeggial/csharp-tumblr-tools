/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: June, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable()]
    public class TumblrBlog
    {
        public TumblrBlog()
        {
            // posts = new HashSet<TumblrPost>();
        }

        public TumblrBlog(string url)
        {
            this.url = url;
        }

        public int blogTotalPosts { get; set; }

        public string cname { get; set; }

        [XmlElement("description")]
        public string description { get; set; }

        public bool isAnonAskEnabled { get; set; }

        public bool isAskEnabled { get; set; }

        public bool isNsfw { get; set; }

        public DateTime lastUpdated { get; set; }

        [XmlElement("name")]
        public string name { get; set; }

        //[XmlIgnoreAttribute]
        [XmlArrayItem("post")]
        public HashSet<TumblrPost> posts { get; set; }

        [XmlElement("timezone")]
        public string timezone { get; set; }

        [XmlElement("title")]
        public string title { get; set; }

        public int totalPosts { get; set; }

        [XmlElement("url")]
        public string url { get; set; }
    }
}