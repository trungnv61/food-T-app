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

        // them
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.UserId;
        }

        // hien thi
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.UserId).ToPagedList(page, pageSize);
        }




        public long Insert2(User entity)
        {
            var user = db.Users.Find(entity.UserId);
            user = db.Users.Add(entity);
            db.SaveChanges();
            return entity.UserId;
        }



        // cap nhat
        //public bool Update(User entity)
        //{
        //    try
        //    {
        //        var user = db.Users.Find(entity.UserId);
        //        user.Name = entity.Name;
        //        user.ImageUrl = entity.ImageUrl;
        //        user.Address = user.Address;
        //        user.Mobile = user.Mobile;
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (NullReferenceException e)
        //    {
        //        return false;
        //    }
        //}

      

        // delete
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
            var user =  db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
