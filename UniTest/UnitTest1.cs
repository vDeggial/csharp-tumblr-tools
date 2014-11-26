using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Tumblr_Tool.Common_Helpers;
using Tumblr_Tool.Tumblr_Objects;

namespace UniTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ImageHelper.addImageDescription(@"F:\Tumblr\Blogs\jai-envie-detoi\tumblr_ku3achzH9H1qz7wuio1_500.jpg", "");
        }
    }
}