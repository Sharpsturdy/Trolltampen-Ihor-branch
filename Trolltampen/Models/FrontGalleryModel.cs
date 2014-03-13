using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolltampen.Models
{
    public class FrontGalleryModel
    {
        public FrontGalleryModel()
        {
            Photos = new ContentModel();
        }
        public ContentModel Photos { get; set; }
        public int GalleryID { get; set; }
    }
}