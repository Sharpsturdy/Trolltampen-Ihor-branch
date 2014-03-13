using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trolltampen.Models;

namespace Trolltampen.Repositories
{
    public interface IGalleryRepository:IDisposable
    {
        void UpdatePhotos(FrontGalleryModel model);
        FrontGalleryModel GetFrontGallery();
        void AddPhotoToGallery(PhotoMediaModel model);
        void UpdateOrderNum(FrontGalleryModel model);
        void DeleteFrontPhoto(int pID);
        void ToActive(int pID, bool toActive);

    }
}
