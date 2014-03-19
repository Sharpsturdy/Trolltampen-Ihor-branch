using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trolltampen.Models;
using Trolltampen.DAL;
using System.Data.Entity;

namespace Trolltampen.Repositories
{
    public class NewsRepository : FrontRepositoryBase, INewsRepository
    {
        public NewsRepository(TTDBContext db)
            : base(db)
        {

        }

        public NewsFrontModel GetNews()
        {
            NewsFrontModel newsModel = new NewsFrontModel() { Photos = new Dictionary<int, string>() };
            Category cat = db.Categories.FirstOrDefault(c => c.Name.Equals("News", StringComparison.InvariantCultureIgnoreCase));
            if (cat == null) { newsModel.News = new List<Article>(); return newsModel; };
            newsModel.News = db.Articles.Where(a => a.CategoryID == cat.ID && a.IsActive).OrderBy(a => a.OrderNum).Take(2).ToList();
            foreach (var item in newsModel.News)
            {
                Photo photo = db.Photos.FirstOrDefault(p => p.ID == item.ContentID);
                newsModel.Photos.Add(item.ID, photo == null ? string.Empty : "/Images/" + photo.FileName);
            }
            return newsModel;

        }
    }
}