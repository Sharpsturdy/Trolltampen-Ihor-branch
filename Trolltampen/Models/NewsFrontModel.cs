using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trolltampen.Models
{
    public class NewsFrontModel
    {
        public IEnumerable<Article> News { get; set; }
        public Dictionary<int, string> Photos { get; set; }
    }
}