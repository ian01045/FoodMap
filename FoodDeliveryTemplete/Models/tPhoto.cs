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
    
    public partial class tPhoto
    {
        public int fPhotoID { get; set; }
        public Nullable<int> fRestaurantID { get; set; }
        public Nullable<int> fMemberID { get; set; }
        public Nullable<int> fIngridentID { get; set; }
        public Nullable<int> fFoodID { get; set; }
        public System.DateTime fDateTime { get; set; }
        public string fFoodImage { get; set; }
        public string fPhotoPath { get; set; }
        public byte[] fBytesImage { get; set; }
    
        public virtual tFood_Ingrident tFood_Ingrident { get; set; }
        public virtual tMemeber tMemeber { get; set; }
        public virtual tRestaurant tRestaurant { get; set; }
        public virtual tRestaurant_Foods tRestaurant_Foods { get; set; }
    }
}
