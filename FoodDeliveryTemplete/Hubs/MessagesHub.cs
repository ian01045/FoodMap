using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Configuration;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Hubs
{  
    [HubName("messagesHub")]
    public class MessagesHub : Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            MessagesRepository mr = new MessagesRepository();
            
            context.Clients.All.updateMessages();
        }

        [HubMethodName("ChatRoom")]
        public static void ChatRoom()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            context.Clients.All.updateMessages();
        }

        [HubMethodName("ChatRead")]
        public static void ChatRead()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            context.Clients.All.updateMessages();
        }

        [HubMethodName("RestaurantChatRoom")]
        public static void RestaurantChatRoom()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            context.Clients.All.updateMessages();
        }

        [HubMethodName("RestaurantChatRead")]
        public static void RestaurantChatRead()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            context.Clients.All.updateMessages();
        }
    }
}