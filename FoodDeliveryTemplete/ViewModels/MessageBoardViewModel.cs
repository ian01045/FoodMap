using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.ViewModels
{
    public class MessageBoardViewModel
    {
        public string MemberName { get; set; }
        public int MessageID { get; set; }
        public string Message { get; set; }
        public string EmptyMessage { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> fRestaurantID { get; set; }
        public Nullable<int> fMemberID { get; set; }
        public Nullable<int> ParentID { get; set; }
    }
}