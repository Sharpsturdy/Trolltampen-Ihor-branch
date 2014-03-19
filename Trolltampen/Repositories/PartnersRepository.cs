using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trolltampen.Models;
using Trolltampen.DAL;
using System.Data.Entity;

namespace Trolltampen.Repositories
{
    public class PartnersRepository : FrontRepositoryBase, IPartnersRepository
    {
        public PartnersRepository(TTDBContext db)
            : base(db)
        {

        }

        public IEnumerable<Article> GetPartners()
        {
            Category cat = db.Categories.FirstOrDefault(c => c.Name.Equals("Partners", StringComparison.InvariantCultureIgnoreCase));
            if (cat == null) return new List<Article>();
            return db.Articles.Where(a => a.CategoryID == cat.ID && a.IsActive).OrderBy(a => a.OrderNum);
        }
    }
}