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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Common_Helpers
{
    public static class XMLHelper
    {
        public const string _QUERY = @"/api/read";

        public static XElement getPostElement(XDocument doc, string elementName)
        {
            return doc != null ? doc.Root.Element(elementName) != null ? doc.Root.Element(elementName) : null : null;
        }

        public static string getPostElementAttributeValue(XDocument doc, string elementName, string attributeName)
        {
            return doc != null ? doc.Root.Element(elementName) != null ?
                doc.Root.Element(elementName).Attribute(attributeName) != null ?
                doc.Root.Element(elementName).Attribute(attributeName).Value : null : null : null;
        }

        public static HashSet<XElement> getPostElementList(XDocument doc)
        {
            return doc.Descendants("post").ToHashSet();
        }

        public static string getPostElementValue(XDocument doc, string elementName)
        {
            return doc != null ? doc.Root.Element(elementName) != null ?
                doc.Root.Element(elementName).Value : null : null;
        }

        public static string getQueryString(string tumblrURL, string type, int start = 0, int maxNumPosts = 0)
        {
            string query = string.Copy(_QUERY);

            if (type != tumblrPostTypes.empty.ToString())
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

                query += "&num=" + ((int)postStepEnum.XML).ToString();
            }
            else if (start != 0)
            {
                query += "?start=" + start.ToString();

                if (maxNumPosts != 0)
                {
                    query += "&end=" + maxNumPosts.ToString();
                }

                query += "&num=" + ((int)postStepEnum.XML).ToString();
            }
            else if (maxNumPosts != 0)
            {
                query += "?end=" + maxNumPosts.ToString();

                query += "&num=" + ((int)postStepEnum.XML).ToString();
            }
            else
            {
                query += "?num=" + ((int)postStepEnum.XML).ToString();
            }

            return CommonHelper.fixURL(tumblrURL) + query;
        }

        public static XDocument getXMLDocument(string url)
        {
            try
            {
                string xmlStr;
                using (var wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    xmlStr = wc.DownloadString(url);
                }
                return XDocument.Parse(xmlStr);
            }
            catch
            {
                return null;
            }
        }

        public static T readObjectFromXML<T>(string filename) where T : new()
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

        public static bool saveObjectAsXML<T>(string filename, Object obj)
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