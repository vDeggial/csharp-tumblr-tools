/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: June, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable()]
    public class ChatPostFragment
    {
        public string label { get; set; }

        public string name { get; set; }

        public string phrase { get; set; }
    }
}