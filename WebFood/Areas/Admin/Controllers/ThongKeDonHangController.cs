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
    public class ThongKeDonHangController : Controller
    {
        // GET: Admin/ThongKeDonHang
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Hinh/ThongKeDonHang.pdf")
            };

        }

        public void ExportExcel(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
            var AccList = dao.ListAllPaging(searchString, page, pageSize);

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A6"].Value = "OrderDetailsId ";
            ws.Cells["B6"].Value = "OrderNo";
            ws.Cells["C6"].Value = "ProductId ";
            ws.Cells["D6"].Value = "Quantity";
            ws.Cells["E6"].Value = "UserId";
            ws.Cells["F6"].Value = "Status";
            ws.Cells["G6"].Value = "PaymentId";
            ws.Cells["H6"].Value = "OrderDate";

            int rowStart = 7;
            foreach (var item in AccList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.OrderDetailsId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.OrderNo;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.ProductId;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Quantity;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.UserId;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Status;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.PaymentId;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.OrderDate;
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