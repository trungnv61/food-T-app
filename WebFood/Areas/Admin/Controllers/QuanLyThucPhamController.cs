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
    public class QuanLyThucPhamController : Controller
    {
        // GET: Admin/QuanLyThucPham
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(page, pageSize);
            FoodOnlineDbContext db = new FoodOnlineDbContext();
            ViewBag.Catalog = new SelectList(db.Categories.ToList(), "CategoryId", "Name", 0);
            return View(model);
        }


        //public ActionResult Index(int page = 1, int pageSize = 10)
        //{
        //    var dao = new UserDao();
        //    var model = dao.ListAllPaging(page, pageSize);
        //    return View(model);
        //}
        // GET
        public ActionResult Create()
        {           
            return View();
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
                return RedirectToAction("Index", "QuanLyThucPham");

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