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
    public class ContasBancariasController : Controller
    {
        private MongoClient client;
        private IMongoDatabase db;

        private readonly MongoRepository _repository = null;


        public ContasBancariasController(IOptions<MongoDBSettings> settings)
        {
            _repository = new MongoRepository(settings);

        }

        // GET api/values
        [HttpGet]
        public async Task<List<ContaBancaria>> Get()
        {
            List<ContaBancaria> listaDeContas = await _repository.contas.Find(x => true).ToListAsync();

            return listaDeContas;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ContaBancaria> Get(String id)
        {
            var filter = Builders<ContaBancaria>.Filter.Eq("_id", id);
            return await _repository.contas.Find(filter).FirstOrDefaultAsync();

        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] JObject objData)
        {
            ContaBancaria novaConta = objData.ToObject<ContaBancaria>();
            //Atualiza Data De Cadastro
            novaConta.DataDeCadastro = DateTime.Now;

            //inserting data
            await _repository.contas.InsertOneAsync(novaConta);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] JObject objData)
        {

            ContaBancaria edicaoConta = objData.ToObject<ContaBancaria>();

            var filter = Builders<ContaBancaria>.Filter.Eq("_id", edicaoConta._id);
            var conta = _repository.contas.Find(filter).FirstOrDefaultAsync();
            if (conta.Result == null)
                return false;
            var update = Builders<ContaBancaria>.Update
                                          .Set(x => x.Banco, edicaoConta.Banco)
                                          .Set(x => x.TipoDeConta, edicaoConta.TipoDeConta)
                                          .Set(x => x.Ativa, edicaoConta.Ativa)
                                          .Set(x => x.NomeDaConta, edicaoConta.NomeDaConta);
                                          
            await _repository.contas.UpdateOneAsync(filter, update);
            return true;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete([FromBody] JObject objData)
        {

            ContaBancaria deleteConta = objData.ToObject<ContaBancaria>();

            var filter = Builders<ContaBancaria>.Filter.Eq("_id", deleteConta._id);
            return await _repository.contas.DeleteOneAsync(filter);

        }
    }
}
