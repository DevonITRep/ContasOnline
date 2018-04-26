import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { DataService } from '../../services/dataService';

/**
 * Generated class for the DespesasPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-despesas',
  templateUrl: 'despesas.html',
})
export class DespesasPage {

  public utilizaCartaoDeCredito: boolean;
  public utilizaQuantidadeDeParcelas: boolean;
  public listaDeCartoes: any[];
  public listaDeCategorias: any[];
  

  constructor(
    public navCtrl: NavController, 
    public navParams: NavParams, 
    private _dataServiceCartoes: DataService,
    private _dataServiceContas: DataService,
    private _dataServiceCategoria: DataService
  ) 
  {
    this.utilizaCartaoDeCredito = false;
    this.utilizaQuantidadeDeParcelas = false;
    this._dataServiceCartoes.setServiceApiName('Cartoes');
    this._dataServiceContas.setServiceApiName('Contas');
    this._dataServiceCategoria.setServiceApiName('Categorias');
    
    //Retorna todas as categorias
    this.retornaTodasAsCategorias();

  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad DespesasPage');
  }

  validaCartaoDeCredito(event) {
    this.utilizaCartaoDeCredito = event.checked;

    //Consulta todos os cartões
    this._dataServiceCartoes
    .getAll<any[]>()
    .subscribe((data: any[]) => this.listaDeCartoes = data);

  }

  validaParcelamento(event){
    this.utilizaQuantidadeDeParcelas = event.checked;
  }

  retornaTodasAsCategorias(){
    //Consulta todas as categorias
    this._dataServiceCategoria
    .getAll<any[]>()
    .subscribe((data: any[]) => this.listaDeCategorias = data);
  }

}
