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
    public class ThongKeThucPhamController : Controller
    {
        // GET: Admin/ThongKeThucPham
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Hinh/ThongKeThucPham.pdf")
            };

        }

        public void ExportExcel(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var AccList = dao.ListAllPaging(searchString, page, pageSize);

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A6"].Value = "ProductId";
            ws.Cells["B6"].Value = "Name";
            ws.Cells["C6"].Value = "Description";
            ws.Cells["D6"].Value = "Price";
            ws.Cells["E6"].Value = "Quantity";
            ws.Cells["F6"].Value = "ImageUrl";
            ws.Cells["G6"].Value = "CategoryId";
            ws.Cells["H6"].Value = "IsActive";
            ws.Cells["I6"].Value = "CreatedDate";

            int rowStart = 7;
            foreach (var item in AccList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ProductId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Description;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Price;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Quantity;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.ImageUrl;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.CategoryId;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.IsActive;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.CreatedDate;
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