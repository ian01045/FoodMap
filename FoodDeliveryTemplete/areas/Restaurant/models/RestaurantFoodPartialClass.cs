using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Models
{
    [MetadataType(typeof(RestaurantFoodPartialClass))]
    public partial class tRestaurant_Foods
    {
        public class RestaurantFoodPartialClass
        {
            [DisplayName("產品編號")]
            public int fFoodID { get; set; }
            [DisplayName("產品名稱")]
            public string fFoodName { get; set; }
            [DisplayName("客製化分類編號")]
            public Nullable<int> fCustomizedCategoryID { get; set; }
            [DisplayName("餐廳編號")]
            public int fRestaurantID { get; set; }
            public string fRemark { get; set; }
            [DisplayName("價格")]
            public decimal fPrice { get; set; }
            [DisplayName("成本")]
            public Nullable<decimal> fCost { get; set; }
            [DisplayName("產品上架")]
            public bool fAvailable { get; set; }
            [DisplayName("折扣")]
            public Nullable<decimal> fDiscount { get; set; }
            [DisplayName("此為食材")]
            public bool fRaw_Cooked { get; set; }
            [DisplayName("建立時間")]
            public Nullable<System.DateTime> fModifiedDate { get; set; }
            [DisplayName("產品照片")]
            public Nullable<int> fPhotoID { get; set; }
            [DisplayName("烹煮時間")]
            public System.TimeSpan fCookTime { get; set; }
            [DisplayName("產品分類")]
            public int fCategoryID { get; set; }
            [DisplayName("此為套餐")]
            public bool fIsMeal { get; set; }
            [DisplayName("產品說明")]
            public string fFoodDescription { get; set; }
        }
    }
    
}