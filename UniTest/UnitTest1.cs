/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: December, 2014
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

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