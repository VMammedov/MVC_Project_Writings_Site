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
    public class MessageController : Controller
    {
        MessageManager mg = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();

        public ActionResult Inbox()
        {
            string p = (string)Session["AdminUserName"];
            var messagelist = mg.GetListInbox(p);
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            string p = (string)Session["AdminUserName"];
            var messagelist = mg.GetListSendbox(p);
            return View(messagelist.Where(x => x.IsDraft == false).ToList());
        }

        public ActionResult Draftbox()
        {
            string p = (string)Session["AdminUserName"];
            var messagelist = mg.GetListDraftbox(p);
            return View(messagelist);
        }

        public ActionResult GetInBoxDetails(int id)
        {
            var messagevalue = mg.GetByID(id);
            return View(messagevalue);
        }

        public ActionResult GetSendBoxDetails(int id)
        {
            var values = mg.GetByID(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult GetDraftBoxDetails(int id)
        {
            var values = mg.GetByID(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult NewMessage(Message p, string button)
        {
            ValidationResult result = messagevalidator.Validate(p);
            if (button=="Save Draft")
            {
                if (result.IsValid)
                {
                    p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    p.SenderMail = "admin@gmail.com";
                    p.IsDraft = true;
                    mg.MessageAdd(p);
                    return RedirectToAction("Draftbox");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else if (button == "Send")
            {
                if (result.IsValid)
                {
                    p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    p.SenderMail = "admin@gmail.com";
                    p.IsDraft = false;
                    mg.MessageAdd(p);
                    return RedirectToAction("Sendbox");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            return View();
        }

        public ActionResult MarkasReadorUnRead(int id)
        {
            var messagevalue = mg.GetByID(id);
            if (messagevalue.IsRead == true)
            {
                messagevalue.IsRead = false;
            }
            else if (messagevalue.IsRead == false)
            {
                messagevalue.IsRead = true;
            }
            mg.MessageUpdate(messagevalue);
            return RedirectToAction("Inbox");
        }
    }
}