using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.yuwang.Models
{
    public class CoordinateUtility
    {

        public double distance(double Longtiude, double Latitude, double Longtiude2, double Latitude2)
        {

            var lat1 = Latitude;
            var lon1 = Longtiude;
            var lat2 = Latitude2;
            var lon2 = Longtiude2;
            var earthRadius = 6371; //appxoximate radius in miles


            var factor = Math.PI / 180;
            var dLat = (lat2 - lat1) * factor;
            var dLon = (lon2 - lon1) * factor;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(lat1 * factor)
              * Math.Cos(lat2 * factor) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = earthRadius * c;

            return d;

        }
    }
}