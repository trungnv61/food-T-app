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

        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.CategoryId;
        }

        public IEnumerable<Category> ListAllPaging(int page, int pageSize)
        {
            return db.Categories.OrderByDescending(x => x.CategoryId).ToPagedList(page, pageSize);
        }
    }
}
