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

namespace FoodDeliveryTemplete.Areas.Advertisement.Controllers
{
    public class AdvertisementRotaionController : Controller
    {
        // GET: Advertisement/AdvertisementRotaion
        FoodDeliveryEntities db = new FoodDeliveryEntities();

        //回傳二進位圖
        public ActionResult GetImageByte(int id)
        {
            tAdvertisement_Article adv = db.tAdvertisement_Article.Find(id);
            byte[] img = adv.fImage;
            return File(img, "image/jpeg");
        }
        private AdvRepository<tAdvertisement_Article> repository = new AdvRepository<tAdvertisement_Article>();

        public ActionResult AdvertisementRotation()
        {
           
        //取已經發佈的廣告並播放
        var advList = db.tAdvertisement_Article.Where(a => a.fIsPublish == true).OrderBy(a => Guid.NewGuid()).ToList();
               advList = (from adv in db.tAdvertisement_Article
                       where adv.fIsPublish == true
                       where adv.fImage != null
                       orderby Guid.NewGuid()
                       select adv).ToList();

            return PartialView(advList);
    }
    }
}