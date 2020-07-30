using CodeTonightBlog.DAL.Common;
using CodeTonightBlog.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeTonightBlog.DAL.Repository
{
    public class FeedbackRepo : GenericMasterRepo<Feedback>, IFeedback
    {
        public string SaveFeedback(Feedback feedback)
        {
            var DynamicParameter = new DynamicParameters();
            DynamicParameter.Add("@Activity", "Insert");
            DynamicParameter.Add("@Email", feedback.Email);
            DynamicParameter.Add("@Job", feedback.Job);
            DynamicParameter.Add("@JobIndustry", feedback.JobIndustry);
            DynamicParameter.Add("@Name", feedback.Name);
            DynamicParameter.Add("@Employment", feedback.Employment);
            DynamicParameter.Add("@Skill", feedback.Skill);
            DynamicParameter.Add("@Experience", feedback.Experience);
            var result = Insert("sprFeedback", DynamicParameter); 
            return result;
        }
    }
}