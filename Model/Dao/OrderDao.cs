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

        public IEnumerable<Order> ListAllPaging(int page, int pageSize)
        {
            return db.Orders.OrderByDescending(x => x.OrderDetailsId).ToPagedList(page, pageSize);
        }
    }
}
