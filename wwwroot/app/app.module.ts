import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import 'rxjs/Rx';
import { UploadService } from './app.service';
import { uploadService } from './upload.service';
import { APP_CONFIG, AppConfig } from './app.config';


import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.components';
import { NavBarComponent } from './components/navbar/navbar.components';
import { LoginComponent } from './components/login/login.component';
import { ClaimsComponent } from './components/claims/claims.component';
import { QuotesComponent } from './components/quotes/quotes.component';
import { TestComponent } from './components/test/test.component';
import { MapComponent } from './components/map/map.component';

import { AppRouting } from './app.routing';
import { AuthHttp } from "./auth.http";
import { AuthService } from "./auth.service";


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        NavBarComponent,
        LoginComponent,
        ClaimsComponent,
        QuotesComponent,
        TestComponent,
        MapComponent
    ],
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        AppRouting 
    ],
    providers: [
        AuthService,
        AuthHttp,
        UploadService,
        uploadService,
        { provide: APP_CONFIG, useValue: AppConfig }
    ],
    bootstrap: [
        AppComponent
    ],
})
export class AppModule { }
