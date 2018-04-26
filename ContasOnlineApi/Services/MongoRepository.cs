using ContasOnlineModel.Modelo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContasOnlineApi.Services
{
    public class MongoRepository
    {

        //delcaring mongo db
        private readonly IMongoDatabase _database;

        public MongoRepository(IOptions<MongoDBSettings> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                if (client != null)
                    _database = client.GetDatabase(settings.Value.Database);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to MongoDb server.", ex);
            }

        }

        public IMongoCollection<Banco> bancos => _database.GetCollection<Banco>("Bancos");
        public IMongoCollection<Cartao> cartoes => _database.GetCollection<Cartao>("Cartoes");
        public IMongoCollection<Conta> contas => _database.GetCollection<Conta>("Contas");
        public IMongoCollection<Categoria> categorias => _database.GetCollection<Categoria>("Categorias");





    }
}
