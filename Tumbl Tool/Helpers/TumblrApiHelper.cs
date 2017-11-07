/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: November, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System.Text;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Helpers
{
    public static class TumblrApiHelper
    {
        /// <summary>
        /// Tumblr API key
        /// </summary>
        private const string ApiKey = "SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY";

        /// <summary>
        /// Tumblr API url scheme
        /// </summary>
        private const string ApiUrl = "https://api.tumblr.com/v2/blog/{0}/{1}?api_key={2}{3}{4}";

        /// <summary>
        /// Tumblr blog avatar query string
        /// </summary>
        private const string AvatarQuery = "avatar";

        /// <summary>
        /// Tumblr blog avatar size
        /// </summary>
        private const string AvatarSize = "128";

        /// <summary>
        /// Tumblr blog info query string
        /// </summary>
        private const string InfoQuery = "info";

        /// <summary>
        /// Tumblr API response posts limit
        /// </summary>
        private const string Limit = "&limit={0}";

        /// <summary>
        /// Tumblr API posts offset (start)
        /// </summary>
        private const string Offset = "&offset={0}";

        /// <summary>
        /// Tumblr blog posts query string
        /// </summary>
        private const string PostQuery = "posts";

        /// <summary>
        /// Generate Tumblr blog avatar query string
        /// </summary>
        /// <param name="tumblrDomain">Tumblr Url</param>
        /// <param name="avatarSize">Avatar image size</param>
        /// <returns>Tumblr API url for blog avatar</returns>
        public static string GenerateAvatarQueryUrl(string tumblrDomain, string avatarSize = AvatarSize)
        {
            return string.Format(ApiUrl, WebHelper.GetDomainName(tumblrDomain), new StringBuilder(AvatarQuery).Append("/").Append(avatarSize).ToString(), ApiKey,
                string.Empty, string.Empty);
        }

        /// <summary>
        /// Generates blog info query string
        /// </summary>
        /// <param name="tumblrDomain">Tumblr blog domain</param>
        /// <returns>Full API blog info query string</returns>
        public static string GenerateInfoQueryUrl(string tumblrDomain)
        {
            return string.Format(ApiUrl, WebHelper.RemoveTrailingBackslash(tumblrDomain), InfoQuery, ApiKey, null, null);
        }

        /// <summary>
        /// Generate Tumblr API query string for posts
        /// </summary>
        /// <param name="tumblrDomain">Tumblr domain</param>
        /// <param name="postType">Tumblr post type</param>
        /// <param name="offset">Tumblr posts offset</param>
        /// <param name="limit">Tumblr post limit per document</param>
        /// <returns>Tumblr API query string for blog posts</returns>
        public static string GeneratePostTypeQueryUrl(string tumblrDomain, TumblrPostType postType, int offset = 0,
            int limit = (int)NumberOfPostsPerApiDocument.ApiV2)
        {
            return string.Format(ApiUrl, WebHelper.RemoveTrailingBackslash(tumblrDomain), (postType == TumblrPostType.All) ?
                PostQuery : new StringBuilder(PostQuery).Append("/").Append(postType.ToString().ToLower()).ToString(), ApiKey,
                string.Format(Offset, offset.ToString()), string.Format(Limit, limit.ToString()));
        }
    }
}