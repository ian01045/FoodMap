using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModel
{
    public class OpenAccountViewModel
    {
        public int fMemberID { get; set; }
        [DisplayName("會員名稱")]
        //[Required(ErrorMessage = "會員名稱為必填")]
        public string fMemeberName { get; set; }

        //----------------------------
        [DisplayName("信用卡帳號")]
        [Required(ErrorMessage = "信用卡帳號為必填")]
        //[DataType(DataType.CreditCard)]
        [RegularExpression(@"\d{16}", ErrorMessage = "信用卡帳號必須為16碼")]
        public string fCreditAccount { get; set; }

        [DisplayName("電子錢包餘額")]
        public string fWalletTotalMoney { get; set; }
    }
}