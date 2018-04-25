using ContasOnlineModel.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ContasOnlineApi.Services
{
    public class MongoContext : IMongoContext
    {
        public MongoClient client { get;set; }
        public IMongoDatabase db { get; set; }

        public MongoContext(String connectionString, String databaseName)
        {
            this.client = new MongoClient(connectionString);
            this.db = client.GetDatabase(databaseName);
        }
    }
}
