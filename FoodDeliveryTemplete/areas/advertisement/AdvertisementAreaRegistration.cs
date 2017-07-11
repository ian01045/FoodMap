using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Advertisement
{
    public class AdvertisementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Advertisement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Advertisement_default",
                "Advertisement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}