using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFood.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult QuanLyTaiKhoan()
        {
            return View();
        }

        public ActionResult QuanLyDonHang()
        {
            return View();
        }

        public ActionResult QuanLyThucPham()
        {
            return View();
        }

        public ActionResult DangKy()
        {
            return View();
        }
    }
}