using System;

namespace Tumblr_Tool.Tumblr_Objects
{
    [Serializable()]
    public class LinkPost : TumblrPost
    {
        public override string description { get; set; }

        public override string linkUrl { get; set; }

        public override string title { get; set; }
    }
}