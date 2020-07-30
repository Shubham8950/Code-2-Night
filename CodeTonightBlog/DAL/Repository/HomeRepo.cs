using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.DAL.Common;
//using MongoDB.Driver;
//using MongoDB.Bson;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CodeTonightBlog.DAL.Repository
{
    public class HomeRepo// : IHome
    {
        //private DataContexxt _context;
        //MongoClient client = new MongoClient("mongodb://localhost:27017");
        //IMongoDatabase data;

        //public HomeRepo(DataContexxt datacontext)
        //{
        //    data = client.GetDatabase("DbTestingMongo");
        //    this._context = datacontext;
        //}
        //public List<SlideShow> GetSlides()
        //{
        //    IMongoCollection<SlideShow> slides = data.GetCollection<SlideShow>("Slides");
        //    var slide = slides.Find(new BsonDocument()).ToList();
        //    return slide;
        //}
        
        //public void Remove(SlideShow slide)
        //{
        //     ObjectId objectId = ObjectId.Parse(slide.Id.ToString());
        //     IMongoCollection<SlideShow> slides = data.GetCollection<SlideShow>("Slides");
        //     var filter = Builders<SlideShow>.Filter.Eq("_id", objectId);
        //     var result = slides.DeleteOne(filter).DeletedCount;

        //}
        //public  void AddNewSlide(SlideShow slide)
        //{
        //    IMongoCollection<SlideShow> Allslides = data.GetCollection<SlideShow>("Slides");
        //    Allslides.InsertOne(slide);
         
        //}
        //public void UpdateSlide(SlideShow slide)
        //{
        //    IMongoCollection<SlideShow> Allslides = data.GetCollection<SlideShow>("Slides");
        //    Allslides.UpdateOne(o => o.Id == slide.Id, Builders<SlideShow>.Update.Set(o => o.Tagline, slide.Tagline).Set(o => o.Title, slide.Title).Set(o => o.Logo, slide.Logo));
        //}

        //private bool disposed = false;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}