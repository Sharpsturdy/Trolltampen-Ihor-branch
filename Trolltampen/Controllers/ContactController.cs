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
    public class ContactController : Controller
    {
         private IContactsRepository contactsRepo;
        public ContactController()
        {
            contactsRepo = new ContactsRepository(new TTDBContext());
        }
        public ContactController(IContactsRepository iContRepo)
        {
            contactsRepo = iContRepo;
        }
        protected override void Dispose(bool disposing)
        {
            contactsRepo.Dispose();
            base.Dispose(disposing);
        }
        //
        // GET: /Contact/
        public ActionResult Index()
        {
            return View(contactsRepo.GetFrontContacts());
        }
	}
}