import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { FinalizarPagamentoPage } from './finalizar-pagamento';

@NgModule({
  declarations: [
    FinalizarPagamentoPage,
  ],
  imports: [
    IonicPageModule.forChild(FinalizarPagamentoPage),
  ],
})
export class FinalizarPagamentoPageModule {}
