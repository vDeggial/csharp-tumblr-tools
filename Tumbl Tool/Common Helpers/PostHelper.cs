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

using System.Collections.Generic;
using System.IO;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Common_Helpers
{
    public static class PostHelper
    {
        public static void generateAnswerPost(ref TumblrPost post, dynamic jPost)
        {
            post = new AnswerPost();
        }

        public static void generateAudioPost(ref TumblrPost post, dynamic jPost)
        {
            post = new AudioPost();
        }

        public static void generateBasePost(ref TumblrPost post, dynamic jPost)
        {
            if (jPost.type != null)
                post.type = jPost.type;

            if (jPost.id != null)
                post.id = jPost.id;

            if (jPost.post_url != null)
                post.url = jPost.post_url;

            if (jPost.caption != null)
                post.caption = jPost.caption;

            if (jPost.date != null)
                post.date = jPost.date;

            if (jPost.format != null)
                post.format = jPost.format;

            if (jPost.reblog_key != null)
                post.reblogKey = jPost.reblog_key;

            if (jPost.short_url != null)
            {
                post.shortURL = jPost.short_url;
            }

            if (jPost.tags != null)
            {
                foreach (string tag in jPost.tags)
                {
                    post.tags.Add(tag);
                }
            }
        }

        public static void generateChatPost(ref TumblrPost post, dynamic jPost)
        {
            post = new ChatPost();
        }

        public static void generateLinkPost(ref TumblrPost post, dynamic jPost)
        {
            post = new LinkPost();
        }

        public static void generatePhotoPost(ref TumblrPost post, dynamic jPost)
        {
            if (jPost.type == tumblrPostTypes.photo.ToString())
            {
                post = new PhotoPost();
                post.photos = new HashSet<PhotoPostImage>();

                foreach (dynamic jPhoto in jPost.photos)
                {
                    PhotoPostImage postImage = new PhotoPostImage();
                    postImage.url = jPhoto.original_size.url;
                    postImage.filename = !string.IsNullOrEmpty(postImage.url) ? Path.GetFileName(postImage.url) : null;
                    postImage.width = jPhoto.original_size != null ? jPhoto.original_size.width : null;
                    postImage.height = jPhoto.original_size != null ? jPhoto.original_size.height : null;
                    postImage.caption = jPhoto.caption != null && !string.IsNullOrEmpty(jPhoto.caption as string) ? jPhoto.caption : null;

                    post.photos.Add(postImage);
                }
            }
        }

        public static void generateQuotePost(ref TumblrPost post, dynamic jPost)
        {
            post = new QuotePost();
        }

        public static void generateTextPost(ref TumblrPost post, dynamic jPost)
        {
            post = new TextPost();
        }

        public static void generateVideoPost(ref TumblrPost post, dynamic jPost)
        {
            post = new VideoPost();
        }
    }
}