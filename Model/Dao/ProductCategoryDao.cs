using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;

namespace Model.Dao
{
   public class ProductCategoryDao
    {
        FoodOnlineDbContext db = null;
        public ProductCategoryDao()
        {
            db = new FoodOnlineDbContext();
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.IsActive == true).OrderBy(x => x.CategoryId).ToList();
        }
    }
}
