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

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Common_Helpers
{
    public static class XMLHelper
    {
        public const string _QUERY = @"/api/read";

        public static XDocument GetDocument(string url)
        {
            try
            {
                string xmlStr = WebHelper.GetDocumentAsString(url);
                return XDocument.Parse(xmlStr);
            }
            catch
            {
                return null;
            }
        }

        public static string GetPostElementAttributeValue(XDocument doc, string elementName, string attributeName)
        {
            return doc != null ? doc.Root.Element(elementName) != null ?
                doc.Root.Element(elementName).Attribute(attributeName) != null ?
                doc.Root.Element(elementName).Attribute(attributeName).Value : null : null : null;
        }

        public static HashSet<XElement> getPostElementList(XDocument doc)
        {
            return doc.Descendants("post").ToHashSet();
        }

        public static string GetPostElementValue(XDocument doc, string elementName)
        {
            return doc != null ? doc.Root.Element(elementName) != null ?
                doc.Root.Element(elementName).Value : null : null;
        }

        public static string GetQueryString(string tumblrURL, string type, int start = 0, int maxNumPosts = 0)
        {
            string query = string.Copy(_QUERY);

            if (type != TumblrPostTypes.empty.ToString())
            {
                query += "?type=" + type;

                if (start != 0)
                {
                    query += "&start=" + start.ToString();
                }

                if (maxNumPosts != 0)
                {
                    query += "&end=" + maxNumPosts.ToString();
                }

                query += "&num=" + ((int)PostStepEnum.XML).ToString();
            }
            else if (start != 0)
            {
                query += "?start=" + start.ToString();

                if (maxNumPosts != 0)
                {
                    query += "&end=" + maxNumPosts.ToString();
                }

                query += "&num=" + ((int)PostStepEnum.XML).ToString();
            }
            else if (maxNumPosts != 0)
            {
                query += "?end=" + maxNumPosts.ToString();

                query += "&num=" + ((int)PostStepEnum.XML).ToString();
            }
            else
            {
                query += "?num=" + ((int)PostStepEnum.XML).ToString();
            }

            return WebHelper.FixURL(tumblrURL) + query;
        }

        public static T ReadObject<T>(string filename) where T : new()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader =
            new System.Xml.Serialization.XmlSerializer(typeof(T));
                System.IO.StreamReader file = new System.IO.StreamReader(
                    @filename);
                T obj;
                obj = (T)reader.Deserialize(file.BaseStream);
                return obj;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static bool SaveObject<T>(string filename, Object obj)
        {
            try
            {
                using (StreamWriter myWriter = new StreamWriter(filename, false))
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(T));
                    mySerializer.Serialize(myWriter.BaseStream, obj);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}