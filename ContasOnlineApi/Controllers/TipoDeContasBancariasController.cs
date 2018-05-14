using ContasOnlineApi.Services;
using ContasOnlineModel.Modelo;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContasOnlineApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class TipoDeContasBancariasController : Controller
    {
        private MongoClient client;
        private IMongoDatabase db;
        

        private readonly MongoRepository _repository = null;


        public TipoDeContasBancariasController(IOptions<MongoDBSettings> settings)
        {
            _repository = new MongoRepository(settings);

        }

        // GET api/values
        [HttpGet]
        public async Task<List<TipoDeContaBancaria>> Get()
        {
            List<TipoDeContaBancaria> listaDeTipoDeContaBancarias = await _repository.tiposDeContasBancarias.Find(x => true).ToListAsync();

            return listaDeTipoDeContaBancarias;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<TipoDeContaBancaria> Get(String id)
        {
            var filter = Builders<TipoDeContaBancaria>.Filter.Eq("_id", id);
            return await _repository.tiposDeContasBancarias.Find(filter).FirstOrDefaultAsync();

        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] JObject objData)
        {
            TipoDeContaBancaria novaTipoDeContaBancaria = objData.ToObject<TipoDeContaBancaria>();
            //inserting data
            await _repository.tiposDeContasBancarias.InsertOneAsync(novaTipoDeContaBancaria);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] JObject objData)
        {

            TipoDeContaBancaria edicaoTipoDeContaBancaria = objData.ToObject<TipoDeContaBancaria>();

            var filter = Builders<TipoDeContaBancaria>.Filter.Eq("_id", edicaoTipoDeContaBancaria._id);
            var conta = _repository.tiposDeContasBancarias.Find(filter).FirstOrDefaultAsync();
            if (conta.Result == null)
                return false;
            var update = Builders<TipoDeContaBancaria>.Update
                                          .Set(x => x.Nome, edicaoTipoDeContaBancaria.Nome);
                                          
            await _repository.tiposDeContasBancarias.UpdateOneAsync(filter, update);
            return true;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete([FromBody] JObject objData)
        {

            TipoDeContaBancaria deleteTipoDeContaBancaria = objData.ToObject<TipoDeContaBancaria>();

            var filter = Builders<TipoDeContaBancaria>.Filter.Eq("_id", deleteTipoDeContaBancaria._id);
            return await _repository.tiposDeContasBancarias.DeleteOneAsync(filter);

        }
    }
}
