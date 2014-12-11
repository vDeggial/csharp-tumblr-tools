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
        public string mode;
        public XDocument xmlDocument;

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

        public HashSet<TumblrPost> getPostList(string type, string mode)
        {
            HashSet<TumblrPost> postList = new HashSet<TumblrPost>();

            if (mode == apiModeEnum.JSON.ToString())
            {
                postList = getPostListJSON(type);
            }

            return postList;
        }

        public HashSet<TumblrPost> getPostListJSON(string type)
        {
            HashSet<TumblrPost> postList = new HashSet<TumblrPost>();

            if (jsonDocument != null && jsonDocument.response != null && jsonDocument.response.posts != null)
            {
                JArray jPostArray = jsonDocument.response.posts;
                HashSet<dynamic> jPostList = jPostArray.ToObject<HashSet<dynamic>>();

                foreach (dynamic jPost in jPostList)
                {
                    TumblrPost post = new TumblrPost();


                    if (type == tumblrPostTypes.photo.ToString())
                    {
                        PostHelper.createPhotoPost(ref post, jPost);
                    }

                    PostHelper.createTumblrPost(ref post, jPost);

                    

                    postList.Add(post);
                }
            }

            // xmlDocument = JSONHelper.jsonToXML(jsonDocument);

            return postList;
        }



        public void getXMLDocument(string url)
        {
            xmlDocument = XMLHelper.getXMLDocument(url);
        }

        public bool isValidTumblr(string url)
        {
            if (mode == apiModeEnum.JSON.ToString())
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
    }
}