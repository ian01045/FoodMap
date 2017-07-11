using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD
{
    public class AllProducts
    {
        public IEnumerable<tRestaurant_Foods> Products { get; set; }
        public IEnumerable<tRestaurant_Foods> MealProducts { get; set; }
        public IEnumerable<tRestaurant_Foods> UnavailableProducts { get; set; }
        public IEnumerable<tRestaurant_Foods> UnavailableMealProducts { get; set; }
    }
}