using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;
using PagedList;

namespace Model.Dao
{
   public class PaymentDao
    {
        FoodOnlineDbContext db = null;

        public PaymentDao()
        {
            db = new FoodOnlineDbContext();
        }


        public long Insert(Payment entity)
        {
            db.Payments.Add(entity);
            db.SaveChanges();
            return entity.PaymentId;
        }

        public IEnumerable<Payment> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Payment> model = db.Payments;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Address.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Name).ToPagedList(page, pageSize);
        }
    }
}
