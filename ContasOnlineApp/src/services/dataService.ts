import 'rxjs/add/operator/map';

import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { Configuration } from '../app/app.constants';

@Injectable()
export class DataService {

    private actionUrl: string;
    private _apiName: string;

    constructor(private http: HttpClient, private _configuration: Configuration) {
        this.actionUrl = _configuration.ServerWithApiUrl;
    }

    public setServiceApiName(apiName: string){
         this._apiName = apiName;
    }

    public getAll<T>(): Observable<T> {
        console.log('URL :=' + this.actionUrl + this._apiName);
        return this.http.get<T>(this.actionUrl + this._apiName + '/');
    }

    public getSingle<T>(id: string): Observable<T> {
        return this.http.get<T>(this.actionUrl+ this._apiName + '/' + id);
    }

    public add<T>(itemName: any): Observable<T> {
        const toAdd = JSON.stringify(itemName);
        console.log('Objeto :=' + toAdd);    
        return this.http.post<T>(this.actionUrl + this._apiName + '/', toAdd);
    }

    public update<T>(id: string, itemToUpdate: any): Observable<T> {
        return this.http
            .put<T>(this.actionUrl + this._apiName + '/' + id, JSON.stringify(itemToUpdate));
    }

    public delete<T>(id: number): Observable<T> {
        return this.http.delete<T>(this.actionUrl + this._apiName + '/' + id);
    }
}


@Injectable()
export class CustomInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!req.headers.has('Content-Type')) {
            req = req.clone({ headers: req.headers.set('Content-Type', 'application/json') });
        }

        req = req.clone({ headers: req.headers.set('Accept', 'application/json') });
        console.log(JSON.stringify(req.headers));
        return next.handle(req);
    }
}