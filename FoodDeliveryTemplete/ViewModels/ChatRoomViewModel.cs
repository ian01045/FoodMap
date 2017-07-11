using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModels
{
    public class ChatRoomViewModel
    {
        public int MessageID { get; set; }
        public string Message { get; set; }       
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> MemberID { get; set; }
        public string MemberName { get; set; }
        public Nullable<int> RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public Nullable<bool> Read { get; set; }
        public Nullable<bool> fFrom { get; set; }
        public int CountRead { get; set; }
        public string name { get; set; }
    }
}