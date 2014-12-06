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

using System;
using System.Collections.Generic;
using System.Linq;
using Tumblr_Tool.Common_Helpers;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Tumblr_Stats
{
    public class TumblrStats
    {
        public int answerPosts;
        public int audioPosts;
        public Tumblr blog;
        public int chatPosts;
        public int linkPosts;
        public int maxNumPosts = 0;
        public int parsed = 0;
        public int photoPosts;
        public int quotePosts;
        public processingCodes statusCode;
        public int textPosts;
        public int totalPosts;
        public int videoPosts;
        private string apiKey = "SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY";
        private string apiMode;
        private CrawlManager crawlManager;
        private int found = 0;
        private string jsonBlogInfoQuery = "info";
        private string jsonCompletePath = "";
        private string jsonPostQuery = "posts";
        private string jsonURL = "http://api.tumblr.com/v2/blog";
        private string query = @"/api/read?num=50";
        private int start;
        private int step = (int)postStepEnum.XML;
        private string tumblrDomain = "";
        private string url;

        public TumblrStats(Tumblr blog, int startNum = 0, int endNum = 0)
        {
            if (blog == null)
            {
                this.blog = new Tumblr();

                setBlogInfo();
                this.tumblrDomain = FileHelper.fixURL(blog.cname).Substring(7);
            }
            else
            {
                this.blog = blog;
                this.url = FileHelper.fixURL(blog.cname);
                this.tumblrDomain = this.url.Substring(7);
            }

            this.maxNumPosts = endNum;
            this.start = startNum;

            crawlManager = new CrawlManager();
            this.step = 50; //20 for JSON, 50 for XML
        }

        public List<TumblrPost> getTumblrPostList(int start = 0)
        {

            if (crawlManager.getMode() == apiModeEnum.JSON.ToString())
            {
                return getTumblrPostListJSON(start);
            }
            else
            {
                return getTumblrPostListXML(start);
            }
        }

        public List<TumblrPost> getTumblrPostListJSON(int start = 0)
        {
            string query = JSONHelper.getQueryString(tumblrDomain, tumblrPostTypes.empty.ToString(), start);

            crawlManager.getJSONDocument(@query);

            List<TumblrPost> posts = crawlManager.getPostList(tumblrPostTypes.empty.ToString(), apiModeEnum.JSON.ToString());
            this.statusCode = processingCodes.OK;
            return posts;
        }

        public List<TumblrPost> getTumblrPostListXML(int start = 0)
        {
            if (start != 0)
            {
                this.query += "&start=" + start.ToString();
            }

            if (maxNumPosts != 0)
            {
                this.query += "&end=" + maxNumPosts.ToString();
            }

            query = XMLHelper.getQueryString(url, tumblrPostTypes.empty.ToString(), start);

            if (crawlManager.isValidTumblr(query))
            {
                crawlManager.getXMLDocument(@query);
                if (totalPosts == 0)
                {
                    totalPosts = XMLHelper.getPostElementAttributeValue(crawlManager.xmlDocument, "posts", "total") != null ?
                        Convert.ToInt32(XMLHelper.getPostElementAttributeValue(crawlManager.xmlDocument, "posts", "total")) : 0;
                }

                List<TumblrPost> posts = crawlManager.getPostList(tumblrPostTypes.empty.ToString(), apiModeEnum.XML.ToString());
                this.statusCode = processingCodes.OK;
                return posts;
            }
            else if (!WebHelper.webURLExists(@query))
            {
                this.statusCode = processingCodes.UnableDownload;
                return null;
            }
            else
            {
                this.statusCode = processingCodes.invalidURL;
                return null;
            }
        }

        public Tumblr parsePosts()
        {
            string url = this.url + this.query;

            if (this.crawlManager.getMode() == apiModeEnum.JSON.ToString())
            {
                string query = string.Copy(this.jsonURL);

                query += "/" + tumblrDomain + "/" + jsonPostQuery;
                query += "?api_key=" + apiKey;

                jsonCompletePath = query;
                url = query;
                step = (int)postStepEnum.JSON;
            }

            if (crawlManager.isValidTumblr(url))
            {
                setBlogInfo();

                // crawlManager.getDocument(url);
                this.totalPosts = this.blog.totalPosts;

                int endCount;

                if (maxNumPosts != 0)
                    endCount = maxNumPosts;
                else
                    endCount = totalPosts;

                int i = start;

                string prefix = string.Empty;

                while (i < this.totalPosts)
                {
                    if (this.crawlManager.getMode() == apiModeEnum.JSON.ToString())
                    {
                        string query = string.Copy(this.jsonURL);

                        query += "/" + tumblrDomain + "/" + jsonPostQuery;
                        query += "?api_key=" + apiKey;

                        jsonCompletePath = query;
                        url = query;
                        url += "&offset=" + i.ToString();
                    }

                    crawlManager.getDocument(url);
                    this.blog.posts.AddRange(getTumblrPostList(i));
                    photoPosts = (from p in blog.posts where p.type == tumblrPostTypes.photo.ToString() select p).ToList().Count;
                    textPosts = (from p in blog.posts where p.type == tumblrPostTypes.regular.ToString() || p.type == tumblrPostTypes.text.ToString() select p).ToList().Count;
                    videoPosts = (from p in blog.posts where p.type == tumblrPostTypes.video.ToString() select p).ToList().Count;
                    linkPosts = (from p in blog.posts where p.type == tumblrPostTypes.link.ToString() select p).ToList().Count;
                    audioPosts = (from p in blog.posts where p.type == tumblrPostTypes.audio.ToString() select p).ToList().Count;
                    quotePosts = (from p in blog.posts where p.type == tumblrPostTypes.quote.ToString() select p).ToList().Count;
                    chatPosts = (from p in blog.posts where p.type == tumblrPostTypes.chat.ToString() || p.type == tumblrPostTypes.conversation.ToString() select p).ToList().Count;
                    answerPosts = (from p in blog.posts where p.type == tumblrPostTypes.answer.ToString() select p).ToList().Count;
                    this.parsed = this.blog.posts.Count;
                    i += step;
                }

                this.found = blog.posts.Count;

                this.statusCode = processingCodes.Done;
            }
            else
            {
                this.statusCode = processingCodes.invalidURL;
            }
            return this.blog;
        }

        public void setAPIMode(string mode)
        {
            this.apiMode = mode; // XML or JSON
            this.crawlManager.setMode(mode);
        }

        private void setBlogInfo()
        {
            if (crawlManager.getMode() == apiModeEnum.XML.ToString())
            {
                crawlManager.setBlogInfo(XMLHelper.getQueryString(url, tumblrPostTypes.empty.ToString(), 0, 1), this.blog);
            }
            else if (crawlManager.getMode() == apiModeEnum.JSON.ToString())
            {
                crawlManager.setBlogInfo(JSONHelper.getQueryString(tumblrDomain, tumblrPostTypes.empty.ToString(), 0, 1), this.blog);
            }
        }
    }
}