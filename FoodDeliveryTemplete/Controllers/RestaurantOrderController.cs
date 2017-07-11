using FoodDeliveryTemplete.Models;
using FoodTrain.Areas.Amy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Controllers
{
    public class RestaurantOrderController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        private Repository<tRestaurantOrders> repository = new Repository<tRestaurantOrders>();
        // GET: RestaurantOrder
        public ActionResult Index(int restaurantid = 0)
        {
            restaurantid = 10;//TODO
            var query = (from c in db.tRestaurantOrder_Details
                         join d in db.tRestaurantOrders
                         on c.fOrderID equals d.fOrderID
                         join e in db.tMemeber
                         on d.fMemberID equals e.fMemberID
                         where d.fRestaurantID == restaurantid
                         select new RestaurantOrderView
                         {
                             fOrderID = c.fOrderID,
                             fOrderDatetime = d.fOrderDatetime,
                             fMemeberName = d.fMemberID.ToString(),
                             fPhone = e.fPhone,
                             fPayment = d.fPayment,
                             fPrice = c.fPrice,
                             fOrderStatus = d.fOrderStatus
                         }).ToList();
            return View(query);
        }

        public ActionResult Delete(int id = 0)
        {
            repository.Delete(repository.GetById(id));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(RestauantOrderForm form)
        {
            tRestaurantOrders order = repository.GetID(form.fOrderID);
            order.fOrderStatus = form.fOrderStatus;
            repository.Update(order);
            return RedirectToAction("Index");
        }
    }
}