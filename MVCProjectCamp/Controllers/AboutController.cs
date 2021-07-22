using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectCamp.Controllers
{
    public class AboutController : Controller
    {
        AboutManager abm = new AboutManager(new EfAboutDal());
        public ActionResult Index()
        {
            var aboutvalues = abm.GetList();
            return View(aboutvalues);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About p)
        {
            abm.AboutAdd(p);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }

        public ActionResult BeActive(int id)
        {
            var aboutvalue = abm.GetByID(id);
            aboutvalue.AboutStatus = true;
            abm.AboutUpdate(aboutvalue);
            return RedirectToAction("Index");
        }

        public ActionResult BePassive(int id)
        {
            var aboutvalue = abm.GetByID(id);
            aboutvalue.AboutStatus = false;
            abm.AboutUpdate(aboutvalue);
            return RedirectToAction("Index");
        }
    }
}