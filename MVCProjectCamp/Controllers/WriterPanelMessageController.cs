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
    public class WriterPanelMessageController : Controller
    {

        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();

        public ActionResult Inbox()
        {
            string p = (string)Session["WriterEmail"];
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterEmail"];
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist.Where(x => x.IsDraft == false).ToList());
        }

        public ActionResult Draftbox()
        {
            string p = (string)Session["WriterEmail"];
            var messagelist = mm.GetListDraftbox(p);
            return View(messagelist);
        }

        public ActionResult GetInBoxDetails(int id)
        {
            var messagevalue = mm.GetByID(id);
            return View(messagevalue);
        }

        public ActionResult GetSendBoxDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult GetDraftBoxDetails(int id)
        {
            var values = mm.GetByID(id);
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
            string sender = (string)Session["WriterEmail"];
            ValidationResult result = messagevalidator.Validate(p);
            p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.SenderMail = sender;
            if (button == "Save as Draft")
            {
                if (result.IsValid)
                {
                    p.IsDraft = true;
                    mm.MessageAdd(p);
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
                    p.IsDraft = false;
                    mm.MessageAdd(p);
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
            else if (button == "Save Draft")
            {
                if (result.IsValid)
                {
                    p.IsDraft = true;
                    mm.MessageUpdate(p);
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
            return View();
        }

        public ActionResult MarkasReadorUnRead(int id)
        {
            var messagevalue = mm.GetByID(id);
            if (messagevalue.IsRead == true)
            {
                messagevalue.IsRead = false;
            }
            else if (messagevalue.IsRead == false)
            {
                messagevalue.IsRead = true;
            }
            mm.MessageUpdate(messagevalue);
            return RedirectToAction("Inbox");
        }

        public PartialViewResult MessageListMenu()
        {
            string p = (string)Session["WriterEmail"];
            List<int> msgcounter = new List<int>();
            msgcounter.Add(mm.GetListInbox(p).Count());
            msgcounter.Add(mm.GetListSendbox(p).Count());
            msgcounter.Add(mm.GetListDraftbox(p).Count());
            return PartialView(msgcounter);
        }
    }
}