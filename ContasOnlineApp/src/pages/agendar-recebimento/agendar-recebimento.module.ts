import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { AgendarRecebimentoPage } from './agendar-recebimento';

@NgModule({
  declarations: [
    AgendarRecebimentoPage,
  ],
  imports: [
    IonicPageModule.forChild(AgendarRecebimentoPage),
  ],
})
export class AgendarRecebimentoPageModule {}
