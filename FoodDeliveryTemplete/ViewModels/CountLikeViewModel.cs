using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModels
{
    public class CountLikeViewModel
    {
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public int CountLike { get; set; }
        public int CountStar { get; set; }


    }
}