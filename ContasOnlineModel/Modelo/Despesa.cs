using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContasOnlineModel.Modelo
{
    public class Despesa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set; }
        public Boolean UtilizouCartaoDeCredito { get; set; }
        public Cartao CartaoUtilizado { get; set; }
        public Boolean DespesaParcelada { get; set; }
        public Int32 QuantidadeDeParcelas { get; set; }
        public Conta ContaDeSaida { get; set; }
        public Categoria Categoria { get; set; }
        public Decimal Valor { get; set; }
        public String Observacao { get; set; }
        public DateTime DataDeCadastro { get; set; }

    }
}