using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModels
{
    public class RestaurantandComment
    {
        public int fRestaurantID { get; set; }
        public string fRestaurantName { get; set; }
        public TimeSpan fOpenTime { get; set; }
        public TimeSpan fCloseTime { get; set; }
        public int? fPhotoID { get; set; }
        public string fDescription { get; set; }
        public int CountComment { get; set; }
        public int CountLikes { get; set; }
        public int MonthStar { get; set; }
    }
}