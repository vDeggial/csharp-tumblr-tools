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

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Tumblr_Tool.Common_Helpers;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Managers
{
    public class CrawlManager
    {
        public CrawlManager()
        {
        }

        public dynamic jsonDocument { get; set; }
        public string mode { get; set; }
        public XDocument xmlDocument { get; set; }
        public void getDocument(string url)
        {
            try
            {
                if (this.mode == "XML")
                    getXMLDocument(url);
                else if (this.mode == "JSON")
                    getJSONDocument(url);
            }
            catch
            {
                this.jsonDocument = null;
                this.xmlDocument = null;
                return;
            }
        }

        public void getJSONDocument(string url)
        {
            try
            {
                this.jsonDocument = JSONHelper.getJSONObject(url);

                 if ((this.jsonDocument != null && this.jsonDocument.meta != null && this.jsonDocument.meta.status == ((int)tumblrAPIResponseEnum.OK).ToString()))
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

        public HashSet<TumblrPost> getPostList(string type, string mode)
        {
            try
            {
                HashSet<TumblrPost> postList = new HashSet<TumblrPost>();

                if (mode == apiModeEnum.JSON.ToString())
                {
                    postList = getPostListJSON(type);
                }

                return postList;
            }
            catch
            {
                return null;
            }
        }

        public HashSet<TumblrPost> getPostListJSON(string type)
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

                        if (type == tumblrPostTypes.photo.ToString())
                        {
                            PostHelper.generatePhotoPost(ref post, jPost);
                        }

                        PostHelper.generateBasePost(ref post, jPost);

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

        public void getXMLDocument(string url)
        {
            try
            {
                this.xmlDocument = XMLHelper.getXMLDocument(url);
            }
            catch
            {
                this.xmlDocument = null;
                return;
            }
        }

        public bool isValidTumblr(string url)
        {
            try
            {
                if (this.mode == apiModeEnum.JSON.ToString())
                {
                    dynamic jsonObject = JSONHelper.getJSONObject(url);
                    return (jsonObject != null && jsonObject.meta != null && jsonObject.meta.status == ((int)tumblrAPIResponseEnum.OK).ToString());
                }
                else
                    return XMLHelper.getXMLDocument(url) != null;
            }
            catch
            {
                return false;
            }
        }

        public bool setBlogInfo(string url, TumblrBlog blog)
        {
            try
            {
                if (this.mode == apiModeEnum.JSON.ToString())
                {
                    return setBlogInfoJSON(url, blog);
                }
                else
                {
                    return setBlogInfoXML(url, blog);
                }
            }
            catch
            {
                return false;
            }
        }

        public bool setBlogInfoJSON(string url, TumblrBlog blog)
        {
            try
            {
                dynamic jsonDocument = JSONHelper.getJSONObject(url);

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

        public bool setBlogInfoXML(string url, TumblrBlog blog)
        {
            try
            {
                XDocument rDoc = XMLHelper.getXMLDocument(url);
                if (rDoc != null)
                {
                    blog.title = XMLHelper.getPostElementAttributeValue(rDoc, "tumblelog", "title");
                    blog.description = XMLHelper.getPostElementValue(rDoc, "tumblelog");
                    blog.timezone = XMLHelper.getPostElementAttributeValue(rDoc, "tumblelog", "timezone");
                    blog.name = XMLHelper.getPostElementAttributeValue(rDoc, "tumblelog", "name");
                    blog.totalPosts = XMLHelper.getPostElementValue(rDoc, "posts") != null ? Convert.ToInt32(XMLHelper.getPostElementAttributeValue(rDoc, "posts", "total")) : 0;
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