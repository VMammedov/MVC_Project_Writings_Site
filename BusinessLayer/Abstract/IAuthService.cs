using System;
using System.Collections.Generic;
using System.Linq;
using EntityLayer.Dto;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        void Register(string adminusername, string password, string role);
        bool Login(LoginDto loginDto);
        bool WriterLogin(WriterLoginDto writerLoginDto);
        void WriterRegister(string writername, string name, string surname, string password);
    }
}
