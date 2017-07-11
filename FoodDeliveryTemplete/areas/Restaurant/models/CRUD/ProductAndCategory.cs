using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD
{
    public class ProductAndCategory
    {
        public IEnumerable<tCategory> categories { get; set; }
        public IEnumerable<tRestaurant_Foods> products { get; set; }
        
    }
}