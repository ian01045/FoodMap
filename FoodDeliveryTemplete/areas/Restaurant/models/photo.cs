using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models
{
    public class photo
    {
        public int fPhotoID { get; set; }
        public Nullable<int> fRestaurantID { get; set; }
        public Nullable<int> fMemberID { get; set; }
        public Nullable<int> fIngridentID { get; set; }
        public Nullable<int> fFoodID { get; set; }
        public System.DateTime fDateTime { get; set; }
        public string fFoodImage { get; set; }
        public string fPhotoPath { get; set; }
        public byte[] fBytesImage { get; set; }

    }
}