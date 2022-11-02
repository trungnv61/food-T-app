﻿using System;
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
        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderByDescending(x => x.UserId).ToPagedList(page, pageSize);
        }


   
        // cap nhat
        public bool Update(User entity)
        {
            try
            {
            var user = db.Users.Find(entity.UserId);
            user.Name = entity.Name;
            user.ImageUrl = entity.ImageUrl;
            user.Address = user.Address;
            user.Mobile = user.Mobile;
            db.SaveChanges();
            return true;
            } 
            catch (Exception EX)
            {
                return false;
            }
        }
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