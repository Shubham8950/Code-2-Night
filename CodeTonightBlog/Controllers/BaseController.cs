using CodeTonightBlog.DAL.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeTonightBlog.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
         
        // GET: Base
      

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            //Logging the Exception
            filterContext.ExceptionHandled = true;


            var Result = this.View("Error", new HandleErrorInfo(exception,
            filterContext.RouteData.Values["controller"].ToString(),
            filterContext.RouteData.Values["action"].ToString()));
            ErroLog(filterContext);
            filterContext.Result = Result;


            //Redirect or return a view, but not both.
            //filterContext.Result = RedirectToAction("Index", "ErrorHandler");
            //// OR 
            //filterContext.Result = new ViewResult
            //{
            //    ViewName = "~/Views/ErrorHandler/Index.cshtml"
            //};

        }

        public static void ErroLog(ExceptionContext exception)
        {
           if(!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/log")))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/log"));
            }
            var filename = System.Web.HttpContext.Current.Server.MapPath("~/log/LogFile.txt");
            var sw = new System.IO.StreamWriter(filename, true);
            sw.WriteLine(DateTime.Now.ToString() + " " + exception.Exception.Message + " " + exception.Exception.InnerException);
            sw.Close();
        }
    }
    public class AuthenticateUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!ISValidLogin())
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                        {"controller", "Users"},
                        {"action", "Login"}
                });
                return;
            }

        }
        public bool ISValidLogin()
        {
            if (HttpContext.Current.Session["User"] != null)
            {

                return true;
            }
            else
            {
                Encrypt.CloseUserSession();
                return false;
            }

        }
    }
    public class AdminAuthenticateUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!ISValidLogin())
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                        {"controller", "Users"},
                        {"action", "Login"}
                });
                return;
            }

        }
        public bool ISValidLogin()
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                var chkrole =JsonConvert.DeserializeObject<Users>(HttpContext.Current.Session["User"].ToString());

                if(chkrole.UserRole=="Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Encrypt.CloseUserSession();
                return false;
            }

        }
    }
}
