/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: September, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System.Collections.Generic;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    public class VideoPost : TumblrPost
    {
        public override string Caption { get; set; }

        public override string Duration { get; set; }

        public override string IsHtml5Capable { get; set; }

        public override string ThumbnailHeight { get; set; }

        public override string ThumbnailUrl { get; set; }

        public override string ThumbnailWidth { get; set; }

        public override HashSet<VideoPostEmbedPlayer> VideoPlayers { get; set; }

        public override string VideoType { get; set; }
        public override string VideoUrl { get; set; }
    }
}