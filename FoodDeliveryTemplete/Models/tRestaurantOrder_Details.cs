//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodDeliveryTemplete.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tRestaurantOrder_Details
    {
        public int fID { get; set; }
        public int fOrderID { get; set; }
        public int fFoodID { get; set; }
        public decimal fPrice { get; set; }
        public string fRemark { get; set; }
        public Nullable<decimal> fDiscount { get; set; }
        public Nullable<int> fQuantity { get; set; }
        public Nullable<bool> fIsCheckout { get; set; }
        public Nullable<int> fCustomizedOptionID { get; set; }
    
        public virtual tRestaurant_Foods tRestaurant_Foods { get; set; }
        public virtual tRestaurantOrders tRestaurantOrders { get; set; }
    }
}