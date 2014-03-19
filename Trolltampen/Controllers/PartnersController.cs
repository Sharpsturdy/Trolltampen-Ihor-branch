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
    public class PartnersController : Controller
    {
        IPartnersRepository partnersRepo;
        public PartnersController()
        {
            partnersRepo = new PartnersRepository(new TTDBContext());
        }

        public PartnersController(IPartnersRepository iPartnersRepo)
        {
            partnersRepo = iPartnersRepo;
        }

        protected override void Dispose(bool disposing)
        {
            partnersRepo.Dispose();
            base.Dispose(disposing);
        }
        //
        // GET: /Partners/
        public ActionResult Index()
        {
            return View(partnersRepo.GetPartners());
        }
	}
}