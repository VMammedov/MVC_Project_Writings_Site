using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MVCProjectCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectCamp.Controllers
{
    public class ScheduleController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal()); 

        // GET: Schedule
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Schedule());
        }

        public JsonResult GetEvents()
        {
            var events = new List<Schedule>();
            DateTime Start = DateTime.Now;
            DateTime End = DateTime.Now;

            foreach (var item in hm.GetList())
            {
                events.Add(
                    new Schedule()
                    {
                        title = item.HeadingName,
                        start = item.HeadingDate.AddDays(1),
                        end = item.HeadingDate,
                        allDay = false,
                    });
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}