using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModel;
using FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class RestaurantController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        Models.CRUD.Repository<tRestaurant_Foods> RestFood = new Models.CRUD.Repository<tRestaurant_Foods>();
        // GET: Member/Restaurant
        public ActionResult Index(int? id)
        {
            if (id > 0)
            {
                Response.Cookies["RestaurantID"].Value = id.ToString();
                Response.Cookies["RestaurantID"].Expires = DateTime.Now.AddDays(1);
                ViewBag.restaurantid = id;
                return View();
            }
            var account = Request.Cookies["AccountLogin"].Value;
            int memberid = db.tMemeber.Where(c => c.fEmail == account).Select(c => c.fMemberID).SingleOrDefault();
            int restaurantid = db.tRestaurant.Where(c => c.fMemberID == memberid).Select(c => c.fRestaurantID).SingleOrDefault();
            if (restaurantid > 0)//有開店
            {
                string restaurantname = db.tRestaurant.Where(c => c.fRestaurantID == restaurantid).Select(c => c.fRestaurantName).SingleOrDefault();
                ViewBag.restaurantname = restaurantname;
                Response.Cookies["RestaurantID"].Value = restaurantid.ToString();
                Response.Cookies["RestaurantID"].Expires = DateTime.Now.AddDays(1);
                ViewBag.restaurantid = restaurantid;
                return View();
            }
            else
            {
                return RedirectToAction("OpenRestaurant");
            }
            //return View();
        }
       
        [HttpGet]
        public ActionResult OpenRestaurant()
        {
            int id = Convert.ToInt16(Request.Cookies["MemberID"].Value);
            tMemeber member = db.tMemeber.Find(id);
            
            ViewBag.MemberID = member.fMemberID;
            ViewBag.MemberName = member.fMemeberName;


            //付款方式的種類
            var query = db.tPaymentWay.ToList();

            List<SelectListItem> items = new List<SelectListItem>();

            foreach(var payway in query)
            {
                items.Add(new SelectListItem { Text = payway.fPaymentway, Value = payway.fPaymentwayID.ToString()});
            }

            ViewBag.Payways = items;

            //一週星期
            List<SelectListItem> weeks = new List<SelectListItem>();
            weeks.Add(new SelectListItem { Text = "星期一", Value = "星期一" });
            weeks.Add(new SelectListItem { Text = "星期二", Value = "星期二" });
            weeks.Add(new SelectListItem { Text = "星期三", Value = "星期三" });
            weeks.Add(new SelectListItem { Text = "星期四", Value = "星期四" });
            weeks.Add(new SelectListItem { Text = "星期五", Value = "星期五" });
            weeks.Add(new SelectListItem { Text = "星期六", Value = "星期六" });
            weeks.Add(new SelectListItem { Text = "星期日", Value = "星期日" });

            ViewBag.Weeks = weeks;

            return View();
        }
        [HttpPost]
        public ActionResult OpenRestaurant( OpenRestaurantViewModel or )
        {
            tMemeber member = db.tMemeber.Find(or.fMemberId);

            ViewBag.MemberID = member.fMemberID;
            ViewBag.MemberName = member.fMemeberName;

            //付款方式的種類
            var query = db.tPaymentWay.ToList();

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var payway in query)
            {
                items.Add(new SelectListItem { Text = payway.fPaymentway, Value = payway.fPaymentwayID.ToString() });
            }


            ViewBag.Payways = items;

            //一週星期
            List<SelectListItem> weeks = new List<SelectListItem>();
            weeks.Add(new SelectListItem { Text = "星期一", Value = "星期一" });
            weeks.Add(new SelectListItem { Text = "星期二", Value = "星期二" });
            weeks.Add(new SelectListItem { Text = "星期三", Value = "星期三" });
            weeks.Add(new SelectListItem { Text = "星期四", Value = "星期四" });
            weeks.Add(new SelectListItem { Text = "星期五", Value = "星期五" });
            weeks.Add(new SelectListItem { Text = "星期六", Value = "星期六" });
            weeks.Add(new SelectListItem { Text = "星期日", Value = "星期日" });

            ViewBag.Weeks = weeks;

            if (ModelState.IsValid)
            {
                tRestaurant create = new tRestaurant();
                create.fMemberID = or.fMemberId;
                create.fOpenTime = or.fOpenTime;
                create.fCloseTime = or.fCloseTime;
                create.fDescription = or.fDescription;
                create.fRestaurantName = or.fRestaurantName;
                create.fAveragePricePerGuest = or.fAveragePricePerGuest;
                //將產生在會員註冊時的照片PhotoID找出來,存到餐廳資料表的PhotoID欄位
                var photoId = db.tPhoto.Where(p => p.fMemberID == or.fMemberId).Select(p => p.fPhotoID).Single();
                create.fPhotoID = photoId;
                //將前端所選的星期加入,寫入欄位
                var week = string.Join(",", or.SelectedWeek);

                create.fRoutine_RestDay_per_week_ = week;

                db.tRestaurant.Add(create);
                db.SaveChanges();

                //抓產生後的RestaurantId
                var RestaurantId = Convert.ToInt32(db.tRestaurant.Select(r => r.fRestaurantID).ToList().Last());
                TempData["name"] = RestaurantId;

                //寫入付款方式               
                foreach(var payway in or.PaywayCheckboxs)
                {
                    tRestaurant_PaymentWay_Detail CreatePayWayDetail = new tRestaurant_PaymentWay_Detail();
                    CreatePayWayDetail.fRestaurantID = RestaurantId;
                    CreatePayWayDetail.fPaymentwayID = payway;
                    db.tRestaurant_PaymentWay_Detail.Add(CreatePayWayDetail);
                }
                db.SaveChanges();

                return RedirectToAction("Index", "Restaurant");
            }
            return View();
        }

        public ActionResult InformationRestaurant()
        {
            int id = Convert.ToInt16(Request.Cookies["MemberID"].Value);
            tRestaurant restaurant = db.tRestaurant.Where(r => r.fMemberID == id).Single();

            InformationRestaurantViewModel infoRest = new InformationRestaurantViewModel();

            infoRest.fMemberID = restaurant.fMemberID;
            infoRest.fRestaurantName = restaurant.fRestaurantName;
            infoRest.fDescription = restaurant.fDescription;
            infoRest.fRoutine_RestDay_per_week_ = restaurant.fRoutine_RestDay_per_week_;
            infoRest.fOpenTime = restaurant.fOpenTime;
            infoRest.fCloseTime = restaurant.fCloseTime;
            infoRest.fAveragePricePerGuest = restaurant.fAveragePricePerGuest;
            infoRest.PaymentWay_Detail = db.tRestaurant_PaymentWay_Detail.Where(d => d.fRestaurantID == restaurant.fRestaurantID);
            
            return View(infoRest);
        }
        [HttpGet]
        public ActionResult InformationRestaurantModifyForm(int id)
        {
           
            tMemeber member = db.tMemeber.Find(id);

            ViewBag.MemberID = member.fMemberID;
            ViewBag.MemberName = member.fMemeberName;
            //------
            //付款方式的種類
            var query = db.tPaymentWay.ToList();

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var payway in query)
            {
                items.Add(new SelectListItem { Text = payway.fPaymentway, Value = payway.fPaymentwayID.ToString() });
            }

            ViewBag.Payways = items;

            //一週星期
            List<SelectListItem> weeks = new List<SelectListItem>();
            weeks.Add(new SelectListItem { Text = "星期一", Value = "星期一" });
            weeks.Add(new SelectListItem { Text = "星期二", Value = "星期二" });
            weeks.Add(new SelectListItem { Text = "星期三", Value = "星期三" });
            weeks.Add(new SelectListItem { Text = "星期四", Value = "星期四" });
            weeks.Add(new SelectListItem { Text = "星期五", Value = "星期五" });
            weeks.Add(new SelectListItem { Text = "星期六", Value = "星期六" });
            weeks.Add(new SelectListItem { Text = "星期日", Value = "星期日" });

            ViewBag.Weeks = weeks;
            //------
            tRestaurant restaurant = db.tRestaurant.Where(r => r.fMemberID == id).Single();

            OpenRestaurantViewModel or = new OpenRestaurantViewModel();
            or.fRestaurantName = restaurant.fRestaurantName;
            or.fMemberId = restaurant.fRestaurantID;
            or.fOpenTime = restaurant.fOpenTime;
            or.fCloseTime = restaurant.fCloseTime;
            or.fDescription = restaurant.fDescription;
            or.fAveragePricePerGuest = restaurant.fAveragePricePerGuest;

            var week = restaurant.fRoutine_RestDay_per_week_.Split(',');
            or.SelectedWeek = week;

            var paymentways = db.tRestaurant_PaymentWay_Detail.Where(p => p.fRestaurantID == restaurant.fRestaurantID).Select(p => p.fPaymentwayID).ToList();
            or.PaywayCheckboxs = paymentways;
        


            return View(or);
        }
        [HttpPost]
        public ActionResult InformationRestaurantModifyForm(int id,OpenRestaurantViewModel or)
        {
            var restaurantId = db.tRestaurant.Where(r => r.fMemberID == id).Select(r => r.fRestaurantID).Single();

            tRestaurant update = db.tRestaurant.Find(restaurantId);
            update.fRestaurantName = or.fRestaurantName;
            update.fOpenTime = or.fOpenTime;
            update.fCloseTime = or.fCloseTime;
            update.fDescription = or.fDescription;
            update.fAveragePricePerGuest = or.fAveragePricePerGuest;
            var week = string.Join(",", or.SelectedWeek);

            //修改重新選的星期
            update.fRoutine_RestDay_per_week_ = week;

            //刪除之前所存的付款方式
            var paymentways = db.tRestaurant_PaymentWay_Detail.Where(p => p.fRestaurantID == restaurantId).Select(p => p.fID).ToList();

            foreach(var paywayId in paymentways)
            {
                tRestaurant_PaymentWay_Detail delete = db.tRestaurant_PaymentWay_Detail.Find(paywayId);
                db.tRestaurant_PaymentWay_Detail.Remove(delete);               
            }

            db.SaveChanges();

            //重新寫入付款方式
            //寫入付款方式               
            foreach (var payway in or.PaywayCheckboxs)
            {
                tRestaurant_PaymentWay_Detail CreatePayWayDetail = new tRestaurant_PaymentWay_Detail();
                CreatePayWayDetail.fRestaurantID = restaurantId;
                CreatePayWayDetail.fPaymentwayID = payway;
                db.tRestaurant_PaymentWay_Detail.Add(CreatePayWayDetail);
            }
            db.SaveChanges();

            return RedirectToAction("InformationRestaurant", "Restaurant");
        }

        //page = 頁數
        //pageRows = 一頁多少筆 & 取多少筆
        int pageRows = 5;
        public ActionResult ShowRestaurantFood(int id)
        {
            ViewBag.RestaurantIDIs = id;
            return PartialView(RestFood.GetAll().Where(c => c.fIsMeal == false && c.fRestaurantID == id).ToList());
        }
        public ActionResult ProductAllProduct(int id)
        {
            var product = RestFood.GetAll().Where(c => c.fIsMeal == false && c.fRestaurantID == id && c.fAvailable == true).ToList();
               
            return PartialView(product.ToList());
        }
        //顯示所有產品(單品)
        public ActionResult Product(int page)
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            //pagecut = 頁數
            //pageRows = 一頁多少筆 & 取多少筆
            var count = RestFood.GetAll().Where(c => c.fIsMeal == false && c.fRestaurantID == RestaurantID && c.fAvailable == true);

            float pagenumber = count.Count();
            ViewBag.productcount = Convert.ToInt32(Math.Ceiling(pagenumber / 5));//計算分頁按鈕數量

            var product = RestFood.GetAll().Where(c => c.fIsMeal == false && c.fRestaurantID == RestaurantID && c.fAvailable == true)
                .Skip((page - 1) * pageRows).Take(pageRows);//抓前五個
            return PartialView(product.ToList());
        }

        //顯示所有套餐
        public ActionResult ProductMeals(int page)
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            //pagecut = 頁數
            //pageRows = 一頁多少筆 & 取多少筆
            var count = RestFood.GetAll().Where(c => c.fIsMeal == true && c.fRestaurantID == RestaurantID && c.fAvailable == true);
            float pagenumber = count.Count();
            ViewBag.productcount = Convert.ToInt32(Math.Ceiling(pagenumber / 5));
            var product = RestFood.GetAll().Where(c => c.fIsMeal == true && c.fRestaurantID == RestaurantID && c.fAvailable == true)
               .Skip((page - 1) * pageRows).Take(pageRows);//抓前五個
            return PartialView(product.ToList());
        }
        //顯示不上架商品(單品)
        public ActionResult ProductUnAvailable(int page)
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            //pagecut = 頁數
            //pageRows = 一頁多少筆 & 取多少筆
            var count = RestFood.GetAll().Where(c => c.fIsMeal == false && c.fRestaurantID == RestaurantID && c.fAvailable == false);

            float pagenumber = count.Count();
            ViewBag.productcount = Convert.ToInt32(Math.Ceiling(pagenumber / 5));

            var product = RestFood.GetAll().Where(c => c.fIsMeal == false && c.fRestaurantID == RestaurantID && c.fAvailable == false)
               .Skip((page - 1) * pageRows).Take(pageRows);//抓前五個
            return PartialView(product.ToList());
        }
        //顯示不上架商品(套餐)
        public ActionResult ProductUnAvailableMeal(int page)
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            //pagecut = 頁數
            //pageRows = 一頁多少筆 & 取多少筆
            var count = RestFood.GetAll().Where(c => c.fIsMeal == true && c.fRestaurantID == RestaurantID && c.fAvailable == false);
            float pagenumber = count.Count();
            ViewBag.productcount = Convert.ToInt32(Math.Ceiling(pagenumber / 5));
            var product = RestFood.GetAll().Where(c => c.fIsMeal == true && c.fRestaurantID == RestaurantID && c.fAvailable == false)
               .Skip((page - 1) * pageRows).Take(pageRows);//抓前五個
            return PartialView(RestFood.GetAll().Where(c => c.fIsMeal == true && c.fRestaurantID == RestaurantID && c.fAvailable == false).ToList());
        }
        //快速刪除產品
        public ActionResult ProductDelete(int? id)
        {
            RestFood.Delete(RestFood.getbyid(id));
            return RedirectToAction("Index", "Restaurant");
        }
        [HttpPost]
        public ActionResult DeleteCheckedProduct(int fFoodID)
        {
            RestFood.Delete(RestFood.getbyid(fFoodID));
            return RedirectToAction("Index", "Restaurant");
        }
        public ActionResult AvailableCheckedProduct(int fFoodID)
        {
            var data = RestFood.getbyid(fFoodID);
            data.fAvailable = !data.fAvailable;
            RestFood.Update(data);
            return RedirectToAction("Index", "Restaurant");
        }
        //設定分類
        public ActionResult FoodCategory()
        {
            return View();
        }
    }
   
}