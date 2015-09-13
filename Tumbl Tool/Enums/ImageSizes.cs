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
    /// Image sizes to download as per Tumblr API
    /// </summary>
    public enum ImageSizes
    {
        /// <summary>
        /// Unknown size
        /// </summary>
        None = 0,

        /// <summary>
        /// Original image size (Max Width: 1280px)
        /// </summary>
        Original = 1280,

        /// <summary>
        /// Large image size (Max Width: 500px)
        /// </summary>
        Large = 500,

        /// <summary>
        /// Medium image size (Max Width: 400px)
        /// </summary>
        Medium = 400,

        /// <summary>
        /// Small image size (Max Width: 250px)
        /// </summary>
        Small = 250,

        /// <summary>
        /// Xtra Small image size (Max Width: 100px)
        /// </summary>
        XSmall = 100,

        /// <summary>
        /// Square image size (75px x 75px)
        /// </summary>
        Square = 75
    }
}