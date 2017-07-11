using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModel
{
    public class RestaurantRegistedViewModel
    {
        public int fMemberID { get; set; }
        [DisplayName("餐廳名稱")]
        [Required(ErrorMessage = "餐廳名稱為必填")]
        public string fMemeberName { get; set; }
        //[DisplayName("手機號碼")]
        //[Required(ErrorMessage = "手機號碼為必填")]
        //public string fPhone { get; set; }
        [DisplayName("電子郵件")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "電子郵件為必填")]
        public string fEmail { get; set; }
        [DisplayName("密碼")]
        //[RegularExpression("/^(?=.*\\d)(?=.*[a-zA-Z]).{6,30}$/", ErrorMessage = "6~30 位數，並且至少包含英文字母、數字")]
        [Required(ErrorMessage = "密碼為必填")]
        [DataType(DataType.Password)]
        public string fPassword { get; set; }
        [DisplayName("密碼確認")]
        [Required(ErrorMessage = "密碼確認為必填")]
        [Compare("fPassword", ErrorMessage = "密碼與密碼確認不符")]
        [DataType(DataType.Password)]
        public string fPasswordConfirm { get; set; }
        [DisplayName("照片")]
        public Nullable<int> fPhotoID { get; set; }
        [DisplayName("餐廳所在地區")]
        [Required(ErrorMessage = "餐廳名稱為必填")]
        public int fAreaID { get; set; }
        [DisplayName("店址")]
        [Required(ErrorMessage = "店址為必填")]
        public string fAddress { get; set; }
        [DisplayName("信用卡帳號")]
        [DataType(DataType.CreditCard)]
        public string fCreditAccount { get; set; }

        public int fRoleID { get; set; }

        public Nullable<int> fCollectionPoint { get; set; }
        [DisplayName("創店時間")]
        [DataType(DataType.Date, ErrorMessage = "時間有誤")]
        [Required(ErrorMessage = "創店時間為必填")]
        public System.DateTime fBirth { get; set; }
        [DisplayName("入會時間")]
        public System.DateTime fJoinDate { get; set; }

        public Nullable<int> fReportsTo { get; set; }
        [DisplayName("餐廳電話號碼")]
        [Required(ErrorMessage = "餐廳電話號碼為必填")]
        public string fTel { get; set; }
        public string fGender { get; set; }


        //經緯度 --yuwnag
        public string fLongitude { get; set; }
        public string fLatitude { get; set; }
    }
}