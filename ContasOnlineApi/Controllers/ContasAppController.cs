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
    public class ContasAppController : Controller
    {
        private MongoClient client;
        private IMongoDatabase db;

        private readonly MongoRepository _repository = null;
        

        public ContasAppController(IOptions<MongoDBSettings> settings)
        {
            _repository = new MongoRepository(settings);
            
        }

        // GET api/values
        [HttpGet]
        public async Task<List<ContaApp>> Get()
        {
            List<ContaApp> listaDeCartoes = await _repository.contasApp.Find(x => true).ToListAsync();
            
            return listaDeCartoes;
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ContaApp> Get(String id)
        {
            var filter = Builders<ContaApp>.Filter.Eq("_id", id);
            return await _repository.contasApp.Find(filter).FirstOrDefaultAsync();
            
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] JObject objData)
        {
            ContaApp novoContaApp = objData.ToObject<ContaApp>();
            //inserting data
            await _repository.contasApp.InsertOneAsync(novoContaApp);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] JObject objData)
        {

            ContaApp edicaoContaApp = objData.ToObject<ContaApp>();

            var filter = Builders<ContaApp>.Filter.Eq("_id", edicaoContaApp._id);
            var banco = _repository.contasApp.Find(filter).FirstOrDefaultAsync();
            if (banco.Result == null)
                return false;
            var update = Builders<ContaApp>.Update
                                          .Set(x => x.Ativa, edicaoContaApp.Ativa)
                                          .Set(x => x.DataDeUltimoAcesso, DateTime.Now)
                                          .Set(x => x.UsuariosCompartilhados, edicaoContaApp.UsuariosCompartilhados);

            await _repository.contasApp.UpdateOneAsync(filter, update);
            return true;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete([FromBody] JObject objData)
        {

            ContaApp deleteContaApp = objData.ToObject<ContaApp>();

            var filter = Builders<ContaApp>.Filter.Eq("_id", deleteContaApp._id);
            return await _repository.contasApp.DeleteOneAsync(filter);

        }
    }
}
