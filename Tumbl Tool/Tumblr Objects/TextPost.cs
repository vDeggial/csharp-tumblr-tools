using System;

namespace Tumblr_Tool.Tumblr_Objects
{
    [Serializable()]
    public class TextPost : TumblrPost
    {
        public override string body { get; set; }

        public override string title { get; set; }
    }
}