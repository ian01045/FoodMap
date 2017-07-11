using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class HomeController : Controller
    {
        // GET: Restaurant/Home
        public ActionResult Index()
        {
            return View();         
        }

        public ActionResult AccountLogout()
        {
            //AccountCondition用來判斷Cookie裡的Account是否與帳號一致
            //判斷帳號是否有登入，且須跟資料庫裡的帳號一致
            Response.Cookies["AccountLogin"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["RoleNum"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["MemberID"].Expires = DateTime.Now.AddSeconds(-1);
            return RedirectToAction("Index", "Home", new { Area = "" });

        }
    }
}