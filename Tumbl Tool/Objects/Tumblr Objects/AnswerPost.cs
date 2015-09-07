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
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable()]
    public class AnswerPost : TumblrPost
    {
        public override string askingName { get; set; }

        public override string askingUrl { get; set; }

        public override string question { get; set; }

        public override string answer { get; set; }

        /// <summary>
        ///
        /// </summary>
        public AnswerPost()
        {
            this.type = TumblrPostTypes.answer.ToString();
        }
    }
}