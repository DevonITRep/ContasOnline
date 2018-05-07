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
using OneSignal.SDK.Serializers;
using RestSharp;

namespace ContasOnlineApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class OneSignalController : Controller
    {
        private MongoClient client;
        private IMongoDatabase db;

        private readonly MongoRepository _repository = null;
        private readonly OneSignalSettings _oneSignalSettings = null;


        public OneSignalController(IOptions<MongoDBSettings> settings, IOptions<OneSignalSettings> settingsOneSignal)
        {
            _repository = new MongoRepository(settings);
            _oneSignalSettings = settingsOneSignal.Value;
        }

        // POST api/values
        [HttpPost]
        public async Task<DeviceAddResult> Post([FromBody] JObject objData)
        {
            UsuarioDeviceOneSignal usuarioDevice = objData.ToObject<UsuarioDeviceOneSignal>();
            
            //OneSignalClient clientOnesignal = new OneSignalClient(_oneSignalSettings.RestKey, _oneSignalSettings.RestUrl);
            

            DeviceAddOptions optionDevide = usuarioDevice.Device;
            try
            {
                optionDevide.AppId = new Guid(_oneSignalSettings.ApiKey);

                DeviceAddResult resultAdd = OneSignalDeviceAdd(optionDevide);


                if (resultAdd.IsSuccess == true)
                {
                    //Consulta os dados do usuario para associar o Token do OneSignal
                    var filter = Builders<Usuario>.Filter.Eq("_id", usuarioDevice.Usuario._id);
                    var usuario = _repository.usuarios.Find(filter).FirstOrDefaultAsync();

                    var update = Builders<Usuario>.Update
                                                  .Set(x => x.TokenOneSignal, resultAdd.Id);
                    //Atualiza o Token do usuario
                    await _repository.usuarios.UpdateOneAsync(filter, update);

                }

                return resultAdd;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private DeviceAddResult OneSignalDeviceAdd(DeviceAddOptions options)
        {
            var restRequest = new RestRequest("players", Method.POST);

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", string.Format("Basic {0}", _oneSignalSettings.RestKey));

            restRequest.RequestFormat = DataFormat.Json;
            restRequest.JsonSerializer = new NewtonsoftJsonSerializer();
            restRequest.AddBody(options);

            RestClient restClient = new RestClient(_oneSignalSettings.RestUrl);
            
            restClient.Timeout = -1;

            var restResponse = restClient.Execute<DeviceAddResult>(restRequest);

            if (restResponse.ErrorException != null)
            {
                throw restResponse.ErrorException;
            }

            return restResponse.Data;
        }



    }
}
