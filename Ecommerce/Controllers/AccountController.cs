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

        public bool VerifyLogin(string Email, string Password)
        {
            if(AccountManager.VerifyLogin(Email, Password))
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public ActionResult login(LoginViewModel dataModel)
        {
            //user verifiedUserData = AccountManager.VerifyLogin(dataModel);
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {   
            return View();
        }

        public bool VerifyEmail(string Email, string Password)
        {
            var existingUser = AccountManager.GetUserByEmail(Email);
            if(existingUser != null)
            {
                return false;
            }
            return true;
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

            return RedirectToAction("login", "account");
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