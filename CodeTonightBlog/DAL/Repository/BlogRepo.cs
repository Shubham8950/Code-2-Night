using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.Models;
//using MongoDB.Driver;
//using MongoDB.Bson;
using System.IO;
using System.Threading.Tasks;
using CodeTonightBlog.DAL.Common;
using Dapper;
namespace CodeTonightBlog.DAL.Repository
{
    public class BlogRepo :GenericMasterRepo<Blog>, IBlog
    {
        //    private DataContexxt _context;
        //    MongoClient client = new MongoClient("mongodb://localhost:27017");
        //    IMongoDatabase data;

        //    public BlogRepo(DataContexxt datacontext)
        //    {
        //        data = client.GetDatabase("DbTestingMongo");
        //        this._context = datacontext;
        //    }
        //    public List<SlideShow> GetSlides()
        //    {
        //        IMongoCollection<SlideShow> slides = data.GetCollection<SlideShow>("Slides");
        //        var slide = slides.Find(new BsonDocument()).ToList();
        //        return slide;
        //    }


        public List<Blog> GetBlogs()
        {
            var DynamicParameter = new DynamicParameters();
            DynamicParameter.Add("@Activity", "List");
            var blog = GetList("sprBlogs", DynamicParameter);
            return blog.ToList();
        }
        public void BlogDelete(int Id)
        {
            Connection.Delete(Id, "sprBlogs", "Delete", "Id");
        }

        public List<Blog> GetMyBlogs(Users user)
        {
            var DynamicParameter =new DynamicParameters();
            DynamicParameter.Add("@Activity", "BlogsByUser");
            DynamicParameter.Add("@UserId", user.UserID);
            var myblog = GetList("sprBlogs", DynamicParameter);
            return myblog.ToList();
        }
        public List<Tag> Tag()
        {
            return GetTableById("sprTags", "List").DataTableToList<Tag>();
        }
        public Blog BlogDetail(string name)
        {
            var DynamicParameter = new DynamicParameters();
            DynamicParameter.Add("@Activity", "BlogsByURL");
            DynamicParameter.Add("@BlogUrl", name);
            var blog = GetList("sprBlogs", DynamicParameter);
            return blog.ToList().FirstOrDefault();
        }
        public void AddBlog(Blog blog)
        {
            var DynamicParameter = new DynamicParameters();
            DynamicParameter.Add("@Activity", "Add");
            DynamicParameter.Add("@BlogUrl", blog.BlogUrl);
            DynamicParameter.Add("@UserID", blog.User.Id);
            DynamicParameter.Add("@BlogMonth", blog.BlogMonth);
            DynamicParameter.Add("@BlogBody", blog.BlogBody);
            DynamicParameter.Add("@Title", blog.Title);
            DynamicParameter.Add("@Categories", blog.Categories);
            DynamicParameter.Add("@Tags", blog.Tags);
            var result=Insert("sprBlogs", DynamicParameter);

        }


        //    private bool disposed = false;
        //    protected virtual void Dispose(bool disposing)
        //    {
        //        if (!this.disposed)
        //        {
        //            if (disposing)
        //            {
        //                _context.Dispose();
        //            }
        //        }
        //        this.disposed = true;
        //    }
        //    public void Dispose()
        //    {
        //        Dispose(true);
        //        GC.SuppressFinalize(this);
        //    }
    }
}