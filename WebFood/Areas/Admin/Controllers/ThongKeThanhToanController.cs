using System;
using System.Collections.Generic;
using System.Linq;
using Rotativa;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using OfficeOpenXml;

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

        public void ExportExcel(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new PaymentDao();
            var AccList = dao.ListAllPaging(searchString, page, pageSize);

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A6"].Value = "PaymentId";
            ws.Cells["B6"].Value = "Name";
            ws.Cells["C6"].Value = "CardNo";
            ws.Cells["D6"].Value = "ExpiryDate";
            ws.Cells["E6"].Value = "CvvNo";
            ws.Cells["F6"].Value = "Address";
            ws.Cells["G6"].Value = "PaymentMode ";
  

            int rowStart = 7;
            foreach (var item in AccList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.PaymentId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.CardNo;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.ExpiryDate;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.CvvNo;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Address;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.PaymentMode;
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