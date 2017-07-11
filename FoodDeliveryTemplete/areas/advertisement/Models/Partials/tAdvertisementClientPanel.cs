using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Advertisement.Models.Partials
{
    //public class tAdvertisementClientPanel
    

    [MetadataType(typeof(tAdvertisementClientPanelMetadata))]
    public partial class tAdvertisementClientPanel
    {
        public class tAdvertisementClientPanelMetadata
        {
            [DisplayName("廣告號碼")]
            [Required(ErrorMessage = "廣告號碼未輸入")]
            public int fID { get; set; }

            [DisplayName("廣告標題")]
            [Required(ErrorMessage = "廣告標題未輸入")]
            public int fSubject { get; set; }

            [AllowHtml]
            [DisplayName("廣告內容")]
            [Required(ErrorMessage = "廣告內容未輸入")]
            public string fContentText { get; set; }

            [DisplayName("廣告發布與否")]
            [DataType(DataType.Date)]
            [Required(ErrorMessage = "廣告發布與否需選擇")]
            public string fIsPublish { get; set; }

            [DisplayName("廣告發布日期")]
            [DataType(DataType.Date)]
            [Required(ErrorMessage = "廣告發布日期需輸入")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
            public string fCreateDate { get; set; }

            [DisplayName("廣告更新日期")]
            [Required(ErrorMessage = "廣告更新日期需輸入")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
            public string fUpdateDate { get; set; }

            [AllowHtml]
            [DisplayName("廣告圖片更新")]
            [Required(ErrorMessage = "廣告圖片更新需輸入")]
            public byte fImage { get; set; }

            [AllowHtml]
            [DisplayName("廣告網址更新")]
            [Required(ErrorMessage = "廣告網址更新需輸入")]
            public string fHtml { get; set; }

             public byte[] BytesImage { get; set; }

        }
    }

}