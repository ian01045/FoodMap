
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.yuwang.ViewModel
{
    public class FoodCategorySelect
    {

        //放所有菜色分類的根類別
        public IEnumerable<tCategory> parent_food_category { get; set; }

        //放所有菜色分類的子類別(無限多層)
        public IEnumerable<tCategory>  child_food_cateogry{ get; set; }

    }
}