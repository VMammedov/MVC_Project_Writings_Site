using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using MVCProjectCamp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectCamp.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));

        [Authorize(Roles = "A"), HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginDto loginDto)
        {
            authService.Register(loginDto.AdminUserName, loginDto.AdminPassword, loginDto.Role);
            return RedirectToAction("Index", "Login");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult WriterRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterRegister(WriterLoginDto writerLoginDto)
        {
            var response = Request["g-recaptcha-response"];
            const string secret = "6LcYaKcbAAAAAJaKBmWY57X53QToYgTNf2TriCVE";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            if (!captchaResponse.Success)
            {
                TempData["Message"] = "Lütfen güvenliği doğrulayınız.";
            }
            else
            {
                authService.WriterRegister(writerLoginDto.WriterEmail, writerLoginDto.WriterName, writerLoginDto.WriterSurName, writerLoginDto.WriterPassword);
                TempData["Message"] = "Güvenlik başarıyla doğrulanmıştır.";
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            return RedirectToAction("WriterRegister");
        }
    }
}