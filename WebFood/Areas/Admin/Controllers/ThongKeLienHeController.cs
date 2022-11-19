using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Rotativa;
using PagedList;
using OfficeOpenXml;

namespace WebFood.Areas.Admin.Controllers
{
    public class ThongKeLienHeController : Controller
    {
        // GET: Admin/ThongKeLienHe
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContactDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Hinh/ThongKeLienHe.pdf")
            };

        }

        public void ExportExcel(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContactDao();
            var AccList = dao.ListAllPaging(searchString, page, pageSize);

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A6"].Value = "ContactId";
            ws.Cells["B6"].Value = "Name";
            ws.Cells["C6"].Value = "Email";
            ws.Cells["D6"].Value = "Subject";
            ws.Cells["E6"].Value = "Message";
            ws.Cells["F6"].Value = "CreatedDate";

            int rowStart = 7;
            foreach (var item in AccList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ContactId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Subject;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Message;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.CreatedDate;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ThongKeTaiKhoan.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
    }
}