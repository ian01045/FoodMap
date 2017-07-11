using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.ViewModels.Haoming
{
    public class RestaurantNewsViewModel
    {
       
        public int fArticleID { get; set; }
        public int fRestaurantID { get; set; }
        [DisplayName("消息分類")]
        [Required(ErrorMessage = "消息分類為必填")]
        public int fNewsTypeID { get; set; }
        [DisplayName("消息內容")]
        [DataType(DataType.MultilineText, ErrorMessage = "消息內容")]
        //[Required(ErrorMessage = "消息內容為必填")]
        [AllowHtml]
        public string fNews { get; set; }
        [DisplayName("發布時間")]
        [DataType(DataType.Date, ErrorMessage = "發布時間")]
        [Required(ErrorMessage = "發布時間為必填")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fPublishedDate { get; set; }
        [DisplayName("消息標題")]
        [Required(ErrorMessage = "消息標題為必填")]
        public string fTitle { get; set; }


        public string NewsTypeName { get; set; }
    }


}