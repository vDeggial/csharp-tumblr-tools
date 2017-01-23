/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Tumblr_Tool.Helpers;
using Tumblr_Tool.Objects;
using Tumblr_Tool.Objects.Tumblr_Objects;

namespace UniTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GenerateTagListTest()
        {
            string logFileName = @"D:\Tumblr\Blogs\jai-envie-detoi\jai-envie-detoi.log";
            SaveFile log = JsonHelper.ReadObjectFromFile<SaveFile>(logFileName);
            TumblrBlog blog = log.Blog;

            HashSet<string> tags = new HashSet<string>();
            SortedSet<string> invalidtags = new SortedSet<string>();

            foreach (TumblrPost post in blog.Posts)
            {
                tags.UnionWith(post.Tags);

                foreach (string tag in post.Tags)
                {
                    if (tag.StartsWith(tag.Substring(0, 1).ToUpper()))
                    {
                        invalidtags.Add("<a href = '" +post.ShortUrl + "' target='_blank'>" + post.ShortUrl + "   :   " + tag
                            + "</a><br>");
                    }
                }

            }

            SortedSet<string> tagsSorted = new SortedSet<string>(tags);
            

            //foreach(string tag in tagsSorted)
            //{
            //    if (tag.StartsWith(tag.Substring(0,1).ToUpper()))
            //    {
            //        invalidtags.Add(tag);
            //    }
            //}

            File.WriteAllText(@"D:\Tumblr\Blogs\jai-envie-detoi\tags.txt", string.Join(",", tagsSorted));
            File.WriteAllLines(@"D:\Tumblr\Blogs\jai-envie-detoi\invalidtags.html", invalidtags);


        }
    }
}