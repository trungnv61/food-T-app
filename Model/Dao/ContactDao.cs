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

        public bool Update(Contact entity)
        {
            try
            {
                var contact = db.Contacts.Where(value => value.ContactId == entity.ContactId).SingleOrDefault();
                contact.Name = entity.Name;
                contact.Email = entity.Email;
                contact.Subject = contact.Subject;
                contact.Message = contact.Message;
                db.SaveChanges();
                return true;
            }
            catch (NullReferenceException e)
            {
                return false;
            }
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


        // delete
        public Contact ViewDetail(int id)
        {
            return db.Contacts.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var contact = db.Contacts.Find(id);
                db.Contacts.Remove(contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
