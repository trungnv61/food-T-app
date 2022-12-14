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

    public class QuanLyTaiKhoanController : Controller
    {
      

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

        //[Authorize(Users = "Admin", Roles = "admin")]
        public ActionResult Index(string searchString ,int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        // GET
    
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //if (id > 0)
                //{
                HttpPostedFileBase file = Request.Files["Picture"];
                if (file != null && file.FileName != "")
                {
                    string serverPath = HttpContext.Server.MapPath("~/Hinh");
                    string filePath = serverPath + "/" + file.FileName;
                    file.SaveAs(filePath);
                    user.ImageUrl = file.FileName;
                }
                var id = dao.Insert(user);
                SetAlert("Thêm người dùng thành công", "success");
                return RedirectToAction("Index", "QuanLyTaiKhoan");

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
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //if (id > 0)
                //{
                HttpPostedFileBase file = Request.Files["Picture"];
                if (file != null && file.FileName != "")
                {
                    string serverPath = HttpContext.Server.MapPath("~/Hinh");
                    string filePath = serverPath + "/" + file.FileName;
                    file.SaveAs(filePath);
                    user.ImageUrl = file.FileName;
                }
                var result = dao.Update(user);
                SetAlert("Cập nhật người dùng thành công", "success");
                return RedirectToAction("Index", "QuanLyTaiKhoan");
            }
            return View("Index");
        }




        // delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            SetAlert("Xóa người dùng thành công", "success");
            return RedirectToAction("Index", "QuanLyTaiKhoan");
        }

 
        public ActionResult Detail(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        // xem chi tiet
        [HttpGet]
        public ActionResult Detail(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
    }
}