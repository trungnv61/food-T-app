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