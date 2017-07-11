using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.ViewModel
{
    public class MyFavoriteRestaurantCategoryViewModel
    {
        //public IEnumerable<string> myfavorite { get; set; }
        public IEnumerable<tMyFavorite> myfavoriteRestaurant { get; set; }

        public IEnumerable<tRestaurant> restaurant { get; set; }

        public IEnumerable<tMyFavoriteCategory> category { get; set; }
    }
}