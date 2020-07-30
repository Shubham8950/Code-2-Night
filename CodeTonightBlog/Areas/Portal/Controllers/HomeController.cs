using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeTonightBlog.Controllers;
namespace CodeTonightBlog.Areas.Portal.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Portal/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
    }
}