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

        public IEnumerable<Contact> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Contact> model = db.Contacts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Subject.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ContactId).ToPagedList(page, pageSize);
        }
    }
}
