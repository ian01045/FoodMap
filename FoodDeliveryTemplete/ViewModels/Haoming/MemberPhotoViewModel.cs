﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModel
{
    public class MemberPhotoViewModel
    {
        public int fMemberID { get; set; }
        [DisplayName("會員名稱")]
        [Required(ErrorMessage = "會員名稱為必填")]
        public string fMemeberName { get; set; }
        [DisplayName("手機號碼")]
        [Required(ErrorMessage = "手機號碼為必填")]
        public string fPhone { get; set; }
        [DisplayName("電子郵件")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "電子郵件為必填")]
        public string fEmail { get; set; }
        [DisplayName("密碼")]
        //[RegularExpression("/^(?=.*\\d)(?=.*[a-zA-Z]).{6,30}$/", ErrorMessage = "6~30 位數，並且至少包含英文字母、數字")]
        [Required(ErrorMessage = "密碼為必填")]
        public string fPassword { get; set; }
        [DisplayName("照片")]
        public Nullable<int> fPhotoID { get; set; }
        [DisplayName("居住地區")]
        [Required(ErrorMessage = "會員名稱為必填")]
        public int fAreaID { get; set; }
        [DisplayName("地址")]
        [Required(ErrorMessage = "地址為必填")]
        public string fAddress { get; set; }
        [DisplayName("信用卡帳號")]
        [DataType(DataType.CreditCard)]
        public string fCreditAccount { get; set; }

        public int fRoleID { get; set; }

        public Nullable<int> fCollectionPoint { get; set; }
        [DisplayName("生日")]
        [DataType(DataType.Date, ErrorMessage = "時間有誤")]
        [Required(ErrorMessage = "生日為必填")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fBirth { get; set; }
        [DisplayName("入會時間")]
        public System.DateTime fJoinDate { get; set; }

        public Nullable<int> fReportsTo { get; set; }
        [DisplayName("家電號碼")]

        public string fTel { get; set; }
        [DisplayName("性別")]
        [Required(ErrorMessage = "會員性別為必填")]
        public string fGender { get; set; }


        //---------------
        public string fPhotoPath { get; set; }
       
        public System.DateTime fDateTime { get; set; }
        public byte[] fPhoto { get; set; }
        //---------------
        [DisplayName("居住地區")]
        public string fAreaName { get; set; }
        public int fCityID { get; set; }
        [DisplayName("居住城市")]
        public string fCityName { get; set; }

    }
}