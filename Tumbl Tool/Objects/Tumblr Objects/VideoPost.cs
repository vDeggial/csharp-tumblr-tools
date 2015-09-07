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
        public override string caption { get; set; }

        public override string duration { get; set; }

        public override string isHtml5Capable { get; set; }

        public override string thumbnailHeight { get; set; }

        public override string thumbnailUrl { get; set; }

        public override string thumbnailWidth { get; set; }

        public override HashSet<VideoPostEmbedPlayer> videoPlayers { get; set; }

        public override string videoUrl { get; set; }
    }
}