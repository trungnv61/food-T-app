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


        public ActionResult ExportPDF() {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Hinh/ThongKeTaiKhoan.pdf")
            };

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
       

        public void ExportExcel(string searchString, int page = 1, int pageSize = 10)
        {

          
            //List<User> AccList = (List<User>)db.Users.Select(x => new User
            //{
            //    UserId = x.UserId,
            //    Name = x.Name,
            //    UserName = x.UserName,
            //    Mobile = x.Mobile,
            //    Email = x.Email,
            //    Address = x.Address,
            //    PostCode = x.PostCode,
            //    ImageUrl = x.ImageUrl,
            //    CreatedDate = x.CreatedDate
            //});


            //IQueryable<User> AccList = db.Users;

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A6"].Value = "UserId";
            ws.Cells["B6"].Value = "Name";
            ws.Cells["C6"].Value = "UserName";
            ws.Cells["D6"].Value = "Mobile";
            ws.Cells["E6"].Value = "Email";
            ws.Cells["F6"].Value = "Address";
            ws.Cells["G6"].Value = "PostCode";
            ws.Cells["H6"].Value = "ImageUrl";
            ws.Cells["I6"].Value = "CreatedDate";

            int rowStart = 7;
            foreach (var item in AccList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.UserId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.UserName;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Mobile;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Address;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.PostCode;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.ImageUrl;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.CreatedDate;
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