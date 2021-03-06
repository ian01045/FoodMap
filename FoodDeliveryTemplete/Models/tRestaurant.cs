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
    
    public partial class tRestaurant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tRestaurant()
        {
            this.tAdvertisement = new HashSet<tAdvertisement>();
            this.tComment = new HashSet<tComment>();
            this.tMessage_Board = new HashSet<tMessage_Board>();
            this.tMyFavorite = new HashSet<tMyFavorite>();
            this.tPhoto = new HashSet<tPhoto>();
            this.tRestaurant_Foods = new HashSet<tRestaurant_Foods>();
            this.tRestaurant_Category_Details = new HashSet<tRestaurant_Category_Details>();
            this.tRestaurantOrders = new HashSet<tRestaurantOrders>();
        }
    
        public int fRestaurantID { get; set; }
        public int fMemberID { get; set; }
        public Nullable<int> fRestaurant_CategoryID { get; set; }
        public Nullable<System.TimeSpan> fOpenTime { get; set; }
        public string fRoutine_RestDay_per_week_ { get; set; }
        public Nullable<System.TimeSpan> fCloseTime { get; set; }
        public Nullable<int> fPaymentWayID { get; set; }
        public Nullable<int> fPhotoID { get; set; }
        public string fSpecial_RestDay_per_month_ { get; set; }
        public string fRestaurantName { get; set; }
        public string fDescription { get; set; }
        public Nullable<int> fAveragePricePerGuest { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tAdvertisement> tAdvertisement { get; set; }
        public virtual tCategory tCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tComment> tComment { get; set; }
        public virtual tMemeber tMemeber { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tMessage_Board> tMessage_Board { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tMyFavorite> tMyFavorite { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tPhoto> tPhoto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tRestaurant_Foods> tRestaurant_Foods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tRestaurant_Category_Details> tRestaurant_Category_Details { get; set; }
        public virtual tRestaurant_PaymentWay_Detail tRestaurant_PaymentWay_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tRestaurantOrders> tRestaurantOrders { get; set; }
    }
}
