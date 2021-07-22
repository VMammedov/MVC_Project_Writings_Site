using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectCamp.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv = new ContactValidator();
        MessageManager mm = new MessageManager(new EfMessageDal());

        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }

        public PartialViewResult ContactPartial()
        {
            return PartialView();
        }

        public PartialViewResult MessageListMenu()
        {
            string p = (string)Session["AdminUserName"];
            List<int> msgcounter = new List<int>();
            msgcounter.Add(cm.GetList().Count());
            msgcounter.Add(mm.GetListInbox(p).Count());
            msgcounter.Add(mm.GetListSendbox(p).Count());
            msgcounter.Add(mm.GetListDraftbox(p).Count());
            return PartialView(msgcounter);
        }

        public ActionResult MarkasReadorUnRead(int id)
        {
            var contactvalue = cm.GetByID(id);
            if (contactvalue.IsRead == true)
            {
                contactvalue.IsRead = false;
            }
            else if (contactvalue.IsRead == false)
            {
                contactvalue.IsRead = true;
            }
            cm.ContactUpdate(contactvalue);
            return RedirectToAction("Index");
        }
    }
}