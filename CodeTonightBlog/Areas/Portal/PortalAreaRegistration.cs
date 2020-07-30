using System.Web.Mvc;

namespace CodeTonightBlog.Areas.Portal
{
    public class PortalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Portal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
           
            context.MapRoute(
              "Portal_default",
              "Portal/{controller}/{action}/{id}",
              new { action = "Index", id = UrlParameter.Optional }
          );
         


            context.MapRoute(
         "Blog_default",
         "Portal/{*.}",
         new { action = "ShowBlog", id = "" },
         new { controller = "Blog" },
         new[] { "CodeTonightBlog.Areas.Portal.Controllers" }
     );
           
          
          
        }
    }
}