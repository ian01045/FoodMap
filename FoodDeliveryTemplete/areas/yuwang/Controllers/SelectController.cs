using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Areas.yuwang.Models;
using FoodDeliveryTemplete.Areas.yuwang.ViewModel;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.yuwang.Controllers
{
    public class SelectController : Controller
    {
        // GET: yuwang/Select

        public FoodDeliveryEntities dc = new FoodDeliveryEntities();

        //回傳餐廳二進位圖
        public ActionResult GetImageByte(int id)
        {
            tPhoto photo = dc.tPhoto.Where(p => p.fMemberID == id).Single();
            byte[] img = photo.fBytesImage;
            return File(img, "image/jpeg");

        }

        //public ActionResult GetImageByte_Rotation(int catID)
        //{
        //   // Random r = new Random();

        //    tPhoto photo = dc.tPhoto.Where(p =>p.tRestaurant.fRestaurant_CategoryID==catID).Take(1).Select(p=>p).Single();
        //    byte[] img = photo.fBytesImage;
        //    return File(img, "image/jpeg");

        //}

        public ActionResult GetImageByte_ResID(int resID)
        {
            tPhoto photo = dc.tPhoto.Where(p => p.tMemeber.tRestaurant.Where(r => r.fRestaurantID == resID).Count() > 0).Select(p => p).Single();
            byte[] img = photo.fBytesImage;
            return File(img, "image/jpeg");
        }


        public ActionResult GetImageByte_FoodID(int foodID)
        {
            tPhoto photo = dc.tPhoto.Where(p => p.fFoodID == foodID).Select(f => f).Single();
            byte[] img = photo.fBytesImage;
            return File(img, "image/jpeg");
        }
        ////在Index內搜尋餐廳，用"餐廳分類"、"菜色分類"、"各種排序"
        //[HttpGet]
        //public ActionResult Index(int restaurant_categoryID, int sortby, int food_categoryID)
        //{


        //    //切換位置傳入的id就是0，清除cookies
        //    //if (restaurant_categoryID == 0 && sortby == 0 && food_categoryID == 0)
        //    //{
        //    //    Response.Cookies["current_latitude"].Expires = DateTime.Now.AddMinutes(-1);
        //    //    Response.Cookies["current_longitude"].Expires = DateTime.Now.AddMinutes(-1);
        //    //    Response.Cookies["current_address"].Expires = DateTime.Now.AddMinutes(-1);

        //    //    return RedirectToAction("Index","Blog");
        //    //}

        //    ViewBag.current_address = Request.Cookies["current_address"].Value;


        //    //依餐廳類別分類
        //    if (restaurant_categoryID != 0 && sortby == 0 && food_categoryID == 0)
        //    {
        //        string current_latitude = Request.Cookies["current_latitude"].Value;
        //        string current_longitude = Request.Cookies["current_longitude"].Value;

        //        //使用者所在座標
        //        double lng = Convert.ToDouble(current_longitude);//經度
        //        double lat = Convert.ToDouble(current_latitude);//緯度
        //        CoordinateUtility utility = new CoordinateUtility();

        //        var restaurants1 = from d in dc.tMemeber.ToList()
        //                           where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5 && d.fRoleID == 2
        //                           where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
        //                           where d.tRestaurant.Where(r=>r.tRestaurant_Category_Details.Where(c=>c.fRestaurant_CategoryID== restaurant_categoryID).Count()>0).Count()>0
        //                           select d;


        //        List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
        //        foreach (tMemeber restaurant in restaurants1)
        //        {
        //            RestaurantSelect rc = new RestaurantSelect();
        //            rc.fRestaurantName = restaurant.fMemeberName;
        //            rc.fMemberID = restaurant.fMemberID;
        //            var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
        //            rc.fRestaurant_Foods = foods;

        //            var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
        //            var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //            rc.fCommentCounts = comment_counts;
        //            var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //            rc.fMyFavCounts = myfav_counts;
        //            var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //            rc.fStarsCounts = stars_counts;

        //            rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();

        //            rc.fJoinDate = restaurant.fJoinDate;

        //            rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

        //            rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

        //            TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //            rc.fAverageArrivalTime = delivery_time;
        //            TimeSpan longest_cook_food_time;
        //            if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //            {
        //                longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                rc.fAverageArrivalTime = arrvial_time;
        //            }

        //            rcs1.Add(rc);
        //        }

        //        return View(rcs1);
        //    }

        //    //依照各種排序分類
        //    if (restaurant_categoryID == 0 && sortby != 0 && food_categoryID == 0)
        //    {

        //        string current_latitude = Request.Cookies["current_latitude"].Value;
        //        string current_longitude = Request.Cookies["current_longitude"].Value;

        //        //使用者所在座標
        //        double lng = Convert.ToDouble(current_longitude);//經度
        //        double lat = Convert.ToDouble(current_latitude);//緯度
        //        CoordinateUtility utility = new CoordinateUtility();


        //        if (sortby == 1)//依照開店日期最新排序
        //        {
        //            var restaurants1 = from d in dc.tMemeber.ToList()
        //                               where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5 && d.fRoleID == 2
        //                               where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
        //                               orderby d.fJoinDate descending
        //                               select d;

        //            List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
        //            foreach (tMemeber restaurant in restaurants1)
        //            {
        //                RestaurantSelect rc = new RestaurantSelect();
        //                rc.fRestaurantName = restaurant.fMemeberName;
        //                rc.fMemberID = restaurant.fMemberID;
        //                var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
        //                rc.fRestaurant_Foods = foods;

        //                var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
        //                var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //                rc.fCommentCounts = comment_counts;
        //                var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //                rc.fMyFavCounts = myfav_counts;
        //                var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //                rc.fStarsCounts = stars_counts;

        //                rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();

        //                rc.fJoinDate = restaurant.fJoinDate;

        //                rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

        //                rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

        //                TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //                rc.fAverageArrivalTime = delivery_time;
        //                TimeSpan longest_cook_food_time;
        //                if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //                {
        //                    longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                    TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                    rc.fAverageArrivalTime = arrvial_time;
        //                }

        //                rcs1.Add(rc);
        //            }

        //            return View(rcs1);
        //        }



        //        if (sortby == 2)//依照餐廳訂單量作熱門排序
        //        {
        //            var restaurants1 = from d in dc.tMemeber.ToList()
        //                               where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5 && d.fRoleID == 2
        //                               where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
        //                               select d;

        //            var res_in_orders = from m in restaurants1
        //                                join r in dc.tRestaurant
        //                                 on m.fMemberID equals r.fMemberID
        //                                 join o in dc.tRestaurantOrders
        //                                 on r.fRestaurantID equals o.fRestaurantID
        //                                 group o by o.fRestaurantID into Group
        //                                 select new
        //                                 {
        //                                     Key = Group.Key,
        //                                     OrderCounts = Group.Count()
        //                                 };
        //            var ordered_res = res_in_orders.OrderByDescending(g => g.OrderCounts);

        //            var res_not_in_orders = from m in restaurants1
        //                                     where m.tRestaurant.Where(r => r.tRestaurantOrders.Where(o => o.fRestaurantID == r.fRestaurantID).Count() == 0).Count() > 0
        //                                     select m;

        //            List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
        //            foreach (var restaurant in ordered_res)
        //            {

        //                RestaurantSelect rc = new RestaurantSelect();
        //                tMemeber member = restaurants1.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m).Single();
        //                rc.fRestaurantName = member.fMemeberName;
        //                rc.fMemberID = member.fMemberID;
        //                var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == member.fMemberID).Select(f => f);
        //                rc.fRestaurant_Foods = foods;

        //                var resId = dc.tRestaurant.Where(r => r.fMemberID == member.fMemberID).Select(r => r.fRestaurantID).Single();
        //                var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //                rc.fCommentCounts = comment_counts;
        //                var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //                rc.fMyFavCounts = myfav_counts;
        //                var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //                rc.fStarsCounts = stars_counts;

        //                rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //                rc.fJoinDate = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fJoinDate).Single();

        //                rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fRestaurantID == restaurant.Key).Select(o => o).Count();

        //                var Longitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLongitude).Single();
        //                var Latitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLatitude).Single();
        //                rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat)));

        //                TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //                rc.fAverageArrivalTime = delivery_time;
        //                TimeSpan longest_cook_food_time;
        //                if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //                {
        //                    longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                    TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                    rc.fAverageArrivalTime = arrvial_time;
        //                }

        //                rcs1.Add(rc);
        //            }


        //            foreach (tMemeber restaurant in res_not_in_orders)
        //            {
        //                RestaurantSelect rc = new RestaurantSelect();
        //                rc.fRestaurantName = restaurant.fMemeberName;
        //                rc.fMemberID = restaurant.fMemberID;
        //                var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
        //                rc.fRestaurant_Foods = foods;

        //                var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
        //                var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //                rc.fCommentCounts = comment_counts;
        //                var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //                rc.fMyFavCounts = myfav_counts;
        //                var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //                rc.fStarsCounts = stars_counts;

        //                rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //                rc.fJoinDate = restaurant.fJoinDate;

        //                rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

        //                rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

        //                TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //                rc.fAverageArrivalTime = delivery_time;
        //                TimeSpan longest_cook_food_time;
        //                if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //                {
        //                    longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                    TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                    rc.fAverageArrivalTime = arrvial_time;
        //                }

        //                rcs1.Add(rc);
        //            }


        //            return View(rcs1);
        //        }



        //        if (sortby == 3)//依照平均星等最高排序
        //        {
        //            var res_member = from d in dc.tMemeber.ToList()
        //                             where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5 && d.fRoleID == 2
        //                             where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
        //                             select d;

        //            var res_in_comment = from m in res_member
        //                               join r in dc.tRestaurant
        //                               on m.fMemberID equals r.fMemberID
        //                               join c in dc.tComment
        //                               on r.fRestaurantID equals c.fRestaurantID
        //                               group c by c.fRestaurantID into Group
        //                               select new
        //                               {
        //                                   Key = Group.Key,
        //                                   AvgStar=Group.Average(g=>g.fStars)
        //                               };
        //            var ordered_res = res_in_comment.OrderByDescending(g => g.AvgStar);

        //            var res_not_in_comment = from m in res_member
        //                                     where m.tRestaurant.Where(r=>m.tComment.Where(c=>c.fRestaurantID==r.fRestaurantID).Count()==0).Count()>0
        //                                     select m;

        //            List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
        //            foreach (var restaurant in ordered_res)
        //            {

        //                RestaurantSelect rc = new RestaurantSelect();
        //                tMemeber member = res_member.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m).Single();
        //                rc.fRestaurantName = member.fMemeberName;
        //                rc.fMemberID = member.fMemberID;
        //                var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == member.fMemberID).Select(f => f);
        //                rc.fRestaurant_Foods = foods;

        //                var resId = dc.tRestaurant.Where(r => r.fMemberID == member.fMemberID).Select(r => r.fRestaurantID).Single();
        //                var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //                rc.fCommentCounts = comment_counts;
        //                var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //                rc.fMyFavCounts = myfav_counts;
        //                var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //                rc.fStarsCounts = stars_counts;

        //                rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //                rc.fJoinDate = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fJoinDate).Single();

        //                rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fRestaurantID == restaurant.Key).Select(o => o).Count();


        //                var Longitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLongitude).Single();
        //                var Latitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLatitude).Single();
        //                rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat)));

        //                TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //                rc.fAverageArrivalTime = delivery_time;
        //                TimeSpan longest_cook_food_time;
        //                if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //                {
        //                    longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                    TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                    rc.fAverageArrivalTime = arrvial_time;
        //                }

        //                rcs1.Add(rc);
        //            }


        //            foreach (tMemeber restaurant in res_not_in_comment)
        //            {
        //                RestaurantSelect rc = new RestaurantSelect();
        //                rc.fRestaurantName = restaurant.fMemeberName;
        //                rc.fMemberID = restaurant.fMemberID;
        //                var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
        //                rc.fRestaurant_Foods = foods;

        //                var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
        //                var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //                rc.fCommentCounts = comment_counts;
        //                var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //                rc.fMyFavCounts = myfav_counts;
        //                var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //                rc.fStarsCounts = stars_counts;

        //                rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //                rc.fJoinDate = restaurant.fJoinDate;

        //                rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

        //                rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

        //                TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //                rc.fAverageArrivalTime = delivery_time;
        //                TimeSpan longest_cook_food_time;
        //                if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //                {
        //                    longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                    TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                    rc.fAverageArrivalTime = arrvial_time;
        //                }

        //                rcs1.Add(rc);
        //            }


        //            return View(rcs1);

        //        }



        //        if (sortby == 4)//依照我的最愛收藏數最多排序
        //        {

        //            var res_member = from d in dc.tMemeber.ToList()
        //                             where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5 && d.fRoleID == 2
        //                             where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
        //                             select d;

        //            var res_in_myfav = from m in res_member
        //                      join r in dc.tRestaurant
        //                      on m.fMemberID equals r.fMemberID
        //                      join f in dc.tMyFavorite
        //                      on r.fRestaurantID equals f.fRestaurantID
        //                      group f by f.fRestaurantID into Group
        //                      select new
        //                      {
        //                          Key = Group.Key,
        //                          Count = Group.Count(),
        //                      };
        //            var ordered_res = res_in_myfav.OrderByDescending(g => g.Count);


        //            var res_not_in_myfav = from m in res_member
        //                                   where m.tRestaurant.Where(r => r.tMyFavorite.Where(f => f.fRestaurantID==r.fRestaurantID).Count() == 0).Count() > 0
        //                                   select m;

        //            List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
        //            foreach (var restaurant in ordered_res)
        //            {

        //                RestaurantSelect rc = new RestaurantSelect();
        //                tMemeber member= res_member.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m).Single();
        //                rc.fRestaurantName = member.fMemeberName;
        //                rc.fMemberID = member.fMemberID;
        //                var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == member.fMemberID).Select(f => f);
        //                rc.fRestaurant_Foods = foods;

        //                var resId = dc.tRestaurant.Where(r => r.fMemberID == member.fMemberID).Select(r => r.fRestaurantID).Single();
        //                var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //                rc.fCommentCounts = comment_counts;
        //                var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //                rc.fMyFavCounts = myfav_counts;
        //                var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //                rc.fStarsCounts = stars_counts;

        //                rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //                rc.fJoinDate = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fJoinDate).Single();

        //                rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fRestaurantID == restaurant.Key).Select(o => o).Count();


        //                var Longitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLongitude).Single();
        //                var Latitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLatitude).Single();
        //                rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat)));

        //                TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //                rc.fAverageArrivalTime = delivery_time;
        //                TimeSpan longest_cook_food_time;
        //                if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //                {
        //                    longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                    TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                    rc.fAverageArrivalTime = arrvial_time;
        //                }

        //                rcs1.Add(rc);
        //            }


        //            foreach (tMemeber restaurant in res_not_in_myfav)
        //            {
        //                RestaurantSelect rc = new RestaurantSelect();
        //                rc.fRestaurantName = restaurant.fMemeberName;
        //                rc.fMemberID = restaurant.fMemberID;
        //                var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
        //                rc.fRestaurant_Foods = foods;

        //                var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
        //                var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //                rc.fCommentCounts = comment_counts;
        //                var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //                rc.fMyFavCounts = myfav_counts;
        //                var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //                rc.fStarsCounts = stars_counts;

        //                rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //                rc.fJoinDate = restaurant.fJoinDate;

        //                rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

        //                rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

        //                TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //                rc.fAverageArrivalTime = delivery_time;
        //                TimeSpan longest_cook_food_time;
        //                if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //                {
        //                    longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                    TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                    rc.fAverageArrivalTime = arrvial_time;
        //                }

        //                rcs1.Add(rc);
        //            }


        //            return View(rcs1);
        //        }
        //    }

        //    //依照菜色類別分類，點選父類別可以找出所有子類別的餐廳
        //    if (restaurant_categoryID == 0 && sortby == 0 && food_categoryID != 0)
        //    {
        //        string current_latitude = Request.Cookies["current_latitude"].Value;
        //        string current_longitude = Request.Cookies["current_longitude"].Value;

        //        //使用者所在座標
        //        double lng = Convert.ToDouble(current_longitude);//經度
        //        double lat = Convert.ToDouble(current_latitude);//緯度
        //        CoordinateUtility utility = new CoordinateUtility();

        //        //找出父類別餐廳
        //        var parent_category_restaurants = from d in dc.tMemeber.ToList()
        //                                          where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5 && d.fRoleID == 2
        //                                          where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
        //                                          where d.tRestaurant.Where(r => r.tRestaurant_Foods.Where(f => f.fCategoryID == food_categoryID).Count() > 0).Count() > 0
        //                                          select d;
        //        SelectByFoodCategory_ResList.Clear();
        //        foreach (tMemeber restaurant in parent_category_restaurants)
        //        {
        //            RestaurantSelect rc = new RestaurantSelect();
        //            rc.fRestaurantName = restaurant.fMemeberName;
        //            rc.fMemberID = restaurant.fMemberID;
        //            var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
        //            rc.fRestaurant_Foods = foods;

        //            var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
        //            var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //            rc.fCommentCounts = comment_counts;
        //            var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //            rc.fMyFavCounts = myfav_counts;
        //            var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //            rc.fStarsCounts = stars_counts;

        //            rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //            rc.fJoinDate = restaurant.fJoinDate;

        //            rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

        //            rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

        //            TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //            rc.fAverageArrivalTime = delivery_time;
        //            TimeSpan longest_cook_food_time;
        //            if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //            {
        //                longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                rc.fAverageArrivalTime = arrvial_time;
        //            }

        //            SelectByFoodCategory_ResList.Add(rc);
        //        }

        //        //找出子類別餐廳
        //        var restaurants = from d in dc.tMemeber.ToList()
        //                          where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5 && d.fRoleID == 2
        //                          where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
        //                          select d;



        //        //FindAllChildRestaurants(restaurants, food_categoryID);


        //        return View(SelectByFoodCategory_ResList);
        //    }
        //    return View();
        //}

        public static List<RestaurantSelect> SelectByFoodCategory_ResList = new List<RestaurantSelect>();


        //遞迴方法找出子類別下的所有餐廳
        public static void FindAllChildRestaurants(IEnumerable<tMemeber> child_category_restaurants,int parentID,string current_latitude,string current_longitude)
        {

                foreach (var restaurant in child_category_restaurants)
                {

                if(restaurant.tRestaurant.Where(r=>r.tRestaurant_Foods.Where(f=>f.tCategory.fParentID==parentID).Count()>0).Count()>0)
                {
                    FoodDeliveryEntities dc = new FoodDeliveryEntities();
                    CoordinateUtility utility = new CoordinateUtility();
                    //使用者所在座標

                    double lng = Convert.ToDouble(current_longitude);//經度
                    double lat = Convert.ToDouble(current_latitude);//緯度

                    FoodDeliveryEntities db = new FoodDeliveryEntities();
                    RestaurantSelect rc = new RestaurantSelect();
                    rc.fRestaurantName = restaurant.fMemeberName;
                    rc.fMemberID = restaurant.fMemberID;
                    var foods = db.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
                    rc.fRestaurant_Foods = foods;

                    var resId = db.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
                    rc.fRestaurantID = resId;
                    var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                    rc.fLikeCounts = like_counts;
                    var myfav_counts = db.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                    rc.fMyFavCounts = myfav_counts;
                    var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                    if (stars_counts != "")
                    {
                        var sc = Convert.ToDouble(stars_counts);
                        rc.fStarsCounts = Math.Round(sc, 1);
                    }
                    else
                    {
                        rc.fStarsCounts = 0;
                    }
                    rc.fAveragePricePerGuest = db.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                    rc.fJoinDate = restaurant.fJoinDate;

                    rc.fOrderCounts = db.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

                    rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

                    TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                    rc.fAverageArrivalTime = delivery_time;
                    TimeSpan longest_cook_food_time;
                    if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                    {
                        longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                        TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                        rc.fAverageArrivalTime = arrvial_time;
                    }


                    var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                    rc.fRestaurant_Categories = categories;

                    SelectByFoodCategory_ResList.Add(rc);

                    foreach(var categoryID in db.tCategory.Where(c=>c.fParentID==parentID).Select(c=>c.fCategoryID))
                    {
                        FindAllChildRestaurants(child_category_restaurants, categoryID, current_latitude, current_longitude);
                    }
                }

            }
            
        }



        //從搜尋列傳過來或搜尋餐廳名、菜色名
        //[HttpPost]
        //public ActionResult Index(string current_address, string current_latitude,string current_longitude,string select_res_string,string select_res_low_price, string select_res_high_price)
        //{

        //    ViewBag.current_address = current_address;//跳轉頁面後保留餐廳地址
        //    ViewBag.select_res_string = select_res_string;//跳轉頁面後保留餐廳或菜色名搜尋字串
        //    ViewBag.select_res_low_price = select_res_low_price;//跳轉頁面後保留最低價
        //    ViewBag.select_res_high_price = select_res_high_price;//跳轉頁面後保留最高價


        //    //從搜尋列跳轉過來的話，地址、經度、緯度都存在cookies
        //    Response.Cookies["current_address"].Value = current_address;

            
        //    if(current_latitude==null&& current_longitude==null)
        //    {
        //        current_latitude = Request.Cookies["current_latitude"].Value;
        //        current_longitude = Request.Cookies["current_longitude"].Value;
        //    }

            
        //    if (Request.Cookies["current_latitude"] == null && Request.Cookies["current_longitude"] == null)
        //    {
        //        Response.Cookies["current_latitude"].Value = current_latitude;
        //        current_latitude = Request.Cookies["current_latitude"].Value;
        //        Response.Cookies["current_longitude"].Value = current_longitude;
        //        current_longitude = Request.Cookies["current_longitude"].Value;
        //    }





        //    //if ((current_latitude == null && current_longitude == null))
        //    //{
        //    //     latitude = Request.Cookies["current_latitude"].Value;
        //    //     longitude = Request.Cookies["current_longitude"].Value;

        //    //}

        //    //Response.Cookies["current_latitude"].Value = current_latitude;
        //    //Response.Cookies["current_longitude"].Value = current_longitude;

        //    //latitude = Request.Cookies["current_latitude"].Value;
        //    //longitude = Request.Cookies["current_longitude"].Value;

        //    //使用者所在座標
        //    double lng = Convert.ToDouble(current_longitude);//經度
        //    double lat = Convert.ToDouble(current_latitude);//緯度
        //    CoordinateUtility utility = new CoordinateUtility();
        //    //var dis = utility.distance(120.30144, 22.62728, lng, lat);
        //    //ViewBag.message = dis;


        //    //使用餐廳名或菜色名搜尋走這
        //    if (select_res_string != null&& select_res_string!="")
        //    {
        //        var restaurants1 = from d in dc.tMemeber.ToList()
        //                           where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5&&d.fRoleID==2//餐廳經緯度在tMemeber
        //                           //餐廳名用會員名在tMember表                                  
        //                           where d.fMemeberName.Contains(select_res_string)|| dc.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(select_res_string)).Count() > 0).Count() > 0
        //                           select d;



        //        List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
        //        foreach (tMemeber restaurant in restaurants1)
        //        {
        //            RestaurantSelect rc = new RestaurantSelect();
        //            rc.fRestaurantName = restaurant.fMemeberName;
        //            rc.fMemberID = restaurant.fMemberID;
        //            var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
        //            rc.fRestaurant_Foods = foods;

        //            var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
        //            var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //            rc.fCommentCounts = comment_counts;
        //            var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //            rc.fMyFavCounts = myfav_counts;
        //            var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //            rc.fStarsCounts = stars_counts;

        //            rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //            rc.fJoinDate = restaurant.fJoinDate;

        //            rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

        //            rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

        //            TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //            rc.fAverageArrivalTime = delivery_time;
        //            TimeSpan longest_cook_food_time;
        //            if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //            {
        //                longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                rc.fAverageArrivalTime = arrvial_time;
        //            }

        //            rcs1.Add(rc);
        //        }


        //        return View(rcs1);
        //    }

        //   //使用價錢範圍搜尋走這
        //    if ((select_res_low_price != null && select_res_low_price != "") && (select_res_high_price != null && select_res_high_price != ""))
        //    {
        //        int low_price = Convert.ToInt32(select_res_low_price);
        //        int high_price = Convert.ToInt32(select_res_high_price);
        //        var restaurants1 = from d in dc.tMemeber.ToList()
        //                           where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5 && d.fRoleID == 2
        //                           where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
        //                           where d.tRestaurant.Where(r=>r.fAveragePricePerGuest>=low_price &&r.fAveragePricePerGuest<=high_price).Count()>0
        //                           select d;


        //        List<RestaurantSelect> rcs2 = new List<RestaurantSelect>();
        //        foreach (tMemeber restaurant in restaurants1)
        //        {
        //            RestaurantSelect rc = new RestaurantSelect();
        //            rc.fRestaurantName = restaurant.fMemeberName;
        //            rc.fMemberID = restaurant.fMemberID;
        //            var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
        //            rc.fRestaurant_Foods = foods;

        //            var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
        //            var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //            rc.fCommentCounts = comment_counts;
        //            var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //            rc.fMyFavCounts = myfav_counts;
        //            var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //            rc.fStarsCounts = stars_counts;

        //            rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //            rc.fJoinDate = restaurant.fJoinDate;

        //            rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

        //            rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

        //            TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //            rc.fAverageArrivalTime = delivery_time;
        //            TimeSpan longest_cook_food_time;
        //            if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
        //            {
        //                longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //                TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
        //                rc.fAverageArrivalTime = arrvial_time;
        //            }

        //            rcs2.Add(rc);
        //        }

        //        return View(rcs2);
        //    }

        //    //從搜尋列載入走下面
        //    var restaurants = from d in dc.tMemeber.ToList()
        //                      where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5&&d.fRoleID==2
        //                      where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
        //                      select d;

        //    List<RestaurantSelect> rcs = new List<RestaurantSelect>();
        //    foreach (tMemeber restaurant in restaurants)
        //    {
        //        RestaurantSelect rc = new RestaurantSelect();
        //        rc.fRestaurantName = restaurant.fMemeberName;
        //        rc.fMemberID = restaurant.fMemberID;
        //        var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
        //        rc.fRestaurant_Foods = foods;

        //        var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
        //        var comment_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Count();
        //        rc.fCommentCounts = comment_counts;
        //        var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
        //        rc.fMyFavCounts = myfav_counts;
        //        var stars_counts = dc.tComment.Where(c => c.fRestaurantID == resId).Select(c => c.fStars).Average();
        //        rc.fStarsCounts = stars_counts;

        //        rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
        //        rc.fJoinDate = restaurant.fJoinDate;

        //        rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

        //        rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

        //        TimeSpan  delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
        //        rc.fAverageArrivalTime = delivery_time;
        //        TimeSpan longest_cook_food_time;
        //        if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count()>0)
        //        {
        //            longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
        //            TimeSpan arrvial_time= delivery_time.Add(longest_cook_food_time);
        //            rc.fAverageArrivalTime = arrvial_time;
        //        }
                
        //        rcs.Add(rc);
        //    }

        //    return View(rcs);

        //}


        //叫用餐廳分類的partial view
        public ActionResult RestaurantCategorySelect()
        {
            return PartialView(dc.tRestaurant_Category);
        }

        //叫用菜色分類的partial view
        public ActionResult CateogrySelect()
        {
            FoodCategorySelect food_category = new FoodCategorySelect();
            //找出父類別
            food_category.parent_food_category = dc.tCategory.Where(c => c.fParentID == null).Select(c => c);
            
            //找出子類別
            food_category.child_food_cateogry = dc.tCategory.Where(c => c.fParentID!=null).Select(c => c);

            return PartialView(food_category);
        }


        //搜尋餐廳入口導覽列
        public ActionResult LocationSearchBar()
        {
            return PartialView();
        }

        //叫用搜尋畫面
        //[HttpGet]
        public ActionResult LocationSearch()
        {
            //tMember_City res_city = new tMember_City();
            //res_city=FindRestaurantCity.
            //ViewBag.datas = dc.tMember_City;
            return View();
        }


        //購物車頁面，讀不到經緯度的cookies，跳出的定位畫面
        public ActionResult PromptLocationSeaerch()
        {
            return View();
        }


        //用在要點餐還沒定位的跳出視窗
        public ActionResult CheckDistance(int restaurantID, string current_latitude,string current_longitude)
        {
            double lng = Convert.ToDouble(current_longitude);//經度
            double lat = Convert.ToDouble(current_latitude);//緯度
            CoordinateUtility utility = new CoordinateUtility();
            var res_memId = from d in dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurantID).Count() > 0).ToList()
                               where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= 5
                               select d.fMemberID;
            var resId=0;
            double distance=0 ;

            if (res_memId.Count()>0)
            {
                 resId = dc.tRestaurant.ToList().Where(r => r.fMemberID == res_memId.Single()).Select(r => r.fRestaurantID).Single();
                var mem = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == resId).Count() > 0).Select(m => m).Single();
                distance = Math.Abs(utility.distance(Convert.ToDouble(mem.fLongitude), Convert.ToDouble(mem.fLatitude), lng, lat));
            }

            resid_distance res = new resid_distance();
            res.resId = resId;
            res.distance = distance;

            return Json(res);
        }


        public ActionResult GetDistance(int restaurantID, string current_latitude, string current_longitude)
        {
            double lng = Convert.ToDouble(current_longitude);//經度
            double lat = Convert.ToDouble(current_latitude);//緯度
            CoordinateUtility utility = new CoordinateUtility();
            var resId = 0;
            double distance = 0;

            var mem = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurantID).Count() > 0).Select(m => m).Single();
            distance = Math.Abs(utility.distance(Convert.ToDouble(mem.fLongitude), Convert.ToDouble(mem.fLatitude), lng, lat));

            resid_distance res = new resid_distance();
            res.resId = restaurantID;
            res.distance = distance;

            return Json(res);
        }


        public class resid_distance
        {
            public int resId { get; set; }
            public double distance { get; set; }
        }


        public ActionResult PromptLocationSearchBar()
        {
            return PartialView();
        }


        //傳回附近餐廳資料給google map呈現
        public ActionResult FindLocationNearByRestaurants(string current_latitude,string current_longitude, string search_range)
        {
          
            int search_range_ = Convert.ToInt32(search_range);


            double lng = Convert.ToDouble(current_longitude);//經度
            double lat = Convert.ToDouble(current_latitude);//緯度
            CoordinateUtility utility = new CoordinateUtility();
            var restaurants1 = from d in dc.tMemeber.ToList()
                               where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= search_range_ && d.fRoleID == 2                                                                                                                                     //餐廳名用會員名在tMember表                                  
                               where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
                               select new {
                                   lat=d.fLatitude,
                                   lng=d.fLongitude,
                                   name=d.fMemeberName,
                                   tel=d.fTel,
                                   address=d.fAddress,
                                   memID=d.fMemberID,
                                   resID=d.tRestaurant.Where(r=>r.fMemberID==d.fMemberID).Select(r=>r.fRestaurantID).Single(),
                                   distance= Math.Round(Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)),1)
                               };

            return Json(restaurants1,JsonRequestBehavior.AllowGet);
        }

        //在google map搜尋餐廳
        public ActionResult SearchResInMap(string res_name,string current_latitude,string current_longitude,string search_range)
        {

            int search_range_ = Convert.ToInt32(search_range);

            double lng = Convert.ToDouble(current_longitude);//經度
            double lat = Convert.ToDouble(current_latitude);//緯度
            CoordinateUtility utility = new CoordinateUtility();
            var restaurants1 = from d in dc.tMemeber.ToList()
                               where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= search_range_ && d.fRoleID == 2                                                                                                                                     //餐廳名用會員名在tMember表                                  
                               where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0
                               where d.fMemeberName.Contains(res_name)
                               select new
                               {
                                   lat = d.fLatitude,
                                   lng = d.fLongitude,
                                   name = d.fMemeberName,
                                   tel = d.fTel,
                                   address = d.fAddress,
                                   memID = d.fMemberID,
                                   resID = d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Select(r => r.fRestaurantID).Single(),
                                   distance = Math.Round(Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)), 1)
                               };

            return Json(restaurants1);
        }


        [HttpPost]
        public ActionResult Index2(string current_address)
        {
            //ViewBag.current_address = Request.Cookies["current_address"];//跳轉頁面後保留餐廳地址

            ViewBag.current_address = current_address;
            return View();
        }

        
        //倒出餐廳搜尋結果
        public ActionResult Restaurants_Select_Outcome(string restaurant_name,string low_price,string high_price,string res_categoryID,string food_categoryID)
        {
            
            string current_latitude = Request.Cookies["current_latitude"].Value;
            string current_longitude = Request.Cookies["current_longitude"].Value;
            int search_range = Convert.ToInt32(Request.Cookies["search_range"].Value);

            //使用者所在座標
            double lng = Convert.ToDouble(current_longitude);//經度
            double lat = Convert.ToDouble(current_latitude);//緯度
            CoordinateUtility utility = new CoordinateUtility();

            //從搜尋列載入走下面
            var restaurants = from d in dc.tMemeber.ToList()
                              where Math.Abs(utility.distance(Convert.ToDouble(d.fLongitude), Convert.ToDouble(d.fLatitude), lng, lat)) <= search_range && d.fRoleID == 2
                              where d.tRestaurant.Where(r => r.fMemberID == d.fMemberID).Count() > 0//有開店的餐廳會員
                              select d;


            //依照開店日期排序
            if (Request.Cookies["orderby"] != null)
            {
                if(Request.Cookies["orderby"].Value=="opendate")
                {
                    restaurants = restaurants.OrderByDescending(m => m.fJoinDate);
                }
            }

            #region 依照銷售量排序
            if (Request.Cookies["orderby"] != null)
            {
                if (Request.Cookies["orderby"].Value == "sales")
                {


                    #region 名稱、價錢、分類搜尋

                    if (restaurant_name != "")
                    {
                        if (Request.Cookies["restaurant_name"] != null)
                        {
                            Request.Cookies.Remove("restaurant_name");
                        }

                        Response.Cookies["restaurant_name"].Value = restaurant_name;
                        restaurant_name = Request.Cookies["restaurant_name"].Value;
                        //string select_res_string = Request.Cookies["select_res_string"].Value;
                        restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
                    }


                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["restaurant_name"] != null)
                        {
                            if (Request.Cookies["restaurant_name"].Value != "")
                            {
                                restaurant_name = Request.Cookies["restaurant_name"].Value;
                                restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
                            }
                        }
                    }




                    if (low_price != "" && high_price != "")
                    {
                        if (Request.Cookies["low_price"] != null && Request.Cookies["high_price"] != null)
                        {
                            Request.Cookies.Remove("low_price");
                            Request.Cookies.Remove("high_price");
                        }

                        Response.Cookies["low_price"].Value = low_price;
                        low_price = Request.Cookies["low_price"].Value;
                        Response.Cookies["high_price"].Value = high_price;
                        high_price = Request.Cookies["high_price"].Value;
                        int low = Convert.ToInt32(low_price);
                        int high = Convert.ToInt32(high_price);

                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low) && (r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
                    }



                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["low_price"] != null && Request.Cookies["high_price"] != null)
                        {
                            if (Request.Cookies["low_price"].Value != "" && Request.Cookies["high_price"].Value != "")
                            {
                                low_price = Request.Cookies["low_price"].Value;
                                high_price = Request.Cookies["high_price"].Value;
                                int low = Convert.ToInt32(low_price);
                                int high = Convert.ToInt32(high_price);
                                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low) && (r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
                            }
                        }
                    }


                    if (res_categoryID != "")
                    {
                        if (Request.Cookies["res_categoryID"] != null)
                        {
                            Request.Cookies.Remove("res_categoryID");
                        }
                        Response.Cookies["res_categoryID"].Value = res_categoryID;
                        res_categoryID = Request.Cookies["res_categoryID"].Value;
                        int res_cat_id = Convert.ToInt32(res_categoryID);
                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);

                    }


                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["res_categoryID"] != null)
                        {
                            if (Request.Cookies["res_categoryID"].Value != "")
                            {
                                res_categoryID = Request.Cookies["res_categoryID"].Value;
                                int res_cat_id = Convert.ToInt32(res_categoryID);
                                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);
                            }
                        }
                    }

                    #endregion


                    var res_in_orders = from m in restaurants
                                        join r in dc.tRestaurant
                                         on m.fMemberID equals r.fMemberID
                                        join o in dc.tRestaurantOrders
                                        on r.fRestaurantID equals o.fRestaurantID
                                        group o by o.fRestaurantID into Group
                                        select new
                                        {
                                            Key = Group.Key,
                                            OrderCounts = Group.Count()
                                        };
                    var ordered_res = res_in_orders.OrderByDescending(g => g.OrderCounts);
                    var res_not_in_orders = from m in restaurants
                                            where m.tRestaurant.Where(r => r.tRestaurantOrders.Where(o => o.fRestaurantID == r.fRestaurantID).Count() == 0).Count() > 0
                                            select m;
                    List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
                    foreach (var restaurant in ordered_res)
                    {

                        RestaurantSelect rc = new RestaurantSelect();
                        tMemeber member = restaurants.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m).Single();
                        rc.fRestaurantName = member.fMemeberName;
                        rc.fMemberID = member.fMemberID;
                        var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == member.fMemberID).Select(f => f);
                        rc.fRestaurant_Foods = foods;

                        var resId = dc.tRestaurant.Where(r => r.fMemberID == member.fMemberID).Select(r => r.fRestaurantID).Single();
                        rc.fRestaurantID = resId;
                        var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                        rc.fLikeCounts = like_counts;
                        var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                        rc.fMyFavCounts = myfav_counts;
                        var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                        if (stars_counts != "")
                        {
                            var sc = Convert.ToDouble(stars_counts);
                            rc.fStarsCounts = Math.Round(sc, 1);
                        }
                        else
                        {
                            rc.fStarsCounts = 0;
                        }
                        rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                        rc.fJoinDate = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fJoinDate).Single();

                        rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fRestaurantID == restaurant.Key).Select(o => o).Count();

                        var Longitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLongitude).Single();
                        var Latitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLatitude).Single();
                        rc.fDistance = Math.Round((Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat))), 1);

                        TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                        rc.fAverageArrivalTime = delivery_time;
                        TimeSpan longest_cook_food_time;
                        if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                        {
                            longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                            TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                            rc.fAverageArrivalTime = arrvial_time;
                        }


                        var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                        rc.fRestaurant_Categories = categories;

                        rcs1.Add(rc);
                    }


                    foreach (tMemeber restaurant in res_not_in_orders)
                    {
                        RestaurantSelect rc = new RestaurantSelect();
                        rc.fRestaurantName = restaurant.fMemeberName;
                        rc.fMemberID = restaurant.fMemberID;
                        var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
                        rc.fRestaurant_Foods = foods;

                        var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
                        rc.fRestaurantID = resId;
                        var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                        rc.fLikeCounts = like_counts;
                        var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                        rc.fMyFavCounts = myfav_counts;
                        var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                        if (stars_counts != "")
                        {
                            var sc = Convert.ToDouble(stars_counts);
                            rc.fStarsCounts = Math.Round(sc, 1);
                        }
                        else
                        {
                            rc.fStarsCounts = 0;
                        }
                        rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                        rc.fJoinDate = restaurant.fJoinDate;

                        rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

                        rc.fDistance = Math.Round((Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat))), 1);

                        TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                        rc.fAverageArrivalTime = delivery_time;
                        TimeSpan longest_cook_food_time;
                        if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                        {
                            longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                            TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                            rc.fAverageArrivalTime = arrvial_time;
                        }

                        var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                        rc.fRestaurant_Categories = categories;

                        rcs1.Add(rc);
                    }


                    return PartialView(rcs1);
                }
            }

            #endregion


            #region 依照收藏數排序
            if (Request.Cookies["orderby"] != null)
            {
                if (Request.Cookies["orderby"].Value == "collection")
                {


                    #region 名稱、價錢、分類搜尋

                    if (restaurant_name != "")
                    {
                        if (Request.Cookies["restaurant_name"] != null)
                        {
                            Request.Cookies.Remove("restaurant_name");
                        }

                        Response.Cookies["restaurant_name"].Value = restaurant_name;
                        restaurant_name = Request.Cookies["restaurant_name"].Value;
                        //string select_res_string = Request.Cookies["select_res_string"].Value;
                        restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
                    }


                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["restaurant_name"] != null)
                        {
                            if (Request.Cookies["restaurant_name"].Value != "")
                            {
                                restaurant_name = Request.Cookies["restaurant_name"].Value;
                                restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
                            }
                        }
                    }




                    if (low_price != "" && high_price != "")
                    {
                        if (Request.Cookies["low_price"] != null && Request.Cookies["high_price"] != null)
                        {
                            Request.Cookies.Remove("low_price");
                            Request.Cookies.Remove("high_price");
                        }

                        Response.Cookies["low_price"].Value = low_price;
                        low_price = Request.Cookies["low_price"].Value;
                        Response.Cookies["high_price"].Value = high_price;
                        high_price = Request.Cookies["high_price"].Value;
                        int low = Convert.ToInt32(low_price);
                        int high = Convert.ToInt32(high_price);

                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low) && (r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
                    }



                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["low_price"] != null && Request.Cookies["high_price"] != null)
                        {
                            if (Request.Cookies["low_price"].Value != "" && Request.Cookies["high_price"].Value != "")
                            {
                                low_price = Request.Cookies["low_price"].Value;
                                high_price = Request.Cookies["high_price"].Value;
                                int low = Convert.ToInt32(low_price);
                                int high = Convert.ToInt32(high_price);
                                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low) && (r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
                            }
                        }
                    }


                    if (res_categoryID != "")
                    {
                        if (Request.Cookies["res_categoryID"] != null)
                        {
                            Request.Cookies.Remove("res_categoryID");
                        }
                        Response.Cookies["res_categoryID"].Value = res_categoryID;
                        res_categoryID = Request.Cookies["res_categoryID"].Value;
                        int res_cat_id = Convert.ToInt32(res_categoryID);
                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);

                    }


                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["res_categoryID"] != null)
                        {
                            if (Request.Cookies["res_categoryID"].Value != "")
                            {
                                res_categoryID = Request.Cookies["res_categoryID"].Value;
                                int res_cat_id = Convert.ToInt32(res_categoryID);
                                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);
                            }
                        }
                    }

                    #endregion

                    var res_in_myfav = from m in restaurants
                                       join r in dc.tRestaurant
                                       on m.fMemberID equals r.fMemberID
                                       join f in dc.tMyFavorite
                                       on r.fRestaurantID equals f.fRestaurantID
                                       group f by f.fRestaurantID into Group
                                       select new
                                       {
                                           Key = Group.Key,
                                           Count = Group.Count(),
                                       };
                    var ordered_res = res_in_myfav.OrderByDescending(g => g.Count);


                    var res_not_in_myfav = from m in restaurants
                                           where m.tRestaurant.Where(r => r.tMyFavorite.Where(f => f.fRestaurantID == r.fRestaurantID).Count() == 0).Count() > 0
                                           select m;

                    List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
                    foreach (var restaurant in ordered_res)
                    {

                        RestaurantSelect rc = new RestaurantSelect();
                        tMemeber member = restaurants.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m).Single();
                        rc.fRestaurantName = member.fMemeberName;
                        rc.fMemberID = member.fMemberID;
                        var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == member.fMemberID).Select(f => f);
                        rc.fRestaurant_Foods = foods;

                        var resId = dc.tRestaurant.Where(r => r.fMemberID == member.fMemberID).Select(r => r.fRestaurantID).Single();
                        rc.fRestaurantID = resId;
                        var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                        rc.fLikeCounts = like_counts;
                        var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                        rc.fMyFavCounts = myfav_counts;
                        var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                        if (stars_counts != "")
                        {
                            var sc = Convert.ToDouble(stars_counts);
                            rc.fStarsCounts = Math.Round(sc, 1);
                        }
                        else
                        {
                            rc.fStarsCounts = 0;
                        }
                        rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                        rc.fJoinDate = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fJoinDate).Single();

                        rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fRestaurantID == restaurant.Key).Select(o => o).Count();


                        var Longitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLongitude).Single();
                        var Latitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLatitude).Single();
                        rc.fDistance = Math.Round((Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat))), 1);

                        TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                        rc.fAverageArrivalTime = delivery_time;
                        TimeSpan longest_cook_food_time;
                        if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                        {
                            longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                            TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                            rc.fAverageArrivalTime = arrvial_time;
                        }

                        var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                        rc.fRestaurant_Categories = categories;

                        rcs1.Add(rc);
                    }


                    foreach (tMemeber restaurant in res_not_in_myfav)
                    {
                        RestaurantSelect rc = new RestaurantSelect();
                        rc.fRestaurantName = restaurant.fMemeberName;
                        rc.fMemberID = restaurant.fMemberID;
                        var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
                        rc.fRestaurant_Foods = foods;

                        var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
                        rc.fRestaurantID = resId;
                        var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                        rc.fLikeCounts = like_counts;
                        var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                        rc.fMyFavCounts = myfav_counts;
                        var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                        if (stars_counts != "")
                        {
                            var sc = Convert.ToDouble(stars_counts);
                            rc.fStarsCounts = Math.Round(sc, 1);
                        }
                        else
                        {
                            rc.fStarsCounts = 0;
                        }
                        rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                        rc.fJoinDate = restaurant.fJoinDate;

                        rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

                        rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

                        TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                        rc.fAverageArrivalTime = delivery_time;
                        TimeSpan longest_cook_food_time;
                        if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                        {
                            longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                            TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                            rc.fAverageArrivalTime = arrvial_time;
                        }

                        var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                        rc.fRestaurant_Categories = categories;

                        rcs1.Add(rc);
                    }


                    return PartialView(rcs1);
                }
            }

            #endregion

        
            #region 依照訂單星等平均排序
            if (Request.Cookies["orderby"] != null)
            {
                if (Request.Cookies["orderby"].Value == "stars")
                {


                    #region 名稱、價錢、分類搜尋

                    if (restaurant_name != "")
                    {
                        if (Request.Cookies["restaurant_name"] != null)
                        {
                            Request.Cookies.Remove("restaurant_name");
                        }

                        Response.Cookies["restaurant_name"].Value = restaurant_name;
                        restaurant_name = Request.Cookies["restaurant_name"].Value;
                        //string select_res_string = Request.Cookies["select_res_string"].Value;
                        restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
                    }


                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["restaurant_name"] != null)
                        {
                            if (Request.Cookies["restaurant_name"].Value != "")
                            {
                                restaurant_name = Request.Cookies["restaurant_name"].Value;
                                restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
                            }
                        }
                    }




                    if (low_price != "" && high_price != "")
                    {
                        if (Request.Cookies["low_price"] != null && Request.Cookies["high_price"] != null)
                        {
                            Request.Cookies.Remove("low_price");
                            Request.Cookies.Remove("high_price");
                        }

                        Response.Cookies["low_price"].Value = low_price;
                        low_price = Request.Cookies["low_price"].Value;
                        Response.Cookies["high_price"].Value = high_price;
                        high_price = Request.Cookies["high_price"].Value;
                        int low = Convert.ToInt32(low_price);
                        int high = Convert.ToInt32(high_price);

                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low) && (r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
                    }



                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["low_price"] != null && Request.Cookies["high_price"] != null)
                        {
                            if (Request.Cookies["low_price"].Value != "" && Request.Cookies["high_price"].Value != "")
                            {
                                low_price = Request.Cookies["low_price"].Value;
                                high_price = Request.Cookies["high_price"].Value;
                                int low = Convert.ToInt32(low_price);
                                int high = Convert.ToInt32(high_price);
                                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low) && (r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
                            }
                        }
                    }


                    if (res_categoryID != "")
                    {
                        if (Request.Cookies["res_categoryID"] != null)
                        {
                            Request.Cookies.Remove("res_categoryID");
                        }
                        Response.Cookies["res_categoryID"].Value = res_categoryID;
                        res_categoryID = Request.Cookies["res_categoryID"].Value;
                        int res_cat_id = Convert.ToInt32(res_categoryID);
                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);

                    }


                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["res_categoryID"] != null)
                        {
                            if (Request.Cookies["res_categoryID"].Value != "")
                            {
                                res_categoryID = Request.Cookies["res_categoryID"].Value;
                                int res_cat_id = Convert.ToInt32(res_categoryID);
                                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);
                            }
                        }
                    }

                    #endregion   

                    var res_not_in_orders = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurantOrders.Where(o => o.fRestaurantID == r.fRestaurantID).Count() == 0).Count() > 0).Select(m => m);
                    var res_in_orders = from m in restaurants
                                        join r in dc.tRestaurant
                                         on m.fMemberID equals r.fMemberID
                                         join o in dc.tRestaurantOrders
                                         on r.fRestaurantID equals o.fRestaurantID
                                         group o by o.fRestaurantID into Group
                                         select new
                                         {
                                             Key = Group.Key,
                                             AvgStar = Group.Average(g => g.fStar)
                                         };
                    var ordered_res = res_in_orders.OrderByDescending(g => g.AvgStar);



                    List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
                    foreach (var restaurant in ordered_res)
                    {
                        RestaurantSelect rc = new RestaurantSelect();
                        tMemeber member = restaurants.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m).Single();
                        rc.fRestaurantName = member.fMemeberName;
                        rc.fMemberID = member.fMemberID;
                        var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == member.fMemberID).Select(f => f);
                        rc.fRestaurant_Foods = foods;

                        var resId = dc.tRestaurant.Where(r => r.fMemberID == member.fMemberID).Select(r => r.fRestaurantID).Single();
                        rc.fRestaurantID = resId;
                        var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                        rc.fLikeCounts = like_counts;
                        var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                        rc.fMyFavCounts = myfav_counts;
                        var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                        if (stars_counts != "")
                        {
                            var sc = Convert.ToDouble(stars_counts);
                            rc.fStarsCounts = Math.Round(sc, 1);
                        }
                        else
                        {
                            rc.fStarsCounts = 0;
                        }
                        rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                        rc.fJoinDate = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fJoinDate).Single();

                        rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fRestaurantID == restaurant.Key).Select(o => o).Count();


                        var Longitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLongitude).Single();
                        var Latitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLatitude).Single();
                        rc.fDistance = Math.Round((Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat))), 1);

                        TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                        rc.fAverageArrivalTime = delivery_time;
                        TimeSpan longest_cook_food_time;
                        if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                        {
                            longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                            TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                            rc.fAverageArrivalTime = arrvial_time;
                        }

                        var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                        rc.fRestaurant_Categories = categories;

                        rcs1.Add(rc);
                    }


                    foreach (tMemeber restaurant in res_not_in_orders)
                    {
                        RestaurantSelect rc = new RestaurantSelect();
                        rc.fRestaurantName = restaurant.fMemeberName;
                        rc.fMemberID = restaurant.fMemberID;
                        var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
                        rc.fRestaurant_Foods = foods;

                        var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
                        rc.fRestaurantID = resId;
                        var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                        rc.fLikeCounts = like_counts;
                        var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                        rc.fMyFavCounts = myfav_counts;
                        var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                        if(stars_counts!="")
                        {
                            var sc = Convert.ToDouble(stars_counts);
                            rc.fStarsCounts = Math.Round(sc, 1);
                        }
                        else
                        {
                            rc.fStarsCounts = 0;
                        }


                        rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                        rc.fJoinDate = restaurant.fJoinDate;

                        rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

                        rc.fDistance = Math.Round((Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat))), 1);

                        TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                        rc.fAverageArrivalTime = delivery_time;
                        TimeSpan longest_cook_food_time;
                        if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                        {
                            longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                            TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                            rc.fAverageArrivalTime = arrvial_time;
                        }

                        var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                        rc.fRestaurant_Categories = categories;

                        rcs1.Add(rc);
                    }


                    return PartialView(rcs1);
                }
            }

            #endregion


            #region 依照按讚數排序

            if (Request.Cookies["orderby"] != null)
            {
                if (Request.Cookies["orderby"].Value == "likes")
                {
                    #region 名稱、價錢、分類搜尋

                    if (restaurant_name != "")
                    {
                        if (Request.Cookies["restaurant_name"] != null)
                        {
                            Request.Cookies.Remove("restaurant_name");
                        }

                        Response.Cookies["restaurant_name"].Value = restaurant_name;
                        restaurant_name = Request.Cookies["restaurant_name"].Value;
                        //string select_res_string = Request.Cookies["select_res_string"].Value;
                        restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
                    }


                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["restaurant_name"] != null)
                        {
                            if (Request.Cookies["restaurant_name"].Value != "")
                            {
                                restaurant_name = Request.Cookies["restaurant_name"].Value;
                                restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
                            }
                        }
                    }




                    if (low_price != "" && high_price != "")
                    {
                        if (Request.Cookies["low_price"] != null && Request.Cookies["high_price"] != null)
                        {
                            Request.Cookies.Remove("low_price");
                            Request.Cookies.Remove("high_price");
                        }

                        Response.Cookies["low_price"].Value = low_price;
                        low_price = Request.Cookies["low_price"].Value;
                        Response.Cookies["high_price"].Value = high_price;
                        high_price = Request.Cookies["high_price"].Value;
                        int low = Convert.ToInt32(low_price);
                        int high = Convert.ToInt32(high_price);

                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low) && (r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
                    }



                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["low_price"] != null && Request.Cookies["high_price"] != null)
                        {
                            if (Request.Cookies["low_price"].Value != "" && Request.Cookies["high_price"].Value != "")
                            {
                                low_price = Request.Cookies["low_price"].Value;
                                high_price = Request.Cookies["high_price"].Value;
                                int low = Convert.ToInt32(low_price);
                                int high = Convert.ToInt32(high_price);
                                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low) && (r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
                            }
                        }
                    }


                    if (res_categoryID != "")
                    {
                        if (Request.Cookies["res_categoryID"] != null)
                        {
                            Request.Cookies.Remove("res_categoryID");
                        }
                        Response.Cookies["res_categoryID"].Value = res_categoryID;
                        res_categoryID = Request.Cookies["res_categoryID"].Value;
                        int res_cat_id = Convert.ToInt32(res_categoryID);
                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);

                    }


                    if (Request.Cookies["adv_search"].Value == "true")
                    {

                        if (Request.Cookies["res_categoryID"] != null)
                        {
                            if (Request.Cookies["res_categoryID"].Value != "")
                            {
                                res_categoryID = Request.Cookies["res_categoryID"].Value;
                                int res_cat_id = Convert.ToInt32(res_categoryID);
                                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);
                            }
                        }
                    }

                    #endregion


                    // var res_not_in_comment = from m in restaurants
                    //where m.tRestaurant.Where(r => m.tComment.Where(c=>c.fRestaurantID==r.fRestaurantID).Count()<0).Count()>0
                    //select m;


                    var res_without_likes = restaurants.Where(m => m.tRestaurant.Where(r => r.tComment.Where(c => c.fRestaurantID == r.fRestaurantID).Where(c=>c.fLike==true).Count() == 0).Count() > 0).Select(m => m);

                    var res_in_comment_with_likes = restaurants.Where(m => m.tRestaurant.Where(r => r.tComment.Where(c => c.fRestaurantID == r.fRestaurantID).Where(c => c.fLike == true).Count() > 0).Count() > 0).Select(m => m);

                    var res_in_comment_with_likes_group = from m in res_in_comment_with_likes
                                                            join r in dc.tRestaurant.ToList()
                                                            on m.fMemberID equals r.fMemberID
                                                            join c in dc.tComment.ToList()
                                                            on r.fRestaurantID equals c.fRestaurantID
                                                            group c by c.fRestaurantID into Group
                                                            select new
                                                            {
                                                                Key = Group.Key,
                                                                Like_Counts = Group.Count(g => g.fLike==true)
                                                            };
                    var ordered_res_in_comment_with_likes_group = res_in_comment_with_likes_group.OrderByDescending(g => g.Like_Counts);
                    //var res_in_comment_without_likes = restaurants.Where(m => m.tRestaurant.Where(r => r.tComment.Where(c => c.fRestaurantID == r.fRestaurantID && (c.fLike == false || c.fLike == null)).Count() == r.tComment.Where(c => c.fRestaurantID == r.fRestaurantID).Count()).Count() > 0).Select(m => m);


                    List<RestaurantSelect> rcs3 = new List<RestaurantSelect>();
                    foreach (var restaurant in ordered_res_in_comment_with_likes_group)
                    {
                        RestaurantSelect rc = new RestaurantSelect();
                        tMemeber member = restaurants.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m).Single();
                        rc.fRestaurantName = member.fMemeberName;
                        rc.fMemberID = member.fMemberID;
                        var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == member.fMemberID).Select(f => f);
                        rc.fRestaurant_Foods = foods;

                        var resId = dc.tRestaurant.Where(r => r.fMemberID == member.fMemberID).Select(r => r.fRestaurantID).Single();
                        rc.fRestaurantID = resId;
                        var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                        rc.fLikeCounts = like_counts;
                        var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                        rc.fMyFavCounts = myfav_counts;
                        var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                        if (stars_counts != "")
                        {
                            var sc = Convert.ToDouble(stars_counts);
                            rc.fStarsCounts = Math.Round(sc, 1);
                        }
                        else
                        {
                            rc.fStarsCounts = 0;
                        }

                        rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                        rc.fJoinDate = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fJoinDate).Single();

                        rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fRestaurantID == restaurant.Key).Select(o => o).Count();


                        var Longitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLongitude).Single();
                        var Latitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLatitude).Single();
                        //rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat)));
                        rc.fDistance = Math.Round((Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat))), 1);

                        TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                        rc.fAverageArrivalTime = delivery_time;
                        TimeSpan longest_cook_food_time;
                        if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                        {
                            longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                            TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                            rc.fAverageArrivalTime = arrvial_time;
                        }

                        var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                        rc.fRestaurant_Categories = categories;

                        rcs3.Add(rc);
                    }
                    foreach (tMemeber restaurant in res_without_likes)
                    {
                        RestaurantSelect rc = new RestaurantSelect();
                        rc.fRestaurantName = restaurant.fMemeberName;
                        rc.fMemberID = restaurant.fMemberID;
                        var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
                        rc.fRestaurant_Foods = foods;

                        var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
                        rc.fRestaurantID = resId;
                        var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                        rc.fLikeCounts = like_counts;
                        var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                        rc.fMyFavCounts = myfav_counts;
                        var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                        if (stars_counts != "")
                        {
                            var sc = Convert.ToDouble(stars_counts);
                            rc.fStarsCounts = Math.Round(sc, 1);
                        }
                        else
                        {
                            rc.fStarsCounts = 0;
                        }
                        rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                        rc.fJoinDate = restaurant.fJoinDate;

                        rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

                        rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

                        TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                        rc.fAverageArrivalTime = delivery_time;
                        TimeSpan longest_cook_food_time;
                        if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                        {
                            longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                            TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                            rc.fAverageArrivalTime = arrvial_time;
                        }

                        var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                        rc.fRestaurant_Categories = categories;

                        rcs3.Add(rc);
                    }



                    return PartialView(rcs3);







                    //var res_in_comment_without_likes = restaurants.Where(m => m.tRestaurant.Where(r => dc.tComment.Where(c => (c.fLike == false||c.fLike==null ).Count()>0).Select(m => m);

                    //var re_in_comment_without_likes = restaurants.Except(res_not_in_comment).Except(res_in_comment_likes).Select(m=>m);
                    //var res_in_comment_with_likes = from m in restaurants
                    //                     join r in dc.tRestaurant
                    //                     on m.fMemberID equals r.fMemberID
                    //                     join c in dc.tComment.Where(c=>c.fLike != false && c.fLike != null).ToList()
                    //                     on r.fRestaurantID equals c.fRestaurantID
                    //                     group c by c.fRestaurantID into Group
                    //                     select new
                    //                     {
                    //                         Key = Group.Key,
                    //                         Like_Counts = Group.Count()
                    //                     };
                    //var ordered_res_with_likes = res_in_comment_with_likes.OrderByDescending(g => g.Like_Counts);


                    //var res_in_comment_without_likes = from m in restaurants
                    //                                join r in dc.tRestaurant
                    //                                on m.fMemberID equals r.fMemberID
                    //                                join c in dc.tComment.Where(c => c.fLike !=true).ToList()
                    //                                on r.fRestaurantID equals c.fRestaurantID
                    //                                group c by c.fRestaurantID into Group
                    //                                select new
                    //                                {
                    //                                    Key = Group.Key,
                    //                                    Like_Counts = Group.Count()
                    //                                };
                    //var ordered_res_without_likes = res_in_comment_with_likes.OrderByDescending(g => g.Like_Counts);


                    //List<RestaurantSelect> rcs1 = new List<RestaurantSelect>();
                    //foreach (var restaurant in ordered_res_with_likes)
                    //{
                    //    RestaurantSelect rc = new RestaurantSelect();
                    //    tMemeber member = restaurants.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m).Single();
                    //    rc.fRestaurantName = member.fMemeberName;
                    //    rc.fMemberID = member.fMemberID;
                    //    var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == member.fMemberID).Select(f => f);
                    //    rc.fRestaurant_Foods = foods;

                    //    var resId = dc.tRestaurant.Where(r => r.fMemberID == member.fMemberID).Select(r => r.fRestaurantID).Single();
                    //    rc.fRestaurantID = resId;
                    //    var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId&&c.fLike==true).Count();
                    //    rc.fLikeCounts = like_counts;
                    //    var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                    //    rc.fMyFavCounts = myfav_counts;
                    //    var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                    //    if (stars_counts != "")
                    //    {
                    //        var sc = Convert.ToDouble(stars_counts);
                    //        rc.fStarsCounts = Math.Round(sc, 1);
                    //    }

                    //    rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                    //    rc.fJoinDate = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fJoinDate).Single();

                    //    rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fRestaurantID == restaurant.Key).Select(o => o).Count();


                    //    var Longitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLongitude).Single();
                    //    var Latitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLatitude).Single();
                    //    rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat)));

                    //    TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                    //    rc.fAverageArrivalTime = delivery_time;
                    //    TimeSpan longest_cook_food_time;
                    //    if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                    //    {
                    //        longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                    //        TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                    //        rc.fAverageArrivalTime = arrvial_time;
                    //    }

                    //    var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                    //    rc.fRestaurant_Categories = categories;

                    //    rcs1.Add(rc);
                    //}

                    //foreach (var restaurant in ordered_res_without_likes)
                    //{
                    //    RestaurantSelect rc = new RestaurantSelect();
                    //    tMemeber member = restaurants.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m).Single();
                    //    rc.fRestaurantName = member.fMemeberName;
                    //    rc.fMemberID = member.fMemberID;
                    //    var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == member.fMemberID).Select(f => f);
                    //    rc.fRestaurant_Foods = foods;

                    //    var resId = dc.tRestaurant.Where(r => r.fMemberID == member.fMemberID).Select(r => r.fRestaurantID).Single();
                    //    rc.fRestaurantID = resId;
                    //    var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                    //    rc.fLikeCounts = like_counts;
                    //    var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                    //    rc.fMyFavCounts = myfav_counts;
                    //    var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                    //    if (stars_counts != "")
                    //    {
                    //        var sc = Convert.ToDouble(stars_counts);
                    //        rc.fStarsCounts = Math.Round(sc, 1);
                    //    }

                    //    rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                    //    rc.fJoinDate = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fJoinDate).Single();

                    //    rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fRestaurantID == restaurant.Key).Select(o => o).Count();


                    //    var Longitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLongitude).Single();
                    //    var Latitude = dc.tMemeber.Where(m => m.tRestaurant.Where(r => r.fRestaurantID == restaurant.Key).Count() > 0).Select(m => m.fLatitude).Single();
                    //    rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(Longitude), Convert.ToDouble(Latitude), lng, lat)));

                    //    TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                    //    rc.fAverageArrivalTime = delivery_time;
                    //    TimeSpan longest_cook_food_time;
                    //    if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                    //    {
                    //        longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                    //        TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                    //        rc.fAverageArrivalTime = arrvial_time;
                    //    }

                    //    var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                    //    rc.fRestaurant_Categories = categories;

                    //    rcs1.Add(rc);
                    //}


                    //foreach (tMemeber restaurant in res_not_in_comment)
                    //{
                    //    RestaurantSelect rc = new RestaurantSelect();
                    //    rc.fRestaurantName = restaurant.fMemeberName;
                    //    rc.fMemberID = restaurant.fMemberID;
                    //    var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
                    //    rc.fRestaurant_Foods = foods;

                    //    var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
                    //    rc.fRestaurantID = resId;
                    //    var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                    //    rc.fLikeCounts = like_counts;
                    //    var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                    //    rc.fMyFavCounts = myfav_counts;
                    //    var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                    //    if (stars_counts != "")
                    //    {
                    //        var sc = Convert.ToDouble(stars_counts);
                    //        rc.fStarsCounts = Math.Round(sc, 1);
                    //    }



                    //    rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                    //    rc.fJoinDate = restaurant.fJoinDate;

                    //    rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

                    //    rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

                    //    TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                    //    rc.fAverageArrivalTime = delivery_time;
                    //    TimeSpan longest_cook_food_time;
                    //    if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                    //    {
                    //        longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                    //        TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                    //        rc.fAverageArrivalTime = arrvial_time;
                    //    }

                    //    var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                    //    rc.fRestaurant_Categories = categories;

                    //    rcs1.Add(rc);
                    //}


                    //return PartialView(rcs1);
                }
            }

            #endregion


            #region 名稱、價錢、分類搜尋

            if (restaurant_name != "")
            {
                if (Request.Cookies["restaurant_name"] != null)
                {
                    Request.Cookies.Remove("restaurant_name");
                }
                    
                Response.Cookies["restaurant_name"].Value = restaurant_name;
                restaurant_name = Request.Cookies["restaurant_name"].Value;
                //string select_res_string = Request.Cookies["select_res_string"].Value;
                restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
            }


            if (Request.Cookies["adv_search"].Value == "true")
            {

                if (Request.Cookies["restaurant_name"] != null)
                {
                    if (Request.Cookies["restaurant_name"].Value != "")
                    {
                        restaurant_name = Request.Cookies["restaurant_name"].Value;
                        restaurants = restaurants.Where(m => m.fMemeberName.Contains(restaurant_name) || dc.tRestaurant.Where(r => r.fMemberID == m.fMemberID).Where(F => F.tRestaurant_Foods.Where(fs => fs.fFoodName.Contains(restaurant_name)).Count() > 0).Count() > 0).Select(r => r);
                    }
                }
            }




            if (low_price != "" && high_price != "")
            {
                if (Request.Cookies["low_price"] != null&& Request.Cookies["high_price"] != null)
                {
                    Request.Cookies.Remove("low_price");
                    Request.Cookies.Remove("high_price");
                }

                Response.Cookies["low_price"].Value = low_price;
                low_price = Request.Cookies["low_price"].Value;
                Response.Cookies["high_price"].Value = high_price;
                high_price = Request.Cookies["high_price"].Value;
                int low = Convert.ToInt32(low_price);
                int high = Convert.ToInt32(high_price);

                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low) && (r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
            }



            if (Request.Cookies["adv_search"].Value == "true")
            {

                if (Request.Cookies["low_price"] != null&& Request.Cookies["high_price"] != null)
                {
                    if (Request.Cookies["low_price"].Value != ""&& Request.Cookies["high_price"].Value != "")
                    {
                        low_price = Request.Cookies["low_price"].Value;
                        high_price = Request.Cookies["high_price"].Value;
                        int low = Convert.ToInt32(low_price);
                        int high = Convert.ToInt32(high_price);
                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => (r.fAveragePricePerGuest >= low )&&( r.fAveragePricePerGuest <= high)).Count() > 0).Select(r => r);
                    }
                }
            }


            if (res_categoryID!="")
            {
                if (Request.Cookies["res_categoryID"] != null)
                {
                    Request.Cookies.Remove("res_categoryID");
                }
                Response.Cookies["res_categoryID"].Value = res_categoryID;
                res_categoryID= Request.Cookies["res_categoryID"].Value;
                int res_cat_id = Convert.ToInt32(res_categoryID);
                restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);

            }


            if (Request.Cookies["adv_search"].Value == "true")
            {

                if (Request.Cookies["res_categoryID"] != null )
                {
                    if (Request.Cookies["res_categoryID"].Value != "" )
                    {
                        res_categoryID = Request.Cookies["res_categoryID"].Value;
                        int res_cat_id = Convert.ToInt32(res_categoryID);
                        restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Category_Details.Where(c => c.fRestaurant_CategoryID == res_cat_id).Count() > 0).Count() > 0).Select(m => m);
                    }
                }
            }









            IEnumerable<tMemeber> parent_category_restaurants;




            if (food_categoryID != "")
            {

                if (Request.Cookies["food_categoryID"] != null)
                {
                    Request.Cookies.Remove("food_categoryID");
                }
                Response.Cookies["food_categoryID"].Value = food_categoryID;
                food_categoryID = Request.Cookies["food_categoryID"].Value;
                int food_cat_id = Convert.ToInt32(food_categoryID);
                
                //找出父類別餐廳
                parent_category_restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Foods.Where(f => f.fCategoryID == food_cat_id).Count() > 0).Count() > 0).Select(m => m);
                SelectByFoodCategory_ResList.Clear();
                foreach (tMemeber restaurant in parent_category_restaurants)
                {
                    RestaurantSelect rc = new RestaurantSelect();
                    rc.fRestaurantName = restaurant.fMemeberName;
                    rc.fMemberID = restaurant.fMemberID;
                    var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
                    rc.fRestaurant_Foods = foods;

                    var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
                    var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                    rc.fLikeCounts = like_counts;
                    var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                    rc.fMyFavCounts = myfav_counts;
                    var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                    if (stars_counts != "")
                    {
                        var sc = Convert.ToDouble(stars_counts);
                        rc.fStarsCounts = Math.Round(sc, 1);
                    }
                    else
                    {
                        rc.fStarsCounts = 0;
                    }
                    rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                    rc.fJoinDate = restaurant.fJoinDate;

                    rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

                    rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

                    TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                    rc.fAverageArrivalTime = delivery_time;
                    TimeSpan longest_cook_food_time;
                    if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                    {
                        longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                        TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                        rc.fAverageArrivalTime = arrvial_time;
                    }

                    var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                    rc.fRestaurant_Categories = categories;

                    SelectByFoodCategory_ResList.Add(rc);
                    //restaurants.ToList().RemoveAll(m => m.fMemberID == restaurant.fMemberID);
                }

                //foreach(var restaurant in parent_category_restaurants)
                //{
                //    restaurants.ToList().Remove(restaurant);
                //}

                //找出子類別餐廳
                //用查詢語法去排除會滿足父類別條件的餐廳
                IEnumerable<tMemeber> child_category_restaurants= restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Foods.Where(f => f.fCategoryID != food_cat_id).Count()==r.tRestaurant_Foods.Count()).Count() >0).Select(m => m);
                string current_latitude2 = Request.Cookies["current_latitude"].Value;
                string current_longitude2 = Request.Cookies["current_longitude"].Value;
                FindAllChildRestaurants(child_category_restaurants, food_cat_id, current_latitude2, current_longitude2);




                return PartialView(SelectByFoodCategory_ResList);
            }



            if (Request.Cookies["adv_search"].Value == "true")
            {

                if (Request.Cookies["food_categoryID"] != null)
                {
                    if (Request.Cookies["food_categoryID"].Value != "")
                    {
                        food_categoryID = Request.Cookies["food_categoryID"].Value;
                        int food_cat_id = Convert.ToInt32(food_categoryID);

                        //找出父類別餐廳
                        parent_category_restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Foods.Where(f => f.fCategoryID == food_cat_id).Count() > 0).Count() > 0).Select(m => m);
                        SelectByFoodCategory_ResList.Clear();
                        foreach (tMemeber restaurant in parent_category_restaurants)
                        {
                            RestaurantSelect rc = new RestaurantSelect();
                            rc.fRestaurantName = restaurant.fMemeberName;
                            rc.fMemberID = restaurant.fMemberID;
                            var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
                            rc.fRestaurant_Foods = foods;

                            var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
                            var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                            rc.fLikeCounts = like_counts;
                            var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                            rc.fMyFavCounts = myfav_counts;
                            var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                            if (stars_counts != "")
                            {
                                var sc = Convert.ToDouble(stars_counts);
                                rc.fStarsCounts = Math.Round(sc, 1);
                            }
                            else
                            {
                                rc.fStarsCounts = 0;
                            }
                            rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                            rc.fJoinDate = restaurant.fJoinDate;

                            rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

                            rc.fDistance = Convert.ToInt32(Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat)));

                            TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                            rc.fAverageArrivalTime = delivery_time;
                            TimeSpan longest_cook_food_time;
                            if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                            {
                                longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                                TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                                rc.fAverageArrivalTime = arrvial_time;
                            }

                            var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                            rc.fRestaurant_Categories = categories;

                            SelectByFoodCategory_ResList.Add(rc);
                        }

                        //找出子類別餐廳
                        //用查詢語法去排除會滿足父類別條件的餐廳
                        IEnumerable<tMemeber> child_category_restaurants = restaurants.Where(m => m.tRestaurant.Where(r => r.tRestaurant_Foods.Where(f => f.fCategoryID != food_cat_id).Count() == r.tRestaurant_Foods.Count()).Count() > 0).Select(m => m);
                        string current_latitude3 = Request.Cookies["current_latitude"].Value;
                        string current_longitude3 = Request.Cookies["current_longitude"].Value;
                        FindAllChildRestaurants(child_category_restaurants, food_cat_id, current_latitude3, current_longitude3);

                        return PartialView(SelectByFoodCategory_ResList);
                    }
                }
            }


            #endregion

            List<RestaurantSelect> rcs = new List<RestaurantSelect>();
            foreach (tMemeber restaurant in restaurants)
            {
                RestaurantSelect rc = new RestaurantSelect();
                rc.fRestaurantName = restaurant.fMemeberName;
                rc.fMemberID = restaurant.fMemberID;
                var foods = dc.tRestaurant_Foods.Where(f => f.tRestaurant.fMemberID == restaurant.fMemberID).Select(f => f);
                rc.fRestaurant_Foods = foods;

                var resId = dc.tRestaurant.Where(r => r.fMemberID == restaurant.fMemberID).Select(r => r.fRestaurantID).Single();
                rc.fRestaurantID = resId;
                var like_counts = dc.tComment.Where(c => c.fRestaurantID == resId && c.fLike == true).Count();
                rc.fLikeCounts = like_counts;
                var myfav_counts = dc.tMyFavorite.Where(f => f.fRestaurantID == resId).Count();
                rc.fMyFavCounts = myfav_counts;
                var stars_counts = dc.tRestaurantOrders.Where(c => c.fRestaurantID == resId).Select(c => c.fStar).Average().ToString();
                if (stars_counts != "")
                {
                    var sc = Convert.ToDouble(stars_counts);
                    rc.fStarsCounts = Math.Round(sc, 1);
                }
                else
                {
                    rc.fStarsCounts = 0;
                }
                rc.fAveragePricePerGuest = dc.tRestaurant.Where(r => r.fRestaurantID == resId).Select(r => r.fAveragePricePerGuest).Single();
                rc.fJoinDate = restaurant.fJoinDate;

                rc.fOrderCounts = dc.tRestaurantOrders.Where(o => o.tRestaurant.fMemberID == restaurant.fMemberID).Select(o => o).Count();

                rc.fDistance = Math.Round((Math.Abs(utility.distance(Convert.ToDouble(restaurant.fLongitude), Convert.ToDouble(restaurant.fLatitude), lng, lat))),1);

                TimeSpan delivery_time = new TimeSpan(0, Convert.ToInt32(rc.fDistance * 2.7), 0);
                rc.fAverageArrivalTime = delivery_time;
                TimeSpan longest_cook_food_time;
                if (dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Count() > 0)
                {
                    longest_cook_food_time = dc.tRestaurant_Foods.Where(f => f.fRestaurantID == resId).Select(f => f.fCookTime).Max();
                    TimeSpan arrvial_time = delivery_time.Add(longest_cook_food_time);
                    rc.fAverageArrivalTime = arrvial_time;
                }

                var categories = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resId).ToList();
                rc.fRestaurant_Categories = categories;

                rcs.Add(rc);
            }

            return PartialView(rcs);
        }


        //叫用餐廳分類的partial view
        public ActionResult RestaurantCategorySelect2()
        {
            return PartialView(dc.tRestaurant_Category);
        }


        //叫用菜色分類的partial view
        public ActionResult CateogrySelect2()
        {
            FoodCategorySelect food_category = new FoodCategorySelect();
            //找出父類別
            food_category.parent_food_category = dc.tCategory.Where(c => c.fParentID == null).Select(c => c);

            //找出子類別
            food_category.child_food_cateogry = dc.tCategory.Where(c => c.fParentID != null).Select(c => c);

            return PartialView(food_category);
        }



        //public string Add_Like(string res_memID, string memID)
        //{
        //    int mid = Convert.ToInt32(memID);
        //    int rid = Convert.ToInt32(res_memID);

        //    int resid= dc.tRestaurant.Where(r => r.tMemeber.fMemberID == rid).Select(r => r.fRestaurantID).Single();
            
        //    if(dc.tComment.Where(c => c.fMemberID == mid && c.fRestaurantID == resid).Count()>0)
        //    {

        //    if(dc.tComment.Where(c => c.fMemberID == mid && c.fRestaurantID == resid).Select(c=>c.fLike).Single()==true)
        //    {
        //        tComment tc = dc.tComment.Where(c => c.fMemberID == mid && c.fRestaurantID == resid).Select(c => c).Single();
        //        tc.fLike = false;
        //        dc.Entry(tc).State = System.Data.Entity.EntityState.Modified;
        //        dc.SaveChanges();
        //        return "收回讚";
        //    }

        //    if (dc.tComment.Where(c => c.fMemberID == mid && c.fRestaurantID == resid).Select(c => c.fLike).Single() == false)
        //    {
        //        tComment tc = dc.tComment.Where(c => c.fMemberID == mid && c.fRestaurantID == resid).Select(c => c).Single();
        //        tc.fLike = true;
        //        dc.Entry(tc).State = System.Data.Entity.EntityState.Modified;
        //        dc.SaveChanges();
        //        return "已按讚";
        //    }

        //    }


        //    tComment add_like = new tComment();
        //    add_like.fMemberID = mid;
        //    add_like.fRestaurantID = resid;
        //    add_like.fLike = true;

        //    dc.tComment.Add(add_like);
        //    dc.SaveChanges();
        //    return "已按讚";

        //}
    }
}