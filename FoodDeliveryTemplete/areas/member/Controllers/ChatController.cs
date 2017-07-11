using FoodDeliveryTemplete.Areas.Member.Models;
using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Member.Controllers
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
            ChatRepository _chatRepository = new ChatRepository();
            int MemberID = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            int Restaurant = id;
            ChatRoomViewModel v = new ChatRoomViewModel();          
            return PartialView("PartialChatList", _chatRepository.GetAllMessages(MemberID, Restaurant));
        }

        public ActionResult GetName(int id = 15)
        {
            string name = db.tRestaurant.Where(c => c.fRestaurantID == id).Select(c => c.fRestaurantName).FirstOrDefault().ToString();
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
            int id = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            ChatReadRepository _chatRepository = new ChatReadRepository();

            return PartialView("PartialUnReadMsg", _chatRepository.GetAllMessages(id));
        }

        public ActionResult SendMesseages(int resid,string content)
        {
            int memberid = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            string resname = db.tRestaurant.Where(c => c.fRestaurantID == resid).Select(c => c.fRestaurantName).FirstOrDefault();
            string memberName = db.tMemeber.Where(c => c.fMemberID == memberid).Select(c => c.fMemeberName).FirstOrDefault();
            ChatRoom cr = new ChatRoom();
            cr.MemberName = memberName;
            cr.fFrom = true;
            cr.Read = false;
            cr.RestaurantName = resname;
            cr.Date = DateTime.Now;
            cr.Message = content;
            cr.MemberID = memberid;
            cr.RestaurantID = resid;
            db.ChatRoom.Add(cr);
            db.SaveChanges();
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadMessage(int id)
        {
            int memberid = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            foreach (var item in db.ChatRoom.Where(c => c.MemberID == memberid & c.RestaurantID == id & c.fFrom==false))
            {
                item.Read = true;
            }
            db.SaveChanges();
            return Content("success");
        }
    }
}