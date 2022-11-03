using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;
using PagedList;

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


  




        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ProductId).ToPagedList(page, pageSize);
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }

        // delete
        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Product entity)
        {
           try
            {
                var product = db.Products.Find(entity.ProductId);
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Quantity = entity.Quantity;
                db.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
     
        }
    }
}
