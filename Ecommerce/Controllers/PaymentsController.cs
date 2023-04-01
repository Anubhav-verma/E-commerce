using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class PaymentsController : Controller
    {
        // GET: Payments
        public ActionResult Index()
        {
            return View();
        }
    }
}