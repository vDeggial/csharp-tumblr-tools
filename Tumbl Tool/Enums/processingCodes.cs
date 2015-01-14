/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

namespace Tumblr_Tool.Enums
{
    public enum ProcessingCodes
    {
        OK,
        Initializing,
        checkingConnection,
        connectionOK,
        connectionError,
        gettingBlogInfo,
        blogInfoOK,
        blogInfoError,
        invalidURL,
        UnableDownload,
        Starting,
        Crawling,
        Parsing,
        errorProcessing,
        saveFileOK,
        saveFileError,
        Done,
        Error,
        SavingLogFile
    }
}