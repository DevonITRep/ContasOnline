import { Component, ViewChild } from '@angular/core';
import { Nav, Platform, AlertController  } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';

import { LoginPage } from '../pages/login/login';
import { CartoesPage } from '../pages/cartoes/cartoes';
import { BancosPage } from '../pages/bancos/bancos';
import { DespesasPage } from '../pages/despesas/despesas';
import { OneSignal } from '@ionic-native/onesignal';
import { isCordovaAvailable } from '../common/available';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  rootPage: any = HomePage;

  static APP_ID: string = "61d48b18-8508-4578-a8ce-148a6347a98a";
  static SENDER_ID: string = "963726746006";


  pages: Array<{title: string, component: any, icon : string}>;

  constructor(public platform: Platform, public statusBar: StatusBar, public splashScreen: SplashScreen,private oneSignal: OneSignal,private alertCtrl: AlertController) {
    this.initializeApp();

    // used for an example of ngFor and navigation
    this.pages = [
      { title: 'Home', component: HomePage , icon: 'home' },
      { title: 'List', component: ListPage, icon: 'partly-sunny' },
      { title: 'Login', component: LoginPage, icon: 'partly-sunny' },
      { title: 'Cartões', component: CartoesPage, icon: 'partly-sunny' },
      { title: 'Bancos', component: BancosPage, icon: 'partly-sunny' },
      { title: 'Despesas', component: DespesasPage, icon: 'partly-sunny' },
     
    ];

  }

  initializeApp() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.styleDefault();
      this.splashScreen.hide();

      this.handlerNotifications();
       
    });

  }

  private handlerNotifications() {

    if (isCordovaAvailable())
    {
        this.oneSignal.startInit(MyApp.APP_ID, MyApp.SENDER_ID);

        this.oneSignal.handleNotificationOpened()
            .subscribe(jsonData => {
              let alert = this.alertCtrl.create({
                title: jsonData.notification.payload.title,
                subTitle: jsonData.notification.payload.body,
                buttons: ['OK']
              });
              alert.present();
              console.log('notificationOpenedCallback: ' + JSON.stringify(jsonData));
            });
        this.oneSignal.endInit();
      }

  }

  openPage(page) {
    // Reset the content nav to have just this page
    // we wouldn't want the back button to show in this scenario
    this.nav.setRoot(page.component);
  }
}
