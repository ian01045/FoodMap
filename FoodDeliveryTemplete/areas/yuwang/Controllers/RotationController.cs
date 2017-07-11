using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.yuwang.Controllers
{
    public class RotationController : Controller
    {
        FoodDeliveryEntities dc = new FoodDeliveryEntities();

        // GET: yuwang/Rotation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InfoRotation()
        {
            //var activity = dc.tRestaurant_Category.Select(c => c);

            RotationClass rat = new RotationClass();
            rat.catogries = dc.tRestaurant_Category.ToList();
            rat.restaurants = dc.tRestaurant.ToList(); 
            return PartialView(rat);
        }

        public class RotationClass
        {
            public IEnumerable<tRestaurant_Category> catogries { get; set; }
            public IEnumerable<tRestaurant> restaurants { get; set; }
        }

        public ActionResult InfoRotation_Food()
        {
            //var activity = dc.tRestaurant_Category.Select(c => c);

            RotationClass_Food rat = new RotationClass_Food();
            rat.foods = dc.tRestaurant_Foods.OrderBy(f=>f.tRestaurantOrder_Details.Count).Take(5);
            rat.restaurants = dc.tRestaurant.ToList();
            return PartialView(rat);
        }

        public class RotationClass_Food
        {
            public IEnumerable<tRestaurant_Foods> foods { get; set; }
            public IEnumerable<tRestaurant> restaurants { get; set; }
        }




    }
}