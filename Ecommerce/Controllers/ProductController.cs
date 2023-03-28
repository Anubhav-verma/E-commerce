using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Ecommerce.Business;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products(int productTypeId=0)
        {
            int categoryId = 0;
            try
            {
                List<product> ProductList = ProductManager.GetProductList();
                if (productTypeId > 0)
                {
                    categoryId = CommonManager.GetCategoryDetails(productTypeId).CategoryID;
                    if (categoryId > 0) { ProductList = ProductList.Where(x => x.category_id == categoryId).ToList(); }
                }
                return View(ProductList);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult AddEditProduct(int ProductID=0)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            product dbModel = new product();
            if (ProductID > 0)
            {
                dbModel = ProductManager.GetProductbyID(ProductID);
            }
            return View(dbModel);
        }
        [HttpPost]
        public ActionResult AddEditProduct(product model, HttpPostedFileBase Image)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            product newModel;
            if (model.product_id == 0)
            {
                model.image_url = CommonManager.GetImageId(Image);//--here image_url refers to image id
                newModel = ProductManager.AddNewProduct(model);
            }
            else
            {
                newModel = ProductManager.EditProduct(model);
            }
            int? productTypeID = db.Categories.Find(newModel.category_id).Product_type_id;
            return RedirectToAction("Products", "Product", new { productTypeId = productTypeID });
        }

        public ActionResult GetImage(int id)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                if (id > 0)
                {
                    ImageData model = db.ImageDatas.Where(x => x.Id == id).FirstOrDefault();
                    return File(model.FileContent, model.FileType);
                }
                return new EmptyResult();
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

        [HttpGet]
        public ActionResult AddEditCategories()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEditCategories(Category model)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            Category newModel = new Category();
            
            newModel.CategoryName = model.CategoryName;
            newModel.Description = model.Description;
            db.Categories.Add(newModel);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult AddeditProductType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddeditProductType(Product_Type model, HttpPostedFileBase Image)
        {
            model.image_id = CommonManager.GetImageId(Image);
            ProductManager.AddProductType(model);
            return View();
        }
        
        public ActionResult DeleteProduct(int ProductID)
        {
            ProductManager.DeleteProduct(ProductID);
            return RedirectToAction("Products", "Product");
        }

        public ActionResult ProductDetails(int ProductID)
        {
            return View();
        }
}