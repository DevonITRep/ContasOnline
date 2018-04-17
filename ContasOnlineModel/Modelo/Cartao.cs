using System;
using System.Collections.Generic;
using System.Text;

namespace ContasOnlineModel.Modelo
{
    public class Cartao
    {

        public Decimal Limite { get; set; }
        public Decimal PrevisaoDeFatura { get; set; }
        public String BandeiraImg { get; set; }
        public String Bandeira { get; set; }
        public Int32 Vencimento { get; set; }

    }
}
