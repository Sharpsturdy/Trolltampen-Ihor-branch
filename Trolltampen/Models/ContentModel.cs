using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolltampen.Models
{
    public class ContentModel
    {
        public ContentModel()
        {
            Gallery = new PhotoGalleryModel();
        }
        public int MediaID { get; set; }
        public PhotoMediaModel Photo { get; set; }
        public PhotoGalleryModel Gallery { get; set; }
        public VideoLinkModel VideoLink { get; set; }
    }
}