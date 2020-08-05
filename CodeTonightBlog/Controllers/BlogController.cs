using CodeTonightBlog.DAL.Common;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.DAL.Repository;
using CodeTonightBlog.Models;
using Microsoft.Security.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeTonightBlog.Controllers
{
    public class BlogController : BaseController
    {
        // GET: Portal/Blog
        private IBlog _blogrepo;
        //   private IHome _homerepo;
        HomeViewModel home = new HomeViewModel();
        public BlogController()
        {
            this._blogrepo = new BlogRepo();
        }

        public BlogController(IBlog blogrepo)
        {
            _blogrepo = blogrepo;
        }
        public ActionResult Index()
        {
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs().ToList();
            return View("index", blog);
        }
        [AuthenticateUser]
        public ActionResult AddBlog()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddBlog(Blog blog)
        {
            Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            string Blogurl = Sanitizer.GetSafeHtmlFragment(blog.Title).Trim().Replace(' ', '-').Replace(".","");
            Blog myblog = new Blog()
            {
                Title = blog.Title,
                Tags = blog.Tags,
                BlogBody = blog.BlogBody,
                Date = DateTime.Now,
                User = new Users() { Name = user.Name, Email = user.Email, Id = user.UserID },
                BlogUrl = Blogurl,
                BlogMonth = DateTime.Now.ToString("MMM-yyyy"),
                VideoEmbed = blog.VideoEmbed,
                AuthorId = Convert.ToString(user.UserID),
            };
            _blogrepo.AddBlog(myblog);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }

        
      
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Users");

        }
        
        [AuthenticateUser]
        public ActionResult MyBlogs()
        {
            Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            var myblogs = _blogrepo.GetMyBlogs(user);
            return View(myblogs);
        }
        [AuthenticateUser]
        public ActionResult MyBlog(string id)
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs();
            blog.MyBlog = _blogrepo.BlogDetail(id);

            return View("BlogDetail", blog);
        }
        [AuthenticateUser]
        public ActionResult Tags(string id)
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs().Where(x => x.Tags.Contains(id)).ToList();
            blog.Tag = Sanitizer.GetSafeHtmlFragment(id);



            return View("Tags", blog);
        }
        [ChildActionOnly]
        public ActionResult TagsList()
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.Tags = string.Join(",",_blogrepo.Tag().Select(x=>x.Tags)).Split(',').ToList();
            return PartialView("_Tags", blog);
        }
        [AuthenticateUser]
        public ActionResult Author(string id)
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs().Where(x => x.AuthorId.ToString().Contains(id.Split('-').First())).ToList();
            blog.Author = id.Split('-').Last();

            return View("Tags", blog);
        }
        [AdminAuthenticateUser]
        public ActionResult Listing()
        {
            var GetBlogs = _blogrepo.GetBlogs();
            return View(GetBlogs);
        }

        [HttpGet]
        public ActionResult Delete(int Blogid)
        {
            _blogrepo.BlogDelete(Blogid);
            return RedirectToAction("Listing");
        }
    }
}