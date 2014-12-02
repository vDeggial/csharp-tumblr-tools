using System.Text.RegularExpressions;

namespace Tumblr_Tool.Common_Helpers
{
    public static class CommonHelper
    {
        private static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string fixURL(string url)
        {
            if (url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1);
            }

            return url;
        }

        public static string getMimeType(string extension)
        {
            string mimeType = "";
            extension = extension.ToLower();
            switch (extension)
            {
                case ".png":
                    mimeType = "image/png";
                    break;

                case ".jpg":
                case ".jpeg":
                    mimeType = "image/jpeg";
                    break;

                case ".bmp":
                    mimeType = "image/bmp";
                    break;

                case ".gif":
                    mimeType = "image/gif";
                    break;

                case ".doc":
                    mimeType = "document/doc";
                    break;

                case ".docx":
                    mimeType = "document/docx";
                    break;

                case ".xls":
                    mimeType = "document/xls";
                    break;

                case ".xlsx":
                    mimeType = "document/xlsx";
                    break;

                case ".pdf":
                    mimeType = "document/pdf";
                    break;

                case ".rtf":
                    mimeType = "docuemnt/rtf";
                    break;

                case ".zip":
                    mimeType = "archive/zip";
                    break;

                case ".rar":
                    mimeType = "archive/rar";
                    break;

                default:
                    mimeType = "binary/other";
                    break;
            }
            return mimeType;
        }

        public static string NewLineToBreak(string input, string strToReplace)
        {
            if (input != null)
                return input.Replace(strToReplace, "\n\r");
            else
                return input;
        }

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTags(string source)
        {
            if (source != null)
                return _htmlRegex.Replace(source, string.Empty);
            else
                return source;
        }
    }
}