using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.Areas.Restaurant.Models;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class PhotoesController : Controller
    {
        private FoodDeliveryEntities db = new FoodDeliveryEntities();
        Repository<tPhoto> reposPhoto = new Repository<tPhoto> ();
        // GET: Restaurant/Photoes
        public ActionResult Index()
        {
            var tPhoto = reposPhoto.GetAllInfo();
            List<photo> photolist = new List<photo>();
            photo photo;
            foreach (var photos in tPhoto)
            {
                photo = new photo();
                photo.fPhotoID = photos.fPhotoID;
                photo.fMemberID = photos.fMemberID;
                photo.fRestaurantID = photos.fRestaurantID;
                photo.fFoodID= photos.fFoodID;
                photo.fBytesImage = photos.fBytesImage;
                photolist.Add(photo);
            }
            return View(photolist.ToList());
        }

        //page = 頁數
        //pageRows = 一頁多少筆 & 取多少筆
        int pageRows = 10;
        //會員照片
        public ActionResult MemberPhotos(int page)
        {
            //pagecut = 頁數
            //pageRows = 一頁多少筆 & 取多少筆
            var count =  reposPhoto.GetAllInfo().Where(c=>c.fMemberID!=null);
            float pagenumber = count.Count();
            ViewBag.productcount = Convert.ToInt32(Math.Ceiling(pagenumber / 10));//計算分頁按鈕數量


            var product = reposPhoto.GetAllInfo().Where(c => c.fMemberID != null)
                                                 .Skip((page - 1) * pageRows).Take(pageRows);//抓前十個
            return PartialView(product.ToList());
        }
        //餐廳照片
        public ActionResult RestaurantPhotos(int page)
        {
            //pagecut = 頁數
            //pageRows = 一頁多少筆 & 取多少筆
            var count = reposPhoto.GetAllInfo().Where(c => c.fRestaurantID != null);
            float pagenumber = count.Count();
            ViewBag.productcount = Convert.ToInt32(Math.Ceiling(pagenumber / 10));//計算分頁按鈕數量
            
            var product = reposPhoto.GetAllInfo().Where(c => c.fRestaurantID != null)
                                                 .Skip((page - 1) * pageRows).Take(pageRows);//抓前十個
            return PartialView(product.ToList());
        }
        //產品照片
        public ActionResult FoodPhotos(int page)
        {
            //pagecut = 頁數
            //pageRows = 一頁多少筆 & 取多少筆
            var count = reposPhoto.GetAllInfo().Where(c => c.fFoodID != null);
            float pagenumber = count.Count();
            ViewBag.productcount = Convert.ToInt32(Math.Ceiling(pagenumber / 10));//計算分頁按鈕數量


            var product = reposPhoto.GetAllInfo().Where(c => c.fFoodID != null)
                                                 .Skip((page - 1) * pageRows).Take(pageRows);//抓前十個
            return PartialView(product.ToList());
        }
        //不屬於誰的照片
        public ActionResult OnlyPhotos(int page)
        {
            var count = reposPhoto.GetAllInfo().Where(c => c.fFoodID == null && c.fMemberID == null && c.fFoodImage == null && c.fRestaurantID == null);
            float pagenumber = count.Count();
            ViewBag.productcount = Convert.ToInt32(Math.Ceiling(pagenumber / 10));//計算分頁按鈕數量
            var product = reposPhoto.GetAllInfo().Where(c => c.fFoodID == null && c.fMemberID == null && c.fFoodImage == null && c.fRestaurantID == null)
                                                 .Skip((page - 1) * pageRows).Take(pageRows);//抓前十個
            return PartialView(product.ToList());
        }

        //新/修會員照片
        public ActionResult CUMemberPhoto(tPhoto member,string img)
        {
            int? memberid = member.fMemberID;
            var photodata = reposPhoto.GetAllInfo().Where(c => c.fMemberID == memberid).SingleOrDefault();
            if (photodata != null)
            {
                photodata.fBytesImage = Convert.FromBase64String(img);
                int findphotoid = photodata.fPhotoID;
                photodata.fPhotoID = findphotoid;
                photodata.fDateTime = DateTime.Now;
                photodata.fMemberID = member.fMemberID;
                reposPhoto.Update(photodata);
            }
            else
            {
                member.fBytesImage = Convert.FromBase64String(img);
                member.fDateTime = DateTime.Now;
                reposPhoto.Insert(member);
            }
            return RedirectToAction("Index");
        }
        //新/修餐廳照片
        public ActionResult CUResataurantPhoto(tPhoto resataurant, string img)
        {
            int? resataurantid = resataurant.fRestaurantID;
            var photodata = reposPhoto.GetAllInfo().Where(c => c.fRestaurantID == resataurantid).SingleOrDefault();
            if (photodata != null)
            {
                photodata.fBytesImage = Convert.FromBase64String(img);
                int findphotoid = photodata.fPhotoID;
                photodata.fPhotoID = findphotoid;
                photodata.fDateTime = DateTime.Now;
                photodata.fMemberID = resataurant.fMemberID;
                reposPhoto.Update(photodata);
            }
            else
            {
                resataurant.fBytesImage = Convert.FromBase64String(img);
                resataurant.fDateTime = DateTime.Now;
                reposPhoto.Insert(resataurant);
            }
            return RedirectToAction("Index");
        }
        //新/修產品照片
        public ActionResult CUFoodPhoto(tPhoto food, string img)
        {
            int? foodid = food.fFoodID;
            var photodata = reposPhoto.GetAllInfo().Where(c => c.fFoodID == foodid).SingleOrDefault();
            if (photodata != null)
            {
                photodata.fBytesImage = Convert.FromBase64String(img);
                int findphotoid = photodata.fPhotoID;
                photodata.fPhotoID = findphotoid;
                photodata.fDateTime = DateTime.Now;
                photodata.fMemberID = food.fMemberID;
                reposPhoto.Update(photodata);
            }
            else
            {
                food.fBytesImage = Convert.FromBase64String(img);
                food.fDateTime = DateTime.Now;
                reposPhoto.Insert(food);
            }
            return RedirectToAction("Index");
        }
        //純新增照片
        public ActionResult addNewPhoto(tPhoto data,string img)
        {
            var photo = reposPhoto.GetAllInfo().Where(c => c.fPhotoID == data.fPhotoID);
            if (photo.Count() != 0)
            {
                var update = reposPhoto.GetID(data.fPhotoID);
                update.fPhotoID = data.fPhotoID;
                update.fBytesImage = Convert.FromBase64String(img);
                update.fDateTime = DateTime.Now;
                reposPhoto.Update(update);
            }
            else
            {
                tPhoto photodata = new tPhoto();
                photodata.fBytesImage = Convert.FromBase64String(img);
                photodata.fDateTime = DateTime.Now;
                reposPhoto.Insert(photodata);
            }
           
            return RedirectToAction("Index");
        }
    }
}
