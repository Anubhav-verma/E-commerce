using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ecommerce.ViewModels;
using Newtonsoft.Json;
using Ecommerce.Business;



namespace Ecommerce.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public async Task<JsonResult> GetCountriesJson()
        {
            var countries = await CommonManager.GetCountriesAsync();
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        
        

    }
}