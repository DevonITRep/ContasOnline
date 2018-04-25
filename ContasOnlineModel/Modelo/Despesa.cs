using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace ContasOnlineModel.Modelo
{
    public class Despesa
    {

        public ObjectId _id { get; set; }
        public Boolean UtilizouCartaoDeCredito {get;set;}
        public Boolean DespesaParcelada {get;set;}
        public Int32 QuantidadeDeParcelas {get;set;}
        public Conta ContaDeSaida {get;set;}
        public Decimal Valor {get;set;}
        public String Observacao {get;set;}
        
    }
}