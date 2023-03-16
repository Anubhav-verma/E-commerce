﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;


namespace Ecommerce.Business
{
    public class ProductManager
    {
        public static void AddNewProduct(product model)
        {
            try
            {
                EcommerceDBEntities db = new EcommerceDBEntities();
                product newModel = new product();
                newModel.name = model.name;
                newModel.description = model.description;
                newModel.image_url = model.image_url;
                newModel.price = model.price;
                newModel.quantity = model.quantity;
                newModel.category_id = model.category_id;
                newModel.created_at = DateTime.Now;
                newModel.updated_at = DateTime.Now;
                db.products.Add(newModel);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
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