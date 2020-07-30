using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using WebApplication9.Controllers;
using WebApplication9.DAL.Interfaces;
using WebApplication9.DAL.Repository;
using WebApplication9.Models;
using System.Data;
namespace WebApplication9.Areas.Admin.Controllers
{


    public class SlidesController : BaseController
    {
        // GET: Admin/Slides
        private IHome _home;
       
        public SlidesController()
        {
            DataContexxt dt = new DataContexxt();

            this._home = new HomeRepo(new DataContexxt());

        }

        public SlidesController(IHome hoemrepo)
        {
            _home = hoemrepo;
        }
        public ActionResult Index()
        {
            var slides=  _home.GetSlides();

            return View(slides);
        }
        public ActionResult NewSlides()
        {
           

            return View();
        }
        [HttpGet]
        public ActionResult EditSlides(string id)
        {

            var slides = _home.GetSlides().Where(x=>x.Id.ToString()==id).First();


            return View(slides);
        }

        [HttpGet]
        public ActionResult RemoveSlide(string id)
        {
            SlideShow slide = new SlideShow()
            {
                Id = id
            };
            _home.Remove(slide);



            return RedirectToAction("index","Slides"); 
        }
        [HttpPost]
        public ActionResult UpdateSlides(string Title, string Tagline,string ObjectId)
        {
            var slides = _home.GetSlides().Where(x => x.Id.ToString() == ObjectId).First();
            slides.Title = Title;
            slides.Tagline = Tagline;
            if (Request.Files.Count > 0 && Request.Files[0].FileName!="")
            {
                HttpPostedFileBase postedFile = Request.Files[0];
                string path = Server.MapPath("~/Content/images/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filename = DateTime.Now.ToString("ddMMyyyyhhmmss") + Path.GetFileName(postedFile.FileName.Replace(' ', '-'));
                postedFile.SaveAs(path + filename);
                slides.Logo = filename;
            }
            _home.UpdateSlide(slides);


            DataTable dt = new DataTable();

         
     



            return RedirectToAction("index","Slides");
        }


        [HttpPost]
        public ActionResult SaveSlides(string Title,string Tagline)
        {
           
            if(Request.Files.Count>0)
            {
                HttpPostedFileBase postedFile = Request.Files[0];
                
                string path = Server.MapPath("~/Content/images/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filename = DateTime.Now.ToString("ddMMyyyyhhmmss") + Path.GetFileName(postedFile.FileName.Replace(' ','-'));

                postedFile.SaveAs(path + filename);
              
                SlideShow Slide = new SlideShow()
                {
                    Title = Title,
                    Tagline = Tagline,
                    Logo = filename

                };
                _home.AddNewSlide(Slide);
            }
         else
            {
                ViewBag.Message = "Upload any image first";
            }



            return RedirectToAction("Index","Slides");


        }
    }
}