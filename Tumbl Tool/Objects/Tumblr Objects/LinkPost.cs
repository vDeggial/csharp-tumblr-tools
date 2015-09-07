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
    public class LinkPost : TumblrPost
    {
        public override string description { get; set; }

        public override string excerpt { get; set; }
        public override string linkAuthor { get; set; }
        public override string linkImage { get; set; }
        public override string linkUrl { get; set; }

        public override string publisher { get; set; }
        public override string title { get; set; }
    }
}