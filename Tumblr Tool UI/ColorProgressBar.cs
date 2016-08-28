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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    [Description("Color Progress Bar")]
    [ToolboxBitmap(typeof(ProgressBar))]
    [Designer(typeof(ColorProgressBarDesigner))]
    [DesignerCategory(@"Code")]
    public class ColorProgressBar : Control
    {
        private Color _barColor = Color.FromArgb(255, 128, 128);

        private Color _borderColor = Color.Black;

        private FillStyles _fillStyle = FillStyles.Solid;

        private int _maximum = 100;

        private int _minimum;

        private int _step = 10;

        //
        // set default values
        //
        private int _value;

        public ColorProgressBar()
        {
            Size = new Size(150, 15);
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer,
                true
                );
        }

        public enum FillStyles
        {
            Solid,
            Dashed
        }

        [Description("ColorProgressBar color")]
        [Category("ColorProgressBar")]
        public Color BarColor
        {
            get
            {
                return _barColor;
            }
            set
            {
                _barColor = value;
                Invalidate();
            }
        }

        [Description("The border color of ColorProgressBar")]
        [Category("ColorProgressBar")]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        [Description("ColorProgressBar fill style")]
        [Category("ColorProgressBar")]
        public FillStyles FillStyle
        {
            get
            {
                return _fillStyle;
            }
            set
            {
                _fillStyle = value;
                Invalidate();
            }
        }

        [Description("The uppper bound of the range this ColorProgressbar is working with.")]
        [Category("ColorProgressBar")]
        [RefreshProperties(RefreshProperties.All)]
        public int Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value;

                if (_maximum < _value)
                    _value = _maximum;
                if (_maximum < _minimum)
                    _minimum = _maximum;

                Invalidate();
            }
        }

        [Description("The lower bound of the range this ColorProgressbar is working with.")]
        [Category("ColorProgressBar")]
        [RefreshProperties(RefreshProperties.All)]
        public int Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {
                _minimum = value;

                if (_minimum > _maximum)
                    _maximum = _minimum;
                if (_minimum > _value)
                    _value = _minimum;

                Invalidate();
            }
        }

        [Description("The amount to jump the current value of the control by when the Step() method is called.")]
        [Category("ColorProgressBar")]
        public int Step
        {
            get
            {
                return _step;
            }
            set
            {
                _step = value;
                Invalidate();
            }
        }

        [Description("The current value for the ColorProgressBar, " +
             "in the range specified by the Minimum and Maximum properties.")]
        [Category("ColorProgressBar")]
        // the rest of the Properties windows must be updated when this peroperty is changed.
        [RefreshProperties(RefreshProperties.All)]
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value < _minimum)
                {
                    throw new ArgumentException("'" + value + "' is not a valid value for 'Value'.\n" +
                        "'Value' must be between 'Minimum' and 'Maximum'.");
                }

                if (value > _maximum)
                {
                    throw new ArgumentException("'" + value + "' is not a valid value for 'Value'.\n" +
                        "'Value' must be between 'Minimum' and 'Maximum'.");
                }

                _value = value;
                Invalidate();
            }
        }

        //
        // Call the Decrement() method to decrease the value displayed by an integer you specify
        //
        public void Decrement(int value)
        {
            if (_value > _minimum)
                _value -= value;
            else
                _value = _minimum;

            Invalidate();
        }

        //
        // Call the Increment() method to increase the value displayed by an integer you specify
        //
        public void Increment(int value)
        {
            if (_value < _maximum)
                _value += value;
            else
                _value = _maximum;

            Invalidate();
        }

        //
        // Call the PerformStep() method to increase the value displayed by the amount set in the Step property
        //
        public void PerformStep()
        {
            if (_value < _maximum)
                _value += _step;
            else
                _value = _maximum;

            Invalidate();
        }

        //
        // Call the PerformStepBack() method to decrease the value displayed by the amount set in the Step property
        //
        public void PerformStepBack()
        {
            if (_value > _minimum)
                _value -= _step;
            else
                _value = _minimum;

            Invalidate();
        }

        //
        // Draw border
        //
        protected void DrawBorder(Graphics g)
        {
            Rectangle borderRect = new Rectangle(0, 0,
                ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            g.DrawRectangle(new Pen(_borderColor, 1), borderRect);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //
            // Calculate matching colors
            //
            Color darkColor = ForeColor;
            Color bgColor = BackColor;

            //
            // Fill background
            //
            SolidBrush bgBrush = new SolidBrush(bgColor);
            e.Graphics.FillRectangle(bgBrush, ClientRectangle);
            bgBrush.Dispose();

            //
            // Check for value
            //
            if (_maximum == _minimum || _value == 0)
            {
                // Draw border only and exit;
                DrawBorder(e.Graphics);
                return;
            }

            //
            // The following is the width of the bar. This will vary with each value.
            //
            int fillWidth = (Width * _value) / (_maximum - _minimum);

            //
            // GDI+ doesn't like rectangles 0px wide or high
            //
            if (fillWidth == 0)
            {
                // Draw border only and exti;
                DrawBorder(e.Graphics);
                return;
            }

            //
            // Rectangles for upper and lower half of bar
            //
            Rectangle leftRect = new Rectangle(0, 0, fillWidth, Height);

            //
            // The gradient brush
            //

            //
            // Paint upper half
            //
            //brush = new LinearGradientBrush(new Point(0, 0),
            //    new Point(0, this.Height / 2), darkColor, _BarColor);
            //e.Graphics.FillRectangle(brush, topRect);
            //brush.Dispose();

            var brush = new LinearGradientBrush(new Point(0, 0),
                new Point(Width, Height), darkColor, _barColor);
            e.Graphics.FillRectangle(brush, leftRect);
            brush.Dispose();

            //
            // Paint lower half
            // (this.Height/2 - 1 because there would be a dark line in the middle of the bar)
            //
            //brush = new LinearGradientBrush(new Point(0, this.Height / 2 - 1),
            //    new Point(0, this.Height), _BarColor, darkColor);
            //e.Graphics.FillRectangle(brush, buttomRect);
            //brush.Dispose();

            //brush = new LinearGradientBrush(new Point(fillWidth/2, 0),
            //    new Point(fillWidth, this.Height), _BarColor, darkColor);
            //e.Graphics.FillRectangle(brush, rightRect);
            //brush.Dispose();

            //
            // Calculate separator's setting
            //
            int sepWidth = (int)(Height * .67);
            int sepCount = fillWidth / sepWidth;
            Color sepColor = ControlPaint.DarkDark(_barColor);

            //
            // Paint separators
            //
            switch (_fillStyle)
            {
                case FillStyles.Dashed:
                    // Draw each separator line
                    for (int i = 1; i <= sepCount; i++)
                    {
                        e.Graphics.DrawLine(new Pen(sepColor, 1),
                            sepWidth * i, 0, sepWidth * i, Height);
                    }
                    break;

                case FillStyles.Solid:
                    // Draw nothing
                    break;
            }

            //
            // Draw border and exit
            //
            DrawBorder(e.Graphics);
        }
    }
}