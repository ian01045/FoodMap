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
    
    public partial class tAdvertisement
    {
        public int fRestaurantID { get; set; }
        public int fAdvertisementID { get; set; }
        public System.DateTime fDays { get; set; }
        public System.DateTime fAppliedDate { get; set; }
        public string fMediaIntroduction { get; set; }
        public Nullable<int> fClickCount { get; set; }
        public int fPriceRegionID { get; set; }
    
        public virtual tAdvertisement_Price tAdvertisement_Price { get; set; }
        public virtual tRestaurant tRestaurant { get; set; }
    }
}
