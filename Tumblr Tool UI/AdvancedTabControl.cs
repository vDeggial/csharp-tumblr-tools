using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    class AdvancedTabControl : TabControl
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
