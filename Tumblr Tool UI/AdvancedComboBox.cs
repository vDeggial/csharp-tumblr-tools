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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    [DesignerCategory(@"Code")]
    public class AdvancedComboBox : ComboBox
    {
        /// <summary>
        ///
        /// </summary>
        public AdvancedComboBox()
        {
            base.DrawMode = DrawMode.OwnerDrawVariable;
            HighlightBackColor = Color.Black;
            HighlightForeColor = Color.White;
            ArrowBackColor = Color.White;
            ArrowForeColor = Color.Black;
            FlatStyle = FlatStyle.Flat;
            ShowArrow = true;
            Style = ComboBoxStyle.DropDownList;
            DropDownStyle = ComboBoxStyle.DropDownList;

            DrawItem += AdvancedComboBox_DrawItem;
            DropDown += AdjustWidthComboBox_DropDown;
            SetStyle(ControlStyles.UserPaint, true);
            Height = 21;
        }

        new public DrawMode DrawMode { get; set; }

        public Color HighlightBackColor { get; set; }

        public Color HighlightForeColor { get; set; }

        public Color ArrowForeColor { get; set; }

        public Color ArrowBackColor { get; set; }

        public bool ShowArrow { get; set; }

        public ComboBoxStyle Style { get { return DropDownStyle; } set { DropDownStyle = value; } }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (SelectedIndex < 0 && Items.Count != 0)
                SelectedIndex = 0;

            DoubleBuffered = true;

            StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center };
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;

            int buttonWidth = SystemInformation.VerticalScrollBarWidth;
            Color highColor = ArrowBackColor;
            Color lowColor = ArrowBackColor;
            Rectangle itemRect = new Rectangle(Width - buttonWidth, 0, buttonWidth, Height);

            if (Items.Count > 0)
            {
                e.Graphics.DrawString(Items[SelectedIndex].ToString(), Font,
                                          new SolidBrush(ForeColor), e.ClipRectangle, sf);
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

            if (ShowArrow)
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

            StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center };
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;

            ComboBox combo = sender as ComboBox;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(HighlightBackColor),
                                         e.Bounds);
                if (combo != null)
                    e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font,
                        new SolidBrush(HighlightForeColor),
                        e.Bounds, sf);
            }
            else
            {
                if (combo != null)
                {
                    e.Graphics.FillRectangle(new SolidBrush(combo.BackColor),
                        e.Bounds);

                    e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font,
                        new SolidBrush(combo.ForeColor),
                        e.Bounds, sf);
                }
            }

            //e.DrawFocusRectangle();
        }

        private void AdjustWidthComboBox_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            width = (from string s in ((ComboBox)sender).Items select (int)g.MeasureString(s, font).Width + vertScrollBarWidth).Concat(new[] { width }).Max();
            senderComboBox.DropDownWidth = width;
        }
    }
}