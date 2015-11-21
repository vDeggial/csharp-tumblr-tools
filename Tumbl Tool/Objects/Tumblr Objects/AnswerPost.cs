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
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [Serializable]
    public class AnswerPost : TumblrPost
    {
        /// <summary>
        ///
        /// </summary>
        public AnswerPost()
        {
            Type = TumblrPostTypes.Answer.ToString().ToLower();
        }

        public override string Answer { get; set; }
        public override string Asker { get; set; }

        public override string AskerUrl { get; set; }

        public override string Question { get; set; }
    }
}