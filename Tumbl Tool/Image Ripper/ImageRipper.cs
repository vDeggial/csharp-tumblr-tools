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
        public SaveFile tumblrPostLog;
        public int parsedPosts = 0;
        public int percentComplete = 0;
        public processingCodes statusCode;
        public int totalImagesCount;
        public int totalPosts = 0;
        public string tumblrURL = "";
        private string apiMode;
        private HashSet<string> errorList;
        private HashSet<string> existingImageList;
        private bool generateLog;
        private int maxNumPosts = 0;
        private int offset = 0;
        private bool parsePhotoSets, parseJPEG, parsePNG, parseGIF;
        public string saveLocation;
        private string tumblrDomain = "";

        public bool logUpdated;

        public ImageRipper()
        {
        }

        public ImageRipper(TumblrBlog blog, string saveLocation, bool generateLog = false, bool parseSets = true, bool parseJPEG = true, bool parsePNG = true, bool parseGIF = true, int startNum = 0, int endNum = 0, string apiMode = "JSON")
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
            setAPIMode(apiMode);
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
                        if (!existingImageList.Contains(image.filename))
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

            imageList.RemoveWhere(x => removeHash.Any(y => x.filename == y.filename));
        }

        public HashSet<TumblrPost> getTumblrPostList(int start = 0)
        {
            string query;
            if (this.apiMode == apiModeEnum.XML.ToString()) //XML
            {
                query = XMLHelper.getQueryString(tumblrURL, tumblrPostTypes.photo.ToString(), start);
            }
            else //JSON
            {
                query = JSONHelper.getQueryString(tumblrDomain, tumblrPostTypes.photo.ToString(), start);
            }

            if (crawlManager.isValidTumblr(@query))
            {
                crawlManager.getDocument(query);
                HashSet<TumblrPost> posts = crawlManager.getPostList(tumblrPostTypes.photo.ToString(), apiMode);
                return posts;
            }
            else
            {
                statusCode = processingCodes.UnableDownload;
                return null;
            }
        }

        public bool isValidTumblr()
        {
            string url = string.Empty;
            if (apiMode == apiModeEnum.XML.ToString())
                url = XMLHelper.getQueryString(this.tumblrURL, tumblrPostTypes.photo.ToString());
            else
            {
                url = JSONHelper.getQueryString(tumblrDomain, tumblrPostTypes.photo.ToString());
            }

            return crawlManager.isValidTumblr(url);
        }

        public TumblrBlog parseBlogPosts(int parseMode)
        {
            statusCode = processingCodes.Starting;
            this.existingImageList = FileHelper.getImageListFromDir(this.saveLocation);
            string url = string.Empty;
            if (apiMode == apiModeEnum.XML.ToString())
                url = XMLHelper.getQueryString(tumblrURL, tumblrPostTypes.photo.ToString());
            else
            {
                url = JSONHelper.getQueryString(tumblrDomain, tumblrPostTypes.photo.ToString());
            }

            this.blog.posts = this.blog.posts != null ? this.blog.posts : new HashSet<TumblrPost>();

            int step;

            if (apiMode == apiModeEnum.JSON.ToString())
                step = (int)postStepEnum.JSON;
            else
                step = (int)postStepEnum.XML;

            //setBlogInfo();

            // crawlManager.getDocument(url);

            if (this.totalPosts == 0)
                this.totalPosts = blog.totalPosts;

            statusCode = processingCodes.Crawling;
            // totalPosts = this.blog.totalPosts;

            bool finished = false;

            int i = offset;

            percentComplete = 0;

            if (parseMode == (int)parseModes.FullRescan)
            {
                while (i < totalPosts && !isCancelled)
                {
                    HashSet<TumblrPost> posts = getTumblrPostList(i);
                    blog.posts.UnionWith(posts);
                    generateImageListForDownload(posts);
                    parsedPosts += blog.posts.Count;
                    percentComplete = totalPosts > 0 ? (int)(((double)parsedPosts / (double)totalPosts) * 100.00) : 0;
                    i += step;

                    if (this.generateLog)
                    {
                        updateLogFile(blog.name);
                    }
                    blog.posts.Clear();
                }
            }
            else if (parseMode == (int)parseModes.NewestOnly)
            {
                while (!finished && i < totalPosts && !isCancelled)
                {
                    HashSet<TumblrPost> posts = getTumblrPostList(i);

                    foreach (TumblrPost post in posts)
                    {
                        if (post.photos.Count > 0 && existingImageList.Contains(post.photos.First().filename))
                        {
                            finished = true;
                            //percentComplete = 100;
                        }
                        else if (!finished)
                        {
                            blog.posts.Add(post);
                        }
                    }
                    parsedPosts += blog.posts.Count;
                    generateImageListForDownload(blog.posts);
                    percentComplete = totalPosts > 0 ? (int)(((double)parsedPosts / (double)totalPosts) * 100.00) : 0;
                    i += step;

                    if (this.generateLog)
                    {
                        updateLogFile(blog.name);
                    }

                    blog.posts.Clear();
                }
            }

            //  statusCode = processingCodes.Parsing;

            // generateImageListForDownload(blog.posts);

            this.totalImagesCount = this.imageList.Count;

            if (imageList.Count == 0)
                blog.posts.Clear();

            return blog;
        }

        public void updateLogFile(string name)
        {
            if (tumblrPostLog == null || tumblrPostLog.blog.name != name)
            {
                tumblrPostLog = new SaveFile(name + ".log", blog);
            }

            updateLogFile(tumblrPostLog);
        }

        public void updateLogFile(SaveFile log)
        {
            foreach (TumblrPost post in blog.posts)
            {
                log.blog.posts.RemoveWhere(p => p.id == post.id);

                log.blog.posts.Add(post);
                this.logUpdated = true;
            }
        }

        public void setAPIMode(string mode)
        {
            this.apiMode = mode; // XML or JSON
            this.crawlManager.mode = mode;
        }

        public bool setBlogInfo()
        {
            string query;
            if (this.apiMode == apiModeEnum.XML.ToString()) //XML
            {
                query = XMLHelper.getQueryString(tumblrURL, tumblrPostTypes.photo.ToString(), 0, 1);
            }
            else //JSON
            {
                query = JSONHelper.getQueryString(tumblrDomain, tumblrPostTypes.photo.ToString(), 0, 1);
            }
            return crawlManager.setBlogInfo(query, this.blog);
        }

        public void setLogFile(ref SaveFile log)
        {
            this.tumblrPostLog = log;
        }
    }
}