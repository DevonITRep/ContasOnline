import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { UsuarioService } from '../services/usuarioService';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';
import { AutenticacaoPage } from '../pages/autenticacao/autenticacao';
import { LoginPage } from '../pages/login/login';
import { Facebook } from '@ionic-native/facebook';
import { SignupPage } from '../pages/signup/signup';
import { CartoesPage } from '../pages/cartoes/cartoes';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { DataService } from '../services/dataService';
import { Configuration } from '../app/app.constants';
import { SlimLoadingBarService } from 'ng2-slim-loading-bar';
import { CurrencyMaskModule } from "ng2-currency-mask";
import { CurrencyMaskConfig, CURRENCY_MASK_CONFIG } from "ng2-currency-mask/src/currency-mask.config";
import { CurrencyPipe } from '@angular/common';
import { BancosPage } from '../pages/bancos/bancos'; 
import { DespesasPage } from '../pages/despesas/despesas';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import {CustomInterceptor} from '../../src/services/dataService';

export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
    align: "right",
    allowNegative: true,
    decimal: ",",
    precision: 2,
    prefix: "R$ ",
    suffix: "",
    thousands: "."
};


@NgModule({
  declarations: [
    MyApp,
    HomePage,
    ListPage,
    AutenticacaoPage,
    LoginPage,
    SignupPage,
    CartoesPage,
    BancosPage,
    DespesasPage
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(MyApp),
    HttpClientModule,
    CurrencyMaskModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    ListPage,
    AutenticacaoPage,
    LoginPage,
    SignupPage,
    CartoesPage,
    BancosPage,
    DespesasPage
  ],
  providers: [
	  UsuarioService,
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    Facebook,
    DataService,
    HttpClient,
    HttpClientModule,
    Configuration,
    SlimLoadingBarService,
    { provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig },
    { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true },
    CurrencyPipe
  ]
})
export class AppModule {}
