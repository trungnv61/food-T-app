using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Dao;
using PagedList;

namespace WebFood.Areas.Admin.Controllers
{
    public class QuanLyLienHeController : Controller
    {
        // GET: Admin/QuanLyLienHe
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContactDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContactDao();
                long id = dao.Insert(contact);
                if (id > 0)
                {

                    return RedirectToAction("Index", "QuanLyLienHe");

                }
                else
                {
                    ModelState.AddModelError("", "Them contact thanh cong");
                }
            }
            return View("Index");
        }


        // delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContactDao().Delete(id);
            return RedirectToAction("Index", "QuanLyLienHe");
        }
        public ActionResult Detail(int id)
        {
            var contact = new ContactDao().ViewDetail(id);
            return View(contact);
        }
    }
}