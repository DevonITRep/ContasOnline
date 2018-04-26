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
    public class ContasController : Controller
    {
        private MongoClient client;
        private IMongoDatabase db;

        private readonly MongoRepository _repository = null;


        public ContasController(IOptions<MongoDBSettings> settings)
        {
            _repository = new MongoRepository(settings);

        }

        // GET api/values
        [HttpGet]
        public async Task<List<Conta>> Get()
        {
            List<Conta> listaDeContas = await _repository.contas.Find(x => true).ToListAsync();

            return listaDeContas;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Conta> Get(String id)
        {
            var filter = Builders<Conta>.Filter.Eq("_id", id);
            return await _repository.contas.Find(filter).FirstOrDefaultAsync();

        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] JObject objData)
        {
            Conta novaConta = objData.ToObject<Conta>();
            //inserting data
            await _repository.contas.InsertOneAsync(novaConta);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] JObject objData)
        {

            Conta edicaoConta = objData.ToObject<Conta>();

            var filter = Builders<Conta>.Filter.Eq("_id", edicaoConta._id);
            var conta = _repository.contas.Find(filter).FirstOrDefaultAsync();
            if (conta.Result == null)
                return false;
            var update = Builders<Conta>.Update
                                          .Set(x => x.Banco, edicaoConta.Banco)
                                          .Set(x => x.NomeDaConta, edicaoConta.NomeDaConta);
                                          
            await _repository.contas.UpdateOneAsync(filter, update);
            return true;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete([FromBody] JObject objData)
        {

            Conta deleteConta = objData.ToObject<Conta>();

            var filter = Builders<Conta>.Filter.Eq("_id", deleteConta._id);
            return await _repository.contas.DeleteOneAsync(filter);

        }
    }
}
