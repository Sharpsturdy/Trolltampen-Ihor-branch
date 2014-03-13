using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trolltampen.Repositories;
using Trolltampen.Models;
using Trolltampen.DAL;

namespace Trolltampen.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        protected ICategoryRepository categoryRepo;
        public CategoriesController()
        {
            this.categoryRepo = new CategoryRepository(new TTDBContext());
        }
        public CategoriesController(ICategoryRepository icategoryRepo)
        {
            this.categoryRepo = icategoryRepo;
        }

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return View("IndexAjax", categoryRepo.GetAllCategories().ToList());
            }
            return View(categoryRepo.GetAllCategories().ToList());
        }

        public ActionResult GetCategoryList()
        {
            return View(categoryRepo.GetAllCategories());
        }

        public ActionResult CreateCategory()
        {
            return View(new CategoryModel() { MediaTypes = categoryRepo.GetMediaTypesSelectList() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryModel cat)
        {
            if (ModelState.IsValid)
            {
                categoryRepo.CreateCategory(cat);
                return RedirectToAction("Index");
            }
            cat.MediaTypes = categoryRepo.GetMediaTypesSelectList();
            return View(cat);
        }

        public ActionResult DeleteCategory(int cID)
        {
            categoryRepo.DeleteCategory(cID);
            return RedirectToAction("Index");
        }

        public ActionResult ActivateCategory(int cID, bool toActivate)
        {
            categoryRepo.UpdateCategoryStatus(cID, toActivate);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateOrder(IEnumerable<Category> categories)
        {
            categoryRepo.UpdateCategoryOrderNum(categories);
            return RedirectToAction("Index");
        }


        public ActionResult EditCategory(int cID)
        {

            CategoryModel model = categoryRepo.GetCategoryForEdit(cID);
            if (model != null) return View(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                categoryRepo.UpdateCategory(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            categoryRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}