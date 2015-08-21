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
 *  Last Updated: August, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Managers
{
    public class DocumentManager
    {
        public DocumentManager()
        {
        }

        public dynamic jsonDocument { get; set; }

        public string mode { get; set; }

        public XDocument xmlDocument { get; set; }

        public void GetDocument(string url)
        {
            try
            {
                if (this.mode == ApiModeEnum.v1XML.ToString())
                    GetDocumentXML(url);
                else if (this.mode == ApiModeEnum.v2JSON.ToString())
                    GetDocumentJSON(url);
            }
            catch
            {
                this.jsonDocument = null;
                this.xmlDocument = null;
                return;
            }
        }

        public void GetDocumentJSON(string url)
        {
            try
            {
                this.jsonDocument = JsonHelper.GetObject(url);

                if ((this.jsonDocument != null && this.jsonDocument.meta != null && this.jsonDocument.meta.status == ((int)TumblrAPIResponseEnum.OK).ToString()))
                {
                }
                else
                {
                    this.jsonDocument = null;
                }
            }
            catch
            {
                this.jsonDocument = null;
                return;
            }
        }

        public void GetDocumentXML(string url)
        {
            try
            {
                this.xmlDocument = XmlHelper.GetDocument(url);
            }
            catch
            {
                this.xmlDocument = null;
                return;
            }
        }

        public HashSet<TumblrPost> GetPostList(string type, string mode)
        {
            try
            {
                HashSet<TumblrPost> postList = new HashSet<TumblrPost>();

                if (mode == ApiModeEnum.v2JSON.ToString())
                {
                    postList = GetPostListJSON(type);
                }
                else
                {
                    postList = GetPostListXML(type);
                }

                return postList;
            }
            catch
            {
                return null;
            }
        }

        public HashSet<TumblrPost> GetPostListXML(string type = "")
        {
            HashSet<TumblrPost> postList = new HashSet<TumblrPost>();
            HashSet<XElement> postElementList = XmlHelper.getPostElementList(xmlDocument);

            foreach (XElement element in postElementList)
            {
                TumblrPost post = new TumblrPost();
                if (type == TumblrPostTypes.photo.ToString())
                {
                    post = new PhotoPost();
                }

                if (element.Attribute("id") != null)
                {
                    post.id = element.Attribute("id").Value;
                }

                if (element.Attribute("url") != null)
                {
                    post.url = element.Attribute("url").Value;
                }

                if (element.Element("photo-caption") != null)
                {
                    post.caption = element.Element("photo-caption").Value;
                }

                if (element.Attribute("format") != null)
                {
                    post.format = element.Attribute("format").Value;
                }

                if (element.Attribute("unix-timestamp") != null)
                {
                    post.date = element.Attribute("unix-timestamp").Value;
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
                    post.type = element.Attribute("type").Value;
                }

                if (element.Attribute("reblog-key") != null)
                {
                    post.reblogKey = element.Attribute("reblog-key").Value;
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
                            setImage.url = image.Value;
                            setImage.filename = !string.IsNullOrEmpty(setImage.url) ? Path.GetFileName(setImage.url) : null;

                            if (setElement.Attribute("photo-caption") != null)
                            {
                                setImage.caption = setElement.Attribute("photo-caption").Value;
                            }

                            if (setElement.Attribute("width") != null)
                            {
                                setImage.width = setElement.Attribute("width").Value;
                            }

                            if (setElement.Attribute("height") != null)
                            {
                                setImage.height = setElement.Attribute("height").Value;
                            }

                            post.photos.Add(setImage);
                        }
                    }
                }
                else
                    if (element.Element("photo-url") != null)
                {
                    PhotoPostImage photo = new PhotoPostImage();
                    photo.url = (element.Element("photo-url").Value);
                    photo.filename = Path.GetFileName(photo.url);
                    post.photos.Add(photo);
                }
                postList.Add(post);
            }

            return postList;
        }

        public HashSet<TumblrPost> GetPostListJSON(string type)
        {
            try
            {
                HashSet<TumblrPost> postList = new HashSet<TumblrPost>();

                if (this.jsonDocument != null && this.jsonDocument.response != null && this.jsonDocument.response.posts != null)
                {
                    JArray jPostArray = jsonDocument.response.posts;
                    HashSet<dynamic> jPostList = jPostArray.ToObject<HashSet<dynamic>>();

                    foreach (dynamic jPost in jPostList)
                    {
                        TumblrPost post = new TumblrPost();

                        if (type == TumblrPostTypes.photo.ToString())
                        {
                            PostHelper.GeneratePhotoPost(ref post, jPost);
                        }

                        PostHelper.GenerateBasePost(ref post, jPost);

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

        public bool GetBlogInfo(string url, TumblrBlog blog)
        {
            try
            {
                if (this.mode == ApiModeEnum.v2JSON.ToString())
                {
                    return GetBlogInfoJSON(url, blog);
                }
                else
                {
                    return GetBlogInfoXML(url, blog);
                }
            }
            catch
            {
                return false;
            }
        }

        public int GetTotalPostCount()
        {
            if (jsonDocument != null && jsonDocument.response != null && jsonDocument.response.blog != null)
            {
                if (jsonDocument.response.total_posts != null)
                    return Convert.ToInt32(jsonDocument.response.total_posts);
                else if (jsonDocument.response.blog.posts != null)
                    return Convert.ToInt32(jsonDocument.response.blog.posts);
            }
            else if (xmlDocument != null)
            {
                return XmlHelper.GetPostElementValue(xmlDocument, "posts") != null ? Convert.ToInt32(XmlHelper.GetPostElementAttributeValue(xmlDocument, "posts", "total")) : 0;
            }

            return 0;
        }

        public bool GetBlogInfoJSON(string url, TumblrBlog blog)
        {
            try
            {
                if (blog == null) blog = new TumblrBlog(url);
                dynamic jsonDocument = JsonHelper.GetObject(url);

                if (jsonDocument != null && jsonDocument.response != null && jsonDocument.response.blog != null)
                {
                    blog.title = jsonDocument.response.blog.title;
                    blog.description = jsonDocument.response.blog.description;
                    blog.name = jsonDocument.response.blog.name;
                    blog.url = jsonDocument.response.blog.url;
                    blog.isNsfw = Convert.ToBoolean(jsonDocument.response.blog.is_nsfw);
                    blog.isAskEnabled = Convert.ToBoolean(jsonDocument.response.blog.ask);
                    blog.isAnonAskEnabled = Convert.ToBoolean(jsonDocument.response.blog.ask_anon);
                    blog.lastUpdated = CommonHelper.UnixTimeStampToDateTime(Convert.ToDouble(jsonDocument.response.blog.updated));

                    if (jsonDocument.response.total_posts != null)
                        blog.totalPosts = Convert.ToInt32(jsonDocument.response.total_posts);
                    else if (jsonDocument.response.blog.posts != null)
                        blog.totalPosts = Convert.ToInt32(jsonDocument.response.blog.posts);

                    if (jsonDocument.response.blog.posts != null)
                        blog.blogTotalPosts = Convert.ToInt32(jsonDocument.response.blog.posts);

                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public bool GetBlogInfoXML(string url, TumblrBlog blog)
        {
            try
            {
                XDocument rDoc = XmlHelper.GetDocument(url);
                if (rDoc != null)
                {
                    blog.title = XmlHelper.GetPostElementAttributeValue(rDoc, "tumblelog", "title");
                    blog.description = XmlHelper.GetPostElementValue(rDoc, "tumblelog");
                    blog.timezone = XmlHelper.GetPostElementAttributeValue(rDoc, "tumblelog", "timezone");
                    blog.name = XmlHelper.GetPostElementAttributeValue(rDoc, "tumblelog", "name");
                    blog.totalPosts = XmlHelper.GetPostElementValue(rDoc, "posts") != null ? Convert.ToInt32(XmlHelper.GetPostElementAttributeValue(rDoc, "posts", "total")) : 0;
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}