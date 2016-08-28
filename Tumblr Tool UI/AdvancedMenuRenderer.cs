/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: August, 2016
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System.Drawing;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    public class AdvancedMenuRenderer : ToolStripProfessionalRenderer
    {
        public Color HighlightBackColor { get; set; }

        public Color HighlightForeColor { get; set; }

        public Color ForeColor { get; set; }

        public Color BackColor { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
            Color c = e.Item.Selected ? HighlightBackColor : BackColor;
            using (SolidBrush brush = new SolidBrush(c))
                e.Graphics.FillRectangle(brush, rc);
            e.Item.ForeColor = e.Item.Selected ? HighlightForeColor : ForeColor;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tsmi"></param>
        /// <param name="e"></param>
        public void ChangeTextForeColor(ToolStripMenuItem tsmi, PaintEventArgs e)
        {
            AdvancedMenuRenderer renderer = tsmi.GetCurrentParent().Renderer as AdvancedMenuRenderer;
            if (tsmi.Selected)
            {
                //TSMI.BackColor = Color.LightGray;
                if (renderer != null) tsmi.ForeColor = renderer.HighlightForeColor;
            }
            else
            {
                //TSMI.BackColor = SystemColors.Menu;
                if (renderer != null) tsmi.ForeColor = renderer.ForeColor;
            }
        }
    }
}