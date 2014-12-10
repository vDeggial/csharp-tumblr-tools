using System;

namespace Tumblr_Tool.Tumblr_Objects
{
    [Serializable()]
    public class AudioPost : TumblrPost
    {
        public override string album { get; set; }

        public override string albumArt { get; set; }

        public override string artist { get; set; }

        public override string audioUrl { get; set; }

        public override string caption { get; set; }

        public override string player { get; set; }

        public override int playsCount { get; set; }

        public override string trackName { get; set; }

        public override int trackNumber { get; set; }

        public override int year { get; set; }
    }
}