using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trolltampen.DAL;
using Trolltampen.Models;
using Trolltampen.Repositories;

namespace Trolltampen.Controllers
{
    [Authorize]
    public class FrontGalleryController : Controller
    {

        IGalleryRepository frontGallRepo;
        public FrontGalleryController()
        {
            frontGallRepo = new GalleryRepository(new TTDBContext());
        }

        public FrontGalleryController(IGalleryRepository igalRepo)
        {
            frontGallRepo = igalRepo;
        }
        //
        // GET: /FrontGallery/
        public ActionResult Index()
        {
            return View(frontGallRepo.GetFrontGallery());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFrontGallery(FrontGalleryModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View("Index", model);
        }

        [HttpPost]
    [ValidateAntiForgeryToken]
        public ActionResult AddPhotoToFrontGallery(PhotoMediaModel model)
        {
            if (!string.IsNullOrEmpty(model.FileName)&&model.PhotoFile!=null)
            {
                frontGallRepo.AddPhotoToGallery(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateOrderNum(FrontGalleryModel model)
        {
            frontGallRepo.UpdateOrderNum(model);
            return RedirectToAction("Index");
        }
        public ActionResult ActivateFrontPhoto(int pID, bool toActivate)
        {
            frontGallRepo.ToActive(pID, toActivate);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteFrontPhoto(int pID)
        {
            frontGallRepo.DeleteFrontPhoto(pID);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            frontGallRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}