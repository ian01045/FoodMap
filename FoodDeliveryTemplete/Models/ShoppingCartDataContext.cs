using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FoodTrain;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Models
{
    public class ShoppingCartDataContext
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        static string strConn = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;
        //ConfigurationManager.ConnectionStrings["FoodDeliveryEntities"].ConnectionString;
        //測試用連接字串，記得改回來
        public static void AddToCart()
        {
            
        }
    }
}