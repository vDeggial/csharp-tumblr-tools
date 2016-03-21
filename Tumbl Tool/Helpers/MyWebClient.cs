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

using System;
using System.ComponentModel;
using System.Net;

namespace Tumblr_Tool.Helpers
{
    [DesignerCategory("Code")]
    public class MyWebClient : WebClient
    {
        /// <summary>
        /// Override for custom WebClient GetRequest
        /// </summary>
        /// <param name="uri">URL to get</param>
        /// <returns>Gets web request</returns>
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            if (w != null)
            {
                w.Timeout = 1 * 60 * 1000;
                return w;
            }
            return null;
        }
    }
}