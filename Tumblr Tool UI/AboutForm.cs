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

using System.Windows.Forms;

namespace Tumblr_Tool
{
    public partial class AboutForm : Form
    {
        private mainForm _mainForm = null;

        public AboutForm()
        {
            InitializeComponent();
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

        public string version
        {
            set
            {
                this.lbl_Version.Text = value;
            }

            get
            {
                return this.lbl_Version.Text;
            }
        }
    }
}