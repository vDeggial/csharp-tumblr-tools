using System;

namespace Tumblr_Tool.Tumblr_Objects
{
    [Serializable()]
    public class QuotePost : TumblrPost
    {
        public override string source { get; set; }

        public override string text { get; set; }
    }
}