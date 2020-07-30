using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeTonightBlog.DAL.Common
{

    public class Tutorial
    {
        public object Id { get; set; }

        public string TopicName { get; set; }
        public string Technology { get; set; }
        public Users user { get; set; }

        public DateTime Topicdate { get; set; }
    }
}