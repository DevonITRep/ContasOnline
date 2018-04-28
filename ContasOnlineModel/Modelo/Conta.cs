using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContasOnlineModel.Modelo
{
    public class Conta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set; }
        public String NomeDaConta { get; set; }
        public Banco Banco {get;set;}
        
    }
}
