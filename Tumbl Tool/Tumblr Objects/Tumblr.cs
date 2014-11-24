using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tumbl_Tool.Tumblr_Objects
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

        public string timezone { get; set; }

        [XmlElement("title")]
        public string title { get; set; }

        public int totalPosts { get; set; }
    }
}