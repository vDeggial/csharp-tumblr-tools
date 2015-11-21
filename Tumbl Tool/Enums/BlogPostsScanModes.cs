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

using System.ComponentModel;

namespace Tumblr_Tool.Enums
{
    /// <summary>
    /// Parse Modes: Full Rescan / New Posts Only
    /// </summary>
    public enum BlogPostsScanModes
    {
        /// <summary>
        /// Scan new posts only
        /// </summary>
        [Description("New")]
        NewestPostsOnly = 0,

        /// <summary>
        /// Full blog rescan
        /// </summary>
        [Description("Full")]
        FullBlogRescan = 1
    }
}