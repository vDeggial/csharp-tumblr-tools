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
        public TumblrBlog blog;
        public Dictionary<string, string> commentsList = new Dictionary<string, string>();
        public CrawlManager crawlManager;
        public HashSet<PhotoPostImage> imageList;
        public bool isCancelled = false;
        public bool logUpdated;
        public int parsedPosts = 0;
        public int percentComplete = 0;
        public string saveLocation;
        public processingCodes statusCode;
        public int totalImagesCount;
        public int totalPosts = 0;
        public SaveFile tumblrPostLog;
        public string tumblrURL = "";
        private string apiMode;
        private HashSet<string> errorList;
        private HashSet<string> existingImageList;
        private bool generateLog;
        private int maxNumPosts = 0;
        private int offset = 0;
        private bool parsePhotoSets, parseJPEG, parsePNG, parseGIF;
        private string tumblrDomain = "";

        public ImageRipper()
        {
        }

        public ImageRipper(ref TumblrBlog blog, ref string saveLocation, bool generateLog = false, bool parseSets = true, bool parseJPEG = true, bool parsePNG = true, bool parseGIF = true, int startNum = 0, int endNum = 0, string apiMode = "JSON")
        {
            this.tumblrURL = FileHelper.fixURL(blog.url);
            this.tumblrDomain = CommonHelper.getDomainName(blog.url);

            this.generateLog = generateLog;

            this.offset = startNum;
            this.saveLocation = saveLocation;

            this.maxNumPosts = endNum;

            this.errorList = new HashSet<string>();

            this.blog = blog;
            this.statusCode = processingCodes.OK;
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
            this.totalPosts = 0;
            this.imageList = new HashSet<PhotoPostImage>();
            this.totalImagesCount = 0;
            this.crawlManager = new CrawlManager();
            setAPIMode(this.apiMode);
        }

        public void generateImageListForDownload(HashSet<TumblrPost> posts)
        {
            //HashSet<TumblrPost> setToRemove = new HashSet<TumblrPost>((from p in posts where !p.isPhotoset() && existingImageList.Contains(p.fileName) select p));
            //posts.RemoveAll(x => setToRemove.Contains(x));

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
                        if (!this.existingImageList.Contains(image.filename))
                        {
                            try
                            {
                                string caption = post.caption;
                                caption = CommonHelper.NewLineToBreak(post.caption, "</p>");
                                caption = CommonHelper.NewLineToBreak(post.caption, "<\n\r\n");
                                caption = CommonHelper.StripTags(caption);
                                this.imageList.Add(image);

                                //if (!this.commentsList.ContainsKey(image.filename))
                                //{
                                //    this.commentsList.Add(image.filename, caption);
                                //}
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

            this.imageList.RemoveWhere(x => removeHash.Any(y => x.filename == y.filename));
        }

        public HashSet<TumblrPost> getTumblrPostList(int start = 0)
        {
            try
            {
                string query;
                if (this.apiMode == apiModeEnum.XML.ToString()) //XML
                {
                    query = XMLHelper.getQueryString(this.tumblrURL, tumblrPostTypes.photo.ToString(), start);
                }
                else //JSON
                {
                    query = JSONHelper.getQueryString(this.tumblrDomain, tumblrPostTypes.photo.ToString(), start);
                }

                if (this.crawlManager.isValidTumblr(@query))
                {
                    this.crawlManager.getDocument(query);
                    HashSet<TumblrPost> posts = crawlManager.getPostList(tumblrPostTypes.photo.ToString(), apiMode);
                    return posts;
                }
                else
                {
                    this.statusCode = processingCodes.UnableDownload;
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool isValidTumblr()
        {
            try
            {
                string url = string.Empty;
                if (this.apiMode == apiModeEnum.XML.ToString())
                    url = XMLHelper.getQueryString(this.tumblrURL, tumblrPostTypes.photo.ToString());
                else
                {
                    url = JSONHelper.getQueryString(this.tumblrDomain, tumblrPostTypes.photo.ToString());
                }

                return this.crawlManager.isValidTumblr(url);
            }
            catch
            {
                return false;
            }
        }

        public TumblrBlog parseBlogPosts(int parseMode)
        {
            try
            {
                this.statusCode = processingCodes.Crawling;
                this.existingImageList = FileHelper.getImageListFromDir(this.saveLocation);
                string url = string.Empty;
                if (this.apiMode == apiModeEnum.XML.ToString())
                    url = XMLHelper.getQueryString(this.tumblrURL, tumblrPostTypes.photo.ToString());
                else
                {
                    url = JSONHelper.getQueryString(this.tumblrDomain, tumblrPostTypes.photo.ToString());
                }

                this.blog.posts = this.blog.posts != null ? this.blog.posts : new HashSet<TumblrPost>();

                int step;

                if (this.apiMode == apiModeEnum.JSON.ToString())
                    step = (int)postStepEnum.JSON;
                else
                    step = (int)postStepEnum.XML;

                //setBlogInfo();

                // crawlManager.getDocument(url);

                if (this.totalPosts == 0)
                    this.totalPosts = this.blog.totalPosts;

                // totalPosts = this.blog.totalPosts;

                bool finished = false;

                int i = this.offset;

                this.percentComplete = 0;

                if (parseMode == (int)parseModes.FullRescan)
                {
                    while (i < this.totalPosts && !this.isCancelled)
                    {
                        HashSet<TumblrPost> posts = getTumblrPostList(i);
                        this.blog.posts.UnionWith(posts);
                        generateImageListForDownload(posts);
                        this.parsedPosts += this.blog.posts.Count;
                        this.percentComplete = this.totalPosts > 0 ? (int)(((double)this.parsedPosts / (double)this.totalPosts) * 100.00) : 0;
                        i += step;

                        if (this.generateLog)
                        {
                            updateLogFile(this.blog.name);
                        }
                        this.blog.posts.Clear();
                    }
                }
                else if (parseMode == (int)parseModes.NewestOnly)
                {
                    while (!finished && i < this.totalPosts && !this.isCancelled)
                    {
                        HashSet<TumblrPost> posts = getTumblrPostList(i);

                        HashSet<TumblrPost> existingHash = new HashSet<TumblrPost>((from p in posts where this.existingImageList.Any(s => s.ToLower() == p.photos.First().filename.ToLower()) select p));

                        posts.RemoveWhere(x => existingHash.Any(y => x.id == y.id));

                        this.blog.posts.UnionWith(posts);
                        this.parsedPosts += posts.Count;

                        if (existingHash.Count > 0 && !finished)
                        {
                            finished = true;
                        }

                        //foreach (TumblrPost post in posts)
                        //{
                        //    if (post.photos.Count > 0 && this.existingImageList.Any(p => p.ToLower() == post.photos.First().filename.ToLower()))
                        //    {
                        //        finished = true;
                        //        //percentComplete = 100;
                        //    }
                        //    else if (!finished)
                        //    {
                        //        this.blog.posts.Add(post);
                        //        this.parsedPosts++;
                        //    }
                        //}
                        // this.parsedPosts += this.blog.posts.Count;
                        generateImageListForDownload(this.blog.posts);
                        this.percentComplete = this.totalPosts > 0 ? (int)(((double)this.parsedPosts / (double)this.totalPosts) * 100.00) : 0;
                        i += step;

                        if (this.generateLog)
                        {
                            updateLogFile(this.blog.name);
                        }

                        this.blog.posts.Clear();
                    }
                }

                //  statusCode = processingCodes.Parsing;

                // generateImageListForDownload(blog.posts);

                this.totalImagesCount = this.imageList.Count;

                if (this.imageList.Count == 0)
                    this.blog.posts.Clear();

                return this.blog;
            }
            catch
            {
                return this.blog;
            }
        }

        public void setAPIMode(string mode)
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

        public bool setBlogInfo()
        {
            try
            {
                string query;
                if (this.apiMode == apiModeEnum.XML.ToString()) //XML
                {
                    query = XMLHelper.getQueryString(this.tumblrURL, tumblrPostTypes.photo.ToString(), 0, 1);
                }
                else //JSON
                {
                    query = JSONHelper.getQueryString(this.tumblrDomain, tumblrPostTypes.photo.ToString(), 0, 1);
                }
                return this.crawlManager.setBlogInfo(query, this.blog);
            }
            catch
            {
                return false;
            }
        }

        public void setLogFile(ref SaveFile log)
        {
            this.tumblrPostLog = log;
        }

        public void updateLogFile(string name)
        {
            try
            {
                if (this.tumblrPostLog == null || this.tumblrPostLog.blog.name != name)
                {
                    this.tumblrPostLog = new SaveFile(name + ".log", this.blog);
                }

                updateLogFile(this.tumblrPostLog);
            }
            catch
            {
                return;
            }
        }

        public void updateLogFile(SaveFile log)
        {
            try
            {
                foreach (TumblrPost post in blog.posts)
                {
                    log.blog.posts.RemoveWhere(p => p.id == post.id);

                    log.blog.posts.Add(post);
                    this.logUpdated = true;
                }
            }
            catch
            {
                return;
            }
        }
    }
}