/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Class: DocumentManager
 *
 *  Description: Class provides functionality for acquiring and processing JSON/XML documents from Tumblr API
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: November, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Managers
{
    public class DocumentManager
    {
        /// <summary>
        ///
        /// </summary>
        public DocumentManager()
        {
            ApiMode = ApiModes.V2Json;
            ImageSize = ImageSizes.Original;
            JsonDocument = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiMode"></param>
        public DocumentManager(ApiModes apiMode)
        {
            ApiMode = apiMode;
            ImageSize = ImageSizes.Original;
            JsonDocument = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiMode"></param>
        /// <param name="imageSize"></param>
        public DocumentManager(ApiModes apiMode, ImageSizes imageSize)
        {
            ApiMode = apiMode;
            ImageSize = imageSize;
            JsonDocument = null;
        }

        public ApiModes ApiMode { get; set; }
        public ImageSizes ImageSize { get; set; }
        public dynamic JsonDocument { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tumblrPostType"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public HashSet<TumblrPost> GetPostListFromDoc(string tumblrPostType, ApiModes mode)
        {
            try
            {
                var postList = GetPostListFromJsonDoc(tumblrPostType);

                return postList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        public bool GetRemoteBlogInfo(string url, TumblrBlog blog)
        {
            try
            {
                return GetRemoteBlogInfoJson(url, blog);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        public void GetRemoteDocument(string url)
        {
            try
            {
                GetRemoteJsonDocument(url);
            }
            catch
            {
                JsonDocument = null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int GetTotalPostCount()
        {
            if (JsonDocument != null && JsonDocument.response != null && JsonDocument.response.blog != null)
            {
                if (JsonDocument.response.total_posts != null)
                    return Convert.ToInt32(JsonDocument.response.total_posts);
                if (JsonDocument.response.blog.posts != null)
                    return Convert.ToInt32(JsonDocument.response.blog.posts);
            }

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tumblrPostType"></param>
        /// <returns></returns>
        private HashSet<TumblrPost> GetPostListFromJsonDoc(string tumblrPostType)
        {
            try
            {
                HashSet<TumblrPost> postList = new HashSet<TumblrPost>();

                if (JsonDocument != null && JsonDocument.response != null && JsonDocument.response.posts != null)
                {
                    JArray jPostArray = JsonDocument.response.posts;
                    HashSet<dynamic> jPostList = jPostArray.ToObject<HashSet<dynamic>>();

                    foreach (dynamic jPost in jPostList)
                    {
                        TumblrPost post = new TumblrPost();

                        if (tumblrPostType == TumblrPostTypes.Photo.ToString().ToLower())
                        {
                            PostHelper.GeneratePhotoPost(ref post, jPost, ImageSize);
                        }

                        PostHelper.IncludeCommonPostFields(ref post, jPost);

                        postList.Add(post);
                    }
                }

                return postList;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        private bool GetRemoteBlogInfoJson(string url, TumblrBlog blog)
        {
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    if (blog == null) blog = new TumblrBlog(url);
                    dynamic jsonDocument = JsonHelper.GetObject(url);

                    if (jsonDocument != null && jsonDocument.response != null && jsonDocument.response.blog != null)
                    {
                        blog.Title = jsonDocument.response.blog.title;
                        blog.Description = jsonDocument.response.blog.description;
                        blog.Name = jsonDocument.response.blog.name;
                        blog.Url = jsonDocument.response.blog.url;
                        blog.Nsfw = Convert.ToBoolean(jsonDocument.response.blog.is_nsfw);
                        blog.AskEnabled = Convert.ToBoolean(jsonDocument.response.blog.ask);
                        blog.AnonAskEnabled = Convert.ToBoolean(jsonDocument.response.blog.ask_anon);
                        blog.LastUpdated = CommonHelper.UnixTimeStampToDateTime(Convert.ToDouble(jsonDocument.response.blog.updated));

                        if (jsonDocument.response.total_posts != null)
                            blog.TotalPosts = Convert.ToInt32(jsonDocument.response.total_posts);
                        else if (jsonDocument.response.blog.posts != null)
                            blog.TotalPosts = Convert.ToInt32(jsonDocument.response.blog.posts);

                        if (jsonDocument.response.blog.posts != null)
                            blog.BlogTotalPosts = Convert.ToInt32(jsonDocument.response.blog.posts);

                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        private void GetRemoteJsonDocument(string url)
        {
            try
            {
                JsonDocument = JsonHelper.GetObject(url);

                if ((JsonDocument != null && JsonDocument.meta != null && JsonDocument.meta.status == ((int)TumblrApiResponses.Ok).ToString()))
                {
                }
                else
                {
                    JsonDocument = null;
                }
            }
            catch
            {
                JsonDocument = null;
            }
        }
    }
}