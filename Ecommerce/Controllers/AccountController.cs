using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Ecommerce.Business;
using Ecommerce.ViewModels;

namespace Ecommerce.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegistrationViewModel model)
        {
            try
            {
                AccountManager.AddUser(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return View();
        }
        [HttpGet]
        public ActionResult EditUserType()
        {
            List<UsersViewModel> userList = AccountManager.GetUserGrid();
            return View(userList);
        }
        [HttpPost]
        public ActionResult EditUserType(int UserId, int UserType)
        {
            AccountManager.EditUser(UserId, UserType);
            return RedirectToAction("EditUserType", "Account");
        }
    }
}