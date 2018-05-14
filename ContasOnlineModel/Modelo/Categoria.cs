using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContasOnlineModel.Modelo
{
    public class Categoria
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public Boolean Ativo { get; set; }
        public String UsuarioDono { get; set; }
        public ContaApp ContaApp { get; set; }

    }
}
