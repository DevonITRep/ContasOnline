import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { PagamentosPagosPage } from './pagamentos-pagos';

@NgModule({
  declarations: [
    PagamentosPagosPage,
  ],
  imports: [
    IonicPageModule.forChild(PagamentosPagosPage),
  ],
})
export class PagamentosPagosPageModule {}
