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

namespace Tumblr_Tool.Enums
{
    /// <summary>
    /// Tumblr API Response Codes
    /// </summary>
    public enum TumblrApiResponseEnum
    {
        /// <summary>
        /// Tumblr blog found
        /// </summary>
        Ok = 200,
        /// <summary>
        /// Access not authorized
        /// </summary>
        NotAuthorized = 401,
        /// <summary>
        /// Tumblr blog not found
        /// </summary>
        NotFound = 404
    }
}