using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContasOnlineModel.Modelo
{
    public class ContaApp
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public DateTime DataDeUltimoAcesso { get; set; }
        public Boolean Ativa { get; set; }
        public Usuario UsuarioDono { get; set; }
        public List<Usuario> UsuariosCompartilhados { get; set; }
    }
}
