using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Common_Helpers
{
    public static class XMLHelper
    {
        public static string queryXML = @"/api/read";

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

        public static List<XElement> getPostElementList(XDocument doc)
        {
            return doc.Descendants("post").ToList();
        }

        public static string getPostElementValue(XDocument doc, string elementName)
        {
            return doc != null ? doc.Root.Element(elementName) != null ?
                doc.Root.Element(elementName).Value : null : null;
        }

        public static string getQueryString(string tumblrURL, string type, int start = 0, int maxNumPosts = 0)
        {
            string query = string.Copy(queryXML);

            if (!string.IsNullOrEmpty(type))
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

            return tumblrURL + query;
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

        public static Object readObjectAsXML<T>(string filename)
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
                return null;
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