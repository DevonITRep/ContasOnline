import { DateTime } from 'ionic-angular';
import { Banco } from './banco';

export class Model {

    constructor(objeto?) {
        Object.assign(this, objeto);
    }
  
  }
  
   //Classe de Despesa, esta deve ser utilizada na pagina de resitro de despesa
  export class ContaBancaria extends Model {

    _id: string;
    NomeDaConta : string;
    Banco : Banco;
    TipoDeConta : object;
    Ativa: boolean;
    DataDeCadastro: DateTime;
    UsuarioDono: string;
    ContaApp: object;

  }