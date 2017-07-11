using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.ViewModel
{
    public class InformationRestaurantViewModel
    {
        [DisplayName("餐廳編號")]
        public int fMemberID { get; set; }
        [DisplayName("餐廳名稱")]
        public string fRestaurantName { get; set; }
        [DisplayName("餐廳簡介")]
        public string fDescription { get; set; }
        [DisplayName("餐廳開店時間")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<System.TimeSpan> fOpenTime { get; set; }
        [DisplayName("餐廳關店時間")]
        public Nullable<System.TimeSpan> fCloseTime { get; set; }
        [DisplayName("一週固定店休")]
        public string fRoutine_RestDay_per_week_ { get; set; }
        [DisplayName("餐廳提供付款方式")]
        public string fPaymentWay { get; set; }
        [DisplayName("餐廳平均消費")]
        public Nullable<int> fAveragePricePerGuest { get; set; }



        public IEnumerable<tRestaurant_PaymentWay_Detail> PaymentWay_Detail { get; set; }


    }
}