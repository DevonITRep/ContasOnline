import { Facebook } from '@ionic-native/facebook';
import { Component } from '@angular/core';
import { Usuario } from '../../model/Usuario';
import { UsuarioService } from '../../services/UsuarioService';


@IonicPage()
@Component({
  selector: 'page-autenticacao',
  templateUrl: 'autenticacao.html'
})

export class AutenticacaoPage {
    public facebook: Facebook;

    constructor(private usuarioService: UsuarioService) {
    //atribuicao do pacote do facebook
    //public facebook: Facebook;
  }
    
  

//método para chamar api do facebook e salvar no banco o usuario    
loginFacebook() {
  let permissions = new Array<string>();
  permissions = ["public_profile", "email"];

  this.facebook.login(permissions).then((response) => {
    let params = new Array<string>();

    this.facebook.api("/me?fields=name,email", params)
      .then(res => {

        //estou usando o model para criar os usuarios
        let usuario = new Usuario();
        usuario.nome = res.name;
        usuario.email = res.email;
        usuario.senha = res.id;
        usuario.login = res.email;

        this.logar(usuario);
      }, (error) => {
        alert(error);
        console.log('ERRO LOGIN: ', error);
      })
  }, (error) => {
    alert(error);
  });
}

  logar(usuario: Usuario) {
    this.usuarioService.salvarFacebook(usuario);
    
  }
}
