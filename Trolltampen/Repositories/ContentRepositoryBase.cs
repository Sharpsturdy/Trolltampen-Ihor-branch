using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trolltampen.Models;
using Trolltampen.DAL;

namespace Trolltampen.Repositories
{
    public class ContentRepositoryBase
    {
        protected TTDBContext db;
        protected IMediaManager mm;
        protected bool disposed = false;
        public ContentRepositoryBase(TTDBContext db)
        {
            this.db = db;
            this.mm = new MediaManager();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected int AddContent(ContentModel model, int mediaID)
        {
            int contentID = 0;
            switch (mediaID)
            {
                case 1:
                    if (model.Photo == null || model.Photo.FileName == null || model.Photo.PhotoFile == null) break;
                    string filename = mm.SavePhoto(model.Photo);
                    Photo p = db.Photos.Add(new Photo() { MediaTypeID = mediaID, FileName = filename });
                    db.SaveChanges();
                    contentID = p.ID;
                    break;
                case 2:
                    if (model.VideoLink == null || model.VideoLink.Link == null) break;
                    VideoLink vl = db.VideoLinks.Add(new VideoLink() { MediaTypeID = 2, Link = model.VideoLink.Link });
                    db.SaveChanges();
                    contentID = vl.ID;
                    break;
                case 3:
                    foreach (var photo in model.Gallery.PhotoFiles.ToList())
                    {
                        if (string.IsNullOrEmpty(photo.FileName) && photo.PhotoFile == null)
                        {
                            model.Gallery.PhotoFiles.Remove(photo);
                        }
                    }
                    if (model.Gallery == null || model.Gallery.PhotoFiles == null || model.Gallery.PhotoFiles.Count == 0) break;
                    PhotoGallery pg = db.PhotoGalleries.Add(new PhotoGallery() { MediaTypeID = 3 });
                    db.SaveChanges();
                    foreach (var item in model.Gallery.PhotoFiles)
                    {
                        if (item == null || item.FileName == null || item.PhotoFile == null) continue;
                        string fileName = mm.SavePhoto(item);
                        db.GalleryPhotos.Add(new GalleryPhoto() { PhotoGalleryID = pg.ID, FileName = fileName });
                    }
                    db.SaveChanges();
                    contentID = pg.ID;
                    break;
                default:
                    break;
            }
            return contentID;
        }
        protected void DeleteContent(int mediaID, int contentID)
        {
            switch (mediaID)
            {
                case 1:
                    Photo delPhoto = db.Photos.Find(contentID);
                    if (delPhoto == null) return;
                    mm.DeletePhoto(delPhoto.FileName);
                    db.Photos.Remove(delPhoto);
                    return;
                case 2:
                    VideoLink vl = db.VideoLinks.Find(contentID);
                    if (vl == null) return;
                    db.VideoLinks.Remove(vl);
                    return;
                case 3:
                    PhotoGallery pg = db.PhotoGalleries.Find(contentID);
                    foreach (var photo in db.GalleryPhotos.Where(p => p.PhotoGalleryID == pg.ID).ToList())
                    {
                        mm.DeletePhoto(photo.FileName);
                        db.GalleryPhotos.Remove(photo);
                    }
                    db.PhotoGalleries.Remove(pg);
                    return;
                default:
                    return;
            }
        }
        protected ContentModel GetContent(int mediaID, int contentID)
        {
            ContentModel cmodel = new ContentModel();
            switch (mediaID)
            {
                case 1:
                    Photo photo = db.Photos.Find(contentID);
                    PhotoMediaModel photomodel = new PhotoMediaModel();
                    if (photo == null) { cmodel.Photo = null; break; }
                    photomodel.FileName = photo.FileName;
                    cmodel.Photo = photomodel;
                    break;
                case 2:
                    VideoLink vl = db.VideoLinks.Find(contentID);
                    if (vl == null) { cmodel.VideoLink = null; break; }
                    VideoLinkModel vlModel = new VideoLinkModel();
                    vlModel.Link = vl.Link;
                    cmodel.VideoLink = vlModel;
                    break;
                case 3:
                    PhotoGallery pg = db.PhotoGalleries.Find(contentID);
                    if (pg == null) { cmodel.Gallery = null; break; }
                    PhotoGalleryModel pgModel = new PhotoGalleryModel();
                    foreach (var p in db.GalleryPhotos.Where(g => g.PhotoGalleryID == pg.ID))
                    {
                        pgModel.PhotoFiles.Add(new PhotoMediaModel()
                        {
                            FileName = p.FileName,
                        });
                    }
                    cmodel.Gallery = pgModel;
                    break;
                case 4:
                    break;
                case 5:
                    cmodel.Gallery = new PhotoGalleryModel();
                    foreach (var item in db.GalleryPhotos.Where(p=>p.PhotoGalleryID==contentID))
                    {
                        cmodel.Gallery.PhotoFiles.Add(new PhotoMediaModel()
                        {
                            FileName=item.FileName,
                            ID=item.ID,
                            IsActive=item.isActive,
                            OrderNum=item.OrderNum
                        });
                    }
                    break;
                default:
                    break;
            }
            cmodel.MediaID = mediaID;
            return cmodel;
        }
        protected int UpdateMedia(int currentContentID, ContentModel model)
        {
            int updatedContentID = 0;
            switch (model.MediaID)
            {
                case 1:
                    if (currentContentID == 0)
                    {
                        if (model.Photo.FileName != null)
                        {
                            string newfile = mm.SavePhoto(model.Photo);
                            Photo newphoto = db.Photos.Add(new Photo()
                            {
                                FileName = newfile,
                                MediaTypeID = model.MediaID
                            });
                            db.SaveChanges();
                            updatedContentID = newphoto.ID;
                        }
                    }
                    else
                    {
                        Photo currentphoto = db.Photos.Find(currentContentID);
                        if (currentphoto == null) { updatedContentID = 0; break; }
                        if (model.Photo.FileName != null)
                        {
                            if (model.Photo.FileName == currentphoto.FileName) { updatedContentID = currentContentID; break; }
                            //Delete current file
                            mm.DeletePhoto(currentphoto.FileName);
                            //Save new file
                            currentphoto.FileName = mm.SavePhoto(model.Photo);
                            updatedContentID = currentContentID;
                        }
                        else
                        {
                            DeleteContent(model.MediaID, currentContentID);
                            //mm.DeletePhoto(currentphoto.FileName);
                            updatedContentID = 0;
                        }
                    }
                    break;
                case 2:
                    if (currentContentID == 0)
                    {
                        if (model.VideoLink != null && model.VideoLink.Link != null)
                        {
                            VideoLink link = db.VideoLinks.Add(new VideoLink()
                            {
                                Link = model.VideoLink.Link,
                                MediaTypeID = model.MediaID
                            });
                            db.SaveChanges();
                            updatedContentID = link.ID;
                        }
                    }
                    else
                    {
                        VideoLink vl = db.VideoLinks.Find(currentContentID);
                        if (vl == null) { updatedContentID = 0; break; }
                        if (model.VideoLink.Link == null)
                        {
                            DeleteContent(model.MediaID, currentContentID);
                            updatedContentID = 0;
                        }
                        else
                        {
                            vl.Link = model.VideoLink.Link;
                            updatedContentID = currentContentID;
                        }
                    }
                    break;
                case 3:
                    foreach (var photo in model.Gallery.PhotoFiles.ToList())
                    {
                        if (string.IsNullOrEmpty(photo.FileName) && photo.PhotoFile == null)
                        {
                            model.Gallery.PhotoFiles.Remove(photo);
                        }
                    }
                    //Remove all photos if user deleted all photos in gallery
                    if (model.Gallery.PhotoFiles.Count == 0)
                    {
                        DeleteContent(model.MediaID, currentContentID);
                        updatedContentID = 0;
                        break;
                    }
                    if (currentContentID == 0)
                    {
                        PhotoGallery pg = db.PhotoGalleries.Add(new PhotoGallery() { MediaTypeID = 3 });
                        db.SaveChanges();
                        updatedContentID = pg.ID;
                    }
                    else
                    {
                        updatedContentID = currentContentID;
                    }
                    //Iterate through new list to add in current galley
                    foreach (var photo in model.Gallery.PhotoFiles)
                    {
                        if (photo == null || photo.FileName == null) continue;

                        if (!db.GalleryPhotos.Where(g => g.ID == updatedContentID).Any(p => p.FileName == photo.FileName))
                        {
                            if (photo.PhotoFile == null) continue;
                            string filename = mm.SavePhoto(photo);
                            db.GalleryPhotos.Add(new GalleryPhoto() { FileName = filename, PhotoGalleryID = updatedContentID });
                        }
                    }
                    db.SaveChanges();
                    //Iterate through current list to remove absent images
                    foreach (var item in db.GalleryPhotos.Where(g => g.PhotoGalleryID == updatedContentID))
                    {
                        if (!model.Gallery.PhotoFiles.Any(p => p.FileName == item.FileName))
                        {
                            mm.DeletePhoto(item.FileName);
                            db.GalleryPhotos.Remove(item);
                        }
                    }
                    db.SaveChanges();
                    break;
                case 5:
                      foreach (var photo in model.Gallery.PhotoFiles.ToList())
                    {
                        if (string.IsNullOrEmpty(photo.FileName) && photo.PhotoFile == null)
                        {
                            model.Gallery.PhotoFiles.Remove(photo);
                        }
                    }
                    //Remove all photos if user deleted all photos in gallery
                    if (model.Gallery.PhotoFiles.Count == 0)
                    {
                        DeleteContent(model.MediaID, 0);
                        updatedContentID = 0;
                        break;
                    }
                    if (currentContentID == 0)
                    {
                        PhotoGallery pg = db.PhotoGalleries.Add(new PhotoGallery() { MediaTypeID = 3 });
                        db.SaveChanges();
                        updatedContentID = pg.ID;
                    }
                    else
                    {
                        updatedContentID = currentContentID;
                    }
                    //Iterate through new list to add in current galley
                    foreach (var photo in model.Gallery.PhotoFiles)
                    {
                        if (photo == null || photo.FileName == null) continue;

                        if (!db.GalleryPhotos.Where(g => g.ID == updatedContentID).Any(p => p.FileName == photo.FileName))
                        {
                            if (photo.PhotoFile == null) continue;
                            string filename = mm.SavePhoto(photo);
                            db.GalleryPhotos.Add(new GalleryPhoto() { FileName = filename, PhotoGalleryID = updatedContentID });
                        }
                    }
                    db.SaveChanges();
                    //Iterate through current list to remove absent images
                    foreach (var item in db.GalleryPhotos.Where(g => g.PhotoGalleryID == updatedContentID))
                    {
                        if (!model.Gallery.PhotoFiles.Any(p => p.FileName == item.FileName))
                        {
                            mm.DeletePhoto(item.FileName);
                            db.GalleryPhotos.Remove(item);
                        }
                    }
                    db.SaveChanges();
                    break;
                case 4:
                    break;
                default:
                    break;
            }
            return updatedContentID;
        }


    }
}