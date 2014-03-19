using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trolltampen.Models;
using Trolltampen.DAL;


namespace Trolltampen.Repositories
{
    public class ContactsRepository : ContentRepositoryBase, IContactsRepository
    {
        public ContactsRepository(TTDBContext db)
            : base(db)
        {

        }

        public void CreateContact(ContactModel model)
        {
            if (model != null)
            {
                model.Contact.MediaTypeID = 1;
                model.Contact.OrderNum = db.Contacts.Count() + 1;
                model.Contact.IsActive = true;
                Contact newContact = db.Contacts.Add(model.Contact);
                db.SaveChanges();
                model.Content.MediaID = model.Contact.MediaTypeID;
                newContact.ContentID = AddContent(model.Content, newContact.MediaTypeID);
                db.SaveChanges();
            }
        }

        public void DeleteContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null) return;
            if (contact.ContentID == 0)
            {
                db.Contacts.Remove(contact);
                db.SaveChanges();
            }
            else
            {
                db.Contacts.Remove(contact);
                DeleteContent(contact.MediaTypeID, contact.ContentID);
                db.SaveChanges();
            }
        }

        public void UpdateContactStatus(int id, bool isActive)
        {
            Contact c = db.Contacts.Find(id);
            if (c != null)
            {
                c.IsActive = isActive;
                db.SaveChanges();
            }
        }

        public void UpdateContactOrderNum(IEnumerable<Contact> contacts)
        {
            if (contacts == null) return;
            int currentNum = 0;
            foreach (var item in contacts.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.Contacts.FirstOrDefault(c => c.ID == item.ID).OrderNum = currentNum;
            }
            db.SaveChanges();
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return db.Contacts.OrderBy(c => c.OrderNum);
        }

        public ContactModel GetContactForEdit(int id)
        {
            ContactModel model = new ContactModel();
            Contact contact = db.Contacts.FirstOrDefault(v => v.ID == id);
            if (contact == null) return null;
            model.Contact = contact;
            model.Content = GetContent(model.Contact.MediaTypeID, model.Contact.ContentID);
            return model;
        }

        public void UpdateContact(ContactModel model)
        {
            Contact updcontact = db.Contacts.Find(model.Contact.ID);
            if (updcontact == null) return;
            updcontact.City = model.Contact.City;
            updcontact.Description = model.Contact.Description;
            updcontact.Email = model.Contact.Email;
            updcontact.FirstName = model.Contact.FirstName;
            updcontact.LastName = model.Contact.LastName;
            updcontact.PhoneNum = model.Contact.PhoneNum;
            updcontact.Position = model.Contact.Position;
            updcontact.PostNo = model.Contact.PostNo;
            updcontact.Sted = model.Contact.Sted;
            db.SaveChanges();
            model.Content.MediaID = updcontact.MediaTypeID;
            updcontact.ContentID = UpdateMedia(updcontact.ContentID, model.Content);
            db.SaveChanges();
        }


        public ContactsFrontModel GetFrontContacts()
        {
            ContactsFrontModel contactsModel = new ContactsFrontModel() { Photos = new Dictionary<int, string>() };
            contactsModel.Contacts = db.Contacts.Where(a => a.IsActive).OrderBy(a => a.OrderNum).ToList();
            foreach (var item in contactsModel.Contacts)
            {
                Photo photo = db.Photos.FirstOrDefault(p => p.ID == item.ContentID);
                contactsModel.Photos.Add(item.ID, photo == null ?  VirtualPathUtility.ToAbsolute("/Content/img/grey_img.png"): "/Images/" + photo.FileName);
            }
            return contactsModel;
        }
    }
}