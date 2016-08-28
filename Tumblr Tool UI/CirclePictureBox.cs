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

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    [DesignerCategory(@"Code")]
    public class CirclePictureBox : PictureBox
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Brush brushImege;
            try
            {
                Bitmap imagem = new Bitmap(Image);
                //get images of the same size as control
                imagem = new Bitmap(imagem, new Size(Width - 1, Height - 1));
                brushImege = new TextureBrush(imagem);
            }
            catch
            {
                Bitmap imagem = new Bitmap(Width - 1, Height - 1, PixelFormat.Format24bppRgb);
                using (Graphics grp = Graphics.FromImage(imagem))
                {
                    grp.FillRectangle(
                        Brushes.White, 0, 0, Width - 1, Height - 1);
                    imagem = new Bitmap(Width - 1, Height - 1, grp);
                }
                brushImege = new TextureBrush(imagem);
            }
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, Width - 1, Height - 1);
            e.Graphics.FillPath(brushImege, path);
            e.Graphics.DrawPath(Pens.Black, path);
        }
    }
}