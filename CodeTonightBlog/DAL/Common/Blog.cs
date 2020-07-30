using CodeTonightBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeTonightBlog.DAL.Common
{
    public class Blog
    {
        // [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Categories { get; set; }
        public DateTime Date { get; set; }
        [AllowHtml]
        public string BlogBody { get; set; }

        [AllowHtml]
        public string VideoEmbed { get; set; }
        public string BlogUrl { get; set; }
        public string BlogMonth { get; set; }
        public Users User { get; set; }
        public string Tags { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }



    }
}