using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Trolltampen.Repositories
{
    public class MediaManager:IMediaManager
    {

        public string SavePhoto(Models.PhotoMediaModel photo)
        {
            HttpPostedFileBase file = photo.PhotoFile;
            string path = HttpContext.Current.Server.MapPath("~/Images/");
            string fileName = photo.FileName;
            file.SaveAs(path + fileName);
            return fileName;
        }

        public void SaveVideoLink(int cID, Models.VideoLinkModel vLink)
        {
            throw new NotImplementedException();
        }

        public void UpdatePhoto(int cID, Models.PhotoMediaModel photo)
        {
            throw new NotImplementedException();
        }

        public void UpdateGallery(int cID, Models.PhotoGalleryModel gallery)
        {
            throw new NotImplementedException();
        }

        public void UpdateVideoLink(int cID, Models.VideoLinkModel vLink)
        {
            throw new NotImplementedException();
        }

        public void DeletePhoto(string fileName)
        {
            File.Delete(HttpContext.Current.Server.MapPath("~/Images/") + fileName);
        }

        public void DeleteGallery(int cID)
        {
            throw new NotImplementedException();
        }

        public void DeleteVideoLink(int cID)
        {
            throw new NotImplementedException();
        }


        public string AddToGallery(int cID, Models.PhotoMediaModel photo)
        {
            throw new NotImplementedException();
        }
    }
}