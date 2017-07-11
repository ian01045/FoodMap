using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModels.Haoming
{
    public class ActivationViewModel
    {
        public Nullable<bool> fActivated { get; set; }
        [DisplayName("請輸入您的驗證碼")]
        //[StringLength(200, ErrorMessage = "餐廳簡介不要超過200個字元")]
        [Required(ErrorMessage = "驗證碼為必填")]
        public string fValidNumber { get; set; }

        public int fMemberID { get; set; }

        public string fMemeberName { get; set; }
    }
}