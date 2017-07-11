using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModels;
using FoodDeliveryTemplete.ViewModel;
using FoodDeliveryTemplete.Areas.Member.Models;

namespace FoodDeliveryTemplete.Areas.Member.Controllers
{
    public class MemberAreaController : Controller
    {
        private FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: Member/MemberArea
        public ActionResult Index()
        {
            return View();
        }
        //---會員的資料

        public ActionResult userprofile()
        {
            int id;
            MemberPhotoViewModel info = new MemberPhotoViewModel();
            id = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            tPhoto photo = db.tPhoto.Where(p => p.fMemberID == id).SingleOrDefault();

            info.fMemberID = photo.tMemeber.fMemberID;
            info.fMemeberName = photo.tMemeber.fMemeberName;
            info.fEmail = photo.tMemeber.fEmail;
            info.fPassword = photo.tMemeber.fPassword;
            info.fGender = photo.tMemeber.fGender;
            info.fBirth = photo.tMemeber.fBirth.Date;
            info.fPhone = photo.tMemeber.fPhone;
            info.fTel = photo.tMemeber.fTel;
            info.fAddress = photo.tMemeber.fAddress;
            info.fAreaID = photo.tMemeber.tMember_Area.fAreaID;
            info.fAreaName = photo.tMemeber.tMember_Area.fAreaName;
            info.fCityID = photo.tMemeber.tMember_Area.tMember_City.fCityID;
            info.fCityName = photo.tMemeber.tMember_Area.tMember_City.fCityName;
            //讓下拉式選單有東西
            ViewBag.datas = db.tMember_City.ToList();
            ViewBag.area = db.tMember_Area.Where(c => c.fCityID == photo.tMemeber.tMember_Area.tMember_City.fCityID).ToList();

            return View(info);
        }

        //----------會員的修改表單
        [HttpGet]
        public ActionResult MemberModifyForm(int id)
        {
            MemberPhotoViewModel info = new MemberPhotoViewModel();

            tPhoto photo = db.tPhoto.Where(p => p.fMemberID == id).Single();

            info.fMemberID = photo.tMemeber.fMemberID;
            info.fMemeberName = photo.tMemeber.fMemeberName;
            info.fEmail = photo.tMemeber.fEmail;
            info.fPassword = photo.tMemeber.fPassword;
            info.fGender = photo.tMemeber.fGender;
            info.fBirth = photo.tMemeber.fBirth;
            info.fPhone = photo.tMemeber.fPhone;
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
        public ActionResult MemberModifyForm(int id, MemberPhotoViewModel info, HttpPostedFileBase memberPhoto)
        {
            tMemeber update = db.tMemeber.Find(id);

            update.fMemeberName = info.fMemeberName;
            update.fEmail = info.fEmail;
            update.fPassword = info.fPassword;
            update.fPasswordConfirm = info.fPassword;
            update.fGender = info.fGender;
            update.fPhone = info.fPhone;
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

            return RedirectToAction("userprofile", "MemberArea");
        }

        //回傳二進位圖
        public ActionResult GetImageByte(int id)
        {
            tPhoto photo = db.tPhoto.Where(p => p.fMemberID == id).Single();
            byte[] img = photo.fBytesImage;
            return File(img, "image/jpeg");
        }
       
        //電子錢包
        [HttpGet]
        public ActionResult MemberWallet( )
        {
            int id = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            if (db.tMemeber.Where(m => m.fMemberID == id).Select(m => m.fCreditAccount).Single() != null)
            {
                MemberWalletViewModel mw = new MemberWalletViewModel();

                tMemeber member = db.tMemeber.Find(id);
                mw.fMemberID = member.fMemberID;
                mw.fMemeberName = member.fMemeberName;
                mw.fCreditAccount = member.fCreditAccount;
                mw.fWalletTotalMoney = member.fWalletTotalMoney;

                return View(mw);
            }
            else
            {
                return RedirectToAction("AccountNotOpen", "MemberArea", new { id = id });
            }

        }

        [HttpPost]
        public ActionResult MemberWallet(MemberWalletViewModel mw)
        {
            var balance = db.tMemeber.Where(m => m.fMemberID == mw.fMemberID).Select(b => b.fWalletTotalMoney).Single();

            tMember_Wallat newWallet = new tMember_Wallat();
            newWallet.fMemberID = mw.fMemberID;
            newWallet.fDeposit = mw.fDeposit;
            newWallet.ModifiedDate = DateTime.Now;
            newWallet.fWallat_Surplus = Convert.ToDecimal(balance) + Convert.ToDecimal(mw.fDeposit);

            db.tMember_Wallat.Add(newWallet);
            db.SaveChanges();

            tMemeber update = db.tMemeber.Find(mw.fMemberID);
            update.fWalletTotalMoney = (Convert.ToDecimal(balance) + Convert.ToDecimal(mw.fDeposit)).ToString();

            db.Entry(update).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("MemberWallet", "MemberArea");
        }

        public ActionResult AccountNotOpen(int id)
        {
            tMemeber member = db.tMemeber.Find(id);


            return View(member);
        }
        [HttpGet]
        public ActionResult OpenAccount(int id)
        {
            tMemeber member = db.tMemeber.Find(id);
            ViewBag.memberName = member.fMemeberName;
            ViewBag.memberId = member.fMemberID;
            return View();
        }
        [HttpPost]
        public ActionResult OpenAccount(OpenAccountViewModel oa)
        {
            if (ModelState.IsValid)
            {
                tMemeber update = db.tMemeber.Find(oa.fMemberID);
                update.fCreditAccount = oa.fCreditAccount;
                update.fWalletTotalMoney = "0";

                db.Entry(update).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("MemberWallet", "MemberArea",new {Area="Member" });
            }

            tMemeber member = db.tMemeber.Find(oa.fMemberID);
            ViewBag.memberName = member.fMemeberName;
            ViewBag.memberId = member.fMemberID;
            return View();
        }

        //不會重跑:我的最愛
        public ActionResult MyFavouriteRestaurant()
        {

            return View();
        }
        //餐廳的分類
        public ActionResult Category(/*int id = 1041*/)
        {
            var id = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            MyFavoriteRestaurantCategoryViewModel vm = new MyFavoriteRestaurantCategoryViewModel();
            vm.category = db.tMyFavoriteCategory.Where(f => f.tMyFavorite.Where(m => m.fMemberID == id && m.fFavCategoryID != null).Count() > 0).ToList();

            List<RestaurantCategory> items = new List<RestaurantCategory>();

            foreach (var category in vm.category)
            {
                RestaurantCategory rc = new RestaurantCategory();
                rc.CategoryId = category.fFavCategoryID;
                rc.CategoryName = category.fFavCategoryName;
                //算出這個人喜歡的類別有幾間餐廳
                rc.count = category.tMyFavorite.Where(f => f.fMemberID == id).Count();

                items.Add(rc);

            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }
        //餐廳partialView
        public ActionResult Restaurant(int categoryId = 0)
        {
            var id = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            MyFavoriteRestaurantCategoryViewModel vm = new MyFavoriteRestaurantCategoryViewModel();
            vm.myfavoriteRestaurant = db.tMyFavorite.Where(f => f.fMemberID == id && f.fFavCategoryID == categoryId);
            return PartialView(vm);
        }
       //餐廳地圖
        public ActionResult RestaurantMap(int Memberid)
        {
            var member = db.tMemeber.Find(Memberid);

            double lat = Convert.ToDouble(member.fLatitude);
            double lng = Convert.ToDouble(member.fLongitude);

            var restaurant = db.tRestaurant.Where(r => r.fMemberID == Memberid).SingleOrDefault();

            ViewBag.RestaurantName = restaurant.fRestaurantName;
            ViewBag.price = restaurant.fAveragePricePerGuest;
            ViewBag.lat = lat;
            ViewBag.lng = lng;
            return PartialView();
        }
        [HttpPost]
        //取消我的最愛
        public ActionResult deleteFav(int memberid , int restid)
        {
            var favoriteId = db.tMyFavorite.Where(f => f.fMemberID == memberid && f.fRestaurantID == restid).Select(f=>f.fID).SingleOrDefault();

            tMyFavorite delete = db.tMyFavorite.Find(favoriteId);

            db.tMyFavorite.Remove(delete);
            db.SaveChanges();

            return RedirectToAction("MyFavouriteRestaurant", "MemberArea");
        }
    }
}