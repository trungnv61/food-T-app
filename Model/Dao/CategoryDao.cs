using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;
using PagedList;

namespace Model.Dao
{
   public class CategoryDao
    {
        FoodOnlineDbContext db = null;
        public CategoryDao()
        {
            db = new FoodOnlineDbContext();
        }

  
        public List<Category> ListNewProduct(int top)
        {
            return db.Categories.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.CategoryId;
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {

            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CategoryId).ToPagedList(page, pageSize);
        }

        // update
        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Where(value => value.CategoryId == entity.CategoryId).SingleOrDefault();
                category.Name = entity.Name;
                category.ImageUrl = entity.ImageUrl;
                //category.IsActive = category.IsActive;
                //category.CreatedDate = category.CreatedDate;
                db.SaveChanges();
                return true;
            }
            catch (NullReferenceException e)
            {
                return false;
            }
        }


        // delete
        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
