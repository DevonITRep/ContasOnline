import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, DateTime,ToastController } from 'ionic-angular';

import { DataService } from '../../services/dataService';
import { Despesa } from '../../model/despesa';


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
  model: Despesa;

  public utilizaCartaoDeCredito: boolean;
  public utilizaQuantidadeDeParcelas: boolean;
  public listaDeCartoes: any[];
  public listaDeCategorias: any[];
  public listaDeContas: any[];
  
  

  constructor(
    public navCtrl: NavController, 
    public navParams: NavParams, 
    private _dataServiceCartoes: DataService,
    private _dataServiceContas: DataService,
    private _dataServiceCategoria: DataService,
    private _dataServiceDespesa: DataService,
    private toast: ToastController
  ) 
  {
    //Instancia uma nova despesa
    this.model = new Despesa();
    this.utilizaCartaoDeCredito = false;
    this.utilizaQuantidadeDeParcelas = false;
    
        
    //Retorna todas as categorias
    this.retornaTodasAsCategorias();

  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad DespesasPage');
  }

  validaCartaoDeCredito(event) {
    this.utilizaCartaoDeCredito = event.checked;
    
    if (event.checked === true)
    {
     //Instancia do DataService
      this._dataServiceCartoes.setServiceApiName('Cartoes');
    
      //Consulta todos os cartões
      this._dataServiceCartoes
      .getAll<any[]>()
      .subscribe((data: any[]) => this.listaDeCartoes = data);

      console.log('Cartoes');
    }
    else{
      //Instancia do DataService
      this._dataServiceContas.setServiceApiName('Contas');
    
       //Consulta todas as contas
       this._dataServiceContas
       .getAll<any[]>()
       .subscribe((data: any[]) => this.listaDeContas = data);
 
    }

  }

  validaParcelamento(event){
    this.utilizaQuantidadeDeParcelas = event.checked;
  }

  retornaTodasAsCategorias(){
    //Instancia do dataservice
    this._dataServiceCategoria.setServiceApiName('Categorias');
    
    //Consulta todas as categorias
    this._dataServiceCategoria
    .getAll<any[]>()
    .subscribe((data: any[]) => this.listaDeCategorias = data);
  }

  savar() {
    this.savarDespesa().subscribe((res) => 
      error => () =>{
        this.toast.create({ message: 'Erro ao salvar o produto.', duration: 3000, position: 'botton' }).present();
      },
      () => {
        this.toast.create({ message: 'Despesa salva.', duration: 3000, position: 'botton' }).present();
      });
  }

  private savarDespesa() {
    
    this._dataServiceDespesa.setServiceApiName('Despesas');
        
    if (this.model._id) {
      return this._dataServiceDespesa.update<any[]>(this.model._id,this.model);
    } else {
      return this._dataServiceDespesa.add<any[]>(this.model);
    }
  }

}

