/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: August, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Managers
{
    public class TumblrStatsManager : INotifyPropertyChanged
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
            DocumentManager = new RemoteDocumentManager();
            ApiVersion = apiMode;
            DocumentManager.ApiVersion = apiMode;
            TotalPostsPerDocument = (int)NumberOfPostsPerApiDocument.ApiV2;

            Blog = blog ?? ((url != null) ? new TumblrBlog(url) : null);

            if (Blog != null)
            {
                Blog.Posts = new HashSet<TumblrPost>();

                TumblrUrl = WebHelper.RemoveTrailingBackslash(Blog.Url);
                TumblrDomain = WebHelper.GetDomainName(TumblrUrl);

                ApiQueryLimit = limit;
                ApiQueryOffset = offset;

                TotalPostsPerDocument = (int)NumberOfPostsPerApiDocument.ApiV2; //20 for JSON, 50 for XML

                var values = Enum.GetValues(typeof(TumblrPostType)).Cast<TumblrPostType>();
                TypesCount = values.Count() - 3;

                // Get Blog Info
                DocumentManager.GetRemoteBlogInfo(TumblrApiHelper.GeneratePostTypeQueryUrl(TumblrDomain, TumblrPostType.All, 0, 1), Blog);
            }
        }

        private int postTypesProcessedCount;
        private ProcessingCode processingStatusCode;
        private int totalAnswerPosts;
        private int totalAudioPosts;
        private int totalChatPosts;
        private int totalLinkPosts;
        private int totalPhotoPosts;
        private int totalQuotePosts;
        private int totalTextPosts;
        private int totalVideoPosts;
        private int totalPostsOverall;
        private int typesCount;

        public TumblrBlog Blog { get; set; }

        public int TypesCount
        {
            get
            {
                return typesCount;
            }
            set
            {
                typesCount = value;

            }
        }
        public int PostTypesProcessedCount
        { get
            {
                return postTypesProcessedCount;
            }
            set
            {
                if (value != postTypesProcessedCount)
                {
                    postTypesProcessedCount = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ProcessingCode ProcessingStatusCode
        { get
            {
                return processingStatusCode;
            }
            set
            {
                if (value != processingStatusCode)
                {
                    processingStatusCode = value;
                    NotifyPropertyChanged();
                }

            }
        }
        public int TotalAnswerPosts
        { get
            {
                return totalAnswerPosts;
            }
            set
            {
                if (value != totalAnswerPosts)
                {
                    totalAnswerPosts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int TotalAudioPosts
        {
            get
            {
                return totalAudioPosts;
            }
            set
            {
                if (value != totalAudioPosts)
                {
                    totalAudioPosts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int TotalChatPosts
        {
            get
            {
                return totalChatPosts;
            }
            set
            {
                if (value != totalChatPosts)
                {
                    totalChatPosts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int TotalLinkPosts
        {
            get
            {
                return totalLinkPosts;
            }
            set
            {
                if (value != totalLinkPosts)
                {
                    totalLinkPosts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int TotalPhotoPosts
        {
            get
            {
                return totalPhotoPosts;
            }
            set
            {
                if (value != totalPhotoPosts)
                {
                    totalPhotoPosts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int TotalPostsForType { get; set; }
        public int TotalPostsFound { get; set; }
        public int TotalPostsOverall
        {
            get
            {
                return totalPostsOverall;
            }
            set
            {
                if (value != totalPostsOverall)
                {
                    totalPostsOverall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int TotalPostsPerDocument { get; set; }
        public int TotalQuotePosts
        {
            get
            {
                return totalQuotePosts;
            }
            set
            {
                if (value != totalQuotePosts)
                {
                    totalQuotePosts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int TotalTextPosts
        {
            get
            {
                return totalTextPosts;
            }
            set
            {
                if (value != totalTextPosts)
                {
                    totalTextPosts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int TotalVideoPosts
        {
            get
            {
                return totalVideoPosts;
            }
            set
            {
                if (value != totalVideoPosts)
                {
                    totalVideoPosts = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int ApiQueryLimit { get; set; }
        private int ApiQueryOffset { get; set; }
        private TumblrApiVersion ApiVersion { get; set; }
        private RemoteDocumentManager DocumentManager { get; set; }
        private string TumblrDomain { get; set; }
        private string TumblrUrl { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool GetTumblrStats()
        {
            try
            {
                string url = TumblrApiHelper.GeneratePostTypeQueryUrl(TumblrDomain, TumblrPostType.All, 0, 1);

                if (TumblrUrl.TumblrBlogExists())

                {
                    DocumentManager.GetRemoteDocument(url);
                    if (DocumentManager.JsonDocument != null)
                        TotalPostsOverall = DocumentManager.GetTotalPostCount();

                    var postTypes = Enum.GetValues(typeof(TumblrPostType)).Cast<TumblrPostType>().ToHashSet();
                    postTypes.RemoveWhere(type => type == TumblrPostType.All || type == TumblrPostType.Regular || type == TumblrPostType.Conversation);

                    foreach (TumblrPostType type in postTypes)
                    {
                        int TotalPostsForType = 0;
                        url = TumblrApiHelper.GeneratePostTypeQueryUrl(TumblrDomain, type, 0, 1);

                        if (TumblrUrl.TumblrBlogExists())
                        {
                            DocumentManager.GetRemoteDocument(url);
                            if (DocumentManager.JsonDocument != null)
                            {
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
                        }
                        else
                        {
                            ProcessingStatusCode = ProcessingCode.InvalidUrl;
                        }
                    }
                }

                return true;
            }

            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}