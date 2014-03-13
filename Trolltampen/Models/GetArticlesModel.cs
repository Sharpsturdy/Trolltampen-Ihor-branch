using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolltampen.Models
{
    public class GetArticlesModel
    {
        public List<Article> Articles { get; set; }

        public Category Category { get; set; }
    }
}