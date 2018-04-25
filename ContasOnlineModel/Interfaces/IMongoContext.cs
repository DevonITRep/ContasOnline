using System;
using MongoDB.Driver;

namespace ContasOnlineModel.Interfaces
{
    public interface IMongoContext
    {
        MongoClient client{get;set;}
        IMongoDatabase db {get;set;}
    }
}