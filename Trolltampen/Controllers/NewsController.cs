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
    public class NewsController : Controller
    {
        INewsRepository newsRepo;
        public NewsController()
        {
            newsRepo = new NewsRepository(new TTDBContext());
        }

        public NewsController(INewsRepository iNewsRepo)
        {
            newsRepo = iNewsRepo;
        }

        protected override void Dispose(bool disposing)
        {
            newsRepo.Dispose();
            base.Dispose(disposing);
        }
        //
        // GET: /News/
        public ActionResult Index()
        {
            return View(newsRepo.GetNews());
        }
	}
}