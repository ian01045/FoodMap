using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.yuwang
{
    public class yuwangAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "yuwang";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "yuwang_default",
                "yuwang/{controller}/{action}/{id}",
                new { controller = "Select",action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}