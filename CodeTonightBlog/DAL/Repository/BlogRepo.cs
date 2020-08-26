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
using System.Data.SqlClient;
using System.Data;

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
            DynamicParameter.Add("@Activity", "ListBlogFile");
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
        public string AddBlog(Blog blog)
        {
            SqlDataAdapter adp = new SqlDataAdapter("sprBlogs",Connection.sqlConStr);
            adp.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@Activity", "Add");
            adp.SelectCommand.Parameters.AddWithValue("@BlogUrl", blog.BlogUrl);
            adp.SelectCommand.Parameters.AddWithValue("@UserID", blog.User.Id);
            adp.SelectCommand.Parameters.AddWithValue("@BlogMonth", blog.BlogMonth);
            adp.SelectCommand.Parameters.AddWithValue("@BlogBody", blog.BlogBody);
            adp.SelectCommand.Parameters.AddWithValue("@Title", blog.Title);
            adp.SelectCommand.Parameters.AddWithValue("@Categories", blog.Categories);
            adp.SelectCommand.Parameters.AddWithValue("@Tags", blog.Tags);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            var id =Convert.ToString( dt.Rows[0][0]);
            return id;
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