export class Model {

    constructor(objeto?) {
        Object.assign(this, objeto);
    }
  
  }
  
   //Classe de Despesa, esta deve ser utilizada na pagina de resitro de despesa
  export class TipoDeContaBancaria extends Model {

    _id: string;
    Nome: string;
    Ativo: boolean;
  }