using FoodDeliveryTemplete.Areas.Member.Models;
using FoodDeliveryTemplete.Areas.Restaurant.Models;
using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class ChatController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: Member/Chat
        public ActionResult Index()
        {
            //若點sidebar重新載入並傳RestaurantID過來
            return View();
        }

        public ActionResult GetMessages(int id = 15)
        {
           
            RestaurantChatRepository _chatRepository = new RestaurantChatRepository();
            int MemberID = id;
            int Restaurant = Convert.ToInt32(Request.Cookies["RestaurantID"].Value); 
           
            return PartialView("PartialChatList", _chatRepository.GetAllMessages(MemberID, Restaurant));
        }

        public ActionResult GetName(int id = 15)
        {
            string name = db.tMemeber.Where(c => c.fMemberID == id).Select(c => c.fMemeberName).FirstOrDefault().ToString();
            return Content(name);
        }

        public ActionResult GetMemberImage(int id = 0)
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

        public ActionResult GetRestaurantImage(int id = 0)
        {
            var photo = (from p in db.tPhoto
                         where p.fRestaurantID == id
                         select new
                         {
                             photoimage = p.fBytesImage
                         }).FirstOrDefault();
            byte[] img = photo.photoimage;
            return File(img, "image/jpeg");
        }

        public ActionResult GetUnReadMessages()
        {
            int id = Convert.ToInt32(Request.Cookies["RestaurantID"].Value);
            RestaurantReadRepository _chatRepository = new RestaurantReadRepository();

            return PartialView("PartialUnReadMsg", _chatRepository.GetAllMessages(id));
        }

        public ActionResult SendMesseages(int id = 0,string content = "")
        {
            int restID = Convert.ToInt32(Request.Cookies["RestaurantID"].Value);
            string memberName = db.tMemeber.Where(c => c.fMemberID == id).Select(c => c.fMemeberName).FirstOrDefault();
            string resname = db.tRestaurant.Where(c => c.fRestaurantID == restID).Select(c => c.fRestaurantName).FirstOrDefault();
            ChatRoom cr = new ChatRoom();
            cr.MemberName = memberName;
            cr.fFrom = false;
            cr.Read = false;
            cr.RestaurantName = resname;
            cr.Date = DateTime.Now;
            cr.Message = content;
            cr.MemberID = id;
            cr.RestaurantID = restID;
            db.ChatRoom.Add(cr);
            db.SaveChanges();
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadMessage(int id)
        {
            int restID = Convert.ToInt32(Request.Cookies["RestaurantID"].Value);
            foreach (var item in db.ChatRoom.Where(c => c.MemberID == id & c.RestaurantID == restID & c.fFrom==true))
            {
                item.Read = true;
            }
            db.SaveChanges();
            return Content("success");
        }
    }
}