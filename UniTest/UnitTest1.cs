using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tumblr_Tool.Common_Helpers;

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