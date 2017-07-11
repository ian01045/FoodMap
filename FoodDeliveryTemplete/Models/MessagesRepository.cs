using FoodDeliveryTemplete.Hubs;
using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoodDeliveryTemplete.Models
{
    public class MessagesRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;
        readonly static string _staticconnString = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;

        public IEnumerable<MessageBoardViewModel> GetAllMessages(int restaurantID)
        {
            var messages = new List<MessageBoardViewModel>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT[MessageID], [Message], [EmptyMessage], [Date], fRestaurantID, fMemberID, fMemberName FROM[dbo].[Messages] where fRestaurantID=" + restaurantID + " order by Date desc ", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {                        
                        messages.Add(item: new MessageBoardViewModel { MessageID = (int)reader["MessageID"], Message = (string)reader["Message"], EmptyMessage = reader["EmptyMessage"] != DBNull.Value ? (string)reader["EmptyMessage"] : "", Date = Convert.ToDateTime(reader["Date"]), fMemberID = reader["fMemberID"] != DBNull.Value ? (int)reader["fMemberID"] : 0, MemberName =reader["fMemberName"] != DBNull.Value ? (string)reader["fMemberName"]:"匿名使用者" });
                    }
                }

            }
            return messages;


        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
            }
        }           
        
    }
}