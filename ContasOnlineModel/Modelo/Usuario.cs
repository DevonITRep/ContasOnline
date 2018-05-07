using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContasOnlineModel.Modelo
{
    public class Usuario
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set; }
        public String NomeCompleto { get; set; }
        public String Login { get; set; }
        public String Senha { get; set; }
        public String Email { get; set; }
        public String TokenOneSignal { get; set; }
        public String FireBaseUserUID { get; set; }
    }
}
