using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Advertisement.Controllers
{
    public class AdvertisementController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        private FoodDeliveryTemplete.Areas.Advertisement.Models.Partials.AdvRepository<tAdvertisement> repository = new FoodDeliveryTemplete.Areas.Advertisement.Models.Partials.AdvRepository<tAdvertisement>();
        // GET: Adv/Products
        public ActionResult Index()
        {

            return View(db.tAdvertisement.ToList());
        }
        public ActionResult Nav()
        {
            return PartialView(db.tAdvertisement.ToList());
        }
        public ActionResult Browse()
        {
            return View();
        }
        public ActionResult NavJson()
        {
            return Json(db.tAdvertisement.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult PartView(int id = 1)
        {
            return PartialView(db.tAdvertisement.Where(p => p.fAdvertisementID == id));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.res = db.tRestaurant.ToList();
            ViewBag.priceregion = db.tAdvertisement_Price.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(tAdvertisement ad)
        {
            repository.Create(ad);
            //ViewBag.datas = db.tAdvertisement.ToList();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id = 0)
        {
            repository.Delete(repository.GetById(id));
            return RedirectToAction("Index");
        }

        [HttpGet]//開啟修改頁面
        public ActionResult Edit(int id = 0)
        {

            return View(repository.GetById(id));
        }//repository.GetById(id)

        [HttpPost]//傳送修改資料，以表格方式傳送
        public ActionResult Edit()
        {
            //接收表單傳過來的資料
            //Category _category = new Category();
            ViewBag.datas = db.tAdvertisement.ToList();

            tAdvertisement _tAdvertisement = new tAdvertisement();
            _tAdvertisement.fAdvertisementID = Convert.ToInt32(Request.Form["fAdvertisementID"]);
            _tAdvertisement.fPriceRegionID = Convert.ToInt32(Request.Form["fPriceRegionID"]);
            _tAdvertisement.fRestaurantID = Convert.ToInt32(Request.Form["fRestaurantID"]);
            _tAdvertisement.fAppliedDate = Convert.ToDateTime(Request.Form["fAppliedDate"]);
            _tAdvertisement.fDays = Convert.ToDateTime(Request.Form["fDays"]);
            _tAdvertisement.fMediaIntroduction = Request.Form["fMediaIntroduction"];

            //Repository
            repository.Update(_tAdvertisement);

            //轉到Index Action顯示修改完的結果
            return RedirectToAction("Index");
        }

        public ActionResult pp()
        {
            return View();
        }

    }
}