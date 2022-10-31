using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Dao;

namespace WebFood.Areas.Admin.Controllers
{
    public class QuanLyTaiKhoanController : Controller
    {
        // GET: Admin/QuanLyTaiKhoan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
            var dao = new UserDao();
            long id = dao.Insert(user);
            if (id > 0)
            {
                return RedirectToAction("Index", "Home");
            } else
            {
                ModelState.AddModelError("", "Them user thanh cong");
            }
            }
        return View("Index");
        }
    }
}