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
using System.IO;
using System.Linq;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Helpers
{
    public static class PostHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateAnswerPost(ref TumblrPost post, dynamic jPost)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            post = new AnswerPost
            {
                Asker = !string.IsNullOrEmpty((string)jPost.asking_name) ? jPost.asking_name : null,
                AskerUrl = !string.IsNullOrEmpty((string)jPost.asking_url) ? jPost.asking_url : null,
                Question = !string.IsNullOrEmpty((string)jPost.question) ? jPost.question : null,
                Answer = !string.IsNullOrEmpty((string)jPost.answer) ? jPost.answer : null
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateAudioPost(ref TumblrPost post, dynamic jPost)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            post = new AudioPost
            {
                Artist = !string.IsNullOrEmpty((string)jPost.artist) ? jPost.artist : null,
                Album = !string.IsNullOrEmpty((string)jPost.album) ? jPost.album : null,
                AlbumArt = !string.IsNullOrEmpty((string)jPost.album_art) ? jPost.album_art : null,
                TrackName = !string.IsNullOrEmpty((string)jPost.track_name) ? jPost.track_name : null,
                Caption = !string.IsNullOrEmpty((string)jPost.caption) ? jPost.caption : null,
                AudioType = !string.IsNullOrEmpty((string)jPost.audio_type) ? jPost.audio_type : null,
                AudioUrl = !string.IsNullOrEmpty((string)jPost.audio_url) ? jPost.audio_url : null,
                PlaysCount = !string.IsNullOrEmpty((string)jPost.plays) ? jPost.plays : null,
                Player = !string.IsNullOrEmpty((string)jPost.player) ? jPost.player : null
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateChatPost(ref TumblrPost post, dynamic jPost)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            post = new ChatPost
            {
                Title = !string.IsNullOrEmpty((string)jPost.title) ? jPost.title : null,
                Body = !string.IsNullOrEmpty((string)jPost.body) ? jPost.body : null
            };

            if (jPost.dialogue != null)
            {
                post.Dialogue = new HashSet<ChatPostFragment>();
                foreach (dynamic jChatFragment in jPost.dialogue)
                {
                    ChatPostFragment chatFragment = new ChatPostFragment
                    {
                        Name = !string.IsNullOrEmpty((string)jChatFragment.name) ? jChatFragment.name : null,
                        Label = !string.IsNullOrEmpty((string)jChatFragment.label) ? jChatFragment.label : null,
                        Phrase = !string.IsNullOrEmpty((string)jChatFragment.phrase) ? jChatFragment.phrase : null
                    };
                    post.Dialogue.Add(chatFragment);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateLinkPost(ref TumblrPost post, dynamic jPost)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            post = new LinkPost
            {
                LinkAuthor = !string.IsNullOrEmpty((string)jPost.link_author) ? jPost.link_author : null,
                LinkImage = !string.IsNullOrEmpty((string)jPost.link_image) ? jPost.link_image : null,
                LinkUrl = !string.IsNullOrEmpty((string)jPost.url) ? jPost.url : null,
                Title = !string.IsNullOrEmpty((string)jPost.title) ? jPost.title : null,
                Description = !string.IsNullOrEmpty((string)jPost.description) ? jPost.description : null,
                Excerpt = !string.IsNullOrEmpty((string)jPost.excerpt) ? jPost.excerpt : null,
                Publisher = !string.IsNullOrEmpty((string)jPost.publisher) ? jPost.publisher : null
            };
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="post"></param>
        ///  <param name="jPost"></param>
        /// <param name="imageSize"></param>
        public static void GeneratePhotoPost(ref TumblrPost post, dynamic jPost, ImageSizes imageSize)
        {
            if (jPost.type == TumblrPostTypes.Photo.ToString().ToLower())
            {
                post = new PhotoPost { Photos = new HashSet<PhotoPostImage>() };

                foreach (dynamic jPhoto in jPost.photos)
                {
                    PhotoPostImage postImage = new PhotoPostImage();

                    if (imageSize == ImageSizes.Original)
                    {
                        postImage.Url = jPhoto.original_size != null ? !string.IsNullOrEmpty((string)jPhoto.original_size.url) ? jPhoto.original_size.url : null : null;
                        postImage.Filename = !string.IsNullOrEmpty(postImage.Url) ? Path.GetFileName(postImage.Url) : null;
                        postImage.Width = jPhoto.original_size != null ? !string.IsNullOrEmpty((string)jPhoto.original_size.width) ? jPhoto.original_size.width : null : null;
                        postImage.Height = jPhoto.original_size != null ? !string.IsNullOrEmpty((string)jPhoto.original_size.height) ? jPhoto.original_size.height : null : null;
                    }
                    else if (imageSize != ImageSizes.None)
                    {
                        if (jPhoto.alt_sizes != null)
                        {
                            var jAltPhotos = new HashSet<dynamic>(jPhoto.alt_sizes);
                            dynamic jPhotoAlt = (from p in jAltPhotos where p.width == ((int)imageSize).ToString() select p).FirstOrDefault();
                            if (jPhotoAlt != null)
                            {
                                postImage.Url = jPhotoAlt.url;
                                postImage.Filename = !string.IsNullOrEmpty(postImage.Url) ? Path.GetFileName(postImage.Url) : null;
                                postImage.Width = jPhotoAlt.width;
                                postImage.Height = jPhotoAlt.height;
                            }
                        }
                    }

                    postImage.Caption = !string.IsNullOrEmpty((string)jPhoto.caption) ? jPhoto.caption : null;
                    postImage.ParentPostId = !string.IsNullOrEmpty((string)jPost.id) ? jPost.id : null;
                    post.Photos.Add(postImage);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateQuotePost(ref TumblrPost post, dynamic jPost)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            post = new QuotePost
            {
                Text = !string.IsNullOrEmpty((string)jPost.text) ? jPost.text : null,
                Source = !string.IsNullOrEmpty((string)jPost.source) ? jPost.source : null
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateTextPost(ref TumblrPost post, dynamic jPost)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            post = new TextPost
            {
                Title = !string.IsNullOrEmpty((string)jPost.title) ? jPost.title : null,
                Body = !string.IsNullOrEmpty((string)jPost.body) ? jPost.body : null
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateVideoPost(ref TumblrPost post, dynamic jPost)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            post = new VideoPost
            {
                Caption = !string.IsNullOrEmpty((string)jPost.caption) ? jPost.caption : null,
                VideoType = !string.IsNullOrEmpty((string)jPost.video_type) ? jPost.video_type : null,
                VideoUrl = !string.IsNullOrEmpty((string)jPost.permalink_url) ? jPost.permalink_url : null,
                ThumbnailUrl = !string.IsNullOrEmpty((string)jPost.thumbnail_url) ? jPost.thumbnail_url : null,
                ThumbnailWidth = !string.IsNullOrEmpty((string)jPost.thumbnail_width) ? jPost.thumbnail_width : null,
                ThumbnailHeight = !string.IsNullOrEmpty((string)jPost.thumbnail_height) ? jPost.thumbnail_height : null,
                IsHtml5Capable = !string.IsNullOrEmpty((string)jPost.html5_capable) ? jPost.html5_capable : null
            };

            if (jPost.player != null)
            {
                post.VideoPlayers = new HashSet<VideoPostEmbedPlayer>();
                foreach (dynamic jPlayer in jPost.player)
                {
                    VideoPostEmbedPlayer player = new VideoPostEmbedPlayer
                    {
                        EmbedCode = !string.IsNullOrEmpty((string)jPlayer.embed_code) ? jPlayer.embed_code : null,
                        Width = !string.IsNullOrEmpty((string)jPlayer.width) ? jPlayer.width : null
                    };
                    post.VideoPlayers.Add(player);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void IncludeCommonPostFields(ref TumblrPost post, dynamic jPost)
        {
            post.Type = !string.IsNullOrEmpty((string)jPost.type) ? jPost.type : null;

            post.Id = !string.IsNullOrEmpty((string)jPost.id) ? jPost.id : null;

            post.Url = !string.IsNullOrEmpty((string)jPost.post_url) ? jPost.post_url : null;

            post.Caption = !string.IsNullOrEmpty((string)jPost.caption) ? jPost.caption : null;

            // post.caption = CommonHelper.NewLineToBreak(post.caption, "\n\r\n", string.Empty);

            post.Date = !string.IsNullOrEmpty((string)jPost.date) ? jPost.date : null;

            post.Format = !string.IsNullOrEmpty((string)jPost.format) ? jPost.format : null;

            post.ReblogKey = !string.IsNullOrEmpty((string)jPost.reblog_key) ? jPost.reblog_key : null;

            post.ShortUrl = !string.IsNullOrEmpty((string)jPost.short_url) ? jPost.short_url : null;

            post.NoteCount = !string.IsNullOrEmpty((string)jPost.note_count) ? jPost.note_count : null;

            post.SourceUrl = !string.IsNullOrEmpty((string)jPost.source_url) ? jPost.source_url : null;

            post.Slug = !string.IsNullOrEmpty((string)jPost.slug) ? jPost.slug : null;

            post.PostAuthor = !string.IsNullOrEmpty((string)jPost.post_author) ? jPost.post_author : null;

            post.State = !string.IsNullOrEmpty((string)jPost.state) ? jPost.state : null;

            post.Timestamp = !string.IsNullOrEmpty((string)jPost.timestamp) ? jPost.timestamp : null;

            if (jPost.tags != null && jPost.tags.Count > 0)
            {
                foreach (string tag in jPost.tags)
                {
                    post.Tags.Add(tag);
                }
            }
            else
            {
                post.Tags = null;
            }

            string datePatt = @"yyyy-MM-dd HH:mm:ss zzz";

            post.LastProcessedDate = DateTime.Now.ToString(datePatt);
        }
    }
}