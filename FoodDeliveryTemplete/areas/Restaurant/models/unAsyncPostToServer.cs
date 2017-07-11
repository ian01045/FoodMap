using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models
{
    public class unAsyncPostToServer
    {
        public int fCategoryID { get; set; }
        public string fCategoryName { get; set; }
        public Nullable<int> fParentID { get; set; }
        public System.DateTime fModifiedDate { get; set; }
        public string fCategoryDescription { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> ParentID { get; set; }
    }
}