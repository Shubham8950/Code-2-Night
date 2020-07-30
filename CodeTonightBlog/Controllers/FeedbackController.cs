using CodeTonightBlog.DAL.Common;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeTonightBlog.Controllers
{
    public class FeedbackController : Controller
    {
        private IFeedback _feedback;
       
        public FeedbackController()
        {
            this._feedback = new FeedbackRepo();
            //this._homerepo = new HomeRepo());
        }

        public FeedbackController(IFeedback feedback)
        {
            _feedback = feedback;
            //_homerepo = homerepo;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveFeedback(Feedback feedback )
        {
            var result = _feedback.SaveFeedback(feedback);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}