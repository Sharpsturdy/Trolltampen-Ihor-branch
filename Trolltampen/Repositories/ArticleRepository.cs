using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Trolltampen.Models;
using Trolltampen.DAL;
using System.Data.Entity;

namespace Trolltampen.Repositories
{
    public class ArticleRepository : ContentRepositoryBase, IArticleRepository
    {
        public ArticleRepository(TTDBContext db)
            : base(db)
        {

        }
        public void CreateArticle(ArticleModel article)
        {
            if (article != null)
            {
                if (article.Article.MediaTypeID == 0) article.Article.MediaTypeID = 4;
                article.Article.CreatedOn = DateTime.Now;
                article.Article.OrderNum = db.Articles.Count() + 1;
                article.Article.IsActive = true;
                Article newArt = db.Articles.Add(article.Article);
                db.SaveChanges();
                newArt.ContentID = AddContent(article.Content, newArt.MediaTypeID);
                db.SaveChanges();
            }
        }

        public void DeleteArticle(int aID)
        {
            Article art = db.Articles.Find(aID);
            if (art != null)
            {
                if (art.ContentID == 0)
                {
                    db.Articles.Remove(art);
                    db.SaveChanges();
                }
                else
                {
                    db.Articles.Remove(art);
                    DeleteContent(art.MediaTypeID, art.ContentID);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateArticleStatus(int aID, bool isActive)
        {
            Article a = db.Articles.Find(aID);
            if (a != null)
            {
                a.IsActive = isActive;
                db.SaveChanges();
            }
        }

        public void UpdateArticleOrderNum(IEnumerable<Models.Article> articles)
        {
            if (articles == null) return;
            int totalProducts = articles.Count();
            int currentNum = 0;
            foreach (var item in articles.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.Articles.FirstOrDefault(c => c.ID == item.ID).OrderNum = currentNum;
            }
            db.SaveChanges();
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return db.Articles.OrderBy(a => a.OrderNum);
        }

        public GetArticlesModel GetArticlesInCategory(int cID = 0)
        {
            GetArticlesModel m = new GetArticlesModel();

            if (cID != 0)
            {
                m.Articles = db.Articles.Where(a => a.CategoryID == cID).OrderBy(a=>a.OrderNum).ToList();
                m.Category = db.Categories.Find(cID);
                return m;
            }
            m.Category = db.Categories.FirstOrDefault();
            m.Articles = db.Articles.Where(a => a.CategoryID == m.Category.ID).OrderBy(a => a.OrderNum).ToList();
            return m;
        }

        public IEnumerable<MediaType> GetMediaTypes()
        {
            return db.MediaTypes;
        }

        public List<SelectListItem> GetMediaTypesSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var mt in GetMediaTypes())
            {
                if (mt.ID == 5) continue;
                list.Add(new SelectListItem()
                {
                    Text = mt.Type,
                    Value = mt.ID.ToString(),
                    Selected = mt.ID == 4 ? true : false
                });
            }
            return list;
        }

        public ArticleModel GetArticleForEdit(int aID)
        {
            ArticleModel model = new ArticleModel();
            Article article = db.Articles.FirstOrDefault(v => v.ID == aID);
            if (article == null) return null;
            model.Article = article;
            model.Content = GetContent(article.MediaTypeID, article.ContentID);
            return model;
        }

        public void UpdateArticle(ArticleModel article)
        {
            Article updArt = db.Articles.Find(article.Article.ID);
            if (updArt == null) return;
            updArt.Headline = article.Article.Headline;
            updArt.ExtLink = article.Article.ExtLink;
            updArt.Ingress = article.Article.Ingress;
            updArt.Body = article.Article.Body;
            db.SaveChanges();
            article.Content.MediaID = article.Article.MediaTypeID;
            updArt.ContentID = UpdateMedia(updArt.ContentID, article.Content);
            db.SaveChanges();
        }

    }


}