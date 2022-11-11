using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebFood.Areas.Admin.Code;
using WebFood.Areas.Admin.Models;

namespace WebFood.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: Admin/DangNhap
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        public bool IsLocalUrl(string url)
        {
            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            var result = new AccountModel().Login(model.UserName, model.Password);
            if (result && ModelState.IsValid)
            {
                if (model.UserName == "Admin" && model.Password == "123456")
                {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                SessionHelper.SetSession(new UserSession() { UserName = model.UserName });
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }  else
                    {

                       return RedirectToAction("Index", "Home");
                    }
                } else
                {
                    ModelState.AddModelError("", "Đây không phải là tài khoản admin");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            return View(model);
        }
    }
}