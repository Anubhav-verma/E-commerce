using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult AddEditProduct()
        {
            EcommerceDBEntities db = new EcommerceDBEntities();

            return View();
        }
        [HttpGet]
        public ActionResult AddEditCategories()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEditCategories(Categories model)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            Category newModel = new Category();
            
            newModel.CategoryName = model.CategoryName;
            newModel.Description = model.Description;
            db.Categories.Add(newModel);
            db.SaveChanges();
            return View();
        }
    }
}