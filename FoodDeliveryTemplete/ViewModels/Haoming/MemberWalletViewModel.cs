using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModel
{
    public class MemberWalletViewModel
    {
        public int fMemberID { get; set; }
        public string fMemeberName { get; set; }
        public string fCreditAccount { get; set; }
        public string fWalletTotalMoney { get; set; }
        [DisplayName("錢包餘額")]
        public decimal fWallat_Surplus { get; set; }
        
        public Nullable<decimal> fWithdraw { get; set; }
        [DisplayName("加值金額")]
        [Required(ErrorMessage = "加值金額為必填")]
        public Nullable<decimal> fDeposit { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}