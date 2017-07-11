
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.yuwang.ViewModel
{
    //為倒入index(view)搜尋出來的餐廳用的viewModel
    public class RestaurantSelect
    {
        public string fRestaurantName { get; set; }


        public int fMemberID { get; set; }

        public IEnumerable<tRestaurant_Foods> fRestaurant_Foods { get; set; }

        public int fLikeCounts { get; set; }

        public int fMyFavCounts { get; set; }

        public double? fStarsCounts { get; set; }//還沒有被評星等所以可能沒有星星數平均

        public int fRestaurantID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public System.DateTime fJoinDate { get; set; }

        public Nullable<int> fAveragePricePerGuest { get; set; }

        public int fOrderCounts { get; set; }

        public double fDistance { get; set; }

        public TimeSpan fAverageArrivalTime { get; set; }

        public IEnumerable<tRestaurant_Category_Details> fRestaurant_Categories { get; set; }

        //public byte[] fBytesImage { get; set; }

        //public Nullable<int> fStars { get; set; }

        //public bool isMyfavorite { get; set; }

        //public IEnumerable<tRestaurant> restaurant { get; set; }

        //public IEnumerable<tCategory> category { get; set; }


    }
}