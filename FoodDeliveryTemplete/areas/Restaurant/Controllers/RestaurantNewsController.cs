using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.ViewModels.Haoming;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class RestaurantNewsController : Controller
    {
        private FoodDeliveryEntities db = new FoodDeliveryEntities();
        
        // GET: Restaurant/RestaurantNews
        public ActionResult Index()
        {
            Response.Write(Request.Cookies["MemberID"].Value);
            HttpCookie cookie = Request.Cookies["MemberID"];
            Response.Write(cookie.Value);
            return View();
        }

        //新發布
        [HttpGet]
        public ActionResult CreateNews()
        {
            ViewBag.datas = db.tNewsType.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateNews(RestaurantNewsViewModel vm )
        {
            Response.Write(Request.Cookies["MemberID"].Value);
            HttpCookie cookie = Request.Cookies["MemberID"];
            Response.Write(cookie.Value);
            var id = Convert.ToInt32(cookie.Value);
            ViewBag.datas = db.tNewsType.ToList();

            var RestaurantId = db.tRestaurant.Where(r => r.fMemberID == id).Select(r => r.fRestaurantID).SingleOrDefault();

            if (ModelState.IsValid)
            {
                tMyFavorite_RestaurantNews create = new tMyFavorite_RestaurantNews();

                create.fRestaurantID = RestaurantId;
                create.fNewsTypeID = vm.fNewsTypeID;
                create.fNews = vm.fNews;
                create.fPublishedDate = vm.fPublishedDate.Date;
                create.fTitle = vm.fTitle;

                db.tMyFavorite_RestaurantNews.Add(create);
                db.SaveChanges();
                    
                return RedirectToAction("RestaurantBoard", "RestaurantNews");

            }


             return View();
        }
        //可編輯的
        [HttpGet]
        public ActionResult NewsModify(int id = 0)
        {

            ViewBag.datas = db.tNewsType.ToList();

            RestaurantNewsViewModel vm = new RestaurantNewsViewModel();

            //var RestaurantId = db.tRestaurant.Where(r => r.fMemberID == id).Select(r => r.fRestaurantID).SingleOrDefault();

            var news = db.tMyFavorite_RestaurantNews.Find(id);


            vm.fNewsTypeID = news.fNewsTypeID;
            vm.fPublishedDate=news.fPublishedDate.Date;
            vm.fTitle = news.fTitle;
            vm.fNews = news.fNews;
            
            return View(vm);
        }
        [HttpPost]
        public ActionResult NewsModify(RestaurantNewsViewModel vm, int id = 0)
        {
            tMyFavorite_RestaurantNews update = db.tMyFavorite_RestaurantNews.Find(id);

            update.fNewsTypeID = vm.fNewsTypeID;
            update.fPublishedDate = vm.fPublishedDate;
            update.fTitle = vm.fTitle;
            update.fNews = vm.fNews;

            db.Entry(update).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("RestaurantBoard", "RestaurantNews");

        }
        public ActionResult ShowNews(int id = 0)
        {
            var thenews = db.tMyFavorite_RestaurantNews.Find(id);

            RestaurantNewsViewModel vm = new RestaurantNewsViewModel();

            vm.fArticleID = thenews.fArticleID;
            vm.fNewsTypeID = thenews.fNewsTypeID;
            vm.fTitle = thenews.fTitle;
            vm.fNews = thenews.fNews;
            vm.fRestaurantID = thenews.fRestaurantID;
            vm.fPublishedDate = thenews.fPublishedDate;
            vm.NewsTypeName = thenews.tNewsType.fNewsTypeName;
        
            
            return View(vm);
        }

        //給勝哥的view不能修改的

        public ActionResult ShowNewsForblog(int id = 1)
        {
            var thenews = db.tMyFavorite_RestaurantNews.Find(id);

            RestaurantNewsViewModel vm = new RestaurantNewsViewModel();

            vm.fArticleID = thenews.fArticleID;
            vm.fNewsTypeID = thenews.fNewsTypeID;
            vm.fTitle = thenews.fTitle;
            vm.fNews = thenews.fNews;
            vm.fRestaurantID = thenews.fRestaurantID;
            vm.fPublishedDate = thenews.fPublishedDate;
            vm.NewsTypeName = thenews.tNewsType.fNewsTypeName;


            return View(vm);
        }

        public ActionResult RestaurantBoard()
        {
            return View();
        }

        public ActionResult NavCategory()
        {
            //var id= (int)Session["MemberID"];
            var id = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            var RestaurantId = db.tRestaurant.Where(r => r.fMemberID == id).Select(r => r.fRestaurantID).SingleOrDefault();

            NavCategoryViewModel vm = new NavCategoryViewModel();

            vm.NewsType = db.tNewsType.Where(f => f.tMyFavorite_RestaurantNews.Where(n => n.fRestaurantID == RestaurantId && n.fNewsTypeID != null).Count() > 0).ToList();

            return PartialView(vm);
        }

        //給勝哥的navpartialview
        public ActionResult NavCategoryForBlog( int id )
        {
            //var id = (int)Session["MemberID"];


            //var RestaurantId = db.tRestaurant.Where(r => r.fMemberID == id).Select(r => r.fRestaurantID).SingleOrDefault();

            NavCategoryViewModel vm = new NavCategoryViewModel();

            vm.NewsType = db.tNewsType.Where(f => f.tMyFavorite_RestaurantNews.Where(n => n.fRestaurantID == id && n.fNewsTypeID != null).Count() > 0).ToList();

            return PartialView(vm);
        }


        public ActionResult News(int categoryid = 0)
        {
            //var id = (int)Session["MemberID"];
            var id = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            var RestaurantId = db.tRestaurant.Where(r => r.fMemberID == id).Select(r => r.fRestaurantID).SingleOrDefault();
            NavCategoryViewModel vm = new NavCategoryViewModel();

            vm.RestaurantNews = db.tMyFavorite_RestaurantNews.Where(n => n.fRestaurantID == RestaurantId && n.fNewsTypeID == categoryid);

            return PartialView(vm);
        }


        //給勝哥的partialview 
        public ActionResult NewsForblog(int id, int categoryid =0)
        {

            //var id = (int)Session["MemberID"];

            

            //var RestaurantId = db.tRestaurant.Where(r => r.fMemberID == id).Select(r => r.fRestaurantID).SingleOrDefault();
            NavCategoryViewModel vm = new NavCategoryViewModel();

            vm.RestaurantNews = db.tMyFavorite_RestaurantNews.Where(n => n.fRestaurantID == id && n.fNewsTypeID == categoryid);

            return PartialView(vm);
        }



        //刪除消息
        public ActionResult DeleteNews(int id)
        {
            tMyFavorite_RestaurantNews delete = db.tMyFavorite_RestaurantNews.Find(id);

            db.tMyFavorite_RestaurantNews.Remove(delete);
            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);

        }
    }
}