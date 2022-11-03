using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Model.Framework;
using Model.Dao;
using PagedList;
using WebFood.Areas.Admin.Models;

namespace WebFood.Areas.Admin.Controllers
{
    public class ThongKeTaiKhoanController : Controller
    {
        // GET: Admin/ThongKeTaiKhoan
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }


        public ActionResult ExportPDF() {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Hinh/ThongKeTaiKhoan.pdf")
            };

        }

       public void ExportExcel()
        {

        }


    }
}