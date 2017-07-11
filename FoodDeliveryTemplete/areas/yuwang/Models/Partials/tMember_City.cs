using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodTrain.Areas.yuwang.Models
{
    [MetadataType(typeof(CityMetadata))]
    public partial class tMember_City
    {
        public class CityMetadata
        {
            [DisplayName("縣市搜尋")]
            [Required(ErrorMessage = "請選擇縣市")]
            public int fCityID { get; set; }

            public string fCityName { get; set; }

        }
    }
}