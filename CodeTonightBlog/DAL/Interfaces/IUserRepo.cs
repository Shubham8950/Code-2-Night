//using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeTonightBlog.DAL.Common;
 

namespace CodeTonightBlog.DAL.Interfaces
{
    public interface IUserRepo 
    {
        List<Users> GetUsers ();
        int AddNewAccount(Users user);
        long ActivateAccount(Guid ActivationCode);
        List<string> GetExistingUserNames();
        Users UserLogin(string UserName, string Password);
        bool ResendVerificationMail(string Email);
        void UserDelete(int userid);
    }
}
