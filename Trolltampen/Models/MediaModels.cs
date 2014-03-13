using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolltampen.Models
{
    public class PhotoMediaModel
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public HttpPostedFileBase PhotoFile { get; set; }
        public int OrderNum { get; set; }
        public bool IsActive { get; set; }
    }

    public class PhotoGalleryModel
    {
        public PhotoGalleryModel()
        {
            PhotoFiles = new List<PhotoMediaModel>();
        }
        public List<PhotoMediaModel> PhotoFiles { get; set; } 
    }

    public class VideoLinkModel
    {
        public string Link { get; set; }
    }
}