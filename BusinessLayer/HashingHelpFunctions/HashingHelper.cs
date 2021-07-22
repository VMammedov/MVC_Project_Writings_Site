using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.HashingHelpFunctions
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string userName, string userPassword, out byte[] userNameHash, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                userNameHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userName));
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userPassword));
            }
        }

        public static void WriterCreatePasswordHash(string userPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userPassword));
            }
        }

        public static bool WriterVerifyPasswordHash(string userPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userPassword));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static bool VerifyPasswordHash(string userName, string userPassword, byte[] passwordHash, byte[] passwordSalt, byte[] userNameHash)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userPassword));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                var computedUserNameHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userName));
                for (int i = 0; i < computedUserNameHash.Length; i++)
                {
                    if (computedUserNameHash[i] != userNameHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
