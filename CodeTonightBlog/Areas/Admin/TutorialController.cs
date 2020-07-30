using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using WebApplication9.DAL.Interfaces;
using WebApplication9.DAL.Repository;
using WebApplication9.Models;

namespace WebApplication9.Areas.Admin
{
    public class TutorialController : Controller
    {

       
        private ITutorial _TutorialRepo;
        //   private IHome _homerepo;
        public TutorialController()
        {
            DataContexxt dt = new DataContexxt();
            this._TutorialRepo = new TutorialRepo(new DataContexxt());
        }

        public TutorialController(ITutorial TutorialRepo)
        {
            _TutorialRepo = TutorialRepo;
            
        }
        
        // GET: Admin/Tutorial
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  JsonResult Addtutorial(Tutorial tut)
        {
            _TutorialRepo.AddTutorials(tut);
            return Json("tutorial", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Addtutorial()
        {
           return View(_TutorialRepo.gettutorials());
        }
        
    }
}