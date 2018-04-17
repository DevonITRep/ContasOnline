import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { InvestimentosPage } from './investimentos';

@NgModule({
  declarations: [
    InvestimentosPage,
  ],
  imports: [
    IonicPageModule.forChild(InvestimentosPage),
  ],
})
export class InvestimentosPageModule {}
