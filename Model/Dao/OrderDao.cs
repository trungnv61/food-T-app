using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;
using PagedList;

namespace Model.Dao
{
    public class OrderDao
    {
        FoodOnlineDbContext db = null;
        public OrderDao()
        {
            db = new FoodOnlineDbContext();
        }

        public long Insert(Order entity)
        {
            db.Orders.Add(entity);
            db.SaveChanges();
            return entity.OrderDetailsId;
        }

        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.OrderNo.Contains(searchString) || x.Status.Contains(searchString));
            }
            return model.OrderByDescending(x => x.OrderDetailsId).ToPagedList(page, pageSize);
        }
    }
}
