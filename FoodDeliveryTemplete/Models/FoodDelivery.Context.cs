﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FoodDeliveryEntities : DbContext
    {
        public FoodDeliveryEntities()
            : base("name=FoodDeliveryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChatRoom> ChatRoom { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tAdvertisement> tAdvertisement { get; set; }
        public virtual DbSet<tAdvertisement_Article> tAdvertisement_Article { get; set; }
        public virtual DbSet<tAdvertisement_Price> tAdvertisement_Price { get; set; }
        public virtual DbSet<tBrowseHistory> tBrowseHistory { get; set; }
        public virtual DbSet<tCategory> tCategory { get; set; }
        public virtual DbSet<tComment> tComment { get; set; }
        public virtual DbSet<tFood_CustomizedOption> tFood_CustomizedOption { get; set; }
        public virtual DbSet<tFood_FoodIngrident_Detail> tFood_FoodIngrident_Detail { get; set; }
        public virtual DbSet<tFood_Ingrident> tFood_Ingrident { get; set; }
        public virtual DbSet<tMember_Area> tMember_Area { get; set; }
        public virtual DbSet<tMember_City> tMember_City { get; set; }
        public virtual DbSet<tMember_Wallat> tMember_Wallat { get; set; }
        public virtual DbSet<tMemeber> tMemeber { get; set; }
        public virtual DbSet<tMessage> tMessage { get; set; }
        public virtual DbSet<tMessage_Board> tMessage_Board { get; set; }
        public virtual DbSet<tMyFavorite> tMyFavorite { get; set; }
        public virtual DbSet<tMyFavorite_RestaurantNews> tMyFavorite_RestaurantNews { get; set; }
        public virtual DbSet<tMyFavoriteCategory> tMyFavoriteCategory { get; set; }
        public virtual DbSet<tNewsType> tNewsType { get; set; }
        public virtual DbSet<tPaymentWay> tPaymentWay { get; set; }
        public virtual DbSet<tPhoto> tPhoto { get; set; }
        public virtual DbSet<tRestaurant> tRestaurant { get; set; }
        public virtual DbSet<tRestaurant_Category> tRestaurant_Category { get; set; }
        public virtual DbSet<tRestaurant_Category_Details> tRestaurant_Category_Details { get; set; }
        public virtual DbSet<tRestaurant_FoodCustomizedCategory> tRestaurant_FoodCustomizedCategory { get; set; }
        public virtual DbSet<tRestaurant_Foods> tRestaurant_Foods { get; set; }
        public virtual DbSet<tRestaurant_Order_States> tRestaurant_Order_States { get; set; }
        public virtual DbSet<tRestaurant_PaymentWay_Detail> tRestaurant_PaymentWay_Detail { get; set; }
        public virtual DbSet<tRestaurantMeal_Detail> tRestaurantMeal_Detail { get; set; }
        public virtual DbSet<tRestaurantOrder_Details> tRestaurantOrder_Details { get; set; }
        public virtual DbSet<tRestaurantOrders> tRestaurantOrders { get; set; }
        public virtual DbSet<tRole> tRole { get; set; }
        public virtual DbSet<tShoppingCart> tShoppingCart { get; set; }
    }
}
