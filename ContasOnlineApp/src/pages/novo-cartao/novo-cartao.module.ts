import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { NovoCartaoPage } from './novo-cartao';

@NgModule({
  declarations: [
    NovoCartaoPage,
  ],
  imports: [
    IonicPageModule.forChild(NovoCartaoPage),
  ],
})
export class NovoCartaoPageModule {}
