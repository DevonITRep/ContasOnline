import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { AgendarPagamentoPage } from './agendar-pagamento';

@NgModule({
  declarations: [
    AgendarPagamentoPage,
  ],
  imports: [
    IonicPageModule.forChild(AgendarPagamentoPage),
  ],
})
export class AgendarPagamentoPageModule {}
