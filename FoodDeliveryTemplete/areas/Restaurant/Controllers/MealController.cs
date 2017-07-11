using FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD;
using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class MealController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        Models.CRUD.Repository<tRestaurant_Foods> ReposMeal = new Models.CRUD.Repository<tRestaurant_Foods>();
        Models.CRUD.Repository<tRestaurantMeal_Detail> ReposMealdata = new Models.CRUD.Repository<tRestaurantMeal_Detail>();
        Models.CRUD.Repository<tCategory> ReposCategory = new Models.CRUD.Repository<tCategory>();
        //取得餐廳ID方法
        //RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
        // GET: Restaurant/Meal
        public ActionResult Index()
        {
            int restid = int.Parse(Request.Cookies["RestaurantID"].Value);//餐廳ID
            var count = ReposMeal.GetAll().Where(c => c.fIsMeal == true && c.fRestaurantID == restid);

            float pagenumber = count.Count();
            ViewBag.productcount = Convert.ToInt32(Math.Ceiling(pagenumber / 10));//計算分頁按鈕數量
            return View();
        }
        //get新增套餐頁面
        public ActionResult CreateMeal()
        {
            return View();
        }
        //post新增套餐頁面
        [HttpPost]
        public ActionResult CreateMeal(tRestaurantMeal_Detail data)
        {
            ReposMealdata.Create(data);
            return RedirectToAction("Index");
        }
        public ActionResult UpdateMeal(tRestaurantMeal_Detail data)
        {
            ReposMealdata.Update(data);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteMeal(int id)
        {
            ReposMealdata.Delete(ReposMealdata.getbyid(id));
            return RedirectToAction("Index");
        }
        public ActionResult ShowMealProduct(int id)
        {
            int restid = int.Parse(Request.Cookies["RestaurantID"].Value);
            if (ReposMealdata.GetAll().Where(c => c.fMealID == id).Count() != 0)
            {
                ViewBag.Product = ReposMeal.GetAll().Where(c => c.fIsMeal == false && c.fRestaurantID== restid).ToList();
                ViewBag.Meal = ReposMealdata.GetAll().Where(c => c.fMealID == id).ToList();

                var Mealdata = ReposMealdata.GetAll().Where(c => c.fMealID == id).ToList();
                return PartialView(Mealdata);
            }
            return RedirectToAction("MealHaveNoProduct");
        }
        public ActionResult MealHaveNoProduct()
        {
            ViewBag.message = "此套餐尚未設定內容";
            return PartialView();
        }
        public ActionResult RestaurantHaveNoMeal()
        {
            ViewBag.message = "餐廳尚未設定套餐商品";
            return PartialView();
        }
        public ActionResult getMeals()
        {
            int restid = int.Parse(Request.Cookies["RestaurantID"].Value);
            var Meals = db.tRestaurant_Foods.Where(c=>c.fIsMeal==true && c.fRestaurantID == restid).ToList();
            
            List<MealsToJSON> MealsToJSON = new List<MealsToJSON>();
            MealsToJSON data =  new MealsToJSON();
            data.MealName = "請選擇";
            MealsToJSON.Add(data);
            foreach (var MealName in Meals)
            {
                data = new MealsToJSON();
                data.MealName = MealName.fFoodName;
                data.FoodId = MealName.fFoodID;
                MealsToJSON.Add(data);
            }

            var json = Json(MealsToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }

        //取得該餐廳套餐
        public ActionResult getMealsCount(int page)
        {
            int restid = int.Parse(Request.Cookies["RestaurantID"].Value);//餐廳ID
            int pageRows = 10;//一次顯示多少筆
            var Meals = ReposMeal.GetAll().Where(c => c.fIsMeal == true && c.fRestaurantID == restid)
                .Skip((page - 1) * pageRows).Take(pageRows);//取多少筆
            
            List<MealsToJSON> MealsToJSON = new List<MealsToJSON>();
            MealsToJSON data = new MealsToJSON();

            foreach (var MealName in Meals)
            {
                int countForMeal = ReposMealdata.GetAll().Where(c => c.fMealID == MealName.fFoodID).Count();
                data = new MealsToJSON();
                data.MealName = MealName.fFoodName;
                data.FoodId = MealName.fFoodID;
                data.FoodCount = countForMeal;
                MealsToJSON.Add(data);
            }

            var json = Json(MealsToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }

        //找套餐內容清單
        public ActionResult returnProductInMeal(int Mealid)
        {
            var Meals = ReposMealdata.GetAll().Where(c => c.fMealID == Mealid).ToList();
            int Mealscount = Meals.Count;
            List<MealsToJSON> MealsToJSON = new List<MealsToJSON>();
            MealsToJSON data = new MealsToJSON();
            if (Mealscount != 0)
            {
                foreach (var MealName in Meals)
                {
                    data = new MealsToJSON();
                    data.fid = MealName.fID;
                    data.FoodId = MealName.fFoodID;
                    data.FoodName = MealName.tRestaurant_Foods1.fFoodName;
                    data.Mealid = MealName.fMealID;
                    data.FoodCount = Mealscount;
                    MealsToJSON.Add(data);
                }
            }
            else
            {
                data.FoodCount = Mealscount;
                MealsToJSON.Add(data);
            }

            var json = Json(MealsToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult getProduct()
        {
            //抓非套餐的商品(但是用套餐的MealsToJSON  因為紀錄的物品相同)
            var Meals = db.tRestaurant_Foods.Where(c => c.fIsMeal == false).ToList();
            List<MealsToJSON> MealsToJSON = new List<MealsToJSON>();
            MealsToJSON data = new MealsToJSON();
            data.MealName = "請選擇";
            MealsToJSON.Add(data);
            foreach (var MealName in Meals)
            {
                data = new MealsToJSON();
                data.MealName = MealName.fFoodName;
                data.FoodId = MealName.fFoodID;
                MealsToJSON.Add(data);
            }

            var json = Json(MealsToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult getProducts(int categoryid)
        {
            int restid = int.Parse(Request.Cookies["RestaurantID"].Value);//餐廳ID
            //----------------------------------------------------------------------------------------------------------------------------
            //存找到的分類
            List<int> allchilds = new List<int>();
            //得到的分類直接儲存
            allchilds.Add(categoryid);

            //還有子分類的話開始找
            IEnumerable<int> childcategoryid = ReposCategory.GetAll().Where(c => c.fParentID == categoryid).Select(c => c.fCategoryID);
            foreach (int child in childcategoryid)//判斷是否還有子分類
            {
                allchilds.Add(child);//將剛抓的子分類存入
                IEnumerable<int> getchilds = findchild(child);//找出子分類的子分類
                foreach (int getchild in getchilds)
                {
                    allchilds.Add(getchild);//將子分類的子分類存入
                }
            }
            //----------------------------------------------------------------------------------------------------------------------------
            //用找到的所有分類找產品
            List<ProductToJson> RestaurantFoods = new List<ProductToJson>();
            foreach (int allcategory in allchilds)
            {
                //ReposMeal為餐廳產品的repostory
                var productinformation = ReposMeal.GetAll().Where(c => c.fCategoryID == allcategory //找此分類商品
                                                                    && c.fRestaurantID == restid //找此餐廳商品
                                                                    && c.fIsMeal == false //商品非套餐
                                                                    && c.fAvailable==true //是上架的商品
                                                                    );
                if (productinformation != null)
                {
                    foreach (var oneproduct in productinformation)
                    {
                        ProductToJson product = new ProductToJson();
                        product.fFoodID = oneproduct.fFoodID;
                        product.fFoodName = oneproduct.fFoodName;
                        RestaurantFoods.Add(product);
                    }
                }
            }
            // data.MealName = "請選擇";
            var json = Json(RestaurantFoods.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }

        //找子分類方法
        private IEnumerable<int> findchild(int fatherscategoryid)
        {
            IEnumerable<int> childsid = ReposCategory.GetAll().Where(c => c.fParentID == fatherscategoryid).Select(c => c.fCategoryID);
            return childsid;
        }
    }
}