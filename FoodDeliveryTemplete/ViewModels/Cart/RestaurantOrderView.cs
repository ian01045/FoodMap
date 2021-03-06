﻿using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodTrain.Areas.Amy.ViewModel
{
    public class RestaurantOrderView
    {
        public RestaurantOrderView()
        {
          foods=new List<FoodNameContentViewModel>();
        }
        public int fOrderID { get; set; }
        public string fMemeberName { get; set; }
        public string fPhone { get; set; }
        public DateTime fOrderDatetime { get; set; }
        public int fOrderStatus { get; set; }
        public decimal fPayment { get; set; }
        public string fAddress { get; set; }
        public decimal fPrice { get; set; }

        public List<FoodNameContentViewModel> foods { get; set; }
    }
    public class SearchForm
    {
        public DateTime fOrderDatetime { get; set; }
        public int fOrderID { get; set; }
        public string fMemeberName { get; set; }
        public string fOrderStatus { get; set; }
    }
    public class RestauantOrderForm
    {
        public int fOrderStatus { get; set; }
        public int fOrderID { get; set; }
    }
}