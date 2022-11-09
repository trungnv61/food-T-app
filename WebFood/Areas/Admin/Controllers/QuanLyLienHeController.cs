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
    public class QuanLyLienHeController : Controller
    {
        // GET: Admin/QuanLyLienHe
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContactDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
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

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContactDao();
                long id = dao.Insert(contact);
                if (id > 0)
                {
                    SetAlert("Thêm liên hệ thành cong", "success");
                    return RedirectToAction("Index", "QuanLyLienHe");

                }
                else
                {
                    ModelState.AddModelError("", "Them contact thanh cong");
                }
            }
            return View("Index");
        }


        // delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContactDao().Delete(id);
            SetAlert("Xóa liên hệ thành cong", "success");
            return RedirectToAction("Index", "QuanLyLienHe");
        }
        public ActionResult Detail(int id)
        {
            var contact = new ContactDao().ViewDetail(id);
            return View(contact);
        }



        //edit
        public ActionResult Edit(int id)
        {
            var contact = new ContactDao().ViewDetail(id);
            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContactDao();
                var id = dao.Update(contact);
                SetAlert("Update liên hệ thành cong", "success");
                return RedirectToAction("Index", "QuanLyLienHe");
                //if (id > 0)
                //{


                //}
                //else
                //{
                //    ModelState.AddModelError("", "Them contact thanh cong");
                //}
            }
            return View("Index");
        }
    }
}