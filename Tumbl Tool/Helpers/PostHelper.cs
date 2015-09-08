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
            post = new AnswerPost();
            post.Asker = !string.IsNullOrEmpty((string)jPost.asking_name) ? jPost.asking_name : null;
            post.AskerUrl = !string.IsNullOrEmpty((string)jPost.asking_url) ? jPost.asking_url : null;
            post.Question = !string.IsNullOrEmpty((string)jPost.question) ? jPost.question : null;
            post.Answer = !string.IsNullOrEmpty((string)jPost.answer) ? jPost.answer : null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateAudioPost(ref TumblrPost post, dynamic jPost)
        {
            post = new AudioPost();
            post.Artist = !string.IsNullOrEmpty((string)jPost.artist) ? jPost.artist : null;
            post.Album = !string.IsNullOrEmpty((string)jPost.album) ? jPost.album : null;
            post.AlbumArt = !string.IsNullOrEmpty((string)jPost.album_art) ? jPost.album_art : null;
            post.TrackName = !string.IsNullOrEmpty((string)jPost.track_name) ? jPost.track_name : null;
            post.Caption = !string.IsNullOrEmpty((string)jPost.caption) ? jPost.caption : null;
            post.AudioType = !string.IsNullOrEmpty((string)jPost.audio_type) ? jPost.audio_type : null;
            post.AudioUrl = !string.IsNullOrEmpty((string)jPost.audio_url) ? jPost.audio_url : null;
            post.PlaysCount = !string.IsNullOrEmpty((string)jPost.plays) ? jPost.plays : null;
            post.Player = !string.IsNullOrEmpty((string)jPost.player) ? jPost.player : null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateChatPost(ref TumblrPost post, dynamic jPost)
        {
            post = new ChatPost();
            post.Title = !string.IsNullOrEmpty((string)jPost.title) ? jPost.title : null;
            post.Body = !string.IsNullOrEmpty((string)jPost.body) ? jPost.body : null;

            if (jPost.dialogue != null)
            {
                post.Dialogue = new HashSet<ChatPostFragment>();
                foreach (dynamic jChatFragment in jPost.dialogue)
                {
                    ChatPostFragment chatFragment = new ChatPostFragment();
                    chatFragment.Name = !string.IsNullOrEmpty((string)jChatFragment.name) ? jChatFragment.name : null;
                    chatFragment.Label = !string.IsNullOrEmpty((string)jChatFragment.label) ? jChatFragment.label : null;
                    chatFragment.Phrase = !string.IsNullOrEmpty((string)jChatFragment.phrase) ? jChatFragment.phrase : null;
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
            post = new LinkPost();
            post.LinkAuthor = !string.IsNullOrEmpty((string)jPost.link_author) ? jPost.link_author : null;
            post.LinkImage = !string.IsNullOrEmpty((string)jPost.link_image) ? jPost.link_image : null;
            post.LinkUrl = !string.IsNullOrEmpty((string)jPost.url) ? jPost.url : null;
            post.Title = !string.IsNullOrEmpty((string)jPost.title) ? jPost.title : null;
            post.Description = !string.IsNullOrEmpty((string)jPost.description) ? jPost.description : null;
            post.Excerpt = !string.IsNullOrEmpty((string)jPost.excerpt) ? jPost.excerpt : null;
            post.Publisher = !string.IsNullOrEmpty((string)jPost.publisher) ? jPost.publisher : null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GeneratePhotoPost(ref TumblrPost post, dynamic jPost, ImageSizes imageSize)
        {
            if (jPost.type == TumblrPostTypes.photo.ToString())
            {
                post = new PhotoPost();
                post.Photos = new HashSet<PhotoPostImage>();

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
                        dynamic jPhotoAlt;
                        if (jPhoto.alt_sizes != null)
                        {
                            var jAltPhotos = new HashSet<dynamic>(jPhoto.alt_sizes);
                            jPhotoAlt = (from p in jAltPhotos where p.width == ((int)imageSize).ToString() select p).FirstOrDefault();
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
                    postImage.ParentPostID = !string.IsNullOrEmpty((string)jPost.id) ? jPost.id : null;
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
            post = new QuotePost();
            post.Text = !string.IsNullOrEmpty((string)jPost.text) ? jPost.text : null;
            post.Source = !string.IsNullOrEmpty((string)jPost.source) ? jPost.source : null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateTextPost(ref TumblrPost post, dynamic jPost)
        {
            post = new TextPost();
            post.Title = !string.IsNullOrEmpty((string)jPost.title) ? jPost.title : null;
            post.Body = !string.IsNullOrEmpty((string)jPost.body) ? jPost.body : null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="post"></param>
        /// <param name="jPost"></param>
        public static void GenerateVideoPost(ref TumblrPost post, dynamic jPost)
        {
            post = new VideoPost();
            post.Caption = !string.IsNullOrEmpty((string)jPost.caption) ? jPost.caption : null;
            post.VideoType = !string.IsNullOrEmpty((string)jPost.video_type) ? jPost.video_type : null;
            post.VideoUrl = !string.IsNullOrEmpty((string)jPost.permalink_url) ? jPost.permalink_url : null;
            post.ThumbnailUrl = !string.IsNullOrEmpty((string)jPost.thumbnail_url) ? jPost.thumbnail_url : null;
            post.ThumbnailWidth = !string.IsNullOrEmpty((string)jPost.thumbnail_width) ? jPost.thumbnail_width : null;
            post.ThumbnailHeight = !string.IsNullOrEmpty((string)jPost.thumbnail_height) ? jPost.thumbnail_height : null;
            post.IsHtml5Capable = !string.IsNullOrEmpty((string)jPost.html5_capable) ? jPost.html5_capable : null;

            if (jPost.player != null)
            {
                post.VideoPlayers = new HashSet<VideoPostEmbedPlayer>();
                foreach (dynamic jPlayer in jPost.player)
                {
                    VideoPostEmbedPlayer player = new VideoPostEmbedPlayer();
                    player.EmbedCode = !string.IsNullOrEmpty((string)jPlayer.embed_code) ? jPlayer.embed_code : null;
                    player.Width = !string.IsNullOrEmpty((string)jPlayer.width) ? jPlayer.width : null;
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

            post.ShortURL = !string.IsNullOrEmpty((string)jPost.short_url) ? jPost.short_url : null;

            post.NoteCount = !string.IsNullOrEmpty((string)jPost.note_count) ? jPost.note_count : null;

            post.SourceURL = !string.IsNullOrEmpty((string)jPost.source_url) ? jPost.source_url : null;

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