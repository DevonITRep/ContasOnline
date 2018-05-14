import { DateTime } from 'ionic-angular';

export class Model {

    constructor(objeto?) {
        Object.assign(this, objeto);
    }
  
  }
  
   //Classe de Despesa, esta deve ser utilizada na pagina de resitro de despesa
  export class Cartao extends Model {

    _id: string;
    Limite: number;
    PrevisaoDeFatura: number;
    BandeiraImg: string;
    Bandeira: string;
    Vencimento: number;
    NomeDoCartao: string;
    DataDeCadastro: DateTime;
    Ativo: boolean;
    UsuarioDono: string;
    ContaApp: object;
  }