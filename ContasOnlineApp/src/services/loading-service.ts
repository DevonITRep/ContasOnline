import { Injectable } from '@angular/core';
import { LoadingController } from 'ionic-angular'

@Injectable()
export class LoadingService {

    loader: any = null;
    constructor(private _loadingController: LoadingController) {
    }
  
    private showLoadingHandler(message) {
        if (this.loader == null) {
            this.loader = this._loadingController.create({
                content: message
            });
            this.loader.present();
        } else {
            this.loader.data.content = message;
        }
    }
  
    private hideLoadingHandler() {
        if (this.loader != null) {
            this.loader.dismiss();
            this.loader = null;
        }
    }
  
    public showLoader(message) {
        this.showLoadingHandler(message);
    }
  
    public hideLoader() {
        this.hideLoadingHandler();
    }
  }