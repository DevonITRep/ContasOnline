import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { DataService } from '../../services/dataService';
import { SlimLoadingBarService } from 'ng2-slim-loading-bar';
import { ToastController } from 'ionic-angular';
import { CurrencyPipe } from '@angular/common';
import { Cartao } from '../../model/cartao';
import { LoadingService } from '../../services/loading-service'
import { CartoesPage } from '../cartoes/cartoes';

/**
 * Generated class for the NovoCartaoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-novo-cartao',
  templateUrl: 'novo-cartao.html',
})
export class NovoCartaoPage {
  model: Cartao;

  constructor(
    public navCtrl: NavController, 
    public navParams: NavParams, 
    private _dataServiceCartao: DataService,
    private _slimLoadingBarService: SlimLoadingBarService,
    private _toastCtrl: ToastController,
    private currencyPipe: CurrencyPipe, 
    private toast: ToastController,
    private loading : LoadingService) {
    
     //Instancia uma novo cartao
     this.model = new Cartao();

    //Instancia do DataService
    this._dataServiceCartao.setServiceApiName('Cartoes');
    
   
  }

 
  savar() {
   
    this.loading.showLoader('Gravando as informações, aguarde !!');
   
        
    if (this.model._id) {
       //atualiza o cadastro do Cartão
       return this._dataServiceCartao.update(this.model._id,this.model).subscribe(
        (response) => {}, 
        (error) =>{
          this.toast.create({ message: 'Erro ao salvar o cartão.', duration: 3000, position: 'botton' }).present();
        }, 
        () => {
          this.toast.create({ message: 'Cartão salva.', duration: 3000, position: 'botton' }).present();
          this.loading.hideLoader();
          this.navCtrl.setRoot(CartoesPage)
        }
      )

    } else {
      
      //Cria um novo cartão de crédito
      return this._dataServiceCartao.add(this.model).subscribe(
        (response) => {}, 
        (error) =>{
          this.toast.create({ message: 'Erro ao salvar o cartão.', duration: 3000, position: 'botton' }).present();
        }, 
        () => {
          this.toast.create({ message: 'Despesa salva.', duration: 3000, position: 'botton' }).present();
          this.loading.hideLoader();
          this.navCtrl.setRoot(CartoesPage)
        }
      )


    }
  }


  ionViewDidLoad() {
    console.log('ionViewDidLoad NovoCartaoPage');
  }

}
