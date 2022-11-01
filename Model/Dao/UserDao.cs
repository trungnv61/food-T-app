using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;
using PagedList;

namespace Model.Dao
{
   public class UserDao
    {
        FoodOnlineDbContext db = null;
        public UserDao()
        {
            db = new FoodOnlineDbContext();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.UserId;
        }

        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderByDescending(x => x.UserId).ToPagedList(page, pageSize);
        }
    }
}
