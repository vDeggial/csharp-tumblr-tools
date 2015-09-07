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

using System;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable()]
    public class AudioPost : TumblrPost
    {
        public override string album { get; set; }

        public override string albumArt { get; set; }

        public override string artist { get; set; }

        public override string audioType { get; set; }
        public override string audioUrl { get; set; }
        public override string caption { get; set; }

        public override string player { get; set; }

        public override string playsCount { get; set; }

        public override string trackName { get; set; }

        public override string trackNumber { get; set; }

        public override string year { get; set; }
    }
}