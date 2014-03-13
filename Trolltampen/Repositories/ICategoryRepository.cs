using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trolltampen.Models;
using System.Web.Mvc;

namespace Trolltampen.Repositories
{
    public interface ICategoryRepository:IDisposable
    {
        void CreateCategory(CategoryModel cat);
        void UpdateCategoryName(Category cat);
        void DeleteCategory(int cID);
        void UpdateCategoryStatus(int cID, bool isActive);
        void UpdateCategoryOrderNum(IEnumerable<Category> categories);
        IEnumerable<Category> GetAllCategories();
        CategoryModel GetCategoryForEdit(int categoryID);
        void UpdateCategory(CategoryModel category);

        //Mediatypes methods
        IEnumerable<MediaType> GetMediaTypes();
        List<SelectListItem> GetMediaTypesSelectList();
    }
}
