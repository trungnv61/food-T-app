using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Dao;

namespace WebFood.Areas.Admin.Controllers
{
    public class QuanLyLienHeController : Controller
    {
        // GET: Admin/QuanLyLienHe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContactDao();
                long id = dao.Insert(contact);
                if (id > 0)
                {

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Them contact thanh cong");
                }
            }
            return View("Index");
        }
    }
}