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
 *  Last Updated: September, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
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
        }

        public dynamic JsonDocument { get; set; }

        public string ApiMode { get; set; }

        public XDocument XmlDocument { get; set; }

        public ImageSizes ImageSize { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        public bool GetBlogInfo(string url, TumblrBlog blog)
        {
            try
            {
                if (ApiMode == ApiModeEnum.v2JSON.ToString())
                {
                    return GetJsonBlogInfo(url, blog);
                }
                else
                {
                    return GetXmlBlogInfo(url, blog);
                }
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
        public void GetDocument(string url)
        {
            try
            {
                if (ApiMode == ApiModeEnum.v1XML.ToString())
                    GetXmlDocument(url);
                else if (ApiMode == ApiModeEnum.v2JSON.ToString())
                    GetJsonDocument(url);
            }
            catch
            {
                JsonDocument = null;
                XmlDocument = null;
                return;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        public bool GetJsonBlogInfo(string url, TumblrBlog blog)
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
        public void GetJsonDocument(string url)
        {
            try
            {
                this.JsonDocument = JsonHelper.GetObject(url);

                if ((JsonDocument != null && this.JsonDocument.meta != null && this.JsonDocument.meta.status == ((int)TumblrAPIResponseEnum.OK).ToString()))
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
                return;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public HashSet<TumblrPost> GetPostListFromDoc(string type, string mode)
        {
            try
            {
                HashSet<TumblrPost> postList = new HashSet<TumblrPost>();

                if (mode == ApiModeEnum.v2JSON.ToString())
                {
                    postList = GetPostListFromJsonDoc(type);
                }
                else
                {
                    postList = GetPostListFromXmlDoc(type);
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
        /// <param name="type"></param>
        /// <returns></returns>
        public HashSet<TumblrPost> GetPostListFromJsonDoc(string type)
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

                        if (type == TumblrPostTypes.photo.ToString())
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
        /// <param name="type"></param>
        /// <returns></returns>
        public HashSet<TumblrPost> GetPostListFromXmlDoc(string type = "")
        {
            if (XmlDocument != null)
            {
                HashSet<TumblrPost> postList = new HashSet<TumblrPost>();
                HashSet<XElement> postElementList = XmlHelper.getPostElementList(XmlDocument);

                foreach (XElement element in postElementList)
                {
                    TumblrPost post = new TumblrPost();
                    if (type == TumblrPostTypes.photo.ToString())
                    {
                        post = new PhotoPost();
                    }

                    if (element.Attribute("id") != null)
                    {
                        post.Id = element.Attribute("id").Value;
                    }

                    if (element.Attribute("url") != null)
                    {
                        post.Url = element.Attribute("url").Value;
                    }

                    if (element.Element("photo-caption") != null)
                    {
                        post.Caption = element.Element("photo-caption").Value;
                    }

                    if (element.Attribute("format") != null)
                    {
                        post.Format = element.Attribute("format").Value;
                    }

                    if (element.Attribute("unix-timestamp") != null)
                    {
                        post.Date = element.Attribute("unix-timestamp").Value;
                    }

                    if (element.Elements("tag") != null)
                    {
                        foreach (string tag in element.Elements("tag").ToHashSet())
                        {
                            post.AddTag(tag);
                        }
                    }

                    if (element.Attribute("type") != null)
                    {
                        post.Type = element.Attribute("type").Value;
                    }

                    if (element.Attribute("reblog-key") != null)
                    {
                        post.ReblogKey = element.Attribute("reblog-key").Value;
                    }

                    // If it is PhotoSet
                    XElement photoset = element.Element("photoset");

                    if (photoset != null)
                    {
                        foreach (XElement setElement in photoset.Descendants("photo"))
                        {
                            XElement image = setElement.Descendants("photo-url").FirstOrDefault();
                            if (image != null)
                            {
                                PhotoPostImage setImage = new PhotoPostImage();
                                setImage.Url = image.Value;
                                setImage.Filename = !string.IsNullOrEmpty(setImage.Url) ? Path.GetFileName(setImage.Url) : null;

                                if (setElement.Attribute("photo-caption") != null)
                                {
                                    setImage.Caption = setElement.Attribute("photo-caption").Value;
                                }

                                if (setElement.Attribute("width") != null)
                                {
                                    setImage.Width = setElement.Attribute("width").Value;
                                }

                                if (setElement.Attribute("height") != null)
                                {
                                    setImage.Height = setElement.Attribute("height").Value;
                                }

                                post.Photos.Add(setImage);
                            }
                        }
                    }
                    else
                        if (element.Element("photo-url") != null)
                    {
                        PhotoPostImage photo = new PhotoPostImage();
                        photo.Url = (element.Element("photo-url").Value);
                        photo.Filename = Path.GetFileName(photo.Url);
                        post.Photos.Add(photo);
                    }
                    postList.Add(post);
                }

                return postList;
            }

            return null;
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
                else if (JsonDocument.response.blog.posts != null)
                    return Convert.ToInt32(JsonDocument.response.blog.posts);
            }
            else if (XmlDocument != null)
            {
                return XmlHelper.GetPostElementValue(XmlDocument, "posts") != null ? Convert.ToInt32(XmlHelper.GetPostElementAttributeValue(XmlDocument, "posts", "total")) : 0;
            }

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        public bool GetXmlBlogInfo(string url, TumblrBlog blog)
        {
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    if (blog == null) blog = new TumblrBlog(url);
                    XDocument rDoc = XmlHelper.GetDocument(url);
                    if (rDoc != null)
                    {
                        blog.Title = XmlHelper.GetPostElementAttributeValue(rDoc, "tumblelog", "title");
                        blog.Description = XmlHelper.GetPostElementValue(rDoc, "tumblelog");
                        blog.Timezone = XmlHelper.GetPostElementAttributeValue(rDoc, "tumblelog", "timezone");
                        blog.Name = XmlHelper.GetPostElementAttributeValue(rDoc, "tumblelog", "name");
                        blog.TotalPosts = XmlHelper.GetPostElementValue(rDoc, "posts") != null ? Convert.ToInt32(XmlHelper.GetPostElementAttributeValue(rDoc, "posts", "total")) : 0;
                        return true;
                    }

                    return false;
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
        public void GetXmlDocument(string url)
        {
            try
            {
                XmlDocument = XmlHelper.GetDocument(url);
            }
            catch
            {
                XmlDocument = null;
                return;
            }
        }
    }
}