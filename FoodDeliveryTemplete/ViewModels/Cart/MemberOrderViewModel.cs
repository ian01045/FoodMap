using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FoodDeliveryTemplete.ViewModels
{
    /// <summary>
    /// 訂單明細
    /// </summary>
    public static class AmyMemberOrder
    {
        /// <summary>
        /// 取得買家訂單資料,這一個訂單有哪些菜色(會員看訂單)
        /// 及(購物車清單)所用到
        /// </summary>
        /// <param name="fOrderID">不傳入</param>
        /// <returns></returns>
        public static List<FoodNameShowViewModel> GetOrderDetail(FoodDeliveryEntities db, int fMemberID, int fOrderID)
        {

            return
              (from a in db.tRestaurantOrders
               where a.fMemberID == fMemberID
               from c in a.tRestaurantOrder_Details
               join d in db.tRestaurant_Foods
               on c.fFoodID equals d.fFoodID
               let _fCustomizedOptionID_1 = c.fCustomizedOptionID == null ? 0 : (int)c.fCustomizedOptionID
               join r in db.tFood_CustomizedOption
               on c.fCustomizedOptionID equals r.fCustomizedOptionID into x
               from j in x.DefaultIfEmpty()
               where c.fOrderID == fOrderID && (c.fRemark.Substring(0, 1) == "0" || c.fRemark == "-1")
               select new FoodNameShowViewModel
               {
                   fFoodName = d.fFoodName,
                   fPrice = d.fPrice,
                   fQuantity = (int)c.fQuantity,
                   fIsMeal = d.fIsMeal,
                   fDeliveryTime = a.fDeliveryTime,
                   fCustomizedCategoryName = j.fCustomizedOptionName,
                   fCustomizedCategoryID = _fCustomizedOptionID_1,
                   SubDishCategories = (from g in db.tRestaurantOrders
                                        where g.fMemberID == fMemberID
                                        from n in g.tRestaurantOrder_Details
                                        join m in db.tRestaurant_Foods
                                        on n.fFoodID equals m.fFoodID
                                        let _fCustomizedOptionID_2 = n.fCustomizedOptionID == null ? 0 : (int)n.fCustomizedOptionID
                                        join p in db.tFood_CustomizedOption
                                        on n.fCustomizedOptionID equals p.fCustomizedOptionID into b
                                        from y in b.DefaultIfEmpty()
                                        where n.fOrderID == fOrderID && n.fRemark == c.fRemark.Substring(1, c.fRemark.Length - 1) && c.fRemark.Substring(0, 1) == "0"
                                        select new FoodNameContentViewModel
                                        {
                                            fFoodName = m.fFoodName,
                                            fCustomizedCategoryName = y.fCustomizedOptionName,
                                            fCustomizedCategoryID = _fCustomizedOptionID_2
                                        }
                                        ).ToList()
               }).ToList();

        }

    }

    /// <summary>
    /// 已沒用到! By Amy
    /// </summary>
    public class MemberOrderViewModel
    {
        public MemberOrderViewModel()
        {
            restaurantfoods = new List<RestaurantFoodViewModel>();
        }

        public int fOrderID { get; set; }
        public DateTime fOrderDatetime { get; set; }
        public int fOrderStatus { get; set; }
        public string fRestaurantName { get; set; }
        public List<RestaurantFoodViewModel> restaurantfoods { get; set; }

        [DisplayName("小計")]
        public int subtotal { get; set; }

        public int Serviece { get; set; }
        [DisplayName("總計")]
        public int sum { get; set; }
    }
    /// <summary>
    /// 已沒用到! By Amy
    /// </summary>
    public class RestaurantFoodViewModel
    {
        public int fFoodID { get; set; }
        public string fFoodName { get; set; }
        public Nullable<int> fCustomizedCategoryID { get; set; }
        public int fRestaurantID { get; set; }
        public string fRemark { get; set; }
        public decimal fPrice { get; set; }
        public Nullable<decimal> fCost { get; set; }
        public bool fAvailable { get; set; }
        public Nullable<decimal> fDiscount { get; set; }
        public bool fRaw_Cooked { get; set; }
        public Nullable<System.DateTime> fModifiedDate { get; set; }
        public Nullable<int> fPhotoID { get; set; }
        public System.TimeSpan fCookTime { get; set; }
        public int fCategoryID { get; set; }
        public bool fIsMeal { get; set; }
    }

}