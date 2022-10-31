using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;

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
    }
}
