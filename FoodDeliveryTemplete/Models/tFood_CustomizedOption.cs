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
    
    public partial class tFood_CustomizedOption
    {
        public int fCustomizedOptionID { get; set; }
        public int fCustomizedCategoryID { get; set; }
        public string fCustomizedOptionName { get; set; }
        public Nullable<decimal> fPrice { get; set; }
    
        public virtual tRestaurant_FoodCustomizedCategory tRestaurant_FoodCustomizedCategory { get; set; }
    }
}
