import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { UsuarioService } from '../services/usuarioService';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';
import { LoginPage } from '../pages/login/login';
import { Facebook } from '@ionic-native/facebook';
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
import { IonicStorageModule } from '@ionic/storage';
import { RegisterPage } from "../pages/register/register";
import {NotificationsPage} from "../pages/notifications/notifications";
import {SearchLocationPage} from "../pages/search-location/search-location";
import {TripDetailPage} from "../pages/trip-detail/trip-detail";
import {TripsPage} from "../pages/trips/trips";
import {SettingsPage} from "../pages/settings/settings";
import {CheckoutTripPage} from "../pages/checkout-trip/checkout-trip";
import { Device } from '@ionic-native/device';
import { Firebase } from '@ionic-native/firebase';
import { AngularFireModule } from 'angularfire2';
import { AngularFireAuth } from 'angularfire2/auth';
import { firebaseConfig } from '../config';
import { AuthService } from '../services/auth.service';


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
    LoginPage,
    CartoesPage,
    BancosPage,
    DespesasPage,
    NotificationsPage,
    SearchLocationPage,
    TripDetailPage,
    TripsPage,
    SettingsPage,
    CheckoutTripPage,
    RegisterPage
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(MyApp),
    HttpClientModule,
    CurrencyMaskModule,
    IonicStorageModule.forRoot({
      name: '__ionic3_start_theme',
        driverOrder: ['indexeddb', 'sqlite', 'websql']
    }),
    AngularFireModule.initializeApp(firebaseConfig.fire)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    ListPage,
    LoginPage,
    CartoesPage,
    BancosPage,
    DespesasPage,
    NotificationsPage,
    SearchLocationPage,
    TripDetailPage,
    TripsPage,
    SettingsPage,
    CheckoutTripPage,
    RegisterPage
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
    CurrencyPipe,
    Device,
    Firebase,
    AngularFireAuth,
    AuthService

  ]
})
export class AppModule {}
