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
        public Tumblr blog;
        public Dictionary<string, string> commentsList = new Dictionary<string, string>();
        public CrawlManager crawlManager;
        public List<string> imagesList;
        public SaveFile log;
        public int percentComplete = 0;
        public postProcessingCodes prevCode;
        public string queryXML = @"/api/read?type=" + tumblrPostTypes.photo.ToString() + "&num=" + ((int)postStepEnum.XML).ToString();
        public postProcessingCodes statusCode;
        public int totalImagesCount;
        public int totalPosts = 0;
        public string tumblrURL = "";
        private string apiKey = "SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY";
        private string apiMode;
        private List<string> errorList;
        private List<string> existingImageList;
        private string jsonBlogInfoQuery = "info";
        private string jsonCompletePath = "";
        private string jsonPostQuery = "posts";
        private string jsonURL = "http://api.tumblr.com/v2/blog";
        private int maxNumPosts = 0;
        private int offset = 0;
        private List<TumblrPost> oldPosts;
        public int parsed = 0;
        private bool parsePhotoSets, parseJPEG, parsePNG, parseGIF;
        private string saveLocation;
        private string tumblrDomain = "";

        public ImageRipper(Tumblr blog, string saveLocation, bool parseSets = true, bool parseJPEG = true, bool parsePNG = true, bool parseGIF = true, int startNum = 0, int endNum = 0, string apiMode = "XML")
        {
            this.tumblrURL = FileHelper.fixURL(blog.cname);
            this.tumblrDomain = blog.cname.Substring(7);

            this.offset = startNum;
            this.saveLocation = saveLocation;
            this.existingImageList = FileHelper.getImageListFromDir(this.saveLocation);

            this.maxNumPosts = endNum;

            this.errorList = new List<string>();

            this.blog = blog;
            this.statusCode = new postProcessingCodes();
            this.parsePhotoSets = parseSets;
            this.parseJPEG = parseJPEG;
            this.parsePNG = parsePNG;
            this.parseGIF = parseGIF;

            this.oldPosts = this.blog != null ? this.blog.posts != null ? new List<TumblrPost>(this.blog.posts) : null : null;
            if (this.blog != null)
            {
                if (this.blog.posts != null)
                {
                    this.blog.posts.Clear();
                }
            }
            this.totalPosts = 0;
            this.imagesList = new List<string>();
            this.totalImagesCount = 0;
            this.crawlManager = new CrawlManager();
            setAPIMode(apiMode);
        }

        public void generateImageListForDownload(List<TumblrPost> posts)
        {
            HashSet<TumblrPost> setToRemove = new HashSet<TumblrPost>((from p in posts where !p.isPhotoset() && existingImageList.Contains(p.fileName) select p));
            posts.RemoveAll(x => setToRemove.Contains(x));

            foreach (PhotoPost post in posts)
            {
                if (post.isPhotoset())
                {
                    foreach (PhotoSetImage image in post.photoset)
                    {
                        if (!existingImageList.Contains(image.filename))
                        {
                            string caption = post.caption;
                            caption = CommonHelper.NewLineToBreak(post.caption, "</p>");
                            caption = CommonHelper.NewLineToBreak(post.caption, "<\n\r\n");
                            caption = CommonHelper.StripTags(caption);
                            this.imagesList.Add(image.imageURL);
                            this.commentsList.Add(image.filename, caption);
                        }
                    }
                }
                else
                {
                    if (!existingImageList.Contains(post.fileName))
                    {
                        string caption = post.caption;
                        caption = CommonHelper.NewLineToBreak(post.caption, "</p>");
                        caption = CommonHelper.NewLineToBreak(post.caption, "<\n\r\n");
                        caption = CommonHelper.StripTags(caption);
                        this.imagesList.Add(post.imageURL);

                        if (caption != null)
                        {
                            try
                            {
                                this.commentsList.Add(post.fileName, caption);
                            }
                            catch
                            {
                                //do nothing
                            }
                        }
                    }
                }
            }

            HashSet<string> removeHash = new HashSet<string>();

            if (!this.parseGIF)
            {
                removeHash.UnionWith(new HashSet<string>((from p in imagesList where p.ToLower().EndsWith(".gif") select p)));
            }

            if (!this.parseJPEG)
            {
                removeHash.UnionWith(new HashSet<string>((from p in imagesList where p.ToLower().EndsWith(".jpg") || p.ToLower().EndsWith(".jpeg") select p)));
            }

            if (!this.parsePNG)
            {
                removeHash.UnionWith(new HashSet<string>((from p in imagesList where p.ToLower().EndsWith(".png") select p)));
            }

            imagesList.RemoveAll(x => removeHash.Contains(x));

            this.totalImagesCount = this.imagesList.Count;
        }

        public List<TumblrPost> getTumblrPostList(int start = 0)
        {
            if (this.apiMode == "XML") //XML
            {
                string query = string.Copy(this.queryXML);
                if (start != 0)
                {
                    query += "&start=" + start.ToString();
                }

                if (maxNumPosts != 0)
                {
                    query += "&end=" + maxNumPosts.ToString();
                }

                if (WebHelper.webURLExists(@tumblrURL + query))
                {
                    crawlManager.getDocument(@tumblrURL + query);
                    List<TumblrPost> posts = crawlManager.getPostList(tumblrPostTypes.photo.ToString(), apiMode);
                    return posts;
                }
                else
                {
                    statusCode = postProcessingCodes.UnableDownload;
                    return null;
                }
            }
            else //JSON
            {
                string query = string.Copy(this.jsonURL);

                query += "/" + tumblrDomain + jsonPostQuery;
                query += "?api_key=" + apiKey;
                query += "&type=photo";

                jsonCompletePath = query;

                if (start != 0)
                {
                    query += "&offset=" + start.ToString();
                }

                if (maxNumPosts != 0)
                {
                    query += "&end=" + maxNumPosts.ToString();
                }

                if (WebHelper.webURLExists(@query))
                {
                    crawlManager.getJSONDocument(query);
                    List<TumblrPost> posts = crawlManager.getPostList(tumblrPostTypes.photo.ToString(), apiMode);
                    return posts;
                }
                else
                {
                    statusCode = postProcessingCodes.UnableDownload;
                    return null;
                }
            }
        }

        public Tumblr parseBlogPosts(int parseMode)
        {
            statusCode = postProcessingCodes.Started;
            string url = "";
            if (apiMode == apiModeEnum.XML.ToString())
                url = tumblrURL + queryXML;
            else
            {
                string query = string.Copy(this.jsonURL);

                query += "/" + tumblrDomain + jsonPostQuery;
                query += "?api_key=" + apiKey;
                query += "&type=" + tumblrPostTypes.photo.ToString();

                jsonCompletePath = query;
                url = jsonCompletePath;
            }

            this.blog.posts = this.blog.posts != null ? this.blog.posts : new List<TumblrPost>();

            int step;

            if (apiMode == apiModeEnum.JSON.ToString())
                step = (int)postStepEnum.JSON;
            else
                step = (int)postStepEnum.XML;

            if (crawlManager.isValidTumblr(url))
            {
                //setBlogInfo();

                // crawlManager.getDocument(url);

                if (this.totalPosts == 0)
                    this.totalPosts = blog.totalPosts;

                statusCode = postProcessingCodes.Crawling;
                // totalPosts = this.blog.totalPosts;

                bool finished = false;

                int i = offset;

                if (parseMode == (int)parseModes.FullRescan)
                {
                    while (i < totalPosts)
                    {
                        blog.posts.AddRange(getTumblrPostList(i));
                        parsed = blog.posts.Count;
                        percentComplete = totalPosts > 0 ? (int)(((double)parsed / (double)totalPosts) * 100.00) : 0;
                        i += step;
                    }
                }
                else if (parseMode == (int)parseModes.NewestOnly)
                {
                    while (!finished && i < totalPosts)
                    {
                        List<TumblrPost> posts = getTumblrPostList(i);

                        foreach (TumblrPost post in posts)
                        {
                            if (existingImageList.Contains(post.fileName))
                            {
                                finished = true;
                                percentComplete = 100;
                            }
                            else if (!finished)
                            {
                                blog.posts.Add(post);
                            }
                        }
                        parsed = blog.posts.Count;
                        percentComplete = totalPosts > 0 ? (int)(((double)parsed / (double)totalPosts) * 100.00) : 0;
                        i += step;
                    }
                }
                saveLogFile(blog.name);
                statusCode = postProcessingCodes.Parsing;

                generateImageListForDownload(blog.posts);

                if (imagesList.Count == 0)
                    blog.posts.Clear();

                statusCode = postProcessingCodes.Done;
            }
            else
            {
                statusCode = postProcessingCodes.invalidURL;
            }

            prevCode = statusCode;
            return blog;
        }

        public void saveLogFile(string name)
        {
            if (log == null)
            {
                SaveFile saveFile = new SaveFile(name + ".log", blog);
                FileManager fileManager = new FileManager();
                fileManager.saveTumblrFile(saveLocation + @"\" + saveFile.getFileName(), saveFile);
            }
            else
            {
                saveLogFile(log);
            }
        }

        public void saveLogFile(SaveFile log)
        {
            foreach (TumblrPost post in blog.posts)
            {
                if (!log.blog.posts.Exists(p => p.id == post.id))
                {
                    log.blog.posts.Add(post);
                }
            }
            FileManager fileManager = new FileManager();
            fileManager.saveTumblrFile(saveLocation + @"\" + log.getFileName(), log);
        }

        public void setAPIMode(string mode)
        {
            this.apiMode = mode; // XML or JSON
            this.crawlManager.setMode(mode);
        }

        public bool setBlogInfo()
        {
            if (this.apiMode == apiModeEnum.XML.ToString()) //XML
            {
                string query = @"/api/read?type=photo&num=1";
                return crawlManager.setBlogInfo(tumblrURL + query, this.blog);
            }
            else //JSON
            {
                string query = string.Copy(this.jsonURL);

                query += "/" + tumblrDomain + jsonPostQuery;
                query += "?api_key=" + apiKey;
                query += "&type=" + tumblrPostTypes.photo.ToString();
                query += "&limit=1";

                jsonCompletePath = query;

                return crawlManager.setBlogInfo(query, this.blog);
            }
        }

        public void setLogFile(SaveFile log)
        {
            this.log = log;
        }
    }
}