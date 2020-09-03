//using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeTonightBlog.DAL.Common;
 

namespace CodeTonightBlog.DAL.Interfaces
{
    public interface IBlog 
    {

        List<Blog> GetBlogs();
        string AddBlog(Blog blog);
        List<Blog> GetMyBlogs(Users user);
        List<Tag> Tag();
        Blog BlogDetail(string name);
        void BlogDelete(int Blogid);
        List<EmployeeDashboard> employeeDashboards();
        DashBoardCount EmployeeDashboardsCount();
        string AddItem(TodoItem todoItem);
        string SaveUpdateItem(TodoItem todoItem);
        List<TodoItem> GetToDo();

        //List<SlideShow> GetSlides ();
        //void AddNewSlide(SlideShow slide);
        //void UpdateSlide(SlideShow slide);
        //void Remove(SlideShow slide);
        //long ActivateAccount(Guid ActivationCode);
        //List<string> GetExistingUserNames();
        //Users UserLogin(string UserName, string Password);
        //bool ResendVerificationMail(string Email);
    }
}
