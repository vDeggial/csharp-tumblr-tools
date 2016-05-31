/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: April, 2016
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System.Collections;
using System.Windows.Forms.Design;

namespace Tumblr_Tool
{
    internal class ColorProgressBarDesigner : ControlDesigner
    {
        // clean up some unnecessary properties
        protected override void PostFilterProperties(IDictionary properties)
        {
            properties.Remove("AllowDrop");
            properties.Remove("BackgroundImage");
            properties.Remove("ContextMenu");
            properties.Remove("FlatStyle");
            properties.Remove("Image");
            properties.Remove("ImageAlign");
            properties.Remove("ImageIndex");
            properties.Remove("ImageList");
            properties.Remove("Text");
            properties.Remove("TextAlign");
        }
    }
}