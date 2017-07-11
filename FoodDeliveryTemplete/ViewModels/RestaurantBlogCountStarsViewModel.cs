using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModels
{
    public class RestaurantBlogCountStarsViewModel
    {
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public int countstar { get; set; }
        public decimal average { get; set; }
    }
}