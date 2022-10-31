using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;

namespace Model.Dao
{
   public class ProductDao
    {
        FoodOnlineDbContext db = null;
        public ProductDao()
        {
            db = new FoodOnlineDbContext();
        }

        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ProductId;
        }
    }
}
