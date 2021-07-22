using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectCamp.Controllers
{
    public class ContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContentByHeading(int id)
        {
            var contentvalue = cm.GetListByHeadingID(id);
            return View(contentvalue);
        }

        public ActionResult GetAllContent(string p)
        {
            var contentvalue = cm.GetListBySearch(p);
            return View(contentvalue);
        }
    }
}