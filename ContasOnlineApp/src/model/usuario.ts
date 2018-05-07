export class Model {

  constructor(objeto?) {
      Object.assign(this, objeto);
  }

}

export class Usuario extends Model {
    nomeCompleto: string; 
    email: string; 
    senha: string; 
    tokenOneSignal: string;
    fireBaseUserUID: string;
}