using System.Collections.Generic;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Tag_Scanner
{
    public class TagScanner
    {
        public TagScanner(TumblrBlog blog = null)
        {
            Blog = blog;
        }

        public TumblrApiVersion ApiVersion { get; set; }
        public TumblrBlog Blog { get; set; }
        public bool IsCancelled { get; private set; }
        public int NumberOfParsedPosts { get; set; }
        public int PercentComplete { get; private set; }
        public ProcessingCode ProcessingStatusCode { get; set; }
        public HashSet<string> TagList { get; set; }
        public int TotalNumberOfPosts { get; set; }
        public string TumblrDomain { get; set; }
        private int ApiQueryOffset { get; set; }
        private int ApiQueryPostLimit { get; set; }
        private DocumentManager DocumentManager { get; set; }
        private string TumblrUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool GetTumblrBlogInfo()
        {
            try
            {
                return DocumentManager.GetRemoteBlogInfo(JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostType.Photo.ToString().ToLower(), 0, 1), Blog);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parseMode"></param>
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
                        TagList.UnionWith(post.Tags);
                    }

                    NumberOfParsedPosts += posts.Count;

                    if (NumberOfParsedPosts > TotalNumberOfPosts) NumberOfParsedPosts = TotalNumberOfPosts;

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
        /// </summary>
        /// <returns></returns>
        public bool TumblrExists()
        {
            try
            {
                var url = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostType.All.ToString().ToLower(), 0, 1);

                return url.TumblrExists();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private HashSet<TumblrPost> GetTumblrPostList(int offset = 0)
        {
            try
            {
                var query = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostType.All.ToString().ToLower(), offset);

                DocumentManager.GetRemoteDocument(query);

                if ((ApiVersion == TumblrApiVersion.V2Json && DocumentManager.JsonDocument != null))
                {
                    DocumentManager.ImageSize = ImageSize.Original;
                    HashSet<TumblrPost> posts = DocumentManager.GetPostListFromDoc(TumblrPostType.All.ToString().ToLower());
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