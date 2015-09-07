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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class AdvancedComboBox : ComboBox
    {
        /// <summary>
        ///
        /// </summary>
        public AdvancedComboBox()
        {

            base.DrawMode = DrawMode.OwnerDrawVariable;
            this.HighlightBackColor = Color.Black;
            this.HighlightForeColor = Color.White;
            this.ArrowBackColor = Color.White;
            this.ArrowForeColor = Color.Black;
            this.FlatStyle = FlatStyle.Flat;
            this.ShowArrow = true;
            this.Style = ComboBoxStyle.DropDownList;
            this.DropDownStyle = ComboBoxStyle.DropDownList;

            this.DrawItem += new DrawItemEventHandler(AdvancedComboBox_DrawItem);
            this.DropDown += new System.EventHandler(AdjustWidthComboBox_DropDown);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Height = 21;
        }

        new public DrawMode DrawMode { get; set; }

        public Color HighlightBackColor { get; set; }

        public Color HighlightForeColor { get; set; }

        public Color ArrowForeColor { get; set; }

        public Color ArrowBackColor { get; set; }

        public bool ShowArrow { get; set; }

        public ComboBoxStyle Style { get { return this.DropDownStyle; } set { this.DropDownStyle = value; } }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.SelectedIndex < 0 && this.Items.Count != 0)
                this.SelectedIndex = 0;

            this.DoubleBuffered = true;

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;

            int buttonWidth = SystemInformation.VerticalScrollBarWidth;
            Color highColor = this.ArrowBackColor;
            Color lowColor = this.ArrowBackColor;
            Rectangle itemRect = new Rectangle(this.Width - buttonWidth, 0, buttonWidth, this.Height);

            if (this.Items.Count > 0)
            {
                e.Graphics.DrawString(this.Items[this.SelectedIndex].ToString(), this.Font,
                                          new SolidBrush(this.ForeColor), e.ClipRectangle, sf);
            }


            //Create the brushes.
            LinearGradientBrush gradientBrush = new LinearGradientBrush(itemRect, highColor,
                    lowColor, LinearGradientMode.Vertical);

            //Fill the rectangle background.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillRectangle(gradientBrush, itemRect);
            gradientBrush.Dispose();

            ////Draw the button outline.
            //Pen outlinePen = new Pen(SystemColors.ButtonShadow, 0.0f);
            //e.Graphics.DrawRectangle(outlinePen, itemRect.X, itemRect.Y, itemRect.Width - 2, itemRect.Height - 2);
            //outlinePen.Dispose();

            if (this.ShowArrow)
            {
                Rectangle rectGlimph = itemRect;
                itemRect.Width -= 4;
                e.Graphics.TranslateTransform(rectGlimph.Left +
                    rectGlimph.Width / 2.0f,
                    rectGlimph.Top + rectGlimph.Height / 2.0f);
                GraphicsPath path = new GraphicsPath();
                PointF[] points = new PointF[3];
                points[0] = new PointF(-6 / 2.0f, -3 / 2.0f);
                points[1] = new PointF(6 / 2.0f, -3 / 2.0f);
                points[2] = new PointF(0, 6 / 2.0f);
                path.AddLine(points[0], points[1]);
                path.AddLine(points[1], points[2]);
                path.CloseFigure();
                e.Graphics.RotateTransform(0);

                SolidBrush br = new SolidBrush(Enabled ? Color.Gray : Color.Gainsboro);
                e.Graphics.FillPath(br, path);
                e.Graphics.ResetTransform();
                br.Dispose();
                path.Dispose();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdvancedComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;

            ComboBox combo = sender as ComboBox;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(this.HighlightBackColor),
                                         e.Bounds);
                e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font,
                                  new SolidBrush(this.HighlightForeColor),
                                  e.Bounds, sf);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(combo.BackColor),
                                         e.Bounds);

                e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font,
                                      new SolidBrush(combo.ForeColor),
                                      e.Bounds, sf);
            }

            //e.DrawFocusRectangle();
        }

        private void AdjustWidthComboBox_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

    }
}