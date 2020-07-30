﻿using CodeTonightBlog.DAL.Common;
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
        public ActionResult Blog()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddBlog(string Title, string Tags, string BlogBody, string VideoEmbed)
        {
            Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            string Blogurl = Sanitizer.GetSafeHtmlFragment(Title).Replace(' ', '-');
            Blog myblog = new Blog()
            {
                Title = Title,
                Tags = Tags,
                BlogBody = BlogBody,
                Date = DateTime.Now,
                User = new Users() { Name = user.Name, Email = user.Email, Id = user.Id },
                BlogUrl = Blogurl,
                BlogMonth = DateTime.Now.ToString("MMM-yyyy"),
                VideoEmbed = VideoEmbed
            };
            _blogrepo.AddBlog(myblog);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MyBlogs()
        {
            Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            var myblogs = _blogrepo.GetMyBlogs(user);
            return View(myblogs);
        }

        public ActionResult MyBlog(string id)
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs();
            blog.MyBlog = _blogrepo.BlogDetail(id);
            return View("BlogDetail", blog);
        }
        public ActionResult Blogs(string id)
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs().ToList();
            return View("Blogs", blog);
        }
        public ActionResult Tags(string id)
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs().Where(x => x.Tags.Contains(id)).ToList();
            blog.Tag = Sanitizer.GetSafeHtmlFragment(id);
            return View("Tags", blog);
        }
        public ActionResult Author(string id)
        {
            //Users user = JsonConvert.DeserializeObject<Users>(Convert.ToString(Session["User"]));
            MyBlogs blog = new MyBlogs();
            blog.BlogsList = _blogrepo.GetBlogs().Where(x => x.User.Id.ToString().Contains(id.Split('-').First())).ToList();
            blog.Author = id.Split('-').Last();
            return View("Tags", blog);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "User");

        }
    }
}