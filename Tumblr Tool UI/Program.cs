using System;
using System.Linq;
using System.Windows.Forms;

namespace Tumblr_Tool
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Count() == 1)
                Application.Run(new mainForm(args[0]));
            else
                Application.Run(new mainForm());
        }
    }
}