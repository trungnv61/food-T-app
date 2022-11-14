using System;
using System.Collections.Generic;
using System.Linq;
using Rotativa;
using System.Web;
using System.Web.Mvc;
using Model.Dao;

namespace WebFood.Areas.Admin.Controllers
{
    public class ThongKeThanhToanController : Controller
    {
        // GET: Admin/ThongKeThanhToan
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new PaymentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Hinh/ThongKeThanhToan.pdf")
            };

        }

        public void ExportExcel()
        {

        }
    }
}