using System.IO;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Tumblr_Objects;

namespace Tumblr_Tool.Common_Helpers
{
    public static class PostHelper
    {
        public static void createPhotoPost(ref TumblrPost post, dynamic jPost)
        {
            post = new PhotoPost();

            if (jPost.type == tumblrPostTypes.photo.ToString())
            {
                foreach (dynamic jPhoto in jPost.photos)
                {
                    PhotoPostImage postImage = new PhotoPostImage();
                    postImage.imageURL = jPhoto.original_size.url;
                    postImage.filename = !string.IsNullOrEmpty(postImage.imageURL) ? Path.GetFileName(postImage.imageURL) : null;

                    post.photos.Add(postImage);
                }
            }
        }

        public static void createTumblrPost(ref TumblrPost post, dynamic jPost)
        {
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

            if (jPost.tags != null)
            {
                foreach (string tag in jPost.tags)
                {
                    post.tags.Add(tag);
                }
            }
        }
    }
}