/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2016
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
            DocumentManager = new DocumentManager();
            SetApiMode(apiMode);
            NumPostsPerDocument = (int)NumberOfPostsPerApiDocument.ApiV2;

            Blog = blog ?? new TumblrBlog(url);

            Blog.Posts = new HashSet<TumblrPost>();

            TumblrUrl = WebHelper.RemoveTrailingBackslash(Blog.Url);
            TumblrDomain = WebHelper.GetDomainName(TumblrUrl);

            MaxNumPosts = limit;
            Offset = offset;

            NumPostsPerDocument = (int)NumberOfPostsPerApiDocument.ApiV2; //20 for JSON, 50 for XML

            SetBlogInfo();
        }

        public int NumAnswerPosts { get; set; }

        public ApiModes ApiMode { get; set; }

        public int NumAudioPosts { get; set; }

        public TumblrBlog Blog { get; set; }

        public int NumChatPosts { get; set; }

        public DocumentManager DocumentManager { get; set; }

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

        public int NumTotalPostsForType { get; set; }

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

            if (url.TumblrExists(DocumentManager.ApiMode))

            {
                DocumentManager.GetRemoteDocument(url);
                NumTotalPostsOverall = DocumentManager.GetTotalPostCount();
            }

            var values = Enum.GetValues(typeof(TumblrPostTypes)).Cast<TumblrPostTypes>();

            foreach (TumblrPostTypes type in values)
            {
                NumTotalPostsForType = 0;
                if (type != TumblrPostTypes.All && type != TumblrPostTypes.Conversation && type != TumblrPostTypes.Regular)
                {
                    url = JsonHelper.GeneratePostQueryString(TumblrDomain, type.ToString().ToLower(), 0, 1);

                    if (url.TumblrExists(DocumentManager.ApiMode))
                    {
                        DocumentManager.GetRemoteDocument(url);
                        NumTotalPostsForType = DocumentManager.GetTotalPostCount();

                        switch (type)
                        {
                            case TumblrPostTypes.Photo:
                                NumPhotoPosts = NumTotalPostsForType;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Text:
                                NumTextPosts = NumTotalPostsForType;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Video:
                                NumVideoPosts = NumTotalPostsForType;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Audio:
                                NumAudioPosts = NumTotalPostsForType;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Link:
                                NumLinkPosts = NumTotalPostsForType;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Quote:
                                NumQuotePosts = NumTotalPostsForType;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Chat:
                                NumChatPosts = NumTotalPostsForType;
                                NumParsed++;
                                break;

                            case TumblrPostTypes.Answer:
                                NumAnswerPosts = NumTotalPostsForType;
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
        /// <param name="mode"></param>
        public void SetApiMode(ApiModes mode)
        {
            try
            {
                ApiMode = mode; // XML or JSON
                DocumentManager.ApiMode = mode;
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
                DocumentManager.GetRemoteBlogInfo(JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.All.ToString().ToLower(), 0, 1), Blog);
            }
            catch
            {
                // ignored
            }
        }
    }
}