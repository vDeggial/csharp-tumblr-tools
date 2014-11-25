using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Tumblr_Tool.Common_Helpers
{
    public static class JSONHelper
    {
        public static JObject getJSONObject(string url)
        {
            return JObject.Parse(getJSONString(url));
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

        public static XDocument jsonToXML(string json)
        {
            return JsonConvert.DeserializeXNode(json, "tumblr");
        }
    }
}