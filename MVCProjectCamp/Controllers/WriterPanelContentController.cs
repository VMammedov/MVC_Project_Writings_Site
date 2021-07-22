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
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent

        ContentManager cm = new ContentManager(new EfContentDal());
        WriterManager wm = new WriterManager(new EfWriterDal());

        public ActionResult MyContent()
        {
            string p = (string)Session["WriterEmail"];
            int writeridinfo = wm.GetSessionID(p);
            var contentvalue = cm.GetListByWriter(writeridinfo);
            return View(contentvalue);
        }

        public ActionResult ContentByHeading(int id)
        {
            var contentvalue = cm.GetListByHeadingID(id);
            return View(contentvalue);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.d = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content p)
        {
            p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            string mail = (string)Session["WriterEmail"];
            p.WriterID = wm.GetSessionID(mail);
            p.ContentStatus = true;
            cm.ContentAdd(p);
            return RedirectToAction("MyContent");
        }
    }
}