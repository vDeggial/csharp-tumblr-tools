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
    }
}