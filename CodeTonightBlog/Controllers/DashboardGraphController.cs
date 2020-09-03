using CodeTonightBlog.DAL.Common;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeTonightBlog.Controllers
{
    [AdminAuthenticateUser]
    public class DashboardGraphController : BaseController
    {
        // GET: DashboardGraph
     
        private IBlog _blogrepo;
        //   private IHome _homerepo;
        HomeViewModel home = new HomeViewModel();
        public DashboardGraphController()
        {
            this._blogrepo = new BlogRepo();
        }

        public DashboardGraphController(IBlog blogrepo)
        {
            _blogrepo = blogrepo;
        }
        public ActionResult Index()
        {
            var obj = new HomeViewModel();
            obj.EmployeeDashboardList= _blogrepo.employeeDashboards();
            obj.DashBoardItem = _blogrepo.EmployeeDashboardsCount();
            obj.GettodoItems = _blogrepo.GetToDo();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Additems(TodoItem todoItem)
        {
            _blogrepo.AddItem(todoItem);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult SaveUpdateItem(TodoItem todoItem)
        {
            _blogrepo.SaveUpdateItem(todoItem);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }
    }
}