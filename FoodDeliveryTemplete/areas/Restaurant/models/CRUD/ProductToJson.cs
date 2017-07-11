using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD
{
    public class ProductToJson
    {
        public int fFoodID { get; set; }
        public string fFoodName { get; set; }
        public decimal fPrice { get; set; }
        public Nullable<System.DateTime> fModifiedDate { get; set; }
        //public System.TimeSpan fCookTime { get; set; }
        public string fFoodDescription { get; set; }
        public byte[] LargePhoto { get; set; }
    }
}