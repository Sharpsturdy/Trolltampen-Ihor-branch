using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolltampen.Models
{
    public class ContactModel
    {
        public ContactModel()
        {
            Contact = new Contact();
            Content = new ContentModel();
        }
        public Contact Contact { get; set; }
        public ContentModel Content { get; set; }
             
    }
}