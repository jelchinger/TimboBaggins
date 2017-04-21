import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { Http, Response, Headers } from '@angular/http';
import { APP_CONFIG, IAppConfig } from './app.config';
import { Component, Inject } from '@angular/core';

@Injectable()
export class uploadService {

    constructor(private http: Http, @Inject(APP_CONFIG) private config: IAppConfig) { }
   
    upload(fileToUpload: any) {

        let input = new FormData();
        input.append("file", fileToUpload);

        return this.http
            .post(this.config.apiEndpoint + "uploadFile", input)
            .do(x => console.log(x));
    }
}