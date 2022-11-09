using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Dao;


namespace WebFood.Areas.Admin.Controllers
{
    public class QuanLyHangHoaController : Controller
    {
        // GET: Admin/QuanLyHangHoa
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString ,page, pageSize);
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

        // GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                //if (id > 0)
                //{
                HttpPostedFileBase file = Request.Files["Picture"];
                if (file != null && file.FileName != "")
                {
                    string serverPath = HttpContext.Server.MapPath("~/Hinh");
                    string filePath = serverPath + "/" + file.FileName;
                    file.SaveAs(filePath);
                    category.ImageUrl = file.FileName;
                }
                var id = dao.Insert(category);
                SetAlert("Thêm hàng hóa thành công", "success");
                return RedirectToAction("Index", "QuanLyHangHoa");

                //}
                //else
                //{
                //    ModelState.AddModelError("", "Them product thanh cong");
                //}
            }
            return View("Index");
        }

        // xoa
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
            SetAlert("Xóa hàng hóa thành công", "success");
            return RedirectToAction("Index", "QuanLyHangHoa");
        }
        public ActionResult Detail(int id)
        {
            var category = new CategoryDao().ViewDetail(id);
            return View(category);
        }

        // cap nhat
        public ActionResult Edit(int id)
        {
            var category = new CategoryDao().ViewDetail(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                //if (id > 0)
                //{
                HttpPostedFileBase file = Request.Files["Picture"];
                if (file != null && file.FileName != "")
                {
                    string serverPath = HttpContext.Server.MapPath("~/Hinh");
                    string filePath = serverPath + "/" + file.FileName;
                    file.SaveAs(filePath);
                    category.ImageUrl = file.FileName;
                }
                var id = dao.Update(category);
                SetAlert("Cập nhật hàng hóa thành công", "success");
                return RedirectToAction("Index", "QuanLyHangHoa");

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