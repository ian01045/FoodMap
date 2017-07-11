using FoodDeliveryTemplete.Areas.Restaurant.Models;
using FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD;
using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class ProductController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        Models.CRUD.Repository<tCategory> reposCategory = new Models.CRUD.Repository<tCategory>();
        Models.CRUD.Repository<FoodDeliveryTemplete.Models.tRestaurant_Foods> reposFood = new Models.CRUD.Repository<FoodDeliveryTemplete.Models.tRestaurant_Foods>();
        Models.CRUD.Repository<tRestaurant_FoodCustomizedCategory> reposCustomizedCategory = new Models.CRUD.Repository<tRestaurant_FoodCustomizedCategory>();
        Models.CRUD.Repository<tPhoto> reposPhoto = new Models.CRUD.Repository<tPhoto>();
        // GET: Restaurant/Product
        public ActionResult ProductIndex()
        {
            return View(reposFood.GetAll()); 
        }
        public ActionResult ProductOnSell()
        {
            return View(reposFood.GetAll().Where(c=>c.fAvailable==true));
        }
       
        public ActionResult ProductCreate()
        {
            return View(); 
        }
        
        public ActionResult ProductCreateFunction()
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            ViewBag.CustomerCategorys = reposCustomizedCategory.GetAll().Where(c => c.fRestaurantID == RestaurantID);
            return View();
        }
        [HttpPost]
        public ActionResult ProductCreateFunction(FoodDeliveryTemplete.Models.tRestaurant_Foods Fooddata, string PhotoID,bool CustomizedKeep)
        {
            if (CustomizedKeep == false)//不保留客製化選項
            {
                Fooddata.fCustomizedCategoryID = null;
            }
            Fooddata.fRestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            Fooddata.fModifiedDate = DateTime.Now;
            //Fooddata.fCookTime = TimeSpan.Parse(cookhour+":"+ cookmin + ":"+ cooksec);
            reposFood.Create(Fooddata);
            //return RedirectToAction("ProductCreateFunction","Product",new { Areas = "Restaurant" });

            //找FoodID
          
            
            if (PhotoID != null)
            {
                int Foodid = reposFood.GetAll()
                      .Where(c => c.fFoodName == Fooddata.fFoodName && c.fRestaurantID == Fooddata.fRestaurantID && c.fCategoryID== Fooddata.fCategoryID)
                      .Select(c => c.fFoodID).SingleOrDefault();

                tPhoto changePhoto = new tPhoto();

                changePhoto.fBytesImage = Convert.FromBase64String(PhotoID);
                changePhoto.fDateTime = DateTime.Today;
                changePhoto.fFoodID = Foodid;
                reposPhoto.Create(changePhoto);
            }
            return RedirectToAction("ProductCreateFunction","Product");
        }
       
        [HttpGet]
        public ActionResult ProductUpdate(int ProductID)
        {
            ViewBag.Category = reposCategory.GetAll().ToList();
            var data = reposFood.getbyid(ProductID);
            char[] delimiterChars = {':'};
            string[] timewords = data.fCookTime.ToString().Split(delimiterChars);
            
            ViewBag.hour = timewords[0];//時
            ViewBag.min = timewords[1];//分
            ViewBag.sec = timewords[2];//秒
            return View(data);
        }
        [HttpPost]
        public ActionResult ProductUpdate(FoodDeliveryTemplete.Models.tRestaurant_Foods Fooddata, string PhotoID,bool deletpic,bool CustomizedKeep)
        {
           
            if (CustomizedKeep == false)//不保留客製化選項
            {
                Fooddata.fCustomizedCategoryID = null;
            }
            Fooddata.fRestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            Fooddata.fModifiedDate = DateTime.Now;
            reposFood.Update(Fooddata);
            
            //---------------------------------------------------------------------------------------------------------------
            //照片判斷
            if (deletpic == true)//要刪除已設定的照片
            {
                tPhoto changePhotodelete = reposPhoto.GetAll().Where(c => c.fFoodID == Fooddata.fFoodID).SingleOrDefault();
                if (changePhotodelete != null)//原本真的有照片才刪除
                {
                    reposPhoto.Delete(changePhotodelete);
                }
            }
            tPhoto changePhoto = reposPhoto.GetAll().Where(c => c.fFoodID == Fooddata.fFoodID).SingleOrDefault();
            if (PhotoID != null)//有新增照片
            {
                if (changePhoto != null)//產品已經有圖片>>修改
                {
                    changePhoto.fBytesImage = Convert.FromBase64String(PhotoID);
                    changePhoto.fDateTime = DateTime.Today;
                    changePhoto.fFoodID = Fooddata.fFoodID;
                    reposPhoto.Update(changePhoto);
                }
                else//還沒有產品圖片>>新增
                {
                    tPhoto addPhoto = new tPhoto();

                    addPhoto.fBytesImage = Convert.FromBase64String(PhotoID);
                    addPhoto.fDateTime = DateTime.Today;
                    addPhoto.fFoodID = Fooddata.fFoodID;
                    reposPhoto.Create(addPhoto);
                }
                
            }

            //return RedirectToAction("ProductCreateFunction","Product",new { Areas = "Restaurant" });
            return RedirectToAction("ProductUpdate", "Product",new { ProductID = Fooddata.fFoodID });
        }

        public ActionResult ProductUpdateOff(int ProductID)
        {
            var data = reposFood.getbyid(ProductID);
            data.fAvailable = !data.fAvailable;
            reposFood.Update(data);
            return RedirectToAction("Index","Home");
        }
    }
}