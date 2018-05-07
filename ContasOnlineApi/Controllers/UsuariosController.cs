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
using OneSignal.SDK;
using OneSignal.SDK.Resources.Devices;

namespace ContasOnlineApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private MongoClient client;
        private IMongoDatabase db;

        private readonly MongoRepository _repository = null;
        private readonly OneSignalSettings _oneSignalSettings = null;



        public UsuariosController(IOptions<MongoDBSettings> settings, IOptions<OneSignalSettings> settingsOneSignal)
        {
            _repository = new MongoRepository(settings);
            _oneSignalSettings = settingsOneSignal.Value;

        }

        // GET api/values
        [HttpGet]
        public async Task<List<Usuario>> Get()
        {
            List<Usuario> listaDeCartoes = await _repository.usuarios.Find(x => true).ToListAsync();
            
            return listaDeCartoes;
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Usuario> Get(String id)
        {
            var filter = Builders<Usuario>.Filter.Eq("_id", id);
            return await _repository.usuarios.Find(filter).FirstOrDefaultAsync();
            
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] JObject objData)
        {
            //Dados do usuario
            Usuario usuario = objData.ToObject<Usuario>();
            
            var filter = Builders<Usuario>.Filter.Eq("FireBaseUserUID", usuario.FireBaseUserUID);
            var usuarioPesquisa = _repository.usuarios.Find(filter).FirstOrDefaultAsync();

            if (usuarioPesquisa.Result == null)
            {
                //inserting data
                await _repository.usuarios.InsertOneAsync(usuario);
            }
            else {
                var update = Builders<Usuario>.Update
                                              .Set(x => x.FireBaseUserUID, usuario.FireBaseUserUID);

            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] JObject objData)
        {

            Usuario edicaoUsuario = objData.ToObject<Usuario>();

            var filter = Builders<Usuario>.Filter.Eq("_id", edicaoUsuario._id);
            var banco = _repository.usuarios.Find(filter).FirstOrDefaultAsync();
            if (banco.Result == null)
                return false;
            var update = Builders<Usuario>.Update
                                          .Set(x => x.Email, edicaoUsuario.Email)
                                          .Set(x => x.Login, edicaoUsuario.Login)
                                          .Set(x => x.NomeCompleto, edicaoUsuario.NomeCompleto)
                                          .Set(x => x.Senha, edicaoUsuario.Senha)
                                          .Set(x => x.TokenOneSignal, edicaoUsuario.TokenOneSignal);

            await _repository.usuarios.UpdateOneAsync(filter, update);
            return true;

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete([FromBody] JObject objData)
        {

            Usuario deleteUsuario = objData.ToObject<Usuario>();

            var filter = Builders<Usuario>.Filter.Eq("_id", deleteUsuario._id);
            return await _repository.usuarios.DeleteOneAsync(filter);

        }
    }
}
