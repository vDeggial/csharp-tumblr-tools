using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Tumblr_Tool.Common_Helpers
{
    public static class JSONHelper
    {
        private static string apiKey = "SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY";
        private static string jsonBlogInfoQuery = "info";
        private static string jsonPostQuery = "posts";
        private static string jsonURL = "http://api.tumblr.com/v2/blog";
        private static int offset = 0;

        public static JObject getJSONObject(string url)
        {
            string result = getJSONString(url);

            if (result != null)
            {
                return JObject.Parse(result);
            }

            else
            {
                return null;
            }
        }

        public static string getJSONString(string url)
        {
            try
            {
                string jsonStr;

                using (var wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    jsonStr = wc.DownloadString(url);
                }

                return jsonStr;
            }
            catch
            {
                return null;
            }
        }

        public static string getQueryString(string tumblrDomain, string type, int start = 0, int maxNumPosts = 0)
        {
            string query = string.Copy(jsonURL);

            query += "/" + tumblrDomain + jsonPostQuery;
            query += "?api_key=" + apiKey;

            if (!string.IsNullOrEmpty(type))
            {
                query += "&type=" + type;
            }

            if (start != 0)
            {
                query += "&offset=" + start.ToString();
            }

            if (maxNumPosts != 0)
            {
                query += "&limit=" + maxNumPosts.ToString();
            }

            return query;
        }

        public static XDocument jsonToXML(string json)
        {
            return JsonConvert.DeserializeXNode(json, "tumblr");
        }
    }
}