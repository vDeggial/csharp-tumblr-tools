/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: June, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.IO;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Helpers
{
    public static class PostHelper
    {
        public static void GenerateAnswerPost(ref TumblrPost post, dynamic jPost)
        {
            post = new AnswerPost();
        }

        public static void GenerateAudioPost(ref TumblrPost post, dynamic jPost)
        {
            post = new AudioPost();
        }

        public static void GenerateBasePost(ref TumblrPost post, dynamic jPost)
        {
            post.type = !string.IsNullOrEmpty((string)jPost.type) ? jPost.type : null;

            post.id = !string.IsNullOrEmpty((string)jPost.id) ? jPost.id : null;

            post.url = !string.IsNullOrEmpty((string)jPost.post_url) ? jPost.post_url : null;

            post.caption = !string.IsNullOrEmpty((string)jPost.caption) ? jPost.caption : null;

            post.caption = CommonHelper.NewLineToBreak(post.caption, "\n\r\n", string.Empty);

            post.date = !string.IsNullOrEmpty((string)jPost.date) ? jPost.date : null;

            post.format = !string.IsNullOrEmpty((string)jPost.format) ? jPost.format : null;

            post.reblogKey = !string.IsNullOrEmpty((string)jPost.reblog_key) ? jPost.reblog_key : null;

            post.shortURL = !string.IsNullOrEmpty((string)jPost.short_url) ? jPost.short_url : null;

            post.noteCount = !string.IsNullOrEmpty((string)jPost.note_count) ? jPost.note_count : null;

            post.sourceURL = !string.IsNullOrEmpty((string)jPost.source_url) ? jPost.source_url : null;

            if (jPost.tags != null && jPost.tags.Count > 0)
            {
                foreach (string tag in jPost.tags)
                {
                    post.tags.Add(tag);
                }
            }
            else
            {
                post.tags = null;
            }

            string datePatt = @"yyyy-MM-dd HH:mm:ss zzz";

            post.lastProcessedDate = DateTime.Now.ToString(datePatt);
        }

        public static void GenerateChatPost(ref TumblrPost post, dynamic jPost)
        {
            post = new ChatPost();
        }

        public static void GenerateLinkPost(ref TumblrPost post, dynamic jPost)
        {
            post = new LinkPost();
        }

        public static void GeneratePhotoPost(ref TumblrPost post, dynamic jPost)
        {
            if (jPost.type == TumblrPostTypes.photo.ToString())
            {
                post = new PhotoPost();
                post.photos = new HashSet<PhotoPostImage>();

                foreach (dynamic jPhoto in jPost.photos)
                {
                    PhotoPostImage postImage = new PhotoPostImage();
                    postImage.url = jPhoto.original_size != null ? !string.IsNullOrEmpty((string)jPhoto.original_size.url) ? jPhoto.original_size.url : null : null;
                    postImage.filename = !string.IsNullOrEmpty(postImage.url) ? Path.GetFileName(postImage.url) : null;
                    postImage.width = jPhoto.original_size != null ? !string.IsNullOrEmpty((string)jPhoto.original_size.width) ? jPhoto.original_size.width : null : null;
                    postImage.height = jPhoto.original_size != null ? !string.IsNullOrEmpty((string)jPhoto.original_size.height) ? jPhoto.original_size.height : null : null;
                    postImage.caption = !string.IsNullOrEmpty((string)jPhoto.caption) ? jPhoto.caption : null;
                    postImage.parentPostID = !string.IsNullOrEmpty((string)jPost.id) ? jPost.id : null;
                    post.photos.Add(postImage);
                }
            }
        }

        public static void GenerateQuotePost(ref TumblrPost post, dynamic jPost)
        {
            post = new QuotePost();
        }

        public static void GenerateTextPost(ref TumblrPost post, dynamic jPost)
        {
            post = new TextPost();
        }

        public static void GenerateVideoPost(ref TumblrPost post, dynamic jPost)
        {
            post = new VideoPost();
        }
    }
}