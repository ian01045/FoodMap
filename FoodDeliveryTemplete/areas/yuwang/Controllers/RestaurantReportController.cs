using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.Areas.yuwang.Models;

namespace FoodDeliveryTemplete.Areas.yuwang.Controllers
{
    public class RestaurantReportController : Controller
    {
        public FoodDeliveryEntities dc = new FoodDeliveryEntities();

        // GET: yuwang/RestaurantReport
        public ActionResult Index(int resID)
        {
            ViewBag.resID = resID;
            return View();
        }


        int under30 = 0;
        int up30under40 = 0;
        int up40under50 = 0;
        int up50under60 = 0;
        int up60 = 0;
        //年齡分組方法
        //private age_group_ordercounts Age_Group(int memeberid)
        //{
        //    var birth = dc.tMemeber.Where(m => m.fMemberID == memeberid).Select(m => m.fBirth).Single();
        //    var age = (DateTime.Now.AddYears(-birth.Year)).Year;
        //    age_group_ordercounts age_group = new age_group_ordercounts();

        //    if (age <= 30)
        //    {
        //        age_group.group_name = "年輕客群(30歲以下)";
        //        age_group.group_orders_counts = under30;
        //        return age_group;
        //    }

        //    else if (age <= 40)
        //    {
        //        age_group.group_name = "中壯年客群(30~40歲)";
        //        age_group.group_orders_counts = up30under40;
        //        return age_group;
        //    }
        //    else if (age <= 50)
        //    {
        //        age_group.group_name = "中年客群(40~50歲)";
        //        age_group.group_orders_counts = up40under50;
        //        return age_group;
        //    }
        //    else if (age <= 60)
        //    {
        //        age_group.group_name = "中老年客群(50~60歲)";
        //        age_group.group_orders_counts = up50under60;
        //        return age_group;
        //    }
        //    else
        //    {
        //        age_group.group_name = "老年客群";
        //        age_group.group_orders_counts = up60;
        //        return age_group;
        //    }
        //}
        public class age_group_ordercounts
        {
            public string name { get; set; }
            public int y { get; set; }
        }
        //計算各年齡層銷售量
        [HttpPost]
        public ActionResult Get_customers_member_age_pie_chart(int restaurantID)
        {
            var members_has_orders = from o in dc.tRestaurantOrders.Where(o => o.fRestaurantID == restaurantID).ToList()
                                     group o by o.fMemberID into Group
                                     select new
                                     {
                                         MemberID = Group.Key,
                                         Order_Counts = Group.Count()
                                     };

            foreach(var mem in members_has_orders)
            {
               var mem_birth= dc.tMemeber.Where(m => m.fMemberID == mem.MemberID).Select(m => m.fBirth).Single();
               var mem_age = (DateTime.Now.AddYears(-mem_birth.Year)).Year;

                if (mem_age <= 30)
                {
                    under30 += mem.Order_Counts;
                    break;
                }

                else if (mem_age <= 40)
                {
                    up30under40 += mem.Order_Counts;
                    break ;
                }
                else if (mem_age <= 50)
                {
                    up40under50 += mem.Order_Counts;
                    break;
                }
                else if (mem_age <= 60)
                {
                    up50under60 += mem.Order_Counts;
                    break;
                }
                else
                {
                    up60 += mem.Order_Counts;
                    break;
                }

            }

            List<age_group_ordercounts> datas = new List<age_group_ordercounts>();
            age_group_ordercounts d1 = new age_group_ordercounts(); d1.name = "年輕客群(30歲以下)";d1.y = under30;
            age_group_ordercounts d2 = new age_group_ordercounts(); d2.name = "中壯年客群(30~40歲)"; d2.y = up30under40;
            age_group_ordercounts d3 = new age_group_ordercounts(); d3.name = "中年客群(40~50歲)"; d3.y = up40under50;
            age_group_ordercounts d4 = new age_group_ordercounts(); d4.name = "中老年客群(50~60歲)"; d4.y = up50under60;
            age_group_ordercounts d5 = new age_group_ordercounts(); d5.name = "老年客群"; d5.y = up60;
            datas.Add(d1); datas.Add(d2); datas.Add(d3); datas.Add(d4); datas.Add(d5);


            //foreach (var mem in members_has_orders)
            //{
            //    var birth = dc.tMemeber.Where(m => m.fMemberID == mem.MemberID).Select(m => m.fBirth).Single();
            //    var age = (DateTime.Now.AddYears(-birth.Year)).Year;

            //    if (age < 30)
            //    {
            //        under30 += mem.Order_Counts;
            //        break;
            //    }
            //    else if (age < 40)
            //    {
            //        up30under40 += mem.Order_Counts;
            //        break;
            //    }
            //    else if (age < 50)
            //    {
            //        up40under50 += mem.Order_Counts;
            //        break;
            //    }
            //    else if (age < 60)
            //    {
            //        up50under60 += mem.Order_Counts;
            //        break;
            //    }
            //    else
            //    {
            //        up60 += mem.Order_Counts;
            //        break;
            //    }
            //}


            //var member_age_range_has_orders = from m1 in members_has_orders.ToList()
            //                                  group m1 by Age_Group(m1.MemberID) into g
            //                                  select new
            //                                  {
            //                                      Group_Name = g.Key.group_name,
            //                                      Group_OrderCounts = g.Key.group_orders_counts
            //                                  };

            //List<ResAreasReport> mem_age_group_out = new List<ResAreasReport>();
            //foreach (var age_group in member_age_range_has_orders)
            //{
            //    ResAreasReport rap = new ResAreasReport();
            //    rap.name = age_group.Group_Name;
            //    rap.y = age_group.Group_OrderCounts;
            //    mem_age_group_out.Add(rap);
            //}

            return Json(datas);
        }

        [HttpPost]
        public ActionResult Get_Orders_On_Time_Portion(int restaurantID)
        {

            ReportData data = new ReportData();

            List<int> res_on_time_orders = new List<int>();
            for (int i = 1; i <= 4; i++)
            {
                if (dc.tRestaurantOrders.Where(o=>o.fRestaurantID==restaurantID).Where(o=>o.fIsDelay==false||o.fIsDelay==null).Where(o=>o.fOrderDatetime.Month==i).Count()>0)
                {
                    //decimal city_res_sales_per_month = dc.tRestaurantOrders.Where(r => r.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 1).Where(o => o.fOrderDatetime.Month == i).Select(o =>o.tRestaurantOrder_Details;
                    int res_orders_on_time_per_month = dc.tRestaurantOrders.Where(o => o.fRestaurantID == restaurantID).Where(o => o.fIsDelay == false || o.fIsDelay == null).Where(o => o.fOrderDatetime.Month == i).Select(o=>o).Count();
                    res_on_time_orders.Add(res_orders_on_time_per_month);
                }
                else
                {
                    res_on_time_orders.Add(0);
                }
            }
            List<int> res_off_time_orders = new List<int>();
            for (int i = 1; i <= 4; i++)
            {
                if (dc.tRestaurantOrders.Where(o => o.fRestaurantID == restaurantID).Where(o => o.fIsDelay ==true).Where(o => o.fOrderDatetime.Month == i).Count() > 0)
                {
                    int res_orders_off_time_per_month = dc.tRestaurantOrders.Where(o => o.fRestaurantID == restaurantID).Where(o => o.fIsDelay == true).Where(o => o.fOrderDatetime.Month == i).Select(o => o).Count();
                    res_off_time_orders.Add(res_orders_off_time_per_month);
                }
                else
                {
                    res_off_time_orders.Add(0);
                }
            }

            data.res_on_time_oders = res_on_time_orders;
            data.res_off_time_oders = res_off_time_orders;
            return Json(data);
        }


        [HttpPost]
        public ActionResult Get_Hot_Sales_Foods_IsMeal(int restaurantID)
        {

            ReportData data = new ReportData();

           // List<decimal> money = new List<decimal>();
            //List<string> name = new List<string>();

            var hot_sales_foods = from f in dc.tRestaurant_Foods.Where(f => f.tRestaurant.fRestaurantID == restaurantID).Where(f=>f.fIsMeal==true).ToList()
                                  join d in dc.tRestaurantOrder_Details
                                  on f.fFoodID equals d.fFoodID
                                  group d by d.fFoodID into g
                                  orderby g.Count() descending
                                  select new
                                  {
                                      FoodID = g.Key,
                                      Food_OrderCount = g.Count()

                                  };

            List<string> hot_slaes_food_name = new List<string>();
            List<int> hot_slaes_count = new List<int>();
            foreach(var food in hot_sales_foods)
            {
                hot_slaes_count.Add(food.Food_OrderCount);

                var name = dc.tRestaurant_Foods.Where(f => f.fFoodID == food.FoodID).Select(f => f.fFoodName).Single();
                hot_slaes_food_name.Add(name);
            }

            data.hot_slaes_foods_name = hot_slaes_food_name.Take(5);
            data.hot_sales_foods_order_count = hot_slaes_count.Take(5);
            
            return Json(data);
        }

        [HttpPost]
        public ActionResult Get_Hot_Sales_Foods_IsNotMeal(int restaurantID)
        {

            ReportData data = new ReportData();

            // List<decimal> money = new List<decimal>();
            //List<string> name = new List<string>();

            var hot_sales_foods = from f in dc.tRestaurant_Foods.Where(f => f.tRestaurant.fRestaurantID == restaurantID).Where(f=>f.fIsMeal==false).ToList()
                                  join d in dc.tRestaurantOrder_Details
                                  on f.fFoodID equals d.fFoodID
                                  group d by d.fFoodID into g
                                  orderby g.Count() descending
                                  select new
                                  {
                                      FoodID = g.Key,
                                      Food_OrderCount = g.Count()

                                  };

            List<string> hot_slaes_food_name = new List<string>();
            List<int> hot_slaes_count = new List<int>();
            foreach (var food in hot_sales_foods)
            {
                hot_slaes_count.Add(food.Food_OrderCount);

                var name = dc.tRestaurant_Foods.Where(f => f.fFoodID == food.FoodID).Select(f => f.fFoodName).Single();
                hot_slaes_food_name.Add(name);
            }

            data.hot_slaes_foods_name = hot_slaes_food_name.Take(5);
            data.hot_sales_foods_order_count = hot_slaes_count.Take(5);

            return Json(data);
        }
    }
}

