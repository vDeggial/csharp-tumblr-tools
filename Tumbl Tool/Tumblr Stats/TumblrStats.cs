using System;
using System.Collections.Generic;
using System.Linq;
using Tumbl_Tool.Common_Helpers;
using Tumbl_Tool.Enums;
using Tumbl_Tool.Managers;
using Tumbl_Tool.Tumblr_Objects;

namespace Tumbl_Tool.Tumblr_Stats
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
        public postProcessingCodes statusCode;
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
                this.tumblrDomain = blog.cname.Substring(7);
            }
            else
            {
                this.blog = blog;
                this.url = FileHelper.fixURL(blog.cname);
                this.tumblrDomain = blog.cname.Substring(7);
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
            string query = string.Copy(this.jsonURL);

            query += "/" + tumblrDomain + jsonPostQuery;
            query += "?api_key=" + apiKey;
            query += "&offset=" + start.ToString();

            jsonCompletePath = query;

            crawlManager.getJSONDocument(@query);

            List<TumblrPost> posts = crawlManager.getPostList("", apiModeEnum.JSON.ToString());
            this.statusCode = postProcessingCodes.OK;
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

            if (crawlManager.isValidTumblr(url + query))
            {
                crawlManager.getXMLDocument(@url + query);
                if (totalPosts == 0)
                {
                    totalPosts = XMLHelper.getPostElementAttributeValue(crawlManager.xmlDocument, "posts", "total") != null ?
                        Convert.ToInt32(XMLHelper.getPostElementAttributeValue(crawlManager.xmlDocument, "posts", "total")) : 0;
                }

                List<TumblrPost> posts = crawlManager.getPostList("", apiModeEnum.XML.ToString());
                this.statusCode = postProcessingCodes.OK;
                return posts;
            }
            else if (!WebHelper.webURLExists(@url + query))
            {
                this.statusCode = postProcessingCodes.UnableDownload;
                return null;
            }
            else
            {
                this.statusCode = postProcessingCodes.invalidURL;
                return null;
            }
        }

        public Tumblr parsePosts()
        {
            string url = this.url + this.query;

            if (this.crawlManager.getMode() == apiModeEnum.JSON.ToString())
            {
                string query = string.Copy(this.jsonURL);

                query += "/" + tumblrDomain + jsonPostQuery;
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

                        query += "/" + tumblrDomain + jsonPostQuery;
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

                this.statusCode = postProcessingCodes.Done;
            }
            else
            {
                this.statusCode = postProcessingCodes.invalidURL;
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
                crawlManager.setBlogInfo(url + query, this.blog);
            }
            else if (crawlManager.getMode() == apiModeEnum.JSON.ToString())
            {
                string query = string.Copy(this.jsonURL);

                query += "/" + tumblrDomain + jsonBlogInfoQuery;
                query += "?api_key=" + apiKey;

                crawlManager.setBlogInfo(query, this.blog);
            }
        }
    }
}