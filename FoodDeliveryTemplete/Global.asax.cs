using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodDeliveryTemplete
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SqlDependency.Start(connString);
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }
    }
}
