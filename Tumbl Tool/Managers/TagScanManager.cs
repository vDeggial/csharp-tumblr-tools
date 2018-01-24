/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2018
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Managers
{
    public class TagScanManager : INotifyPropertyChanged
    {
        public TagScanManager(TumblrBlog blog = null, bool photoPostOnly = false)
        {
            Blog = blog;
            DocumentManager = new RemoteDocumentManager();
            TumblrUrl = WebHelper.RemoveTrailingBackslash(blog.Url);
            TumblrDomain = WebHelper.GetDomainName(blog.Url);
            TagList = new HashSet<string>();
            PhotoPostOnly = photoPostOnly;
        }

        private int numberOfParsedPosts;
        private ProcessingCode processingStatusCode;
        private HashSet<string> tagList;
        private int percentComplete;
        private int tagCount;


        public TumblrApiVersion ApiVersion { get; set; }
        public TumblrBlog Blog { get; set; }
        public bool IsCancelled { get; set; }

        public int TagCount
        {
            get
            {
                return tagCount;
            }
            set
            {
                if (value != tagCount)
                {
                    tagCount = value;
                    NotifyPropertyChanged();

                }
            }
        }

        public int NumberOfParsedPosts
        { get
            {
                return numberOfParsedPosts;
            }
            set
            {
                if (value != numberOfParsedPosts)
                {
                    numberOfParsedPosts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int PercentComplete
        { get
            {
                return percentComplete;
            }
            private set
            {
                if (value != percentComplete)
                {
                    percentComplete = value;
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
        public HashSet<string> TagList
        { get
            {
                return tagList;
            }
            set
            {
                tagList = value;
            }
        }
        public int TotalNumberOfPosts { get; set; }
        public string TumblrDomain { get; set; }
        private int ApiQueryOffset { get; set; }
        private int ApiQueryPostLimit { get; set; }
        private RemoteDocumentManager DocumentManager { get; set; }
        private string TumblrUrl { get; set; }
        private bool PhotoPostOnly { get; set; }

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
        public bool GetTumblrBlogInfo()
        {
            try
            {
                return DocumentManager.GetRemoteBlogInfo(
                    TumblrApiHelper.GeneratePostTypeQueryUrl(TumblrDomain, PhotoPostOnly == true ? TumblrPostType.Photo : TumblrPostType.All, 0, 1), Blog);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public TumblrBlog ScanTags()
        {
            try
            {
                ProcessingStatusCode = ProcessingCode.Crawling;

                Blog.Posts = Blog.Posts ?? new HashSet<TumblrPost>();

                var numPostsPerDocument = (int)NumberOfPostsPerApiDocument.ApiV2;

                if (TotalNumberOfPosts == 0)
                    TotalNumberOfPosts = Blog.TotalPosts;

                PercentComplete = 0;

                while (ApiQueryOffset < TotalNumberOfPosts && !IsCancelled)
                {
                    HashSet<TumblrPost> posts = GetTumblrPostList(ApiQueryOffset);

                    foreach (TumblrPost post in posts)
                    {
                        lock (TagList)
                        {
                            if (post.Tags != null) TagList.UnionWith(post.Tags);
                        }
                    }

                    if (TagCount != tagList.Count) TagCount = tagList.Count;
                    NumberOfParsedPosts += posts.Count;

                    NumberOfParsedPosts = (NumberOfParsedPosts > TotalNumberOfPosts) ? TotalNumberOfPosts : NumberOfParsedPosts;

                    PercentComplete = TotalNumberOfPosts > 0 ? (int)((NumberOfParsedPosts / (double)TotalNumberOfPosts) * 100.00) : 0;
                    ApiQueryOffset += numPostsPerDocument;

                    Blog.Posts = new HashSet<TumblrPost>();
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
        ///
        ///
        ///
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private HashSet<TumblrPost> GetTumblrPostList(int offset = 0)
        {
            try
            {
                DocumentManager.GetRemoteDocument(
                    TumblrApiHelper.GeneratePostTypeQueryUrl(TumblrDomain, PhotoPostOnly == true ? TumblrPostType.Photo : TumblrPostType.All, offset));

                if ((ApiVersion == TumblrApiVersion.V2Json && DocumentManager.RemoteDocument != null))
                {
                    HashSet<TumblrPost> posts = DocumentManager.GetPostListFromDoc(TumblrPostType.All);
                    return posts;
                }
                ProcessingStatusCode = ProcessingCode.UnableDownload;
                return new HashSet<TumblrPost>();
            }
            catch
            {
                return new HashSet<TumblrPost>();
            }
        }
    }
}