import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { PagamentosPendentesPage } from './pagamentos-pendentes';

@NgModule({
  declarations: [
    PagamentosPendentesPage,
  ],
  imports: [
    IonicPageModule.forChild(PagamentosPendentesPage),
  ],
})
export class PagamentosPendentesPageModule {}
