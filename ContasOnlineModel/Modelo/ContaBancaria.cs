using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContasOnlineModel.Modelo
{
    public class ContaBancaria
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set; }
        public String NomeDaConta { get; set; }
        public Banco Banco {get;set;}
        public TipoDeContaBancaria TipoDeConta { get; set; }
        public bool Ativa { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public String UsuarioDono { get; set; }
        public ContaApp ContaApp { get; set; }
    }
}

