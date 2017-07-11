using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD
{
    public class RestaurantFoodToJson
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
        public string fFoodDescription { get; set; }
    }
}