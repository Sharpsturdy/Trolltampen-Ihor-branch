using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolltampen.Models
{
    public class ContactsFrontModel
    {
        public List<Contact> Contacts { get; set; }
        public Dictionary<int, string> Photos { get; set; }
    }
}