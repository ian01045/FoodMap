using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Models
{
    public class ClassRestaurantandComment
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        public List<RestaurantandComment> ShowRestandComment()
        {
            List<RestaurantandComment> datas = new List<RestaurantandComment>();
            var restaurant = db.tRestaurant.ToList();
            foreach (var item in restaurant)//將Restaurant的資料全部倒進RestaurantandComment
            {
                RestaurantandComment rc = new RestaurantandComment();
                rc.fRestaurantID = item.fRestaurantID;
                rc.fRestaurantName = item.fRestaurantName;
                rc.fPhotoID = item.fPhotoID;
                rc.fOpenTime = item.fOpenTime.Value;
                rc.fCloseTime = item.fCloseTime.Value;
                rc.fDescription = item.fDescription;
                rc.CountComment = db.tComment.Where(c => c.fRestaurantID == item.fRestaurantID & c.fLike == true).Count();
                rc.CountLikes = db.tComment.Where(c => c.fRestaurantID == item.fRestaurantID & c.fStars != null).Count();
                datas.Add(rc);
            }
            return (datas);
        }

        public List<RestaurantandComment> Order(string sortby)
        {
            var rs = (from r in db.tRestaurant                      
                      join c in db.tComment
                      on r.fRestaurantID equals c.fRestaurantID
                      into gp                      
                      select new
                      {
                          r.fRestaurantID,
                          r.fRestaurantName,
                          r.fOpenTime,
                          r.fCloseTime,
                          r.fDescription,
                          r.fPhotoID,
                          CountStar = gp.Count(a=>a.fStars!=null),
                          CountComment = gp.Count(a => a.fLike != null)
                      }).AsQueryable();
            switch (sortby)
            {
                case "ID":
                    rs = rs.OrderBy(c => c.fRestaurantID);
                    break;
                case "Comment":
                    rs = rs.OrderByDescending(c => c.CountComment);
                    break;
                case "Star":
                    rs = rs.OrderByDescending(c => c.CountStar);
                    break;
            }


            List<RestaurantandComment> datas = new List<RestaurantandComment>();                  
            foreach (var item in rs)//將Restaurant的資料全部倒進RestaurantandComment
            {
                RestaurantandComment rc = new RestaurantandComment();
                rc.fRestaurantID = item.fRestaurantID;
                rc.fRestaurantName = item.fRestaurantName;
                rc.fPhotoID = item.fPhotoID;
                rc.fOpenTime = item.fOpenTime.Value;
                rc.fCloseTime = item.fCloseTime.Value;
                rc.fDescription = item.fDescription;
                rc.CountComment = db.tComment.Where(c => c.fRestaurantID == item.fRestaurantID & c.fLike == true).Count();
                rc.CountLikes = db.tComment.Where(c => c.fRestaurantID == item.fRestaurantID & c.fStars != null).Count();
                datas.Add(rc);
            }
            return (datas);
        }
        string connection = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;
        public List<RestaurantandComment> MonthStar()
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(sqlMonthStar(), con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<RestaurantandComment> datas = new List<RestaurantandComment>();
                    while (reader.Read())
                    {
                        RestaurantandComment rc = new RestaurantandComment();
                        rc.fRestaurantID = Convert.ToInt32(reader["fRestaurantID"]);
                        rc.fRestaurantName = reader["fRestaurantName"].ToString();
                        int fPhoto = reader["fPhotoID"] != DBNull.Value ? Convert.ToInt32(reader["fPhotoID"]) : 0;
                        rc.fPhotoID = fPhoto;
                        rc.fOpenTime = TimeSpan.Parse(reader["fOpenTime"].ToString());
                        rc.fCloseTime = TimeSpan.Parse(reader["fCloseTime"].ToString());
                        rc.fDescription = reader["fDescription"].ToString();
                        rc.CountComment = Convert.ToInt32(reader["Liked"]);
                        rc.CountLikes = Convert.ToInt32(reader["Star"]);
                        datas.Add(rc);
                    }
                    reader.Close();
                    con.Close();
                    return (datas);
                }
            }               
        }

        public List<RestaurantandComment> WeekStar()
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(sqlWeekStar(), con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<RestaurantandComment> datas = new List<RestaurantandComment>();
                    while (reader.Read())
                    {
                        RestaurantandComment rc = new RestaurantandComment();
                        rc.fRestaurantID = Convert.ToInt32(reader["fRestaurantID"]);
                        rc.fRestaurantName = reader["fRestaurantName"].ToString();
                        int fPhoto = reader["fPhotoID"] != DBNull.Value ? Convert.ToInt32(reader["fPhotoID"]) : 0;
                        rc.fPhotoID = fPhoto;
                        rc.fOpenTime = TimeSpan.Parse(reader["fOpenTime"].ToString());
                        rc.fCloseTime = TimeSpan.Parse(reader["fCloseTime"].ToString());
                        rc.fDescription = reader["fDescription"].ToString();
                        rc.CountComment = Convert.ToInt32(reader["Liked"]);
                        rc.CountLikes = Convert.ToInt32(reader["Star"]);
                        datas.Add(rc);
                    }
                    reader.Close();
                    con.Close();
                    return (datas);
                }
            }
        }

        public string sqlMonthStar()
        {
            string s;
            //在count裡面寫條件式，若c.fDate介於2017-03-01或2017-03-31之間的話，則結果顯示1否則null
            s = "select c.monthstar,r.fRestaurantID,fRestaurantName,r.fOpenTime,r.fCloseTime,r.fDescription,fPhotoID,SUM(CASE WHEN c1.fLike=1 THEN 1 ELSE 0 END) AS Liked,count(c1.fStars) as Star";
            s += " from tRestaurant r left join";
            s += " (select count(*) as monthstar,fRestaurantID from tComment where fDate between '" + DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + "' and '" + DateTime.Now.ToString("yyyy-MM-dd") + "' group by fRestaurantID) as c";
            s += " on r.fRestaurantID = c.fRestaurantID";
            s += " left join tComment c1";
            s += " on r.fRestaurantID = c1.fRestaurantID";
            s += " group by c.monthstar,r.fRestaurantID,fRestaurantName,r.fOpenTime,r.fCloseTime,r.fDescription,fPhotoID";
            s += " order by monthstar desc";
            return s;
        }

        public string sqlWeekStar()
        {
            string s;
            //在count裡面寫條件式，若c.fDate介於2017-03-01或2017-03-31之間的話，則結果顯示1否則null
            s = "select c.monthstar,r.fRestaurantID,fRestaurantName,r.fOpenTime,r.fCloseTime,r.fDescription,fPhotoID,SUM(CASE WHEN c1.fLike=1 THEN 1 ELSE 0 END) AS Liked,count(c1.fStars) as Star";
            s += " from tRestaurant r left join";
            s += " (select count(*) as monthstar,fRestaurantID from tComment where fDate between '" + DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "' and '" + DateTime.Now.ToString("yyyy-MM-dd") + "' group by fRestaurantID) as c";
            s += " on r.fRestaurantID = c.fRestaurantID";
            s += " left join tComment c1";
            s += " on r.fRestaurantID = c1.fRestaurantID";
            s += " group by c.monthstar,r.fRestaurantID,fRestaurantName,r.fOpenTime,r.fCloseTime,r.fDescription,fPhotoID";
            s += " order by monthstar desc";
            return s;
        }
    }
}