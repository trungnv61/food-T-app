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
    public class QuanLyDonHangController : Controller
    {
        // GET: Admin/QuanLyDonHang
     
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
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
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDao();
                long id = dao.Insert(order);
                if (id > 0)
                {
                    SetAlert("Thêm đơn hàng thành công", "success");
                    return RedirectToAction("Index", "QuanLyDonHang");

                }
                else
                {
                    ModelState.AddModelError("", "Thêm đơn hàng thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new OrderDao().Delete(id);
            SetAlert("Xóa đơn hàng thành công", "success");
            return RedirectToAction("Index", "QuanLyDonHang");
        }
        public ActionResult Detail(int id)
        {
            var order = new OrderDao().ViewDetail(id);
            return View(order);
        }


        // edit
        public ActionResult Edit(int id)
        {
            var user = new OrderDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDao();
                var id = dao.Update(order);
                SetAlert("Cập nhật đơn hàng thành công", "success");
                return RedirectToAction("Index", "QuanLyDonHang");
                //if (id > 0)
                //{


                //}
                //else
                //{
                //    ModelState.AddModelError("", "Them order thanh cong");
                //}
            }
            return View("Index");
        }

    }
}