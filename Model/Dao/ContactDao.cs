using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Framework;
using PagedList;

namespace Model.Dao
{
    public class ContactDao
    {
        FoodOnlineDbContext db = null;
        public ContactDao()
        {
            db = new FoodOnlineDbContext();
        }

        public long Insert(Contact entity)
        {
            db.Contacts.Add(entity);
            db.SaveChanges();
            return entity.ContactId;
        }

        public IEnumerable<Contact> ListAllPaging(int page, int pageSize)
        {
            return db.Contacts.OrderByDescending(x => x.ContactId).ToPagedList(page, pageSize);
        }
    }
}
