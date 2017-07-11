using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Member.Controllers
{
    //Member的blog
  
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
            if (Session["fCartID"] == null)
            {
                Session["fCartID"] = Guid.NewGuid().ToString();
            }
            Response.Cookies["RestaurantBlogID"].Value = id.ToString();

            ViewBag.fCartID = Session["fCartID"].ToString();
            //ClassCookieStorage.RestaurantID = id;
            //ClassCookieStorage.MemberID = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            //ClassCookieStorage.MemberName = Request.Cookies["MemberName"].Value;
            ClassRestaurantBlog bl = new ClassRestaurantBlog();
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

        //若按過讚之後所跳轉的partialview
        public ActionResult PartialCountLike(int id = 0)
        {
            id = Convert.ToInt32(Request.Cookies["RestaurantBlogID"].Value);
            ClassRestaurantBlog rb = new ClassRestaurantBlog();   
            return PartialView(rb.CountedLike(id));
        }

        //若已將餐廳加入我的最愛則顯示此partialview
        public ActionResult PartialMyFavorite(int id = 0)
        {     
            return PartialView();
        }


        //判斷是否按過讚
        public ActionResult ConditionLike(int id =0)
        {         
            int memberid = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            ClassRestaurantBlog rb = new ClassRestaurantBlog();
            int count = rb.ConditionofLike(id, memberid);
            if (count != 0)
            {
                return PartialView("PartialCountLike", rb.CountedLike(id));
                
            }
            return PartialView(rb.CountedLike(id));

        }

        //判斷是否加過我的最愛
        public ActionResult ConditionMyFavorite(int id = 0)
        {
          
            int memberid = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            ClassRestaurantBlog rb = new ClassRestaurantBlog();
            int count = rb.ConditionofFavorite(id, memberid);
            if (count != 0)
            {
                return PartialView("PartialMyFavorite", rb.CountedLike(id));

            }
            return PartialView();

        }

        //給讚
        public ActionResult GiveLike(int id = 0)
        {
            tComment cm = new tComment();
            cm.fMemberID = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            cm.fLike = true;
           
            cm.fRestaurantID = id;
            cm.fDate = DateTime.Now;
            db.tComment.Add(cm);
            db.SaveChanges();
            return Content("成功");
        }

        public ActionResult AddMyFav(int id = 0,int content=1)
        {
            tMyFavorite myfav = new tMyFavorite();
            myfav.fMemberID = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            myfav.fRestaurantID = id;
            myfav.fFavCategoryID = content;
            db.tMyFavorite.Add(myfav);
            db.SaveChanges();
            return Content("成功");
        }

        //收回讚
        public ActionResult DeleteLike(int id = 0)
        {
            int memberid = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            var delete = (from y in db.tComment
                          where y.fMemberID == memberid & y.fRestaurantID == id & y.fLike == true
                     select y).FirstOrDefault();
            db.tComment.Remove(delete);
            db.SaveChanges();
            return Content("成功");
        }

        public ActionResult DeleteLove(int id = 0)
        {
            int memberid = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            var delete = (from y in db.tMyFavorite
                          where y.fMemberID == memberid & y.fRestaurantID == id
                          select y).FirstOrDefault();
            db.tMyFavorite.Remove(delete);
            db.SaveChanges();
            return Content("成功");
        }

        public ActionResult GiveStars(int id = 0)
        {
            //id傳RestaurantID
            ClassRestaurantBlog cr = new ClassRestaurantBlog();
           
            return PartialView("PartialGiveStars",cr.GetStarInformation(id));
        }

        public ActionResult MemberGiveStars(int id = 0,int restID=0)
        {
            string shit = "success";
            int MemberID = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            ClassRestaurantBlog cr = new ClassRestaurantBlog();
            if (cr.MemberGiveStars(MemberID, restID) == 0)
            {
                tComment tc = new tComment();
                tc.fMemberID = MemberID;
                tc.fStars = id;
                tc.fRestaurantID = restID;
                tc.fLike = false;
                tc.fDate = DateTime.Now;
                db.tComment.Add(tc);
                db.SaveChanges();
            }
            else
            {
                //修改
                tComment tc = db.tComment.FirstOrDefault(c => c.fMemberID == MemberID & c.fRestaurantID == restID & c.fStars != null);
                tc.fStars = id;
                db.Entry(tc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new {shit },JsonRequestBehavior.AllowGet);
            }

            return Json(new { shit }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MemberGiveStarsCondition(int id = 0)
        {            
            int MemberID = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            ClassRestaurantBlog cr = new ClassRestaurantBlog();
            int count = cr.MemberGiveStars(MemberID, id);
            if (count != 0)//有評價過了
            {
                return PartialView(cr.MemberStars(MemberID, id));
            }
            return PartialView("NotGiveCommentYet");
        }
    }
}