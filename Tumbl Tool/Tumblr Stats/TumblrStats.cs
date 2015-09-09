/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: September, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Linq;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Tumblr_Stats
{
    public class TumblrStats
    {
        /// <summary>
        ///
        /// </summary>
        public TumblrStats()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="url"></param>
        /// <param name="apiMode"></param>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        public TumblrStats(TumblrBlog blog, string url, string apiMode, int startNum = 0, int endNum = 0)
        {
            this.DocManager = new DocumentManager();
            SetAPIMode(apiMode);
            Step = (int)PostStepEnum.JSON;

            if (blog == null)
            {
                this.Blog = new TumblrBlog(url);
            }
            else
            {
                this.Blog = blog;
            }

            this.Blog.Posts = new HashSet<TumblrPost>();

            this.Url = WebHelper.RemoveTrailingBackslash(this.Blog.Url);
            this.TumblrDomain = WebHelper.GetDomainName(this.Url);

            this.MaxNumPosts = endNum;
            this.Start = startNum;

            this.Step = (int)PostStepEnum.JSON; //20 for JSON, 50 for XML

            SetBlogInfo();
        }

        public int AnswerPosts { get; set; }

        public string ApiMode { get; set; }

        public int AudioPosts { get; set; }

        public TumblrBlog Blog { get; set; }

        public int ChatPosts { get; set; }

        public DocumentManager DocManager { get; set; }

        public int Found { get; set; }

        public int LinkPosts { get; set; }

        public int MaxNumPosts { get; set; }

        public int Parsed { get; set; }

        public int PhotoPosts { get; set; }

        public int QuotePosts { get; set; }

        public int Start { get; set; }

        public ProcessingCodes StatusCode { get; set; }

        public int Step { get; set; }

        public int TextPosts { get; set; }

        public int TotalPosts { get; set; }

        public int TotalPostsOverall { get; set; }

        public string TumblrDomain { get; set; }

        public string Url { get; set; }

        public int VideoPosts { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool getStats()
        {
            string url = XmlHelper.GenerateQueryString(this.Url, TumblrPostTypes.all.ToString(), 0, 1);
            if (this.DocManager.ApiMode == ApiModeEnum.v2JSON.ToString())
            {
                url = JsonHelper.GenerateQueryString(this.TumblrDomain, TumblrPostTypes.all.ToString(), 0, 1);
            }

            if (url.TumblrExists(this.DocManager.ApiMode))

            {
                this.DocManager.GetDocument(url);
                this.TotalPostsOverall = this.DocManager.GetTotalPostCount();
            }

            var values = Enum.GetValues(typeof(TumblrPostTypes)).Cast<TumblrPostTypes>();

            foreach (TumblrPostTypes type in values)
            {
                this.TotalPosts = 0;
                if (type != TumblrPostTypes.all && type != TumblrPostTypes.conversation && type != TumblrPostTypes.regular)
                {
                    url = XmlHelper.GenerateQueryString(this.Url, type.ToString(), 0, 1);
                    if (this.DocManager.ApiMode == ApiModeEnum.v2JSON.ToString())
                    {
                        url = JsonHelper.GenerateQueryString(this.TumblrDomain, type.ToString(), 0, 1);
                    }

                    if (url.TumblrExists(this.DocManager.ApiMode))
                    {
                        this.DocManager.GetDocument(url);
                        this.TotalPosts = this.DocManager.GetTotalPostCount();

                        switch (type)
                        {
                            case TumblrPostTypes.photo:
                                this.PhotoPosts = this.TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.text:
                                this.TextPosts = this.TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.video:
                                this.VideoPosts = this.TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.audio:
                                this.AudioPosts = this.TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.link:
                                this.LinkPosts = this.TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.quote:
                                this.QuotePosts = this.TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.chat:
                                this.ChatPosts = this.TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.answer:
                                this.AnswerPosts = this.TotalPosts;
                                Parsed++;
                                break;
                        }
                        this.StatusCode = ProcessingCodes.OK;
                    }
                    else
                    {
                        this.StatusCode = ProcessingCodes.invalidURL;
                    }
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public TumblrBlog ParsePosts()
        {
            try
            {
                string url = XmlHelper.GenerateQueryString(this.Url, TumblrPostTypes.all.ToString(), 0);
                this.Step = (int)PostStepEnum.XML;

                if (this.DocManager.ApiMode == ApiModeEnum.v2JSON.ToString())
                {
                    url = JsonHelper.GenerateQueryString(this.TumblrDomain, TumblrPostTypes.all.ToString(), 0);
                    this.Step = (int)PostStepEnum.JSON;
                }

                if (url.TumblrExists(this.DocManager.ApiMode))
                {
                    this.TotalPosts = this.Blog.TotalPosts;

                    int endCount;

                    if (MaxNumPosts != 0)
                        endCount = MaxNumPosts;
                    else
                        endCount = TotalPosts;

                    int i = Start;

                    this.Parsed = 0;

                    while (i < this.TotalPosts)
                    {
                        if (this.DocManager.ApiMode == ApiModeEnum.v2JSON.ToString())
                        {
                            url = JsonHelper.GenerateQueryString(this.TumblrDomain, TumblrPostTypes.all.ToString(), i);
                        }

                        this.DocManager.GetDocument(url);
                        this.Blog.Posts.UnionWith(this.DocManager.GetPostListFromDoc(TumblrPostTypes.all.ToString(), this.DocManager.ApiMode));

                        this.PhotoPosts += (from p in this.Blog.Posts where p.Type == TumblrPostTypes.photo.ToString() select p).ToList().Count;
                        this.TextPosts += (from p in this.Blog.Posts where p.Type == TumblrPostTypes.regular.ToString() || p.Type == TumblrPostTypes.text.ToString() select p).ToList().Count;
                        this.VideoPosts += (from p in this.Blog.Posts where p.Type == TumblrPostTypes.video.ToString() select p).ToList().Count;
                        this.LinkPosts += (from p in this.Blog.Posts where p.Type == TumblrPostTypes.link.ToString() select p).ToList().Count;
                        this.AudioPosts += (from p in this.Blog.Posts where p.Type == TumblrPostTypes.audio.ToString() select p).ToList().Count;
                        this.QuotePosts += (from p in this.Blog.Posts where p.Type == TumblrPostTypes.quote.ToString() select p).ToList().Count;
                        this.ChatPosts += (from p in this.Blog.Posts where p.Type == TumblrPostTypes.chat.ToString() || p.Type == TumblrPostTypes.conversation.ToString() select p).ToList().Count;
                        this.AnswerPosts += (from p in this.Blog.Posts where p.Type == TumblrPostTypes.answer.ToString() select p).ToList().Count;
                        this.Parsed += this.Blog.Posts.Count;
                        this.Blog.Posts.Clear();
                        i += Step;
                    }

                    this.Found = Blog.Posts.Count;
                }
                else
                {
                    this.StatusCode = ProcessingCodes.invalidURL;
                }
                return this.Blog;
            }
            catch
            {
                return this.Blog;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mode"></param>
        public void SetAPIMode(string mode)
        {
            try
            {
                this.ApiMode = mode; // XML or JSON
                this.DocManager.ApiMode = mode;
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void SetBlogInfo()
        {
            try
            {
                if (this.DocManager.ApiMode == ApiModeEnum.v1XML.ToString())
                {
                    this.DocManager.GetBlogInfo(XmlHelper.GenerateQueryString(this.Url, TumblrPostTypes.all.ToString(), 0, 1), this.Blog);
                }
                else if (this.DocManager.ApiMode == ApiModeEnum.v2JSON.ToString())
                {
                    this.DocManager.GetBlogInfo(JsonHelper.GenerateQueryString(this.TumblrDomain, TumblrPostTypes.all.ToString(), 0, 1), this.Blog);
                }
            }
            catch
            {
                return;
            }
        }
    }
}