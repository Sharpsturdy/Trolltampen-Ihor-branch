using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trolltampen.Models;
using Trolltampen.DAL;

namespace Trolltampen.Repositories
{
    public class GalleryRepository : ContentRepositoryBase, IGalleryRepository
    {
        public GalleryRepository(TTDBContext db)
            : base(db)
        {

        }

        public void UpdatePhotos(FrontGalleryModel model)
        {

        }

        public FrontGalleryModel GetFrontGallery()
        {
            FrontGalleryModel model = new FrontGalleryModel();
            PhotoGallery frontGallery = db.PhotoGalleries.FirstOrDefault(g => g.MediaTypeID == 5);
            if (frontGallery == null) return new FrontGalleryModel();
            model.GalleryID = frontGallery.ID;
            model.Photos.Gallery.PhotoFiles = GetContent(frontGallery.MediaTypeID, frontGallery.ID).Gallery.PhotoFiles.OrderBy(p => p.OrderNum).ToList();
            return model;
        }


        public void AddPhotoToGallery(PhotoMediaModel model)
        {
            PhotoGallery pg = db.PhotoGalleries.FirstOrDefault(g => g.MediaTypeID == 5);
            if (pg == null) { pg = db.PhotoGalleries.Add(new PhotoGallery() { MediaTypeID = 5 }); db.SaveChanges(); }
            db.GalleryPhotos.Add(new GalleryPhoto()
                {
                    PhotoGalleryID = pg.ID,
                    isActive = true,
                    OrderNum = db.GalleryPhotos.Where(p => p.PhotoGalleryID == pg.ID).Count() + 1,
                    FileName = mm.SavePhoto(model)
                });
            db.SaveChanges();
        }


        public void UpdateOrderNum(FrontGalleryModel model)
        {
            if (model == null || model.Photos == null || model.Photos.Gallery == null || model.Photos.Gallery.PhotoFiles == null) return;
            int totalProducts = model.Photos.Gallery.PhotoFiles.Count;
            int currentNum = 0;
            foreach (var item in model.Photos.Gallery.PhotoFiles.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.GalleryPhotos.FirstOrDefault(p => p.ID == item.ID).OrderNum = currentNum;
            }
            db.SaveChanges();
        }


        public void DeleteFrontPhoto(int pID)
        {
            GalleryPhoto p= db.GalleryPhotos.Find(pID);
            if(p!=null)
            {
                mm.DeletePhoto(p.FileName);
                db.GalleryPhotos.Remove(p);
                db.SaveChanges();
                
            }
        }

        public void ToActive(int pID, bool toActive)
        {
            GalleryPhoto p= db.GalleryPhotos.Find(pID);
            if (p != null)
            {
                p.isActive = toActive;
                db.SaveChanges();
            }
        }
    }
}