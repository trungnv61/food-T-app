using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Model.Dao;
using PagedList;


namespace WebFood.Areas.Admin.Controllers
{
    public class ThongKeHangHoaController : Controller
    {
        // GET: Admin/ThongKeHangHoa
        [Authorize(Users = "Admin", Roles = "admin")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }


        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Hinh/ThongKeHangHoa.pdf")
            };

        }
    }
}