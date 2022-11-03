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
        // GET: Admin/QuanLyTaiKhoan
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult Create(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //    var dao = new UserDao();
        //    long id = dao.Insert(user);
        //    if (id > 0)
        //    {

        //        return RedirectToAction("Index", "Home");

        //    } else
        //    {
        //        ModelState.AddModelError("", "Them user thanh cong");
        //    }
        //    }
        //return View("Index");
        //}



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
                return RedirectToAction("Index", "QuanLyTaiKhoan");
            }
            return View("Index");
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
                return RedirectToAction("Index", "QuanLyTaiKhoan");

                //}
                //else
                //{
                //    ModelState.AddModelError("", "Them product thanh cong");
                //}
            }
            return View("Index");
        }


        // delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index", "QuanLyTaiKhoan");
        }

        // xem chi tiet
        [HttpGet]
        public ActionResult Detail(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
    }
}