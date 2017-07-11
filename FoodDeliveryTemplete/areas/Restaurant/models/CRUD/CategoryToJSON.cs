using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD
{
    public class CategoryToJSON
    {
        //分類用
        public int CategoryId;
        public string CategoryName = "";
        public string CategoryParent = "";
    }
}