using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeTonightBlog.DAL.Common
{
    public class Users
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string UserRole { get; set; }
        public Boolean IsActive { get; set; }
        [Phone]
        public string ContactNo { get; set; }
        public Guid ActivationCode { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}