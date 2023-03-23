using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Ecommerce.Business;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Product_Type> AllProductList = CommonManager.GetAllProductTypes();
            return View(AllProductList);
        }

        public ActionResult About()
        {
            return View();
        }
        

       

        
    }
}