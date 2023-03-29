using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;


namespace Ecommerce.Business
{
    public class ProductManager
    {
        public static product AddNewProduct(product model)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            product newModel = new product();
            try
            {
                newModel.name = model.name;
                newModel.description = model.description;
                newModel.image_url = model.image_url;
                newModel.price = model.price;
                newModel.quantity = model.quantity;
                newModel.category_id = model.category_id;
                newModel.created_at = DateTime.Now;
                newModel.updated_at = DateTime.Now;
                newModel = db.products.Add(newModel);
                db.SaveChanges();
                return newModel;
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

        public static product EditProduct(product model)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            product newModel = GetProductbyID(model.product_id);
            try
            {
                newModel.name = model.name;
                newModel.Material = model.Material;
                newModel.description = model.description;
                newModel.image_url = model.image_url;
                newModel.price = model.price;
                newModel.quantity = model.quantity;
                newModel.category_id = model.category_id;
                newModel.created_at = model.created_at;
                newModel.updated_at = DateTime.Now;
                db.Entry(newModel).State = EntityState.Modified;
                db.SaveChanges();
                return newModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        public static SelectList GetCategoryList()
        {
            try
            {
                EcommerceDBEntities db = new EcommerceDBEntities();
                var categoryList = db.Categories.ToList();
                return new SelectList(categoryList, "CategoryID", "CategoryName");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        } 

        public static List<product> GetProductList()
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                return db.products.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void AddProductType(Product_Type dataModel)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                Product_Type newDataModel = new Product_Type();
                newDataModel.product_type_name = dataModel.product_type_name;
                newDataModel.description = dataModel.description;
                newDataModel.image_id = dataModel.image_id;
                db.Product_Type.Add(newDataModel);
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

        public static void DeleteProduct(int ProductId)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                db.products.Remove(db.products.Where(x=>x.product_id == ProductId).FirstOrDefault());
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static product GetProductbyID(int ProductID)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                return db.products.Where(x => x.product_id == ProductID).FirstOrDefault();
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

    //public class productList
    //{
    //    public Int64 ProductId { get; set; }
    //    public string ProductName { get; set;}
    //    public string ImageUrl { get; set; }
    //    public Int64 ProductPrice { get; set; }
    //    public Int32 Quantity { get; set; }
    //}
    
}