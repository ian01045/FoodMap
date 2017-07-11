using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModels.Haoming
{
    public class NavCategoryViewModel
    {
        public IEnumerable<tMyFavorite_RestaurantNews> RestaurantNews { get; set; }

        public IEnumerable<tNewsType> NewsType { get; set; }

       
    }
}