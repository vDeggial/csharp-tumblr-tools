using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tumbl_Tool.Tumblr_Objects;

namespace Tumbl_Tool.Common_Helpers
{
    public class LogFile
    {
        public string fileName { get; set; }
        public List<TumblrPost> posts { get; set; }
    }
}
