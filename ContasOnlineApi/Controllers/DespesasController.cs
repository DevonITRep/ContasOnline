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
    public class DespesasController : Controller
    {
        private readonly MongoRepository _repository = null;
        
        public DespesasController(IOptions<MongoDBSettings> settings)
        {
            _repository = new MongoRepository(settings);

        }

        // GET api/values
        [HttpGet]
        public async Task<List<Despesa>> Get()
        {
            List<Despesa> listaDeDespesas = await _repository.despesas.Find(x => true).ToListAsync();

            return listaDeDespesas;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Despesa> Get(String id)
        {
            var filter = Builders<Despesa>.Filter.Eq("_id", id);
            return await _repository.despesas.Find(filter).FirstOrDefaultAsync();

        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] JObject objData)
        {
            Despesa novaDespesa = objData.ToObject<Despesa>();
            //Atualiza Data De Cadastro
            novaDespesa.DataDeCadastro = DateTime.Now;

            //inserting data
            await _repository.despesas.InsertOneAsync(novaDespesa);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] JObject objData)
        {

            Despesa edicaoDespesa = objData.ToObject<Despesa>();

            var filter = Builders<Despesa>.Filter.Eq("_id", edicaoDespesa._id);
            var conta = _repository.despesas.Find(filter).FirstOrDefaultAsync();
            if (conta.Result == null)
                return false;
            var update = Builders<Despesa>.Update
                                          .Set(x => x.ContaDeSaida, edicaoDespesa.ContaDeSaida)
                                          .Set(x => x.DespesaParcelada, edicaoDespesa.DespesaParcelada)
                                          .Set(x => x.Observacao, edicaoDespesa.Observacao)
                                          .Set(x => x.QuantidadeDeParcelas, edicaoDespesa.QuantidadeDeParcelas)
                                          .Set(x => x.UtilizouCartaoDeCredito, edicaoDespesa.UtilizouCartaoDeCredito)
                                          .Set(x => x.Valor, edicaoDespesa.Valor);
                                          
            await _repository.despesas.UpdateOneAsync(filter, update);
            return true;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete([FromBody] JObject objData)
        {

            Despesa deleteDespesa = objData.ToObject<Despesa>();

            var filter = Builders<Despesa>.Filter.Eq("_id", deleteDespesa._id);
            return await _repository.despesas.DeleteOneAsync(filter);

        }
    }
}
