using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Tumbl_Tool.Common_Helpers;
using Tumbl_Tool.Enums;
using Tumbl_Tool.Tumblr_Objects;


namespace Tumbl_Tool.Managers
{
   public class CrawlManager
    {
       public XDocument xmlDocument;
       public dynamic jsonDocument;
       private string mode;
       public CrawlManager()
       {

       }

       public void setMode(string mode)
       {
           this.mode = mode;
       }

       public string getMode()
       {
           return this.mode;
       }

       public void getDocument(string url)
       {
           if (this.mode == "XML")
               getXMLDocument(url);
           else if (mode == "JSON")
               getJSONDocument(url);
       }

       public void getXMLDocument(string url)
       {
           xmlDocument =  XMLHelper.getXMLDocument(url);
       }

       public void getJSONDocument(string url)
       {
           jsonDocument = JSONHelper.getJSONObject(url);
       }

       public int getTotalNumPosts()
       {
           if (mode == "XML" && xmlDocument != null)
           {
               return getTotalNumPostsXML();
           }

           else if (mode == "JSON" && jsonDocument != null)
           {
               return getTotalNumPostsJSON();
           }

           else
           {
               return 0;
           }

       }

       public int getTotalNumPostsXML()
       {
           return Convert.ToInt32(xmlDocument.Root.Element("posts").Attribute("total").Value);
       }


       public int getTotalNumPostsJSON()
       {
           return Convert.ToInt32(jsonDocument.response.blog.posts.ToString());
       }

       public bool isValidTumblr(string url)
       {
           if (mode == "JSON")
               return JSONHelper.getJSONObject(url) != null;
               
           else
               return XMLHelper.getXMLDocument(url) != null;

       }

       public List<TumblrPost> getPostListJSON(string type = "")
       {
           List<TumblrPost> postList = new List<TumblrPost>();
           JArray jPostArray = jsonDocument.response.posts;
           List<dynamic> jPostList = jPostArray.ToObject<List<dynamic>>();

           foreach (dynamic jPost in jPostList)
           {
               TumblrPost post = new TumblrPost();
               if (type == "photo")
               {
                   post = new PhotoPost();
               }

               if (jPost.type != null)
                post.type = jPost.type;

               if (jPost.id != null)
               post.id = jPost.id;

               if (jPost.post_url != null)
               post.url = jPost.post_url;

               if (jPost.caption != null)
               post.caption = jPost.caption;

               if (jPost.date != null)
               post.date = jPost.date;

               if (jPost.format != null)
               post.format = jPost.format;

               if (jPost.reblog_key != null)
               post.reblogKey = jPost.reblog_key;

               if (jPost.tags !=  null)
               {
                   foreach (string tag in jPost.tags)
                   {
                       post.addTag(tag);
                   }
               }

               if (jPost.type == tumblrPostTypes.photo.ToString())
               {

                   if (jPost.photos.Count == 1)
                   {
                       post.imageURL = jPost.photos[0].original_size.url;
                       post.fileName = Path.GetFileName(post.imageURL);
                   }
                   else
                   {
                       foreach (dynamic jPhoto in jPost.photos)
                       {
                           PhotoSetImage setImage = new PhotoSetImage();
                           setImage.imageURL = jPhoto.original_size.url;
                           setImage.filename = !string.IsNullOrEmpty(setImage.imageURL) ? Path.GetFileName(setImage.imageURL) : null;

                           post.addImageToPhotoSet(setImage);
                       }
                   }
               }

               postList.Add(post);
           }
           

           // xmlDocument = JSONHelper.jsonToXML(jsonDocument);

           return postList;
       }


       public List<TumblrPost> getPostListXML(string type = "")
       {
           List<TumblrPost> postList = new List<TumblrPost>();
           List<XElement> postElementList = XMLHelper.getPostElementList(xmlDocument);

           foreach (XElement element in postElementList)
           {
               TumblrPost post = new TumblrPost();
               if (type == tumblrPostTypes.photo.ToString())
               {
                   post = new PhotoPost();
               }

               if (element.Attribute("id") != null)
               {
                   post.id = element.Attribute("id").Value;
               }

               if (element.Attribute("url") != null)
               {
                   post.url = element.Attribute("url").Value;
               }

               if (element.Element("photo-caption") != null)
               {
                   post.caption = element.Element("photo-caption").Value;
               }

               if (element.Attribute("format") != null)
               {
                   post.format = element.Attribute("format").Value;
               }

               if (element.Attribute("unix-timestamp") != null)
               {
                   post.date = element.Attribute("unix-timestamp").Value;
               }

               if (element.Elements("tag") != null)
               {

                   foreach (string tag in element.Elements("tag").ToList())
                   {
                       post.addTag(tag);

                   }
               }

               if (element.Attribute("type") != null)
               {
                   post.type = element.Attribute("type").Value;
               }

               if (element.Attribute("reblog-key") != null)
               {
                   post.reblogKey = element.Attribute("reblog-key").Value;
               }

               if (element.Element("photo-url") != null)
               {
                   post.imageURL = (element.Element("photo-url").Value);
                   post.fileName = Path.GetFileName(post.imageURL);
               }

               // If it is PhotoSet
               XElement photoset = element.Element("photoset");

               if (photoset != null)
               {
                   foreach (XElement setElement in photoset.Descendants("photo"))
                   {
                       XElement image = setElement.Descendants("photo-url").FirstOrDefault();
                       if (image != null)
                       {

                           PhotoSetImage setImage = new PhotoSetImage();
                           setImage.imageURL = image.Value;
                           setImage.filename = !string.IsNullOrEmpty(setImage.imageURL) ? Path.GetFileName(setImage.imageURL) : null;

                           if (setElement.Attribute("photo-caption") != null)
                           {
                               setImage.caption = setElement.Attribute("photo-caption").Value;
                           }

                           if (setElement.Attribute("width") != null)
                           {
                               setImage.width = setElement.Attribute("width").Value;
                           }

                           if (setElement.Attribute("height") != null)
                           {
                               setImage.height = setElement.Attribute("height").Value;
                           }

                           if (setElement.Attribute("offset") != null)
                           {
                               setImage.offset = setElement.Attribute("offset").Value;
                           }

                           post.addImageToPhotoSet(setImage);

                       }
                   }

               }
               postList.Add(post);
           }

           return postList;
       }


       public List<TumblrPost> getPostList( string type = "", string mode = "XML")
       {
           List<TumblrPost> postList = new List<TumblrPost>();

           if (mode == "JSON")
           {
               postList = getPostListJSON(type);
           }

           else
           {
               postList = getPostListXML(type);
           }

           return postList;
       }

       public bool setBlogInfo(string url, Tumblr blog)
       {

           if (mode == apiModeEnum.JSON.ToString())
           {
               return setBlogInfoJSON(url, blog);
               
           }

           else
           {
               return setBlogInfoXML(url, blog);
           }

           
       }

       public bool setBlogInfoXML(string url, Tumblr blog)
       {
           XDocument rDoc = XMLHelper.getXMLDocument(url);
           if (rDoc != null)
           {
               blog.title = XMLHelper.getPostElementAttributeValue(rDoc, "tumblelog", "title");
               blog.description = XMLHelper.getPostElementValue(rDoc, "tumblelog");
               blog.timezone = XMLHelper.getPostElementAttributeValue(rDoc, "tumblelog", "timezone");
               blog.name = XMLHelper.getPostElementAttributeValue(rDoc, "tumblelog", "name");
               blog.totalPosts = XMLHelper.getPostElementValue(rDoc, "posts") != null ? Convert.ToInt32(XMLHelper.getPostElementAttributeValue(rDoc, "posts", "total")) : 0;
               return true;
           }

           return false;
       }

       public bool setBlogInfoJSON(string url, Tumblr blog)
       {
           dynamic jsonDocument = JSONHelper.getJSONObject(url);

           if (jsonDocument != null)
           {
               blog.title = jsonDocument.response.blog.title;
               blog.description = jsonDocument.response.blog.description;
               blog.timezone = "";
               blog.name = jsonDocument.response.blog.name;

               if (jsonDocument.response.total_posts != null)
                   blog.totalPosts = Convert.ToInt32(jsonDocument.response.total_posts);
               else if (jsonDocument.response.blog.posts != null)
                   blog.totalPosts = Convert.ToInt32(jsonDocument.response.blog.posts);

               return true;
           }
           

           return false;
       }
    }
}
