using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tumbl_Tool.Tumblr_Objects
{
    [Serializable()]
    public class Tumblr
    {
        [XmlElement("name")]
        public string name { get; set; }
        [XmlElement("cname")]
        public string cname { get; set; }
        [XmlElement("title")]
        public string title { get; set; }
        [XmlElement("description")]
        public string description { get; set; }

        //[XmlIgnoreAttribute]
        public List<TumblrPost> posts { get; set; }

        public string timezone { get; set; }

        public int totalPosts { get; set; }


        public Tumblr(string name = "",string cname = "",string title = "",string description = "")
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
    }
}
