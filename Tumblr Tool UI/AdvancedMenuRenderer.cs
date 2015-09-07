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

        public AdvancedMenuRenderer()
            : base()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Color backColor = BackColor;
            Color foreColor = ForeColor;

            Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
            Color c = e.Item.Selected ? this.HighlightBackColor : this.BackColor;
            using (SolidBrush brush = new SolidBrush(c))
                e.Graphics.FillRectangle(brush, rc);
            e.Item.ForeColor = e.Item.Selected ? this.HighlightForeColor : this.ForeColor;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="TSMI"></param>
        /// <param name="e"></param>
        public void ChangeTextForeColor(ToolStripMenuItem TSMI, PaintEventArgs e)
        {
            Color foreColor = TSMI.GetCurrentParent().ForeColor;
            Color backColor = TSMI.BackColor;

            AdvancedMenuRenderer renderer = TSMI.GetCurrentParent().Renderer as AdvancedMenuRenderer;
            if (TSMI.Selected)
            {
                //TSMI.BackColor = Color.LightGray;
                TSMI.ForeColor = renderer.HighlightForeColor;
            }
            else
            {
                //TSMI.BackColor = SystemColors.Menu;
                TSMI.ForeColor = renderer.ForeColor;
            }
        }
    }
}