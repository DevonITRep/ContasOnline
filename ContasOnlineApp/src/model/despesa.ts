import { DateTime } from 'ionic-angular';

export class Model {

    constructor(objeto?) {
        Object.assign(this, objeto);
    }
  
  }
  
   //Classe de Despesa, esta deve ser utilizada na pagina de resitro de despesa
  export class Despesa extends Model {

    _id: string;
    UtilizouCartaoDeCredito: boolean;
    CartaoUtilizado: object;
    DespesaParcelada: boolean;
    QuantidadeDeParcelas: number;
    ContaDeSaida: object;
    Categoria: object;
    Valor: number;
    Observacao: string;
    DataDeCadastro: DateTime;

  }