using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;



namespace WebFood.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var Category = new CategoryDao();
            ViewBag.NewProduct = Category.ListNewProduct(4);
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var dao = new ProductCategoryDao();
            var model = dao.ListAll();
            return PartialView(model);
        }
    }
}