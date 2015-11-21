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
using System.Collections.Generic;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable()]
    public class ChatPost : TumblrPost
    {
        public override string Body { get; set; }

        public override HashSet<ChatPostFragment> Dialogue { get; set; }

        public override string Title { get; set; }
    }
}