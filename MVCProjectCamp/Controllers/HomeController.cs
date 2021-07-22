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
    public class HomeController : Controller
    {

        ContactManager cm = new ContactManager(new EfContactDal());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        [AllowAnonymous, HttpGet]
        public ActionResult HomePage()
        {
            return View();
        }

        [AllowAnonymous, HttpPost]
        public ActionResult HomePage(Contact p)
        {
            p.ContactDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.IsRead = false;
            cm.ContactAdd(p);
            return View();
        }
    }
}