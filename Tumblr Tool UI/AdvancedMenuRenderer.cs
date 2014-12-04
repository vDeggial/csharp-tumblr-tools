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

using System.Drawing;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    public class AdvancedMenuRenderer : ToolStripProfessionalRenderer
    {
        public Color HighlightBackColor { get; set; }

        public Color HighlightForeColor { get; set; }

        public AdvancedMenuRenderer()
            : base()
        {
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Color backColor = e.Item.BackColor;
            Color foreColor = e.Item.ForeColor;

            Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
            Color c = e.Item.Selected ? HighlightBackColor : backColor;
            using (SolidBrush brush = new SolidBrush(c))
                e.Graphics.FillRectangle(brush, rc);
        }

        public void changeTextForeColor(ToolStripMenuItem TSMI, PaintEventArgs e)
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
                TSMI.ForeColor = foreColor;
            }
        }
    }
}