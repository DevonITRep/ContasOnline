import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { DataService } from '../../services/dataService';
import { SlimLoadingBarService } from 'ng2-slim-loading-bar';
import { ToastController } from 'ionic-angular';
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
  
  public cartoes: any[];

  constructor(
    public navCtrl: NavController, 
    public navParams: NavParams,
    private _dataService: DataService,
    private _slimLoadingBarService: SlimLoadingBarService,
    private _toastCtrl: ToastController ) {
      _dataService.setServiceApiName('Cartoes');
  }

  ionViewDidLoad() {
    this._slimLoadingBarService.start();
    this._dataService
            .getAll<any[]>()
            .subscribe((data: any[]) => this.cartoes = data,
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
              console.log('Dados :=' + this.cartoes[0].bandeira);    
              this._slimLoadingBarService.complete();
            });
  }

}
