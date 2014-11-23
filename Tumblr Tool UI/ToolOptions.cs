using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tumblr_Tool
{
    public class ToolOptions
    {
        public bool parseOnly { get; set; }
        public bool parseJPEG { get; set; }
        public bool parsePNG { get; set; }
        public bool parseGIF { get; set; }
        public bool parsePhotoSets { get; set; }

        public string apiMode { get; set; }


    }
}
