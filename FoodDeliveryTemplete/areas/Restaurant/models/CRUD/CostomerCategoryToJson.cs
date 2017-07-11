using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD
{
    public class CostomerCategoryToJson
    {
        public int CustomizedCategoryID { get; set; }
        public string CustomizedCategoryName { get; set; }
        public Nullable<int> ParentID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int OptionCount;
    }
}