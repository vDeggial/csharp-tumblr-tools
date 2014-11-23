using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Tumblr_Tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( string[] args )
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
