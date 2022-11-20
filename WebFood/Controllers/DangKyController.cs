using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;


namespace WebFood.Controllers
{
    public class DangKyController : Controller
    {
        // GET: DangKy
        FoodOnlineDbContext _db = new FoodOnlineDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email && s.UserName == s.UserName);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Home");
                }
                else 
                {
                    ViewBag.error = "Email already exists";
                    ViewBag.errorN = "UserName already exists";
                    return View();
                }


            }
            return View();


        }
    }
}