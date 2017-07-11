using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;
using System.Net.Mail;
using System.Net;

namespace FoodDeliveryTemplete.Controllers
{
    public class HomeController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: Home
        public ActionResult Index()
        {
            if (Request.Cookies["AccountLogin"] != null && Request.Cookies["RoleNum"].Value == "3")
            {
                return RedirectToAction("Index", "Home", new { Area = "Member" });
            }
            if (Request.Cookies["AccountLogin"] != null && Request.Cookies["RoleNum"].Value == "2")
            {
                return RedirectToAction("Index", "Restaurant", new { Area = "Restaurant" });
            }
            if (Request.Cookies["AccountLogin"] != null && Request.Cookies["RoleNum"].Value == "1")
            {
                return RedirectToAction("Index", "Home", new { Area = "Administrator" });
            }

            return View();
        }



      
    }
}