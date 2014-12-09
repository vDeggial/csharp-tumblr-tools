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

using System.Linq;
using Tumblr_Tool.Common_Helpers;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Managers;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Tumblr_Stats
{
    public class TumblrStats
    {
        public int answerPosts;
        public int audioPosts;
        public Tumblr blog;
        public int chatPosts;
        public int linkPosts;
        public int maxNumPosts = 0;
        public int parsed = 0;
        public int photoPosts;
        public int quotePosts;
        public processingCodes statusCode;
        public int textPosts;
        public int totalPosts;
        public int videoPosts;
        private string apiMode;
        private CrawlManager crawlManager;
        private int found = 0;
        private int start;
        private int step = (int)postStepEnum.XML;
        private string tumblrDomain = "";
        private string url;

        public TumblrStats()
        {

        }

        public TumblrStats(Tumblr blog, string url, string apiMode, int startNum = 0, int endNum = 0)
        {
            crawlManager = new CrawlManager();
            setAPIMode(apiMode);

            if (blog == null)
            {
                this.blog = new Tumblr();

            }
            else
            {
                this.blog = blog;
                
            }

            this.blog.cname = url;
            this.tumblrDomain = CommonHelper.getDomainName(url);
            setBlogInfo();
            this.url = FileHelper.fixURL(this.blog.cname);
            this.tumblrDomain = CommonHelper.getDomainName(this.url);

            this.maxNumPosts = endNum;
            this.start = startNum;

            
            this.step = (int)postStepEnum.JSON; //20 for JSON, 50 for XML
        }

        public Tumblr parsePosts()
        {
            string url = XMLHelper.getQueryString(this.url, tumblrPostTypes.empty.ToString(), 0);
            step = (int)postStepEnum.XML;

            if (this.crawlManager.mode == apiModeEnum.JSON.ToString())
            {
                url = JSONHelper.getQueryString(tumblrDomain, tumblrPostTypes.empty.ToString(), 0);
                step = (int)postStepEnum.JSON;
            }

            if (crawlManager.isValidTumblr(url))
            {
                //setBlogInfo();

                // crawlManager.getDocument(url);
                this.totalPosts = this.blog.totalPosts;

                int endCount;

                if (maxNumPosts != 0)
                    endCount = maxNumPosts;
                else
                    endCount = totalPosts;

                int i = start;

                this.parsed = 0;

                string prefix = string.Empty;

                while (i < this.totalPosts)
                {
                    if (this.crawlManager.mode == apiModeEnum.JSON.ToString())
                    {
                        url = JSONHelper.getQueryString(tumblrDomain, tumblrPostTypes.empty.ToString(), i);
                    }

                    crawlManager.getDocument(url);
                    this.blog.posts.AddRange(crawlManager.getPostList(tumblrPostTypes.empty.ToString(), crawlManager.mode));

                    photoPosts += (from p in blog.posts where p.type == tumblrPostTypes.photo.ToString() select p).ToList().Count;
                    textPosts += (from p in blog.posts where p.type == tumblrPostTypes.regular.ToString() || p.type == tumblrPostTypes.text.ToString() select p).ToList().Count;
                    videoPosts += (from p in blog.posts where p.type == tumblrPostTypes.video.ToString() select p).ToList().Count;
                    linkPosts += (from p in blog.posts where p.type == tumblrPostTypes.link.ToString() select p).ToList().Count;
                    audioPosts += (from p in blog.posts where p.type == tumblrPostTypes.audio.ToString() select p).ToList().Count;
                    quotePosts += (from p in blog.posts where p.type == tumblrPostTypes.quote.ToString() select p).ToList().Count;
                    chatPosts += (from p in blog.posts where p.type == tumblrPostTypes.chat.ToString() || p.type == tumblrPostTypes.conversation.ToString() select p).ToList().Count;
                    answerPosts += (from p in blog.posts where p.type == tumblrPostTypes.answer.ToString() select p).ToList().Count;
                    this.parsed += this.blog.posts.Count;
                    this.blog.posts.Clear();
                    i += step;
                }

                this.found = blog.posts.Count;

            }
            else
            {
                this.statusCode = processingCodes.invalidURL;
            }
            return this.blog;
        }

        public void setAPIMode(string mode)
        {
            this.apiMode = mode; // XML or JSON
            this.crawlManager.mode = mode;
        }

        private void setBlogInfo()
        {
            if (crawlManager.mode == apiModeEnum.XML.ToString())
            {
                crawlManager.setBlogInfo(XMLHelper.getQueryString(url, tumblrPostTypes.empty.ToString(), 0, 1), this.blog);
            }
            else if (crawlManager.mode == apiModeEnum.JSON.ToString())
            {
                crawlManager.setBlogInfo(JSONHelper.getQueryString(tumblrDomain, tumblrPostTypes.empty.ToString(), 0, 1), this.blog);
            }
        }
    }
}