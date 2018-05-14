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
    public class BancosController : Controller
    {
        private readonly MongoRepository _repository = null;

        public BancosController(IOptions<MongoDBSettings> settings)
        {
            _repository = new MongoRepository(settings);
            
        }

        // GET api/values
        [HttpGet]
        public async Task<List<Banco>> Get()
        {
            List<Banco> listaDeCartoes = await _repository.bancos.Find(x => true).ToListAsync();
            
            return listaDeCartoes;
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Banco> Get(String id)
        {
            var filter = Builders<Banco>.Filter.Eq("_id", id);
            return await _repository.bancos.Find(filter).FirstOrDefaultAsync();
            
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] JObject objData)
        {
            Banco novoBanco = objData.ToObject<Banco>();
            //inserting data
            await _repository.bancos.InsertOneAsync(novoBanco);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] JObject objData)
        {

            Banco edicaoBanco = objData.ToObject<Banco>();

            var filter = Builders<Banco>.Filter.Eq("_id", edicaoBanco._id);
            var banco = _repository.bancos.Find(filter).FirstOrDefaultAsync();
            if (banco.Result == null)
                return false;
            var update = Builders<Banco>.Update
                                          .Set(x => x.Nome, edicaoBanco.Nome);

            await _repository.bancos.UpdateOneAsync(filter, update);
            return true;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete([FromBody] JObject objData)
        {

            Banco deleteBanco = objData.ToObject<Banco>();

            var filter = Builders<Banco>.Filter.Eq("_id", deleteBanco._id);
            return await _repository.bancos.DeleteOneAsync(filter);

        }
    }
}
