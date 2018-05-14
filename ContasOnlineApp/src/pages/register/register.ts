import {Component} from "@angular/core";
import {NavController} from "ionic-angular";
import {LoginPage} from "../login/login";
import {HomePage} from "../home/home";
import { Device } from '@ionic-native/device';
import { DataService } from '../../services/dataService';
import { Usuario } from '../../model/usuario';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import * as firebase from 'firebase/app';
import { LoadingService } from '../../services/loading-service'


@Component({
  selector: 'page-register',
  templateUrl: 'register.html'
})
export class RegisterPage {
  model: Usuario;
  signupError: string;
	form: FormGroup;


  //Modelo de deviceInfo
  deviceInfo : { language: string, timezone: number,  game_version: string,  device_os:string,  device_type:number,  device_model:string  };
    
  //Objeto de Usuario + Device
  usuarioDevice : {Usuario : object , Device: object};

  //Retorno do cadastro
  retorno : {};


  constructor(
    public nav: NavController,
    private device: Device ,  
    private _dataServiceOneSignal: DataService, 
    private _dataServiceUsuario: DataService,
    fb: FormBuilder,
    private navCtrl: NavController,
    private auth: AuthService,
    private loading : LoadingService
  ) 
  {
     //Instancia uma nova despesa
    this.model = new Usuario();

    this.form = fb.group({
      fullname: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
			email: ['', Validators.compose([Validators.required, Validators.email])],
			password: ['', Validators.compose([Validators.required, Validators.minLength(6)])]
		});

    this._dataServiceOneSignal.setServiceApiName('OneSignal');
    this._dataServiceUsuario.setServiceApiName('Usuarios');
  }

  // register and go to home page
  register() {

    var device_type = 0;

    if ( this.device.platform == "Android"){
      device_type = 1; 
    }
    else if (this.device.platform == "iOS"){
      device_type = 0; 
    }

    //Informações do DEVICE
    this.deviceInfo = {language : "en",  timezone : -28800 , game_version : "", device_os : this.device.version , device_type : device_type , device_model : this.device.manufacturer };

    //Cria o vinculo do USUARIO com o DEVICE
    this.usuarioDevice = {Usuario : this.model, Device : this.deviceInfo };

    this.nav.setRoot(HomePage);
  }

  signup() {
		let data = this.form.value;
		let credentials = {
			email: data.email,
			password: data.password
    };
    
    this.loading.showLoader('Gravando as informações, aguarde !!');
    
		this.auth.signUp(credentials).then((userInfo: firebase.User) => {
        
        //Dados do Usuário
        this.model.email =data.email;
        this.model.nomeCompleto = data.fullname;
        this.model.senha = data.password;
        this.model.fireBaseUserUID = userInfo.uid;
        this.model.tokenOneSignal = '';
        
        //Cria um novo usuario na Base do ContasOnline
        this._dataServiceUsuario.add(this.model).subscribe(
          (response) => {}, 
          (error) =>{}, 
          () => {
            
            console.log('cadastro realizado com sucesso !!');
            this.loading.hideLoader();
            this.navCtrl.setRoot(HomePage)
          }
        )
      }  
     ,
      error => { 
        this.signupError = error.message
        console.log('cadastro realizado com sucesso !!');
        this.loading.hideLoader();
      }
		);
  }


  // go to login page
  login() {
    this.nav.setRoot(LoginPage);
  }
}