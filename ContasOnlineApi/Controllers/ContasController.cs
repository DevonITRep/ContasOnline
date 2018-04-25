using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContasOnlineModel.Modelo;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ContasOnlineApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class ContasController : Controller
    {
        // GET api/values
        [HttpGet]
        public List<Cartao> Get()
        {
            List<Cartao> listaDeCartoes = new List<Cartao>();
        
            return listaDeCartoes;
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
