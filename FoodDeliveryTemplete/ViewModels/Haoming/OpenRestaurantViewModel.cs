using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModel
{
    public class OpenRestaurantViewModel
    {
        [DisplayName("詳細餐廳名稱")]
        public string fRestaurantName { get; set; }
        public int fMemberId { get; set; }
        [DisplayName("每周固定店休")]
        public string fRoutine_RestDay_per_week_ { get; set; }
        //[DisplayName("每月固定店休")]
        //public string fSpecial_RestDay_per_month_ { get; set;}
        [DisplayName("餐廳簡介")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "餐廳簡介不要超過200個字元")]
        [Required(ErrorMessage = "餐廳簡介為必填")]
        public string fDescription { get; set; }
        [DisplayName("開店時間")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "開店時間為必填")]
        public Nullable<System.TimeSpan> fOpenTime { get; set; }
        [DisplayName("關店時間")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "關店時間為必填")]
        public Nullable<System.TimeSpan> fCloseTime { get; set; }
        [DisplayName("餐廳平均消費")]
        [Required(ErrorMessage = "餐廳平均消費為必填")]
        public Nullable<int> fAveragePricePerGuest { get; set; }



        public IList<string> SelectedWeek { get; set; }
        public IList<int> PaywayCheckboxs { get; set; }
        

    }
}