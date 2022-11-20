using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Rotativa;
using PagedList;
using OfficeOpenXml;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using System.Data;
using ClosedXML.Excel;
using Model.Framework;

namespace WebFood.Areas.Admin.Controllers
{
    public class ThongKeDonHangController : Controller
    {
        FoodOnlineDbContext db = new FoodOnlineDbContext();
        // GET: Admin/ThongKeDonHang
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult ExportPDF(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "ThongKeDonHang.pdf");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public EmptyResult ExportWord(string GridHtml)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ThongKeDonHang.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";
            Response.Output.Write(GridHtml);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }


        [HttpPost]
        public FileResult ExportExcel()
        {

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("OrderDetailsId"),
                                            new DataColumn("OrderNo"),
                                            new DataColumn("ProductId"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("UserId"),
                                            new DataColumn("Status"),
                                            new DataColumn("PaymentId"),
                                            new DataColumn("OrderDate"),
            });


            var orders = from order in db.Orders.OrderByDescending(x => x.OrderDetailsId)
                        select order;

            foreach (var order in orders)
            {
                dt.Rows.Add(order.OrderDetailsId, order.OrderNo, order.ProductId, order.Quantity, order.UserId,
                            order.Status, order.PaymentId, order.OrderDate);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ThongKeDonHang.xlsx");
                }
            }
        }

    }
}