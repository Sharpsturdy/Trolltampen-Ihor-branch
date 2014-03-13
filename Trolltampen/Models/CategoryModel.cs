using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Trolltampen.App_Code;

namespace Trolltampen.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            Category = new Category();
            Content = new ContentModel();
        }
        public Category Category { get; set; }
        public IEnumerable<SelectListItem> MediaTypes { get; set; }
        public ContentModel Content { get; set; }


    }
}
