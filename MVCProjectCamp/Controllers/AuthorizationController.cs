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
    public class AuthorizationController : Controller
    {
        AdminManager am = new AdminManager(new EfAdminDal());

        [Authorize(Roles = "A")]
        public ActionResult Index()
        {
            var adminvalues = am.GetList();
            return View(adminvalues);
        }

        [HttpGet]
        public ActionResult ChangeAdmminRole(int id)
        {
            var adminvalue = am.GetByID(id);
            return PartialView(adminvalue);
        }

        [HttpPost]
        public ActionResult ChangeAdmminRole(Admin p)
        {            
            am.AdminUpdate(p);
            return RedirectToAction("Index");
        }

        public ActionResult PassiveAdmin(int id)
        {
            var adminvalue = am.GetByID(id);
            am.AdminDelete(adminvalue);
            return RedirectToAction("Index");
        }

        public ActionResult ActivateAdmin(int id)
        {
            var adminvalue = am.GetByID(id);
            adminvalue.AdminStatus = true;
            am.AdminUpdate(adminvalue);
            return View();
        }
        
    }
}