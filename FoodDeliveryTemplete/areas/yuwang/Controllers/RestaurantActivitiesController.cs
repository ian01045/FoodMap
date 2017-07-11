using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.yuwang.Controllers
{
    public class RestaurantActivitiesController : Controller
    {
        FoodDeliveryEntities dc = new FoodDeliveryEntities();
        
        // GET: yuwang/RestaurantActivities
        public ActionResult Index(int resID)
        {
            ViewBag.resid = resID;

            return View();
        }

        public ActionResult InvolvedActivities_Manage(int resID)
        {
            ViewBag.resid = resID;
            ViewBag.res_categories = dc.tRestaurant_Category.ToList();
            var res_activities = dc.tRestaurant_Category_Details.Where(d => d.fRestaurantID == resID).Select(d => d).ToList();
            return PartialView(res_activities);
        }

        [HttpPost]
        public string Delete_Involved_Activity(int fID)
        {
            tRestaurant_Category_Details delete_act = dc.tRestaurant_Category_Details.Find(fID);
            dc.tRestaurant_Category_Details.Remove(delete_act);
            dc.SaveChanges();
            return "已刪除";
        }


        [HttpPost]
        public string Add_Activity(int actID,int resID)
        {
            if (dc.tRestaurant_Category_Details.Where(d=>d.fRestaurantID==resID&&d.fRestaurant_CategoryID==actID).Count()==0)
            {
                tRestaurant_Category_Details add_act = new tRestaurant_Category_Details();
                add_act.fRestaurantID = resID;
                add_act.fRestaurant_CategoryID = actID;
                dc.tRestaurant_Category_Details.Add(add_act);
                dc.SaveChanges();
                return "參與活動成功";
            }
                return "您已參與活動";
        }
    }
}