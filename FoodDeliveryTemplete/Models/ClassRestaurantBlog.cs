using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Models
{
    public class ClassRestaurantBlog
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        string connection = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;
        public RestaurantBlogViewModels Index(int id=0)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(sqBlogInformation(id), con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    RestaurantBlogViewModels rc = new RestaurantBlogViewModels();
                    while (reader.Read())
                    {                        
                        rc.fRestaurantID = Convert.ToInt32(reader["fRestaurantID"]);
                        rc.fRestaurantName = reader["fRestaurantName"].ToString();
                        int fPhoto = reader["fPhotoID"] != DBNull.Value ? Convert.ToInt32(reader["fPhotoID"]) : 0;
                        rc.fPhotoID = fPhoto;
                        rc.fOpenTime = TimeSpan.Parse(reader["fOpenTime"].ToString());
                        rc.fCloseTime = TimeSpan.Parse(reader["fCloseTime"].ToString());
                        rc.fDescription = reader["fDescription"].ToString();
                        rc.fRoutine_RestDay_per_week_ = reader["fRoutine_RestDay(per week)"].ToString();
                        rc.fPaymentway = reader["fPaymentway"].ToString();
                        rc.CountComment = Convert.ToInt32(reader["comment"]);
                        rc.CountStar = Convert.ToInt32(reader["star"]);
                        rc.CountLike = Convert.ToInt32(reader["Liked"]);                     
                    }
                    reader.Close();
                    con.Close();
                    return rc;
                }
            }
        }
        public string sqBlogInformation(int id)
        {
            string s;
            s = "select c.countcomment,r.fRestaurantID,fRestaurantName,r.fOpenTime,r.fCloseTime,r.fDescription,fPhotoID,fPaymentway,SUM(CASE WHEN c1.fLike=1 THEN 1 ELSE 0 END) AS Liked,count(c1.fComment) as comment,count(c1.fStars) as star,r.[fRoutine_RestDay(per week)]";
            s += " from tRestaurant r left join ";
            s += " (select count(fComment) as countcomment,fRestaurantID from tComment group by fRestaurantID) as c";
            s += " on r.fRestaurantID = c.fRestaurantID";
            s += " left join tComment c1";
            s += " on r.fRestaurantID = c1.fRestaurantID";
            s += " left join tPaymentWay p";
            s += " on r.fPaymentWayID = p.fPaymentwayID";
            s += " where r.fRestaurantID = " + id + "";
            s += " group by c.countcomment,r.fRestaurantID,fRestaurantName,r.fOpenTime,r.fCloseTime,r.fDescription,fPhotoID,fPaymentway,r.[fRoutine_RestDay(per week)]";
           
            return s;
        }

        //讀取某間餐廳的讚數，利用id來判斷條件
        public CountLikeViewModel CountedLike(int id)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(SqlCountLike(id), con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    CountLikeViewModel rc = new CountLikeViewModel();
                    while (reader.Read())
                    {
                        rc.RestaurantName = (string)reader["fRestaurantName"];
                        rc.RestaurantID = Convert.ToInt32(reader["fRestaurantID"]);
                        rc.CountLike = Convert.ToInt32(reader["Liked"]);
                    }
                    reader.Close();
                    con.Close();
                    return rc;
                }
            }
        }

        public string SqlCountLike(int id)
        {
            string s;
            s = "select r.fRestaurantID,fRestaurantName,SUM(CASE WHEN c1.fLike=1 THEN 1 ELSE 0 END) AS Liked,count(c1.fStars) as star";
            s += " from tRestaurant r";
            s += " left join tComment c1";
            s += " on r.fRestaurantID = c1.fRestaurantID";
            s += " where r.fRestaurantID ="+id+"";
            s += " group by r.fRestaurantID,fRestaurantName";
            return s;
        }

        //讀取評價星星等相關資料於部落格中
        public List<RestaurantBlogViewModels> CommentandStar(int id)
        {
            var data = (from c in db.tComment
                        join m in db.tMemeber
                        on c.fMemberID equals m.fMemberID
                        where c.fRestaurantID == id && c.fStars!=null
                        select new
                        {
                            fComment = c.fComment,
                            fStar = c.fStars,
                            fMemberID = c.fMemberID,
                            fLike = c.fLike,
                            fDate = c.fDate,
                            fMemberName = m.fMemeberName

                        }).AsQueryable();
            List<RestaurantBlogViewModels> rc = new List<RestaurantBlogViewModels>();
            foreach (var r in data)
            {
                RestaurantBlogViewModels bl = new RestaurantBlogViewModels();
                bl.fDate = r.fDate;
                bl.fStars = r.fStar;
                bl.fComment = r.fComment;
                bl.fMemberID = r.fMemberID;
                bl.fLike = r.fLike;
                bl.fMemeberName = r.fMemberName;
                rc.Add(bl);
            }
            return rc;

        }

        public int ConditionofLike(int RestaurantID,int MemberID)
        {
            var condition = (from i in db.tComment
                            where i.fRestaurantID == RestaurantID & i.fMemberID == MemberID & i.fLike == true
                            select i).ToList().Count();
            return condition;
        }

        public int ConditionofFavorite(int RestaurantID, int MemberID)
        {
            var condition = (from i in db.tMyFavorite
                             where i.fRestaurantID == RestaurantID & i.fMemberID == MemberID
                             select i).ToList().Count();
            return condition;
        }

        public RestaurantBlogCountStarsViewModel GetStarInformation(int restaurantID =0)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(SqlGetStars(restaurantID), con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    RestaurantBlogCountStarsViewModel rc = new RestaurantBlogCountStarsViewModel();
                    while (reader.Read())
                    {
                        rc.RestaurantID = (int)reader["fRestaurantID"];
                        rc.RestaurantName = (string)reader["fRestaurantName"];
                        rc.average = reader["star"] !=DBNull.Value? (int)reader["star"] :0;
                        rc.countstar = (int)reader["countstar"];
                    }
                    reader.Close();
                    con.Close();
                    return rc;
                }
            }
        }

        public string SqlGetStars(int id)
        {
            string s;
            s = "select r.fRestaurantID,fRestaurantName,fPhotoID,count(c1.fStars) countstar,avg(c1.fStars) as star";
            s += " from tRestaurant r";
            s += " left join tComment c1";
            s += " on r.fRestaurantID = c1.fRestaurantID";
            s += " where r.fRestaurantID =" + id + "";
            s += " group by r.fRestaurantID,fRestaurantName,fPhotoID";
            return s;
        }

        public int MemberGiveStars(int MemberID=0, int RestaurantID=0)
        {
            //若count不為0 代表已有給評價
            var count = db.tComment.Where(c => c.fMemberID == MemberID && c.fRestaurantID==RestaurantID && c.fStars != null).Count();
            return count;
        }

        public tComment MemberStars(int MemberID = 0, int RestaurantID = 0)
        {
            //若count不為0 代表已有給評價
            tComment tc = new tComment();
            var count = db.tComment.Where(c => c.fMemberID == MemberID && c.fRestaurantID == RestaurantID && c.fStars != null).Select(c=>c.fStars).SingleOrDefault();
            tc.fStars = count;           
            return tc;
        }

    }
}