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
using System.Xml.Serialization;

namespace Tumblr_Tool.Objects.Tumblr_Objects
{
    [XmlInclude(typeof(PhotoPost)), XmlInclude(typeof(AnswerPost)), XmlInclude(typeof(AudioPost)), XmlInclude(typeof(ChatPost)), XmlInclude(typeof(LinkPost)), XmlInclude(typeof(QuotePost)), XmlInclude(typeof(TextPost)), XmlInclude(typeof(VideoPost))]
    [Serializable]
    public class TumblrPost
    {
        public TumblrPost()
        {
            Tags = new HashSet<string>();
        }

        public virtual string Album { get; set; }

        public virtual string AlbumArt { get; set; }

        public virtual string Answer { get; set; }
        public virtual string Artist { get; set; }

        public virtual string Asker { get; set; }

        public virtual string AskerUrl { get; set; }
        public virtual string AudioType { get; set; }
        public virtual string AudioUrl { get; set; }
        public virtual string Body { get; set; }
        public virtual string Caption { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        public virtual string Description { get; set; }
        public virtual HashSet<ChatPostFragment> Dialogue { get; set; }
        public virtual string Duration { get; set; }
        public virtual string Excerpt { get; set; }

        [XmlElement("format")]
        public string Format { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        public virtual string IsHtml5Capable { get; set; }
        public string LastProcessedDate { get; set; }
        public virtual string LinkAuthor { get; set; }
        public virtual string LinkImage { get; set; }
        public virtual string LinkUrl { get; set; }
        public string NoteCount { get; set; }

        [XmlArrayItem("photo")]
        public virtual HashSet<PhotoPostImage> Photos { get; set; }

        public virtual string Player { get; set; }
        public virtual string PlaysCount { get; set; }
        public string PostAuthor { get; set; }

        [XmlElement("postText")]
        public string PostText { get; set; }

        public virtual string Publisher { get; set; }
        public virtual string Question { get; set; }

        [XmlElement("reblogKey")]
        public string ReblogKey { get; set; }

        public string ShortUrl { get; set; }

        public string Slug { get; set; }

        public virtual string Source { get; set; }

        public string SourceUrl { get; set; }

        public string State { get; set; }

        [XmlArrayItem("tag")]
        public HashSet<string> Tags { get; set; }

        public virtual string Text { get; set; }

        public virtual string ThumbnailHeight { get; set; }

        public virtual string ThumbnailUrl { get; set; }
        public virtual string ThumbnailWidth { get; set; }
        public string Timestamp { get; set; }
        public virtual string Title { get; set; }

        public virtual string TrackName { get; set; }

        public virtual string TrackNumber { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        public virtual HashSet<VideoPostEmbedPlayer> VideoPlayers { get; set; }

        public virtual string VideoType { get; set; }
        public virtual string VideoUrl { get; set; }

        public virtual string Year { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        public virtual void AddImageToPhotoSet(PhotoPostImage image)
        {
            // void
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tag"></param>
        public void AddTag(string tag)
        {
            if (Tags == null)
                Tags = new HashSet<string>();

            Tags.Add(tag);
        }
    }
}