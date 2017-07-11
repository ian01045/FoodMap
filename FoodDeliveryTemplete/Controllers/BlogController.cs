using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Controllers
{
    public class BlogController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: Blog
        public ActionResult Index()
        {
           
            //Show出全部的部落格
            return View();
        }

        public ActionResult BlogShow()
        {
            ClassRestaurantandComment rc = new ClassRestaurantandComment();
            //最一開始載入頁面時會載入全部的restaurant
            return PartialView(rc.ShowRestandComment());
        }

        public ActionResult BlogSearch(string sortby = null)
        {
            //部落格搜尋，依據回傳的值，會有不同的排序
            ClassRestaurantandComment rc = new ClassRestaurantandComment();
            return PartialView(rc.Order(sortby));
        }

        public ActionResult BlogDate(string sortby = null)
        {
            if (sortby == "month")
            {
                ClassRestaurantandComment rc = new ClassRestaurantandComment();
                return PartialView(rc.MonthStar());
            }
            else if (sortby == "week")
            {
                ClassRestaurantandComment rc = new ClassRestaurantandComment();
                return PartialView(rc.WeekStar());
            }
            return PartialView("BlogShow");
        }
        
        public ActionResult GetBlogImage(int id = 0)
        {
            //抓blog搜尋的圖片
            tPhoto photo = db.tPhoto.Find(id);
            byte[] img = photo.fBytesImage;
            return File(img, "image/jpeg");
        }

        public ActionResult RestaurantBlog(int id)
        {
           
            //string fFoodID = Request.QueryString["fFoodID"];
            if (Session["fCartID"] == null)
            {
                Session["fCartID"] = Guid.NewGuid().ToString();
            }
            ViewBag.fCartID = Session["fCartID"].ToString();
            ClassRestaurantBlog bl = new ClassRestaurantBlog();
            Response.Cookies["RestaurantBlogID"].Value = id.ToString();
            return View(bl.Index(id));
        }

        public ActionResult ShowCommentandStar(int id)
        {
            ClassRestaurantBlog bl = new ClassRestaurantBlog();
            return PartialView(bl.CommentandStar(id));
        }           

        public ActionResult GetFoodImage(int id = 0)
        {
            //抓food圖片
            var photo = (from p in db.tPhoto
                         where p.fFoodID == id
                         select new
                         {
                             photoimage = p.fBytesImage
                         }).FirstOrDefault();            
            byte[] img = photo.photoimage;
            return File(img, "image/jpeg");
        }

        public ActionResult GetMemberPhoto(int id = 0)
        {
            var photo = (from p in db.tPhoto
                         where p.fMemberID == id
                         select new
                         {
                             photoimage = p.fBytesImage
                         }).FirstOrDefault();
            byte[] img = photo.photoimage;
            return File(img, "image/jpeg");
        }

      

    }
}