using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContasOnlineApi.Services
{
    public class MongoDataAccess
    {
        MongoClient _client;
        IMongoDatabase _db;
        
        public MongoDataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("EmployeeDB");
        }


    }
}
