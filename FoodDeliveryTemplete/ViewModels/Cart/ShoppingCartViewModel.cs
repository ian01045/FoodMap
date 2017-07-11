using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModels
{
    /// <summary>
    /// 單點菜色詳細內容
    /// </summary>
    public class FoodNameContentViewModel : IEquatable<FoodNameContentViewModel>
    {
        public int fCustomizedCategoryID { get; set; }
        public int fCategoryID { get; set; }
        public int fRestaurantID { get; set; }
        public string fCustomizedCategoryName { get; set; }
        public string fFoodName { get; set; }
        public string fFoodDescription { get; set; }
        [DisplayFormat(DataFormatString = "{0:c0}")]
        public decimal fCost { get; set; }
        public string fFoodImage { get; set; }
        public Byte[] fBytesImage { get; set; }
        public int fFoodID { get; set; }
        [DisplayFormat(DataFormatString = "{0:c0}")]
        public decimal fPrice { get; set; }
        public int fQuantity { get; set; }
        public int fRecordID { get; set; }
        public string fPhotoPath { get; set; }
        public string fMemeberName { get; set; }
        public string fAddress { get; set; }
        public string fPhone { get; set; }
        public Boolean fIsMeal { get; set; }
        public string fCategoryName { get; set; }
        public int? fMealRecordID { get; set; }
        public TimeSpan fCookTime { get; set; }
        public TimeSpan fDeliveryTime { get; set; }
        public List<CustomerOption> CustomerOptions { get; set; }

        public bool Equals(FoodNameContentViewModel other)
        {
            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return fFoodID.Equals(other.fFoodID);
        }

        public int GetHashCode(FoodNameContentViewModel obj)
        {
            return obj.fFoodID;//.GetHashCode();
        }
    }
    public class tRestaurant_Foods

    {
        public Byte[] fBytesImage { get; set; }
    }

    public class CustomerOption
    {
        public int fCustomizedOptionID { get; set; }
        public int fCustomizedCategoryID { get; set; }
        public string fCustomizedOptionName { get; set; }
        public decimal fPrice { get; set; }
    }

    /// <summary>
    /// 顯示購物車
    /// </summary>
    public class FoodNameShowViewModel : FoodNameContentViewModel
    {
        public List<FoodNameContentViewModel> SubDishCategories { get; set; }
    }

    /// <summary>
    /// update-複雜模型繫結
    /// </summary>
    public class ShoppingCartForm
    {
        public int fQuantity { get; set; }
        public int fRecordID { get; set; }
    }

    public class PaymentWayViewModel{
        public int fPaymentwayID { get; set; }
        public string fPaymentway { get; set; }

    }

    /// <summary>
    /// 購物車頁面
    /// </summary>
    public class CartListViewModel
    {
        #region 訂單訊息
        public int fOrderID { get; set; }
        public DateTime fOrderDatetime { get; set; }
        public int fOrderStatus { get; set; }
        public string fState_Description { get; set; }
        #endregion
        #region 菜色資訊
        public int fRestaurantID { get; set; }
        public string IsMeal { get; set; }
        public int fFoodID { get; set; }
        public string fRestaurantName { get; set; }
        #endregion
        #region 結帳的基本資料
        public int fMemberID { get; set; }
        public string fMemeberName { get; set; }
        public string fAddress { get; set; }
        public string fPhone { get; set; }
        public string fEmail { get; set; }
        public int tPaymentWay { get; set; }
        public int SumTotal { get; set; }//消費總額
        public decimal newSurplus { get; set; }//新餘額
        #endregion
        #region 付款方式
        public List<PaymentWayViewModel> PaymentWays { get; set; }
        #endregion
        public List<FoodNameShowViewModel> FoodNameShowViewModels { get; set; }
        public List<FoodNameContentViewModel> FoodNameContentViewModels { get; set; }
        public List<FoodNameContentViewModels_meal> FoodNameContentViewModels_meal { get; set; }
    }

    /// <summary>
    /// 套餐繼承單點,目的=>在controller時,套餐linq可以直接使用單點的資訊 也可額外擴充想要的功能
    /// </summary>
    public class FoodNameContentViewModels_meal : FoodNameContentViewModel
    {
        //public List<IGrouping<string, List<FoodNameContentViewModel>>> SubDishCategories { get; set; }
        //public  List<FoodNameContentViewModel> SubDishCategories { get; set; }
        public List<IGrouping<string, FoodNameContentViewModel>> SubDishCategories { get; set; }
    }

    /// <summary>
    /// 選購套餐-複雜模型繫結
    /// </summary>
    public class mealOrder
    {
        /// <summary>
        /// 此套餐的fFoodID
        /// </summary>
        public int fFoodID { get; set; }
        /// <summary>
        /// 被選購的子項目(Radio Button選的)
        /// </summary>
        public List<fFood> fFoods { get; set; }
        /// <summary>
        /// 套餐數量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 客製化選項
        /// </summary>
        public int CCID { get; set; }
    }

    public struct fFood
    {
        public int fFoodID { get; set; }
        public int CCID { get; set; }
    }
}