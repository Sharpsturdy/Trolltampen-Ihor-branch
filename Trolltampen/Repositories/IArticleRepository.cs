using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trolltampen.Models;
using System.Web.Mvc;

namespace Trolltampen.Repositories
{
    public interface IArticleRepository : IDisposable
    {
        void CreateArticle(ArticleModel cat);
        void DeleteArticle(int aID);
        void UpdateArticleStatus(int aID, bool isActive);
        void UpdateArticleOrderNum(IEnumerable<Article> categories);
        IEnumerable<Article> GetAllArticles();
        GetArticlesModel GetArticlesInCategory(int cID = 0);

        ArticleModel GetArticleForEdit(int aID);
        void UpdateArticle(ArticleModel article);

        IEnumerable<MediaType> GetMediaTypes();
        List<SelectListItem> GetMediaTypesSelectList();
    }
}
