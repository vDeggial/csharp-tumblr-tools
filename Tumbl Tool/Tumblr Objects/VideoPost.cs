using System.Collections.Generic;

namespace Tumblr_Tool.Tumblr_Objects
{
    public class VideoPost : TumblrPost
    {
        public override string caption { get; set; }

        public override double duration { get; set; }

        public override bool isHtml5Capable { get; set; }

        public override int thumbnailHeight { get; set; }

        public override string thumbnailUrl { get; set; }

        public override int thumbnailWidth { get; set; }

        public override HashSet<VideoPostEmbedPlayer> videoPlayers { get; set; }

        public override string videoUrl { get; set; }
    }
}