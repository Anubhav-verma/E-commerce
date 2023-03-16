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

        public ActionResult Products()
        {
            try
            {
                List<product> ProductList = ProductManager.GetProductList();
                return View(ProductList);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        [HttpGet]
        public ActionResult AddEditProduct()
        {
            EcommerceDBEntities db = new EcommerceDBEntities();

            return View();
        }
        [HttpPost]
        public ActionResult AddEditProduct(product model, HttpPostedFileBase Image)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            if (Image != null && Image.ContentLength > 0)
            {
                byte[] fileData = new byte[Image.ContentLength];
                Image.InputStream.Read(fileData, 0, Image.ContentLength);
                ImageData ImageModel = new ImageData
                {
                    FileName = Image.FileName,
                    FileType = Image.ContentType,
                    FileContent = fileData
                };
                var temp = db.ImageDatas.Add(ImageModel);
                db.SaveChanges();

                model.image_url = temp.Id;
            }
            
            ProductManager.AddNewProduct(model);
            return View();
        }

        public ActionResult GetImage(int id)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            ImageData model = db.ImageDatas.Where(x => x.Id == id).FirstOrDefault();
            return File(model.FileContent, model.FileType);
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
    }
}