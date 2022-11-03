﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Rotativa;
using PagedList;


namespace WebFood.Areas.Admin.Controllers
{
    public class ThongKeDonHangController : Controller
    {
        // GET: Admin/ThongKeDonHang
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Hinh/ThongKeDonHang.pdf")
            };

        }
    }
}