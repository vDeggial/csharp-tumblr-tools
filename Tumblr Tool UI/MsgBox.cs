/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    [DesignerCategory(@"Code")]
    internal sealed class MsgBox : Form
    {
        private const int CsDropshadow = 0x00020000;
        private static MsgBox _msgBox;
        private readonly Panel _plHeader = new Panel();
        private readonly Panel _plFooter = new Panel();
        private readonly Panel _plIcon = new Panel();
        private readonly PictureBox _picIcon = new PictureBox();
        private readonly FlowLayoutPanel _flpButtons = new FlowLayoutPanel();
        private readonly Label _lblTitle;
        private readonly Label _lblMessage;
        private readonly List<Button> _buttonCollection = new List<Button>();
        private static DialogResult _buttonResult;
        private static Timer _timer;
        private static Point _lastMousePos;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool MessageBeep(uint type);

        private MsgBox()
        {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.White;
            StartPosition = FormStartPosition.CenterScreen;
            Padding = new Padding(3);
            Width = 400;
            ShowInTaskbar = false;

            _lblTitle = new Label
            {
                ForeColor = Color.Black,
                Font = new Font("Century Gothic", 18),
                Dock = DockStyle.Top,
                Height = 50
            };

            _lblMessage = new Label
            {
                ForeColor = Color.Black,
                Font = new Font("Century Gothic", 10),
                Dock = DockStyle.Fill
            };

            _flpButtons.FlowDirection = FlowDirection.LeftToRight;
            _flpButtons.Dock = DockStyle.Fill;
            _flpButtons.Anchor = AnchorStyles.None;

            _plHeader.Dock = DockStyle.Fill;
            _plHeader.Padding = new Padding(20);
            _plHeader.Controls.Add(_lblMessage);
            _plHeader.Controls.Add(_lblTitle);

            _plFooter.Dock = DockStyle.Bottom;
            _plFooter.Padding = new Padding(20);
            _plFooter.BackColor = Color.White;
            _plFooter.Height = 80;
            _plFooter.Controls.Add(_flpButtons);

            _picIcon.Width = 32;
            _picIcon.Height = 32;
            _picIcon.Location = new Point(30, 50);

            _plIcon.Dock = DockStyle.Left;
            _plIcon.Padding = new Padding(20);
            _plIcon.Width = 70;
            _plIcon.Controls.Add(_picIcon);

            List<Control> controlCollection = new List<Control>
            {
                this,
                _lblTitle,
                _lblMessage,
                _flpButtons,
                _plHeader,
                _plFooter,
                _plIcon,
                _picIcon
            };

            foreach (Control control in controlCollection)
            {
                control.MouseDown += MsgBox_MouseDown;
                control.MouseMove += MsgBox_MouseMove;
            }

            Controls.Add(_plHeader);
            Controls.Add(_plIcon);
            Controls.Add(_plFooter);
        }

        private static void MsgBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _lastMousePos = new Point(e.X, e.Y);
            }
        }

        private static void MsgBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _msgBox.Left += e.X - _lastMousePos.X;
                _msgBox.Top += e.Y - _lastMousePos.Y;
            }
        }

        public static DialogResult Show(string message)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;

            InitButtons(Buttons.Ok);

            _msgBox.ShowDialog();
            return _buttonResult;
        }

        public static DialogResult Show(string message, bool beep)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;

            InitButtons(Buttons.Ok);

            if (beep)
                MessageBeep(0);

            _msgBox.ShowDialog();
            return _buttonResult;
        }

        public static DialogResult Show(string message, string title, bool beep)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox.Size = MessageSize(message);

            InitButtons(Buttons.Ok);

            if (beep)
                MessageBeep(0);
            _msgBox.ShowDialog();
            return _buttonResult;
        }

        public static DialogResult Show(string message, string title, Buttons buttons, bool beep)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox._plIcon.Hide();

            InitButtons(buttons);

            _msgBox.Size = MessageSize(message);
            if (beep)
                MessageBeep(0);
            _msgBox.ShowDialog();
            return _buttonResult;
        }

        public static DialogResult Show(string message, string title, Buttons buttons, Icon icon, bool beep)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;

            InitButtons(buttons);
            InitIcon(icon);

            _msgBox.Size = MessageSize(message);
            if (beep)
                MessageBeep(0);
            _msgBox.ShowDialog();

            return _buttonResult;
        }

        public static DialogResult Show(string message, string title, Buttons buttons, Icon icon, AnimateStyle style, bool beep)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox.Height = 0;

            InitButtons(buttons);
            InitIcon(icon);

            _timer = new Timer();
            Size formSize = MessageSize(message);

            switch (style)
            {
                case AnimateStyle.SlideDown:
                    _msgBox.Size = new Size(formSize.Width, 0);
                    _timer.Interval = 1;
                    _timer.Tag = new AnimateMsgBox(formSize, style);
                    break;

                case AnimateStyle.FadeIn:
                    _msgBox.Size = formSize;
                    _msgBox.Opacity = 0;
                    _timer.Interval = 20;
                    _timer.Tag = new AnimateMsgBox(formSize, style);
                    break;

                case AnimateStyle.ZoomIn:
                    _msgBox.Size = new Size(formSize.Width + 100, formSize.Height + 100);
                    _timer.Tag = new AnimateMsgBox(formSize, style);
                    _timer.Interval = 1;
                    break;
            }

            _timer.Tick += Timer_Tick;
            _timer.Start();

            if (beep)
                MessageBeep(0);
            _msgBox.ShowDialog();

            return _buttonResult;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            AnimateMsgBox animate = (AnimateMsgBox)timer.Tag;

            switch (animate.Style)
            {
                case AnimateStyle.SlideDown:
                    if (_msgBox.Height < animate.FormSize.Height)
                    {
                        _msgBox.Height += 17;
                        _msgBox.Invalidate();
                    }
                    else
                    {
                        _timer.Stop();
                        _timer.Dispose();
                    }
                    break;

                case AnimateStyle.FadeIn:
                    if (_msgBox.Opacity < 1)
                    {
                        _msgBox.Opacity += 0.1;
                        _msgBox.Invalidate();
                    }
                    else
                    {
                        _timer.Stop();
                        _timer.Dispose();
                    }
                    break;

                case AnimateStyle.ZoomIn:
                    if (_msgBox.Width > animate.FormSize.Width)
                    {
                        _msgBox.Width -= 17;
                        _msgBox.Invalidate();
                    }
                    if (_msgBox.Height > animate.FormSize.Height)
                    {
                        _msgBox.Height -= 17;
                        _msgBox.Invalidate();
                    }
                    break;
            }
        }

        private static void InitButtons(Buttons buttons)
        {
            switch (buttons)
            {
                case Buttons.AbortRetryIgnore:
                    _msgBox.InitAbortRetryIgnoreButtons();
                    break;

                case Buttons.Ok:
                    _msgBox.InitOkButton();
                    break;

                case Buttons.OkCancel:
                    _msgBox.InitOkCancelButtons();
                    break;

                case Buttons.RetryCancel:
                    _msgBox.InitRetryCancelButtons();
                    break;

                case Buttons.YesNo:
                    _msgBox.InitYesNoButtons();
                    break;

                case Buttons.YesNoCancel:
                    _msgBox.InitYesNoCancelButtons();
                    break;
            }

            foreach (Button btn in _msgBox._buttonCollection)
            {
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Century Gothic", 8);
                btn.Padding = new Padding(3);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Height = 30;
                btn.FlatAppearance.BorderColor = Color.Black;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;
                btn.TabStop = false;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Anchor = AnchorStyles.None;

                btn.MouseEnter += Button_MouseEnter;
                btn.MouseLeave += Button_MouseLeave;

                _msgBox._flpButtons.Controls.Add(btn);
            }
        }

        public static void Button_MouseEnter(object sender, EventArgs e)
        {

            if (sender is Button button)
            {
                button.UseVisualStyleBackColor = false;
                button.ForeColor = Color.Maroon;
                button.FlatAppearance.BorderColor = Color.Maroon;
                button.FlatAppearance.MouseOverBackColor = Color.White;
                button.FlatAppearance.BorderSize = 1;
            }
        }

        public static void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.UseVisualStyleBackColor = true;
                button.ForeColor = Color.Black;
                button.FlatAppearance.BorderSize = 0;
            }
        }

        private static void InitIcon(Icon icon)
        {
            switch (icon)
            {
                case Icon.Application:
                    _msgBox._picIcon.Image = SystemIcons.Application.ToBitmap();
                    break;

                case Icon.Exclamation:
                    _msgBox._picIcon.Image = SystemIcons.Exclamation.ToBitmap();
                    break;

                case Icon.Error:
                    _msgBox._picIcon.Image = SystemIcons.Error.ToBitmap();
                    break;

                case Icon.Info:
                    _msgBox._picIcon.Image = SystemIcons.Information.ToBitmap();
                    break;

                case Icon.Question:
                    _msgBox._picIcon.Image = SystemIcons.Question.ToBitmap();
                    break;

                case Icon.Shield:
                    _msgBox._picIcon.Image = SystemIcons.Shield.ToBitmap();
                    break;

                case Icon.Warning:
                    _msgBox._picIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
            }
        }

        private void InitAbortRetryIgnoreButtons()
        {
            Button btnAbort = new Button { Text = @"Abort" };
            btnAbort.Click += ButtonClick;

            Button btnRetry = new Button { Text = @"Retry" };
            btnRetry.Click += ButtonClick;

            Button btnIgnore = new Button { Text = @"Ignore" };
            btnIgnore.Click += ButtonClick;

            _buttonCollection.Add(btnAbort);
            _buttonCollection.Add(btnRetry);
            _buttonCollection.Add(btnIgnore);
        }

        private void InitOkButton()
        {
            Button btnOk = new Button { Text = @"OK" };
            btnOk.Click += ButtonClick;

            _buttonCollection.Add(btnOk);
        }

        private void InitOkCancelButtons()
        {
            Button btnOk = new Button { Text = @"OK" };
            btnOk.Click += ButtonClick;

            Button btnCancel = new Button { Text = @"Cancel" };
            btnCancel.Click += ButtonClick;

            _buttonCollection.Add(btnOk);
            _buttonCollection.Add(btnCancel);
        }

        private void InitRetryCancelButtons()
        {
            Button btnRetry = new Button { Text = @"OK" };
            btnRetry.Click += ButtonClick;

            Button btnCancel = new Button { Text = @"Cancel" };
            btnCancel.Click += ButtonClick;

            _buttonCollection.Add(btnRetry);
            _buttonCollection.Add(btnCancel);
        }

        private void InitYesNoButtons()
        {
            Button btnYes = new Button { Text = @"Yes" };
            btnYes.Click += ButtonClick;

            Button btnNo = new Button { Text = @"No" };
            btnNo.Click += ButtonClick;

            _buttonCollection.Add(btnYes);
            _buttonCollection.Add(btnNo);
        }

        private void InitYesNoCancelButtons()
        {
            Button btnYes = new Button { Text = @"Abort" };
            btnYes.Click += ButtonClick;

            Button btnNo = new Button { Text = @"Retry" };
            btnNo.Click += ButtonClick;

            Button btnCancel = new Button { Text = @"Cancel" };
            btnCancel.Click += ButtonClick;

            _buttonCollection.Add(btnYes);
            _buttonCollection.Add(btnNo);
            _buttonCollection.Add(btnCancel);
        }

        private static void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Text)
            {
                case "Abort":
                    _buttonResult = DialogResult.Abort;
                    break;

                case "Retry":
                    _buttonResult = DialogResult.Retry;
                    break;

                case "Ignore":
                    _buttonResult = DialogResult.Ignore;
                    break;

                case "OK":
                    _buttonResult = DialogResult.OK;
                    break;

                case "Cancel":
                    _buttonResult = DialogResult.Cancel;
                    break;

                case "Yes":
                    _buttonResult = DialogResult.Yes;
                    break;

                case "No":
                    _buttonResult = DialogResult.No;
                    break;
            }

            _msgBox.Dispose();
        }

        private static Size MessageSize(string message)
        {
            Graphics g = _msgBox.CreateGraphics();
            int width = 350;
            int height = 230;

            SizeF size = g.MeasureString(message, new Font("Century Gothic", 10));

            if (message.Length < 150)
            {
                if ((int)size.Width > 350)
                {
                    width = (int)size.Width;
                }
            }
            else
            {
                string[] groups = (from Match m in Regex.Matches(message, ".{1,180}") select m.Value).ToArray();
                int lines = groups.Length + 1;
                width = 700;
                height += (int)(size.Height + 10) * lines;
            }
            return new Size(width, height);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CsDropshadow;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1));
            Pen pen = new Pen(Color.FromArgb(0, 0, 0));

            g.DrawRectangle(pen, rect);
        }

        public enum Buttons
        {
            AbortRetryIgnore = 1,
            Ok = 2,
            OkCancel = 3,
            RetryCancel = 4,
            YesNo = 5,
            YesNoCancel = 6
        }

        public new enum Icon
        {
            Application = 1,
            Exclamation = 2,
            Error = 3,
            Warning = 4,
            Info = 5,
            Question = 6,
            Shield = 7,
            Search = 8
        }

        public enum AnimateStyle
        {
            SlideDown = 1,
            FadeIn = 2,
            ZoomIn = 3
        }
    }

    internal class AnimateMsgBox
    {
        public Size FormSize;
        public MsgBox.AnimateStyle Style;

        public AnimateMsgBox(Size formSize, MsgBox.AnimateStyle style)
        {
            FormSize = formSize;
            Style = style;
        }
    }
}