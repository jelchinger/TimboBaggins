import { ModuleWithProviders } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { HomeComponent } from './components/home/home.components';
import { QuotesComponent } from './components/quotes/quotes.component';
import { ClaimsComponent } from './components/claims/claims.component';
import { LoginComponent } from './components/login/login.component';
import { TestComponent } from './components/test/test.component';
import { MapComponent } from './components/map/map.component';
import { AuthService } from "./auth.service";



const appRoutes: Routes = [
    {
        path: "",
        component: HomeComponent
    },
    {
        path: "home",
        redirectTo: ""
    },
    {
        path: "quotes",
        component: QuotesComponent
    },
    {
        path: "claims",
        component: ClaimsComponent
    },
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "test",
        component: TestComponent
    },
    {
        path: "map",
        component: MapComponent
    }
];

export const AppRoutingProviders: any[] = [
];

export const AppRouting: ModuleWithProviders = RouterModule.forRoot(appRoutes);
