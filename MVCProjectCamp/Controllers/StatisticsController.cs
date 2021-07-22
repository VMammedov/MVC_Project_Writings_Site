using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectCamp.Controllers
{
    public class StatisticsController : Controller
    {
        Context c = new Context(); 
        public ActionResult Index1()
        {
            ViewBag.d1 = c.Categories.Count();
            var value1 = c.Categories.Where(a => a.CategoryName == "Programming").Single().CategoryID;
            ViewBag.d2 =  c.Headings.Where(a => a.CategoryID == value1).Count();
            ViewBag.d3 = (from x in c.Writers where x.WriterName.Contains("e") select x.WriterName).ToList().Count.ToString();
            var value2 = c.Headings.GroupBy(x => x.CategoryID).OrderByDescending(y => y.Count()).Select(z => z.Key).FirstOrDefault();
            ViewBag.d4 = c.Categories.Where(a => a.CategoryID == value2).Single().CategoryName;
            ViewBag.d5 = c.Categories.Count(x => x.CategoryStatus == true) - c.Categories.Count(x => x.CategoryStatus == false);
            return View();
        }
    }
}