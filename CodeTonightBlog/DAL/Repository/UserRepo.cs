using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeTonightBlog.DAL.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using CodeTonightBlog.DAL.Common;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace CodeTonightBlog.DAL.Repository
{
    public class UserRepo : GenericMasterRepo<Users>, IUserRepo
    {

        public List<Users> GetUsers()
        {
             return GetAll("sprUsers", "List");
        }
        public List<string> GetExistingUserNames()
        {
            return GetAll("sprUsers", "List").Select(x => x.Username).ToList();
        }

        public Users UserLogin(string UserName, string Password)
        {
            var DynamicParameter = new DynamicParameters();
            DynamicParameter.Add("@Activity", "Login");
            DynamicParameter.Add("@UserName", UserName);
            DynamicParameter.Add("@Password", Encrypt(Password));
            var userlist = GetList("sprUsers", DynamicParameter);

            if (userlist.Count() > 0)
            {
                Users user = userlist.First();
                return user;
            }
            else
                return new Users();
        }
        public void UserDelete(int userid)
        {
            Connection.Delete(userid, "sprUsers", "Delete", "UserId");
        }

        public bool ResendVerificationMail(string Email)
        {
            Guid activationCode = Guid.NewGuid();
            var DynamicParameter = new DynamicParameters();
            DynamicParameter.Add("@Activity", "Resend");
            DynamicParameter.Add("@Email", Email);
            DynamicParameter.Add("@ActivationCode", activationCode);

            var userlist = Insert("sprUsers", DynamicParameter);


            if (Convert.ToInt32(userlist) > 0)
            {
                SendVerificationLinkEmail(Email, activationCode);
                return true;
            }
            return false;
        }

        public long ActivateAccount(Guid ActivationCode)
        {
            var DynamicParameter = new DynamicParameters();
            DynamicParameter.Add("@Activity", "Activate");

            DynamicParameter.Add("@ActivationCode", ActivationCode);

            return Convert.ToInt64(Insert("sprUsers", DynamicParameter));

        }
        public int AddNewAccount(Users user)
        {
            var DynamicParameter = new DynamicParameters();
            DynamicParameter.Add("@Activity", "FindByMail");
            DynamicParameter.Add("@Email", user.Email);


            int exists = ExecuteScalar("sprUsers", DynamicParameter);

            Guid activationcode = Guid.NewGuid();
            user.ActivationCode = activationcode;
            user.Password = Encrypt(user.Password);
            DynamicParameter.Add("@Password", user.Password);
            DynamicParameter.Add("@Email", user.Email);
            DynamicParameter.Add("@ContactNo", user.ContactNo);
            DynamicParameter.Add("@Name", user.Name);
            DynamicParameter.Add("@Username", user.Username);
            DynamicParameter.Add("@ActivationCode", user.ActivationCode);
            DynamicParameter.Add("@Activity", "Add");
            DynamicParameter.Add("@UserRole", user.UserRole);
            DynamicParameter.Add("@IsActive", user.IsActive);
            if (exists < 1)
            {

                SendVerificationLinkEmail(user.Email, activationcode);
                Insert("sprUsers", DynamicParameter);
                // users.InsertOne(user);
            }
            return exists;
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public void SendVerificationLinkEmail(string emailID, Guid activationCode)
        {
            var verifyUrl = "/Users/VerifyAccount/" + activationCode;
            var link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("info@code2night.com", "Code2night");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "87Lkce~2"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your Code2night account is" +
                " successfully created. Please click on the below link to verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "webmail.code2night.com",
                Port = 587,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }


    }
}