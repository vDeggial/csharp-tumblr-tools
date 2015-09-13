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

namespace Tumblr_Tool
{
    public class ToolOptions
    {
        //public string ApiMode { get; set; }

        /// <summary>
        /// Option to generate posts log file
        /// </summary>
        public bool GenerateLog { get; set; }

        /// <summary>
        /// Option to parse .gif files
        /// </summary>
        public bool ParseGif { get; set; }

        /// <summary>
        /// Option to parse .jpg files
        /// </summary>
        public bool ParseJpeg { get; set; }

        /// <summary>
        /// Option to parse posts without image files download
        /// </summary>
        public bool ParseOnly { get; set; }

        /// <summary>
        /// Option to parse image photosets
        /// </summary>
        public bool ParsePhotoSets { get; set; }

        /// <summary>
        /// Option to parse .png files
        /// </summary>
        public bool ParsePng { get; set; }
    }
}