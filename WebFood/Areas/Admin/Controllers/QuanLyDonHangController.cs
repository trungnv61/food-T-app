using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Dao;

namespace WebFood.Areas.Admin.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        // GET: Admin/QuanLyDonHang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDao();
                long id = dao.Insert(order);
                if (id > 0)
                {

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Them category thanh cong");
                }
            }
            return View("Index");
        }
    }
}