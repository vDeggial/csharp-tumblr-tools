using System.Net;
using System.Net.NetworkInformation;

namespace Tumbl_Tool.Common_Helpers
{
    public static class WebHelper
    {
        public static bool CheckForInternetConnection()
        {
            return new Ping().Send("www.google.com").Status == IPStatus.Success;
        }

        public static string stripHTMLTags(string HTML)
        {
            // Removes tags from passed HTML
            System.Text.RegularExpressions.Regex objRegEx = new System.Text.RegularExpressions.Regex("<[^>]*>");

            return objRegEx.Replace(HTML, "");
        }

        public static bool webURLExists(string url)
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead(url))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}