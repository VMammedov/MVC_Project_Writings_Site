using BusinessLayer.Abstract;
using EntityLayer.Dto;
using BusinessLayer.HashingHelpFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class AuthManager : IAuthService
    {
        IAdminService _adminService;
        IWriterService _writerService;

        public AuthManager(IAdminService adminService, IWriterService writerService)
        {
            _adminService = adminService;
            _writerService = writerService;
        }

        public bool Login(LoginDto loginDto)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var usernamehash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.AdminUserName));
                var uservalue = _adminService.GetList();
                foreach (var item in uservalue)
                {
                    if(HashingHelper.VerifyPasswordHash(loginDto.AdminUserName, loginDto.AdminPassword, item.AdminPasswordHash, item.AdminPasswordSalt, item.UserName) && item.AdminStatus==true)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void Register(string adminusername, string password, string role)
        {
            byte[] userNameHash, passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(adminusername, password, out userNameHash, out passwordHash, out passwordSalt);
            var admin = new Admin
            {
                UserName = userNameHash,
                AdminPasswordHash = passwordHash,
                AdminPasswordSalt = passwordSalt,
                AdminRole = role,
                AdminStatus =true,
            };
            _adminService.AdminAdd(admin);
        }

        public bool WriterLogin(WriterLoginDto writerLoginDto)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var writervalue = _writerService.GetList();
                foreach (var item in writervalue)
                {
                    if (HashingHelper.WriterVerifyPasswordHash(writerLoginDto.WriterPassword, item.WriterPasswordHash, item.WriterPasswordSalt) && writerLoginDto.WriterEmail==item.WriterMail && item.WriterStatus==true)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void WriterRegister(string mail, string name, string surname, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.WriterCreatePasswordHash(password, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterMail = mail,
                WriterPasswordHash = passwordHash,
                WriterPasswordSalt = passwordSalt,
                WriterStatus = true,
                WriterName = name,
                WriterSurName = surname,
            };
            _writerService.WriterAdd(writer);
        }
    }
}