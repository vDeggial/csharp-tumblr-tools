/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Linq;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Managers
{
    public class TumblrStatsManager
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="url"></param>
        /// <param name="apiMode"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public TumblrStatsManager(TumblrBlog blog = null, string url = null, TumblrApiVersion apiMode = TumblrApiVersion.V2Json, int offset = 0, int limit = 0)
        {
            DocumentManager = new DocumentManager();
            ApiVersion = apiMode;
            DocumentManager.ApiVersion = apiMode;
            TotalPostsPerDocument = (int)NumberOfPostsPerApiDocument.ApiV2;

            Blog = blog ?? new TumblrBlog(url);

            Blog.Posts = new HashSet<TumblrPost>();

            TumblrUrl = WebHelper.RemoveTrailingBackslash(Blog.Url);
            TumblrDomain = WebHelper.GetDomainName(TumblrUrl);

            ApiQueryLimit = limit;
            ApiQueryOffset = offset;

            TotalPostsPerDocument = (int)NumberOfPostsPerApiDocument.ApiV2; //20 for JSON, 50 for XML

            // Get Blog Info
            DocumentManager.GetRemoteBlogInfo(JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostType.All, 0, 1), Blog);
        }

        public TumblrBlog Blog { get; set; }
        public int PostTypesProcessedCount { get; set; }
        public ProcessingCode ProcessingStatusCode { get; set; }
        public int TotalAnswerPosts { get; set; }
        public int TotalAudioPosts { get; set; }
        public int TotalChatPosts { get; set; }
        public int TotalLinkPosts { get; set; }
        public int TotalPhotoPosts { get; set; }
        public int TotalPostsForType { get; set; }
        public int TotalPostsFound { get; set; }
        public int TotalPostsOverall { get; set; }
        public int TotalPostsPerDocument { get; set; }
        public int TotalQuotePosts { get; set; }
        public int TotalTextPosts { get; set; }
        public int TotalVideoPosts { get; set; }

        private int ApiQueryLimit { get; set; }
        private int ApiQueryOffset { get; set; }
        private TumblrApiVersion ApiVersion { get; set; }
        private DocumentManager DocumentManager { get; set; }
        private string TumblrDomain { get; set; }
        private string TumblrUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool GetTumblrStats()
        {
            var url = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostType.All, 0, 1);

            if (url.TumblrExists())

            {
                DocumentManager.GetRemoteDocument(url);
                TotalPostsOverall = DocumentManager.GetTotalPostCount();

                var postTypes = Enum.GetValues(typeof(TumblrPostType)).Cast<TumblrPostType>().ToHashSet();
                postTypes.RemoveWhere(type => type == TumblrPostType.All || type == TumblrPostType.Regular || type == TumblrPostType.Conversation);

                foreach (TumblrPostType type in postTypes)
                {
                    int TotalPostsForType = 0;
                    url = JsonHelper.GeneratePostQueryString(TumblrDomain, type, 0, 1);

                    if (url.TumblrExists())
                    {
                        DocumentManager.GetRemoteDocument(url);
                        TotalPostsForType = DocumentManager.GetTotalPostCount();

                        switch (type)
                        {
                            case TumblrPostType.Photo:
                                TotalPhotoPosts = TotalPostsForType;
                                break;

                            case TumblrPostType.Text:
                                TotalTextPosts = TotalPostsForType;
                                break;

                            case TumblrPostType.Video:
                                TotalVideoPosts = TotalPostsForType;
                                break;

                            case TumblrPostType.Audio:
                                TotalAudioPosts = TotalPostsForType;
                                break;

                            case TumblrPostType.Link:
                                TotalLinkPosts = TotalPostsForType;
                                break;

                            case TumblrPostType.Quote:
                                TotalQuotePosts = TotalPostsForType;
                                break;

                            case TumblrPostType.Chat:
                                TotalChatPosts = TotalPostsForType;
                                break;

                            case TumblrPostType.Answer:
                                TotalAnswerPosts = TotalPostsForType;
                                break;
                        }

                        PostTypesProcessedCount++;

                        ProcessingStatusCode = ProcessingCode.Ok;
                    }
                    else
                    {
                        ProcessingStatusCode = ProcessingCode.InvalidUrl;
                    }
                }
            }

            return true;
        }
    }
}