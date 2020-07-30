using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.DAL.Repository;
namespace CodeTonightBlog.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private IUserRepo _userrepo;
        //  private IHome _homerepo;
       
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
            var getusr = _userrepo.GetUsers();
            return View(getusr);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _userrepo.UserDelete(id);
            
            return RedirectToAction("Index");
        }    
	}
}