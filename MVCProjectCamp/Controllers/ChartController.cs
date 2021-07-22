using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using MVCProjectCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectCamp.Controllers
{
    public class ChartController : Controller
    {
        public ActionResult Graph1()
        {
            return View();
        }

        public ActionResult Graph2()
        {
            return View();
        }

        public ActionResult CreateGraph1()
        {
            return Json(CChart(), JsonRequestBehavior.AllowGet);
        }

        public List<CategoryChart> CChart()
        {
            List<CategoryChart> categoryCharts = new List<CategoryChart>();
            using(var context = new Context())
            {
                categoryCharts = context.Categories.Select(x => new CategoryChart
                {
                    CategoryName = x.CategoryName,
                    CategoryCount = x.Headings.Count()
                }).ToList();
            }
            return categoryCharts;
        }

        public ActionResult CreateGraph2()
        {
            return Json(HChart(), JsonRequestBehavior.AllowGet);
        }

        public List<HeadingChart> HChart()
        {
            List<HeadingChart> headingCharts = new List<HeadingChart>();
            using (var context = new Context())
            {
                headingCharts = context.Headings.Select(x => new HeadingChart
                {
                    HeadingName = x.HeadingName,
                    HeadingCount = x.Contents.Count()
                }).ToList();
            }
            return headingCharts;
        }
    }
}