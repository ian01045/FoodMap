using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Models
{
    [MetadataType(typeof(CategoryPartialClass))]
    public partial class tCategory
    {
        public class CategoryPartialClass
        {
            public int fCategoryID { get; set; }
            [DisplayName("產品分類")]
            [Required(ErrorMessage = "分類為必填")]
            public string fCategoryName { get; set; }
            [DisplayName("子分類")]
            public Nullable<int> fParentID { get; set; }
            [DisplayName("設定此分類的時間")]
            public System.DateTime fModifiedDate { get; set; }
            [DisplayName("分類說明")]
            public string fCategoryDescription { get; set; }
        }
    }
}