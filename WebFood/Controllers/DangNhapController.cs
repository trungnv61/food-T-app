using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFood.Areas.Admin.Code;
using WebFood.Areas.Admin.Models;

namespace WebFood.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model, string Url)
        {
            var result = new AccountModel().Login(model.UserName, model.Password);
            if (result && ModelState.IsValid)
            {
                if (model.UserName == "Admin" && model.Password == "123456")
                {
                    SessionHelper.SetSession(new UserSession() { UserName = model.UserName });
                    return RedirectToAction("Index", "./Admin/Home/Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không đúng");
            }
            return View(model);
        }
    }
}