using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeTonightBlog.DAL.Common
{
    public class MyBlogs
    {
        public List<Blog> BlogsList { get; set; }
        public Blog MyBlog { get; set; }
        public string Tag { get; set; }
        public string Author { get; set; }
    }
}