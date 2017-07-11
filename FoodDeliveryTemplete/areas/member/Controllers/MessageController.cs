using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.Member.Controllers
{
    public class MessageController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMessages()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            int id = Convert.ToInt32(Request.Cookies["RestaurantBlogID"].Value);
            return PartialView("PartialMsgList", _messageRepository.GetAllMessages(id));
        }

        
        public ActionResult SendMessage(int id,string content)
        {
            int _MemberID = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            var membername = (from i in db.tMemeber
                           where i.fMemberID == _MemberID
                           select i.fMemeberName).FirstOrDefault().ToString();

          
            Messages msg = new Messages();
            msg.Message = content;
            msg.fMemberName = membername;
            msg.Date = DateTime.Now;
            msg.fRestaurantID = id;
            msg.fMemberID = _MemberID;

            db.Messages.Add(msg);
            db.SaveChanges();
            string success = "成功";
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFoodImage(int id = 0)
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