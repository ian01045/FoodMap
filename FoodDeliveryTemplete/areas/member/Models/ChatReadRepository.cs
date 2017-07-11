using FoodDeliveryTemplete.Hubs;
using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Member.Models
{
    public class ChatReadRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;
        readonly static string _staticconnString = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;

        public IEnumerable<ChatReadViewModel> GetAllMessages(int MemberID)
        {
            var messages = new List<ChatReadViewModel>();
            using (var connection = new SqlConnection(_connString))
            {                
                connection.Open();
                using (var command = new SqlCommand(@"SELECT MemberID, RestaurantID, RestaurantName, Message, Date, fFrom FROM[dbo].[ChatRoom] where [Read] = 0 and MemberID = "+MemberID+" and fFrom = 0", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    
                    var reader = command.ExecuteReader();
                    int count = CountUnRead();

                    while (reader.Read())
                    {
                        //count++;//計算未讀訊息幾則
                        messages.Add(item: new ChatReadViewModel { Message = (string)reader["Message"], CountRead = count, MemberID = (int)reader["MemberID"], RestaurantID = (int)reader["RestaurantID"], RestaurantName = reader["RestaurantName"] != DBNull.Value ? (string)reader["RestaurantName"] : "尚無聊天對象", fFrom = (bool)reader["fFrom"],Date = Convert.ToDateTime(reader["Date"]) });
                    }
                }
            }
            return messages;

        }
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        public int CountUnRead()
        {
            var count = (from i in db.ChatRoom
                        where i.Read == false
                        select i).Count();
            return count;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.ChatRead();
            }
        }
    }
}