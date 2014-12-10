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
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tumblr_Tool.Tumblr_Objects
{
    [XmlInclude(typeof(PhotoPost))]
    [Serializable()]
    public class TumblrPost
    {
        public TumblrPost()
        {
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

        public virtual List<ChatPostFragment> dialogue { get; set; }

        public virtual double duration { get; set; }

        public virtual string fileName { get; set; }

        [XmlElement("format")]
        public string format { get; set; }

        [XmlElement("id")]
        public string id { get; set; }

        //Overridden
        public virtual string imageURL { get; set; }

        public virtual bool isHtml5Capable { get; set; }

        public virtual string linkUrl { get; set; }

        public virtual List<PhotoPostImage> photoset { get; set; }

        public virtual string player { get; set; }

        public virtual int playsCount { get; set; }

        [XmlElement("postText")]
        public string postText { get; set; }

        [XmlElement("reblogKey")]
        public string reblogKey { get; set; }

        public virtual string source { get; set; }

        public List<string> tags { get; set; }

        public virtual string text { get; set; }

        public virtual int thumbnailHeight { get; set; }

        public virtual string thumbnailUrl { get; set; }

        public virtual int thumbnailWidth { get; set; }

        public virtual string title { get; set; }

        public virtual string trackName { get; set; }

        public virtual int trackNumber { get; set; }

        [XmlElement("type")]
        public string type { get; set; }

        [XmlElement("url")]
        public string url { get; set; }

        public virtual List<VideoPostEmbedPlayer> videoPlayers { get; set; }

        public virtual string videoUrl { get; set; }

        public virtual int year { get; set; }

        public virtual void addImageToPhotoSet(PhotoPostImage image)
        {
            // void
        }

        public void addTag(string tag)
        {
            if (this.tags == null)
                tags = new List<string>();

            this.tags.Add(tag);
        }

        public virtual bool isPhotoset()
        {
            return false;
        }
    }
}