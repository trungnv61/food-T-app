using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;

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
    }
}
