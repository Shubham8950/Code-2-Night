using CodeTonightBlog.DAL.Common;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.DAL.Repository;
using CodeTonightBlog.Models;
using Microsoft.Security.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            return View("index", blog);
        }
        public ActionResult BindBlogs()
        {
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs().ToList();
            foreach (var Blog in blog.BlogsList)
            {
                var body = "";
                using (StreamReader reader = new StreamReader(Server.MapPath("/BlogFiles/Blog_" + Blog.Id + ".txt")))
                {
                    body = reader.ReadToEnd();
                }
                Blog.BlogBody = body;

                //string textfile = Blog.BlogBody;
                //string fullPath = Server.MapPath("/BlogFiles/Blog_" + Blog.Id + ".txt");
                //using (StreamWriter writer = new StreamWriter(fullPath))
                //{
                //    writer.WriteLine(textfile);
                //}
            }
            return PartialView("_BlogsList", blog);
        }
        [AuthenticateUser]
        public ActionResult AddBlog()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddBlog(Blog blog)
        {
            string textfile = blog.BlogBody;

            Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            string Blogurl = Sanitizer.GetSafeHtmlFragment(blog.Title).Trim().Replace(' ', '-').Replace(".", "");
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
            var id = _blogrepo.AddBlog(myblog);
            string fullPath = Server.MapPath("/BlogFiles/Blog_" + id + ".txt");
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine(textfile);
            }
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
            foreach (var blog in myblogs)
            {
                var body = "";
                using (StreamReader reader = new StreamReader(Server.MapPath("/BlogFiles/Blog_" + blog.Id + ".txt")))
                {
                    body = reader.ReadToEnd();
                }
                blog.BlogBody = body;
            }
            return View(myblogs);
        }
        //[AuthenticateUser]
        public ActionResult MyBlog(string id)
        {

            string body = string.Empty;
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs();
            foreach (var blogDetail in blog.BlogsList)
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("/BlogFiles/Blog_" + blogDetail.Id + ".txt")))
                {
                    body = reader.ReadToEnd();
                }
                blogDetail.BlogBody = body;
            }

            blog.MyBlog = _blogrepo.BlogDetail(id);
          
            using (StreamReader reader = new StreamReader(Server.MapPath("/BlogFiles/Blog_" + blog.MyBlog.Id + ".txt")))
            {
                body = reader.ReadToEnd();
            }
            blog.MyBlog.BlogBody = body;

            return View("BlogDetail", blog);
        }
        //[AuthenticateUser]
        public ActionResult Tags(string id)
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs().Where(x => x.Tags.Contains(id)).ToList();
            blog.Tag = Sanitizer.GetSafeHtmlFragment(id);
            foreach (var blogDetail in blog.BlogsList)
            {
                string body = "";
                using (StreamReader reader = new StreamReader(Server.MapPath("/BlogFiles/Blog_" + blogDetail.Id + ".txt")))
                {
                    body = reader.ReadToEnd();
                }
                blogDetail.BlogBody = body;
            }
            return View("Tags", blog);
        }
        [ChildActionOnly]
        public ActionResult TagsList()
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.Tags = string.Join(",", _blogrepo.Tag().Select(x => x.Tags)).Split(',').ToList();
            return PartialView("_Tags", blog);
        }
        [AuthenticateUser]
        public ActionResult Author(string id)
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs().Where(x => x.AuthorId.ToString().Contains(id.Split('-').First())).ToList();
            blog.Author = id.Split('-').Last();
            foreach (var blogDetail in blog.BlogsList)
            {
                string body = "";
                using (StreamReader reader = new StreamReader(Server.MapPath("/BlogFiles/Blog_" + blogDetail.Id + ".txt")))
                {
                    body = reader.ReadToEnd();
                }
                blogDetail.BlogBody = body;
            }
            return View("Tags", blog);
        }
        [AdminAuthenticateUser]
        public ActionResult Listing()
        {
            var GetBlogs = _blogrepo.GetBlogs();
            foreach (var blogDetail in GetBlogs)
            {
                string body = "";
                using (StreamReader reader = new StreamReader(Server.MapPath("/BlogFiles/Blog_" + blogDetail.Id + ".txt")))
                {
                    body = reader.ReadToEnd();
                }
                blogDetail.BlogBody = body;
            }
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