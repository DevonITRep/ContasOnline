using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace ContasOnlineModel.Modelo
{
    public class Conta
    {

        public ObjectId _id { get; set; }
        public Decimal NomeDaConta { get; set; }
        public Banco Banco {get;set;}
        
    }
}
