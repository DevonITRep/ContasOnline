using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContasOnlineModel.Modelo
{
    public class Cartao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set; }
        public Decimal Limite { get; set; }
        public Decimal PrevisaoDeFatura { get; set; }
        public String BandeiraImg { get; set; }
        public String Bandeira { get; set; }
        public Int32 Vencimento { get; set; }
        public String NomeDoCartao { get; set; }
    }
}
