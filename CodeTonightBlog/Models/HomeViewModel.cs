using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CodeTonightBlog.DAL.Common
{
    public class HomeViewModel
    {
      public List<EmployeeDashboard> EmployeeDashboardList { get; set; }
      public DashBoardCount DashBoardItem { get; set; }
        public List<TodoItem> GettodoItems { get; set; }
    }

    public class DashBoardCount
    {
        public int TotalUsers { get; set; }
        public int TotalBlogs { get; set; }
        public int LifeTimeViews { get; set; }
        public int MonthlyViews { get; set; }
        public int WeeklyViews { get; set; }
    }
}