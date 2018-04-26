using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContasOnlineApi.Services;
using ContasOnlineModel.Modelo;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ContasOnlineApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class CartoesController : Controller
    {
        private MongoClient client;
        private IMongoDatabase db;

        private readonly MongoRepository _repository = null;


        public CartoesController(IOptions<MongoDBSettings> settings)
        {
            _repository = new MongoRepository(settings);

        }

        // GET api/values
        [HttpGet]
        public async Task<List<Cartao>> Get()
        {
            List<Cartao> listaDeCartoes = await _repository.cartoes.Find(x => true).ToListAsync();

            return listaDeCartoes;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Cartao> Get(String id)
        {
            var filter = Builders<Cartao>.Filter.Eq("_id", id);
            return await _repository.cartoes.Find(filter).FirstOrDefaultAsync();

        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] JObject objData)
        {
            Cartao novoCartao = objData.ToObject<Cartao>();
            //inserting data
            await _repository.cartoes.InsertOneAsync(novoCartao);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] JObject objData)
        {

            Cartao edicaoCartao = objData.ToObject<Cartao>();

            var filter = Builders<Cartao>.Filter.Eq("_id", edicaoCartao._id);
            var banco = _repository.cartoes.Find(filter).FirstOrDefaultAsync();
            if (banco.Result == null)
                return false;
            var update = Builders<Cartao>.Update
                                          .Set(x => x.Bandeira, edicaoCartao.Bandeira)
                                          .Set(x => x.BandeiraImg, edicaoCartao.BandeiraImg)
                                          .Set(x => x.Limite, edicaoCartao.Limite)
                                          .Set(x => x.PrevisaoDeFatura, edicaoCartao.PrevisaoDeFatura)
                                          .Set(x => x.Vencimento, edicaoCartao.Vencimento)
                                          .Set(x => x.NomeDoCartao, edicaoCartao.NomeDoCartao);

            await _repository.cartoes.UpdateOneAsync(filter, update);
            return true;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete([FromBody] JObject objData)
        {

            Cartao deleteCartao = objData.ToObject<Cartao>();

            var filter = Builders<Cartao>.Filter.Eq("_id", deleteCartao._id);
            return await _repository.cartoes.DeleteOneAsync(filter);

        }
    }
}
