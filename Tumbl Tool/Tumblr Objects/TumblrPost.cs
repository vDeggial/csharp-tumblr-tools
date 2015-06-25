/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tumblr_Tool.Tumblr_Objects
{
    [XmlInclude(typeof(PhotoPost)), XmlInclude(typeof(AnswerPost)), XmlInclude(typeof(AudioPost)), XmlInclude(typeof(ChatPost)), XmlInclude(typeof(LinkPost)), XmlInclude(typeof(QuotePost)), XmlInclude(typeof(TextPost)), XmlInclude(typeof(VideoPost))]
    [Serializable()]
    public class TumblrPost
    {
        public TumblrPost()
        {
            this.tags = new HashSet<string>();
        }

        public virtual string album { get; set; }

        public virtual string albumArt { get; set; }

        public virtual string artist { get; set; }

        public virtual string audioUrl { get; set; }

        public virtual string body { get; set; }

        public virtual string caption { get; set; }

        [XmlElement("date")]
        public string date { get; set; }

        public virtual string description { get; set; }

        public virtual HashSet<ChatPostFragment> dialogue { get; set; }

        public virtual string duration { get; set; }

        [XmlElement("format")]
        public string format { get; set; }

        [XmlElement("id")]
        public string id { get; set; }

        public virtual string isHtml5Capable { get; set; }

        public string lastProcessedDate { get; set; }

        public virtual string linkUrl { get; set; }

        public string noteCount { get; set; }

        [XmlArrayItem("photo")]
        public virtual HashSet<PhotoPostImage> photos { get; set; }

        public virtual string player { get; set; }

        public virtual string playsCount { get; set; }

        [XmlElement("postText")]
        public string postText { get; set; }

        [XmlElement("reblogKey")]
        public string reblogKey { get; set; }

        public string shortURL { get; set; }

        public virtual string source { get; set; }

        public string sourceURL { get; set; }

        [XmlArrayItem("tag")]
        public HashSet<string> tags { get; set; }

        public virtual string text { get; set; }

        public virtual string thumbnailHeight { get; set; }

        public virtual string thumbnailUrl { get; set; }

        public virtual string thumbnailWidth { get; set; }

        public virtual string title { get; set; }

        public virtual string trackName { get; set; }

        public virtual string trackNumber { get; set; }

        [XmlElement("type")]
        public string type { get; set; }

        [XmlElement("url")]
        public string url { get; set; }

        public virtual HashSet<VideoPostEmbedPlayer> videoPlayers { get; set; }

        public virtual string videoUrl { get; set; }

        public virtual string year { get; set; }

        public virtual void AddImageToPhotoSet(PhotoPostImage image)
        {
            // void
        }

        public void AddTag(string tag)
        {
            if (this.tags == null)
                this.tags = new HashSet<string>();

            this.tags.Add(tag);
        }
    }
}