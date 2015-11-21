/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: November, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable()]
    public class AudioPost : TumblrPost
    {
        public override string Album { get; set; }

        public override string AlbumArt { get; set; }

        public override string Artist { get; set; }

        public override string AudioType { get; set; }
        public override string AudioUrl { get; set; }
        public override string Caption { get; set; }

        public override string Player { get; set; }

        public override string PlaysCount { get; set; }

        public override string TrackName { get; set; }

        public override string TrackNumber { get; set; }

        public override string Year { get; set; }
    }
}