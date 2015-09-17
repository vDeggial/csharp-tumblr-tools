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
        public TumblrStats(TumblrBlog blog, string url, ApiModeEnum apiMode, int startNum = 0, int endNum = 0)
        {
            DocManager = new DocumentManager();
            SetApiMode(apiMode);
            Step = (int)PostStepEnum.Json;

            Blog = blog ?? new TumblrBlog(url);

            Blog.Posts = new HashSet<TumblrPost>();

            Url = WebHelper.RemoveTrailingBackslash(Blog.Url);
            TumblrDomain = WebHelper.GetDomainName(Url);

            MaxNumPosts = endNum;
            Start = startNum;

            Step = (int)PostStepEnum.Json; //20 for JSON, 50 for XML

            SetBlogInfo();
        }

        public int AnswerPosts { get; set; }

        public ApiModeEnum ApiMode { get; set; }

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
        public bool GetStats()
        {
            var url = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.All.ToString().ToLower(), 0, 1);

            if (url.TumblrExists(DocManager.ApiMode))

            {
                DocManager.GetDocument(url);
                TotalPostsOverall = DocManager.GetTotalPostCount();
            }

            var values = Enum.GetValues(typeof(TumblrPostTypes)).Cast<TumblrPostTypes>();

            foreach (TumblrPostTypes type in values)
            {
                TotalPosts = 0;
                if (type != TumblrPostTypes.All && type != TumblrPostTypes.Conversation && type != TumblrPostTypes.Regular)
                {
                    url = JsonHelper.GeneratePostQueryString(TumblrDomain, type.ToString().ToLower(), 0, 1);

                    if (url.TumblrExists(DocManager.ApiMode))
                    {
                        DocManager.GetDocument(url);
                        TotalPosts = DocManager.GetTotalPostCount();

                        switch (type)
                        {
                            case TumblrPostTypes.Photo:
                                PhotoPosts = TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.Text:
                                TextPosts = TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.Video:
                                VideoPosts = TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.Audio:
                                AudioPosts = TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.Link:
                                LinkPosts = TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.Quote:
                                QuotePosts = TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.Chat:
                                ChatPosts = TotalPosts;
                                Parsed++;
                                break;

                            case TumblrPostTypes.Answer:
                                AnswerPosts = TotalPosts;
                                Parsed++;
                                break;
                        }
                        StatusCode = ProcessingCodes.Ok;
                    }
                    else
                    {
                        StatusCode = ProcessingCodes.InvalidUrl;
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
                Step = (int)PostStepEnum.Json;

                var url = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.All.ToString().ToLower(),0,1);

                if (url.TumblrExists(DocManager.ApiMode))
                {
                    TotalPosts = Blog.TotalPosts;

                    int i = Start;

                    Parsed = 0;

                    while (i < TotalPosts)
                    {
                        if (DocManager.ApiMode == ApiModeEnum.ApiV2Json)
                        {
                            url = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.All.ToString().ToLower(), i,1);
                        }

                        DocManager.GetDocument(url);
                        Blog.Posts.UnionWith(DocManager.GetPostListFromDoc(TumblrPostTypes.All.ToString().ToLower(), DocManager.ApiMode));

                        PhotoPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Photo.ToString().ToLower() select p).ToList().Count;
                        TextPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Regular.ToString().ToLower() || p.Type == TumblrPostTypes.Text.ToString().ToLower() select p).ToList().Count;
                        VideoPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Video.ToString().ToLower() select p).ToList().Count;
                        LinkPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Link.ToString().ToLower() select p).ToList().Count;
                        AudioPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Audio.ToString().ToLower() select p).ToList().Count;
                        QuotePosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Quote.ToString().ToLower() select p).ToList().Count;
                        ChatPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Chat.ToString().ToLower() || p.Type == TumblrPostTypes.Conversation.ToString().ToLower() select p).ToList().Count;
                        AnswerPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Answer.ToString().ToLower() select p).ToList().Count;
                        Parsed += Blog.Posts.Count;
                        Blog.Posts.Clear();
                        i += Step;
                    }

                    Found = Blog.Posts.Count;
                }
                else
                {
                    StatusCode = ProcessingCodes.InvalidUrl;
                }
                return Blog;
            }
            catch
            {
                return Blog;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mode"></param>
        public void SetApiMode(ApiModeEnum mode)
        {
            try
            {
                ApiMode = mode; // XML or JSON
                DocManager.ApiMode = mode;
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void SetBlogInfo()
        {
            try
            {
                DocManager.GetBlogInfo(JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.All.ToString().ToLower(), 0, 1), Blog);
            }
            catch
            {
                // ignored
            }
        }
    }
}