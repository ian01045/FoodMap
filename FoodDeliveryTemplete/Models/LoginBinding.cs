using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Models
{
    public class LoginBinding
    {
        //此類別用來接收登入所傳來的值
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]        
        public string Account { get; set; }

        [DisplayName("舊密碼")]
        [Required(ErrorMessage ="請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("新密碼")]
        [Required(ErrorMessage = "請輸入新密碼")]
        [DataType(DataType.Password)]        
        public string ResetPassword { get; set; }

        [DisplayName("確認新密碼")]
        [Required(ErrorMessage = "請確認密碼")]
        [DataType(DataType.Password)]
        [Compare("ResetPassword", ErrorMessage = "密碼不一致，請重新輸入")]
        public string ConfirmResetPassword { get; set; }
    }
}