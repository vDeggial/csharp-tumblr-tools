using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Tumbl_Tool.Common_Helpers
{
    public static class XMLHelper
    {
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