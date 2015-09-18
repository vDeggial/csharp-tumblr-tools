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
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public TumblrStats(TumblrBlog blog, string url, ApiModes apiMode, int offset = 0, int limit = 0)
        {
            DocManager = new DocumentManager();
            SetApiMode(apiMode);
            NumPostsPerDocument = (int)NumberOfPostsPerDocument.ApiV2;

            Blog = blog ?? new TumblrBlog(url);

            Blog.Posts = new HashSet<TumblrPost>();

            TumblrUrl = WebHelper.RemoveTrailingBackslash(Blog.Url);
            TumblrDomain = WebHelper.GetDomainName(TumblrUrl);

            MaxNumPosts = limit;
            Offset = offset;

            NumPostsPerDocument = (int)NumberOfPostsPerDocument.ApiV2; //20 for JSON, 50 for XML

            SetBlogInfo();
        }

        public int NumAnswerPosts { get; set; }

        public ApiModes ApiMode { get; set; }

        public int NumAudioPosts { get; set; }

        public TumblrBlog Blog { get; set; }

        public int NumChatPosts { get; set; }

        public DocumentManager DocManager { get; set; }

        public int NumPostsFound { get; set; }

        public int NumLinkPosts { get; set; }

        public int MaxNumPosts { get; set; }

        public int NumParsed { get; set; }

        public int NumPhotoPosts { get; set; }

        public int NumQuotePosts { get; set; }

        public int Offset { get; set; }

        public ProcessingCodes StatusCode { get; set; }

        public int NumPostsPerDocument { get; set; }

        public int NumTextPosts { get; set; }

        public int NumTotalPosts { get; set; }

        public int NumTotalPostsOverall { get; set; }

        public string TumblrDomain { get; set; }

        public string TumblrUrl { get; set; }

        public int NumVideoPosts { get; set; }

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
                NumTotalPostsOverall = DocManager.GetTotalPostCount();
            }

            var values = Enum.GetValues(typeof(TumblrPostTypes)).Cast<TumblrPostTypes>();

            foreach (TumblrPostTypes type in values)
            {
                NumTotalPosts = 0;
                if (type != TumblrPostTypes.All && type != TumblrPostTypes.Conversation && type != TumblrPostTypes.Regular)
                {
                    url = JsonHelper.GeneratePostQueryString(TumblrDomain, type.ToString().ToLower(), 0, 1);

                    if (url.TumblrExists(DocManager.ApiMode))
                    {
                        DocManager.GetDocument(url);
                        NumTotalPosts = DocManager.GetTotalPostCount();

                        switch (type)
                        {
                            case TumblrPostTypes.Photo:
                                NumPhotoPosts = NumTotalPosts;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Text:
                                NumTextPosts = NumTotalPosts;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Video:
                                NumVideoPosts = NumTotalPosts;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Audio:
                                NumAudioPosts = NumTotalPosts;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Link:
                                NumLinkPosts = NumTotalPosts;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Quote:
                                NumQuotePosts = NumTotalPosts;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Chat:
                                NumChatPosts = NumTotalPosts;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Answer:
                                NumAnswerPosts = NumTotalPosts;
                                NumParsed++;
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
                NumPostsPerDocument = (int)NumberOfPostsPerDocument.ApiV2;

                var url = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.All.ToString().ToLower(),0,1);

                if (url.TumblrExists(DocManager.ApiMode))
                {
                    NumTotalPosts = Blog.TotalPosts;

                    int i = Offset;

                    NumParsed = 0;

                    while (i < NumTotalPosts)
                    {
                        if (DocManager.ApiMode == ApiModes.V2Json)
                        {
                            url = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.All.ToString().ToLower(), i,1);
                        }

                        DocManager.GetDocument(url);
                        Blog.Posts.UnionWith(DocManager.GetPostListFromDoc(TumblrPostTypes.All.ToString().ToLower(), DocManager.ApiMode));

                        NumPhotoPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Photo.ToString().ToLower() select p).ToList().Count;
                        NumTextPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Regular.ToString().ToLower() || p.Type == TumblrPostTypes.Text.ToString().ToLower() select p).ToList().Count;
                        NumVideoPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Video.ToString().ToLower() select p).ToList().Count;
                        NumLinkPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Link.ToString().ToLower() select p).ToList().Count;
                        NumAudioPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Audio.ToString().ToLower() select p).ToList().Count;
                        NumQuotePosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Quote.ToString().ToLower() select p).ToList().Count;
                        NumChatPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Chat.ToString().ToLower() || p.Type == TumblrPostTypes.Conversation.ToString().ToLower() select p).ToList().Count;
                        NumAnswerPosts += (from p in Blog.Posts where p.Type == TumblrPostTypes.Answer.ToString().ToLower() select p).ToList().Count;
                        NumParsed += Blog.Posts.Count;
                        Blog.Posts.Clear();
                        i += NumPostsPerDocument;
                    }

                    NumPostsFound = Blog.Posts.Count;
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
        public void SetApiMode(ApiModes mode)
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