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
    public class CategoriasController : Controller
    {
        private MongoClient client;
        private IMongoDatabase db;

        private readonly MongoRepository _repository = null;


        public CategoriasController(IOptions<MongoDBSettings> settings)
        {
            _repository = new MongoRepository(settings);

        }

        // GET api/values
        [HttpGet]
        public async Task<List<Categoria>> Get()
        {
            List<Categoria> listaDeCategorias = await _repository.categorias.Find(x => true).ToListAsync();

            return listaDeCategorias;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Categoria> Get(String id)
        {
            var filter = Builders<Categoria>.Filter.Eq("_id", id);
            return await _repository.categorias.Find(filter).FirstOrDefaultAsync();

        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] JObject objData)
        {
            Categoria novaCategoria = objData.ToObject<Categoria>();
            //inserting data
            await _repository.categorias.InsertOneAsync(novaCategoria);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] JObject objData)
        {

            Categoria edicaoCategoria = objData.ToObject<Categoria>();

            var filter = Builders<Categoria>.Filter.Eq("_id", edicaoCategoria._id);
            var categoria = _repository.categorias.Find(filter).FirstOrDefaultAsync();
            if (categoria.Result == null)
                return false;
            var update = Builders<Categoria>.Update
                                          .Set(x => x.Nome, edicaoCategoria.Nome);
                                          
            await _repository.categorias.UpdateOneAsync(filter, update);
            return true;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete([FromBody] JObject objData)
        {

            Categoria deleteCategoria = objData.ToObject<Categoria>();

            var filter = Builders<Categoria>.Filter.Eq("_id", deleteCategoria._id);
            return await _repository.categorias.DeleteOneAsync(filter);

        }
    }
}
