using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Models
{
    [MetadataType(typeof(tAdvertisementMetadata))]
    public partial class tAdvertisement
    {
        public class tAdvertisementMetadata
        {
            [DisplayName("廣告編號")]
            [Required(ErrorMessage = "廣告編號要輸入")]
            public int fAdvertisementID { get; set; }

            [DisplayName("餐廳編號")]
            [Required(ErrorMessage = "餐廳編號要輸入")]
            public int fRestaurantID { get; set; }


            [DisplayName("廣告價格區域編號")]
            [Required(ErrorMessage = "廣告價格區域編號要輸入")]
            public int fPriceRegionID { get; set; }



            [DisplayName("廣告投放起始日期")]
            [DataType(DataType.Date)]
            [Required(ErrorMessage = "廣告投放起始日期要輸入")]
            public string fAppliedDate { get; set; }

            [DisplayName("廣告投放結束日期")]
            [DataType(DataType.Date)]
            [Required(ErrorMessage = "廣告投放結束日期要輸入")]
            public string fDays { get; set; }

            [DisplayName("廣告簡介")]
            [Required(ErrorMessage = "廣告簡介要輸入")]
            [StringLength(200, ErrorMessage = "產品描述不要超過200個字元")]
            public string fMediaIntroduction { get; set; }

            //[DisplayName("廣告圖片")]
            //[Required(ErrorMessage = "廣告圖片要輸入")]
            //public string fAdvertisementPicture { get; set; }

        }
    }
}