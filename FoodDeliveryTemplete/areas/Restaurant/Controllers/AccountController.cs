using FoodDeliveryTemplete.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class AccountController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: Restaurant/Account
        public ActionResult Index()
        {
            return View();
        }

        //----餐廳會員的資料
        public ActionResult RestaurantProfile()
        {
            int id = 0;

            int.TryParse(Request.Cookies["MemberID"].Value, out id);

            MemberPhotoViewModel info = new MemberPhotoViewModel();

            tPhoto photo = db.tPhoto.Where(p => p.fMemberID == id).Single();

            info.fMemberID = photo.tMemeber.fMemberID;
            info.fMemeberName = photo.tMemeber.fMemeberName;
            info.fEmail = photo.tMemeber.fEmail;
            info.fPassword = photo.tMemeber.fPassword;
            info.fGender = photo.tMemeber.fGender;
            info.fBirth = photo.tMemeber.fBirth.Date;
            //info.fPhone = photo.tMemeber.fPhone;
            info.fTel = photo.tMemeber.fTel;
            info.fAddress = photo.tMemeber.fAddress;
            info.fAreaID = photo.tMemeber.tMember_Area.fAreaID;
            info.fAreaName = photo.tMemeber.tMember_Area.fAreaName;
            info.fCityID = photo.tMemeber.tMember_Area.tMember_City.fCityID;
            info.fCityName = photo.tMemeber.tMember_Area.tMember_City.fCityName;


            return View(info);
        }
        //-----餐廳會員的修改表單
        [HttpGet]
        public ActionResult RestaurantModifyForm(int id)
        {
            MemberPhotoViewModel info = new MemberPhotoViewModel();

            tPhoto photo = db.tPhoto.Where(p => p.fMemberID == id).Single();

            info.fMemberID = photo.tMemeber.fMemberID;
            info.fMemeberName = photo.tMemeber.fMemeberName;
            info.fEmail = photo.tMemeber.fEmail;
            info.fPassword = photo.tMemeber.fPassword;
            info.fBirth = photo.tMemeber.fBirth;
            //info.fPhone = photo.tMemeber.fPhone;
            info.fTel = photo.tMemeber.fTel;
            info.fAddress = photo.tMemeber.fAddress;
            info.fAreaID = photo.tMemeber.tMember_Area.fAreaID;
            info.fCityID = photo.tMemeber.tMember_Area.tMember_City.fCityID;

            //讓下拉式選單有東西
            ViewBag.datas = db.tMember_City.ToList();
            ViewBag.area = db.tMember_Area.Where(c => c.fCityID == photo.tMemeber.tMember_Area.tMember_City.fCityID).ToList();

            return View(info);
        }
        [HttpPost]
        public ActionResult RestaurantModifyForm(int id, MemberPhotoViewModel info, HttpPostedFileBase memberPhoto)
        {
            tMemeber update = db.tMemeber.Find(id);

            update.fMemeberName = info.fMemeberName;
            update.fEmail = info.fEmail;
            update.fPassword = info.fPassword;
            update.fPasswordConfirm = info.fPassword;
            //update.fPhone = info.fPhone;
            update.fTel = info.fTel;
            update.fAreaID = info.fAreaID;
            update.fAddress = info.fAddress;

            db.Entry(update).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            var photoID = db.tPhoto.Where(p => p.fMemberID == id).Select(p => p.fPhotoID).Single();
            tPhoto changePhoto = db.tPhoto.Find(photoID);
            //將上傳的圖轉成二進位

            if (memberPhoto != null)
            {
                var imgSize = memberPhoto.ContentLength;
                byte[] imgByte = new byte[imgSize];
                memberPhoto.InputStream.Read(imgByte, 0, imgSize);

                changePhoto.fBytesImage = imgByte;
                changePhoto.fDateTime = DateTime.Today;

                db.Entry(changePhoto).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }




            return RedirectToAction("RestaurantProfile", "Account");
        }
    }
}