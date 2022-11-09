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
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString ,page, pageSize);
            FoodOnlineDbContext db = new FoodOnlineDbContext();
            ViewBag.Catalog = new SelectList(db.Categories.ToList(), "CategoryId", "Name", 0);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-error";
            }
        }


        //public ActionResult Index(int page = 1, int pageSize = 10)
        //{
        //    var dao = new UserDao();
        //    var model = dao.ListAllPaging(page, pageSize);
        //    return View(model);
        //}

        // GET QuanLyThucPham/Create
        public ActionResult Create()
        {
            FoodOnlineDbContext db = new FoodOnlineDbContext();
            ViewBag.Catalog = new SelectList(db.Categories.ToList(), "CategoryId", "Name", 0);
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
                SetAlert("Add thực phẩm thành công", "success");
                return RedirectToAction("Index", "QuanLyThucPham");

                //}
                //else
                //{
                //    ModelState.AddModelError("", "Them product thanh cong");
                //}
            }
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            FoodOnlineDbContext db = new FoodOnlineDbContext();
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Catalog = new SelectList(db.Categories.ToList(), "CategoryId", "Name", 0);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
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
                var result = dao.Update(product);
                if (result)
                {
                    SetAlert("Update thực phẩm thành công", "success");
                    return RedirectToAction("Index", "QuanLyThucPham");
                }

                //}
                //else
                //{
                //    ModelState.AddModelError("", "Them product thanh cong");
                //}
            }
            return View("Index");
        }

        // xoa
        // delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            SetAlert("Delete thực phẩm thành công", "success");
            return RedirectToAction("Index", "QuanLyThucPham");
        }
        public ActionResult Detail(int id)
        {
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }

    }
}