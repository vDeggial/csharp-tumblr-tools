/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: November, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System.Collections.Generic;
using System.Linq;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Objects;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace Tumblr_Tool.Image_Ripper
{
    public class ImageRipper
    {
        /// <summary>
        /// Constructor for the class
        /// </summary>
        public ImageRipper()
        {
            ImageList = new HashSet<PhotoPostImage>();
            ErrorList = new HashSet<string>();
            DocumentManager = new DocumentManager();
            Blog = new TumblrBlog();
            CommentsList = new Dictionary<string, string>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="saveLocation"></param>
        /// <param name="generateLog"></param>
        /// <param name="parseSets"></param>
        /// <param name="parseJpeg"></param>
        /// <param name="parsePng"></param>
        /// <param name="parseGif"></param>
        /// <param name="imageSize"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="apiMode"></param>
        public ImageRipper(TumblrBlog blog, string saveLocation, bool generateLog = false, bool parseSets = true,
            bool parseJpeg = true, bool parsePng = true, bool parseGif = true, ImageSizes imageSize = ImageSizes.None, int offset = 0, int limit = 0, ApiModes apiMode = ApiModes.V2Json)
        {
            TumblrUrl = WebHelper.RemoveTrailingBackslash(blog.Url);
            TumblrDomain = WebHelper.GetDomainName(blog.Url);

            GenerateLog = generateLog;

            Offset = offset;
            SaveLocation = saveLocation;

            Limit = limit;

            ErrorList = new HashSet<string>();

            Blog = blog;
            StatusCode = ProcessingCodes.Ok;
            ParsePhotoSets = parseSets;
            ParseJpeg = parseJpeg;
            ParsePng = parsePng;
            ParseGif = parseGif;
            ImageSize = imageSize;
            ApiMode = apiMode;

            Blog?.Posts?.Clear();
            TotalNumberOfPosts = 0;
            ImageList = new HashSet<PhotoPostImage>();
            TotalNumberOfImages = 0;
            DocumentManager = new DocumentManager();
            SetApiMode(ApiMode);
            CommentsList = new Dictionary<string, string>();
        }

        public ApiModes ApiMode { get; set; }

        public TumblrBlog Blog { get; set; }
        public Dictionary<string, string> CommentsList { get; set; }
        public DocumentManager DocumentManager { get; set; }
        public HashSet<string> ErrorList { get; set; }
        public HashSet<string> ExistingImageList { get; set; }
        public bool GenerateLog { get; set; }
        public HashSet<PhotoPostImage> ImageList { get; set; }
        public ImageSizes ImageSize { get; set; }
        public bool IsCancelled { get; set; }

        public bool IsLogUpdated { get; set; }

        public int Limit { get; set; }

        public int NumberOfParsedPosts { get; set; }

        public int Offset { get; set; }

        public bool ParseGif { get; set; }

        public bool ParseJpeg { get; set; }

        public bool ParsePhotoSets { get; set; }

        public bool ParsePng { get; set; }

        public int PercentComplete { get; set; }

        public string SaveLocation { get; set; }

        public ProcessingCodes StatusCode { get; set; }

        public int TotalNumberOfImages { get; set; }

        public int TotalNumberOfPosts { get; set; }

        public string TumblrDomain { get; set; }

        public SaveFile TumblrPostLog { get; set; }

        public string TumblrUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parseMode"></param>
        /// <returns></returns>
        public TumblrBlog ParseBlogPosts(BlogPostsScanModes parseMode)
        {
            try
            {
                StatusCode = ProcessingCodes.Crawling;
                ExistingImageList = FileHelper.GenerateFolderImageList(SaveLocation);

                Blog.Posts = Blog.Posts ?? new HashSet<TumblrPost>();

                var numPostsPerDocument = (int)NumberOfPostsPerApiDocument.ApiV2;

                if (TotalNumberOfPosts == 0)
                    TotalNumberOfPosts = Blog.TotalPosts;

                bool finished = false;

                PercentComplete = 0;

                while (Offset < TotalNumberOfPosts && !IsCancelled && !finished)
                {
                    HashSet<TumblrPost> posts = GetTumblrPostList(Offset);

                    HashSet<TumblrPost> existingHash = new HashSet<TumblrPost>((from p in posts
                                                                                where FileHelper.IsExistingFile(ExistingImageList, p.Photos.Last().Filename)
                                                                                select p));

                    posts.RemoveWhere(x => existingHash.Contains(x));

                    if (parseMode == BlogPostsScanModes.NewestPostsOnly && existingHash.Count > 0)
                    {
                        finished = true;
                    }

                    if (posts.Count != 0)
                    {
                        Blog.Posts.UnionWith(posts);
                        NumberOfParsedPosts += posts.Count;
                        GenerateImageListForDownload(posts);
                    }

                    else
                    {
                        NumberOfParsedPosts += numPostsPerDocument;
                    }

                    PercentComplete = TotalNumberOfPosts > 0 ? (int)((NumberOfParsedPosts / (double)TotalNumberOfPosts) * 100.00) : 0;
                    Offset += numPostsPerDocument;

                    if (GenerateLog)
                    {
                        UpdateLogFile(Blog.Name);
                    }
                    Blog.Posts = new HashSet<TumblrPost>();
                }



                TotalNumberOfImages = ImageList.Count;

                if (ImageList.Count == 0)
                    Blog.Posts = new HashSet<TumblrPost>();

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
        /// <returns></returns>
        public bool SetBlogInfo()
        {
            try
            {
                var query = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.Photo.ToString().ToLower(), 0, 1);
                return DocumentManager.GetRemoteBlogInfo(query, Blog);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="log"></param>
        public void SetLogFile(SaveFile log)
        {
            TumblrPostLog = log;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool TumblrExists()
        {
            try
            {
                var url = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.All.ToString().ToLower(), 0, 1);

                return url.TumblrExists(ApiMode);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="logFileName"></param>
        public void UpdateLogFile(string logFileName)
        {
            try
            {
                if (TumblrPostLog == null || TumblrPostLog.Blog.Name != logFileName)
                {
                    TumblrPostLog = new SaveFile(logFileName + ".log", Blog);
                }

                UpdateLogFile(TumblrPostLog);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="posts"></param>
        private void GenerateImageListForDownload(HashSet<TumblrPost> posts)
        {
            foreach (var tumblrPost in posts)
            {
                var post = (PhotoPost)tumblrPost;
                if (!ParsePhotoSets && post.Photos.Count > 1)
                {
                    // do not parse images from photoset
                }
                else
                {
                    foreach (PhotoPostImage image in post.Photos)
                    {

                        try
                        {
                            //post.caption = CommonHelper.NewLineToBreak(post.caption, "</p>", string.Empty);

                            //post.caption = post.caption.StripTags();
                            ImageList.Add(image);
                        }
                        catch
                        {
                            return;
                        }
                    }
                }
            }

            if (ImageList.Count > 0)
            {
                HashSet<PhotoPostImage> removeHash = new HashSet<PhotoPostImage>();

                if (!ParseGif)
                {
                    removeHash.UnionWith(new HashSet<PhotoPostImage>((from p in ImageList where p.Filename.ToLower().EndsWith(".gif") select p)));
                }

                if (!ParseJpeg)
                {
                    removeHash.UnionWith(new HashSet<PhotoPostImage>((from p in ImageList where p.Filename.ToLower().EndsWith(".jpg") || p.Filename.ToLower().EndsWith(".jpeg") select p)));
                }

                if (!ParsePng)
                {
                    removeHash.UnionWith(new HashSet<PhotoPostImage>((from p in ImageList where p.Filename.ToLower().EndsWith(".png") select p)));
                }

                ImageList.RemoveWhere(x => removeHash.Contains(x));
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
                var query = JsonHelper.GeneratePostQueryString(TumblrDomain, TumblrPostTypes.Photo.ToString().ToLower(), offset);

                DocumentManager.GetRemoteDocument(query);

                if ((ApiMode == ApiModes.V2Json && DocumentManager.JsonDocument != null))
                {
                    DocumentManager.ImageSize = ImageSize;
                    HashSet<TumblrPost> posts = DocumentManager.GetPostListFromDoc(TumblrPostTypes.Photo.ToString().ToLower(), ApiMode);
                    return posts;
                }
                StatusCode = ProcessingCodes.UnableDownload;
                return new HashSet<TumblrPost>();
            }
            catch
            {
                return new HashSet<TumblrPost>();
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="logFile"></param>
        private void UpdateLogFile(SaveFile logFile)
        {
            try
            {
                foreach (TumblrPost post in Blog.Posts)
                {
                    logFile.Blog.Posts.RemoveWhere(p => p.Id == post.Id);

                    logFile.Blog.Posts.Add(post);
                    IsLogUpdated = true;
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}