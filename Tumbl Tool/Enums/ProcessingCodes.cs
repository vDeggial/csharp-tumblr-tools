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
    /// Processing codes
    /// </summary>
    public enum ProcessingCodes
    {
        /// <summary>
        /// Ok state
        /// </summary>
        Ok,

        /// <summary>
        /// Initializing the process
        /// </summary>
        Initializing,

        /// <summary>
        /// Checking the connection to internet
        /// </summary>
        CheckingConnection,

        /// <summary>
        /// Internet connection found
        /// </summary>
        ConnectionOk,

        /// <summary>
        /// No Internet connection found
        /// </summary>
        ConnectionError,

        /// <summary>
        /// Getting initial blog info
        /// </summary>
        GettingBlogInfo,

        /// <summary>
        /// Got blog information ok
        /// </summary>
        BlogInfoOk,

        /// <summary>
        /// Error getting blog info
        /// </summary>
        BlogInfoError,

        /// <summary>
        /// Invalid Url
        /// </summary>
        InvalidUrl,

        /// <summary>
        /// Unable to download document
        /// </summary>
        UnableDownload,

        /// <summary>
        /// Starting the process
        /// </summary>
        Starting,

        /// <summary>
        /// Crawling the blog posts
        /// </summary>
        Crawling,

        /// <summary>
        /// Parsing the blog posts
        /// </summary>
        Parsing,

        /// <summary>
        /// Error processing the document
        /// </summary>
        ErrorProcessing,

        /// <summary>
        /// Saved tumblr tools savefile ok
        /// </summary>
        SaveFileOk,

        /// <summary>
        /// Error saving tumblr tools savefile
        /// </summary>
        SaveFileError,

        /// <summary>
        /// Operation complete
        /// </summary>
        Done,

        /// <summary>
        /// Error during operations
        /// </summary>
        Error,

        /// <summary>
        /// Saving log file
        /// </summary>
        SavingLogFile
    }
}