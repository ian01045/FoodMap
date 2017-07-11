using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodTrain.Areas.yuwang.Models;

namespace FoodDeliveryTemplete.Areas.yuwang.Models
{
    public class ReportData
    {
       public IEnumerable<int> consumers_per_month { get; set; }

       public IEnumerable<int> restaurants_per_month { get; set; }


        public IEnumerable<decimal> res_sales_amount_city1 { get; set; }
        public IEnumerable<decimal> res_sales_amount_city2 { get; set; }
        public IEnumerable<decimal> res_sales_amount_city3 { get; set; }
        public IEnumerable<decimal> res_sales_amount_city4 { get; set; }
        public IEnumerable<string> res_sales_amount_city_names { get; set; }

        public IEnumerable<int> res_on_time_oders { get; set; }
        public IEnumerable<int> res_off_time_oders { get; set; }
        //public IEnumerable<double> restaurants_areas_taipeicity_portion { get; set; }


        public IEnumerable<int> hot_sales_foods_order_count { get; set; }
        public IEnumerable<string> hot_slaes_foods_name { get; set; }
    }
}