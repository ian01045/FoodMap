using FoodDeliveryTemplete.Areas.Advertisement.Models.Partials;
using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Areas.Advertisement.Models;
using PagedList;
using PagedList.Mvc;
using System.Text;

namespace FoodDeliveryTemplete.Areas.Advertisement.Controllers
{
    public class AdvertisementClientPanelController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        private AdvRepository<tAdvertisement_Article> repository = new AdvRepository<tAdvertisement_Article>();
        public ActionResult Index(int? page)
        {
            return View(db.tAdvertisement_Article.ToList().ToPagedList(page ?? 1, 20));
        }
        public ActionResult Detail(int ID)
        {
            return View(db.tAdvertisement_Article.Where(a => a.fId == ID).Single());
        }
        public ActionResult Detail2(int ID)
        {
            return View(db.tAdvertisement_Article.Where(a => a.fId == ID).Single());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.datas = db.tAdvertisement_Article.ToList();
            ViewBag.res = db.tRestaurant.ToList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        /*變數fimg名稱絕對不能跟欄位fImage相同，因為會抓到跟table同樣欄位名稱*/
        public ActionResult Create(tAdvertisement_Article ad, HttpPostedFileBase fimg)
        {
            if (fimg != null && fimg.ContentLength > 0)
            {
                //將上傳的圖轉成二進位
                var imgSize = fimg.ContentLength;
                byte[] imgByte = new byte[imgSize];
                fimg.InputStream.Read(imgByte, 0, imgSize);
                ad.fImage = imgByte;
                db.tAdvertisement_Article.Add(ad);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "請選擇圖檔!!";
            }
 //repository.Create(ad);沒有效果
            db.tAdvertisement_Article.Add(ad);
            db.SaveChanges();
            
            
            //ViewBag.datas = db.tAdvertisement.ToList();
            return RedirectToAction("Index");
        }
  
    public ActionResult Delete(int id = 0)
        {
            //repository.Delete(repository.GetById(id));
            db.tAdvertisement_Article.Remove(db.tAdvertisement_Article.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]//開啟修改頁面
        public ActionResult Edit(int id = 0)
        {
            ViewBag.res = db.tRestaurant.ToList();
            var editData = db.tAdvertisement_Article.Find(id);
            return View(editData);
        }//repository.GetById(id)

        [HttpPost]//傳送修改資料，以表格方式傳送
        [ValidateInput(false)]
        public ActionResult Edit(HttpPostedFileBase fimg)
        {
            //接收表單傳過來的資料
            ViewBag.datas = db.tAdvertisement_Article.ToList();

            var Id = Convert.ToInt32(Request.Form["fID"]);
            tAdvertisement_Article edit_article = db.tAdvertisement_Article.Find(Id);
            
            edit_article.fSubject= Request.Form["fSubject"];
            edit_article.fContentText = Request.Unvalidated.Form["fContentText"];
            //textarea存成字串

            edit_article.fCreateDate = Convert.ToDateTime(Request.Form["fCreateDate"]);
            edit_article.fUpdateDate = Convert.ToDateTime(Request.Form["fUpdateDate"]);
            edit_article.fHtml = Request.Form["fHtml"];
                //將上傳的圖轉成二進位
                var imgSize = fimg.ContentLength;
                byte[] imgByte = new byte[imgSize];
                fimg.InputStream.Read(imgByte, 0, imgSize);
                edit_article.fImage = imgByte;
                db.Entry(edit_article).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            //轉到Index Action顯示修改完的結果
            return RedirectToAction("Index");
        }


        }

       
    

       

        

    }
