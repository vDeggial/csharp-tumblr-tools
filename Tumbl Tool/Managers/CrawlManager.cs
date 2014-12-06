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
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Tumblr_Tool.Common_Helpers;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Managers
{
    public class CrawlManager
    {
        public dynamic jsonDocument;
        public XDocument xmlDocument;
        private string mode;

        public CrawlManager()
        {
        }

        public void getDocument(string url)
        {
            if (this.mode == "XML")
                getXMLDocument(url);
            else if (mode == "JSON")
                getJSONDocument(url);
        }

        public void getJSONDocument(string url)
        {
            jsonDocument = JSONHelper.getJSONObject(url);
        }

        public string getMode()
        {
            return this.mode;
        }

        public List<TumblrPost> getPostList(string type, string mode)
        {
            List<TumblrPost> postList = new List<TumblrPost>();

            if (mode == apiModeEnum.JSON.ToString())
            {
                postList = getPostListJSON(type);
            }
            else
            {
                postList = getPostListXML(type);
            }

            return postList;
        }

        public List<TumblrPost> getPostListJSON(string type)
        {
            List<TumblrPost> postList = new List<TumblrPost>();

            if (jsonDocument != null && jsonDocument.response != null && jsonDocument.response.posts != null)
            {
                JArray jPostArray = jsonDocument.response.posts;
                List<dynamic> jPostList = jPostArray.ToObject<List<dynamic>>();

                foreach (dynamic jPost in jPostList)
                {
                    TumblrPost post = new TumblrPost();
                    if (type == tumblrPostTypes.photo.ToString())
                    {
                        post = new PhotoPost();
                    }

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

                    if (jPost.tags != null)
                    {
                        foreach (string tag in jPost.tags)
                        {
                            post.addTag(tag);
                        }
                    }

                    if (jPost.type == tumblrPostTypes.photo.ToString())
                    {
                        if (jPost.photos.Count == 1)
                        {
                            post.imageURL = jPost.photos[0].original_size.url;
                            post.fileName = Path.GetFileName(post.imageURL);
                        }
                        else
                        {
                            foreach (dynamic jPhoto in jPost.photos)
                            {
                                PhotoSetImage setImage = new PhotoSetImage();
                                setImage.imageURL = jPhoto.original_size.url;
                                setImage.filename = !string.IsNullOrEmpty(setImage.imageURL) ? Path.GetFileName(setImage.imageURL) : null;

                                post.addImageToPhotoSet(setImage);
                            }
                        }
                    }

                    postList.Add(post);
                }
            }

            // xmlDocument = JSONHelper.jsonToXML(jsonDocument);

            return postList;
        }

        public List<TumblrPost> getPostListXML(string type = "")
        {
            List<TumblrPost> postList = new List<TumblrPost>();
            List<XElement> postElementList = XMLHelper.getPostElementList(xmlDocument);

            foreach (XElement element in postElementList)
            {
                TumblrPost post = new TumblrPost();
                if (type == tumblrPostTypes.photo.ToString())
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
                    foreach (string tag in element.Elements("tag").ToList())
                    {
                        post.addTag(tag);
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

                if (element.Element("photo-url") != null)
                {
                    post.imageURL = (element.Element("photo-url").Value);
                    post.fileName = Path.GetFileName(post.imageURL);
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
                            PhotoSetImage setImage = new PhotoSetImage();
                            setImage.imageURL = image.Value;
                            setImage.filename = !string.IsNullOrEmpty(setImage.imageURL) ? Path.GetFileName(setImage.imageURL) : null;

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

                            if (setElement.Attribute("offset") != null)
                            {
                                setImage.offset = setElement.Attribute("offset").Value;
                            }

                            post.addImageToPhotoSet(setImage);
                        }
                    }
                }
                postList.Add(post);
            }

            return postList;
        }

        public void getXMLDocument(string url)
        {
            xmlDocument = XMLHelper.getXMLDocument(url);
        }

        public bool isValidTumblr(string url)
        {
            if (mode == "JSON")
                return JSONHelper.getJSONObject(url) != null;
            else
                return XMLHelper.getXMLDocument(url) != null;
        }

        public bool setBlogInfo(string url, Tumblr blog)
        {
            if (mode == apiModeEnum.JSON.ToString())
            {
                return setBlogInfoJSON(url, blog);
            }
            else
            {
                return setBlogInfoXML(url, blog);
            }
        }

        public bool setBlogInfoJSON(string url, Tumblr blog)
        {
            dynamic jsonDocument = JSONHelper.getJSONObject(url);

            if (jsonDocument != null)
            {
                blog.title = jsonDocument.response.blog.title;
                blog.description = jsonDocument.response.blog.description;
                blog.timezone = "";
                blog.name = jsonDocument.response.blog.name;

                if (jsonDocument.response.total_posts != null)
                    blog.totalPosts = Convert.ToInt32(jsonDocument.response.total_posts);
                else if (jsonDocument.response.blog.posts != null)
                    blog.totalPosts = Convert.ToInt32(jsonDocument.response.blog.posts);

                return true;
            }

            return false;
        }

        public bool setBlogInfoXML(string url, Tumblr blog)
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

        public void setMode(string mode)
        {
            this.mode = mode;
        }
    }
}