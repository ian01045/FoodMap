using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Models
{
    
    [MetadataType(typeof(MegaDataMembers))]
    public partial class tMemeber
    {        
        public class MegaDataMembers
        {           
            public int fMemberID { get; set; }
            public string fMemeberName { get; set; }
            public string fPhone { get; set; }
            [DisplayName("會員信箱")]
            [Required(ErrorMessage = "請輸入信箱")]
            [EmailAddress(ErrorMessage = "格式不正確，ex.foodtotrain@mail.com")]
            public string fEmail { get; set; }
            public string fPassword { get; set; }
            public string fPasswordConfirm { get; set; }
            public Nullable<int> fPhotoID { get; set; }
            public int fAreaID { get; set; }
            public string fAddress { get; set; }
            public string fCreditAccount { get; set; }
            public int fRoleID { get; set; }
            public Nullable<int> fCollectionPoint { get; set; }
            public System.DateTime fBirth { get; set; }
            public System.DateTime fJoinDate { get; set; }
            public Nullable<int> fReportsTo { get; set; }
            public string fTel { get; set; }
            public string fGender { get; set; }
        }
    }
}