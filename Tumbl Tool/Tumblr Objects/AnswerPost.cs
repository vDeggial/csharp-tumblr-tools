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
            this.type = tumblrPostTypes.answer.ToString();
        }
    }
}