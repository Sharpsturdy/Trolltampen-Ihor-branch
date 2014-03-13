using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Trolltampen.Models;
using Trolltampen.DAL;
using System.Data.Entity;

namespace Trolltampen.Repositories
{
    public class CategoryRepository:ContentRepositoryBase,ICategoryRepository
    {

        public CategoryRepository(TTDBContext db)
            : base(db)
        {

        }
        public void CreateCategory(CategoryModel cat)
        {
            if (cat != null)
            {
                if (cat.Category.MediaTypeID == 0) cat.Category.MediaTypeID = 4;
                cat.Category.OrderNum = db.Categories.Count() + 1;
                cat.Category.IsActive = true;
                Category newCat = db.Categories.Add(cat.Category);
                db.SaveChanges();
                cat.Content.MediaID = cat.Category.MediaTypeID;
                newCat.ContentID = AddContent(cat.Content, newCat.MediaTypeID);
                db.SaveChanges();
            }
        }

        public void UpdateCategoryName(Category cat)
        {
            if (cat != null)
            {
                Category c = db.Categories.Find(cat.ID);
                if (c != null)
                {
                    c.Name = cat.Name;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteCategory(int cID)
        {
            Category cat = db.Categories.Find(cID);
            if (cat != null)
            {
                if (cat.Articles.Count == 0)
                {
                    if (cat.ContentID == 0)
                    {
                        db.Categories.Remove(cat);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Categories.Remove(cat);
                        DeleteContent(cat.MediaTypeID, cat.ContentID);
                        db.SaveChanges();
                    }

                }
            }
        }

        public void UpdateCategoryStatus(int cID, bool isActive)
        {
            Category c = db.Categories.Find(cID);
            if (c != null)
            {
                c.IsActive = isActive;
                db.SaveChanges();
            }
        }

        public void UpdateCategoryOrderNum(IEnumerable<Category> categories)
        {

            if (categories == null) return;
            int totalProducts = categories.Count();
            int currentNum = 0;
            foreach (var item in categories.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.Categories.FirstOrDefault(c => c.ID == item.ID).OrderNum = currentNum;
            }
            db.SaveChanges();

        }

        public IEnumerable<Category> GetAllCategories()
        {
            return db.Categories.OrderBy(c => c.OrderNum);
        }

        public CategoryModel GetCategoryForEdit(int categoryID)
        {
            CategoryModel model = new CategoryModel();
            Category category = db.Categories.Include(c => c.MediaType).FirstOrDefault(v => v.ID == categoryID);
            if (category == null) return null;
            model.Category = category;
            model.Content = GetContent(model.Category.MediaTypeID, model.Category.ContentID);
            return model;

        }

        public void UpdateCategory(CategoryModel category)
        {
            Category updcat = db.Categories.Find(category.Category.ID);
            if (updcat == null) return;
            updcat.Name = category.Category.Name;
            db.SaveChanges();
            category.Content.MediaID = category.Category.MediaTypeID;
            updcat.ContentID = UpdateMedia(updcat.ContentID, category.Content);
            db.SaveChanges();
        }

        public IEnumerable<Models.MediaType> GetMediaTypes()
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
    }
}