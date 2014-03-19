using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trolltampen.Models;
using Trolltampen.DAL;
using Trolltampen.Repositories;

namespace Trolltampen.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private IContactsRepository contactsRepo;
        public ContactsController()
        {
            contactsRepo = new ContactsRepository(new TTDBContext());
        }
        public ContactsController(IContactsRepository iContRepo)
        {
            contactsRepo = iContRepo;
        }

        // GET: /Addresses/
        public ActionResult Index()
        {
            return View(contactsRepo.GetAllContacts().ToList());
        }


        // GET: /Addresses/Create
        public ActionResult Create()
        {
            ContactModel model = new ContactModel();
            model.Contact.MediaTypeID = 1;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactModel model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Contact.CreatedOn = DateTime.Now;
            contactsRepo.CreateContact(model);
            return RedirectToAction("Index");

        }

        // GET: /Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactModel model = contactsRepo.GetContactForEdit((int)id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactModel model)
        {
            if (!ModelState.IsValid) return View(model);
            contactsRepo.UpdateContact(model);
            return RedirectToAction("Index");
        }

        // GET: /Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            contactsRepo.DeleteContact((int)id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateOrder(IEnumerable<Contact> contacts)
        {
            contactsRepo.UpdateContactOrderNum(contacts);
            return RedirectToAction("Index");
        }

        public ActionResult ActivateContact(int id, bool toActivate)
        {
            contactsRepo.UpdateContactStatus(id, toActivate);
            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            contactsRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
