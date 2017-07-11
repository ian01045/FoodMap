using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD
{
    public class CostomerOptionToJSON
    {
        public int fCustomizedOptionID { get; set; }
        public int fCustomizedCategoryID { get; set; }
        public string fCustomizedOptionName { get; set; }
        public Nullable<decimal> fPrice { get; set; }
        
    }
}