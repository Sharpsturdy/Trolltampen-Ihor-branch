using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trolltampen.Models;

namespace Trolltampen.Repositories
{
    public interface IContactsRepository:IDisposable
    {
        void CreateContact(ContactModel model);
        void DeleteContact(int id);
        void UpdateContactStatus(int id, bool isActive);
        void UpdateContactOrderNum(IEnumerable<Contact> categories);
        IEnumerable<Contact> GetAllContacts();
        ContactModel GetContactForEdit(int id);
        void UpdateContact(ContactModel model);

        ContactsFrontModel GetFrontContacts();
    }
}
