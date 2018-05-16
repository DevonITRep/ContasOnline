import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';
import { SlimLoadingBarService } from 'ng2-slim-loading-bar';
import { CurrencyPipe } from '@angular/common';
import { Cartao } from '../../model/cartao';
import { NovoCartaoPage } from '../novo-cartao/novo-cartao';
import { DataService } from '../../services/dataService';



/**
 * Generated class for the CartoesPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-cartoes',
  templateUrl: 'cartoes.html',
})
export class CartoesPage {
 
  public cartoes: Cartao[];
  
  constructor(
    public navCtrl: NavController, 
    public navParams: NavParams,
    private _dataServiceCartao: DataService,
    private _slimLoadingBarService: SlimLoadingBarService,
    private _toastCtrl: ToastController,
    private currencyPipe: CurrencyPipe, 
    private toast: ToastController) {
   
      _dataServiceCartao.setServiceApiName('Cartoes');
   
  }

  getCurrency(amount: number) {
    return this.currencyPipe.transform(amount, 'EUR', true, '1.2-2');
  }

  novoCartaoDeCredito(){
    this.navCtrl.setRoot(NovoCartaoPage)
  }

  ionViewDidLoad() {
    this._slimLoadingBarService.start();
    this._dataServiceCartao
            .getAll<any[]>()
            .subscribe((data: Cartao[]) => this.cartoes = data,
            error => () => {
              let toast = this._toastCtrl.create({
                  message: 'Erro encontrado !!!',
                  duration: 3000,
                  position: 'bottom'
                });
                toast.present();
            },
            () => {
              let toast = this._toastCtrl.create({
                  message: 'Informações carregadas com sucesso !!!',
                  duration: 3000,
                  position: 'bottom'
                });
                
              toast.present();    
              this._slimLoadingBarService.complete();
            });
  }

}
