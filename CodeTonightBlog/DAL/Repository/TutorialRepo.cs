using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeTonightBlog.DAL.Interfaces;
using CodeTonightBlog.DAL.Common;

namespace CodeTonightBlog.DAL.Repository
{
    public class TutorialRepo//:ITutorial
    {
        //private DataContexxt _context;
        //MongoClient client = new MongoClient("mongodb://localhost:27017");
        //IMongoDatabase data;
        //public TutorialRepo(DataContexxt datacontext)
        //{
        //    data = client.GetDatabase("DbTestingMongo");
        //    this._context = datacontext;
        //}

        //public void AddTutorials(Tutorial Topics)
        //{

        //    IMongoCollection<Tutorial> Topic = data.GetCollection<Tutorial>("TutorialTopic");
        //    Topic.InsertOne(Topics);

        //}

        //public List<Tutorial> DisplyTopic(Users user)
        //{
        //    IMongoCollection<Tutorial> DisplayTopic = data.GetCollection<Tutorial>("TutorialTopic");
        //    var myTopic = DisplayTopic.Find(new BsonDocument()).ToList().Where(x => x.user.Id.ToString() == user.Id.ToString()).ToList();
        //    return myTopic;
        //}
        //public List<Tutorial> ListTutorial(string technology)
        //{
        //    IMongoCollection<Tutorial> DisplayTopic = data.GetCollection<Tutorial>("TutorialTopic");
        //    var myTopic = DisplayTopic.Find(new BsonDocument()).ToList().Where(x => x.Technology.ToString() == technology).ToList();
        //    return myTopic;

        //}

        //public List<Tutorial> gettutorials()
        //{
        //    IMongoCollection<Tutorial> topic = data.GetCollection<Tutorial>("TutorialTopic");
        //    var topics = topic.Find(new BsonDocument()).ToList();
        //    return topics;
        //}




    }
}