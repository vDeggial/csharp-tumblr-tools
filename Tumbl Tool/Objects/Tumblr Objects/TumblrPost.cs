/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: August, 2016
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

        [XmlIgnore]
        public virtual string Album { get; set; }

        [XmlIgnore]
        public virtual string AlbumArt { get; set; }

        [XmlIgnore]
        public virtual string Answer { get; set; }

        [XmlIgnore]
        public virtual string Artist { get; set; }

        [XmlIgnore]
        public virtual string Asker { get; set; }

        [XmlIgnore]
        public virtual string AskerUrl { get; set; }

        [XmlIgnore]
        public virtual string AudioType { get; set; }

        [XmlIgnore]
        public virtual string AudioUrl { get; set; }

        [XmlIgnore]
        public virtual string Body { get; set; }

        [XmlIgnore]
        public virtual string Caption { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlIgnore]
        public virtual string Description { get; set; }

        [XmlIgnore]
        public virtual HashSet<ChatPostFragment> Dialogue { get; set; }

        [XmlIgnore]
        public virtual string Duration { get; set; }

        [XmlIgnore]
        public virtual string Excerpt { get; set; }

        public string Format { get; set; }

        public string Id { get; set; }

        [XmlIgnore]
        public virtual string IsHtml5Capable { get; set; }

        public string LastProcessedDate { get; set; }

        [XmlIgnore]
        public virtual string LinkAuthor { get; set; }

        [XmlIgnore]
        public virtual string LinkImage { get; set; }

        [XmlIgnore]
        public virtual string LinkUrl { get; set; }

        [XmlIgnore]
        public string NoteCount { get; set; }

        [XmlIgnore]
        public virtual HashSet<PhotoPostImage> Photos { get; set; }

        [XmlIgnore]
        public virtual string Player { get; set; }

        [XmlIgnore]
        public virtual string PlaysCount { get; set; }

        public string PostAuthor { get; set; }

        [XmlElement("postText")]
        public string PostText { get; set; }

        [XmlIgnore]
        public virtual string Publisher { get; set; }

        [XmlIgnore]
        public virtual string Question { get; set; }

        public string ReblogKey { get; set; }

        public string ShortUrl { get; set; }

        public string Slug { get; set; }

        [XmlIgnore]
        public virtual string Source { get; set; }

        public string SourceTitle { get; set; }
        public string SourceUrl { get; set; }
        public string State { get; set; }

        public string Summary { get; set; }

        public HashSet<string> Tags { get; set; }

        [XmlIgnore]
        public virtual string Text { get; set; }

        [XmlIgnore]
        public virtual string ThumbnailHeight { get; set; }

        [XmlIgnore]
        public virtual string ThumbnailUrl { get; set; }

        [XmlIgnore]
        public virtual string ThumbnailWidth { get; set; }

        public string Timestamp { get; set; }

        [XmlIgnore]
        public virtual string Title { get; set; }

        [XmlIgnore]
        public virtual string TrackName { get; set; }

        [XmlIgnore]
        public virtual string TrackNumber { get; set; }

        [XmlIgnore]
        public virtual string ImagePermalink { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        [XmlIgnore]
        public virtual HashSet<VideoPostEmbedPlayer> VideoPlayers { get; set; }

        [XmlIgnore]
        public virtual string VideoType { get; set; }

        [XmlIgnore]
        public virtual string VideoUrl { get; set; }

        [XmlIgnore]
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