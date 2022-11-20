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
using OfficeOpenXml;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using System.Data;
using ClosedXML.Excel;

namespace WebFood.Areas.Admin.Controllers
{
    public class ThongKeTaiKhoanController : Controller
    {
        FoodOnlineDbContext db = new FoodOnlineDbContext();
        // GET: Admin/ThongKeTaiKhoan
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
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
                return File(stream.ToArray(), "application/pdf", "ThongKeTaiKhoan.pdf");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public EmptyResult ExportWord(string GridHtml)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ThongKeTaiKhoan.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";
            Response.Output.Write(GridHtml);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }



        //public void ExportExcel(string searchString, int page = 1, int pageSize = 10)
        //{
        //    var dao = new UserDao();
        //    var AccList = dao.ListAllPaging(searchString, page, pageSize);

        //    ExcelPackage pck = new ExcelPackage();
        //    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

        //    ws.Cells["A6"].Value = "UserId";
        //    ws.Cells["B6"].Value = "Name";
        //    ws.Cells["C6"].Value = "UserName";
        //    ws.Cells["D6"].Value = "Mobile";
        //    ws.Cells["E6"].Value = "Email";
        //    ws.Cells["F6"].Value = "Address";
        //    ws.Cells["G6"].Value = "PostCode";
        //    ws.Cells["H6"].Value = "ImageUrl";
        //    ws.Cells["I6"].Value = "CreatedDate";

        //    int rowStart = 7;
        //    foreach (var item in AccList)
        //    {
        //        ws.Cells[string.Format("A{0}", rowStart)].Value = item.UserId;
        //        ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
        //        ws.Cells[string.Format("C{0}", rowStart)].Value = item.UserName;
        //        ws.Cells[string.Format("D{0}", rowStart)].Value = item.Mobile;
        //        ws.Cells[string.Format("E{0}", rowStart)].Value = item.Email;
        //        ws.Cells[string.Format("F{0}", rowStart)].Value = item.Address;
        //        ws.Cells[string.Format("G{0}", rowStart)].Value = item.PostCode;
        //        ws.Cells[string.Format("H{0}", rowStart)].Value = item.ImageUrl;
        //        ws.Cells[string.Format("I{0}", rowStart)].Value = item.CreatedDate;
        //        rowStart++;
        //    }

        //    ws.Cells["A:AZ"].AutoFitColumns();
        //    Response.Clear();
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment: filename=" + "ThongKeTaiKhoan.xlsx");
        //    Response.BinaryWrite(pck.GetAsByteArray());
        //    Response.End();
        //}


        [HttpPost]
        public FileResult ExportExcel()
        {
   
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn("UserId"),
                                            new DataColumn("Name"),
                                            new DataColumn("UserName"),
                                            new DataColumn("Mobile"),
                                            new DataColumn("Email"),
                                            new DataColumn("Address"),
                                            new DataColumn("PostCode"),
                                            new DataColumn("Password"),
                                            new DataColumn("ImageUrl"),
                                            new DataColumn("CreatedDate")
            });
               

            var users = from user in db.Users.Where(x => x.UserId >= 3 && x.UserId <= 12).OrderByDescending(x => x.UserId)
                        select user;

            foreach (var user in users)
            {
                dt.Rows.Add(user.UserId, user.Name, user.UserName, user.Mobile, user.Email,
                            user.Address, user.PostCode, user.Password, user.ImageUrl, user.CreatedDate);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ThongKeTaiKhoan.xlsx");
                }
            }
        }


    }
}