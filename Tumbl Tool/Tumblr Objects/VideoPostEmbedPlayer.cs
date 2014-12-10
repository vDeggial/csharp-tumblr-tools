using System;

namespace Tumblr_Tool.Tumblr_Objects
{
    [Serializable()]
    public class VideoPostEmbedPlayer
    {
        public string embedCode { get; set; }

        public int width { get; set; }
    }
}