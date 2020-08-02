using CodeTonightBlog.DAL.Common;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.DAL.Repository;
using CodeTonightBlog.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services;

namespace CodeTonightBlog.Controllers
{
    public class UsersController : BaseController
    {
        // GET: User

        private IUserRepo _userrepo;
      //  private IHome _homerepo;
        HomeViewModel home = new HomeViewModel();
        public UsersController()
        {
           this._userrepo = new UserRepo();
           //this._homerepo = new HomeRepo());
        }

        public UsersController(IUserRepo userrepo)
        {
            _userrepo = userrepo;
             //_homerepo = homerepo;
        }
        public ActionResult Index()
        {
            var list = _userrepo.GetUsers();
            return View(home);
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SqlServerTutorial()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            var list = _userrepo.GetExistingUserNames();
            return View(list);
        }
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult VerifyAccount(Guid Id)
        {
            ViewBag.Activated = _userrepo.ActivateAccount(Id);
            return View();
        }

        [HttpPost]
        public JsonResult ResendVerificationMail(string Email)
        {
            var result = _userrepo.ResendVerificationMail(Email);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [WebMethod]
        [HttpPost]
        public JsonResult UserFetch(string Name)
        {
            var list = _userrepo.GetExistingUserNames();
            return Json(list.Contains(Name), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Blog(string id)
        {
             return View();
        }


        [HttpPost]
        public ActionResult Login(string UserName, string Password, bool Remember)
        {
            var user = _userrepo.UserLogin(UserName, Password);
            if (user.Username == null)
                return Json(1, JsonRequestBehavior.AllowGet);
            else
            {
                int timeout = Remember ? 525600 : 60; // 525600 min = 1 year
                var ticket = new FormsAuthenticationTicket(user.Email, Remember, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                Session["User"] = JsonConvert.SerializeObject(user);
                Session["UserName"] = user.Username;
                return Json(new string[] { "1", user.UserRole }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public PartialViewResult SuccessFulRegistration()
        {
             return PartialView("~/Views/Shared/_SuccessfulRegistration.cshtml");
        }

        [WebMethod]
        [HttpPost]
        public JsonResult Registerations(string Name, string Email, string Contact, string Username, string Password)
        {
            Users user = new Users()
            {
                Username = Username,
                Email = Email,
                ContactNo = Contact,
                Password = Password,
                Name = Name,
                UserRole = "Author",
                IsActive = false

            };
            int i = _userrepo.AddNewAccount(user);
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MultipleFilesDownload()
        {
            return View();
        }

        //
        [AdminAuthenticateUser]
        public ActionResult UserLists()
        {
            var getusr = _userrepo.GetUsers();
            return View(getusr);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _userrepo.UserDelete(id);

            return RedirectToAction("UserLists");
        }

    }
}