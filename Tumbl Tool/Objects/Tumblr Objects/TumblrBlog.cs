/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: April, 2016
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable]
    public class TumblrBlog
    {
        public TumblrBlog()
        {
            // posts = new HashSet<TumblrPost>();
        }

        public TumblrBlog(string url)
        {
            Url = url;
        }

        public bool AnonAskEnabled { get; set; }
        public bool AskEnabled { get; set; }
        public int BlogTotalPosts { get; set; }

        public string Cname { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        public DateTime LastUpdated { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        public bool Nsfw { get; set; }

        //[XmlIgnoreAttribute]
        [XmlArrayItem("Post")]
        public HashSet<TumblrPost> Posts { get; set; }

        [XmlElement("Timezone")]
        public string Timezone { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        public int TotalPosts { get; set; }

        [XmlElement("Url")]
        public string Url { get; set; }
    }
}