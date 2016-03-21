/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2016
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

namespace Tumblr_Tool.Enums
{
    /// <summary>
    /// Error types
    /// </summary>
    public enum ErrorTypes
    {
        /// <summary>
        /// Connection error
        /// </summary>
        ConnectionError,

        /// <summary>
        /// Parsing error
        /// </summary>
        ParsingError,

        /// <summary>
        /// Internal system error
        /// </summary>
        InternalError,

        /// <summary>
        /// Unknown error
        /// </summary>
        UnknownError,

        /// <summary>
        /// Xml error
        /// </summary>
        XmlError,

        /// <summary>
        /// /Json error
        /// </summary>
        JsonError,

        /// <summary>
        /// Download error
        /// </summary>
        DownloadError,

        /// <summary>
        /// Crawl error
        /// </summary>
        CrawlError,

        /// <summary>
        /// Url error
        /// </summary>
        UrlError
    }
}