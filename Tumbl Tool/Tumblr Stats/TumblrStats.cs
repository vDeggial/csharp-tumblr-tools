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
using System.Linq;
using Tumblr_Tool.Common_Helpers;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Tumblr_Stats
{
    public class TumblrStats
    {
        public TumblrStats()
        {
        }

        public TumblrStats(TumblrBlog blog, string url, string apiMode, int startNum = 0, int endNum = 0)
        {
            this.crawlManager = new CrawlManager();
            SetAPIMode(apiMode);
            step = (int)PostStepEnum.JSON;

            if (blog == null)
            {
                this.blog = new TumblrBlog();
            }
            else
            {
                this.blog = blog;
            }

            this.blog.posts = new HashSet<TumblrPost>();
            this.blog.url = url;
            this.tumblrDomain = CommonHelper.GetDomainName(url);
            SetBlogInfo();
            this.url = FileHelper.FixURL(this.blog.url);
            this.tumblrDomain = CommonHelper.GetDomainName(this.url);

            this.maxNumPosts = endNum;
            this.start = startNum;

            this.step = (int)PostStepEnum.JSON; //20 for JSON, 50 for XML
        }

        public int answerPosts { get; set; }

        public string apiMode { get; set; }

        public int audioPosts { get; set; }

        public TumblrBlog blog { get; set; }

        public int chatPosts { get; set; }

        public CrawlManager crawlManager { get; set; }

        public int found { get; set; }

        public int linkPosts { get; set; }

        public int maxNumPosts { get; set; }

        public int parsed { get; set; }

        public int photoPosts { get; set; }

        public int quotePosts { get; set; }

        public int start { get; set; }

        public ProcessingCodes statusCode { get; set; }

        public int step { get; set; }

        public int textPosts { get; set; }

        public int totalPosts { get; set; }

        public string tumblrDomain { get; set; }

        public string url { get; set; }

        public int videoPosts { get; set; }

        public TumblrBlog ParsePosts()
        {
            try
            {
                string url = XMLHelper.GetQueryString(this.url, TumblrPostTypes.empty.ToString(), 0);
                this.step = (int)PostStepEnum.XML;

                if (this.crawlManager.mode == ApiModeEnum.JSON.ToString())
                {
                    url = JSONHelper.GenerateQueryString(this.tumblrDomain, TumblrPostTypes.empty.ToString(), 0);
                    this.step = (int)PostStepEnum.JSON;
                }

                if (this.crawlManager.IsValidTumblr(url))
                {
                    this.totalPosts = this.blog.totalPosts;

                    int endCount;

                    if (maxNumPosts != 0)
                        endCount = maxNumPosts;
                    else
                        endCount = totalPosts;

                    int i = start;

                    this.parsed = 0;

                    while (i < this.totalPosts)
                    {
                        if (this.crawlManager.mode == ApiModeEnum.JSON.ToString())
                        {
                            url = JSONHelper.GenerateQueryString(this.tumblrDomain, TumblrPostTypes.empty.ToString(), i);
                        }

                        this.crawlManager.GetDocument(url);
                        this.blog.posts.UnionWith(this.crawlManager.GetPostList(TumblrPostTypes.empty.ToString(), this.crawlManager.mode));

                        this.photoPosts += (from p in this.blog.posts where p.type == TumblrPostTypes.photo.ToString() select p).ToList().Count;
                        this.textPosts += (from p in this.blog.posts where p.type == TumblrPostTypes.regular.ToString() || p.type == TumblrPostTypes.text.ToString() select p).ToList().Count;
                        this.videoPosts += (from p in this.blog.posts where p.type == TumblrPostTypes.video.ToString() select p).ToList().Count;
                        this.linkPosts += (from p in this.blog.posts where p.type == TumblrPostTypes.link.ToString() select p).ToList().Count;
                        this.audioPosts += (from p in this.blog.posts where p.type == TumblrPostTypes.audio.ToString() select p).ToList().Count;
                        this.quotePosts += (from p in this.blog.posts where p.type == TumblrPostTypes.quote.ToString() select p).ToList().Count;
                        this.chatPosts += (from p in this.blog.posts where p.type == TumblrPostTypes.chat.ToString() || p.type == TumblrPostTypes.conversation.ToString() select p).ToList().Count;
                        this.answerPosts += (from p in this.blog.posts where p.type == TumblrPostTypes.answer.ToString() select p).ToList().Count;
                        this.parsed += this.blog.posts.Count;
                        this.blog.posts.Clear();
                        i += step;
                    }

                    this.found = blog.posts.Count;
                }
                else
                {
                    this.statusCode = ProcessingCodes.invalidURL;
                }
                return this.blog;
            }
            catch
            {
                return this.blog;
            }
        }

        public void SetAPIMode(string mode)
        {
            try
            {
                this.apiMode = mode; // XML or JSON
                this.crawlManager.mode = mode;
            }
            catch
            {
                return;
            }
        }

        public void SetBlogInfo()
        {
            try
            {
                if (this.crawlManager.mode == ApiModeEnum.XML.ToString())
                {
                    this.crawlManager.SetBlogInfo(XMLHelper.GetQueryString(this.url, TumblrPostTypes.empty.ToString(), 0, 1), this.blog);
                }
                else if (this.crawlManager.mode == ApiModeEnum.JSON.ToString())
                {
                    this.crawlManager.SetBlogInfo(JSONHelper.GenerateQueryString(this.tumblrDomain, TumblrPostTypes.empty.ToString(), 0, 1), this.blog);
                }
            }
            catch
            {
                return;
            }
        }
    }
}