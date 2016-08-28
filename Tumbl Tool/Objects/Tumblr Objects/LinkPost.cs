/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: August, 2016
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable()]
    public class LinkPost : TumblrPost
    {
        public override string Description { get; set; }

        public override string Excerpt { get; set; }
        public override string LinkAuthor { get; set; }
        public override string LinkImage { get; set; }
        public override string LinkUrl { get; set; }

        public override string Publisher { get; set; }
        public override string Title { get; set; }
    }
}