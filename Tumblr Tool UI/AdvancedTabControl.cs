/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: December, 2014
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    internal class AdvancedTabControl : TabControl
    {
        public Color ColorBackground { get; set; }

        public Color ColorForeground { get; set; }

        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();

        public AdvancedTabControl()
        {
            this.ColorBackground = Color.Black;
            this.ColorForeground = Color.White;
        }
    }
}