using System;
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
        // GET
        public ActionResult Create()
        {           
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                //if (id > 0)
                //{
                HttpPostedFileBase file = Request.Files["Picture"];
                if (file != null && file.FileName != "")
                {
                    string serverPath = HttpContext.Server.MapPath("~/Hinh");
                    string filePath = serverPath + "/" + file.FileName;
                    file.SaveAs(filePath);
                    product.ImageUrl = file.FileName;
                }
                var id = dao.Insert(product);
                return RedirectToAction("Index", "Home");

                //}
                //else
                //{
                //    ModelState.AddModelError("", "Them product thanh cong");
                //}
            }
            return View("Index");
        }
    }
}