using System;
using System.Collections.Generic;

namespace Tumblr_Tool.Tumblr_Objects
{
    [Serializable()]
    public class ChatPost : TumblrPost
    {
        public override string body { get; set; }

        public override HashSet<ChatPostFragment> dialogue { get; set; }

        public override string title { get; set; }
    }
}