using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using Tumbl_Tool.Common_Helpers;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Tumbl_Tool.Tumblr_Objects;

namespace UniTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            XDocument doc = JSONHelper.jsonToXML(JSONHelper.getJSONString("http://api.tumblr.com/v2/blog/jai-envie-de-toi.nu/posts?api_key=SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY"));
            dynamic d = JSONHelper.getJSONObject("http://api.tumblr.com/v2/blog/jai-envie-de-toi.nu/posts?api_key=SyqUQV9GroNgxpH7W6ysgIpyQV2yYp38n42XtXSWQp43DSUPVY");
            JArray t = d.response.posts;
            List<dynamic> list = t.ToObject<List<dynamic>>();
            List<TumblrPost> tPosts = new List<TumblrPost>();
            string type = "photo";
            foreach (dynamic jPost in list)
            {
                TumblrPost post = new TumblrPost();
                if (type == "photo")
                {
                    post = new PhotoPost();
                }

                if (jPost.photos.Count == 1)
                {
                    post.imageURL = jPost.photos[0].original_size.url;
                    post.fileName = Path.GetFileName(post.imageURL);
                }

                tPosts.Add(post);
            }


            string s = doc.ToString();

            File.WriteAllText(@"d:\test.txt", s);
        }
    }
}
