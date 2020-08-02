using CodeTonightBlog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeTonightBlog.Areas.Admin.Controllers
{
    [AdminAuthenticateUser]
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult EditSlide(string Id)
        {
            return View();
        }
        [ActionName("SlideShow")]
        public ActionResult MainSlides()
        {
            return View();
        }
        public ActionResult RemoveSlide(string Id)
        {
               return View();
        }
    }
}