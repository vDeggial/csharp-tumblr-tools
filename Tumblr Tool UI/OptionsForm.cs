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
                return Enum.GetName(typeof(apiModeEnum), this.select_APIMode.SelectedIndex);
            }

            set
            {
                this.select_APIMode.SelectedIndex = (int)Enum.Parse(typeof(apiModeEnum), value);
            }
        }

        public mainForm mainForm
        {
            get
            {
                return _mainForm;
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
                return _options;
            }
        }

        public bool parseDownload
        {
            get
            {
                return check_ParseDownload.Checked;
            }
        }

        public bool generateLog
        {
            get
            {
                return check_GenerateLog.Checked;
            }

            set
            {
                check_GenerateLog.Checked = value;
            }
        }

        public bool parseGIF
        {
            get
            {
                return check_GIF.Checked;
            }
        }

        public bool parseJPEG
        {
            get
            {
                return check_JPEG.Checked;
            }
        }

        public bool parseOnly
        {
            get
            {
                return check_ParseOnly.Checked;
            }
        }

        public bool parsePhotoSets
        {
            get
            {
                return check_PhotoSets.Checked;
            }
        }

        public bool parsePNG
        {
            get
            {
                return check_PNG.Checked;
            }
        }

        public void setOptions()
        {
            _options.parseJPEG = this.parseJPEG;
            _options.parsePNG = this.parsePNG;
            _options.parseGIF = this.parseGIF;
            _options.parsePhotoSets = this.parsePhotoSets;
            _options.parseOnly = this.parseOnly;
            _options.apiMode = this.apiMode;
            _options.generateLog = this.check_GenerateLog.Checked;
        }

        private void btn_Accept_Click(object sender, EventArgs e)
        {
            this._mainForm.loadOptions();
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            restoreOptions();
            this.Close();
        }

        private void check_ParseDownload_CheckedChanged(object sender, EventArgs e)
        {
            if (check_ParseDownload.Checked)
            {
                check_ParseOnly.Checked = false;
            }
        }

        private void check_ParseOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (check_ParseOnly.Checked)
            {
                check_ParseDownload.Checked = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void restoreOptions()
        {
            this.check_GIF.Checked = _options.parseGIF;
            this.check_JPEG.Checked = _options.parseJPEG;
            this.check_ParseDownload.Checked = !_options.parseOnly;
            this.check_ParseOnly.Checked = _options.parseOnly;
            this.check_PhotoSets.Checked = _options.parsePhotoSets;
            this.check_PNG.Checked = _options.parsePNG;
            this.apiMode = _options.apiMode;
            this.check_GenerateLog.Checked = _options.generateLog;
        }

        private void setOptions(object sender, EventArgs e)
        {
            setOptions();
        }
    }
}