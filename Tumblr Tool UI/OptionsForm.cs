/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Windows.Forms;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool
{
    public partial class OptionsForm : Form
    {
        private mainForm _mainForm = null;

        private ToolOptions _options = new ToolOptions();

        public OptionsForm()
        {
            InitializeComponent();
        }

        public string apiMode
        {
            get
            {
                return Enum.GetName(typeof(ApiModeEnum), this.select_APIMode.SelectedIndex);
            }

            set
            {
                this.select_APIMode.SelectedIndex = (int)Enum.Parse(typeof(ApiModeEnum), value);
            }
        }

        public bool generateLog
        {
            get
            {
                return this.check_GenerateLog.Checked;
            }

            set
            {
                this.check_GenerateLog.Checked = value;
            }
        }

        public mainForm mainForm
        {
            get
            {
                return this._mainForm;
            }

            set
            {
                this._mainForm = value;
            }
        }

        public ToolOptions options
        {
            get
            {
                return this._options;
            }

            set
            {
                this._options = value;
            }
        }

        public bool parseDownload
        {
            get
            {
                return this.check_ParseDownload.Checked;
            }
        }

        public bool parseGIF
        {
            get
            {
                return this.check_GIF.Checked;
            }
        }

        public bool parseJPEG
        {
            get
            {
                return this.check_JPEG.Checked;
            }
        }

        public bool parseOnly
        {
            get
            {
                return this.check_ParseOnly.Checked;
            }
        }

        public bool parsePhotoSets
        {
            get
            {
                return this.check_PhotoSets.Checked;
            }
        }

        public bool parsePNG
        {
            get
            {
                return this.check_PNG.Checked;
            }
        }

        public void SetOptions()
        {
            this._options.parseJPEG = this.parseJPEG;
            this._options.parsePNG = this.parsePNG;
            this._options.parseGIF = this.parseGIF;
            this._options.parsePhotoSets = this.parsePhotoSets;
            this._options.parseOnly = this.parseOnly;
            this._options.apiMode = this.apiMode;
            this._options.generateLog = this.check_GenerateLog.Checked;
        }

        private void btn_Accept_Click(object sender, EventArgs e)
        {
            this._mainForm.SetOptions();
            this.mainForm.SaveOptions(this.mainForm.optionsFileName);
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            RestoreOptions();
            this.Close();
        }

        private void check_ParseDownload_CheckedChanged(object sender, EventArgs e)
        {
            if (this.check_ParseDownload.Checked)
            {
                this.check_ParseOnly.Checked = false;
            }
        }

        private void check_ParseOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (this.check_ParseOnly.Checked)
            {
                this.check_ParseDownload.Checked = false;
            }
        }

        private void mode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void RestoreOptions()
        {
            this.check_GIF.Checked = this._options.parseGIF;
            this.check_JPEG.Checked = this._options.parseJPEG;
            this.check_ParseDownload.Checked = !this._options.parseOnly;
            this.check_ParseOnly.Checked = this._options.parseOnly;
            this.check_PhotoSets.Checked = this._options.parsePhotoSets;
            this.check_PNG.Checked = this._options.parsePNG;
            this.apiMode = this._options.apiMode;
            this.check_GenerateLog.Checked = this._options.generateLog;
        }

        private void SetOptions(object sender, EventArgs e)
        {
            SetOptions();
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            this._mainForm.Button_MouseEnter(sender, e);
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            this._mainForm.Button_MouseLeave(sender, e);
        }
    }
}