using FoodDeliveryTemplete.Hubs;
using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Areas.Restaurant.Models
{
    public class RestaurantChatRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;
        readonly static string _staticconnString = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;

        public IEnumerable<ChatRoomViewModel> GetAllMessages(int MemberID, int RestaurantID)
        {
            var messages = new List<ChatRoomViewModel>();
            using (var connection = new SqlConnection(_connString))
            {
                int count = 0;
                connection.Open();
                using (var command = new SqlCommand(@"SELECT MessageID, MemberID, RestaurantID, RestaurantName, Message, fFrom FROM[dbo].[ChatRoom] where MemberID = " + MemberID + "and RestaurantID = " + RestaurantID + "", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;//計算未讀訊息幾則
                        messages.Add(item: new ChatRoomViewModel { MessageID = (int)reader["MessageID"], Message = (string)reader["Message"], CountRead = count, MemberID = (int)reader["MemberID"], RestaurantID = (int)reader["RestaurantID"], RestaurantName = reader["RestaurantName"] != DBNull.Value ? (string)reader["RestaurantName"] : "尚無聊天對象", fFrom = (bool)reader["fFrom"] });
                    }
                }
            }
            return messages;

        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.RestaurantChatRoom();
            }
        }
    }
}