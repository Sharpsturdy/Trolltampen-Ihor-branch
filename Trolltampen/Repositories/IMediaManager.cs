using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trolltampen.Models;

namespace Trolltampen.Repositories
{
    public interface IMediaManager
    {
        string SavePhoto(PhotoMediaModel photo);
        string  AddToGallery(int cID, PhotoMediaModel photo);
        void SaveVideoLink(int cID, VideoLinkModel vLink);
        void UpdatePhoto(int cID, PhotoMediaModel photo);
        void UpdateGallery(int cID, PhotoGalleryModel gallery);
        void UpdateVideoLink(int cID, VideoLinkModel vLink);
        void DeletePhoto(string fileName);
        void DeleteGallery(int cID);
        void DeleteVideoLink(int cID);
    }
}
