using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Models;
using System.Security.Cryptography;
using System.Text;
using Ecommerce.ViewModels;
using System.Data.Entity;
using Ecommerce.Enums;

namespace Ecommerce.Business
{
    public class AccountManager
    {
        public static void AddUser(RegistrationViewModel dataModel)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                string encryptedPassword = GetEncryptedPassword(dataModel.Password);
                user newDataModel = new user();
                newDataModel.email = dataModel.Email;
                newDataModel.password = encryptedPassword;
                newDataModel.first_name = dataModel.FirstName;
                newDataModel.last_name = dataModel.LastName;
                newDataModel.phone = dataModel.PhoneNumber;
                newDataModel.created_at = DateTime.Now;
                newDataModel.updated_at = DateTime.Now;
                newDataModel.user_type = (int)CommonEnums.UserType.Customer;
                db.users.Add(newDataModel);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void EditUser(int UserId, int UserType)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                user dbModel = db.users.Find(UserId);
                dbModel.user_type = UserType;
                dbModel.updated_at = DateTime.Now;
                db.Entry(dbModel).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        public static string GetEncryptedPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // convert the password to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                // compute the hash value of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                // convert the hash bytes to a base64 string
                string hashString = Convert.ToBase64String(hashBytes);
                // store the hash string in the database
                return hashString;
            }
        }

        public static bool AuthenticateUserPassword(string storedPassword, string password)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            using (SHA256 sha256 = SHA256.Create())
            {
                // convert the password to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                // compute the hash value of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                // convert the hash bytes to a base64 string
                string hashString = Convert.ToBase64String(hashBytes);
                // retrieve the stored hash string from the database
                string storedHashString = storedPassword;
                // compare the hash string with the stored hash string
                if (hashString == storedHashString)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public static List<UsersViewModel> GetUserGrid()
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                
                var userList = (from users in db.users
                                select new UsersViewModel { UserID = users.customer_id, FirstName = users.first_name, LastName = users.last_name, UserType = users.user_type });
                return userList.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }
        public static user GetUserByEmail(string email)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                return db.users.Where(x => x.email == email).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        public static bool VerifyLogin(string Email, string Password)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            user actualUser;
            try
            {
                actualUser = db.users.Where(x => x.email == Email).FirstOrDefault();
                
                if (AuthenticateUserPassword(actualUser.password, Password))
                {
                    SetSessionVariables(actualUser);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        public static void SetSessionVariables(user userModel)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            HttpContext.Current.Session["UserID"] = userModel.customer_id;
            HttpContext.Current.Session["UserType"] = userModel.user_type;
            HttpContext.Current.Session["UserName"] = userModel.first_name;
        }
    }

    public class UserViewModelGrid : UsersViewModel
    {
        public UserViewModelGrid Usergrid { get; set; }
    }
}