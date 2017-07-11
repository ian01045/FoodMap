using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModels
{
	public class RestaurantBlogViewModels
	{
        public int fRestaurantID { get; set; }
        public int fMemberID { get; set; }
        //public Nullable<int> fRestaurant_CategoryID { get; set; }
        public Nullable<System.TimeSpan> fOpenTime { get; set; }
        public string fRoutine_RestDay_per_week_ { get; set; }
        public Nullable<System.TimeSpan> fCloseTime { get; set; }
        public Nullable<int> fPaymentWayID { get; set; }
        public Nullable<int> fPhotoID { get; set; }
        public string fSpecial_RestDay_per_month_ { get; set; }
        public string fRestaurantName { get; set; }
        public string fDescription { get; set; }
        public string fPaymentway { get; set; }
        public Nullable<int> fStars { get; set; }
        public Nullable<bool> fLike { get; set; }
        public string fComment { get; set; }
        public Nullable<System.DateTime> fDate { get; set; }
        public int CountStar { get; set; }
        public int CountComment { get; set; }
        public int CountLike { get; set; }
        public string fMemeberName { get; set; }
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Check { get; set; }

        public int fFavCategoryID { get; set; }
        public string fFavCategoryName { get; set; }

    }
}