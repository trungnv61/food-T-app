using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace WebFood.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        //[Authorize(Users = "Admin", Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        // Write Logic Here


    }
}