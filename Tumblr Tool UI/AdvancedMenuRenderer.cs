using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    public class AdvancedMenuRenderer: ToolStripProfessionalRenderer {

        public Color HighlightBackColor { get; set; }

        public Color HighlightForeColor { get; set; }


        public AdvancedMenuRenderer() : base() { }

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
