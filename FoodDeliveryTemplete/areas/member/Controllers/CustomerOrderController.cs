using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Member.Controllers
{
    public class CustomerOrderController : Controller
    {
        // GET: Amy/CustomerOrder

        FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: CustomerOrder
        public ActionResult OrderIndex()
        {
            int _fMemberID = 0;
            #region 判斷有無登入
            //沒有登入的動作
            //使用MVC講義 3-21頁 .Net framework內建通用委派型別
            //使用Lambda產生無名方法，指派到剛剛定義好的通用委派DelAction
            Func<ActionResult> DelAction = () =>
            {
                #region 還沒有登入
                Session["fCartID"] = null;
                Session.Abandon();
                return RedirectToAction("LoginView", "Account");
                #endregion
            };
            if (Request.Cookies["MemberID"] == null)
            {
                DelAction();
            }
            else if (!int.TryParse(Request.Cookies["MemberID"].Value, out _fMemberID) || _fMemberID == 0)
            {
                DelAction();
            }
            #endregion

            //將會員訂過的餐點  依照訂單號碼  從畫面中呈現

            #region 舊寫法不使用
            #region 未分群
            /*
            var product = from c in db.tRestaurantOrders
                          join d in db.tRestaurantOrder_Details
                          on c.fOrderID equals d.fOrderID
                          join e in db.tRestaurant_Foods
                          on d.fFoodID equals e.fFoodID
                          where c.fMemberID == _fMemberID
                          select new MemberOrderViewModel
                          {
                              fFoodName = e.fFoodName,
                          };
            */
            #endregion

            #region 使用Linq查詢
            /*
            var product = from c in db.tRestaurantOrders
                          where c.fMemberID == _fMemberID
                          join d in db.tRestaurantOrder_Details
                          on c.fOrderID equals d.fOrderID
                          group d by c.fOrderID into x
                          select new MemberOrderViewModel
                          {
                              fOrderID = x.FirstOrDefault().fOrderID,
                              restaurantfoods = (from a in x
                                                 join e in db.tRestaurant_Foods
                                                 on a.fFoodID equals e.fFoodID
                                                 select new RestaurantFoodViewModel
                                                 {
                                                     fFoodName = e.fFoodName
                                                 }
                              ).ToList()
                          };
            */
            #endregion

            #region 使用Linq查詢 + 導覽屬性
            /*
            var product = from c in db.tRestaurantOrders
                          where c.fMemberID == _fMemberID
                          orderby c.fOrderDatetime descending
                          select new MemberOrderViewModel
                          {
                              fOrderID = c.fOrderID,
                              fOrderDatetime = c.fOrderDatetime,
                              fOrderStatus = c.fOrderStatus,
                              fRestaurantName = c.tRestaurant.fRestaurantName,
                              restaurantfoods = (from d in c.tRestaurantOrder_Details//有使用導覽屬性
                                                 join e in db.tRestaurant_Foods
                                                 on d.fFoodID equals e.fFoodID
                                                 select new RestaurantFoodViewModel
                                                 {
                                                     fFoodName = e.fFoodName
                                                 }).ToList(),
                          };
            */
            #endregion

            #region 使用Linq方法 + 導覽屬性
            /*
            var product = db.tRestaurantOrders.Where(c => c.fMemberID == _fMemberID)
                .Select(c => new MemberOrderViewModel
                {
                    fOrderID = c.fOrderID,
                    restaurantfoods = c.tRestaurantOrder_Details.Join(db.tRestaurant_Foods,
                     a => a.fFoodID,
                     b => b.fFoodID,
                     (a, b) => new RestaurantFoodViewModel
                     {
                         fFoodName = b.fFoodName
                     }
                    ).ToList()
                });
            */
            //var returnObj = product.ToList();
            #endregion
            #endregion

            var product = (from c in db.tRestaurantOrders
                           where c.fMemberID == _fMemberID
                           orderby c.fOrderDatetime descending
                           select new CartListViewModel
                           {
                               fOrderID = c.fOrderID,
                               fOrderDatetime = c.fOrderDatetime,
                               fOrderStatus = c.fOrderStatus,
                               fState_Description = c.tRestaurant_Order_States.fState_Description,
                               fRestaurantName = c.tRestaurant.fRestaurantName,
                           }).ToList();
            for (int i = 0; i < product.Count; i++)
            {
                product[i].FoodNameShowViewModels = AmyMemberOrder.GetOrderDetail(db, _fMemberID, product[i].fOrderID);
            }

            return View(product.ToList());
        }
    }
}
