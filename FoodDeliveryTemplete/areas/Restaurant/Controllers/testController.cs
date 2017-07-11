using FoodDeliveryTemplete.Areas.Restaurant.Models;
using FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD;
using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class testController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: Restaurant/test
        public ActionResult Index()
        {
            ViewBag.firstCate =db.tCategory.Where(c => c.fParentID == null).ToList();
            return View();
        }
        public ActionResult GetFirstCategoryData()
        {
            var tCategorydatas = db.tCategory.Where(c => c.fParentID == null).ToList();
            List<CategoryToJSON> CategoryToJSON = new List<CategoryToJSON>();
            CategoryToJSON data = new CategoryToJSON();
            data.CategoryName = "請選擇";
            CategoryToJSON.Add(data);
            foreach (var CategoryData in tCategorydatas)
            {
                data = new CategoryToJSON();
                data.CategoryId = CategoryData.fCategoryID;
                data.CategoryName = CategoryData.fCategoryName;
                data.CategoryParent = CategoryData.fParentID.ToString();
                CategoryToJSON.Add(data);
            }

            var json = Json(CategoryToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult GetSECCategoryData(int CategoryID)
        {
            var tCategorydatas = db.tCategory.Where(c => c.fParentID == CategoryID).ToList();
            List<CategoryToJSON> CategoryToJSON = new List<CategoryToJSON>();
            CategoryToJSON data = new CategoryToJSON();
            data.CategoryName = "請選擇";
            CategoryToJSON.Add(data);
            foreach (var CategoryData in tCategorydatas)
            {
                data = new CategoryToJSON();
                data.CategoryId = CategoryData.fCategoryID;
                data.CategoryName = CategoryData.fCategoryName;
                data.CategoryParent = CategoryData.fParentID.ToString();
                CategoryToJSON.Add(data);
            }

            var json = Json(CategoryToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
      
    }
}