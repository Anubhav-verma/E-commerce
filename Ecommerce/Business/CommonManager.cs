using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ecommerce.ViewModels;
using Newtonsoft.Json;
using Ecommerce.ViewModels;
using System.Globalization;
using Ecommerce.Models;

namespace Ecommerce.Business
{
    public class CommonManager
    {

        public static async Task<List<string>> GetCountriesAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://restcountries.com/v3.1/");
                var response = await client.GetAsync("all");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var countries = JsonConvert.DeserializeObject<List<string>>(json);
                    return countries;
                }

                return null;
            }
        }

        public static List<Countries> GetCountryList()
        {
            try
            {
                var countries = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new RegionInfo(c.Name))
                .GroupBy(r => r.TwoLetterISORegionName)
                .Select(g => new Countries
                {
                    Id = g.Key,
                    Name = g.First().DisplayName
                })
                .OrderBy(c => c.Name)
                .ToList();

                return countries;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Int32 GetImageId(HttpPostedFileBase Image)
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
                var TempImage = db.ImageDatas.Add(ImageModel);
                db.SaveChanges();
                return TempImage.Id;
            }
            else
            {
                return 0;
            }
        }

        public static List<Product_Type> GetAllProductTypes()
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                return db.Product_Type.ToList();
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

        public static Category GetCategoryDetails(int productTypeID)
        {
            EcommerceDBEntities db = new EcommerceDBEntities();
            try
            {
                return db.Categories.Where(x => x.Product_type_id == productTypeID).FirstOrDefault();
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

}