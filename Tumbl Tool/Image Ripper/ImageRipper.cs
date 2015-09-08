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
        /// <param name="parseJPEG"></param>
        /// <param name="parsePNG"></param>
        /// <param name="parseGIF"></param>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <param name="apiMode"></param>
        public ImageRipper(TumblrBlog blog, string saveLocation, bool generateLog = false, bool parseSets = true,
            bool parseJPEG = true, bool parsePNG = true, bool parseGIF = true, ImageSizes imageSize = ImageSizes.None, int startNum = 0, int endNum = 0, string apiMode = "v2JSON")
        {
            this.TumblrURL = WebHelper.RemoveTrailingBackslash(blog.Url);
            this.TumblrDomain = WebHelper.GetDomainName(blog.Url);

            this.GenerateLog = generateLog;

            this.Offset = startNum;
            this.SaveLocation = saveLocation;

            this.MaximumNumberOfPosts = endNum;

            this.ErrorList = new HashSet<string>();

            this.Blog = blog;
            this.StatusCode = ProcessingCodes.OK;
            this.ParsePhotoSets = parseSets;
            this.ParseJPEG = parseJPEG;
            this.ParsePNG = parsePNG;
            this.ParseGIF = parseGIF;
            this.ImageSize = imageSize;

            if (this.Blog != null)
            {
                if (this.Blog.Posts != null)
                {
                    this.Blog.Posts.Clear();
                }
            }
            this.TotalNumberOfPosts = 0;
            this.ImageList = new HashSet<PhotoPostImage>();
            this.TotalNumberOfImages = 0;
            this.DocumentManager = new DocumentManager();
            SetAPIMode(this.ApiMode);
            this.CommentsList = new Dictionary<string, string>();
        }

        public string ApiMode { get; set; }

        public ImageSizes ImageSize { get; set; }

        public TumblrBlog Blog { get; set; }

        public Dictionary<string, string> CommentsList { get; set; }

        public DocumentManager DocumentManager { get; set; }

        public HashSet<string> ErrorList { get; set; }

        public HashSet<string> ExistingImageList { get; set; }

        public bool GenerateLog { get; set; }

        public HashSet<PhotoPostImage> ImageList { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsLogUpdated { get; set; }

        public int MaximumNumberOfPosts { get; set; }

        public int NumberOfParsedPosts { get; set; }

        public int Offset { get; set; }

        public bool ParseGIF { get; set; }

        public bool ParseJPEG { get; set; }

        public bool ParsePhotoSets { get; set; }

        public bool ParsePNG { get; set; }

        public int PercentComplete { get; set; }

        public string SaveLocation { get; set; }

        public ProcessingCodes StatusCode { get; set; }

        public int TotalNumberOfImages { get; set; }

        public int TotalNumberOfPosts { get; set; }

        public string TumblrDomain { get; set; }

        public SaveFile TumblrPostLog { get; set; }

        public string TumblrURL { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="posts"></param>
        public void GenerateImageListForDownload(HashSet<TumblrPost> posts)
        {
            foreach (PhotoPost post in posts)
            {
                if (!this.ParsePhotoSets && post.Photos.Count > 1)
                {
                    // do not parse images from photoset
                }
                else
                {
                    foreach (PhotoPostImage image in post.Photos)
                    {
                        if (!this.ExistingImageList.Contains(image.Filename, StringComparer.OrdinalIgnoreCase))
                        {
                            try
                            {
                                //post.caption = CommonHelper.NewLineToBreak(post.caption, "</p>", string.Empty);

                                //post.caption = post.caption.StripTags();
                                this.ImageList.Add(image);
                            }
                            catch
                            {
                                return;
                            }
                        }
                        else
                        {
                            image.Downloaded = true;
                        }
                    }
                }
            }

            if (this.ImageList.Count > 0)
            {
                HashSet<PhotoPostImage> removeHash = new HashSet<PhotoPostImage>();

                if (!this.ParseGIF)
                {
                    removeHash.UnionWith(new HashSet<PhotoPostImage>((from p in ImageList where p.Filename.ToLower().EndsWith(".gif") select p)));
                }

                if (!this.ParseJPEG)
                {
                    removeHash.UnionWith(new HashSet<PhotoPostImage>((from p in ImageList where p.Filename.ToLower().EndsWith(".jpg") || p.Filename.ToLower().EndsWith(".jpeg") select p)));
                }

                if (!this.ParsePNG)
                {
                    removeHash.UnionWith(new HashSet<PhotoPostImage>((from p in ImageList where p.Filename.ToLower().EndsWith(".png") select p)));
                }

                this.ImageList.RemoveWhere(x => removeHash.Contains(x));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public HashSet<TumblrPost> GetTumblrPostList(int start = 0)
        {
            try
            {
                string query;
                if (this.ApiMode == ApiModeEnum.v1XML.ToString()) //XML
                {
                    query = XmlHelper.GenerateQueryString(this.TumblrURL, TumblrPostTypes.photo.ToString(), start);
                }
                else //JSON
                {
                    query = JsonHelper.GenerateQueryString(this.TumblrDomain, TumblrPostTypes.photo.ToString(), start);
                }

                this.DocumentManager.GetDocument(query);

                if ((this.ApiMode == ApiModeEnum.v2JSON.ToString() && this.DocumentManager.JsonDocument != null)
                    || (this.ApiMode == ApiModeEnum.v1XML.ToString() && this.DocumentManager.XmlDocument != null))
                {
                    DocumentManager.ImageSize = this.ImageSize;
                    HashSet<TumblrPost> posts = DocumentManager.GetPostListFromDoc(TumblrPostTypes.photo.ToString(), ApiMode);
                    return posts;
                }
                else
                {
                    this.StatusCode = ProcessingCodes.UnableDownload;
                    return new HashSet<TumblrPost>();
                }
            }
            catch
            {
                return new HashSet<TumblrPost>();
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
                string url = string.Empty;
                if (this.ApiMode == ApiModeEnum.v1XML.ToString())
                    url = XmlHelper.GenerateQueryString(this.TumblrURL, TumblrPostTypes.empty.ToString(),0,1);
                else
                {
                    url = JsonHelper.GenerateQueryString(this.TumblrDomain, TumblrPostTypes.empty.ToString(),0,1);
                }

                return url.TumblrExists(this.ApiMode);
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
        public TumblrBlog ParseBlogPosts(ParseModes parseMode)
        {
            try
            {
                this.StatusCode = ProcessingCodes.Crawling;
                this.ExistingImageList = FileHelper.GenerateFolderImageList(this.SaveLocation);
                string url = string.Empty;
                if (this.ApiMode == ApiModeEnum.v1XML.ToString())
                    url = XmlHelper.GenerateQueryString(this.TumblrURL, TumblrPostTypes.photo.ToString());
                else
                {
                    url = JsonHelper.GenerateQueryString(this.TumblrDomain, TumblrPostTypes.photo.ToString());
                }

                this.Blog.Posts = this.Blog.Posts != null ? this.Blog.Posts : new HashSet<TumblrPost>();

                int step;

                if (this.ApiMode == ApiModeEnum.v2JSON.ToString())
                    step = (int)PostStepEnum.JSON;
                else
                    step = (int)PostStepEnum.XML;

                if (this.TotalNumberOfPosts == 0)
                    this.TotalNumberOfPosts = this.Blog.TotalPosts;

                bool finished = false;

                this.PercentComplete = 0;

                if (parseMode == ParseModes.FullRescan)
                {
                    while (this.Offset < this.TotalNumberOfPosts && !this.IsCancelled)
                    {
                        HashSet<TumblrPost> posts = GetTumblrPostList(this.Offset);
                        this.Blog.Posts.UnionWith(posts);
                        GenerateImageListForDownload(posts);
                        this.NumberOfParsedPosts += this.Blog.Posts.Count;
                        this.PercentComplete = this.TotalNumberOfPosts > 0 ? (int)(((double)this.NumberOfParsedPosts / (double)this.TotalNumberOfPosts) * 100.00) : 0;
                        this.Offset += step;

                        if (this.GenerateLog)
                        {
                            UpdateLogFile(this.Blog.Name);
                        }
                        this.Blog.Posts = new HashSet<TumblrPost>();
                    }
                }
                else if (parseMode == ParseModes.NewestOnly)
                {
                    while (!finished && this.Offset < this.TotalNumberOfPosts && !this.IsCancelled)
                    {
                        HashSet<TumblrPost> posts = GetTumblrPostList(Offset);

                        HashSet<TumblrPost> existingHash = new HashSet<TumblrPost>((from p in posts
                                                                                    where this.ExistingImageList.Contains(p.Photos.Last().Filename,
                                                                                        StringComparer.OrdinalIgnoreCase)
                                                                                    select p));

                        posts.RemoveWhere(x => existingHash.Contains(x));

                        this.Blog.Posts.UnionWith(posts);
                        this.NumberOfParsedPosts += posts.Count;

                        if (existingHash.Count > 0 && !finished)
                        {
                            finished = true;
                        }

                        GenerateImageListForDownload(posts);
                        this.PercentComplete = this.TotalNumberOfPosts > 0 ? (int)(((double)this.NumberOfParsedPosts / (double)this.TotalNumberOfPosts) * 100.00) : 0;
                        this.Offset += step;

                        if (this.GenerateLog)
                        {
                            UpdateLogFile(this.Blog.Name);
                        }

                        this.Blog.Posts = new HashSet<TumblrPost>();
                    }
                }

                this.TotalNumberOfImages = this.ImageList.Count;

                if (this.ImageList.Count == 0)
                    this.Blog.Posts = new HashSet<TumblrPost>();

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
                this.DocumentManager.ApiMode = mode;
            }
            catch
            {
                return;
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
                string query;
                if (this.ApiMode == ApiModeEnum.v1XML.ToString()) //XML
                {
                    query = XmlHelper.GenerateQueryString(this.TumblrURL, TumblrPostTypes.photo.ToString(), 0, 1);
                }
                else //JSON
                {
                    query = JsonHelper.GenerateQueryString(this.TumblrDomain, TumblrPostTypes.photo.ToString(), 0, 1);
                }
                return this.DocumentManager.GetBlogInfo(query, this.Blog);
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
            this.TumblrPostLog = log;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public void UpdateLogFile(string name)
        {
            try
            {
                if (this.TumblrPostLog == null || this.TumblrPostLog.Blog.Name != name)
                {
                    this.TumblrPostLog = new SaveFile(name + ".log", this.Blog);
                }

                UpdateLogFile(this.TumblrPostLog);
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="log"></param>
        public void UpdateLogFile(SaveFile log)
        {
            try
            {
                foreach (TumblrPost post in Blog.Posts)
                {
                    log.Blog.Posts.RemoveWhere(p => p.Id == post.Id);

                    log.Blog.Posts.Add(post);
                    this.IsLogUpdated = true;
                }
            }
            catch
            {
                return;
            }
        }
    }
}