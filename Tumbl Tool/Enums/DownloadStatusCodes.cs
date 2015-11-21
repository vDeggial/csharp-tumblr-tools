/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: November, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

namespace Tumblr_Tool.Enums
{
    /// <summary>
    /// Download Status Codes
    /// </summary>
    public enum DownloadStatusCodes
    {
        /// <summary>
        /// Ok to start the download process
        /// </summary>
        Ok,

        /// <summary>
        /// File already exists
        /// </summary>
        FileExists,

        /// <summary>
        /// Unable to download file
        /// </summary>
        UnableDownload,

        /// <summary>
        /// Preparing to download
        /// </summary>
        Preparing,

        /// <summary>
        /// Starting the download process
        /// </summary>
        Starting,

        /// <summary>
        /// Download is in progress
        /// </summary>
        Downloading,

        /// <summary>
        /// Finished downloading
        /// </summary>
        Done
    }
}