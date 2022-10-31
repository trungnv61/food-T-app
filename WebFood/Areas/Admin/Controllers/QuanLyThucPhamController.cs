﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Dao;

namespace WebFood.Areas.Admin.Controllers
{
    public class QuanLyThucPhamController : Controller
    {
        // GET: Admin/QuanLyThucPham
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                long id = dao.Insert(product);
                if (id > 0)
                {

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Them product thanh cong");
                }
            }
            return View("Index");
        }
    }
}