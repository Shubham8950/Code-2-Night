using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.DAL.Repository;

namespace CodeTonightBlog.Areas.Admin.Controllers
{
    public class BlogsController : Controller
    {
        private IBlog _blogrespo;
        
        //  private IHome _homerepo;

        public BlogsController()
        {
            this._blogrespo = new BlogRepo();
            //this._homerepo = new HomeRepo());
        }

        public BlogsController(IBlog blogrespo)
        {
            _blogrespo = blogrespo;
            //_homerepo = homerepo;
        }


        public ActionResult Index()
        {
            var GetBlogs = _blogrespo.GetBlogs();
            return View(GetBlogs);
        }

        [HttpGet]
        public ActionResult Delete(int Blogid)
        {
            _blogrespo.BlogDelete(Blogid);

            return RedirectToAction("Index");
        }
    }
}