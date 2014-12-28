/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: December, 2014
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Tumblr_Objects
{
    [Serializable()]
    public class AnswerPost : TumblrPost
    {
        public string askingName { get; set; }

        public string askingUrl { get; set; }

        public string question { get; set; }

        public string answer { get; set; }

        public AnswerPost()
        {
            this.type = TumblrPostTypes.answer.ToString();
        }
    }
}