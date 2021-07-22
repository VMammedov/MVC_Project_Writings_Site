using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
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
    public class TalentController : Controller
    {
        // GET: Talent

        TalentManager tm = new TalentManager(new EfTalentDal());
        TalentValidator tv = new TalentValidator();

        public ActionResult Index()
        {
            var talentvalue = tm.GetList();
            return View(talentvalue);
        }

        [HttpGet]
        public ActionResult AddTalent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTalent(Talent p)
        {
            ValidationResult result = tv.Validate(p);
            if (result.IsValid)
            {
                p.TalentStatus = true;
                tm.TalentAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditTalent(int id)
        {
            var talentvalue = tm.GetByID(id);
            return View(talentvalue);
        }

        [HttpPost]
        public ActionResult EditTalent(Talent p)
        {
            ValidationResult result = tv.Validate(p);
            if (result.IsValid)
            {
                p.TalentStatus = true;
                tm.TalentUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTalent(int id)
        {
            var talentvalue = tm.GetByID(id);
            tm.TalentDelete(talentvalue);
            return RedirectToAction("Index");
        }
    }
}