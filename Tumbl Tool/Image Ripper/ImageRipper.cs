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
        public HashSet<string> imageList;
        public bool isCancelled = false;
        public SaveFile log;
        public int parsed = 0;
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
        private HashSet<TumblrPost> oldPosts;
        private bool parsePhotoSets, parseJPEG, parsePNG, parseGIF;
        private string saveLocation;
        private string tumblrDomain = "";

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
            this.existingImageList = FileHelper.getImageListFromDir(this.saveLocation);

            this.maxNumPosts = endNum;

            this.errorList = new HashSet<string>();

            this.blog = blog;
            this.statusCode = processingCodes.OK;
            this.parsePhotoSets = parseSets;
            this.parseJPEG = parseJPEG;
            this.parsePNG = parsePNG;
            this.parseGIF = parseGIF;

            this.oldPosts = this.blog != null ? this.blog.posts != null ? new HashSet<TumblrPost>(this.blog.posts) : null : null;
            if (this.blog != null)
            {
                if (this.blog.posts != null)
                {
                    this.blog.posts.Clear();
                }
            }
            this.totalPosts = 0;
            this.imageList = new HashSet<string>();
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
                                this.imageList.Add(image.url);

                                //if (!this.commentsList.ContainsKey(image.filename))
                                //{
                                //    this.commentsList.Add(image.filename, caption);
                                //}
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }

            HashSet<string> removeHash = new HashSet<string>();

            if (!this.parseGIF)
            {
                removeHash.UnionWith(new HashSet<string>((from p in imageList where p.ToLower().EndsWith(".gif") select p)));
            }

            if (!this.parseJPEG)
            {
                removeHash.UnionWith(new HashSet<string>((from p in imageList where p.ToLower().EndsWith(".jpg") || p.ToLower().EndsWith(".jpeg") select p)));
            }

            if (!this.parsePNG)
            {
                removeHash.UnionWith(new HashSet<string>((from p in imageList where p.ToLower().EndsWith(".png") select p)));
            }

            imageList.RemoveWhere(x => removeHash.Contains(x));

            
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

            if (parseMode == (int)parseModes.FullRescan)
            {
                while (i < totalPosts && !isCancelled)
                {
                    HashSet<TumblrPost> posts = getTumblrPostList(i);
                    blog.posts.UnionWith(posts);
                    generateImageListForDownload(posts);
                    parsed += blog.posts.Count;
                    percentComplete = totalPosts > 0 ? (int)(((double)parsed / (double)totalPosts) * 100.00) : 0;
                    i += step;

                    if (this.generateLog)
                    {
                        saveLogFile(blog.name);
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
                            percentComplete = 100;
                        }
                        else if (!finished)
                        {
                            blog.posts.Add(post);
                        }
                    }
                    parsed += blog.posts.Count;
                    generateImageListForDownload(blog.posts);
                    percentComplete = totalPosts > 0 ? (int)(((double)parsed / (double)totalPosts) * 100.00) : 0;
                    i += step;
                    if (this.generateLog)
                    {
                        saveLogFile(blog.name);
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

        public void saveLogFile(string name)
        {
            if (log == null || log.blog.name != name)
            {
                log = new SaveFile(name + ".log", blog);
            }

            saveLogFile(log);
        }

        public void saveLogFile(SaveFile log)
        {
            bool logUpdated = false;
            foreach (TumblrPost post in blog.posts)
            {
                if (!log.blog.posts.ToList().Exists(p => p.id == post.id))
                {
                    log.blog.posts.Add(post);
                    logUpdated = true;
                }
            }

            if (logUpdated)
            {
                FileManager fileManager = new FileManager();
                fileManager.saveTumblrFile(saveLocation + @"\" + log.getFileName(), log);
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

        public void setLogFile(SaveFile log)
        {
            this.log = log;
        }
    }
}