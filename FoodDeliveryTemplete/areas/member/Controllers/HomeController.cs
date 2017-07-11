using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: Member/Home
        public ActionResult Index()
        {           
            //AccountCondition用來判斷Cookie裡的Account是否與帳號一致
            //判斷帳號是否有登入，且須跟資料庫裡的帳號一致
            if (Request.Cookies["AccountLogin"] != null && Request.Cookies["RoleNum"].Value== "3")
            {
                return View();
            }
            return RedirectToAction("Index","Home",new {Area = "" });
          
        }

        public ActionResult AccountLogout()
        {
            //AccountCondition用來判斷Cookie裡的Account是否與帳號一致
            //判斷帳號是否有登入，且須跟資料庫裡的帳號一致
            Response.Cookies["AccountLogin"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["RoleNum"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["MemberID"].Expires = DateTime.Now.AddSeconds(-1);
            return RedirectToAction("Index", "Home",new {Area = "" });

        }
    }
}