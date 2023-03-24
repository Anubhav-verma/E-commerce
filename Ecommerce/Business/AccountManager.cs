using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Models;
using System.Security.Cryptography;
using System.Text;
using Ecommerce.ViewModels;
using System.Data.Entity;

namespace Ecommerce.Business
{
    public class AccountManager
    {
        public static void AddUser(RegistrationViewModel dataModel)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                GetEncryptedPassword(dataModel.Password);
                user newDataModel = new user();
                newDataModel.email = dataModel.Email;
                newDataModel.password = dataModel.Password;
                newDataModel.first_name = dataModel.FirstName;
                newDataModel.last_name = dataModel.LastName;
                newDataModel.phone = dataModel.PhoneNumber;
                newDataModel.created_at = DateTime.Now;
                newDataModel.updated_at = DateTime.Now;
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

        public static bool AuthenticateUserPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // convert the password to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                // compute the hash value of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                // convert the hash bytes to a base64 string
                string hashString = Convert.ToBase64String(hashBytes);
                // retrieve the stored hash string from the database
                string storedHashString = "storedHashString";
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
    }

    public class UserViewModelGrid : UsersViewModel
    {
        public UserViewModelGrid Usergrid { get; set; }
    }
}