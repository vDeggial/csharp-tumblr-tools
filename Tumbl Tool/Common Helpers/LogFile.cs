using System.Collections.Generic;
using Tumbl_Tool.Tumblr_Objects;

namespace Tumbl_Tool.Common_Helpers
{
    public class LogFile
    {
        public string fileName { get; set; }

        public List<TumblrPost> posts { get; set; }
    }
}