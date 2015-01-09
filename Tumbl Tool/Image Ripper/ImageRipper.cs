/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: December, 2014
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.Linq;
using Tumblr_Tool.Common_Helpers;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Image_Ripper
{
    public class ImageRipper
    {
        public ImageRipper()
        {
        }

        public ImageRipper(TumblrBlog blog, string saveLocation, bool generateLog = false, bool parseSets = true, bool parseJPEG = true, bool parsePNG = true, bool parseGIF = true, int startNum = 0, int endNum = 0, string apiMode = "JSON")
        {
            this.tumblrURL = FileHelper.FixURL(blog.url);
            this.tumblrDomain = CommonHelper.GetDomainName(blog.url);

            this.generateLog = generateLog;

            this.offset = startNum;
            this.saveLocation = saveLocation;

            this.maximumNUmberOfPosts = endNum;

            this.errorList = new HashSet<string>();

            this.blog = blog;
            this.statusCode = ProcessingCodes.OK;
            this.parsePhotoSets = parseSets;
            this.parseJPEG = parseJPEG;
            this.parsePNG = parsePNG;
            this.parseGIF = parseGIF;

            if (this.blog != null)
            {
                if (this.blog.posts != null)
                {
                    this.blog.posts.Clear();
                }
            }
            this.totalNumberOfPosts = 0;
            this.imageList = new HashSet<PhotoPostImage>();
            this.totalNumberOfImages = 0;
            this.crawlManager = new CrawlManager();
            SetAPIMode(this.apiMode);
            this.commentsList = new Dictionary<string, string>();
        }

        public string apiMode { get; set; }

        public TumblrBlog blog { get; set; }

        public Dictionary<string, string> commentsList { get; set; }

        public CrawlManager crawlManager { get; set; }

        public HashSet<string> errorList { get; set; }

        public HashSet<string> existingImageList { get; set; }

        public bool generateLog { get; set; }

        public HashSet<PhotoPostImage> imageList { get; set; }

        public bool isCancelled { get; set; }

        public bool isLogUpdated { get; set; }

        public int maximumNUmberOfPosts { get; set; }

        public int numberOfParsedPosts { get; set; }

        public int offset { get; set; }

        public bool parseGIF { get; set; }

        public bool parseJPEG { get; set; }

        public bool parsePhotoSets { get; set; }

        public bool parsePNG { get; set; }

        public int percentComplete { get; set; }

        public string saveLocation { get; set; }

        public ProcessingCodes statusCode { get; set; }

        public int totalNumberOfImages { get; set; }

        public int totalNumberOfPosts { get; set; }

        public string tumblrDomain { get; set; }

        public SaveFile tumblrPostLog { get; set; }

        public string tumblrURL { get; set; }

        public void GenerateImageListForDownload(HashSet<TumblrPost> posts)
        {

            foreach (PhotoPost post in posts)
            {
                if (!this.parsePhotoSets && post.photos.Count > 1)
                {
                    // do not parse images from photoset
                }
                else
                {
                    foreach (PhotoPostImage image in post.photos)
                    {
                        if (!this.existingImageList.Contains(image.filename, StringComparer.OrdinalIgnoreCase))
                        {
                            try
                            {
                                //post.caption = CommonHelper.NewLineToBreak(post.caption, "</p>", string.Empty);
                                
                                //post.caption = post.caption.StripTags();
                                this.imageList.Add(image);
                            }
                            catch
                            {
                                return;
                            }
                        }
                        else
                        {
                            image.downloaded = true;
                        }
                    }
                }
            }

            if (this.imageList.Count > 0)
            {
                HashSet<PhotoPostImage> removeHash = new HashSet<PhotoPostImage>();

                if (!this.parseGIF)
                {
                    removeHash.UnionWith(new HashSet<PhotoPostImage>((from p in imageList where p.filename.ToLower().EndsWith(".gif") select p)));
                }

                if (!this.parseJPEG)
                {
                    removeHash.UnionWith(new HashSet<PhotoPostImage>((from p in imageList where p.filename.ToLower().EndsWith(".jpg") || p.filename.ToLower().EndsWith(".jpeg") select p)));
                }

                if (!this.parsePNG)
                {
                    removeHash.UnionWith(new HashSet<PhotoPostImage>((from p in imageList where p.filename.ToLower().EndsWith(".png") select p)));
                }

                this.imageList.RemoveWhere(x => removeHash.Contains(x));
            }
        }

        public HashSet<TumblrPost> GetTumblrPostList(int start = 0)
        {
            try
            {
                string query;
                if (this.apiMode == ApiModeEnum.XML.ToString()) //XML
                {
                    query = XMLHelper.GetQueryString(this.tumblrURL, TumblrPostTypes.photo.ToString(), start);
                }
                else //JSON
                {
                    query = JSONHelper.GenerateQueryString(this.tumblrDomain, TumblrPostTypes.photo.ToString(), start);
                }

                this.crawlManager.GetDocument(query);

                if (this.crawlManager.jsonDocument != null)
                {
                    HashSet<TumblrPost> posts = crawlManager.GetPostList(TumblrPostTypes.photo.ToString(), apiMode);
                    return posts;
                }
                else
                {
                    this.statusCode = ProcessingCodes.UnableDownload;
                    return new HashSet<TumblrPost>();
                }
            }
            catch
            {
                return new HashSet<TumblrPost>();
            }
        }

        public bool IsValidTumblr()
        {
            try
            {
                string url = string.Empty;
                if (this.apiMode == ApiModeEnum.XML.ToString())
                    url = XMLHelper.GetQueryString(this.tumblrURL, TumblrPostTypes.photo.ToString());
                else
                {
                    url = JSONHelper.GenerateQueryString(this.tumblrDomain, TumblrPostTypes.photo.ToString());
                }

                return this.crawlManager.IsValidTumblr(url);
            }
            catch
            {
                return false;
            }
        }

        public TumblrBlog ParseBlogPosts(ParseModes parseMode)
        {
            try
            {
                this.statusCode = ProcessingCodes.Crawling;
                this.existingImageList = FileHelper.GetImageListFromDir(this.saveLocation);
                string url = string.Empty;
                if (this.apiMode == ApiModeEnum.XML.ToString())
                    url = XMLHelper.GetQueryString(this.tumblrURL, TumblrPostTypes.photo.ToString());
                else
                {
                    url = JSONHelper.GenerateQueryString(this.tumblrDomain, TumblrPostTypes.photo.ToString());
                }

                this.blog.posts = this.blog.posts != null ? this.blog.posts : new HashSet<TumblrPost>();

                int step;

                if (this.apiMode == ApiModeEnum.JSON.ToString())
                    step = (int)PostStepEnum.JSON;
                else
                    step = (int)PostStepEnum.XML;

                if (this.totalNumberOfPosts == 0)
                    this.totalNumberOfPosts = this.blog.totalPosts;

                bool finished = false;


                this.percentComplete = 0;

                if (parseMode == ParseModes.FullRescan)
                {
                    while (this.offset < this.totalNumberOfPosts && !this.isCancelled)
                    {
                        HashSet<TumblrPost> posts = GetTumblrPostList(this.offset);
                        this.blog.posts.UnionWith(posts);
                        GenerateImageListForDownload(posts);
                        this.numberOfParsedPosts += this.blog.posts.Count;
                        this.percentComplete = this.totalNumberOfPosts > 0 ? (int)(((double)this.numberOfParsedPosts / (double)this.totalNumberOfPosts) * 100.00) : 0;
                        this.offset += step;

                        if (this.generateLog)
                        {
                            UpdateLogFile(this.blog.name);
                        }
                        this.blog.posts = new HashSet<TumblrPost>();
                    }
                }
                else if (parseMode == ParseModes.NewestOnly)
                {
                    while (!finished && this.offset < this.totalNumberOfPosts && !this.isCancelled)
                    {
                        HashSet<TumblrPost> posts = GetTumblrPostList(offset);

                        HashSet<TumblrPost> existingHash = new HashSet<TumblrPost>((from p in posts
                                                                                    where this.existingImageList.Contains(p.photos.Last().filename,
                                                                                        StringComparer.OrdinalIgnoreCase)
                                                                                    select p));

                        posts.RemoveWhere(x => existingHash.Contains(x));

                        this.blog.posts.UnionWith(posts);
                        this.numberOfParsedPosts += posts.Count;

                        if (existingHash.Count > 0 && !finished)
                        {
                            finished = true;
                        }

                        GenerateImageListForDownload(this.blog.posts);
                        this.percentComplete = this.totalNumberOfPosts > 0 ? (int)(((double)this.numberOfParsedPosts / (double)this.totalNumberOfPosts) * 100.00) : 0;
                        this.offset += step;

                        if (this.generateLog)
                        {
                            UpdateLogFile(this.blog.name);
                        }

                        this.blog.posts = new HashSet<TumblrPost>();
                    }
                }

                this.totalNumberOfImages = this.imageList.Count;

                if (this.imageList.Count == 0)
                    this.blog.posts = new HashSet<TumblrPost>();

                return this.blog;
            }
            catch
            {
                return this.blog;
            }
        }

        public void SetAPIMode(string mode)
        {
            try
            {
                this.apiMode = mode; // XML or JSON
                this.crawlManager.mode = mode;
            }
            catch
            {
                return;
            }
        }

        public bool SetBlogInfo()
        {
            try
            {
                string query;
                if (this.apiMode == ApiModeEnum.XML.ToString()) //XML
                {
                    query = XMLHelper.GetQueryString(this.tumblrURL, TumblrPostTypes.photo.ToString(), 0, 1);
                }
                else //JSON
                {
                    query = JSONHelper.GenerateQueryString(this.tumblrDomain, TumblrPostTypes.photo.ToString(), 0, 1);
                }
                return this.crawlManager.SetBlogInfo(query, this.blog);
            }
            catch
            {
                return false;
            }
        }

        public void SetLogFile(SaveFile log)
        {
            this.tumblrPostLog = log;
        }

        public void UpdateLogFile(string name)
        {
            try
            {
                if (this.tumblrPostLog == null || this.tumblrPostLog.blog.name != name)
                {
                    this.tumblrPostLog = new SaveFile(name + ".log", this.blog);
                }

                UpdateLogFile(this.tumblrPostLog);
            }
            catch
            {
                return;
            }
        }

        public void UpdateLogFile(SaveFile log)
        {
            try
            {
                foreach (TumblrPost post in blog.posts)
                {
                    log.blog.posts.RemoveWhere(p => p.id == post.id);

                    log.blog.posts.Add(post);
                    this.isLogUpdated = true;
                }
            }
            catch
            {
                return;
            }
        }
    }
}